using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FINAL_DotNet
{
    internal sealed class CauHinhSmtp
    {
        private const string BienMayChu = "PNJ_SMTP_SERVER";
        private const string BienCong = "PNJ_SMTP_PORT";
        private const string BienSsl = "PNJ_SMTP_SSL";
        private const string BienTaiKhoan = "PNJ_SMTP_USER";
        private const string BienMatKhau = "PNJ_SMTP_PASSWORD";
        private const string BienTenGui = "PNJ_SMTP_SENDER_NAME";

        public string MayChu { get; set; }
        public int Cong { get; set; }
        public bool SuDungSsl { get; set; }
        public string TaiKhoanGui { get; set; }
        public string MatKhauUngDung { get; set; }
        public string TenNguoiGui { get; set; }

        public static CauHinhSmtp DocTuBienMoiTruong()
        {
            int cong;
            if (!int.TryParse(DocBien(BienCong), out cong) || cong < 1 || cong > 65535) cong = 587;
            bool suDungSsl;
            if (!bool.TryParse(DocBien(BienSsl), out suDungSsl)) suDungSsl = true;
            return new CauHinhSmtp
            {
                MayChu = DocBien(BienMayChu) ?? "smtp.gmail.com",
                Cong = cong,
                SuDungSsl = suDungSsl,
                TaiKhoanGui = DocBien(BienTaiKhoan),
                MatKhauUngDung = Environment.GetEnvironmentVariable(BienMatKhau),
                TenNguoiGui = DocBien(BienTenGui) ?? "PNJ MANAGER"
            };
        }

        public static void LuuVaoBienMoiTruong(CauHinhSmtp cauHinh)
        {
            DatBien(BienMayChu, cauHinh.MayChu);
            DatBien(BienCong, cauHinh.Cong.ToString());
            DatBien(BienSsl, cauHinh.SuDungSsl.ToString());
            DatBien(BienTaiKhoan, cauHinh.TaiKhoanGui);
            DatBien(BienMatKhau, cauHinh.MatKhauUngDung);
            DatBien(BienTenGui, cauHinh.TenNguoiGui);
        }

        public bool ThuKiemTra(out string loi)
        {
            if (string.IsNullOrWhiteSpace(MayChu))
            {
                loi = "Máy chủ SMTP không được để trống.";
                return false;
            }
            if (Cong < 1 || Cong > 65535)
            {
                loi = "Cổng SMTP phải nằm trong khoảng 1 đến 65535.";
                return false;
            }
            if (!EmailHopLe(TaiKhoanGui))
            {
                loi = "Tài khoản gửi phải là địa chỉ email hợp lệ.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(MatKhauUngDung))
            {
                loi = "Mật khẩu ứng dụng SMTP chưa được cấu hình.";
                return false;
            }
            loi = null;
            return true;
        }

        public static bool EmailHopLe(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || email.Length > 254) return false;
            try
            {
                var diaChi = new MailAddress(email.Trim());
                return string.Equals(diaChi.Address, email.Trim(), StringComparison.OrdinalIgnoreCase);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static string DocBien(string ten)
        {
            string giaTri = Environment.GetEnvironmentVariable(ten);
            if (string.IsNullOrWhiteSpace(giaTri))
                giaTri = Environment.GetEnvironmentVariable(ten, EnvironmentVariableTarget.User);
            return string.IsNullOrWhiteSpace(giaTri) ? null : giaTri.Trim();
        }

        private static void DatBien(string ten, string giaTri)
        {
            string chuanHoa = string.IsNullOrWhiteSpace(giaTri) ? null : giaTri.Trim();
            Environment.SetEnvironmentVariable(ten, chuanHoa, EnvironmentVariableTarget.User);
            Environment.SetEnvironmentVariable(ten, chuanHoa, EnvironmentVariableTarget.Process);
        }
    }

    internal sealed class DuLieuNguoiNhanEmail
    {
        public int? KhachHangId { get; set; }
        public int? HoaDonId { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string TenSanPham { get; set; }
        public string TongTien { get; set; }
        public string NgayMua { get; set; }
        public string HanBaoHanh { get; set; }
        public string MaHoaDon { get; set; }
        public string GhiChu { get; set; }
    }

    internal static class BoMayCaNhanHoaEmail
    {
        public static string DienNoiDung(string noiDung, DuLieuNguoiNhanEmail nguoiNhan)
        {
            string ketQua = noiDung ?? string.Empty;
            var giaTri = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "HoTen", nguoiNhan?.HoTen ?? string.Empty },
                { "TenSanPham", nguoiNhan?.TenSanPham ?? string.Empty },
                { "Sdt", nguoiNhan?.SoDienThoai ?? string.Empty },
                { "Email", nguoiNhan?.Email ?? string.Empty },
                { "TongTien", nguoiNhan?.TongTien ?? string.Empty },
                { "ThanhTien", nguoiNhan?.TongTien ?? string.Empty },
                { "NgayMua", nguoiNhan?.NgayMua ?? string.Empty },
                { "HanBaoHanh", nguoiNhan?.HanBaoHanh ?? string.Empty },
                { "MaHoaDon", nguoiNhan?.MaHoaDon ?? string.Empty },
                { "GhiChu", nguoiNhan?.GhiChu ?? string.Empty }
            };
            foreach (var item in giaTri)
            {
                ketQua = Regex.Replace(ketQua, "\\{\\{" + Regex.Escape(item.Key) + "\\}\\}",
                    _ => item.Value, RegexOptions.IgnoreCase);
                ketQua = Regex.Replace(ketQua, "\\{" + Regex.Escape(item.Key) + "\\}",
                    _ => item.Value, RegexOptions.IgnoreCase);
            }
            return ketQua;
        }
    }

    internal sealed class DichVuGuiEmail
    {
        public async Task GuiAsync(
            CauHinhSmtp cauHinh,
            string emailNhan,
            string tieuDe,
            string noiDung,
            IEnumerable<string> tepDinhKem)
        {
            string loi;
            if (!cauHinh.ThuKiemTra(out loi)) throw new InvalidOperationException(loi);
            if (!CauHinhSmtp.EmailHopLe(emailNhan))
                throw new InvalidOperationException("Địa chỉ email người nhận không hợp lệ.");
            if (string.IsNullOrWhiteSpace(tieuDe))
                throw new InvalidOperationException("Tiêu đề email không được để trống.");
            if (string.IsNullOrWhiteSpace(noiDung))
                throw new InvalidOperationException("Nội dung email không được để trống.");

            using (var thu = new MailMessage())
            using (var smtp = new SmtpClient(cauHinh.MayChu, cauHinh.Cong))
            {
                thu.From = new MailAddress(cauHinh.TaiKhoanGui, cauHinh.TenNguoiGui);
                thu.To.Add(new MailAddress(emailNhan.Trim()));
                thu.Subject = tieuDe.Trim();
                thu.Body = noiDung;
                thu.IsBodyHtml = LaNoiDungHtml(noiDung);
                thu.BodyEncoding = System.Text.Encoding.UTF8;
                thu.SubjectEncoding = System.Text.Encoding.UTF8;

                foreach (string duongDan in (tepDinhKem ?? Enumerable.Empty<string>()).Distinct())
                {
                    if (!string.IsNullOrWhiteSpace(duongDan) && File.Exists(duongDan))
                        thu.Attachments.Add(new Attachment(duongDan));
                }

                smtp.EnableSsl = cauHinh.SuDungSsl;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(cauHinh.TaiKhoanGui, cauHinh.MatKhauUngDung);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Timeout = 30000;
                await smtp.SendMailAsync(thu);
            }
        }

        private static bool LaNoiDungHtml(string noiDung)
        {
            string giaTri = (noiDung ?? string.Empty).ToLowerInvariant();
            return giaTri.Contains("<html") || giaTri.Contains("</") || giaTri.Contains("<p>");
        }
    }
}
