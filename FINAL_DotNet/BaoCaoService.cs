using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using Microsoft.Reporting.WinForms;

namespace FINAL_DotNet
{
    internal sealed class CauHinhBaoCao
    {
        public CauHinhBaoCao(string tieuDeCuaSo, IEnumerable<DongBaoCao> cacDong,
            IDictionary<string, string> thamSo)
        {
            TieuDeCuaSo = tieuDeCuaSo;
            CacDong = cacDong.ToList();
            ThamSo = thamSo.Select(item => new ReportParameter(item.Key, item.Value ?? string.Empty)).ToList();
        }

        public string TieuDeCuaSo { get; }
        public List<DongBaoCao> CacDong { get; }
        public List<ReportParameter> ThamSo { get; }
    }

    public sealed class DongBaoCao
    {
        public int SoThuTu { get; set; }
        public string NoiDung { get; set; }
    }

    internal static class BaoCaoService
    {
        private const string MauBaoCao = "FINAL_DotNet.Reports.BaoCaoChung.rdlc";
        private static readonly CultureInfo VanHoaVietNam = CultureInfo.GetCultureInfo("vi-VN");

        public static string TaiNguyenMauBaoCao => MauBaoCao;

        public static CauHinhBaoCao TaoHoaDon(int hoaDonId)
        {
            using (var db = DatabaseConnection.CreateContext())
            {
                HoaDon hoaDon = db.HoaDons
                    .Include(hd => hd.KhachHang)
                    .Include(hd => hd.NhanVien)
                    .Include(hd => hd.ChiTietHoaDons.Select(ct => ct.SanPham))
                    .AsNoTracking()
                    .SingleOrDefault(hd => hd.HoaDonId == hoaDonId);
                if (hoaDon == null) throw new InvalidOperationException("Không tìm thấy hóa đơn đã chọn.");
                if (hoaDon.TrangThai != "DA_THANH_TOAN")
                    throw new InvalidOperationException("Chỉ lập báo cáo cho hóa đơn đã thanh toán.");

                List<DongBaoCao> cacDong = hoaDon.ChiTietHoaDons
                    .OrderBy(ct => ct.ChiTietHoaDonId)
                    .Select((ct, index) => new DongBaoCao
                    {
                        SoThuTu = index + 1,
                        NoiDung = string.Format(VanHoaVietNam,
                            "{0} - {1}\r\nSố lượng: {2:N0}    Đơn giá: {3:N0} đ    Thành tiền: {4:N0} đ    Bảo hành: {5}",
                            $"SP{ct.SanPhamId:000000}", ct.SanPham.TenSanPham, ct.SoLuong, ct.DonGiaBan,
                            ct.ThanhTien ?? ct.SoLuong * ct.DonGiaBan,
                            ct.HanBaoHanh.HasValue ? ct.HanBaoHanh.Value.ToString("dd/MM/yyyy") : "Không có")
                    }).ToList();

                return new CauHinhBaoCao("Báo cáo hóa đơn " + $"HD{hoaDon.HoaDonId:000000}", cacDong,
                    TaoThamSo(
                        "HÓA ĐƠN BÁN HÀNG",
                        $"HD{hoaDon.HoaDonId:000000}",
                        "Ngày lập: " + hoaDon.NgayLap.ToString("dd/MM/yyyy HH:mm"),
                        "Khách hàng: " + hoaDon.KhachHang.HoTen,
                        "Nhân viên: " + hoaDon.NhanVien.HoTen,
                        "Thanh toán: " + hoaDon.PhuongThucThanhToan,
                        "CHI TIẾT SẢN PHẨM",
                        "Tổng tiền: " + DinhDangTien(hoaDon.TongTien),
                        "Giảm giá: " + DinhDangTien(hoaDon.GiamGia),
                        "Thành tiền: " + DinhDangTien(hoaDon.ThanhTien),
                        string.Empty));
            }
        }

        public static CauHinhBaoCao TaoPhieuNhap(int phieuNhapId)
        {
            using (var db = DatabaseConnection.CreateContext())
            {
                PhieuNhap phieu = db.PhieuNhaps
                    .Include(pn => pn.NhaCungCap)
                    .Include(pn => pn.NhanVien)
                    .Include(pn => pn.ChiTietPhieuNhaps.Select(ct => ct.SanPham))
                    .AsNoTracking()
                    .SingleOrDefault(pn => pn.PhieuNhapId == phieuNhapId);
                if (phieu == null) throw new InvalidOperationException("Không tìm thấy phiếu nhập đã chọn.");
                if (phieu.TrangThai != "HOAN_THANH")
                    throw new InvalidOperationException("Chỉ lập báo cáo cho phiếu nhập đã hoàn thành.");

                List<DongBaoCao> cacDong = phieu.ChiTietPhieuNhaps
                    .OrderBy(ct => ct.ChiTietPhieuNhapId)
                    .Select((ct, index) => new DongBaoCao
                    {
                        SoThuTu = index + 1,
                        NoiDung = string.Format(VanHoaVietNam,
                            "{0} - {1}\r\nSố lượng: {2:N0}    Đơn giá nhập: {3:N0} đ    Thành tiền: {4:N0} đ",
                            $"SP{ct.SanPhamId:000000}", ct.SanPham.TenSanPham, ct.SoLuong, ct.DonGiaNhap,
                            ct.ThanhTien ?? ct.SoLuong * ct.DonGiaNhap)
                    }).ToList();

                return new CauHinhBaoCao("Báo cáo phiếu nhập " + $"PN{phieu.PhieuNhapId:000000}", cacDong,
                    TaoThamSo(
                        "PHIẾU NHẬP HÀNG",
                        $"PN{phieu.PhieuNhapId:000000}",
                        "Ngày nhập: " + phieu.NgayNhap.ToString("dd/MM/yyyy HH:mm"),
                        "Nhà cung cấp: " + phieu.NhaCungCap.TenNhaCungCap,
                        "Nhân viên: " + phieu.NhanVien.HoTen,
                        "Trạng thái: Hoàn thành",
                        "CHI TIẾT NHẬP HÀNG",
                        string.Empty,
                        string.Empty,
                        "Tổng tiền nhập: " + DinhDangTien(phieu.TongTienNhap),
                        "Ghi chú: " + (string.IsNullOrWhiteSpace(phieu.GhiChu) ? "Không có" : phieu.GhiChu)));
            }
        }

        public static CauHinhBaoCao TaoPhieuThuMua(int phieuThuMuaId)
        {
            using (var db = DatabaseConnection.CreateContext())
            {
                PhieuThuMua phieu = db.PhieuThuMuas
                    .Include(item => item.KhachHang)
                    .Include(item => item.NhanVien)
                    .Include(item => item.ChiTietPhieuThuMuas.Select(detail => detail.ChatLieu))
                    .Include(item => item.ChiTietPhieuThuMuas.Select(detail => detail.SanPham))
                    .AsNoTracking()
                    .SingleOrDefault(item => item.PhieuThuMuaId == phieuThuMuaId);
                if (phieu == null) throw new InvalidOperationException("Không tìm thấy phiếu thu mua đã chọn.");

                List<DongBaoCao> cacDong = phieu.ChiTietPhieuThuMuas
                    .OrderBy(detail => detail.ChiTietPhieuThuMuaId)
                    .Select((detail, index) => new DongBaoCao
                    {
                        SoThuTu = index + 1,
                        NoiDung = string.Format(VanHoaVietNam,
                            "{0}{1} - {2}\r\nTrọng lượng: {3:N3} {4}    Đơn giá: {5:N0} đ    Thành tiền: {6:N0} đ",
                            detail.SanPhamId.HasValue ? $"SP{detail.SanPhamId:000000} / " : string.Empty,
                            detail.ChatLieu.TenChatLieu,
                            detail.TenSanPhamThu,
                            detail.TrongLuong,
                            detail.DonViTinh,
                            detail.DonGiaThuMua,
                            detail.ThanhTien ?? detail.TrongLuong * detail.DonGiaThuMua)
                    }).ToList();

                string ghiChu = phieu.GhiChu ?? string.Empty;

                return new CauHinhBaoCao("Phiếu thu mua " + $"PTM{phieu.PhieuThuMuaId:000000}", cacDong,
                    TaoThamSo(
                        "PHIẾU THU MUA TỪ KHÁCH HÀNG",
                        $"PTM{phieu.PhieuThuMuaId:000000}",
                        "Ngày thu mua: " + phieu.NgayThuMua.ToString("dd/MM/yyyy HH:mm"),
                        "Khách hàng: " + phieu.KhachHang.HoTen + " - " + phieu.KhachHang.SoDienThoai,
                        "Nhân viên: " + phieu.NhanVien.HoTen,
                        "Mã nguồn: " + (phieu.MaPhieuNguon ?? "Không có") + " | Trạng thái: " +
                        (phieu.TrangThai == "HOAN_THANH" ? "Hoàn thành" : "Đã hủy"),
                        "CHI TIẾT THU MUA",
                        string.Empty,
                        string.Empty,
                        "Tổng tiền: " + DinhDangTien(phieu.TongTienThuMua),
                        "Ghi chú: " + (string.IsNullOrWhiteSpace(ghiChu) ? "Không có" : ghiChu)));
            }
        }

        public static CauHinhBaoCao TaoPhieuBaoHanh(int phieuBaoHanhId)
        {
            using (var db = DatabaseConnection.CreateContext())
            {
                PhieuBaoHanh phieu = db.PhieuBaoHanhs
                    .Include(pbh => pbh.ChiTietHoaDon.HoaDon.KhachHang)
                    .Include(pbh => pbh.ChiTietHoaDon.SanPham)
                    .AsNoTracking()
                    .SingleOrDefault(pbh => pbh.PhieuBaoHanhId == phieuBaoHanhId);
                if (phieu == null) throw new InvalidOperationException("Không tìm thấy phiếu bảo hành đã chọn.");

                var cacDong = new List<DongBaoCao>
                {
                    Dong(1, "Khách hàng", phieu.ChiTietHoaDon.HoaDon.KhachHang.HoTen + " - " + phieu.ChiTietHoaDon.HoaDon.KhachHang.SoDienThoai),
                    Dong(2, "Hóa đơn", $"HD{phieu.ChiTietHoaDon.HoaDonId:000000}"),
                    Dong(3, "Sản phẩm", $"SP{phieu.ChiTietHoaDon.SanPhamId:000000}" + " - " + phieu.ChiTietHoaDon.SanPham.TenSanPham),
                    Dong(4, "Hạn bảo hành", phieu.ChiTietHoaDon.HanBaoHanh.HasValue ? phieu.ChiTietHoaDon.HanBaoHanh.Value.ToString("dd/MM/yyyy") : "Không có"),
                    Dong(5, "Nội dung tiếp nhận", phieu.NoiDungBaoHanh),
                    Dong(6, "Ngày trả dự kiến", DinhDangNgay(phieu.NgayTraDuKien)),
                    Dong(7, "Ngày trả thực tế", DinhDangNgay(phieu.NgayTraThucTe)),
                    Dong(8, "Ghi chú", string.IsNullOrWhiteSpace(phieu.GhiChu) ? "Không có" : phieu.GhiChu)
                };

                return new CauHinhBaoCao("Phiếu tiếp nhận bảo hành " + $"PBH{phieu.PhieuBaoHanhId:000000}", cacDong,
                    TaoThamSo(
                        "PHIẾU TIẾP NHẬN BẢO HÀNH",
                        $"PBH{phieu.PhieuBaoHanhId:000000}",
                        "Ngày tiếp nhận: " + phieu.NgayTiepNhan.ToString("dd/MM/yyyy HH:mm"),
                        "Trạng thái: " + TenTrangThaiBaoHanh(phieu.TrangThai),
                        string.Empty,
                        string.Empty,
                        "THÔNG TIN BẢO HÀNH",
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        "Khách hàng vui lòng giữ phiếu này khi nhận lại sản phẩm."));
            }
        }

        private static Dictionary<string, string> TaoThamSo(string tieuDe, string maChungTu,
            string thongTin1, string thongTin2, string thongTin3, string thongTin4, string tieuDeChiTiet,
            string tongKet1, string tongKet2, string tongKet3, string ghiChu)
        {
            return new Dictionary<string, string>
            {
                { "pTenCuaHang", "PNJ MANAGER - HỆ THỐNG QUẢN LÝ CỬA HÀNG ĐÁ QUÝ" },
                { "pTieuDe", tieuDe },
                { "pMaChungTu", maChungTu },
                { "pThongTin1", thongTin1 },
                { "pThongTin2", thongTin2 },
                { "pThongTin3", thongTin3 },
                { "pThongTin4", thongTin4 },
                { "pTieuDeChiTiet", tieuDeChiTiet },
                { "pTongKet1", tongKet1 },
                { "pTongKet2", tongKet2 },
                { "pTongKet3", tongKet3 },
                { "pGhiChu", ghiChu },
                { "pNgayIn", "Ngày in: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") }
            };
        }

        private static DongBaoCao Dong(int soThuTu, string nhan, string giaTri)
        {
            return new DongBaoCao { SoThuTu = soThuTu, NoiDung = nhan + ": " + (giaTri ?? string.Empty) };
        }

        private static string DinhDangTien(decimal giaTri) => giaTri.ToString("N0", VanHoaVietNam) + " đ";
        private static string DinhDangNgay(DateTime? giaTri) => giaTri.HasValue ? giaTri.Value.ToString("dd/MM/yyyy HH:mm") : "Chưa có";

        private static string TenTrangThaiBaoHanh(string ma)
        {
            switch (ma)
            {
                case "TIEP_NHAN": return "Tiếp nhận";
                case "DANG_XU_LY": return "Đang xử lý";
                case "HOAN_THANH": return "Hoàn thành";
                case "DA_TRA": return "Đã trả";
                default: return ma;
            }
        }
    }
}
