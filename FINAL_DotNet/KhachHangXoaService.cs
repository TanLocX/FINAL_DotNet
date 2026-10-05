using System;
using System.Data;
using System.Linq;

namespace FINAL_DotNet
{
    internal enum KetQuaXoaKhachHang
    {
        DaXoa,
        KhongTonTai,
        KhachLe,
        DaThayDoi,
        CoLichSu,
        ConDiemTichLuy
    }

    internal static class KhachHangXoaService
    {
        public static KetQuaXoaKhachHang Xoa(
            int khachHangId, string hoTenDuKien, string soDienThoaiDuKien)
        {
            if (!CurrentUserSession.DaDangNhap || !CurrentUserSession.HienTai.LaQuanTriVien)
            {
                throw new UnauthorizedAccessException("Chỉ quản trị viên được xóa khách hàng.");
            }

            using (var db = DatabaseConnection.CreateContext())
            using (var giaoDich = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                var khachHang = db.KhachHangs.SingleOrDefault(kh => kh.KhachHangId == khachHangId);
                if (khachHang == null)
                {
                    return KetQuaXoaKhachHang.KhongTonTai;
                }

                if (khachHang.KhachHangId == 1 || khachHang.SoDienThoai == "0000000000")
                {
                    return KetQuaXoaKhachHang.KhachLe;
                }

                if (khachHang.HoTen != hoTenDuKien ||
                    khachHang.SoDienThoai != soDienThoaiDuKien)
                {
                    return KetQuaXoaKhachHang.DaThayDoi;
                }

                if (db.HoaDons.Any(hd => hd.KhachHangId == khachHangId) ||
                    db.PhieuThuMuas.Any(ptm => ptm.KhachHangId == khachHangId) ||
                    db.NhatKyGuiEmails.Any(nk => nk.KhachHangId == khachHangId))
                {
                    return KetQuaXoaKhachHang.CoLichSu;
                }

                if (khachHang.DiemTichLuy != 0)
                {
                    return KetQuaXoaKhachHang.ConDiemTichLuy;
                }

                db.KhachHangs.Remove(khachHang);
                db.SaveChanges();
                giaoDich.Commit();
                return KetQuaXoaKhachHang.DaXoa;
            }
        }
    }
}
