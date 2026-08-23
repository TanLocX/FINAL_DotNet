using System;

namespace FINAL_DotNet
{
    internal sealed class ThongTinPhienDangNhap
    {
        public ThongTinPhienDangNhap(
            int taiKhoanId,
            int nhanVienId,
            string tenDangNhap,
            string hoTen,
            string vaiTro)
        {
            TaiKhoanId = taiKhoanId;
            NhanVienId = nhanVienId;
            TenDangNhap = tenDangNhap;
            HoTen = hoTen;
            VaiTro = vaiTro;
        }

        public int TaiKhoanId { get; }
        public int NhanVienId { get; }
        public string TenDangNhap { get; }
        public string HoTen { get; }
        public string VaiTro { get; }
        public bool LaQuanTriVien => VaiTro == "ADMIN";
    }

    internal static class CurrentUserSession
    {
        private static ThongTinPhienDangNhap hienTai;

        public static bool DaDangNhap => hienTai != null;

        public static ThongTinPhienDangNhap HienTai
        {
            get
            {
                if (hienTai == null)
                {
                    throw new InvalidOperationException("Chưa có phiên đăng nhập.");
                }

                return hienTai;
            }
        }

        public static void BatDau(
            int taiKhoanId,
            int nhanVienId,
            string tenDangNhap,
            string hoTen,
            string vaiTro)
        {
            string vaiTroChuanHoa = (vaiTro ?? string.Empty).Trim().ToUpperInvariant();
            if (vaiTroChuanHoa != "ADMIN" && vaiTroChuanHoa != "NHANVIEN")
            {
                throw new InvalidOperationException("Vai trò tài khoản không hợp lệ.");
            }

            hienTai = new ThongTinPhienDangNhap(
                taiKhoanId,
                nhanVienId,
                tenDangNhap,
                hoTen,
                vaiTroChuanHoa);
        }

        public static void KetThuc()
        {
            hienTai = null;
        }
    }
}
