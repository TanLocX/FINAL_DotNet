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

IF COL_LENGTH(N'dbo.NhanVien', N'NhanVienId') IS NOT NULL
BEGIN
    PRINT N'CSDL đã dùng schema v2. Không cần chạy migration lần nữa.';
    RETURN;
END;

IF COL_LENGTH(N'dbo.NhanVien', N'MaNhanVien') IS NULL
   OR COL_LENGTH(N'dbo.TaiKhoan', N'MatKhau') IS NULL
   OR COL_LENGTH(N'dbo.SanPham', N'MaSanPham') IS NULL
BEGIN
    THROW 50100, N'Schema hiện tại không phải schema legacy dự kiến. Migration đã dừng.', 1;
END;

IF OBJECT_ID(N'dbo.NhanVien_V2', N'U') IS NOT NULL
BEGIN
    THROW 50101, N'Phát hiện bảng migration V2 còn sót lại. Cần kiểm tra thủ công trước khi chạy.', 1;
END;

IF EXISTS
(
    SELECT SoDienThoai
    FROM dbo.NhanVien
    WHERE NULLIF(LTRIM(RTRIM(SoDienThoai)), '') IS NOT NULL
    GROUP BY SoDienThoai
    HAVING COUNT(*) > 1
)
    THROW 50102, N'Dữ liệu nhân viên có số điện thoại trùng.', 1;

IF EXISTS
(
    SELECT MaNhanVien
    FROM dbo.TaiKhoan
    GROUP BY MaNhanVien
    HAVING COUNT(*) > 1
)
    THROW 50103, N'Một nhân viên legacy đang có nhiều tài khoản.', 1;

BEGIN TRY
    BEGIN TRANSACTION;

    CREATE TABLE dbo.NhanVien_V2
    (
        NhanVienId      INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_NhanVien PRIMARY KEY,
        HoTen           NVARCHAR(150) NOT NULL,
        GioiTinh        NVARCHAR(10) NULL,
        NgaySinh        DATE NULL,
        SoDienThoai     VARCHAR(15) NULL,
        Email           VARCHAR(254) NULL,
        DiaChi          NVARCHAR(255) NULL,
        ChucVu          NVARCHAR(50) NOT NULL,
        DangLamViec     BIT NOT NULL CONSTRAINT DF_NhanVien_DangLamViec DEFAULT 1,
        CONSTRAINT CK_NhanVien_NgaySinh CHECK (NgaySinh IS NULL OR NgaySinh <= CONVERT(DATE, GETDATE()))
    );

    CREATE UNIQUE INDEX UX_NhanVien_SoDienThoai
        ON dbo.NhanVien_V2(SoDienThoai)
        WHERE SoDienThoai IS NOT NULL;

    CREATE TABLE dbo.TaiKhoan_V2
    (
        TaiKhoanId       INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_TaiKhoan PRIMARY KEY,
        NhanVienId       INT NOT NULL,
        TenDangNhap      VARCHAR(50) NOT NULL,
        MatKhauHash      VARCHAR(255) NOT NULL,
        VaiTro           VARCHAR(20) NOT NULL,
        PhaiDoiMatKhau   BIT NOT NULL CONSTRAINT DF_TaiKhoan_PhaiDoi DEFAULT 0,
        DangHoatDong     BIT NOT NULL CONSTRAINT DF_TaiKhoan_HoatDong DEFAULT 1,
        CONSTRAINT UQ_TaiKhoan_NhanVien UNIQUE (NhanVienId),
        CONSTRAINT UQ_TaiKhoan_TenDangNhap UNIQUE (TenDangNhap),
        CONSTRAINT FK_TaiKhoan_NhanVien FOREIGN KEY (NhanVienId)
            REFERENCES dbo.NhanVien_V2(NhanVienId),
        CONSTRAINT CK_TaiKhoan_VaiTro CHECK (VaiTro IN ('ADMIN', 'NHANVIEN'))
    );

    CREATE TABLE dbo.KhachHang_V2
    (
        KhachHangId          INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_KhachHang PRIMARY KEY,
        HoTen                NVARCHAR(150) NOT NULL,
        SoDienThoai          VARCHAR(15) NOT NULL,
        Email                VARCHAR(254) NULL,
        DiaChi               NVARCHAR(255) NULL,
        NgaySinh             DATE NULL,
        ChoPhepNhanEmail     BIT NOT NULL CONSTRAINT DF_KhachHang_Email DEFAULT 0,
        DiemTichLuy          INT NOT NULL CONSTRAINT DF_KhachHang_Diem DEFAULT 0,
        DangHoatDong         BIT NOT NULL CONSTRAINT DF_KhachHang_HoatDong DEFAULT 1,
        CONSTRAINT UQ_KhachHang_SoDienThoai UNIQUE (SoDienThoai),
        CONSTRAINT CK_KhachHang_Diem CHECK (DiemTichLuy >= 0),
        CONSTRAINT CK_KhachHang_NgaySinh CHECK (NgaySinh IS NULL OR NgaySinh <= CONVERT(DATE, GETDATE()))
    );

    CREATE TABLE dbo.DanhMuc_V2
    (
        DanhMucId        INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_DanhMuc PRIMARY KEY,
        TenDanhMuc       NVARCHAR(100) NOT NULL,
        MoTa             NVARCHAR(255) NULL,
        DangHoatDong     BIT NOT NULL CONSTRAINT DF_DanhMuc_HoatDong DEFAULT 1,
        CONSTRAINT UQ_DanhMuc_Ten UNIQUE (TenDanhMuc)
    );

    CREATE TABLE dbo.NhaCungCap_V2
    (
        NhaCungCapId     INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_NhaCungCap PRIMARY KEY,
        TenNhaCungCap    NVARCHAR(150) NOT NULL,
        NguoiLienHe      NVARCHAR(100) NULL,
        SoDienThoai      VARCHAR(15) NOT NULL,
        Email            VARCHAR(254) NULL,
        DiaChi           NVARCHAR(255) NULL,
        DangHoatDong     BIT NOT NULL CONSTRAINT DF_NCC_HoatDong DEFAULT 1,
        CONSTRAINT UQ_NCC_Ten UNIQUE (TenNhaCungCap),
        CONSTRAINT UQ_NCC_SoDienThoai UNIQUE (SoDienThoai)
    );

    CREATE TABLE dbo.ChatLieu_V2
    (
        ChatLieuId       INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_ChatLieu PRIMARY KEY,
        TenChatLieu      NVARCHAR(100) NOT NULL,
        GiaMuaVao        DECIMAL(18,2) NOT NULL CONSTRAINT DF_ChatLieu_GiaMua DEFAULT 0,
        GiaBanRa         DECIMAL(18,2) NOT NULL CONSTRAINT DF_ChatLieu_GiaBan DEFAULT 0,
        DangHoatDong     BIT NOT NULL CONSTRAINT DF_ChatLieu_HoatDong DEFAULT 1,
        CONSTRAINT UQ_ChatLieu_Ten UNIQUE (TenChatLieu),
        CONSTRAINT CK_ChatLieu_GiaMua CHECK (GiaMuaVao >= 0),
        CONSTRAINT CK_ChatLieu_GiaBan CHECK (GiaBanRa >= 0)
    );

    CREATE TABLE dbo.SanPham_V2
    (
        SanPhamId        INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_SanPham PRIMARY KEY,
        DanhMucId        INT NOT NULL,
        TenSanPham       NVARCHAR(150) NOT NULL,
        GiaVon           DECIMAL(18,2) NOT NULL CONSTRAINT DF_SanPham_GiaVon DEFAULT 0,
        GiaBan           DECIMAL(18,2) NOT NULL CONSTRAINT DF_SanPham_GiaBan DEFAULT 0,
        SoLuongTon       INT NOT NULL CONSTRAINT DF_SanPham_Ton DEFAULT 0,
        DuongDanAnh      NVARCHAR(500) NULL,
        DangKinhDoanh    BIT NOT NULL CONSTRAINT DF_SanPham_KinhDoanh DEFAULT 1,
        CONSTRAINT FK_SanPham_DanhMuc FOREIGN KEY (DanhMucId)
            REFERENCES dbo.DanhMuc_V2(DanhMucId),
        CONSTRAINT CK_SanPham_GiaVon CHECK (GiaVon >= 0),
        CONSTRAINT CK_SanPham_GiaBan CHECK (GiaBan >= 0),
        CONSTRAINT CK_SanPham_Ton CHECK (SoLuongTon >= 0)
    );

    CREATE TABLE dbo.ChiTietChatLieu_V2
    (
        SanPhamId        INT NOT NULL,
        ChatLieuId       INT NOT NULL,
        TrongLuong       DECIMAL(10,3) NOT NULL,
        DonViTinh        NVARCHAR(20) NOT NULL,
        CONSTRAINT PK_ChiTietChatLieu PRIMARY KEY (SanPhamId, ChatLieuId),
        CONSTRAINT FK_CTChatLieu_SanPham FOREIGN KEY (SanPhamId)
            REFERENCES dbo.SanPham_V2(SanPhamId),
        CONSTRAINT FK_CTChatLieu_ChatLieu FOREIGN KEY (ChatLieuId)
            REFERENCES dbo.ChatLieu_V2(ChatLieuId),
        CONSTRAINT CK_CTChatLieu_TrongLuong CHECK (TrongLuong > 0)
    );

    CREATE TABLE dbo.HoaDon_V2
    (
        HoaDonId              INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_HoaDon PRIMARY KEY,
        NhanVienId            INT NOT NULL,
        KhachHangId           INT NOT NULL,
        NgayLap               DATETIME2 NOT NULL CONSTRAINT DF_HoaDon_NgayLap DEFAULT SYSDATETIME(),
        TongTien              DECIMAL(18,2) NOT NULL CONSTRAINT DF_HoaDon_Tong DEFAULT 0,
        GiamGia               DECIMAL(18,2) NOT NULL CONSTRAINT DF_HoaDon_GiamGia DEFAULT 0,
        ThanhTien             DECIMAL(18,2) NOT NULL CONSTRAINT DF_HoaDon_ThanhTien DEFAULT 0,
        PhuongThucThanhToan   NVARCHAR(50) NOT NULL,
        TrangThai             VARCHAR(20) NOT NULL CONSTRAINT DF_HoaDon_TrangThai DEFAULT 'DA_THANH_TOAN',
        CONSTRAINT FK_HoaDon_NhanVien FOREIGN KEY (NhanVienId)
            REFERENCES dbo.NhanVien_V2(NhanVienId),
        CONSTRAINT FK_HoaDon_KhachHang FOREIGN KEY (KhachHangId)
            REFERENCES dbo.KhachHang_V2(KhachHangId),
        CONSTRAINT CK_HoaDon_Tong CHECK (TongTien >= 0),
        CONSTRAINT CK_HoaDon_GiamGia CHECK (GiamGia >= 0),
        CONSTRAINT CK_HoaDon_ThanhTien CHECK (ThanhTien >= 0),
        CONSTRAINT CK_HoaDon_TrangThai CHECK (TrangThai IN ('DA_THANH_TOAN', 'DA_HUY'))
    );

    CREATE TABLE dbo.ChiTietHoaDon_V2
    (
        ChiTietHoaDonId   INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_ChiTietHoaDon PRIMARY KEY,
        HoaDonId          INT NOT NULL,
        SanPhamId         INT NOT NULL,
        SoLuong           INT NOT NULL,
        DonGiaBan         DECIMAL(18,2) NOT NULL,
        ThanhTien AS (CONVERT(DECIMAL(18,2), SoLuong * DonGiaBan)) PERSISTED,
        HanBaoHanh        DATE NULL,
        CONSTRAINT UQ_CTHoaDon_HoaDon_SanPham UNIQUE (HoaDonId, SanPhamId),
        CONSTRAINT FK_CTHoaDon_HoaDon FOREIGN KEY (HoaDonId)
            REFERENCES dbo.HoaDon_V2(HoaDonId),
        CONSTRAINT FK_CTHoaDon_SanPham FOREIGN KEY (SanPhamId)
            REFERENCES dbo.SanPham_V2(SanPhamId),
        CONSTRAINT CK_CTHoaDon_SoLuong CHECK (SoLuong > 0),
        CONSTRAINT CK_CTHoaDon_DonGia CHECK (DonGiaBan >= 0)
    );

    CREATE TABLE dbo.PhieuNhap_V2
    (
        PhieuNhapId       INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhieuNhap PRIMARY KEY,
        NhanVienId        INT NOT NULL,
        NhaCungCapId      INT NOT NULL,
        NgayNhap          DATETIME2 NOT NULL CONSTRAINT DF_PhieuNhap_Ngay DEFAULT SYSDATETIME(),
        TongTienNhap      DECIMAL(18,2) NOT NULL CONSTRAINT DF_PhieuNhap_Tong DEFAULT 0,
        TrangThai         VARCHAR(20) NOT NULL CONSTRAINT DF_PhieuNhap_TrangThai DEFAULT 'HOAN_THANH',
        GhiChu            NVARCHAR(500) NULL,
        CONSTRAINT FK_PhieuNhap_NhanVien FOREIGN KEY (NhanVienId)
            REFERENCES dbo.NhanVien_V2(NhanVienId),
        CONSTRAINT FK_PhieuNhap_NCC FOREIGN KEY (NhaCungCapId)
            REFERENCES dbo.NhaCungCap_V2(NhaCungCapId),
        CONSTRAINT CK_PhieuNhap_Tong CHECK (TongTienNhap >= 0),
        CONSTRAINT CK_PhieuNhap_TrangThai CHECK (TrangThai IN ('HOAN_THANH', 'DA_HUY'))
    );

    CREATE TABLE dbo.ChiTietPhieuNhap_V2
    (
        ChiTietPhieuNhapId    INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_ChiTietPhieuNhap PRIMARY KEY,
        PhieuNhapId           INT NOT NULL,
        SanPhamId             INT NOT NULL,
        SoLuong               INT NOT NULL,
        DonGiaNhap            DECIMAL(18,2) NOT NULL,
        ThanhTien AS (CONVERT(DECIMAL(18,2), SoLuong * DonGiaNhap)) PERSISTED,
        CONSTRAINT UQ_CTPhieuNhap_Phieu_SanPham UNIQUE (PhieuNhapId, SanPhamId),
        CONSTRAINT FK_CTPhieuNhap_PhieuNhap FOREIGN KEY (PhieuNhapId)
            REFERENCES dbo.PhieuNhap_V2(PhieuNhapId),
        CONSTRAINT FK_CTPhieuNhap_SanPham FOREIGN KEY (SanPhamId)
            REFERENCES dbo.SanPham_V2(SanPhamId),
        CONSTRAINT CK_CTPhieuNhap_SoLuong CHECK (SoLuong > 0),
        CONSTRAINT CK_CTPhieuNhap_DonGia CHECK (DonGiaNhap >= 0)
    );

    CREATE TABLE dbo.PhieuThuMua_V2
    (
        PhieuThuMuaId      INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhieuThuMua PRIMARY KEY,
        MaPhieuNguon       NVARCHAR(50) NULL,
        NhanVienId         INT NOT NULL,
        KhachHangId        INT NOT NULL,
        NgayThuMua         DATETIME2 NOT NULL CONSTRAINT DF_PhieuThuMua_Ngay DEFAULT SYSDATETIME(),
        TongTienThuMua     DECIMAL(18,2) NOT NULL CONSTRAINT DF_PhieuThuMua_Tong DEFAULT 0,
        TrangThai          VARCHAR(20) NOT NULL CONSTRAINT DF_PhieuThuMua_TrangThai DEFAULT 'HOAN_THANH',
        GhiChu             NVARCHAR(500) NULL,
        CONSTRAINT FK_PhieuThuMua_NhanVien FOREIGN KEY (NhanVienId)
            REFERENCES dbo.NhanVien_V2(NhanVienId),
        CONSTRAINT FK_PhieuThuMua_KhachHang FOREIGN KEY (KhachHangId)
            REFERENCES dbo.KhachHang_V2(KhachHangId),
        CONSTRAINT CK_PhieuThuMua_Tong CHECK (TongTienThuMua >= 0),
        CONSTRAINT CK_PhieuThuMua_TrangThai CHECK (TrangThai IN ('HOAN_THANH', 'DA_HUY'))
    );

    CREATE TABLE dbo.ChiTietPhieuThuMua_V2
    (
        ChiTietPhieuThuMuaId  INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_ChiTietPhieuThuMua PRIMARY KEY,
        PhieuThuMuaId         INT NOT NULL,
        ChatLieuId            INT NOT NULL,
        SanPhamId             INT NULL,
        TenSanPhamThu         NVARCHAR(150) NOT NULL,
        TrongLuong            DECIMAL(10,3) NOT NULL,
        DonViTinh             NVARCHAR(20) NOT NULL,
        DonGiaThuMua          DECIMAL(18,2) NOT NULL,
        ThanhTien AS (CONVERT(DECIMAL(18,2), TrongLuong * DonGiaThuMua)) PERSISTED,
        CONSTRAINT FK_CTThuMua_Phieu FOREIGN KEY (PhieuThuMuaId)
            REFERENCES dbo.PhieuThuMua_V2(PhieuThuMuaId),
        CONSTRAINT FK_CTThuMua_ChatLieu FOREIGN KEY (ChatLieuId)
            REFERENCES dbo.ChatLieu_V2(ChatLieuId),
        CONSTRAINT FK_CTThuMua_SanPham FOREIGN KEY (SanPhamId)
            REFERENCES dbo.SanPham_V2(SanPhamId),
        CONSTRAINT CK_CTThuMua_TrongLuong CHECK (TrongLuong > 0),
        CONSTRAINT CK_CTThuMua_DonGia CHECK (DonGiaThuMua >= 0)
    );

    CREATE TABLE dbo.PhieuBaoHanh_V2
    (
        PhieuBaoHanhId    INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhieuBaoHanh PRIMARY KEY,
        ChiTietHoaDonId   INT NOT NULL,
        NgayTiepNhan      DATETIME2 NOT NULL CONSTRAINT DF_BaoHanh_Ngay DEFAULT SYSDATETIME(),
        NoiDungBaoHanh    NVARCHAR(500) NOT NULL,
        TrangThai         VARCHAR(20) NOT NULL CONSTRAINT DF_BaoHanh_TrangThai DEFAULT 'TIEP_NHAN',
        NgayTraDuKien     DATE NULL,
        NgayTraThucTe     DATETIME2 NULL,
        GhiChu            NVARCHAR(500) NULL,
        CONSTRAINT FK_BaoHanh_CTHoaDon FOREIGN KEY (ChiTietHoaDonId)
            REFERENCES dbo.ChiTietHoaDon_V2(ChiTietHoaDonId),
        CONSTRAINT CK_BaoHanh_TrangThai
            CHECK (TrangThai IN ('TIEP_NHAN', 'DANG_XU_LY', 'HOAN_THANH', 'DA_TRA')),
        CONSTRAINT CK_BaoHanh_NgayTraDuKien
            CHECK (NgayTraDuKien IS NULL OR NgayTraDuKien >= CONVERT(DATE, NgayTiepNhan)),
        CONSTRAINT CK_BaoHanh_NgayTraThucTe
            CHECK (NgayTraThucTe IS NULL OR NgayTraThucTe >= NgayTiepNhan)
    );

    CREATE TABLE dbo.MauEmail_V2
    (
        MauEmailId          INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_MauEmail PRIMARY KEY,
        TenMau              NVARCHAR(100) NOT NULL,
        TieuDeMau           NVARCHAR(255) NOT NULL,
        NoiDungMau          NVARCHAR(MAX) NOT NULL,
        DangHoatDong        BIT NOT NULL CONSTRAINT DF_MauEmail_HoatDong DEFAULT 1,
        TaiKhoanCapNhatId   INT NULL,
        NgayCapNhat         DATETIME2 NOT NULL CONSTRAINT DF_MauEmail_Ngay DEFAULT SYSDATETIME(),
        CONSTRAINT UQ_MauEmail_Ten UNIQUE (TenMau),
        CONSTRAINT FK_MauEmail_TaiKhoan FOREIGN KEY (TaiKhoanCapNhatId)
            REFERENCES dbo.TaiKhoan_V2(TaiKhoanId)
    );

    CREATE TABLE dbo.NhatKyGuiEmail_V2
    (
        NhatKyGuiEmailId    BIGINT IDENTITY(1,1) NOT NULL CONSTRAINT PK_NhatKyGuiEmail PRIMARY KEY,
        TaiKhoanId          INT NOT NULL,
        KhachHangId         INT NULL,
        HoaDonId            INT NULL,
        MauEmailId          INT NULL,
        ThoiGianGui         DATETIME2 NOT NULL CONSTRAINT DF_NhatKyEmail_ThoiGian DEFAULT SYSDATETIME(),
        EmailNhan           VARCHAR(254) NOT NULL,
        TieuDe              NVARCHAR(255) NOT NULL,
        LoaiGui             VARCHAR(20) NOT NULL,
        TrangThai           VARCHAR(20) NOT NULL,
        GhiChu              NVARCHAR(1000) NULL,
        CONSTRAINT FK_NhatKyEmail_TaiKhoan FOREIGN KEY (TaiKhoanId)
            REFERENCES dbo.TaiKhoan_V2(TaiKhoanId),
        CONSTRAINT FK_NhatKyEmail_KhachHang FOREIGN KEY (KhachHangId)
            REFERENCES dbo.KhachHang_V2(KhachHangId),
        CONSTRAINT FK_NhatKyEmail_HoaDon FOREIGN KEY (HoaDonId)
            REFERENCES dbo.HoaDon_V2(HoaDonId),
        CONSTRAINT FK_NhatKyEmail_MauEmail FOREIGN KEY (MauEmailId)
            REFERENCES dbo.MauEmail_V2(MauEmailId),
        CONSTRAINT CK_NhatKyEmail_LoaiGui CHECK (LoaiGui IN ('DON', 'HANG_LOAT')),
        CONSTRAINT CK_NhatKyEmail_TrangThai CHECK (TrangThai IN ('THANH_CONG', 'THAT_BAI'))
    );

    DECLARE @NhanVienMap TABLE (MaCu VARCHAR(50) PRIMARY KEY, IdMoi INT NOT NULL);
    DECLARE @KhachHangMap TABLE (MaCu VARCHAR(50) PRIMARY KEY, IdMoi INT NOT NULL);
    DECLARE @DanhMucMap TABLE (MaCu VARCHAR(50) PRIMARY KEY, IdMoi INT NOT NULL);
    DECLARE @NhaCungCapMap TABLE (MaCu VARCHAR(50) PRIMARY KEY, IdMoi INT NOT NULL);
    DECLARE @ChatLieuMap TABLE (MaCu VARCHAR(50) PRIMARY KEY, IdMoi INT NOT NULL);
    DECLARE @SanPhamMap TABLE (MaCu VARCHAR(50) PRIMARY KEY, IdMoi INT NOT NULL);
    DECLARE @HoaDonMap TABLE (MaCu VARCHAR(50) PRIMARY KEY, IdMoi INT NOT NULL);
    DECLARE @ChiTietHoaDonMap TABLE (MaCu VARCHAR(50) PRIMARY KEY, IdMoi INT NOT NULL);
    DECLARE @PhieuNhapMap TABLE (MaCu VARCHAR(50) PRIMARY KEY, IdMoi INT NOT NULL);
    DECLARE @PhieuThuMuaMap TABLE (MaCu VARCHAR(50) PRIMARY KEY, IdMoi INT NOT NULL);

    MERGE dbo.NhanVien_V2 AS target
    USING
    (
        SELECT RTRIM(MaNhanVien) AS MaCu, HoTen, GioiTinh, NgaySinh,
               NULLIF(LTRIM(RTRIM(SoDienThoai)), '') AS SoDienThoai,
               NULLIF(LTRIM(RTRIM(Email)), '') AS Email, DiaChi,
               COALESCE(NULLIF(LTRIM(RTRIM(ChucVu)), ''), N'Nhân viên') AS ChucVu
        FROM dbo.NhanVien
    ) AS source ON 1 = 0
    WHEN NOT MATCHED THEN
        INSERT (HoTen, GioiTinh, NgaySinh, SoDienThoai, Email, DiaChi, ChucVu, DangLamViec)
        VALUES (source.HoTen, source.GioiTinh, source.NgaySinh, source.SoDienThoai,
                source.Email, source.DiaChi, source.ChucVu, 1)
    OUTPUT source.MaCu, inserted.NhanVienId INTO @NhanVienMap(MaCu, IdMoi);

    INSERT dbo.TaiKhoan_V2
        (NhanVienId, TenDangNhap, MatKhauHash, VaiTro, PhaiDoiMatKhau, DangHoatDong)
    SELECT map.IdMoi, tk.TenDangNhap, tk.MatKhau,
           CASE WHEN UPPER(ISNULL(tk.Quyen, '')) IN ('ADMIN', 'QUANLY', 'QUẢN LÝ')
                THEN 'ADMIN' ELSE 'NHANVIEN' END,
           0, ISNULL(tk.TrangThai, 1)
    FROM dbo.TaiKhoan tk
    JOIN @NhanVienMap map ON map.MaCu = RTRIM(tk.MaNhanVien);

    MERGE dbo.KhachHang_V2 AS target
    USING
    (
        SELECT RTRIM(MaKhachHang) AS MaCu, TenKhachHang,
               COALESCE(NULLIF(LTRIM(RTRIM(SoDienThoai)), ''),
                   CONCAT('09', RIGHT('00000000' + CONVERT(VARCHAR(8), ROW_NUMBER() OVER (ORDER BY MaKhachHang)), 8))) AS SoDienThoai,
               DiaChi, ISNULL(DiemTichLuy, 0) AS DiemTichLuy
        FROM dbo.KhachHang
    ) AS source ON 1 = 0
    WHEN NOT MATCHED THEN
        INSERT (HoTen, SoDienThoai, Email, DiaChi, NgaySinh, ChoPhepNhanEmail, DiemTichLuy, DangHoatDong)
        VALUES (source.TenKhachHang, source.SoDienThoai, NULL, source.DiaChi, NULL, 0, source.DiemTichLuy, 1)
    OUTPUT source.MaCu, inserted.KhachHangId INTO @KhachHangMap(MaCu, IdMoi);

    MERGE dbo.DanhMuc_V2 AS target
    USING
    (
        SELECT RTRIM(MaDanhMuc) AS MaCu, TenDanhMuc, MoTa
        FROM dbo.DanhMuc
    ) AS source ON 1 = 0
    WHEN NOT MATCHED THEN
        INSERT (TenDanhMuc, MoTa, DangHoatDong)
        VALUES (source.TenDanhMuc, source.MoTa, 1)
    OUTPUT source.MaCu, inserted.DanhMucId INTO @DanhMucMap(MaCu, IdMoi);

    MERGE dbo.NhaCungCap_V2 AS target
    USING
    (
        SELECT RTRIM(MaNCC) AS MaCu, TenNCC,
               COALESCE(NULLIF(LTRIM(RTRIM(SoDienThoai)), ''),
                   CONCAT('08', RIGHT('00000000' + CONVERT(VARCHAR(8), ROW_NUMBER() OVER (ORDER BY MaNCC)), 8))) AS SoDienThoai,
               DiaChi
        FROM dbo.NhaCungCap
    ) AS source ON 1 = 0
    WHEN NOT MATCHED THEN
        INSERT (TenNhaCungCap, NguoiLienHe, SoDienThoai, Email, DiaChi, DangHoatDong)
        VALUES (source.TenNCC, NULL, source.SoDienThoai, NULL, source.DiaChi, 1)
    OUTPUT source.MaCu, inserted.NhaCungCapId INTO @NhaCungCapMap(MaCu, IdMoi);

    MERGE dbo.ChatLieu_V2 AS target
    USING
    (
        SELECT RTRIM(MaChatLieu) AS MaCu, TenChatLieu,
               ISNULL(GiaMuaVao, 0) AS GiaMuaVao, ISNULL(GiaBanRa, 0) AS GiaBanRa
        FROM dbo.ChatLieu
    ) AS source ON 1 = 0
    WHEN NOT MATCHED THEN
        INSERT (TenChatLieu, GiaMuaVao, GiaBanRa, DangHoatDong)
        VALUES (source.TenChatLieu, source.GiaMuaVao, source.GiaBanRa, 1)
    OUTPUT source.MaCu, inserted.ChatLieuId INTO @ChatLieuMap(MaCu, IdMoi);

    MERGE dbo.SanPham_V2 AS target
    USING
    (
        SELECT RTRIM(sp.MaSanPham) AS MaCu, dm.IdMoi AS DanhMucId, sp.TenSanPham,
               ISNULL(sp.GiaVon, 0) AS GiaVon, ISNULL(sp.GiaBan, 0) AS GiaBan,
               ISNULL(sp.SoLuongTon, 0) AS SoLuongTon, CONVERT(NVARCHAR(500), sp.DuongDanAnh) AS DuongDanAnh
        FROM dbo.SanPham sp
        JOIN @DanhMucMap dm ON dm.MaCu = RTRIM(sp.MaDanhMuc)
    ) AS source ON 1 = 0
    WHEN NOT MATCHED THEN
        INSERT (DanhMucId, TenSanPham, GiaVon, GiaBan, SoLuongTon, DuongDanAnh, DangKinhDoanh)
        VALUES (source.DanhMucId, source.TenSanPham, source.GiaVon, source.GiaBan,
                source.SoLuongTon, source.DuongDanAnh, 1)
    OUTPUT source.MaCu, inserted.SanPhamId INTO @SanPhamMap(MaCu, IdMoi);

    INSERT dbo.ChiTietChatLieu_V2 (SanPhamId, ChatLieuId, TrongLuong, DonViTinh)
    SELECT sp.IdMoi, cl.IdMoi,
           CASE WHEN ISNULL(ct.TrongLuong, 0) > 0 THEN ct.TrongLuong ELSE 0.001 END,
           COALESCE(NULLIF(LTRIM(RTRIM(ct.DonViTinh)), ''), N'gram')
    FROM dbo.ChiTietChatLieu ct
    JOIN @SanPhamMap sp ON sp.MaCu = RTRIM(ct.MaSanPham)
    JOIN @ChatLieuMap cl ON cl.MaCu = RTRIM(ct.MaChatLieu);

    MERGE dbo.HoaDon_V2 AS target
    USING
    (
        SELECT RTRIM(hd.MaHoaDon) AS MaCu, nv.IdMoi AS NhanVienId, kh.IdMoi AS KhachHangId,
               ISNULL(hd.NgayLap, SYSDATETIME()) AS NgayLap,
               ISNULL(hd.TongTien, 0) AS TongTien,
               ISNULL(hd.GiamGia, 0) AS GiamGia,
               ISNULL(hd.ThanhTien, 0) AS ThanhTien,
               COALESCE(NULLIF(LTRIM(RTRIM(hd.PhuongThucThanhToan)), ''), N'Tiền mặt') AS PhuongThucThanhToan,
               CASE WHEN UPPER(ISNULL(hd.TrangThai, '')) LIKE '%HUY%' THEN 'DA_HUY' ELSE 'DA_THANH_TOAN' END AS TrangThai
        FROM dbo.HoaDon hd
        JOIN @NhanVienMap nv ON nv.MaCu = RTRIM(hd.MaNhanVien)
        JOIN @KhachHangMap kh ON kh.MaCu = RTRIM(hd.MaKhachHang)
    ) AS source ON 1 = 0
    WHEN NOT MATCHED THEN
        INSERT (NhanVienId, KhachHangId, NgayLap, TongTien, GiamGia, ThanhTien, PhuongThucThanhToan, TrangThai)
        VALUES (source.NhanVienId, source.KhachHangId, source.NgayLap, source.TongTien,
                source.GiamGia, source.ThanhTien, source.PhuongThucThanhToan, source.TrangThai)
    OUTPUT source.MaCu, inserted.HoaDonId INTO @HoaDonMap(MaCu, IdMoi);

    MERGE dbo.ChiTietHoaDon_V2 AS target
    USING
    (
        SELECT RTRIM(ct.MaChiTiet) AS MaCu, hd.IdMoi AS HoaDonId, sp.IdMoi AS SanPhamId,
               CASE WHEN ct.SoLuong > 0 THEN ct.SoLuong ELSE 1 END AS SoLuong,
               COALESCE(ct.DonGia,
                   CASE WHEN ct.SoLuong > 0 THEN ct.TongCong / ct.SoLuong END, 0) AS DonGiaBan
        FROM dbo.ChiTietHoaDon ct
        JOIN @HoaDonMap hd ON hd.MaCu = RTRIM(ct.MaHoaDon)
        JOIN @SanPhamMap sp ON sp.MaCu = RTRIM(ct.MaSanPham)
    ) AS source ON 1 = 0
    WHEN NOT MATCHED THEN
        INSERT (HoaDonId, SanPhamId, SoLuong, DonGiaBan, HanBaoHanh)
        VALUES (source.HoaDonId, source.SanPhamId, source.SoLuong, source.DonGiaBan, NULL)
    OUTPUT source.MaCu, inserted.ChiTietHoaDonId INTO @ChiTietHoaDonMap(MaCu, IdMoi);

    MERGE dbo.PhieuNhap_V2 AS target
    USING
    (
        SELECT RTRIM(pn.MaPhieuNhap) AS MaCu, nv.IdMoi AS NhanVienId, ncc.IdMoi AS NhaCungCapId,
               ISNULL(pn.NgayNhap, SYSDATETIME()) AS NgayNhap, ISNULL(pn.TongTienNhap, 0) AS TongTienNhap
        FROM dbo.PhieuNhap pn
        JOIN @NhanVienMap nv ON nv.MaCu = RTRIM(pn.MaNhanVien)
        JOIN @NhaCungCapMap ncc ON ncc.MaCu = RTRIM(pn.MaNCC)
    ) AS source ON 1 = 0
    WHEN NOT MATCHED THEN
        INSERT (NhanVienId, NhaCungCapId, NgayNhap, TongTienNhap, TrangThai, GhiChu)
        VALUES (source.NhanVienId, source.NhaCungCapId, source.NgayNhap, source.TongTienNhap, 'HOAN_THANH', N'Dữ liệu chuyển từ schema cũ')
    OUTPUT source.MaCu, inserted.PhieuNhapId INTO @PhieuNhapMap(MaCu, IdMoi);

    INSERT dbo.ChiTietPhieuNhap_V2 (PhieuNhapId, SanPhamId, SoLuong, DonGiaNhap)
    SELECT pn.IdMoi, sp.IdMoi,
           CASE WHEN ct.SoLuong > 0 THEN ct.SoLuong ELSE 1 END,
           COALESCE(ct.GiaNhap, CASE WHEN ct.SoLuong > 0 THEN ct.ThanhTien / ct.SoLuong END, 0)
    FROM dbo.ChiTietPhieuNhap ct
    JOIN @PhieuNhapMap pn ON pn.MaCu = RTRIM(ct.MaPhieuNhap)
    JOIN @SanPhamMap sp ON sp.MaCu = RTRIM(ct.MaSanPham);

    MERGE dbo.PhieuThuMua_V2 AS target
    USING
    (
        SELECT RTRIM(ptm.MaPhieuThu) AS MaCu, nv.IdMoi AS NhanVienId, kh.IdMoi AS KhachHangId,
               ISNULL(ptm.NgayThuMua, SYSDATETIME()) AS NgayThuMua,
               ISNULL(ptm.TongTienThu, 0) AS TongTienThuMua
        FROM dbo.PhieuThuMua ptm
        JOIN @NhanVienMap nv ON nv.MaCu = RTRIM(ptm.MaNhanVien)
        JOIN @KhachHangMap kh ON kh.MaCu = RTRIM(ptm.MaKhachHang)
    ) AS source ON 1 = 0
    WHEN NOT MATCHED THEN
        INSERT (NhanVienId, KhachHangId, NgayThuMua, TongTienThuMua, TrangThai, GhiChu)
        VALUES (source.NhanVienId, source.KhachHangId, source.NgayThuMua, source.TongTienThuMua, 'HOAN_THANH', N'Dữ liệu chuyển từ schema cũ')
    OUTPUT source.MaCu, inserted.PhieuThuMuaId INTO @PhieuThuMuaMap(MaCu, IdMoi);

    INSERT dbo.ChiTietPhieuThuMua_V2
        (PhieuThuMuaId, ChatLieuId, SanPhamId, TenSanPhamThu, TrongLuong, DonViTinh, DonGiaThuMua)
    SELECT ptm.IdMoi, cl.IdMoi, sp.IdMoi,
           COALESCE(NULLIF(LTRIM(RTRIM(ct.TenSanPhamThu)), ''), N'Sản phẩm thu mua legacy'),
           CASE WHEN ISNULL(ct.TrongLuong, 0) > 0 THEN ct.TrongLuong ELSE 0.001 END,
           COALESCE(NULLIF(LTRIM(RTRIM(ct.DonViTinhThu)), ''), N'gram'),
           COALESCE(ct.DonGiaThu,
               CASE WHEN ISNULL(ct.TrongLuong, 0) > 0 THEN ct.ThanhTien / ct.TrongLuong END, 0)
    FROM dbo.ChiTietPhieuThuMua ct
    JOIN @PhieuThuMuaMap ptm ON ptm.MaCu = RTRIM(ct.MaPhieuThu)
    JOIN @ChatLieuMap cl ON cl.MaCu = RTRIM(ct.MaChatLieu)
    LEFT JOIN @SanPhamMap sp ON sp.MaCu = RTRIM(ct.MaSanPham);

    INSERT dbo.PhieuBaoHanh_V2
        (ChiTietHoaDonId, NgayTiepNhan, NoiDungBaoHanh, TrangThai, NgayTraDuKien, NgayTraThucTe, GhiChu)
    SELECT ct.IdMoi, ISNULL(pbh.NgayTiepNhan, SYSDATETIME()), pbh.NoiDungBaoHanh,
           CASE
               WHEN UPPER(ISNULL(pbh.TrangThai, '')) LIKE '%TRA%' THEN 'DA_TRA'
               WHEN UPPER(ISNULL(pbh.TrangThai, '')) LIKE '%HOAN%' THEN 'HOAN_THANH'
               WHEN UPPER(ISNULL(pbh.TrangThai, '')) LIKE '%XU%' THEN 'DANG_XU_LY'
               ELSE 'TIEP_NHAN'
           END,
           NULL, NULL, N'Dữ liệu chuyển từ schema cũ'
    FROM dbo.PhieuBaoHanh pbh
    JOIN @ChiTietHoaDonMap ct ON ct.MaCu = RTRIM(pbh.MaChiTiet);

    UPDATE hd
    SET TongTien = totals.TongTien,
        GiamGia = CASE WHEN hd.GiamGia > totals.TongTien THEN totals.TongTien ELSE hd.GiamGia END,
        ThanhTien = totals.TongTien - CASE WHEN hd.GiamGia > totals.TongTien THEN totals.TongTien ELSE hd.GiamGia END
    FROM dbo.HoaDon_V2 hd
    JOIN
    (
        SELECT HoaDonId, SUM(ThanhTien) AS TongTien
        FROM dbo.ChiTietHoaDon_V2
        GROUP BY HoaDonId
    ) totals ON totals.HoaDonId = hd.HoaDonId;

    UPDATE pn
    SET TongTienNhap = totals.TongTienNhap
    FROM dbo.PhieuNhap_V2 pn
    JOIN
    (
        SELECT PhieuNhapId, SUM(ThanhTien) AS TongTienNhap
        FROM dbo.ChiTietPhieuNhap_V2
        GROUP BY PhieuNhapId
    ) totals ON totals.PhieuNhapId = pn.PhieuNhapId;

    UPDATE ptm
    SET TongTienThuMua = totals.TongTienThuMua
    FROM dbo.PhieuThuMua_V2 ptm
    JOIN
    (
        SELECT PhieuThuMuaId, SUM(ThanhTien) AS TongTienThuMua
        FROM dbo.ChiTietPhieuThuMua_V2
        GROUP BY PhieuThuMuaId
    ) totals ON totals.PhieuThuMuaId = ptm.PhieuThuMuaId;

    CREATE INDEX IX_SanPham_DanhMucId ON dbo.SanPham_V2(DanhMucId);
    CREATE INDEX IX_CTChatLieu_ChatLieuId ON dbo.ChiTietChatLieu_V2(ChatLieuId);
    CREATE INDEX IX_HoaDon_TrangThai_NgayLap ON dbo.HoaDon_V2(TrangThai, NgayLap);
    CREATE INDEX IX_CTHoaDon_SanPhamId ON dbo.ChiTietHoaDon_V2(SanPhamId);
    CREATE INDEX IX_PhieuNhap_TrangThai_NgayNhap ON dbo.PhieuNhap_V2(TrangThai, NgayNhap);
    CREATE INDEX IX_CTPhieuNhap_SanPhamId ON dbo.ChiTietPhieuNhap_V2(SanPhamId);
    CREATE INDEX IX_PhieuThuMua_TrangThai_Ngay ON dbo.PhieuThuMua_V2(TrangThai, NgayThuMua);
    CREATE INDEX IX_CTThuMua_ChatLieuId ON dbo.ChiTietPhieuThuMua_V2(ChatLieuId);
    CREATE INDEX IX_BaoHanh_TrangThai_Ngay ON dbo.PhieuBaoHanh_V2(TrangThai, NgayTiepNhan);
    CREATE INDEX IX_NhatKyEmail_ThoiGian ON dbo.NhatKyGuiEmail_V2(ThoiGianGui);
    CREATE INDEX IX_NhatKyEmail_TrangThai ON dbo.NhatKyGuiEmail_V2(TrangThai);

    DROP TABLE dbo.PhieuBaoHanh;
    DROP TABLE dbo.ChiTietHoaDon;
    DROP TABLE dbo.ChiTietPhieuNhap;
    DROP TABLE dbo.ChiTietPhieuThuMua;
    DROP TABLE dbo.ChiTietChatLieu;
    DROP TABLE dbo.HoaDon;
    DROP TABLE dbo.PhieuNhap;
    DROP TABLE dbo.PhieuThuMua;
    DROP TABLE dbo.SanPham;
    DROP TABLE dbo.TaiKhoan;
    DROP TABLE dbo.KhachHang;
    DROP TABLE dbo.NhaCungCap;
    DROP TABLE dbo.ChatLieu;
    DROP TABLE dbo.DanhMuc;
    DROP TABLE dbo.NhanVien;

    EXEC sys.sp_rename N'dbo.NhanVien_V2', N'NhanVien';
    EXEC sys.sp_rename N'dbo.TaiKhoan_V2', N'TaiKhoan';
    EXEC sys.sp_rename N'dbo.KhachHang_V2', N'KhachHang';
    EXEC sys.sp_rename N'dbo.DanhMuc_V2', N'DanhMuc';
    EXEC sys.sp_rename N'dbo.NhaCungCap_V2', N'NhaCungCap';
    EXEC sys.sp_rename N'dbo.ChatLieu_V2', N'ChatLieu';
    EXEC sys.sp_rename N'dbo.SanPham_V2', N'SanPham';
    EXEC sys.sp_rename N'dbo.ChiTietChatLieu_V2', N'ChiTietChatLieu';
    EXEC sys.sp_rename N'dbo.HoaDon_V2', N'HoaDon';
    EXEC sys.sp_rename N'dbo.ChiTietHoaDon_V2', N'ChiTietHoaDon';
    EXEC sys.sp_rename N'dbo.PhieuNhap_V2', N'PhieuNhap';
    EXEC sys.sp_rename N'dbo.ChiTietPhieuNhap_V2', N'ChiTietPhieuNhap';
    EXEC sys.sp_rename N'dbo.PhieuThuMua_V2', N'PhieuThuMua';
    EXEC sys.sp_rename N'dbo.ChiTietPhieuThuMua_V2', N'ChiTietPhieuThuMua';
    EXEC sys.sp_rename N'dbo.PhieuBaoHanh_V2', N'PhieuBaoHanh';
    EXEC sys.sp_rename N'dbo.MauEmail_V2', N'MauEmail';
    EXEC sys.sp_rename N'dbo.NhatKyGuiEmail_V2', N'NhatKyGuiEmail';

    UPDATE dbo.PhieuThuMua
    SET MaPhieuNguon = N'LEGACY-PTM-' + RIGHT(N'000000' + CONVERT(NVARCHAR(10), PhieuThuMuaId), 6)
    WHERE MaPhieuNguon IS NULL;
    CREATE UNIQUE INDEX UX_PhieuThuMua_MaNguon ON dbo.PhieuThuMua(MaPhieuNguon)
        WHERE MaPhieuNguon IS NOT NULL;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0
        ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

PRINT N'Đã chuyển schema legacy sang schema v2 và giữ lại dữ liệu cũ.';
GO
