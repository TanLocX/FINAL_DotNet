using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FINAL_DotNet
{
    internal sealed class ThongTinMayChuSaoLuu
    {
        public string TenMayChu { get; set; }
        public string TenCoSoDuLieu { get; set; }
        public string PhienBanSqlServer { get; set; }
        public string ThuMucSaoLuuMacDinh { get; set; }
        public bool CoQuyenSaoLuu { get; set; }
        public bool CoQuyenPhucHoi { get; set; }
    }

    internal sealed class BanSaoLuuHienThi
    {
        public int BackupSetId { get; set; }
        public DateTime BatDau { get; set; }
        public DateTime? HoanTat { get; set; }
        public decimal KichThuocMb { get; set; }
        public string DuongDan { get; set; }
        public string NguoiThucHien { get; set; }
        public string LoaiBanSao { get; set; }
        public string ThoiGianHienThi => HoanTat.GetValueOrDefault(BatDau).ToString("dd/MM/yyyy HH:mm:ss");
        public string KichThuocHienThi => KichThuocMb.ToString("N1", CultureInfo.GetCultureInfo("vi-VN")) + " MB";
    }

    internal static class SaoLuuPhucHoiService
    {
        private const int ThoiGianChoLenhGiay = 0;

        public static ThongTinMayChuSaoLuu LayThongTinMayChu()
        {
            string tenCoSoDuLieu = LayTenCoSoDuLieu();
            var ketQua = new ThongTinMayChuSaoLuu { TenCoSoDuLieu = tenCoSoDuLieu };
            using (SqlConnection connection = DatabaseConnection.CreateSqlConnection())
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = @"
SELECT
    CONVERT(nvarchar(256), SERVERPROPERTY('ServerName')) AS TenMayChu,
    CONVERT(nvarchar(128), SERVERPROPERTY('ProductVersion')) AS PhienBan,
    CONVERT(nvarchar(4000), SERVERPROPERTY('InstanceDefaultBackupPath')) AS ThuMuc,
    CASE WHEN IS_SRVROLEMEMBER('sysadmin') = 1 OR IS_MEMBER('db_owner') = 1 OR IS_MEMBER('db_backupoperator') = 1 THEN 1 ELSE 0 END AS CoQuyenSaoLuu,
    CASE WHEN IS_SRVROLEMEMBER('sysadmin') = 1 OR IS_SRVROLEMEMBER('dbcreator') = 1 OR USER_NAME() = 'dbo' THEN 1 ELSE 0 END AS CoQuyenPhucHoi;";
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read()) throw new InvalidOperationException("Không đọc được thông tin SQL Server.");
                    ketQua.TenMayChu = reader["TenMayChu"] as string ?? connection.DataSource;
                    ketQua.PhienBanSqlServer = reader["PhienBan"] as string ?? string.Empty;
                    ketQua.ThuMucSaoLuuMacDinh = reader["ThuMuc"] as string ?? string.Empty;
                    ketQua.CoQuyenSaoLuu = Convert.ToInt32(reader["CoQuyenSaoLuu"], CultureInfo.InvariantCulture) == 1;
                    ketQua.CoQuyenPhucHoi = Convert.ToInt32(reader["CoQuyenPhucHoi"], CultureInfo.InvariantCulture) == 1;
                }
            }

            if (string.IsNullOrWhiteSpace(ketQua.ThuMucSaoLuuMacDinh))
            {
                try
                {
                    BanSaoLuuHienThi ganNhat = LayLichSuSaoLuu(1).FirstOrDefault();
                    if (ganNhat != null) ketQua.ThuMucSaoLuuMacDinh = LayThuMuc(ganNhat.DuongDan);
                }
                catch
                {
                    // Một số tài khoản được phép BACKUP nhưng không được đọc lịch sử trong msdb.
                }
            }
            return ketQua;
        }

        public static List<BanSaoLuuHienThi> LayLichSuSaoLuu(int soLuong = 50)
        {
            string tenCoSoDuLieu = LayTenCoSoDuLieu();
            var ketQua = new List<BanSaoLuuHienThi>();
            using (SqlConnection connection = DatabaseConnection.CreateSqlConnection("master"))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = @"
SELECT TOP (@SoLuong)
    bs.backup_set_id,
    bs.backup_start_date,
    bs.backup_finish_date,
    CONVERT(decimal(18,2), COALESCE(NULLIF(bs.compressed_backup_size, 0), bs.backup_size) / 1048576.0) AS KichThuocMb,
    media.physical_device_name,
    bs.user_name,
    bs.is_copy_only
FROM msdb.dbo.backupset AS bs
OUTER APPLY
(
    SELECT TOP (1) bmf.physical_device_name
    FROM msdb.dbo.backupmediafamily AS bmf
    WHERE bmf.media_set_id = bs.media_set_id
    ORDER BY bmf.family_sequence_number
) AS media
WHERE bs.database_name = @TenCoSoDuLieu AND bs.type = 'D'
ORDER BY bs.backup_finish_date DESC, bs.backup_set_id DESC;";
                command.Parameters.Add("@SoLuong", SqlDbType.Int).Value = Math.Max(1, Math.Min(200, soLuong));
                command.Parameters.Add("@TenCoSoDuLieu", SqlDbType.NVarChar, 128).Value = tenCoSoDuLieu;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ketQua.Add(new BanSaoLuuHienThi
                        {
                            BackupSetId = Convert.ToInt32(reader["backup_set_id"], CultureInfo.InvariantCulture),
                            BatDau = Convert.ToDateTime(reader["backup_start_date"], CultureInfo.InvariantCulture),
                            HoanTat = reader["backup_finish_date"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["backup_finish_date"], CultureInfo.InvariantCulture),
                            KichThuocMb = Convert.ToDecimal(reader["KichThuocMb"], CultureInfo.InvariantCulture),
                            DuongDan = reader["physical_device_name"] as string ?? string.Empty,
                            NguoiThucHien = reader["user_name"] as string ?? string.Empty,
                            LoaiBanSao = Convert.ToBoolean(reader["is_copy_only"], CultureInfo.InvariantCulture) ? "Copy-only" : "Đầy đủ"
                        });
                    }
                }
            }
            return ketQua;
        }

        public static string TaoTenFileSaoLuu(string tienTo = "PNJ")
        {
            string tenCoSoDuLieu = Regex.Replace(LayTenCoSoDuLieu(), @"[^A-Za-z0-9_-]", "_");
            string tienToAnToan = Regex.Replace(tienTo ?? "PNJ", @"[^A-Za-z0-9_-]", "_");
            return tienToAnToan + "_" + tenCoSoDuLieu + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".bak";
        }

        public static string TaoSaoLuu(string thuMucTrenMayChu, string tenFile, Action<string> baoTienTrinh = null)
        {
            string duongDan = KetHopDuongDan(thuMucTrenMayChu, tenFile);
            TaoSaoLuuTaiDuongDan(duongDan, baoTienTrinh);
            return duongDan;
        }

        public static string PhucHoi(string duongDanBanSao, string thuMucSaoLuuAnToan, Action<string> baoTienTrinh = null)
        {
            KiemTraDuongDanDayDu(duongDanBanSao, "Đường dẫn bản sao phục hồi");
            KiemTraDuongDanDayDu(thuMucSaoLuuAnToan, "Thư mục sao lưu an toàn");
            string tenCoSoDuLieu = LayTenCoSoDuLieu();

            baoTienTrinh?.Invoke("Đang đọc và xác minh bản sao được chọn...");
            int viTriBanSao = DocVaKiemTraBanSao(duongDanBanSao, tenCoSoDuLieu);
            XacMinhBanSao(duongDanBanSao, viTriBanSao);

            string duongDanAnToan = TaoSaoLuu(thuMucSaoLuuAnToan,
                TaoTenFileSaoLuu("TruocPhucHoi"), baoTienTrinh);

            baoTienTrinh?.Invoke("Đang ngắt các kết nối và phục hồi CSDL...");
            SqlConnection.ClearAllPools();
            string tenDaTrichDan = TrichDanDinhDanh(tenCoSoDuLieu);
            try
            {
                using (SqlConnection connection = DatabaseConnection.CreateSqlConnection("master"))
                {
                    GanTienTrinh(connection, baoTienTrinh);
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandTimeout = ThoiGianChoLenhGiay;
                        command.CommandText = @"
DECLARE @DuongDan nvarchar(4000) = @pDuongDan;
ALTER DATABASE " + tenDaTrichDan + @" SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
RESTORE DATABASE " + tenDaTrichDan + @" FROM DISK = @DuongDan
WITH FILE = @pViTri, REPLACE, RECOVERY, CHECKSUM, STATS = 5;
ALTER DATABASE " + tenDaTrichDan + @" SET MULTI_USER;";
                        command.Parameters.Add("@pDuongDan", SqlDbType.NVarChar, 4000).Value = duongDanBanSao.Trim();
                        command.Parameters.Add("@pViTri", SqlDbType.Int).Value = viTriBanSao;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                ThuChuyenVeNhieuNguoiDung(tenCoSoDuLieu);
                throw;
            }
            finally
            {
                SqlConnection.ClearAllPools();
            }

            baoTienTrinh?.Invoke("Phục hồi hoàn tất. Bản sao an toàn: " + duongDanAnToan);
            return duongDanAnToan;
        }

        private static void TaoSaoLuuTaiDuongDan(string duongDan, Action<string> baoTienTrinh)
        {
            string tenCoSoDuLieu = LayTenCoSoDuLieu();
            string tenDaTrichDan = TrichDanDinhDanh(tenCoSoDuLieu);
            baoTienTrinh?.Invoke("Đang sao lưu CSDL đến " + duongDan + "...");
            using (SqlConnection connection = DatabaseConnection.CreateSqlConnection())
            {
                GanTienTrinh(connection, baoTienTrinh);
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandTimeout = ThoiGianChoLenhGiay;
                    command.CommandText = @"
DECLARE @DuongDan nvarchar(4000) = @pDuongDan;
BACKUP DATABASE " + tenDaTrichDan + @" TO DISK = @DuongDan
WITH COPY_ONLY, NOINIT, CHECKSUM, STATS = 5, NAME = @pTenBanSao, DESCRIPTION = @pMoTa;";
                    command.Parameters.Add("@pDuongDan", SqlDbType.NVarChar, 4000).Value = duongDan;
                    command.Parameters.Add("@pTenBanSao", SqlDbType.NVarChar, 128).Value = "PNJ Manager - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    command.Parameters.Add("@pMoTa", SqlDbType.NVarChar, 255).Value = "Bản sao copy-only được tạo bởi PNJ Manager";
                    command.ExecuteNonQuery();
                }
            }
            baoTienTrinh?.Invoke("Đang xác minh tính toàn vẹn của file .bak...");
            int viTri = DocVaKiemTraBanSao(duongDan, tenCoSoDuLieu);
            XacMinhBanSao(duongDan, viTri);
            baoTienTrinh?.Invoke("Sao lưu và xác minh hoàn tất.");
        }

        private static int DocVaKiemTraBanSao(string duongDan, string tenCoSoDuLieu)
        {
            using (SqlConnection connection = DatabaseConnection.CreateSqlConnection("master"))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandTimeout = ThoiGianChoLenhGiay;
                command.CommandText = "RESTORE HEADERONLY FROM DISK = @pDuongDan;";
                command.Parameters.Add("@pDuongDan", SqlDbType.NVarChar, 4000).Value = duongDan.Trim();
                connection.Open();
                int viTriHopLe = 0;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int cotDatabaseName = reader.GetOrdinal("DatabaseName");
                    int cotBackupType = reader.GetOrdinal("BackupType");
                    int cotPosition = reader.GetOrdinal("Position");
                    while (reader.Read())
                    {
                        string databaseName = reader.IsDBNull(cotDatabaseName) ? string.Empty : reader.GetString(cotDatabaseName);
                        int backupType = Convert.ToInt32(reader.GetValue(cotBackupType), CultureInfo.InvariantCulture);
                        if (backupType == 1 && string.Equals(databaseName, tenCoSoDuLieu, StringComparison.OrdinalIgnoreCase))
                            viTriHopLe = Convert.ToInt32(reader.GetValue(cotPosition), CultureInfo.InvariantCulture);
                    }
                }
                if (viTriHopLe == 0)
                    throw new InvalidOperationException("File .bak không chứa bản sao đầy đủ của CSDL " + tenCoSoDuLieu + ".");
                return viTriHopLe;
            }
        }

        private static void XacMinhBanSao(string duongDan, int viTri)
        {
            using (SqlConnection connection = DatabaseConnection.CreateSqlConnection("master"))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandTimeout = ThoiGianChoLenhGiay;
                command.CommandText = "RESTORE VERIFYONLY FROM DISK = @pDuongDan WITH FILE = @pViTri;";
                command.Parameters.Add("@pDuongDan", SqlDbType.NVarChar, 4000).Value = duongDan.Trim();
                command.Parameters.Add("@pViTri", SqlDbType.Int).Value = viTri;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private static void ThuChuyenVeNhieuNguoiDung(string tenCoSoDuLieu)
        {
            try
            {
                using (SqlConnection connection = DatabaseConnection.CreateSqlConnection("master"))
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandTimeout = 30;
                    command.CommandText = "IF DB_ID(@pTen) IS NOT NULL ALTER DATABASE " + TrichDanDinhDanh(tenCoSoDuLieu) + " SET MULTI_USER WITH ROLLBACK IMMEDIATE;";
                    command.Parameters.Add("@pTen", SqlDbType.NVarChar, 128).Value = tenCoSoDuLieu;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                // Giữ nguyên lỗi Restore ban đầu; quản trị viên có thể xử lý trạng thái CSDL trong SSMS.
            }
        }

        private static void GanTienTrinh(SqlConnection connection, Action<string> baoTienTrinh)
        {
            if (baoTienTrinh == null) return;
            connection.FireInfoMessageEventOnUserErrors = true;
            connection.InfoMessage += (sender, args) =>
            {
                string message = args.Message?.Trim();
                if (!string.IsNullOrWhiteSpace(message)) baoTienTrinh(message);
            };
        }

        private static string KetHopDuongDan(string thuMuc, string tenFile)
        {
            KiemTraDuongDanDayDu(thuMuc, "Thư mục sao lưu trên máy chủ");
            if (string.IsNullOrWhiteSpace(tenFile)) throw new InvalidOperationException("Tên file sao lưu không được để trống.");
            string ten = tenFile.Trim();
            if (!ten.EndsWith(".bak", StringComparison.OrdinalIgnoreCase)) ten += ".bak";
            if (ten.IndexOfAny(new[] { '\\', '/', ':', '*', '?', '"', '<', '>', '|' }) >= 0)
                throw new InvalidOperationException("Tên file sao lưu chứa ký tự không hợp lệ.");
            char dauPhanCach = thuMuc.Trim().Contains("/") && !thuMuc.Trim().Contains("\\") ? '/' : '\\';
            return thuMuc.Trim().TrimEnd('\\', '/') + dauPhanCach + ten;
        }

        private static void KiemTraDuongDanDayDu(string duongDan, string tenTruong)
        {
            if (string.IsNullOrWhiteSpace(duongDan)) throw new InvalidOperationException(tenTruong + " không được để trống.");
            string value = duongDan.Trim();
            bool laDuongDanWindows = Regex.IsMatch(value, @"^[A-Za-z]:\\") || value.StartsWith(@"\\", StringComparison.Ordinal);
            bool laDuongDanLinux = value.StartsWith("/", StringComparison.Ordinal);
            if (!laDuongDanWindows && !laDuongDanLinux)
                throw new InvalidOperationException(tenTruong + " phải là đường dẫn đầy đủ trên máy chủ SQL.");
        }

        private static string LayThuMuc(string duongDan)
        {
            if (string.IsNullOrWhiteSpace(duongDan)) return string.Empty;
            int viTri = Math.Max(duongDan.LastIndexOf('\\'), duongDan.LastIndexOf('/'));
            return viTri > 0 ? duongDan.Substring(0, viTri) : string.Empty;
        }

        private static string LayTenCoSoDuLieu()
        {
            string ten = DatabaseConnection.GetDatabaseName();
            if (string.IsNullOrWhiteSpace(ten)) throw new InvalidOperationException("Kết nối chưa chỉ định tên CSDL.");
            string[] cam = { "master", "model", "msdb", "tempdb" };
            if (cam.Contains(ten, StringComparer.OrdinalIgnoreCase))
                throw new InvalidOperationException("Không cho phép Backup/Restore CSDL hệ thống.");
            return ten.Trim();
        }

        private static string TrichDanDinhDanh(string ten) => "[" + ten.Replace("]", "]]" ) + "]";
    }
}
