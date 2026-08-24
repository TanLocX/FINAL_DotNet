USE QL_CuaHangDaQuy_PNJ;
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET QUOTED_IDENTIFIER ON;
SET NUMERIC_ROUNDABORT OFF;

IF
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
) <> 17
BEGIN
    THROW 50002, N'CSDL chưa có đủ 17 bảng. Hãy chạy 01_CreateDatabase.sql trước.', 1;
END;

IF EXISTS
(
    SELECT 1
    FROM sys.tables t
    JOIN sys.partitions p ON p.object_id = t.object_id AND p.index_id IN (0, 1)
    WHERE t.schema_id = SCHEMA_ID(N'dbo')
      AND t.name IN
      (
          N'NhanVien', N'TaiKhoan', N'KhachHang', N'DanhMuc', N'NhaCungCap',
          N'ChatLieu', N'SanPham', N'ChiTietChatLieu', N'HoaDon', N'ChiTietHoaDon',
          N'PhieuNhap', N'ChiTietPhieuNhap', N'PhieuThuMua', N'ChiTietPhieuThuMua',
          N'PhieuBaoHanh', N'MauEmail', N'NhatKyGuiEmail'
      )
    GROUP BY t.object_id
    HAVING SUM(p.rows) > 0
)
BEGIN
    THROW 50003, N'CSDL đã có dữ liệu. Script dừng để tránh seed trùng hoặc ghi đè.', 1;
END;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    INSERT dbo.NhanVien
        (HoTen, GioiTinh, NgaySinh, SoDienThoai, Email, DiaChi, ChucVu, DangLamViec)
    VALUES
        (N'Nguyễn Minh Anh', N'Nam', '1988-03-12', '0901000001', 'minhanh@pnj-demo.local', N'Quận 1, TP.HCM', N'Quản lý cửa hàng', 1),
        (N'Trần Ngọc Lan', N'Nữ', '1994-07-25', '0901000002', 'ngoclan@pnj-demo.local', N'Quận 3, TP.HCM', N'Nhân viên bán hàng', 1),
        (N'Lê Hoàng Nam', N'Nam', '1992-11-08', '0901000003', 'hoangnam@pnj-demo.local', N'Quận Bình Thạnh, TP.HCM', N'Nhân viên kho', 1),
        (N'Phạm Thu Hà', N'Nữ', '1996-01-19', '0901000004', 'thuha@pnj-demo.local', N'Thành phố Thủ Đức, TP.HCM', N'Chăm sóc khách hàng', 1),
        (N'Võ Quốc Bảo', N'Nam', '1990-05-30', '0901000005', 'quocbao@pnj-demo.local', N'Quận 7, TP.HCM', N'Nhân viên thu mua', 1),
        (N'Đặng Mỹ Linh', N'Nữ', '1998-09-14', '0901000006', 'mylinh@pnj-demo.local', N'Quận Tân Bình, TP.HCM', N'Nhân viên bán hàng', 0);

    -- Mật khẩu demo cho cả 6 tài khoản: PnjDemo@123
    INSERT dbo.TaiKhoan
        (NhanVienId, TenDangNhap, MatKhauHash, VaiTro, PhaiDoiMatKhau, DangHoatDong)
    SELECT nv.NhanVienId, seed.TenDangNhap, seed.MatKhauHash, seed.VaiTro,
           seed.PhaiDoiMatKhau, seed.DangHoatDong
    FROM
    (
        VALUES
            ('0901000001', 'admin',    '$2a$11$gD6xjcAIROGd16kLuTzc5.sLNP7TpiFh0qS7WkMPcQOcToXho0K26', 'ADMIN',     0, 1),
            ('0901000002', 'ngoclan',  '$2a$11$MS8pGt7CBeV4HBOnRB6rseof.uQrSuDMi1Xy8yZ6EmUfTWqW4JNxS', 'NHANVIEN', 0, 1),
            ('0901000003', 'hoangnam', '$2a$11$aINU/WFFJW8jlL4lP.bhdemw3lV5VkxngbiqCp9H3RVDW6E8Sf2ce', 'NHANVIEN', 0, 1),
            ('0901000004', 'thuha',    '$2a$11$krSEwKvdugJ5k5KVsZaCiuc3jZ1amlgiJN4yDwW3Nxfw//y6f119m', 'NHANVIEN', 1, 1),
            ('0901000005', 'quocbao',  '$2a$11$nNsSC4r3E9hI..uK03uyj.cRSEc4K0WpqifeF4E0R8rBv.Vh.bdpi', 'NHANVIEN', 0, 1),
            ('0901000006', 'mylinh',   '$2a$11$lle/VpG4YrTYx7KaDFlGpefzCpVxkVjtPEBZM3YxcRTkVNMIgPVFe', 'NHANVIEN', 0, 0)
    ) AS seed(SoDienThoaiNhanVien, TenDangNhap, MatKhauHash, VaiTro, PhaiDoiMatKhau, DangHoatDong)
    JOIN dbo.NhanVien nv ON nv.SoDienThoai = seed.SoDienThoaiNhanVien;

    INSERT dbo.KhachHang
        (HoTen, SoDienThoai, Email, DiaChi, NgaySinh, ChoPhepNhanEmail, DiemTichLuy, DangHoatDong)
    VALUES
        (N'Khách lẻ', '0000000000', NULL, NULL, NULL, 0, 0, 1),
        (N'Nguyễn Thảo Vy', '0912000001', 'thaovy@example.com', N'Quận 5, TP.HCM', '1995-04-18', 1, 1250, 1),
        (N'Trần Gia Hân', '0912000002', 'giahan@example.com', N'Quận 10, TP.HCM', '1991-10-02', 1, 860, 1),
        (N'Lê Đức Thành', '0912000003', 'ducthanh@example.com', N'Thành phố Thủ Đức, TP.HCM', '1987-06-21', 0, 430, 1),
        (N'Phạm Khánh Linh', '0912000004', 'khanhlinh@example.com', N'Quận 7, TP.HCM', '1998-02-11', 1, 720, 1),
        (N'Vũ Hoàng Yến', '0912000005', 'hoangyen@example.com', N'Quận Phú Nhuận, TP.HCM', '1993-12-09', 1, 510, 1),
        (N'Đỗ Quang Huy', '0912000006', 'quanghuy@example.com', N'Quận Bình Tân, TP.HCM', '1989-08-16', 1, 300, 1),
        (N'Bùi Mai Chi', '0912000007', 'maichi@example.com', N'Quận Gò Vấp, TP.HCM', '1997-05-27', 1, 150, 0);

    INSERT dbo.DanhMuc (TenDanhMuc, MoTa, DangHoatDong)
    VALUES
        (N'Nhẫn', N'Nhẫn cưới, nhẫn thời trang và nhẫn đá quý', 1),
        (N'Bông tai', N'Bông tai kim loại quý và đá quý', 1),
        (N'Dây chuyền', N'Dây chuyền vàng, bạc và bạch kim', 1),
        (N'Vòng tay', N'Vòng tay trang sức các loại', 1),
        (N'Lắc chân', N'Lắc chân vàng và bạc', 1),
        (N'Mặt dây chuyền', N'Mặt dây chuyền kim loại quý và đá quý', 1);

    INSERT dbo.NhaCungCap
        (TenNhaCungCap, NguoiLienHe, SoDienThoai, Email, DiaChi, DangHoatDong)
    VALUES
        (N'Công ty Vàng Sài Gòn', N'Nguyễn Văn Lâm', '0283800001', 'lienhe@vangsaigon.example', N'Quận 1, TP.HCM', 1),
        (N'Đá quý Á Châu', N'Trần Kim Ngân', '0283800002', 'sales@aquyachau.example', N'Quận 3, TP.HCM', 1),
        (N'Bạc Việt 925', N'Lê Thành Công', '0283800003', 'hello@bacviet.example', N'Quận 5, TP.HCM', 1),
        (N'Kim cương Hoàn Mỹ', N'Phạm Ngọc Diệp', '0283800004', 'contact@hoanmydiamond.example', N'Quận 7, TP.HCM', 1),
        (N'Bạch kim Đông Dương', N'Võ Minh Tú', '0283800005', 'info@bachkimdongduong.example', N'Thành phố Thủ Đức, TP.HCM', 1),
        (N'Ruby Việt Nam', N'Đặng Hồng Nhung', '0283800006', 'sales@rubyvietnam.example', N'Quận Tân Bình, TP.HCM', 1),
        (N'Sapphire Phương Nam', N'Bùi Thanh Sơn', '0283800007', 'hello@sapphirephuongnam.example', N'Quận Phú Nhuận, TP.HCM', 1),
        (N'Ngọc lục bảo Gia Bảo', N'Đỗ Mỹ Duyên', '0283800008', 'contact@giabaoemerald.example', N'Quận Bình Thạnh, TP.HCM', 0);

    INSERT dbo.ChatLieu (TenChatLieu, GiaMuaVao, GiaBanRa, DangHoatDong)
    VALUES
        (N'Vàng 24K', 2050000, 2250000, 1),
        (N'Vàng 18K', 1500000, 1750000, 1),
        (N'Vàng 14K', 1100000, 1350000, 1),
        (N'Bạc 925', 18000, 35000, 1),
        (N'Bạch kim', 900000, 1250000, 1),
        (N'Kim cương', 18000000, 25000000, 1),
        (N'Ruby', 8500000, 12000000, 1),
        (N'Sapphire', 7500000, 11000000, 1),
        (N'Emerald', 9000000, 13000000, 1);

    INSERT dbo.SanPham
        (DanhMucId, TenSanPham, GiaVon, GiaBan, SoLuongTon, DuongDanAnh, DangKinhDoanh)
    SELECT dm.DanhMucId, seed.TenSanPham, seed.GiaVon, seed.GiaBan,
           seed.SoLuongTon, seed.DuongDanAnh, seed.DangKinhDoanh
    FROM
    (
        VALUES
            (N'Bông tai', N'Bông tai kim cương bạch kim', 35000000.00, 49000000.00, 4, N'Resources\bong_tai_kim_cuong_bach_kim.png', 1),
            (N'Bông tai', N'Bông tai ruby vàng 18K', 18000000.00, 25000000.00, 6, N'Resources\bong_tai_ruby_vang_18k.png', 1),
            (N'Dây chuyền', N'Dây chuyền bạc 925', 1200000.00, 2200000.00, 15, N'Resources\day_chuyen_bac_925.png', 1),
            (N'Dây chuyền', N'Dây chuyền sapphire bạch kim', 42000000.00, 59000000.00, 3, N'Resources\day_chuyen_sapphire_bach_kim.png', 1),
            (N'Lắc chân', N'Lắc chân vàng 14K', 5500000.00, 8500000.00, 8, N'Resources\lac_chan_vang_14k.png', 1),
            (N'Nhẫn', N'Nhẫn emerald vàng 18K', 22000000.00, 31500000.00, 5, N'Resources\nhan_emerald_vang_18k.png', 1),
            (N'Nhẫn', N'Nhẫn kim cương 18K', 30000000.00, 45000000.00, 4, N'Resources\nhan_kim_cuong_18k.png', 1),
            (N'Nhẫn', N'Nhẫn vàng 24K trơn', 10500000.00, 13000000.00, 10, N'Resources\nhan_vang_24k_tron.png', 1),
            (N'Vòng tay', N'Vòng tay bạc 925 trơn', 1400000.00, 2500000.00, 12, N'Resources\vong_tay_bac_925_tron.png', 1),
            (N'Vòng tay', N'Vòng tay vàng 24K', 25000000.00, 32500000.00, 6, N'Resources\vong_tay_vang_24k.png', 1)
    ) AS seed(TenDanhMuc, TenSanPham, GiaVon, GiaBan, SoLuongTon, DuongDanAnh, DangKinhDoanh)
    JOIN dbo.DanhMuc dm ON dm.TenDanhMuc = seed.TenDanhMuc;

    INSERT dbo.ChiTietChatLieu (SanPhamId, ChatLieuId, TrongLuong, DonViTinh)
    SELECT sp.SanPhamId, cl.ChatLieuId, seed.TrongLuong, seed.DonViTinh
    FROM
    (
        VALUES
            (N'Bông tai kim cương bạch kim', N'Bạch kim', 6.500, N'gram'),
            (N'Bông tai kim cương bạch kim', N'Kim cương', 1.200, N'carat'),
            (N'Bông tai ruby vàng 18K', N'Vàng 18K', 5.800, N'gram'),
            (N'Bông tai ruby vàng 18K', N'Ruby', 1.400, N'carat'),
            (N'Dây chuyền bạc 925', N'Bạc 925', 15.000, N'gram'),
            (N'Dây chuyền sapphire bạch kim', N'Bạch kim', 12.000, N'gram'),
            (N'Dây chuyền sapphire bạch kim', N'Sapphire', 2.500, N'carat'),
            (N'Lắc chân vàng 14K', N'Vàng 14K', 8.000, N'gram'),
            (N'Nhẫn emerald vàng 18K', N'Vàng 18K', 5.000, N'gram'),
            (N'Nhẫn emerald vàng 18K', N'Emerald', 1.200, N'carat'),
            (N'Nhẫn kim cương 18K', N'Vàng 18K', 4.500, N'gram'),
            (N'Nhẫn kim cương 18K', N'Kim cương', 0.800, N'carat'),
            (N'Nhẫn vàng 24K trơn', N'Vàng 24K', 7.500, N'gram'),
            (N'Vòng tay bạc 925 trơn', N'Bạc 925', 18.000, N'gram'),
            (N'Vòng tay vàng 24K', N'Vàng 24K', 20.000, N'gram')
    ) AS seed(TenSanPham, TenChatLieu, TrongLuong, DonViTinh)
    JOIN dbo.SanPham sp ON sp.TenSanPham = seed.TenSanPham
    JOIN dbo.ChatLieu cl ON cl.TenChatLieu = seed.TenChatLieu;

    INSERT dbo.HoaDon
        (NhanVienId, KhachHangId, NgayLap, TongTien, GiamGia, ThanhTien, PhuongThucThanhToan, TrangThai)
    SELECT nv.NhanVienId, kh.KhachHangId, seed.NgayLap, 0, seed.GiamGia, 0,
           seed.PhuongThucThanhToan, seed.TrangThai
    FROM
    (
        VALUES
            ('0901000002', '0000000000', CONVERT(DATETIME2, '2026-05-05T09:00:00'), 0.00, N'Tiền mặt', 'DA_THANH_TOAN'),
            ('0901000002', '0912000001', CONVERT(DATETIME2, '2026-05-12T10:30:00'), 500000.00, N'Chuyển khoản', 'DA_THANH_TOAN'),
            ('0901000001', '0912000002', CONVERT(DATETIME2, '2026-05-20T14:15:00'), 1000000.00, N'Thẻ ngân hàng', 'DA_THANH_TOAN'),
            ('0901000004', '0912000003', CONVERT(DATETIME2, '2026-06-03T16:20:00'), 0.00, N'Tiền mặt', 'DA_THANH_TOAN'),
            ('0901000002', '0912000004', CONVERT(DATETIME2, '2026-06-15T11:10:00'), 300000.00, N'Chuyển khoản', 'DA_HUY'),
            ('0901000001', '0912000005', CONVERT(DATETIME2, '2026-07-01T13:40:00'), 1500000.00, N'Thẻ ngân hàng', 'DA_THANH_TOAN'),
            ('0901000004', '0912000006', CONVERT(DATETIME2, '2026-07-12T09:45:00'), 0.00, N'Tiền mặt', 'DA_HUY'),
            ('0901000002', '0912000001', CONVERT(DATETIME2, '2026-07-20T15:05:00'), 750000.00, N'Chuyển khoản', 'DA_THANH_TOAN')
    ) AS seed(SoDienThoaiNhanVien, SoDienThoaiKhachHang, NgayLap, GiamGia, PhuongThucThanhToan, TrangThai)
    JOIN dbo.NhanVien nv ON nv.SoDienThoai = seed.SoDienThoaiNhanVien
    JOIN dbo.KhachHang kh ON kh.SoDienThoai = seed.SoDienThoaiKhachHang;

    INSERT dbo.ChiTietHoaDon (HoaDonId, SanPhamId, SoLuong, DonGiaBan, HanBaoHanh)
    SELECT hd.HoaDonId, sp.SanPhamId, seed.SoLuong, seed.DonGiaBan, seed.HanBaoHanh
    FROM
    (
        VALUES
            (CONVERT(DATETIME2, '2026-05-05T09:00:00'), N'Dây chuyền bạc 925', 1, 2200000.00, CONVERT(DATE, '2027-05-05')),
            (CONVERT(DATETIME2, '2026-05-05T09:00:00'), N'Vòng tay bạc 925 trơn', 1, 2500000.00, CONVERT(DATE, '2027-05-05')),
            (CONVERT(DATETIME2, '2026-05-12T10:30:00'), N'Nhẫn vàng 24K trơn', 1, 13000000.00, CONVERT(DATE, '2027-05-12')),
            (CONVERT(DATETIME2, '2026-05-12T10:30:00'), N'Lắc chân vàng 14K', 1, 8500000.00, CONVERT(DATE, '2027-05-12')),
            (CONVERT(DATETIME2, '2026-05-20T14:15:00'), N'Nhẫn kim cương 18K', 1, 45000000.00, CONVERT(DATE, '2028-05-20')),
            (CONVERT(DATETIME2, '2026-05-20T14:15:00'), N'Bông tai kim cương bạch kim', 1, 49000000.00, CONVERT(DATE, '2028-05-20')),
            (CONVERT(DATETIME2, '2026-06-03T16:20:00'), N'Bông tai ruby vàng 18K', 1, 25000000.00, CONVERT(DATE, '2027-06-03')),
            (CONVERT(DATETIME2, '2026-06-03T16:20:00'), N'Dây chuyền bạc 925', 2, 2200000.00, CONVERT(DATE, '2027-06-03')),
            (CONVERT(DATETIME2, '2026-06-15T11:10:00'), N'Vòng tay bạc 925 trơn', 2, 2500000.00, CONVERT(DATE, '2027-06-15')),
            (CONVERT(DATETIME2, '2026-06-15T11:10:00'), N'Lắc chân vàng 14K', 1, 8500000.00, CONVERT(DATE, '2027-06-15')),
            (CONVERT(DATETIME2, '2026-07-01T13:40:00'), N'Dây chuyền sapphire bạch kim', 1, 59000000.00, CONVERT(DATE, '2028-07-01')),
            (CONVERT(DATETIME2, '2026-07-01T13:40:00'), N'Nhẫn emerald vàng 18K', 1, 31500000.00, CONVERT(DATE, '2028-07-01')),
            (CONVERT(DATETIME2, '2026-07-12T09:45:00'), N'Vòng tay vàng 24K', 1, 32500000.00, CONVERT(DATE, '2027-07-12')),
            (CONVERT(DATETIME2, '2026-07-12T09:45:00'), N'Nhẫn vàng 24K trơn', 1, 13000000.00, CONVERT(DATE, '2027-07-12')),
            (CONVERT(DATETIME2, '2026-07-20T15:05:00'), N'Bông tai ruby vàng 18K', 1, 25000000.00, CONVERT(DATE, '2027-07-20')),
            (CONVERT(DATETIME2, '2026-07-20T15:05:00'), N'Nhẫn emerald vàng 18K', 1, 31500000.00, CONVERT(DATE, '2028-07-20'))
    ) AS seed(NgayLap, TenSanPham, SoLuong, DonGiaBan, HanBaoHanh)
    JOIN dbo.HoaDon hd ON hd.NgayLap = seed.NgayLap
    JOIN dbo.SanPham sp ON sp.TenSanPham = seed.TenSanPham;

    UPDATE hd
    SET hd.TongTien = totals.TongTien,
        hd.ThanhTien = totals.TongTien - hd.GiamGia
    FROM dbo.HoaDon hd
    JOIN
    (
        SELECT HoaDonId, SUM(ThanhTien) AS TongTien
        FROM dbo.ChiTietHoaDon
        GROUP BY HoaDonId
    ) totals ON totals.HoaDonId = hd.HoaDonId;

    INSERT dbo.PhieuNhap
        (NhanVienId, NhaCungCapId, NgayNhap, TongTienNhap, TrangThai, GhiChu)
    SELECT nv.NhanVienId, ncc.NhaCungCapId, seed.NgayNhap, 0, seed.TrangThai, seed.GhiChu
    FROM
    (
        VALUES
            ('0901000003', '0283800001', CONVERT(DATETIME2, '2026-04-02T08:30:00'), 'HOAN_THANH', N'Nhập vàng đầu tháng'),
            ('0901000003', '0283800002', CONVERT(DATETIME2, '2026-04-08T09:15:00'), 'HOAN_THANH', N'Nhập đá quý'),
            ('0901000001', '0283800003', CONVERT(DATETIME2, '2026-04-16T10:00:00'), 'HOAN_THANH', N'Bổ sung sản phẩm bạc'),
            ('0901000003', '0283800004', CONVERT(DATETIME2, '2026-05-02T13:20:00'), 'HOAN_THANH', N'Nhập sản phẩm kim cương'),
            ('0901000001', '0283800005', CONVERT(DATETIME2, '2026-05-18T14:45:00'), 'DA_HUY', N'Hủy do sai quy cách'),
            ('0901000003', '0283800006', CONVERT(DATETIME2, '2026-06-05T09:40:00'), 'HOAN_THANH', N'Nhập trang sức ruby'),
            ('0901000003', '0283800007', CONVERT(DATETIME2, '2026-06-22T11:25:00'), 'HOAN_THANH', N'Nhập trang sức sapphire'),
            ('0901000001', '0283800008', CONVERT(DATETIME2, '2026-07-06T15:30:00'), 'DA_HUY', N'Nhà cung cấp tạm ngừng hoạt động')
    ) AS seed(SoDienThoaiNhanVien, SoDienThoaiNCC, NgayNhap, TrangThai, GhiChu)
    JOIN dbo.NhanVien nv ON nv.SoDienThoai = seed.SoDienThoaiNhanVien
    JOIN dbo.NhaCungCap ncc ON ncc.SoDienThoai = seed.SoDienThoaiNCC;

    INSERT dbo.ChiTietPhieuNhap (PhieuNhapId, SanPhamId, SoLuong, DonGiaNhap)
    SELECT pn.PhieuNhapId, sp.SanPhamId, seed.SoLuong, seed.DonGiaNhap
    FROM
    (
        VALUES
            (CONVERT(DATETIME2, '2026-04-02T08:30:00'), N'Nhẫn vàng 24K trơn', 8, 10000000.00),
            (CONVERT(DATETIME2, '2026-04-02T08:30:00'), N'Vòng tay vàng 24K', 5, 24000000.00),
            (CONVERT(DATETIME2, '2026-04-08T09:15:00'), N'Bông tai ruby vàng 18K', 5, 17500000.00),
            (CONVERT(DATETIME2, '2026-04-08T09:15:00'), N'Nhẫn emerald vàng 18K', 4, 21500000.00),
            (CONVERT(DATETIME2, '2026-04-16T10:00:00'), N'Dây chuyền bạc 925', 12, 1150000.00),
            (CONVERT(DATETIME2, '2026-04-16T10:00:00'), N'Vòng tay bạc 925 trơn', 10, 1350000.00),
            (CONVERT(DATETIME2, '2026-05-02T13:20:00'), N'Nhẫn kim cương 18K', 3, 29000000.00),
            (CONVERT(DATETIME2, '2026-05-02T13:20:00'), N'Bông tai kim cương bạch kim', 3, 34000000.00),
            (CONVERT(DATETIME2, '2026-05-18T14:45:00'), N'Dây chuyền sapphire bạch kim', 2, 41000000.00),
            (CONVERT(DATETIME2, '2026-05-18T14:45:00'), N'Bông tai kim cương bạch kim', 1, 34500000.00),
            (CONVERT(DATETIME2, '2026-06-05T09:40:00'), N'Bông tai ruby vàng 18K', 4, 18000000.00),
            (CONVERT(DATETIME2, '2026-06-05T09:40:00'), N'Lắc chân vàng 14K', 6, 5300000.00),
            (CONVERT(DATETIME2, '2026-06-22T11:25:00'), N'Dây chuyền sapphire bạch kim', 3, 42000000.00),
            (CONVERT(DATETIME2, '2026-06-22T11:25:00'), N'Nhẫn emerald vàng 18K', 3, 22000000.00),
            (CONVERT(DATETIME2, '2026-07-06T15:30:00'), N'Nhẫn emerald vàng 18K', 2, 21800000.00),
            (CONVERT(DATETIME2, '2026-07-06T15:30:00'), N'Nhẫn kim cương 18K', 2, 29800000.00)
    ) AS seed(NgayNhap, TenSanPham, SoLuong, DonGiaNhap)
    JOIN dbo.PhieuNhap pn ON pn.NgayNhap = seed.NgayNhap
    JOIN dbo.SanPham sp ON sp.TenSanPham = seed.TenSanPham;

    UPDATE pn
    SET pn.TongTienNhap = totals.TongTienNhap
    FROM dbo.PhieuNhap pn
    JOIN
    (
        SELECT PhieuNhapId, SUM(ThanhTien) AS TongTienNhap
        FROM dbo.ChiTietPhieuNhap
        GROUP BY PhieuNhapId
    ) totals ON totals.PhieuNhapId = pn.PhieuNhapId;

    INSERT dbo.PhieuThuMua
        (MaPhieuNguon, NhanVienId, KhachHangId, NgayThuMua, TongTienThuMua, TrangThai, GhiChu)
    SELECT seed.MaPhieuNguon, nv.NhanVienId, kh.KhachHangId, seed.NgayThuMua, 0, seed.TrangThai, seed.GhiChu
    FROM
    (
        VALUES
            (N'SEED-TM-001', '0901000005', '0912000001', CONVERT(DATETIME2, '2026-05-07T09:20:00'), 'HOAN_THANH', N'Thu mua vàng cũ'),
            (N'SEED-TM-002', '0901000005', '0912000002', CONVERT(DATETIME2, '2026-05-16T14:10:00'), 'HOAN_THANH', N'Thu mua trang sức bạc'),
            (N'SEED-TM-003', '0901000001', '0912000003', CONVERT(DATETIME2, '2026-05-28T10:35:00'), 'HOAN_THANH', N'Thu mua nhẫn cũ'),
            (N'SEED-TM-004', '0901000005', '0912000004', CONVERT(DATETIME2, '2026-06-09T15:25:00'), 'DA_HUY', N'Khách hàng đổi ý'),
            (N'SEED-TM-005', '0901000005', '0912000005', CONVERT(DATETIME2, '2026-06-19T11:45:00'), 'HOAN_THANH', N'Thu mua vàng 18K'),
            (N'SEED-TM-006', '0901000001', '0912000006', CONVERT(DATETIME2, '2026-07-03T13:15:00'), 'HOAN_THANH', N'Thu mua bạch kim'),
            (N'SEED-TM-007', '0901000005', '0912000001', CONVERT(DATETIME2, '2026-07-14T09:50:00'), 'HOAN_THANH', N'Thu mua lắc chân'),
            (N'SEED-TM-008', '0901000005', '0912000002', CONVERT(DATETIME2, '2026-07-25T16:05:00'), 'DA_HUY', N'Không đạt kiểm định')
    ) AS seed(MaPhieuNguon, SoDienThoaiNhanVien, SoDienThoaiKhachHang, NgayThuMua, TrangThai, GhiChu)
    JOIN dbo.NhanVien nv ON nv.SoDienThoai = seed.SoDienThoaiNhanVien
    JOIN dbo.KhachHang kh ON kh.SoDienThoai = seed.SoDienThoaiKhachHang;

    INSERT dbo.ChiTietPhieuThuMua
        (PhieuThuMuaId, ChatLieuId, SanPhamId, TenSanPhamThu, TrongLuong, DonViTinh, DonGiaThuMua)
    SELECT ptm.PhieuThuMuaId, cl.ChatLieuId, sp.SanPhamId, seed.TenSanPhamThu,
           seed.TrongLuong, seed.DonViTinh, seed.DonGiaThuMua
    FROM
    (
        VALUES
            (CONVERT(DATETIME2, '2026-05-07T09:20:00'), N'Vàng 24K', N'Nhẫn vàng 24K trơn', N'Nhẫn vàng 24K cũ', 6.200, N'gram', 2000000.00),
            (CONVERT(DATETIME2, '2026-05-07T09:20:00'), N'Vàng 18K', NULL, N'Dây chuyền vàng 18K cũ', 9.500, N'gram', 1450000.00),
            (CONVERT(DATETIME2, '2026-05-16T14:10:00'), N'Bạc 925', N'Vòng tay bạc 925 trơn', N'Vòng tay bạc cũ', 21.000, N'gram', 17000.00),
            (CONVERT(DATETIME2, '2026-05-28T10:35:00'), N'Vàng 18K', N'Nhẫn kim cương 18K', N'Nhẫn vàng 18K cũ', 4.100, N'gram', 1460000.00),
            (CONVERT(DATETIME2, '2026-05-28T10:35:00'), N'Kim cương', NULL, N'Viên kim cương tháo rời', 0.450, N'carat', 17500000.00),
            (CONVERT(DATETIME2, '2026-06-09T15:25:00'), N'Ruby', NULL, N'Viên ruby cũ', 0.900, N'carat', 8000000.00),
            (CONVERT(DATETIME2, '2026-06-19T11:45:00'), N'Vàng 18K', N'Bông tai ruby vàng 18K', N'Đôi bông tai vàng 18K cũ', 5.000, N'gram', 1480000.00),
            (CONVERT(DATETIME2, '2026-07-03T13:15:00'), N'Bạch kim', NULL, N'Nhẫn bạch kim cũ', 7.200, N'gram', 880000.00),
            (CONVERT(DATETIME2, '2026-07-14T09:50:00'), N'Vàng 14K', N'Lắc chân vàng 14K', N'Lắc chân vàng 14K cũ', 7.600, N'gram', 1050000.00),
            (CONVERT(DATETIME2, '2026-07-25T16:05:00'), N'Sapphire', NULL, N'Viên sapphire chưa kiểm định', 1.100, N'carat', 7000000.00)
    ) AS seed(NgayThuMua, TenChatLieu, TenSanPham, TenSanPhamThu, TrongLuong, DonViTinh, DonGiaThuMua)
    JOIN dbo.PhieuThuMua ptm ON ptm.NgayThuMua = seed.NgayThuMua
    JOIN dbo.ChatLieu cl ON cl.TenChatLieu = seed.TenChatLieu
    LEFT JOIN dbo.SanPham sp ON sp.TenSanPham = seed.TenSanPham;

    UPDATE ptm
    SET ptm.TongTienThuMua = totals.TongTienThuMua
    FROM dbo.PhieuThuMua ptm
    JOIN
    (
        SELECT PhieuThuMuaId, SUM(ThanhTien) AS TongTienThuMua
        FROM dbo.ChiTietPhieuThuMua
        GROUP BY PhieuThuMuaId
    ) totals ON totals.PhieuThuMuaId = ptm.PhieuThuMuaId;

    INSERT dbo.PhieuBaoHanh
        (ChiTietHoaDonId, NgayTiepNhan, NoiDungBaoHanh, TrangThai, NgayTraDuKien, NgayTraThucTe, GhiChu)
    SELECT cthd.ChiTietHoaDonId, seed.NgayTiepNhan, seed.NoiDungBaoHanh,
           seed.TrangThai, seed.NgayTraDuKien, seed.NgayTraThucTe, seed.GhiChu
    FROM
    (
        VALUES
            (CONVERT(DATETIME2, '2026-05-05T09:00:00'), N'Dây chuyền bạc 925', CONVERT(DATETIME2, '2026-06-10T09:00:00'), N'Làm sạch và đánh bóng', 'DA_TRA', CONVERT(DATE, '2026-06-12'), CONVERT(DATETIME2, '2026-06-12T16:00:00'), N'Khách đã nhận sản phẩm'),
            (CONVERT(DATETIME2, '2026-05-12T10:30:00'), N'Nhẫn vàng 24K trơn', CONVERT(DATETIME2, '2026-06-20T10:20:00'), N'Chỉnh lại kích thước nhẫn', 'HOAN_THANH', CONVERT(DATE, '2026-06-24'), CONVERT(DATETIME2, '2026-06-23T14:30:00'), N'Đã liên hệ khách'),
            (CONVERT(DATETIME2, '2026-05-20T14:15:00'), N'Nhẫn kim cương 18K', CONVERT(DATETIME2, '2026-07-02T13:10:00'), N'Kiểm tra và siết chấu kim cương', 'DANG_XU_LY', CONVERT(DATE, '2026-07-08'), NULL, N'Đang xử lý tại xưởng'),
            (CONVERT(DATETIME2, '2026-06-03T16:20:00'), N'Bông tai ruby vàng 18K', CONVERT(DATETIME2, '2026-07-10T15:00:00'), N'Thay khóa bông tai', 'TIEP_NHAN', CONVERT(DATE, '2026-07-15'), NULL, N'Đã nhận đủ đôi'),
            (CONVERT(DATETIME2, '2026-07-01T13:40:00'), N'Dây chuyền sapphire bạch kim', CONVERT(DATETIME2, '2026-07-18T11:30:00'), N'Kiểm tra móc khóa dây chuyền', 'DA_TRA', CONVERT(DATE, '2026-07-22'), CONVERT(DATETIME2, '2026-07-21T17:10:00'), N'Hoàn thành sớm'),
            (CONVERT(DATETIME2, '2026-07-20T15:05:00'), N'Nhẫn emerald vàng 18K', CONVERT(DATETIME2, '2026-08-02T09:40:00'), N'Đánh bóng và kiểm tra viên chủ', 'HOAN_THANH', CONVERT(DATE, '2026-08-07'), CONVERT(DATETIME2, '2026-08-06T15:20:00'), N'Chờ khách đến nhận')
    ) AS seed(NgayLapHoaDon, TenSanPham, NgayTiepNhan, NoiDungBaoHanh, TrangThai, NgayTraDuKien, NgayTraThucTe, GhiChu)
    JOIN dbo.HoaDon hd ON hd.NgayLap = seed.NgayLapHoaDon AND hd.TrangThai = 'DA_THANH_TOAN'
    JOIN dbo.ChiTietHoaDon cthd ON cthd.HoaDonId = hd.HoaDonId
    JOIN dbo.SanPham sp ON sp.SanPhamId = cthd.SanPhamId AND sp.TenSanPham = seed.TenSanPham;

    INSERT dbo.MauEmail
        (TenMau, TieuDeMau, NoiDungMau, DangHoatDong, TaiKhoanCapNhatId, NgayCapNhat)
    SELECT seed.TenMau, seed.TieuDeMau, seed.NoiDungMau, seed.DangHoatDong,
           tk.TaiKhoanId, seed.NgayCapNhat
    FROM
    (
        VALUES
            (N'Xác nhận hóa đơn', N'PNJ - Xác nhận hóa đơn {{MaHoaDon}}', N'Xin chào {{HoTen}}, cảm ơn bạn đã mua sắm. Tổng thanh toán: {{ThanhTien}}.', 1, 'admin', CONVERT(DATETIME2, '2026-05-01T08:00:00')),
            (N'Nhắc lịch bảo hành', N'PNJ - Thông tin bảo hành sản phẩm', N'Xin chào {{HoTen}}, sản phẩm {{TenSanPham}} của bạn có hạn bảo hành đến {{HanBaoHanh}}.', 1, 'thuha', CONVERT(DATETIME2, '2026-05-01T08:10:00')),
            (N'Thông báo hoàn thành bảo hành', N'PNJ - Sản phẩm bảo hành đã hoàn thành', N'Sản phẩm {{TenSanPham}} đã hoàn thành bảo hành. Mời bạn đến cửa hàng nhận sản phẩm.', 1, 'thuha', CONVERT(DATETIME2, '2026-05-01T08:20:00')),
            (N'Chúc mừng sinh nhật', N'PNJ chúc mừng sinh nhật {{HoTen}}', N'Kính chúc bạn một sinh nhật nhiều niềm vui. PNJ gửi tặng bạn ưu đãi trong tháng sinh nhật.', 1, 'admin', CONVERT(DATETIME2, '2026-05-01T08:30:00')),
            (N'Khuyến mãi khách hàng thân thiết', N'Ưu đãi dành riêng cho khách hàng thân thiết PNJ', N'Cảm ơn {{HoTen}} đã đồng hành cùng PNJ. Mời bạn khám phá chương trình ưu đãi mới.', 1, 'admin', CONVERT(DATETIME2, '2026-05-01T08:40:00')),
            (N'Mẫu chương trình cũ', N'Chương trình ưu đãi đã kết thúc', N'Nội dung mẫu lưu để đối chiếu lịch sử.', 0, 'admin', CONVERT(DATETIME2, '2026-04-01T08:00:00'))
    ) AS seed(TenMau, TieuDeMau, NoiDungMau, DangHoatDong, TenDangNhap, NgayCapNhat)
    JOIN dbo.TaiKhoan tk ON tk.TenDangNhap = seed.TenDangNhap;

    INSERT dbo.NhatKyGuiEmail
        (TaiKhoanId, KhachHangId, HoaDonId, MauEmailId, ThoiGianGui, EmailNhan, TieuDe, LoaiGui, TrangThai, GhiChu)
    SELECT tk.TaiKhoanId, kh.KhachHangId, hd.HoaDonId, me.MauEmailId,
           seed.ThoiGianGui, seed.EmailNhan, seed.TieuDe, seed.LoaiGui, seed.TrangThai, seed.GhiChu
    FROM
    (
        VALUES
            ('thuha', '0912000001', CONVERT(DATETIME2, '2026-05-12T10:30:00'), N'Xác nhận hóa đơn', CONVERT(DATETIME2, '2026-05-12T10:40:00'), 'thaovy@example.com', N'PNJ - Xác nhận hóa đơn HD000002', 'DON', 'THANH_CONG', NULL),
            ('thuha', '0912000002', CONVERT(DATETIME2, '2026-05-20T14:15:00'), N'Xác nhận hóa đơn', CONVERT(DATETIME2, '2026-05-20T14:30:00'), 'giahan@example.com', N'PNJ - Xác nhận hóa đơn HD000003', 'DON', 'THANH_CONG', NULL),
            ('thuha', '0912000003', CONVERT(DATETIME2, '2026-06-03T16:20:00'), N'Xác nhận hóa đơn', CONVERT(DATETIME2, '2026-06-03T16:35:00'), 'ducthanh@example.com', N'PNJ - Xác nhận hóa đơn HD000004', 'DON', 'THAT_BAI', N'Địa chỉ email từ chối nhận thư'),
            ('thuha', '0912000001', NULL, N'Chúc mừng sinh nhật', CONVERT(DATETIME2, '2026-06-05T08:00:00'), 'thaovy@example.com', N'PNJ chúc mừng sinh nhật Nguyễn Thảo Vy', 'DON', 'THANH_CONG', NULL),
            ('admin', '0912000001', NULL, N'Khuyến mãi khách hàng thân thiết', CONVERT(DATETIME2, '2026-06-15T08:30:00'), 'thaovy@example.com', N'Ưu đãi dành riêng cho khách hàng thân thiết PNJ', 'HANG_LOAT', 'THANH_CONG', NULL),
            ('admin', '0912000002', NULL, N'Khuyến mãi khách hàng thân thiết', CONVERT(DATETIME2, '2026-06-15T08:30:05'), 'giahan@example.com', N'Ưu đãi dành riêng cho khách hàng thân thiết PNJ', 'HANG_LOAT', 'THANH_CONG', NULL),
            ('admin', '0912000004', NULL, N'Khuyến mãi khách hàng thân thiết', CONVERT(DATETIME2, '2026-06-15T08:30:10'), 'khanhlinh@example.com', N'Ưu đãi dành riêng cho khách hàng thân thiết PNJ', 'HANG_LOAT', 'THAT_BAI', N'Lỗi kết nối SMTP thử nghiệm'),
            ('thuha', '0912000001', NULL, N'Thông báo hoàn thành bảo hành', CONVERT(DATETIME2, '2026-06-23T14:35:00'), 'thaovy@example.com', N'PNJ - Sản phẩm bảo hành đã hoàn thành', 'DON', 'THANH_CONG', NULL),
            ('thuha', '0912000002', NULL, N'Nhắc lịch bảo hành', CONVERT(DATETIME2, '2026-07-02T13:20:00'), 'giahan@example.com', N'PNJ - Thông tin bảo hành sản phẩm', 'DON', 'THANH_CONG', NULL),
            ('thuha', '0912000005', CONVERT(DATETIME2, '2026-07-01T13:40:00'), N'Xác nhận hóa đơn', CONVERT(DATETIME2, '2026-07-01T13:50:00'), 'hoangyen@example.com', N'PNJ - Xác nhận hóa đơn HD000006', 'DON', 'THANH_CONG', NULL),
            ('thuha', '0912000001', CONVERT(DATETIME2, '2026-07-20T15:05:00'), N'Xác nhận hóa đơn', CONVERT(DATETIME2, '2026-07-20T15:15:00'), 'thaovy@example.com', N'PNJ - Xác nhận hóa đơn HD000008', 'DON', 'THANH_CONG', NULL),
            ('admin', '0912000006', NULL, N'Khuyến mãi khách hàng thân thiết', CONVERT(DATETIME2, '2026-07-25T09:00:00'), 'quanghuy@example.com', N'Ưu đãi dành riêng cho khách hàng thân thiết PNJ', 'HANG_LOAT', 'THANH_CONG', NULL)
    ) AS seed(TenDangNhap, SoDienThoaiKhachHang, NgayLapHoaDon, TenMau, ThoiGianGui, EmailNhan, TieuDe, LoaiGui, TrangThai, GhiChu)
    JOIN dbo.TaiKhoan tk ON tk.TenDangNhap = seed.TenDangNhap
    JOIN dbo.KhachHang kh ON kh.SoDienThoai = seed.SoDienThoaiKhachHang
    JOIN dbo.MauEmail me ON me.TenMau = seed.TenMau
    LEFT JOIN dbo.HoaDon hd ON hd.NgayLap = seed.NgayLapHoaDon;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0
        ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

PRINT N'Đã thêm dữ liệu mẫu cho 17 bảng. Mật khẩu demo: PnjDemo@123';
GO
