-- Tao lai ba ban ghi doc lap de demo thao tac bam nut Xoa trong ung dung.
-- Co the chay lai sau khi da xoa cac ban ghi demo; khong xoa du lieu hien co.
SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    IF EXISTS
    (
        SELECT 1 FROM dbo.NhaCungCap
        WHERE SoDienThoai = '0999009901'
          AND TenNhaCungCap <> N'DEMO XOA - Nha cung cap'
    )
        THROW 50700, N'So dien thoai demo da duoc nha cung cap khac su dung.', 1;

    IF NOT EXISTS (SELECT 1 FROM dbo.DanhMuc WHERE TenDanhMuc = N'DEMO XOA - Danh muc')
        INSERT dbo.DanhMuc (TenDanhMuc, MoTa, DangHoatDong)
        VALUES (N'DEMO XOA - Danh muc', N'Ban ghi de demo bam nut Xoa danh muc', 1);

    IF NOT EXISTS (SELECT 1 FROM dbo.ChatLieu WHERE TenChatLieu = N'DEMO XOA - Chat lieu')
        INSERT dbo.ChatLieu (TenChatLieu, GiaMuaVao, GiaBanRa, DangHoatDong)
        VALUES (N'DEMO XOA - Chat lieu', 1000000, 1200000, 1);

    IF NOT EXISTS (SELECT 1 FROM dbo.NhaCungCap WHERE TenNhaCungCap = N'DEMO XOA - Nha cung cap')
        INSERT dbo.NhaCungCap
            (TenNhaCungCap, NguoiLienHe, SoDienThoai, Email, DiaChi, DangHoatDong)
        VALUES
            (N'DEMO XOA - Nha cung cap', N'Demo', '0999009901', NULL, NULL, 1);

    IF EXISTS
    (
        SELECT 1 FROM dbo.DanhMuc dm
        WHERE dm.TenDanhMuc = N'DEMO XOA - Danh muc'
          AND (dm.DangHoatDong = 0 OR EXISTS
              (SELECT 1 FROM dbo.SanPham sp WHERE sp.DanhMucId = dm.DanhMucId))
    )
        THROW 50701, N'Danh muc demo khong du dieu kien xoa.', 1;

    IF EXISTS
    (
        SELECT 1 FROM dbo.ChatLieu cl
        WHERE cl.TenChatLieu = N'DEMO XOA - Chat lieu'
          AND (cl.DangHoatDong = 0 OR EXISTS
              (SELECT 1 FROM dbo.ChiTietChatLieu ct WHERE ct.ChatLieuId = cl.ChatLieuId)
              OR EXISTS
              (SELECT 1 FROM dbo.ChiTietPhieuThuMua tm WHERE tm.ChatLieuId = cl.ChatLieuId))
    )
        THROW 50702, N'Chat lieu demo khong du dieu kien xoa.', 1;

    IF EXISTS
    (
        SELECT 1 FROM dbo.NhaCungCap ncc
        WHERE ncc.TenNhaCungCap = N'DEMO XOA - Nha cung cap'
          AND (ncc.DangHoatDong = 0 OR ncc.SoDienThoai <> '0999009901' OR EXISTS
              (SELECT 1 FROM dbo.PhieuNhap pn WHERE pn.NhaCungCapId = ncc.NhaCungCapId))
    )
        THROW 50703, N'Nha cung cap demo khong du dieu kien xoa.', 1;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;

SELECT N'Danh muc' AS Loai, DanhMucId AS Id, TenDanhMuc AS Ten
FROM dbo.DanhMuc WHERE TenDanhMuc = N'DEMO XOA - Danh muc'
UNION ALL
SELECT N'Chat lieu', ChatLieuId, TenChatLieu
FROM dbo.ChatLieu WHERE TenChatLieu = N'DEMO XOA - Chat lieu'
UNION ALL
SELECT N'Nha cung cap', NhaCungCapId, TenNhaCungCap
FROM dbo.NhaCungCap WHERE TenNhaCungCap = N'DEMO XOA - Nha cung cap';
