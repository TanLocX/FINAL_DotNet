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

IF COL_LENGTH(N'dbo.NhanVien', N'NhanVienId') IS NULL
   OR OBJECT_ID(N'dbo.MauEmail', N'U') IS NULL
   OR OBJECT_ID(N'dbo.NhatKyGuiEmail', N'U') IS NULL
BEGIN
    THROW 50200, N'CSDL chưa dùng schema v2. Hãy chạy 04_MigrateLegacyToV2.sql trước.', 1;
END;

IF EXISTS (SELECT 1 FROM dbo.SanPham WHERE TenSanPham = N'Bông tai kim cương bạch kim')
BEGIN
    PRINT N'Dữ liệu mẫu sau migration đã tồn tại. Không chèn lại.';
    RETURN;
END;

BEGIN TRY
    BEGIN TRANSACTION;

    INSERT dbo.NhanVien
        (HoTen, GioiTinh, NgaySinh, SoDienThoai, Email, DiaChi, ChucVu, DangLamViec)
    SELECT seed.HoTen, seed.GioiTinh, seed.NgaySinh, seed.SoDienThoai,
           seed.Email, seed.DiaChi, seed.ChucVu, seed.DangLamViec
    FROM
    (
        VALUES
            (N'Võ Quốc Bảo', N'Nam', CONVERT(DATE, '1990-05-30'), '0901000005', 'quocbao@pnj-demo.local', N'Quận 7, TP.HCM', N'Nhân viên thu mua', 1),
            (N'Đặng Mỹ Linh', N'Nữ', CONVERT(DATE, '1998-09-14'), '0901000006', 'mylinh@pnj-demo.local', N'Quận Tân Bình, TP.HCM', N'Nhân viên bán hàng', 0)
    ) AS seed(HoTen, GioiTinh, NgaySinh, SoDienThoai, Email, DiaChi, ChucVu, DangLamViec)
    WHERE NOT EXISTS (SELECT 1 FROM dbo.NhanVien nv WHERE nv.SoDienThoai = seed.SoDienThoai);

    -- Mật khẩu demo cho các tài khoản bổ sung: PnjDemo@123
    INSERT dbo.TaiKhoan
        (NhanVienId, TenDangNhap, MatKhauHash, VaiTro, PhaiDoiMatKhau, DangHoatDong)
    SELECT nv.NhanVienId, seed.TenDangNhap, seed.MatKhauHash, 'NHANVIEN',
           seed.PhaiDoiMatKhau, seed.DangHoatDong
    FROM
    (
        VALUES
            ('0900000004', 'dangky',  '$2a$11$krSEwKvdugJ5k5KVsZaCiuc3jZ1amlgiJN4yDwW3Nxfw//y6f119m', 1, 1),
            ('0901000005', 'quocbao', '$2a$11$nNsSC4r3E9hI..uK03uyj.cRSEc4K0WpqifeF4E0R8rBv.Vh.bdpi', 0, 1),
            ('0901000006', 'mylinh',  '$2a$11$lle/VpG4YrTYx7KaDFlGpefzCpVxkVjtPEBZM3YxcRTkVNMIgPVFe', 0, 0)
    ) AS seed(SoDienThoaiNhanVien, TenDangNhap, MatKhauHash, PhaiDoiMatKhau, DangHoatDong)
    JOIN dbo.NhanVien nv ON nv.SoDienThoai = seed.SoDienThoaiNhanVien
    WHERE NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan tk WHERE tk.TenDangNhap = seed.TenDangNhap)
      AND NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan tk WHERE tk.NhanVienId = nv.NhanVienId);

    INSERT dbo.KhachHang
        (HoTen, SoDienThoai, Email, DiaChi, NgaySinh, ChoPhepNhanEmail, DiemTichLuy, DangHoatDong)
    SELECT seed.HoTen, seed.SoDienThoai, seed.Email, seed.DiaChi, seed.NgaySinh,
           seed.ChoPhepNhanEmail, seed.DiemTichLuy, seed.DangHoatDong
    FROM
    (
        VALUES
            (N'Khách lẻ', '0000000000', NULL, NULL, NULL, 0, 0, 1),
            (N'Nguyễn Thảo Vy', '0912000001', 'thaovy@example.com', N'Quận 5, TP.HCM', CONVERT(DATE, '1995-04-18'), 1, 1250, 1),
            (N'Trần Gia Hân', '0912000002', 'giahan@example.com', N'Quận 10, TP.HCM', CONVERT(DATE, '1991-10-02'), 1, 860, 1),
            (N'Lê Đức Thành', '0912000003', 'ducthanh@example.com', N'Thành phố Thủ Đức, TP.HCM', CONVERT(DATE, '1987-06-21'), 0, 430, 1),
            (N'Phạm Khánh Linh', '0912000004', 'khanhlinh@example.com', N'Quận 7, TP.HCM', CONVERT(DATE, '1998-02-11'), 1, 720, 1),
            (N'Vũ Hoàng Yến', '0912000005', 'hoangyen@example.com', N'Quận Phú Nhuận, TP.HCM', CONVERT(DATE, '1993-12-09'), 1, 510, 1),
            (N'Đỗ Quang Huy', '0912000006', 'quanghuy@example.com', N'Quận Bình Tân, TP.HCM', CONVERT(DATE, '1989-08-16'), 1, 300, 1),
            (N'Bùi Mai Chi', '0912000007', 'maichi@example.com', N'Quận Gò Vấp, TP.HCM', CONVERT(DATE, '1997-05-27'), 1, 150, 0)
    ) AS seed(HoTen, SoDienThoai, Email, DiaChi, NgaySinh, ChoPhepNhanEmail, DiemTichLuy, DangHoatDong)
    WHERE NOT EXISTS (SELECT 1 FROM dbo.KhachHang kh WHERE kh.SoDienThoai = seed.SoDienThoai);

    INSERT dbo.DanhMuc (TenDanhMuc, MoTa, DangHoatDong)
    SELECT seed.TenDanhMuc, seed.MoTa, 1
    FROM
    (
        VALUES
            (N'Nhẫn', N'Nhẫn cưới, nhẫn thời trang và nhẫn đá quý'),
            (N'Bông tai', N'Bông tai kim loại quý và đá quý'),
            (N'Dây chuyền', N'Dây chuyền vàng, bạc và bạch kim'),
            (N'Vòng tay', N'Vòng tay trang sức các loại'),
            (N'Lắc chân', N'Lắc chân vàng và bạc'),
            (N'Mặt dây chuyền', N'Mặt dây chuyền kim loại quý và đá quý')
    ) AS seed(TenDanhMuc, MoTa)
    WHERE NOT EXISTS (SELECT 1 FROM dbo.DanhMuc dm WHERE dm.TenDanhMuc = seed.TenDanhMuc);

    INSERT dbo.NhaCungCap
        (TenNhaCungCap, NguoiLienHe, SoDienThoai, Email, DiaChi, DangHoatDong)
    SELECT seed.TenNhaCungCap, seed.NguoiLienHe, seed.SoDienThoai,
           seed.Email, seed.DiaChi, seed.DangHoatDong
    FROM
    (
        VALUES
            (N'Công ty Vàng Sài Gòn', N'Nguyễn Văn Lâm', '0283800001', 'lienhe@vangsaigon.example', N'Quận 1, TP.HCM', 1),
            (N'Đá quý Á Châu', N'Trần Kim Ngân', '0283800002', 'sales@aquyachau.example', N'Quận 3, TP.HCM', 1),
            (N'Bạc Việt 925', N'Lê Thành Công', '0283800003', 'hello@bacviet.example', N'Quận 5, TP.HCM', 1),
            (N'Kim cương Hoàn Mỹ', N'Phạm Ngọc Diệp', '0283800004', 'contact@hoanmydiamond.example', N'Quận 7, TP.HCM', 1),
            (N'Bạch kim Đông Dương', N'Võ Minh Tú', '0283800005', 'info@bachkimdongduong.example', N'Thành phố Thủ Đức, TP.HCM', 1),
            (N'Ruby Việt Nam', N'Đặng Hồng Nhung', '0283800006', 'sales@rubyvietnam.example', N'Quận Tân Bình, TP.HCM', 1),
            (N'Sapphire Phương Nam', N'Bùi Thanh Sơn', '0283800007', 'hello@sapphirephuongnam.example', N'Quận Phú Nhuận, TP.HCM', 1),
            (N'Ngọc lục bảo Gia Bảo', N'Đỗ Mỹ Duyên', '0283800008', 'contact@giabaoemerald.example', N'Quận Bình Thạnh, TP.HCM', 0)
    ) AS seed(TenNhaCungCap, NguoiLienHe, SoDienThoai, Email, DiaChi, DangHoatDong)
    WHERE NOT EXISTS (SELECT 1 FROM dbo.NhaCungCap ncc WHERE ncc.SoDienThoai = seed.SoDienThoai);

    INSERT dbo.ChatLieu (TenChatLieu, GiaMuaVao, GiaBanRa, DangHoatDong)
    SELECT seed.TenChatLieu, seed.GiaMuaVao, seed.GiaBanRa, 1
    FROM
    (
        VALUES
            (N'Vàng 24K', 2050000.00, 2250000.00),
            (N'Vàng 18K', 1500000.00, 1750000.00),
            (N'Vàng 14K', 1100000.00, 1350000.00),
            (N'Bạc 925', 18000.00, 35000.00),
            (N'Bạch kim', 900000.00, 1250000.00),
            (N'Kim cương', 18000000.00, 25000000.00),
            (N'Ruby', 8500000.00, 12000000.00),
            (N'Sapphire', 7500000.00, 11000000.00),
            (N'Emerald', 9000000.00, 13000000.00)
    ) AS seed(TenChatLieu, GiaMuaVao, GiaBanRa)
    WHERE NOT EXISTS (SELECT 1 FROM dbo.ChatLieu cl WHERE cl.TenChatLieu = seed.TenChatLieu);

    INSERT dbo.SanPham
        (DanhMucId, TenSanPham, GiaVon, GiaBan, SoLuongTon, DuongDanAnh, DangKinhDoanh)
    SELECT dm.DanhMucId, seed.TenSanPham, seed.GiaVon, seed.GiaBan,
           seed.SoLuongTon, seed.DuongDanAnh, 1
    FROM
    (
        VALUES
            (N'Bông tai', N'Bông tai kim cương bạch kim', 35000000.00, 49000000.00, 4, N'Resources\bong_tai_kim_cuong_bach_kim.png'),
            (N'Bông tai', N'Bông tai ruby vàng 18K', 18000000.00, 25000000.00, 6, N'Resources\bong_tai_ruby_vang_18k.png'),
            (N'Dây chuyền', N'Dây chuyền bạc 925', 1200000.00, 2200000.00, 15, N'Resources\day_chuyen_bac_925.png'),
            (N'Dây chuyền', N'Dây chuyền sapphire bạch kim', 42000000.00, 59000000.00, 3, N'Resources\day_chuyen_sapphire_bach_kim.png'),
            (N'Lắc chân', N'Lắc chân vàng 14K', 5500000.00, 8500000.00, 8, N'Resources\lac_chan_vang_14k.png'),
            (N'Nhẫn', N'Nhẫn emerald vàng 18K', 22000000.00, 31500000.00, 5, N'Resources\nhan_emerald_vang_18k.png'),
            (N'Nhẫn', N'Nhẫn kim cương 18K', 30000000.00, 45000000.00, 4, N'Resources\nhan_kim_cuong_18k.png'),
            (N'Nhẫn', N'Nhẫn vàng 24K trơn', 10500000.00, 13000000.00, 10, N'Resources\nhan_vang_24k_tron.png'),
            (N'Vòng tay', N'Vòng tay bạc 925 trơn', 1400000.00, 2500000.00, 12, N'Resources\vong_tay_bac_925_tron.png'),
            (N'Vòng tay', N'Vòng tay vàng 24K', 25000000.00, 32500000.00, 6, N'Resources\vong_tay_vang_24k.png')
    ) AS seed(TenDanhMuc, TenSanPham, GiaVon, GiaBan, SoLuongTon, DuongDanAnh)
    JOIN dbo.DanhMuc dm ON dm.TenDanhMuc = seed.TenDanhMuc
    WHERE NOT EXISTS (SELECT 1 FROM dbo.SanPham sp WHERE sp.TenSanPham = seed.TenSanPham);

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
    JOIN dbo.ChatLieu cl ON cl.TenChatLieu = seed.TenChatLieu
    WHERE NOT EXISTS
    (
        SELECT 1 FROM dbo.ChiTietChatLieu ct
        WHERE ct.SanPhamId = sp.SanPhamId AND ct.ChatLieuId = cl.ChatLieuId
    );

    INSERT dbo.HoaDon
        (NhanVienId, KhachHangId, NgayLap, TongTien, GiamGia, ThanhTien, PhuongThucThanhToan, TrangThai)
    SELECT nv.NhanVienId, kh.KhachHangId, seed.NgayLap, 0, seed.GiamGia, 0,
           seed.PhuongThucThanhToan, seed.TrangThai
    FROM
    (
        VALUES
            ('0987654321', '0000000000', CONVERT(DATETIME2, '2026-05-05T09:00:00'), 0.00, N'Tiền mặt', 'DA_THANH_TOAN'),
            ('0987654321', '0912000001', CONVERT(DATETIME2, '2026-05-12T10:30:00'), 500000.00, N'Chuyển khoản', 'DA_THANH_TOAN'),
            ('0901234567', '0912000002', CONVERT(DATETIME2, '2026-05-20T14:15:00'), 1000000.00, N'Thẻ ngân hàng', 'DA_THANH_TOAN'),
            ('0900000004', '0912000003', CONVERT(DATETIME2, '2026-06-03T16:20:00'), 0.00, N'Tiền mặt', 'DA_THANH_TOAN'),
            ('0987654321', '0912000004', CONVERT(DATETIME2, '2026-06-15T11:10:00'), 300000.00, N'Chuyển khoản', 'DA_HUY'),
            ('0901234567', '0912000005', CONVERT(DATETIME2, '2026-07-01T13:40:00'), 1500000.00, N'Thẻ ngân hàng', 'DA_THANH_TOAN'),
            ('0900000004', '0912000006', CONVERT(DATETIME2, '2026-07-12T09:45:00'), 0.00, N'Tiền mặt', 'DA_HUY'),
            ('0987654321', '0912000001', CONVERT(DATETIME2, '2026-07-20T15:05:00'), 750000.00, N'Chuyển khoản', 'DA_THANH_TOAN')
    ) AS seed(SoDienThoaiNhanVien, SoDienThoaiKhachHang, NgayLap, GiamGia, PhuongThucThanhToan, TrangThai)
    JOIN dbo.NhanVien nv ON nv.SoDienThoai = seed.SoDienThoaiNhanVien
    JOIN dbo.KhachHang kh ON kh.SoDienThoai = seed.SoDienThoaiKhachHang;

    INSERT dbo.ChiTietHoaDon (HoaDonId, SanPhamId, SoLuong, DonGiaBan, HanBaoHanh)
    SELECT hd.HoaDonId, sp.SanPhamId, seed.SoLuong, seed.DonGiaBan, seed.HanBaoHanh
    FROM
    (
        VALUES
            ('2026-05-05T09:00:00', N'Dây chuyền bạc 925', 1, 2200000.00, CONVERT(DATE, '2027-05-05')),
            ('2026-05-05T09:00:00', N'Vòng tay bạc 925 trơn', 1, 2500000.00, CONVERT(DATE, '2027-05-05')),
            ('2026-05-12T10:30:00', N'Nhẫn vàng 24K trơn', 1, 13000000.00, CONVERT(DATE, '2027-05-12')),
            ('2026-05-12T10:30:00', N'Lắc chân vàng 14K', 1, 8500000.00, CONVERT(DATE, '2027-05-12')),
            ('2026-05-20T14:15:00', N'Nhẫn kim cương 18K', 1, 45000000.00, CONVERT(DATE, '2028-05-20')),
            ('2026-05-20T14:15:00', N'Bông tai kim cương bạch kim', 1, 49000000.00, CONVERT(DATE, '2028-05-20')),
            ('2026-06-03T16:20:00', N'Bông tai ruby vàng 18K', 1, 25000000.00, CONVERT(DATE, '2027-06-03')),
            ('2026-06-03T16:20:00', N'Dây chuyền bạc 925', 2, 2200000.00, CONVERT(DATE, '2027-06-03')),
            ('2026-06-15T11:10:00', N'Vòng tay bạc 925 trơn', 2, 2500000.00, CONVERT(DATE, '2027-06-15')),
            ('2026-06-15T11:10:00', N'Lắc chân vàng 14K', 1, 8500000.00, CONVERT(DATE, '2027-06-15')),
            ('2026-07-01T13:40:00', N'Dây chuyền sapphire bạch kim', 1, 59000000.00, CONVERT(DATE, '2028-07-01')),
            ('2026-07-01T13:40:00', N'Nhẫn emerald vàng 18K', 1, 31500000.00, CONVERT(DATE, '2028-07-01')),
            ('2026-07-12T09:45:00', N'Vòng tay vàng 24K', 1, 32500000.00, CONVERT(DATE, '2027-07-12')),
            ('2026-07-12T09:45:00', N'Nhẫn vàng 24K trơn', 1, 13000000.00, CONVERT(DATE, '2027-07-12')),
            ('2026-07-20T15:05:00', N'Bông tai ruby vàng 18K', 1, 25000000.00, CONVERT(DATE, '2027-07-20')),
            ('2026-07-20T15:05:00', N'Nhẫn emerald vàng 18K', 1, 31500000.00, CONVERT(DATE, '2028-07-20'))
    ) AS seed(NgayLap, TenSanPham, SoLuong, DonGiaBan, HanBaoHanh)
    JOIN dbo.HoaDon hd ON hd.NgayLap = CONVERT(DATETIME2, seed.NgayLap)
    JOIN dbo.SanPham sp ON sp.TenSanPham = seed.TenSanPham;

    UPDATE hd SET TongTien = totals.TongTien, ThanhTien = totals.TongTien - hd.GiamGia
    FROM dbo.HoaDon hd
    JOIN (SELECT HoaDonId, SUM(ThanhTien) TongTien FROM dbo.ChiTietHoaDon GROUP BY HoaDonId) totals
      ON totals.HoaDonId = hd.HoaDonId;

    INSERT dbo.PhieuNhap
        (NhanVienId, NhaCungCapId, NgayNhap, TongTienNhap, TrangThai, GhiChu)
    SELECT nv.NhanVienId, ncc.NhaCungCapId, seed.NgayNhap, 0, seed.TrangThai, seed.GhiChu
    FROM
    (
        VALUES
            ('0901234567', '0283800001', CONVERT(DATETIME2, '2026-04-02T08:30:00'), 'HOAN_THANH', N'Nhập vàng đầu tháng'),
            ('0987654321', '0283800002', CONVERT(DATETIME2, '2026-04-08T09:15:00'), 'HOAN_THANH', N'Nhập đá quý'),
            ('0901234567', '0283800003', CONVERT(DATETIME2, '2026-04-16T10:00:00'), 'HOAN_THANH', N'Bổ sung sản phẩm bạc'),
            ('0987654321', '0283800004', CONVERT(DATETIME2, '2026-05-02T13:20:00'), 'HOAN_THANH', N'Nhập sản phẩm kim cương'),
            ('0901234567', '0283800005', CONVERT(DATETIME2, '2026-05-18T14:45:00'), 'DA_HUY', N'Hủy do sai quy cách'),
            ('0987654321', '0283800006', CONVERT(DATETIME2, '2026-06-05T09:40:00'), 'HOAN_THANH', N'Nhập trang sức ruby'),
            ('0901234567', '0283800007', CONVERT(DATETIME2, '2026-06-22T11:25:00'), 'HOAN_THANH', N'Nhập trang sức sapphire'),
            ('0987654321', '0283800008', CONVERT(DATETIME2, '2026-07-06T15:30:00'), 'DA_HUY', N'Nhà cung cấp tạm ngừng hoạt động')
    ) AS seed(SoDienThoaiNhanVien, SoDienThoaiNCC, NgayNhap, TrangThai, GhiChu)
    JOIN dbo.NhanVien nv ON nv.SoDienThoai = seed.SoDienThoaiNhanVien
    JOIN dbo.NhaCungCap ncc ON ncc.SoDienThoai = seed.SoDienThoaiNCC;

    INSERT dbo.ChiTietPhieuNhap (PhieuNhapId, SanPhamId, SoLuong, DonGiaNhap)
    SELECT pn.PhieuNhapId, sp.SanPhamId, seed.SoLuong, seed.DonGiaNhap
    FROM
    (
        VALUES
            ('2026-04-02T08:30:00', N'Nhẫn vàng 24K trơn', 8, 10000000.00), ('2026-04-02T08:30:00', N'Vòng tay vàng 24K', 5, 24000000.00),
            ('2026-04-08T09:15:00', N'Bông tai ruby vàng 18K', 5, 17500000.00), ('2026-04-08T09:15:00', N'Nhẫn emerald vàng 18K', 4, 21500000.00),
            ('2026-04-16T10:00:00', N'Dây chuyền bạc 925', 12, 1150000.00), ('2026-04-16T10:00:00', N'Vòng tay bạc 925 trơn', 10, 1350000.00),
            ('2026-05-02T13:20:00', N'Nhẫn kim cương 18K', 3, 29000000.00), ('2026-05-02T13:20:00', N'Bông tai kim cương bạch kim', 3, 34000000.00),
            ('2026-05-18T14:45:00', N'Dây chuyền sapphire bạch kim', 2, 41000000.00), ('2026-05-18T14:45:00', N'Bông tai kim cương bạch kim', 1, 34500000.00),
            ('2026-06-05T09:40:00', N'Bông tai ruby vàng 18K', 4, 18000000.00), ('2026-06-05T09:40:00', N'Lắc chân vàng 14K', 6, 5300000.00),
            ('2026-06-22T11:25:00', N'Dây chuyền sapphire bạch kim', 3, 42000000.00), ('2026-06-22T11:25:00', N'Nhẫn emerald vàng 18K', 3, 22000000.00),
            ('2026-07-06T15:30:00', N'Nhẫn emerald vàng 18K', 2, 21800000.00), ('2026-07-06T15:30:00', N'Nhẫn kim cương 18K', 2, 29800000.00)
    ) AS seed(NgayNhap, TenSanPham, SoLuong, DonGiaNhap)
    JOIN dbo.PhieuNhap pn ON pn.NgayNhap = CONVERT(DATETIME2, seed.NgayNhap)
    JOIN dbo.SanPham sp ON sp.TenSanPham = seed.TenSanPham;

    UPDATE pn SET TongTienNhap = totals.TongTienNhap
    FROM dbo.PhieuNhap pn
    JOIN (SELECT PhieuNhapId, SUM(ThanhTien) TongTienNhap FROM dbo.ChiTietPhieuNhap GROUP BY PhieuNhapId) totals
      ON totals.PhieuNhapId = pn.PhieuNhapId;

    INSERT dbo.PhieuThuMua
        (NhanVienId, KhachHangId, NgayThuMua, TongTienThuMua, TrangThai, GhiChu)
    SELECT nv.NhanVienId, kh.KhachHangId, seed.NgayThuMua, 0, seed.TrangThai, seed.GhiChu
    FROM
    (
        VALUES
            ('0901000005', '0912000001', CONVERT(DATETIME2, '2026-05-07T09:20:00'), 'HOAN_THANH', N'Thu mua vàng cũ'),
            ('0901000005', '0912000002', CONVERT(DATETIME2, '2026-05-16T14:10:00'), 'HOAN_THANH', N'Thu mua trang sức bạc'),
            ('0901234567', '0912000003', CONVERT(DATETIME2, '2026-05-28T10:35:00'), 'HOAN_THANH', N'Thu mua nhẫn cũ'),
            ('0901000005', '0912000004', CONVERT(DATETIME2, '2026-06-09T15:25:00'), 'DA_HUY', N'Khách hàng đổi ý'),
            ('0901000005', '0912000005', CONVERT(DATETIME2, '2026-06-19T11:45:00'), 'HOAN_THANH', N'Thu mua vàng 18K'),
            ('0901234567', '0912000006', CONVERT(DATETIME2, '2026-07-03T13:15:00'), 'HOAN_THANH', N'Thu mua bạch kim'),
            ('0901000005', '0912000001', CONVERT(DATETIME2, '2026-07-14T09:50:00'), 'HOAN_THANH', N'Thu mua lắc chân'),
            ('0901000005', '0912000002', CONVERT(DATETIME2, '2026-07-25T16:05:00'), 'DA_HUY', N'Không đạt kiểm định')
    ) AS seed(SoDienThoaiNhanVien, SoDienThoaiKhachHang, NgayThuMua, TrangThai, GhiChu)
    JOIN dbo.NhanVien nv ON nv.SoDienThoai = seed.SoDienThoaiNhanVien
    JOIN dbo.KhachHang kh ON kh.SoDienThoai = seed.SoDienThoaiKhachHang;

    INSERT dbo.ChiTietPhieuThuMua
        (PhieuThuMuaId, ChatLieuId, SanPhamId, TenSanPhamThu, TrongLuong, DonViTinh, DonGiaThuMua)
    SELECT ptm.PhieuThuMuaId, cl.ChatLieuId, sp.SanPhamId, seed.TenSanPhamThu,
           seed.TrongLuong, seed.DonViTinh, seed.DonGiaThuMua
    FROM
    (
        VALUES
            ('2026-05-07T09:20:00', N'Vàng 24K', N'Nhẫn vàng 24K trơn', N'Nhẫn vàng 24K cũ', 6.200, N'gram', 2000000.00),
            ('2026-05-07T09:20:00', N'Vàng 18K', NULL, N'Dây chuyền vàng 18K cũ', 9.500, N'gram', 1450000.00),
            ('2026-05-16T14:10:00', N'Bạc 925', N'Vòng tay bạc 925 trơn', N'Vòng tay bạc cũ', 21.000, N'gram', 17000.00),
            ('2026-05-28T10:35:00', N'Vàng 18K', N'Nhẫn kim cương 18K', N'Nhẫn vàng 18K cũ', 4.100, N'gram', 1460000.00),
            ('2026-05-28T10:35:00', N'Kim cương', NULL, N'Viên kim cương tháo rời', 0.450, N'carat', 17500000.00),
            ('2026-06-09T15:25:00', N'Ruby', NULL, N'Viên ruby cũ', 0.900, N'carat', 8000000.00),
            ('2026-06-19T11:45:00', N'Vàng 18K', N'Bông tai ruby vàng 18K', N'Đôi bông tai vàng 18K cũ', 5.000, N'gram', 1480000.00),
            ('2026-07-03T13:15:00', N'Bạch kim', NULL, N'Nhẫn bạch kim cũ', 7.200, N'gram', 880000.00),
            ('2026-07-14T09:50:00', N'Vàng 14K', N'Lắc chân vàng 14K', N'Lắc chân vàng 14K cũ', 7.600, N'gram', 1050000.00),
            ('2026-07-25T16:05:00', N'Sapphire', NULL, N'Viên sapphire chưa kiểm định', 1.100, N'carat', 7000000.00)
    ) AS seed(NgayThuMua, TenChatLieu, TenSanPham, TenSanPhamThu, TrongLuong, DonViTinh, DonGiaThuMua)
    JOIN dbo.PhieuThuMua ptm ON ptm.NgayThuMua = CONVERT(DATETIME2, seed.NgayThuMua)
    JOIN dbo.ChatLieu cl ON cl.TenChatLieu = seed.TenChatLieu
    LEFT JOIN dbo.SanPham sp ON sp.TenSanPham = seed.TenSanPham;

    UPDATE ptm SET TongTienThuMua = totals.TongTienThuMua
    FROM dbo.PhieuThuMua ptm
    JOIN (SELECT PhieuThuMuaId, SUM(ThanhTien) TongTienThuMua FROM dbo.ChiTietPhieuThuMua GROUP BY PhieuThuMuaId) totals
      ON totals.PhieuThuMuaId = ptm.PhieuThuMuaId;

    INSERT dbo.PhieuBaoHanh
        (ChiTietHoaDonId, NgayTiepNhan, NoiDungBaoHanh, TrangThai, NgayTraDuKien, NgayTraThucTe, GhiChu)
    SELECT cthd.ChiTietHoaDonId, seed.NgayTiepNhan, seed.NoiDungBaoHanh,
           seed.TrangThai, seed.NgayTraDuKien, seed.NgayTraThucTe, seed.GhiChu
    FROM
    (
        VALUES
            ('2026-05-05T09:00:00', N'Dây chuyền bạc 925', CONVERT(DATETIME2, '2026-06-10T09:00:00'), N'Làm sạch và đánh bóng', 'DA_TRA', CONVERT(DATE, '2026-06-12'), CONVERT(DATETIME2, '2026-06-12T16:00:00'), N'Khách đã nhận sản phẩm'),
            ('2026-05-12T10:30:00', N'Nhẫn vàng 24K trơn', CONVERT(DATETIME2, '2026-06-20T10:20:00'), N'Chỉnh lại kích thước nhẫn', 'HOAN_THANH', CONVERT(DATE, '2026-06-24'), CONVERT(DATETIME2, '2026-06-23T14:30:00'), N'Đã liên hệ khách'),
            ('2026-05-20T14:15:00', N'Nhẫn kim cương 18K', CONVERT(DATETIME2, '2026-07-02T13:10:00'), N'Kiểm tra và siết chấu kim cương', 'DANG_XU_LY', CONVERT(DATE, '2026-07-08'), NULL, N'Đang xử lý tại xưởng'),
            ('2026-06-03T16:20:00', N'Bông tai ruby vàng 18K', CONVERT(DATETIME2, '2026-07-10T15:00:00'), N'Thay khóa bông tai', 'TIEP_NHAN', CONVERT(DATE, '2026-07-15'), NULL, N'Đã nhận đủ đôi'),
            ('2026-07-01T13:40:00', N'Dây chuyền sapphire bạch kim', CONVERT(DATETIME2, '2026-07-18T11:30:00'), N'Kiểm tra móc khóa dây chuyền', 'DA_TRA', CONVERT(DATE, '2026-07-22'), CONVERT(DATETIME2, '2026-07-21T17:10:00'), N'Hoàn thành sớm'),
            ('2026-07-20T15:05:00', N'Nhẫn emerald vàng 18K', CONVERT(DATETIME2, '2026-08-02T09:40:00'), N'Đánh bóng và kiểm tra viên chủ', 'HOAN_THANH', CONVERT(DATE, '2026-08-07'), CONVERT(DATETIME2, '2026-08-06T15:20:00'), N'Chờ khách đến nhận')
    ) AS seed(NgayLapHoaDon, TenSanPham, NgayTiepNhan, NoiDungBaoHanh, TrangThai, NgayTraDuKien, NgayTraThucTe, GhiChu)
    JOIN dbo.HoaDon hd ON hd.NgayLap = CONVERT(DATETIME2, seed.NgayLapHoaDon) AND hd.TrangThai = 'DA_THANH_TOAN'
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
            (N'Nhắc lịch bảo hành', N'PNJ - Thông tin bảo hành sản phẩm', N'Xin chào {{HoTen}}, sản phẩm {{TenSanPham}} của bạn có hạn bảo hành đến {{HanBaoHanh}}.', 1, 'dangky', CONVERT(DATETIME2, '2026-05-01T08:10:00')),
            (N'Thông báo hoàn thành bảo hành', N'PNJ - Sản phẩm bảo hành đã hoàn thành', N'Sản phẩm {{TenSanPham}} đã hoàn thành bảo hành. Mời bạn đến cửa hàng nhận sản phẩm.', 1, 'dangky', CONVERT(DATETIME2, '2026-05-01T08:20:00')),
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
            ('dangky', '0912000001', '2026-05-12T10:30:00', N'Xác nhận hóa đơn', '2026-05-12T10:40:00', 'thaovy@example.com', N'PNJ - Xác nhận hóa đơn HD000002', 'DON', 'THANH_CONG', NULL),
            ('dangky', '0912000002', '2026-05-20T14:15:00', N'Xác nhận hóa đơn', '2026-05-20T14:30:00', 'giahan@example.com', N'PNJ - Xác nhận hóa đơn HD000003', 'DON', 'THANH_CONG', NULL),
            ('dangky', '0912000003', '2026-06-03T16:20:00', N'Xác nhận hóa đơn', '2026-06-03T16:35:00', 'ducthanh@example.com', N'PNJ - Xác nhận hóa đơn HD000004', 'DON', 'THAT_BAI', N'Địa chỉ email từ chối nhận thư'),
            ('dangky', '0912000001', NULL, N'Chúc mừng sinh nhật', '2026-06-05T08:00:00', 'thaovy@example.com', N'PNJ chúc mừng sinh nhật Nguyễn Thảo Vy', 'DON', 'THANH_CONG', NULL),
            ('admin', '0912000001', NULL, N'Khuyến mãi khách hàng thân thiết', '2026-06-15T08:30:00', 'thaovy@example.com', N'Ưu đãi dành riêng cho khách hàng thân thiết PNJ', 'HANG_LOAT', 'THANH_CONG', NULL),
            ('admin', '0912000002', NULL, N'Khuyến mãi khách hàng thân thiết', '2026-06-15T08:30:05', 'giahan@example.com', N'Ưu đãi dành riêng cho khách hàng thân thiết PNJ', 'HANG_LOAT', 'THANH_CONG', NULL),
            ('admin', '0912000004', NULL, N'Khuyến mãi khách hàng thân thiết', '2026-06-15T08:30:10', 'khanhlinh@example.com', N'Ưu đãi dành riêng cho khách hàng thân thiết PNJ', 'HANG_LOAT', 'THAT_BAI', N'Lỗi kết nối SMTP thử nghiệm'),
            ('dangky', '0912000001', NULL, N'Thông báo hoàn thành bảo hành', '2026-06-23T14:35:00', 'thaovy@example.com', N'PNJ - Sản phẩm bảo hành đã hoàn thành', 'DON', 'THANH_CONG', NULL),
            ('dangky', '0912000002', NULL, N'Nhắc lịch bảo hành', '2026-07-02T13:20:00', 'giahan@example.com', N'PNJ - Thông tin bảo hành sản phẩm', 'DON', 'THANH_CONG', NULL),
            ('dangky', '0912000005', '2026-07-01T13:40:00', N'Xác nhận hóa đơn', '2026-07-01T13:50:00', 'hoangyen@example.com', N'PNJ - Xác nhận hóa đơn HD000006', 'DON', 'THANH_CONG', NULL),
            ('dangky', '0912000001', '2026-07-20T15:05:00', N'Xác nhận hóa đơn', '2026-07-20T15:15:00', 'thaovy@example.com', N'PNJ - Xác nhận hóa đơn HD000008', 'DON', 'THANH_CONG', NULL),
            ('admin', '0912000006', NULL, N'Khuyến mãi khách hàng thân thiết', '2026-07-25T09:00:00', 'quanghuy@example.com', N'Ưu đãi dành riêng cho khách hàng thân thiết PNJ', 'HANG_LOAT', 'THANH_CONG', NULL)
    ) AS seed(TenDangNhap, SoDienThoaiKhachHang, NgayLapHoaDon, TenMau, ThoiGianGui, EmailNhan, TieuDe, LoaiGui, TrangThai, GhiChu)
    JOIN dbo.TaiKhoan tk ON tk.TenDangNhap = seed.TenDangNhap
    JOIN dbo.KhachHang kh ON kh.SoDienThoai = seed.SoDienThoaiKhachHang
    JOIN dbo.MauEmail me ON me.TenMau = seed.TenMau
    LEFT JOIN dbo.HoaDon hd ON hd.NgayLap = TRY_CONVERT(DATETIME2, seed.NgayLapHoaDon);

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0
        ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

PRINT N'Đã bổ sung dữ liệu mẫu sau migration, đồng thời giữ nguyên dữ liệu legacy.';
GO
