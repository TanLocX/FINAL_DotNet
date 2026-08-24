USE QL_CuaHangDaQuy_PNJ;
GO

SET XACT_ABORT ON;
SET NOCOUNT ON;

IF OBJECT_ID(N'dbo.PhieuThuMua', N'U') IS NULL
    THROW 50300, N'Không tìm thấy bảng dbo.PhieuThuMua.', 1;

BEGIN TRY
    BEGIN TRANSACTION;

    IF COL_LENGTH(N'dbo.PhieuThuMua', N'MaPhieuNguon') IS NULL
        ALTER TABLE dbo.PhieuThuMua ADD MaPhieuNguon NVARCHAR(50) NULL;

    UPDATE dbo.PhieuThuMua
    SET MaPhieuNguon = N'LEGACY-PTM-' + RIGHT(N'000000' + CONVERT(NVARCHAR(10), PhieuThuMuaId), 6)
    WHERE MaPhieuNguon IS NULL;

    IF EXISTS
    (
        SELECT MaPhieuNguon
        FROM dbo.PhieuThuMua
        WHERE MaPhieuNguon IS NOT NULL
        GROUP BY MaPhieuNguon
        HAVING COUNT(*) > 1
    )
        THROW 50301, N'MaPhieuNguon đang bị trùng; không thể tạo chỉ mục duy nhất.', 1;

    IF NOT EXISTS
    (
        SELECT 1
        FROM sys.indexes
        WHERE object_id = OBJECT_ID(N'dbo.PhieuThuMua')
          AND name = N'UX_PhieuThuMua_MaNguon'
    )
        CREATE UNIQUE INDEX UX_PhieuThuMua_MaNguon ON dbo.PhieuThuMua(MaPhieuNguon)
            WHERE MaPhieuNguon IS NOT NULL;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

PRINT N'Đã bổ sung MaPhieuNguon và cơ chế chống import trùng cho dữ liệu thu mua.';
GO
