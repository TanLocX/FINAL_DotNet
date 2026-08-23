USE master;
GO

IF DB_ID(N'QL_CuaHangDaQuy_PNJ') IS NULL
BEGIN
    CREATE DATABASE QL_CuaHangDaQuy_PNJ;
END;
GO

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

IF EXISTS
(
    SELECT 1
    FROM sys.tables
    WHERE is_ms_shipped = 0
      AND name <> N'sysdiagrams'
)
BEGIN
    THROW 50001, N'CSDL đã có bảng nghiệp vụ. Script dừng để không ghi đè dữ liệu hiện tại.', 1;
END;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    CREATE TABLE dbo.NhanVien
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
        ON dbo.NhanVien(SoDienThoai)
        WHERE SoDienThoai IS NOT NULL;

    CREATE TABLE dbo.TaiKhoan
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
            REFERENCES dbo.NhanVien(NhanVienId),
        CONSTRAINT CK_TaiKhoan_VaiTro CHECK (VaiTro IN ('ADMIN', 'NHANVIEN'))
    );

    CREATE TABLE dbo.KhachHang
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

    CREATE TABLE dbo.DanhMuc
    (
        DanhMucId        INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_DanhMuc PRIMARY KEY,
        TenDanhMuc       NVARCHAR(100) NOT NULL,
        MoTa             NVARCHAR(255) NULL,
        DangHoatDong     BIT NOT NULL CONSTRAINT DF_DanhMuc_HoatDong DEFAULT 1,
        CONSTRAINT UQ_DanhMuc_Ten UNIQUE (TenDanhMuc)
    );

    CREATE TABLE dbo.NhaCungCap
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

    CREATE TABLE dbo.ChatLieu
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

    CREATE TABLE dbo.SanPham
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
            REFERENCES dbo.DanhMuc(DanhMucId),
        CONSTRAINT CK_SanPham_GiaVon CHECK (GiaVon >= 0),
        CONSTRAINT CK_SanPham_GiaBan CHECK (GiaBan >= 0),
        CONSTRAINT CK_SanPham_Ton CHECK (SoLuongTon >= 0)
    );

    CREATE TABLE dbo.ChiTietChatLieu
    (
        SanPhamId        INT NOT NULL,
        ChatLieuId       INT NOT NULL,
        TrongLuong       DECIMAL(10,3) NOT NULL,
        DonViTinh        NVARCHAR(20) NOT NULL,
        CONSTRAINT PK_ChiTietChatLieu PRIMARY KEY (SanPhamId, ChatLieuId),
        CONSTRAINT FK_CTChatLieu_SanPham FOREIGN KEY (SanPhamId)
            REFERENCES dbo.SanPham(SanPhamId),
        CONSTRAINT FK_CTChatLieu_ChatLieu FOREIGN KEY (ChatLieuId)
            REFERENCES dbo.ChatLieu(ChatLieuId),
        CONSTRAINT CK_CTChatLieu_TrongLuong CHECK (TrongLuong > 0)
    );

    CREATE TABLE dbo.HoaDon
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
            REFERENCES dbo.NhanVien(NhanVienId),
        CONSTRAINT FK_HoaDon_KhachHang FOREIGN KEY (KhachHangId)
            REFERENCES dbo.KhachHang(KhachHangId),
        CONSTRAINT CK_HoaDon_Tong CHECK (TongTien >= 0),
        CONSTRAINT CK_HoaDon_GiamGia CHECK (GiamGia >= 0),
        CONSTRAINT CK_HoaDon_ThanhTien CHECK (ThanhTien >= 0),
        CONSTRAINT CK_HoaDon_TrangThai CHECK (TrangThai IN ('DA_THANH_TOAN', 'DA_HUY'))
    );

    CREATE TABLE dbo.ChiTietHoaDon
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
            REFERENCES dbo.HoaDon(HoaDonId),
        CONSTRAINT FK_CTHoaDon_SanPham FOREIGN KEY (SanPhamId)
            REFERENCES dbo.SanPham(SanPhamId),
        CONSTRAINT CK_CTHoaDon_SoLuong CHECK (SoLuong > 0),
        CONSTRAINT CK_CTHoaDon_DonGia CHECK (DonGiaBan >= 0)
    );

    CREATE TABLE dbo.PhieuNhap
    (
        PhieuNhapId       INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhieuNhap PRIMARY KEY,
        NhanVienId        INT NOT NULL,
        NhaCungCapId      INT NOT NULL,
        NgayNhap          DATETIME2 NOT NULL CONSTRAINT DF_PhieuNhap_Ngay DEFAULT SYSDATETIME(),
        TongTienNhap      DECIMAL(18,2) NOT NULL CONSTRAINT DF_PhieuNhap_Tong DEFAULT 0,
        TrangThai         VARCHAR(20) NOT NULL CONSTRAINT DF_PhieuNhap_TrangThai DEFAULT 'HOAN_THANH',
        GhiChu            NVARCHAR(500) NULL,
        CONSTRAINT FK_PhieuNhap_NhanVien FOREIGN KEY (NhanVienId)
            REFERENCES dbo.NhanVien(NhanVienId),
        CONSTRAINT FK_PhieuNhap_NCC FOREIGN KEY (NhaCungCapId)
            REFERENCES dbo.NhaCungCap(NhaCungCapId),
        CONSTRAINT CK_PhieuNhap_Tong CHECK (TongTienNhap >= 0),
        CONSTRAINT CK_PhieuNhap_TrangThai CHECK (TrangThai IN ('HOAN_THANH', 'DA_HUY'))
    );

    CREATE TABLE dbo.ChiTietPhieuNhap
    (
        ChiTietPhieuNhapId    INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_ChiTietPhieuNhap PRIMARY KEY,
        PhieuNhapId           INT NOT NULL,
        SanPhamId             INT NOT NULL,
        SoLuong               INT NOT NULL,
        DonGiaNhap            DECIMAL(18,2) NOT NULL,
        ThanhTien AS (CONVERT(DECIMAL(18,2), SoLuong * DonGiaNhap)) PERSISTED,
        CONSTRAINT UQ_CTPhieuNhap_Phieu_SanPham UNIQUE (PhieuNhapId, SanPhamId),
        CONSTRAINT FK_CTPhieuNhap_PhieuNhap FOREIGN KEY (PhieuNhapId)
            REFERENCES dbo.PhieuNhap(PhieuNhapId),
        CONSTRAINT FK_CTPhieuNhap_SanPham FOREIGN KEY (SanPhamId)
            REFERENCES dbo.SanPham(SanPhamId),
        CONSTRAINT CK_CTPhieuNhap_SoLuong CHECK (SoLuong > 0),
        CONSTRAINT CK_CTPhieuNhap_DonGia CHECK (DonGiaNhap >= 0)
    );

    CREATE TABLE dbo.PhieuThuMua
    (
        PhieuThuMuaId      INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhieuThuMua PRIMARY KEY,
        NhanVienId         INT NOT NULL,
        KhachHangId        INT NOT NULL,
        NgayThuMua         DATETIME2 NOT NULL CONSTRAINT DF_PhieuThuMua_Ngay DEFAULT SYSDATETIME(),
        TongTienThuMua     DECIMAL(18,2) NOT NULL CONSTRAINT DF_PhieuThuMua_Tong DEFAULT 0,
        TrangThai          VARCHAR(20) NOT NULL CONSTRAINT DF_PhieuThuMua_TrangThai DEFAULT 'HOAN_THANH',
        GhiChu             NVARCHAR(500) NULL,
        CONSTRAINT FK_PhieuThuMua_NhanVien FOREIGN KEY (NhanVienId)
            REFERENCES dbo.NhanVien(NhanVienId),
        CONSTRAINT FK_PhieuThuMua_KhachHang FOREIGN KEY (KhachHangId)
            REFERENCES dbo.KhachHang(KhachHangId),
        CONSTRAINT CK_PhieuThuMua_Tong CHECK (TongTienThuMua >= 0),
        CONSTRAINT CK_PhieuThuMua_TrangThai CHECK (TrangThai IN ('HOAN_THANH', 'DA_HUY'))
    );

    CREATE TABLE dbo.ChiTietPhieuThuMua
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
            REFERENCES dbo.PhieuThuMua(PhieuThuMuaId),
        CONSTRAINT FK_CTThuMua_ChatLieu FOREIGN KEY (ChatLieuId)
            REFERENCES dbo.ChatLieu(ChatLieuId),
        CONSTRAINT FK_CTThuMua_SanPham FOREIGN KEY (SanPhamId)
            REFERENCES dbo.SanPham(SanPhamId),
        CONSTRAINT CK_CTThuMua_TrongLuong CHECK (TrongLuong > 0),
        CONSTRAINT CK_CTThuMua_DonGia CHECK (DonGiaThuMua >= 0)
    );

    CREATE TABLE dbo.PhieuBaoHanh
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
            REFERENCES dbo.ChiTietHoaDon(ChiTietHoaDonId),
        CONSTRAINT CK_BaoHanh_TrangThai
            CHECK (TrangThai IN ('TIEP_NHAN', 'DANG_XU_LY', 'HOAN_THANH', 'DA_TRA')),
        CONSTRAINT CK_BaoHanh_NgayTraDuKien
            CHECK (NgayTraDuKien IS NULL OR NgayTraDuKien >= CONVERT(DATE, NgayTiepNhan)),
        CONSTRAINT CK_BaoHanh_NgayTraThucTe
            CHECK (NgayTraThucTe IS NULL OR NgayTraThucTe >= NgayTiepNhan)
    );

    CREATE TABLE dbo.MauEmail
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
            REFERENCES dbo.TaiKhoan(TaiKhoanId)
    );

    CREATE TABLE dbo.NhatKyGuiEmail
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
            REFERENCES dbo.TaiKhoan(TaiKhoanId),
        CONSTRAINT FK_NhatKyEmail_KhachHang FOREIGN KEY (KhachHangId)
            REFERENCES dbo.KhachHang(KhachHangId),
        CONSTRAINT FK_NhatKyEmail_HoaDon FOREIGN KEY (HoaDonId)
            REFERENCES dbo.HoaDon(HoaDonId),
        CONSTRAINT FK_NhatKyEmail_MauEmail FOREIGN KEY (MauEmailId)
            REFERENCES dbo.MauEmail(MauEmailId),
        CONSTRAINT CK_NhatKyEmail_LoaiGui CHECK (LoaiGui IN ('DON', 'HANG_LOAT')),
        CONSTRAINT CK_NhatKyEmail_TrangThai CHECK (TrangThai IN ('THANH_CONG', 'THAT_BAI'))
    );

    CREATE INDEX IX_SanPham_DanhMucId ON dbo.SanPham(DanhMucId);
    CREATE INDEX IX_CTChatLieu_ChatLieuId ON dbo.ChiTietChatLieu(ChatLieuId);
    CREATE INDEX IX_HoaDon_TrangThai_NgayLap ON dbo.HoaDon(TrangThai, NgayLap);
    CREATE INDEX IX_CTHoaDon_SanPhamId ON dbo.ChiTietHoaDon(SanPhamId);
    CREATE INDEX IX_PhieuNhap_TrangThai_NgayNhap ON dbo.PhieuNhap(TrangThai, NgayNhap);
    CREATE INDEX IX_CTPhieuNhap_SanPhamId ON dbo.ChiTietPhieuNhap(SanPhamId);
    CREATE INDEX IX_PhieuThuMua_TrangThai_Ngay ON dbo.PhieuThuMua(TrangThai, NgayThuMua);
    CREATE INDEX IX_CTThuMua_ChatLieuId ON dbo.ChiTietPhieuThuMua(ChatLieuId);
    CREATE INDEX IX_BaoHanh_TrangThai_Ngay ON dbo.PhieuBaoHanh(TrangThai, NgayTiepNhan);
    CREATE INDEX IX_NhatKyEmail_ThoiGian ON dbo.NhatKyGuiEmail(ThoiGianGui);
    CREATE INDEX IX_NhatKyEmail_TrangThai ON dbo.NhatKyGuiEmail(TrangThai);

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0
        ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

PRINT N'Đã tạo CSDL QL_CuaHangDaQuy_PNJ với 17 bảng nghiệp vụ.';
GO
