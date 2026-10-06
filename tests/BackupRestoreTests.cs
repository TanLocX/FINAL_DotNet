using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using FINAL_DotNet;

internal static class BackupRestoreTests
{
    private static string server;
    private static string database;
    private static string directory;
    private static int assertions;
    private static int failures;
    private static readonly List<string> Databases = new List<string>();

    private static int Main(string[] args)
    {
        server = args.Length > 0 ? args[0] : @"(localdb)\MSSQLLocalDB";
        database = "PNJ_BackupRestoreTests_" + Guid.NewGuid().ToString("N");
        directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "backup-output", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        string[] variables = { "PNJ_DB_SERVER", "PNJ_DB_NAME", "PNJ_DB_USER", "PNJ_DB_PASSWORD" };
        string[] previous = variables.Select(Environment.GetEnvironmentVariable).ToArray();
        try
        {
            Environment.SetEnvironmentVariable("PNJ_DB_SERVER", server);
            Environment.SetEnvironmentVariable("PNJ_DB_NAME", database);
            Environment.SetEnvironmentVariable("PNJ_DB_USER", null);
            Environment.SetEnvironmentVariable("PNJ_DB_PASSWORD", null);
            CreateDatabase(database);
            Run("server information and backup history", CheckServerInformation);
            Run("uncompressed backup, restore and safety snapshot", CheckRoundTrip);
            Run("compression fallback with progress callback", CheckCompressionFallback);
            Run("failed backup must not verify an older backup as success", CheckFailedBackup);
            Run("missing, corrupt and foreign backup rejected before restore", CheckInvalidBackups);
            Run("failed restore throws and returns database to MULTI_USER", CheckFailedRestore);
            Run("multiple backup sets restore the selected historical version", CheckMultipleSets);
            Run("path validation and system database protection", CheckValidation);
            Console.WriteLine((failures == 0 ? "PASS" : "FAIL") + ": " + assertions + " assertions, " + failures + " failed scenarios.");
            return failures == 0 ? 0 : 1;
        }
        catch (Exception error) { Console.Error.WriteLine(error); return 1; }
        finally
        {
            SqlConnection.ClearAllPools();
            foreach (string name in Databases)
            {
                Execute("master", "IF DB_ID(@name) IS NOT NULL BEGIN ALTER DATABASE [" + name +
                    "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [" + name + "]; END",
                    new SqlParameter("@name", name));
                // Remove only this suite's history entries from msdb.
                Execute("msdb", "EXEC dbo.sp_delete_database_backuphistory @database_name = @name", new SqlParameter("@name", name));
            }
            for (int i = 0; i < variables.Length; i++) Environment.SetEnvironmentVariable(variables[i], previous[i]);
            // All files in this fresh GUID directory were created by this suite.
            foreach (string file in Directory.GetFiles(directory)) File.Delete(file);
            Directory.Delete(directory);
            Console.WriteLine("Disposed temporary databases and backup files.");
        }
    }

    private static SqlConnection Connection(string catalog)
    {
        return new SqlConnection(new SqlConnectionStringBuilder { DataSource = server, InitialCatalog = catalog,
            IntegratedSecurity = true, TrustServerCertificate = true, MultipleActiveResultSets = true }.ConnectionString);
    }

    private static void Execute(string catalog, string sql, params SqlParameter[] parameters)
    {
        using (var connection = Connection(catalog))
        using (var command = new SqlCommand(sql, connection))
        {
            command.CommandTimeout = 60;
            command.Parameters.AddRange(parameters);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    private static T Scalar<T>(string catalog, string sql)
    {
        using (var connection = Connection(catalog))
        using (var command = new SqlCommand(sql, connection))
        {
            connection.Open();
            return (T)Convert.ChangeType(command.ExecuteScalar(), typeof(T));
        }
    }

    private static void CreateDatabase(string name)
    {
        Databases.Add(name);
        Execute("master", "CREATE DATABASE [" + name + "]");
        Execute(name, "CREATE TABLE dbo.BackupProbe (Id INT PRIMARY KEY, Marker INT NOT NULL); INSERT dbo.BackupProbe VALUES (1, 10);");
    }

    private static void Marker(int value) { Execute(database, "UPDATE dbo.BackupProbe SET Marker = @value", new SqlParameter("@value", value)); }
    private static int Marker() { return Scalar<int>(database, "SELECT Marker FROM dbo.BackupProbe WHERE Id = 1"); }

    private static void Assert(bool condition, string label)
    {
        if (!condition) throw new Exception(label);
        assertions++;
    }

    private static void Run(string name, Action test)
    {
        try { test(); Console.WriteLine("PASS: " + name); }
        catch (Exception error) { failures++; Console.Error.WriteLine("FAIL: " + name + "\n" + error.GetBaseException().Message); }
    }

    private static void MustFail(Action action, string label)
    {
        bool failed = false;
        try { action(); } catch (Exception) { failed = true; }
        Assert(failed, label);
    }

    private static DataTable Header(string file)
    {
        using (var connection = Connection("master"))
        using (var command = new SqlCommand("RESTORE HEADERONLY FROM DISK = @file", connection))
        using (var adapter = new SqlDataAdapter(command))
        {
            command.Parameters.AddWithValue("@file", file);
            var table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }

    private static string Restore(string file, int? position = null, Action<string> progress = null)
    {
        return SaoLuuPhucHoiService.PhucHoi(file, directory, false, progress, position);
    }

    private static void CheckServerInformation()
    {
        var info = SaoLuuPhucHoiService.LayThongTinMayChu();
        Assert(info.TenCoSoDuLieu == database, "Uses the configured disposable database");
        Assert(info.CoQuyenSaoLuu && info.CoQuyenPhucHoi, "Local test login can back up and restore");
        Assert(SaoLuuPhucHoiService.LayLichSuSaoLuu().Count == 0, "New database has no backup history");
        Assert(SaoLuuPhucHoiService.TaoTenFileSaoLuu("..bad/name").EndsWith(".bak"), "Generated filename sanitized");
    }

    private static void CheckRoundTrip()
    {
        Marker(10);
        var messages = new List<string>();
        string file = SaoLuuPhucHoiService.TaoSaoLuu(directory, "roundtrip.bak", false, messages.Add);
        Assert(File.Exists(file), "Backup file created");
        DataRow header = Header(file).Rows[0];
        Assert(Convert.ToBoolean(header["IsCopyOnly"]), "Backup is copy-only");
        Assert(Convert.ToBoolean(header["HasBackupChecksums"]), "Backup has checksums");
        Assert(!Convert.ToBoolean(header["Compressed"]), "Uncompressed option honored");
        Assert(messages.Any(message => message.Contains("percent") || message.Contains("phần trăm")), "SQL progress delivered");
        var history = SaoLuuPhucHoiService.LayLichSuSaoLuu();
        Assert(history.Any(item => item.DuongDan == file && item.KichThuocMb > 0), "History includes completed backup");
        Marker(20);
        string safety = Restore(file, null, messages.Add);
        Assert(Marker() == 10, "Restored backed-up data");
        Assert(safety != file && File.Exists(safety), "Safety snapshot created before restore");
        Assert(Scalar<string>("master", "SELECT user_access_desc FROM sys.databases WHERE name = '" + database + "'") == "MULTI_USER", "Successful restore returns MULTI_USER");
        Restore(safety);
        Assert(Marker() == 20, "Safety snapshot contains pre-restore data");
    }

    private static void CheckCompressionFallback()
    {
        Marker(30);
        var messages = new List<string>();
        string file = SaoLuuPhucHoiService.TaoSaoLuu(directory, "compression.bak", true, messages.Add);
        Assert(File.Exists(file), "Compression request still produces a usable backup");
        DataRow row = Header(file).Rows[0];
        bool compressed = Convert.ToBoolean(row["Compressed"]);
        Assert(compressed || messages.Any(message => message.Contains("NO_COMPRESSION")), "Unsupported compression reported and retried");
        Marker(31);
        messages.Clear();
        string safety = SaoLuuPhucHoiService.PhucHoi(file, directory, true, messages.Add);
        Assert(Marker() == 30, "Compression or fallback backup contains current data");
        DataTable safetyHeader = Header(safety);
        bool safetyCompressed = Convert.ToBoolean(safetyHeader.Rows[safetyHeader.Rows.Count - 1]["Compressed"]);
        Assert(safetyCompressed || messages.Any(message => message.Contains("NO_COMPRESSION")), "Safety snapshot supports compression fallback with progress");
        Restore(safety);
        Assert(Marker() == 31, "Fallback safety snapshot preserves pre-restore data");
    }

    private static void CheckFailedBackup()
    {
        string file = SaoLuuPhucHoiService.TaoSaoLuu(directory, "existing.bak", false);
        int previousSets = Header(file).Rows.Count;
        Marker(40);
        bool failed = false;
        // Allow reading the old set, but deny the write needed to append a new backup.
        using (var readLock = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            try { SaoLuuPhucHoiService.TaoSaoLuu(directory, "existing.bak", false, message => { }); }
            catch (Exception) { failed = true; }
        }
        Assert(failed, "Failed backup cannot return success by verifying an older set");
        Assert(Header(file).Rows.Count == previousSets, "Failed backup leaves older backup intact");
        Assert(Marker() == 40, "Failed backup preserves live data");
    }

    private static void CheckInvalidBackups()
    {
        Marker(50);
        int safetyFiles = Directory.GetFiles(directory, "TruocPhucHoi_*.bak").Length;
        MustFail(() => Restore(Path.Combine(directory, "missing.bak")), "Missing backup rejected");
        string corrupt = Path.Combine(directory, "corrupt.bak");
        File.WriteAllText(corrupt, "not a SQL Server backup");
        MustFail(() => Restore(corrupt), "Invalid backup rejected");
        string truncated = SaoLuuPhucHoiService.TaoSaoLuu(directory, "truncated.bak", false);
        using (var stream = new FileStream(truncated, FileMode.Open, FileAccess.Write))
            stream.SetLength(stream.Length / 2);
        Assert(Header(truncated).Rows.Count == 1, "Truncated backup still has a readable header");
        MustFail(() => Restore(truncated), "VERIFYONLY rejects a valid-header backup with missing payload");
        string foreignDatabase = database + "_Other";
        CreateDatabase(foreignDatabase);
        string foreignFile = Path.Combine(directory, "foreign.bak");
        Execute("master", "BACKUP DATABASE [" + foreignDatabase + "] TO DISK = @file WITH COPY_ONLY, CHECKSUM, NO_COMPRESSION",
            new SqlParameter("@file", foreignFile));
        MustFail(() => Restore(foreignFile), "Backup of another database rejected");
        Assert(Marker() == 50, "Invalid inputs preserve live data");
        Assert(Directory.GetFiles(directory, "TruocPhucHoi_*.bak").Length == safetyFiles, "Invalid inputs rejected before safety backup and SINGLE_USER");
    }

    private static void CheckFailedRestore()
    {
        Marker(60);
        string file = SaoLuuPhucHoiService.TaoSaoLuu(directory, "restore-failure.bak", false);
        string hidden = file + ".hidden";
        Marker(61);
        bool moved = false;
        bool failed = false;
        try
        {
            try
            {
                Restore(file, null, message =>
                {
                    if (!moved && message.StartsWith("Đang ngắt các kết nối"))
                    {
                        File.Move(file, hidden);
                        moved = true;
                    }
                });
            }
            catch (Exception) { failed = true; }
        }
        finally { if (moved) File.Move(hidden, file); }
        Assert(moved, "Failure injected after validation and safety backup");
        Assert(failed, "Failed RESTORE cannot report success");
        Assert(Marker() == 61, "Failed restore preserves original data");
        Assert(Scalar<string>("master", "SELECT user_access_desc FROM sys.databases WHERE name = '" + database + "'") == "MULTI_USER", "Failed restore returns MULTI_USER");
    }

    private static void CheckMultipleSets()
    {
        Marker(70);
        string file = SaoLuuPhucHoiService.TaoSaoLuu(directory, "multiple.bak", false);
        Marker(71);
        SaoLuuPhucHoiService.TaoSaoLuu(directory, "multiple.bak", false);
        Assert(Header(file).Rows.Count == 2, "Appending backup preserves both versions");
        var positions = SaoLuuPhucHoiService.LayLichSuSaoLuu().Where(item => item.DuongDan == file)
            .Select(item => item.ViTriBanSao).OrderBy(position => position).ToArray();
        Assert(positions.SequenceEqual(new[] { 1, 2 }), "History identifies each backup set inside the file");
        Marker(72);
        Restore(file, 1);
        Assert(Marker() == 70, "Selecting first historical version restores first set");
        Restore(file);
        Assert(Marker() == 71, "Manual file selection restores latest matching set");
        int before = Marker();
        MustFail(() => Restore(file, 99), "Nonexistent backup position rejected");
        Assert(Marker() == before, "Invalid backup position preserves data");
    }

    private static void CheckValidation()
    {
        string relative = "relative_" + Guid.NewGuid().ToString("N");
        MustFail(() => SaoLuuPhucHoiService.TaoSaoLuu(relative, "test.bak", false), "Relative backup directory rejected");
        Assert(!Directory.Exists(relative), "Invalid backup directory rejected before creating it");
        MustFail(() => SaoLuuPhucHoiService.TaoSaoLuu(directory, "../test.bak", false), "Filename traversal rejected");
        MustFail(() => Restore("relative.bak"), "Relative restore path rejected");
        string current = Environment.GetEnvironmentVariable("PNJ_DB_NAME");
        try
        {
            Environment.SetEnvironmentVariable("PNJ_DB_NAME", "master");
            MustFail(() => SaoLuuPhucHoiService.TaoSaoLuu(directory, "system.bak", false), "System database backup blocked");
            MustFail(() => Restore(Path.Combine(directory, "roundtrip.bak")), "System database restore blocked");
        }
        finally { Environment.SetEnvironmentVariable("PNJ_DB_NAME", current); }
    }
}
