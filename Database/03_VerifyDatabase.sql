USE QL_CuaHangDaQuy_PNJ;
GO

SET NOCOUNT ON;

DECLARE @SoBangNghiepVu INT =
(
    SELECT COUNT(*)
    FROM sys.tables
    WHERE schema_id = SCHEMA_ID(N'dbo')
      AND name IN
      (
          N'NhanVien', N'TaiKhoan', N'KhachHang', N'DanhMuc', N'NhaCungCap',
          N'ChatLieu', N'SanPham', N'ChiTietChatLieu', N'HoaDon', N'ChiTietHoaDon',
          N'PhieuNhap', N'ChiTietPhieuNhap', N'PhieuThuMua', N'ChiTietPhieuThuMua',
          N'PhieuBaoHanh', N'MauEmail', N'NhatKyGuiEmail'
      )
);

IF @SoBangNghiepVu <> 17
    THROW 50010, N'CSDL không có đủ 17 bảng nghiệp vụ.', 1;

IF COL_LENGTH(N'dbo.PhieuThuMua', N'MaPhieuNguon') IS NULL
    THROW 50020, N'Bảng PhieuThuMua chưa có cột MaPhieuNguon phục vụ import Excel.', 1;

IF EXISTS
(
    SELECT MaPhieuNguon
    FROM dbo.PhieuThuMua
    WHERE MaPhieuNguon IS NOT NULL
    GROUP BY MaPhieuNguon
    HAVING COUNT(*) > 1
)
    THROW 50021, N'Dữ liệu PhieuThuMua có MaPhieuNguon bị trùng.', 1;

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.PhieuThuMua')
      AND name = N'UX_PhieuThuMua_MaNguon'
      AND is_unique = 1
      AND has_filter = 1
)
    THROW 50022, N'Chưa có chỉ mục duy nhất lọc UX_PhieuThuMua_MaNguon.', 1;

DECLARE @SoDong TABLE
(
    ThuTu   INT NOT NULL,
    TenBang SYSNAME NOT NULL,
    SoDong  BIGINT NOT NULL
);

INSERT @SoDong (ThuTu, TenBang, SoDong)
VALUES
    (1, N'NhanVien', (SELECT COUNT_BIG(*) FROM dbo.NhanVien)),
    (2, N'TaiKhoan', (SELECT COUNT_BIG(*) FROM dbo.TaiKhoan)),
    (3, N'KhachHang', (SELECT COUNT_BIG(*) FROM dbo.KhachHang)),
    (4, N'DanhMuc', (SELECT COUNT_BIG(*) FROM dbo.DanhMuc)),
    (5, N'NhaCungCap', (SELECT COUNT_BIG(*) FROM dbo.NhaCungCap)),
    (6, N'ChatLieu', (SELECT COUNT_BIG(*) FROM dbo.ChatLieu)),
    (7, N'SanPham', (SELECT COUNT_BIG(*) FROM dbo.SanPham)),
    (8, N'ChiTietChatLieu', (SELECT COUNT_BIG(*) FROM dbo.ChiTietChatLieu)),
    (9, N'HoaDon', (SELECT COUNT_BIG(*) FROM dbo.HoaDon)),
    (10, N'ChiTietHoaDon', (SELECT COUNT_BIG(*) FROM dbo.ChiTietHoaDon)),
    (11, N'PhieuNhap', (SELECT COUNT_BIG(*) FROM dbo.PhieuNhap)),
    (12, N'ChiTietPhieuNhap', (SELECT COUNT_BIG(*) FROM dbo.ChiTietPhieuNhap)),
    (13, N'PhieuThuMua', (SELECT COUNT_BIG(*) FROM dbo.PhieuThuMua)),
    (14, N'ChiTietPhieuThuMua', (SELECT COUNT_BIG(*) FROM dbo.ChiTietPhieuThuMua)),
    (15, N'PhieuBaoHanh', (SELECT COUNT_BIG(*) FROM dbo.PhieuBaoHanh)),
    (16, N'MauEmail', (SELECT COUNT_BIG(*) FROM dbo.MauEmail)),
    (17, N'NhatKyGuiEmail', (SELECT COUNT_BIG(*) FROM dbo.NhatKyGuiEmail));

SELECT TenBang, SoDong,
       CASE WHEN SoDong >= 6 THEN N'ĐẠT' ELSE N'CHƯA ĐẠT' END AS KetQua
FROM @SoDong
ORDER BY ThuTu;

IF EXISTS (SELECT 1 FROM @SoDong WHERE SoDong < 6)
    THROW 50011, N'Có bảng nghiệp vụ chưa đủ 6 dòng dữ liệu mẫu.', 1;

IF EXISTS
(
    SELECT 1
    FROM sys.check_constraints
    WHERE parent_object_id IN
    (
        SELECT object_id
        FROM sys.tables
        WHERE schema_id = SCHEMA_ID(N'dbo')
    )
      AND (is_disabled = 1 OR is_not_trusted = 1)
)
    THROW 50012, N'Có CHECK constraint bị tắt hoặc không được SQL Server tin cậy.', 1;

IF EXISTS
(
    SELECT 1
    FROM dbo.HoaDon hd
    CROSS APPLY
    (
        SELECT SUM(ct.ThanhTien) AS TongChiTiet
        FROM dbo.ChiTietHoaDon ct
        WHERE ct.HoaDonId = hd.HoaDonId
    ) totals
    WHERE totals.TongChiTiet IS NULL
       OR hd.TongTien <> totals.TongChiTiet
       OR hd.ThanhTien <> totals.TongChiTiet - hd.GiamGia
)
    THROW 50013, N'Tổng tiền hóa đơn không khớp chi tiết.', 1;

IF EXISTS
(
    SELECT 1
    FROM dbo.PhieuNhap pn
    CROSS APPLY
    (
        SELECT SUM(ct.ThanhTien) AS TongChiTiet
        FROM dbo.ChiTietPhieuNhap ct
        WHERE ct.PhieuNhapId = pn.PhieuNhapId
    ) totals
    WHERE totals.TongChiTiet IS NULL
       OR pn.TongTienNhap <> totals.TongChiTiet
)
    THROW 50014, N'Tổng tiền phiếu nhập không khớp chi tiết.', 1;

IF EXISTS
(
    SELECT 1
    FROM dbo.PhieuThuMua ptm
    CROSS APPLY
    (
        SELECT SUM(ct.ThanhTien) AS TongChiTiet
        FROM dbo.ChiTietPhieuThuMua ct
        WHERE ct.PhieuThuMuaId = ptm.PhieuThuMuaId
    ) totals
    WHERE totals.TongChiTiet IS NULL
       OR ptm.TongTienThuMua <> totals.TongChiTiet
)
    THROW 50015, N'Tổng tiền phiếu thu mua không khớp chi tiết.', 1;

IF NOT EXISTS
(
    SELECT 1
    FROM dbo.KhachHang
    WHERE HoTen = N'Khách lẻ'
      AND SoDienThoai = '0000000000'
      AND ChoPhepNhanEmail = 0
      AND DangHoatDong = 1
)
    THROW 50016, N'Bản ghi Khách lẻ không đúng quy ước.', 1;

IF EXISTS
(
    SELECT 1
    FROM dbo.NhatKyGuiEmail nk
    JOIN dbo.KhachHang kh ON kh.KhachHangId = nk.KhachHangId
    WHERE kh.SoDienThoai = '0000000000'
)
    THROW 50017, N'Khách lẻ không được xuất hiện trong nhật ký gửi email.', 1;

IF (SELECT COUNT(*) FROM dbo.SanPham WHERE DuongDanAnh LIKE N'Resources\%.png') < 10
    THROW 50018, N'Chưa có đủ 10 đường dẫn ảnh sản phẩm mẫu.', 1;

IF NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan WHERE DangHoatDong = 0)
   OR NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan WHERE PhaiDoiMatKhau = 1)
   OR NOT EXISTS (SELECT 1 FROM dbo.HoaDon WHERE TrangThai = 'DA_HUY')
   OR NOT EXISTS (SELECT 1 FROM dbo.PhieuNhap WHERE TrangThai = 'DA_HUY')
   OR NOT EXISTS (SELECT 1 FROM dbo.PhieuThuMua WHERE TrangThai = 'DA_HUY')
   OR NOT EXISTS (SELECT 1 FROM dbo.NhatKyGuiEmail WHERE TrangThai = 'THAT_BAI')
    THROW 50019, N'Dữ liệu mẫu chưa có đủ trạng thái cần dùng cho demo.', 1;

SELECT
    sp.SanPhamId,
    CONCAT('SP', RIGHT('000000' + CONVERT(VARCHAR(6), sp.SanPhamId), 6)) AS MaHienThi,
    sp.TenSanPham,
    dm.TenDanhMuc,
    sp.GiaVon,
    sp.GiaBan,
    sp.SoLuongTon,
    sp.DuongDanAnh
FROM dbo.SanPham sp
JOIN dbo.DanhMuc dm ON dm.DanhMucId = sp.DanhMucId
ORDER BY sp.SanPhamId;

PRINT N'KIỂM TRA THÀNH CÔNG: đủ 17 bảng, mỗi bảng có ít nhất 6 dòng và dữ liệu tổng hợp hợp lệ.';
GO
