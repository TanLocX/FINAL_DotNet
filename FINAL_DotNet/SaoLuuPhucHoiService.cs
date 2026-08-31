using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
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

    internal sealed class ThongTinMayChuDto
    {
        public string TenMayChu { get; set; }
        public string PhienBan { get; set; }
        public string ThuMuc { get; set; }
        public int CoQuyenSaoLuu { get; set; }
        public int CoQuyenPhucHoi { get; set; }
    }

    internal sealed class BanSaoLuuLichSuDto
    {
        public int backup_set_id { get; set; }
        public DateTime backup_start_date { get; set; }
        public DateTime? backup_finish_date { get; set; }
        public decimal KichThuocMb { get; set; }
        public string physical_device_name { get; set; }
        public string user_name { get; set; }
        public bool is_copy_only { get; set; }
    }

    internal sealed class RestoreHeaderInfoDto
    {
        public string DatabaseName { get; set; }
        public short? BackupType { get; set; }
        public int? Position { get; set; }
    }

    internal static class SaoLuuPhucHoiService
    {
        private const int ThoiGianChoLenhGiay = 0;

        public static ThongTinMayChuSaoLuu LayThongTinMayChu()
        {
            string tenCoSoDuLieu = LayTenCoSoDuLieu();
            var ketQua = new ThongTinMayChuSaoLuu { TenCoSoDuLieu = tenCoSoDuLieu };

            using (var db = DatabaseConnection.CreateContext())
            {
                const string sql = @"
SELECT
    CONVERT(nvarchar(256), SERVERPROPERTY('ServerName')) AS TenMayChu,
    CONVERT(nvarchar(128), SERVERPROPERTY('ProductVersion')) AS PhienBan,
    CONVERT(nvarchar(4000), SERVERPROPERTY('InstanceDefaultBackupPath')) AS ThuMuc,
    CASE WHEN IS_SRVROLEMEMBER('sysadmin') = 1 OR IS_MEMBER('db_owner') = 1 OR IS_MEMBER('db_backupoperator') = 1 THEN 1 ELSE 0 END AS CoQuyenSaoLuu,
    CASE WHEN IS_SRVROLEMEMBER('sysadmin') = 1 OR IS_SRVROLEMEMBER('dbcreator') = 1 OR USER_NAME() = 'dbo' THEN 1 ELSE 0 END AS CoQuyenPhucHoi;";

                ThongTinMayChuDto dto = db.Database.SqlQuery<ThongTinMayChuDto>(sql).FirstOrDefault();
                if (dto == null) throw new InvalidOperationException("Không đọc được thông tin SQL Server.");

                ketQua.TenMayChu = dto.TenMayChu ?? db.Database.Connection.DataSource;
                ketQua.PhienBanSqlServer = dto.PhienBan ?? string.Empty;
                ketQua.ThuMucSaoLuuMacDinh = dto.ThuMuc ?? string.Empty;
                ketQua.CoQuyenSaoLuu = dto.CoQuyenSaoLuu == 1;
                ketQua.CoQuyenPhucHoi = dto.CoQuyenPhucHoi == 1;
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
            using (var db = DatabaseConnection.CreateContext("master"))
            {
                const string sql = @"
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

                var dtoList = db.Database.SqlQuery<BanSaoLuuLichSuDto>(
                    sql,
                    new SqlParameter("@SoLuong", Math.Max(1, Math.Min(200, soLuong))),
                    new SqlParameter("@TenCoSoDuLieu", tenCoSoDuLieu)
                ).ToList();

                return dtoList.Select(item => new BanSaoLuuHienThi
                {
                    BackupSetId = item.backup_set_id,
                    BatDau = item.backup_start_date,
                    HoanTat = item.backup_finish_date,
                    KichThuocMb = item.KichThuocMb,
                    DuongDan = item.physical_device_name ?? string.Empty,
                    NguoiThucHien = item.user_name ?? string.Empty,
                    LoaiBanSao = item.is_copy_only ? "Copy-only" : "Đầy đủ"
                }).ToList();
            }
        }

        public static string TaoTenFileSaoLuu(string tienTo = "PNJ")
        {
            string tenCoSoDuLieu = Regex.Replace(LayTenCoSoDuLieu(), @"[^A-Za-z0-9_-]", "_");
            string tienToAnToan = Regex.Replace(tienTo ?? "PNJ", @"[^A-Za-z0-9_-]", "_");
            return tienToAnToan + "_" + tenCoSoDuLieu + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".bak";
        }

        public static string TaoSaoLuu(string thuMucTrenMayChu, string tenFile, bool nenDuLieu = true, Action<string> baoTienTrinh = null)
        {
            string duongDan = KetHopDuongDan(thuMucTrenMayChu, tenFile);
            TaoSaoLuuTaiDuongDan(duongDan, nenDuLieu, baoTienTrinh);
            return duongDan;
        }

        public static string PhucHoi(string duongDanBanSao, string thuMucSaoLuuAnToan, bool nenDuLieuAnToan = true, Action<string> baoTienTrinh = null)
        {
            KiemTraDuongDanDayDu(duongDanBanSao, "Đường dẫn bản sao phục hồi");
            KiemTraDuongDanDayDu(thuMucSaoLuuAnToan, "Thư mục sao lưu an toàn");
            string tenCoSoDuLieu = LayTenCoSoDuLieu();

            baoTienTrinh?.Invoke("Đang đọc và xác minh bản sao được chọn...");
            int viTriBanSao = DocVaKiemTraBanSao(duongDanBanSao, tenCoSoDuLieu);
            XacMinhBanSao(duongDanBanSao, viTriBanSao);

            string duongDanAnToan = TaoSaoLuu(thuMucSaoLuuAnToan,
                TaoTenFileSaoLuu("TruocPhucHoi"), nenDuLieuAnToan, baoTienTrinh);

            baoTienTrinh?.Invoke("Đang ngắt các kết nối và phục hồi CSDL...");
            SqlConnection.ClearAllPools();
            string tenDaTrichDan = TrichDanDinhDanh(tenCoSoDuLieu);
            try
            {
                using (var db = DatabaseConnection.CreateContext("master"))
                {
                    GanTienTrinh(db, baoTienTrinh);
                    db.Database.CommandTimeout = ThoiGianChoLenhGiay;

                    string restoreSql = @"
DECLARE @DuongDan nvarchar(4000) = @pDuongDan;
ALTER DATABASE " + tenDaTrichDan + @" SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
RESTORE DATABASE " + tenDaTrichDan + @" FROM DISK = @DuongDan
WITH FILE = @pViTri, REPLACE, RECOVERY, CHECKSUM, STATS = 5;
ALTER DATABASE " + tenDaTrichDan + @" SET MULTI_USER;";

                    db.Database.ExecuteSqlCommand(
                        TransactionalBehavior.DoNotEnsureTransaction,
                        restoreSql,
                        new SqlParameter("@pDuongDan", duongDanBanSao.Trim()),
                        new SqlParameter("@pViTri", viTriBanSao));
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

        public static bool XoaBanSaoVatLy(string duongDan)
        {
            if (string.IsNullOrWhiteSpace(duongDan)) return false;
            try
            {
                if (File.Exists(duongDan))
                {
                    File.Delete(duongDan);
                    return true;
                }
            }
            catch
            {
                // File trên máy chủ SQL từ xa hoặc đang bị khóa
            }
            return false;
        }

        private static void TaoSaoLuuTaiDuongDan(string duongDan, bool nenDuLieu, Action<string> baoTienTrinh)
        {
            string tenCoSoDuLieu = LayTenCoSoDuLieu();
            string tenDaTrichDan = TrichDanDinhDanh(tenCoSoDuLieu);
            string tuyChonNen = nenDuLieu ? ", COMPRESSION" : ", NO_COMPRESSION";
            baoTienTrinh?.Invoke("Đang sao lưu CSDL đến " + duongDan + (nenDuLieu ? " (có nén)..." : "..."));

            using (var db = DatabaseConnection.CreateContext())
            {
                GanTienTrinh(db, baoTienTrinh);
                db.Database.CommandTimeout = ThoiGianChoLenhGiay;

                string backupSql = @"
DECLARE @DuongDan nvarchar(4000) = @pDuongDan;
BACKUP DATABASE " + tenDaTrichDan + @" TO DISK = @DuongDan
WITH COPY_ONLY, NOINIT, CHECKSUM, STATS = 5" + tuyChonNen + @", NAME = @pTenBanSao, DESCRIPTION = @pMoTa;";

                db.Database.ExecuteSqlCommand(
                    TransactionalBehavior.DoNotEnsureTransaction,
                    backupSql,
                    new SqlParameter("@pDuongDan", duongDan),
                    new SqlParameter("@pTenBanSao", "PNJ Manager - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")),
                    new SqlParameter("@pMoTa", "Bản sao copy-only được tạo bởi PNJ Manager"));
            }

            baoTienTrinh?.Invoke("Đang xác minh tính toàn vẹn của file .bak...");
            int viTri = DocVaKiemTraBanSao(duongDan, tenCoSoDuLieu);
            XacMinhBanSao(duongDan, viTri);
            baoTienTrinh?.Invoke("Sao lưu và xác minh hoàn tất.");
        }

        private static int DocVaKiemTraBanSao(string duongDan, string tenCoSoDuLieu)
        {
            using (var db = DatabaseConnection.CreateContext("master"))
            {
                db.Database.CommandTimeout = ThoiGianChoLenhGiay;
                var rows = db.Database.SqlQuery<RestoreHeaderInfoDto>(
                    "RESTORE HEADERONLY FROM DISK = @pDuongDan;",
                    new SqlParameter("@pDuongDan", duongDan.Trim())
                ).ToList();

                int viTriHopLe = 0;
                foreach (var row in rows)
                {
                    if (row.BackupType == 1 && string.Equals(row.DatabaseName, tenCoSoDuLieu, StringComparison.OrdinalIgnoreCase))
                    {
                        viTriHopLe = row.Position.GetValueOrDefault(1);
                    }
                }

                if (viTriHopLe == 0)
                    throw new InvalidOperationException("File .bak không chứa bản sao đầy đủ của CSDL " + tenCoSoDuLieu + ".");
                return viTriHopLe;
            }
        }

        private static void XacMinhBanSao(string duongDan, int viTri)
        {
            using (var db = DatabaseConnection.CreateContext("master"))
            {
                db.Database.CommandTimeout = ThoiGianChoLenhGiay;
                db.Database.ExecuteSqlCommand(
                    TransactionalBehavior.DoNotEnsureTransaction,
                    "RESTORE VERIFYONLY FROM DISK = @pDuongDan WITH FILE = @pViTri;",
                    new SqlParameter("@pDuongDan", duongDan.Trim()),
                    new SqlParameter("@pViTri", viTri));
            }
        }

        private static void ThuChuyenVeNhieuNguoiDung(string tenCoSoDuLieu)
        {
            try
            {
                using (var db = DatabaseConnection.CreateContext("master"))
                {
                    db.Database.CommandTimeout = 30;
                    db.Database.ExecuteSqlCommand(
                        TransactionalBehavior.DoNotEnsureTransaction,
                        "IF DB_ID(@pTen) IS NOT NULL ALTER DATABASE " + TrichDanDinhDanh(tenCoSoDuLieu) + " SET MULTI_USER WITH ROLLBACK IMMEDIATE;",
                        new SqlParameter("@pTen", tenCoSoDuLieu));
                }
            }
            catch
            {
                // Giữ nguyên lỗi Restore ban đầu; quản trị viên có thể xử lý trạng thái CSDL trong SSMS.
            }
        }

        private static void GanTienTrinh(QL_CuaHangDaQuy_PNJEntities db, Action<string> baoTienTrinh)
        {
            if (baoTienTrinh == null) return;
            var connection = db.Database.Connection as SqlConnection;
            if (connection == null) return;

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

        private static string TrichDanDinhDanh(string ten) => "[" + ten.Replace("]", "]]") + "]";
    }
}
