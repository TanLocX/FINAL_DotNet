using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FINAL_DotNet;

// Integration tests against a disposable SQL Server database, never the business database.
internal static class IdentityCodeGenerationTests
{
    private static string server;
    private static string database;
    private static int checks;
    private static int unique;
    private static readonly Dictionary<Type, Func<object>> Factories = new Dictionary<Type, Func<object>>
    {
        { typeof(DanhMuc), () => new DanhMuc { TenDanhMuc = Name(), DangHoatDong = true } },
        { typeof(ChatLieu), () => new ChatLieu { TenChatLieu = Name(), DangHoatDong = true } },
        { typeof(NhaCungCap), () => NewSupplier() },
        { typeof(NhanVien), () => NewStaff() },
        { typeof(KhachHang), () => NewCustomer() },
        { typeof(TaiKhoan), () => new TaiKhoan { NhanVien = NewStaff(), TenDangNhap = Name(),
            MatKhauHash = "test", VaiTro = "NHANVIEN", DangHoatDong = true } },
        { typeof(SanPham), () => NewProduct() },
        { typeof(HoaDon), () => NewInvoice() },
        { typeof(ChiTietHoaDon), () => new ChiTietHoaDon { HoaDonId = 1, SanPham = NewProduct(),
            SoLuong = 1, DonGiaBan = 10 } },
        { typeof(PhieuNhap), () => NewReceipt() },
        { typeof(ChiTietPhieuNhap), () => new ChiTietPhieuNhap { PhieuNhapId = 1, SanPham = NewProduct(),
            SoLuong = 1, DonGiaNhap = 10 } },
        { typeof(PhieuThuMua), () => NewPurchase() },
        { typeof(ChiTietPhieuThuMua), () => new ChiTietPhieuThuMua { PhieuThuMuaId = 1, ChatLieuId = 1,
            TenSanPhamThu = Name(), TrongLuong = 1, DonViTinh = "gram", DonGiaThuMua = 10 } },
        { typeof(PhieuBaoHanh), () => new PhieuBaoHanh { ChiTietHoaDonId = 1, NgayTiepNhan = DateTime.Today,
            NoiDungBaoHanh = Name(), TrangThai = "TIEP_NHAN" } },
        { typeof(MauEmail), () => NewTemplate() },
        { typeof(NhatKyGuiEmail), () => new NhatKyGuiEmail { TaiKhoanId = 1, ThoiGianGui = DateTime.Now,
            EmailNhan = "test@example.com", TieuDe = Name(), LoaiGui = "DON", TrangThai = "THANH_CONG" } }
    };

    private static int Main(string[] args)
    {
        server = args.Length > 0 ? args[0] : @"(localdb)\MSSQLLocalDB";
        database = "PNJ_CodeGenerationTests_" + Guid.NewGuid().ToString("N");
        try
        {
            string root = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
            string script = File.ReadAllText(Path.Combine(root, "Database", "01_CreateDatabase.sql"))
                .Replace("QL_CuaHangDaQuy_PNJ", database);
            using (var connection = new SqlConnection(ConnectionString("master")))
            {
                connection.Open();
                foreach (string batch in Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase))
                    if (!string.IsNullOrWhiteSpace(batch))
                        using (var command = new SqlCommand(batch, connection)) command.ExecuteNonQuery();
            }

            SeedParentsAndCheckNewTables();
            foreach (Type type in Factories.Keys) CheckDeleteAndRecreate(type);
            CheckEmptyAndTruncatedTables();
            CheckBigIntIdentity();
            CheckRollbackAndFailedInsert();
            CheckMultipleDetailsAndCompositeKey();
            CheckAsyncSaves().GetAwaiter().GetResult();
            CheckConcurrentSaves();
            Console.WriteLine("PASS: " + checks + " assertions; all 16 identity tables, empty/truncated tables, " +
                "BIGINT, rollback, failed insert, parent/detail transaction, composite key, async and concurrent inserts.");
            return 0;
        }
        catch (Exception error)
        {
            Console.Error.WriteLine(error);
            return 1;
        }
        finally
        {
            SqlConnection.ClearAllPools();
            using (var connection = new SqlConnection(ConnectionString("master")))
            {
                connection.Open();
                using (var command = new SqlCommand("IF DB_ID(@name) IS NOT NULL BEGIN ALTER DATABASE [" +
                    database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [" + database + "]; END", connection))
                {
                    command.Parameters.AddWithValue("@name", database);
                    command.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Disposed test database: " + database);
        }
    }

    private static string ConnectionString(string catalog)
    {
        return new SqlConnectionStringBuilder { DataSource = server, InitialCatalog = catalog,
            IntegratedSecurity = true, MultipleActiveResultSets = true, TrustServerCertificate = true }.ConnectionString;
    }

    private static QL_CuaHangDaQuy_PNJEntities Context()
    {
        var builder = new EntityConnectionStringBuilder { Provider = "System.Data.SqlClient",
            ProviderConnectionString = ConnectionString(database),
            Metadata = "res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl" };
        return new QL_CuaHangDaQuy_PNJEntities(builder.ConnectionString);
    }

    private static string Name() { return "TEST_" + Interlocked.Increment(ref unique); }
    private static NhanVien NewStaff() { return new NhanVien { HoTen = Name(), ChucVu = "Test", DangLamViec = true }; }
    private static KhachHang NewCustomer() { return new KhachHang { HoTen = Name(), SoDienThoai = Name(), DangHoatDong = true }; }
    private static NhaCungCap NewSupplier() { return new NhaCungCap { TenNhaCungCap = Name(), SoDienThoai = Name(), DangHoatDong = true }; }
    private static SanPham NewProduct() { return new SanPham { DanhMucId = 1, TenSanPham = Name(), GiaVon = 5, GiaBan = 10, DangKinhDoanh = true }; }
    private static HoaDon NewInvoice() { return new HoaDon { NhanVienId = 1, KhachHangId = 1,
        NgayLap = DateTime.Now, PhuongThucThanhToan = "Test", TrangThai = "DA_THANH_TOAN" }; }
    private static PhieuNhap NewReceipt() { return new PhieuNhap { NhanVienId = 1, NhaCungCapId = 1,
        NgayNhap = DateTime.Now, TrangThai = "HOAN_THANH" }; }
    private static PhieuThuMua NewPurchase() { return new PhieuThuMua { NhanVienId = 1, KhachHangId = 1,
        NgayThuMua = DateTime.Now, TrangThai = "HOAN_THANH" }; }
    private static MauEmail NewTemplate() { return new MauEmail { TenMau = Name(), TieuDeMau = "Test",
        NoiDungMau = "Test", NgayCapNhat = DateTime.Now, DangHoatDong = true }; }

    private static long Id(object entity)
    {
        Type type = System.Data.Entity.Core.Objects.ObjectContext.GetObjectType(entity.GetType());
        return Convert.ToInt64(type.GetProperty(type.Name + "Id").GetValue(entity));
    }

    private static long Maximum(Type type)
    {
        using (var db = Context())
            return db.Database.SqlQuery<long>("SELECT COALESCE(MAX(CONVERT(BIGINT, [" + type.Name +
                "Id])), 0) FROM dbo.[" + type.Name + "]").Single();
    }

    private static object Insert(Type type)
    {
        object entity = Factories[type]();
        using (var db = Context()) { db.Set(type).Add(entity); db.SaveChanges(); }
        return entity;
    }

    private static void Delete(object entity)
    {
        Type type = entity.GetType();
        using (var db = Context())
        {
            object stored = db.Set(type).Find(type == typeof(NhatKyGuiEmail) ? (object)Id(entity) : (int)Id(entity));
            db.Set(type).Remove(stored);
            db.SaveChanges();
        }
    }

    private static void Equal(long expected, long actual, string label)
    {
        if (expected != actual) throw new Exception(label + ": expected " + expected + ", got " + actual);
        checks++;
    }

    private static void SeedParentsAndCheckNewTables()
    {
        // Insert in dependency order so all reference IDs are 1.
        foreach (Type type in new[] { typeof(DanhMuc), typeof(ChatLieu), typeof(NhaCungCap), typeof(NhanVien),
            typeof(KhachHang), typeof(SanPham), typeof(HoaDon), typeof(PhieuNhap), typeof(PhieuThuMua),
            typeof(ChiTietHoaDon), typeof(ChiTietPhieuNhap), typeof(ChiTietPhieuThuMua), typeof(PhieuBaoHanh),
            typeof(MauEmail), typeof(NhatKyGuiEmail), typeof(TaiKhoan) })
        {
            // The email log needs its sender first.
            if (type == typeof(NhatKyGuiEmail))
            {
                using (var db = Context())
                {
                    db.TaiKhoans.Add(new TaiKhoan { NhanVienId = 1, TenDangNhap = Name(), MatKhauHash = "test",
                        VaiTro = "ADMIN", DangHoatDong = true });
                    db.SaveChanges();
                }
            }
            if (type == typeof(TaiKhoan)) { Equal(1, Maximum(type), "New TaiKhoan seed"); continue; }
            Equal(1, Id(Insert(type)), "New " + type.Name + " seed");
        }
    }

    private static void CheckDeleteAndRecreate(Type type)
    {
        long baseline = Maximum(type);
        object first = Insert(type);
        object last = Insert(type);
        Equal(baseline + 1, Id(first), type.Name + " first insert");
        Equal(baseline + 2, Id(last), type.Name + " second insert");
        Delete(last);
        object replacement = Insert(type);
        Equal(Id(last), Id(replacement), type.Name + " reuse deleted maximum");
        Delete(first);
        object afterGap = Insert(type);
        Equal(baseline + 3, Id(afterGap), type.Name + " do not fill interior gap");
        Delete(afterGap);
        Delete(replacement);
        Console.WriteLine("PASS: " + type.Name + " delete/recreate");
    }

    private static void CheckEmptyAndTruncatedTables()
    {
        using (var db = Context()) db.Database.ExecuteSqlCommand("DELETE FROM dbo.MauEmail");
        Equal(1, Id(Insert(typeof(MauEmail))), "Restart after DELETE all");
        using (var db = Context()) db.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.NhatKyGuiEmail");
        Equal(1, Id(Insert(typeof(NhatKyGuiEmail))), "Restart after TRUNCATE");
        using (var db = Context()) { db.MauEmails.Single().DangHoatDong = false; db.SaveChanges(); }
        Equal(2, Id(Insert(typeof(MauEmail))), "Inactive row remains in maximum");
    }

    private static void CheckBigIntIdentity()
    {
        using (var db = Context()) db.Database.ExecuteSqlCommand(@"
SET IDENTITY_INSERT dbo.NhatKyGuiEmail ON;
INSERT dbo.NhatKyGuiEmail (NhatKyGuiEmailId, TaiKhoanId, ThoiGianGui, EmailNhan, TieuDe, LoaiGui, TrangThai)
VALUES (3000000000, 1, GETDATE(), 'test@example.com', N'Test', 'DON', 'THANH_CONG');
SET IDENTITY_INSERT dbo.NhatKyGuiEmail OFF;");
        object record = Insert(typeof(NhatKyGuiEmail));
        Equal(3000000001, Id(record), "BIGINT greater than INT");
        Delete(record);
        Equal(3000000001, Id(Insert(typeof(NhatKyGuiEmail))), "BIGINT maximum reuse");
    }

    private static void CheckRollbackAndFailedInsert()
    {
        long baseline = Maximum(typeof(DanhMuc));
        using (var db = Context())
        using (var transaction = db.Database.BeginTransaction())
        {
            var category = new DanhMuc { TenDanhMuc = Name(), DangHoatDong = true };
            db.DanhMucs.Add(category);
            db.SaveChanges();
            Equal(baseline + 1, category.DanhMucId, "Inside existing transaction");
            transaction.Rollback();
        }
        Equal(baseline, Maximum(typeof(DanhMuc)), "Rollback preserves data");
        Equal(baseline + 1, Id(Insert(typeof(DanhMuc))), "Reuse rolled back identity");

        using (var db = Context())
        {
            var invalid = new DanhMuc { TenDanhMuc = db.DanhMucs.First().TenDanhMuc, DangHoatDong = true };
            db.DanhMucs.Add(invalid);
            bool failed = false;
            try { db.SaveChanges(); } catch (DbUpdateException) { failed = true; }
            Equal(1, failed ? 1 : 0, "Unique constraint still enforced");
        }
        Equal(baseline + 2, Id(Insert(typeof(DanhMuc))), "Reuse failed insert identity");
    }

    private static void CheckMultipleDetailsAndCompositeKey()
    {
        long detailMaximum = Maximum(typeof(ChiTietHoaDon));
        using (var db = Context())
        using (var transaction = db.Database.BeginTransaction())
        {
            var invoice = NewInvoice();
            db.HoaDons.Add(invoice);
            db.SaveChanges();
            var details = new[] {
                new ChiTietHoaDon { HoaDonId = invoice.HoaDonId, SanPhamId = 1, SoLuong = 1, DonGiaBan = 10 },
                new ChiTietHoaDon { HoaDonId = invoice.HoaDonId, SanPhamId = 2, SoLuong = 1, DonGiaBan = 10 } };
            db.ChiTietHoaDons.AddRange(details);
            db.ChiTietChatLieux.Add(new ChiTietChatLieu { SanPhamId = 1, ChatLieuId = 1, TrongLuong = 1, DonViTinh = "gram" });
            db.SaveChanges();
            Equal(detailMaximum + 1, details.Min(detail => detail.ChiTietHoaDonId), "Batch detail first ID");
            Equal(detailMaximum + 2, details.Max(detail => detail.ChiTietHoaDonId), "Batch detail last ID");
            transaction.Commit();
        }
        using (var db = Context()) Equal(1, db.ChiTietChatLieux.Count(), "Composite key still saves");
    }

    private static async Task CheckAsyncSaves()
    {
        long baseline = Maximum(typeof(DanhMuc));
        using (var db = Context())
        {
            var category = new DanhMuc { TenDanhMuc = Name(), DangHoatDong = true };
            db.DanhMucs.Add(category);
            await db.SaveChangesAsync();
            Equal(baseline + 1, category.DanhMucId, "Async first insert");
            db.DanhMucs.Remove(category);
            await db.SaveChangesAsync(CancellationToken.None);
            category = new DanhMuc { TenDanhMuc = Name(), DangHoatDong = true };
            db.DanhMucs.Add(category);
            await db.SaveChangesAsync(CancellationToken.None);
            Equal(baseline + 1, category.DanhMucId, "Async reuse deleted maximum");
        }
        using (var db = Context())
        using (var transaction = db.Database.BeginTransaction())
        {
            db.DanhMucs.Add(new DanhMuc { TenDanhMuc = Name(), DangHoatDong = true });
            await db.SaveChangesAsync();
            transaction.Rollback();
        }
        Equal(baseline + 1, Maximum(typeof(DanhMuc)), "Async honors existing transaction rollback");
    }

    private static void CheckConcurrentSaves()
    {
        long baseline = Maximum(typeof(DanhMuc));
        // Force a reseed before competing inserts.
        object deleted = Insert(typeof(DanhMuc));
        Delete(deleted);
        using (var ready = new CountdownEvent(4))
        using (var start = new ManualResetEventSlim(false))
        {
            Task<long>[] tasks = Enumerable.Range(0, 4).Select(index => Task.Run(() =>
            {
                using (var db = Context())
                {
                    var category = new DanhMuc { TenDanhMuc = Name(), DangHoatDong = true };
                    db.DanhMucs.Add(category);
                    ready.Signal();
                    if (!start.Wait(TimeSpan.FromSeconds(15))) throw new TimeoutException("Concurrent start");
                    db.SaveChanges();
                    return (long)category.DanhMucId;
                }
            })).ToArray();
            if (!ready.Wait(TimeSpan.FromSeconds(15))) throw new TimeoutException("Concurrent readiness");
            start.Set();
            Task.WaitAll(tasks);
            long[] ids = tasks.Select(task => task.Result).OrderBy(id => id).ToArray();
            for (int i = 0; i < ids.Length; i++) Equal(baseline + i + 1, ids[i], "Concurrent ID " + i);
        }
    }
}
