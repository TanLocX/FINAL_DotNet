# Cơ sở dữ liệu QL_CuaHangDaQuy_PNJ

Chạy các script bằng SQL Server Management Studio theo đúng thứ tự:

1. `01_CreateDatabase.sql`: tạo CSDL và 17 bảng nghiệp vụ.
2. `02_SeedData.sql`: thêm dữ liệu mẫu cho toàn bộ bảng.
3. `03_VerifyDatabase.sql`: kiểm tra số dòng, tổng tiền và các trạng thái demo.

Nếu CSDL schema v2 đã tồn tại từ trước, chạy thêm
`06_AddMaPhieuNguonThuMua.sql` một lần để bổ sung mã nguồn chống import Excel
trùng, sau đó chạy lại `03_VerifyDatabase.sql`.

Nếu database đang dùng schema cũ có khóa `MaNhanVien`, `MaSanPham` dạng chuỗi:

1. Tạo và xác minh file backup `.bak`.
2. Chạy `04_MigrateLegacyToV2.sql` để chuyển schema và giữ dữ liệu cũ.
3. Chạy `05_CompleteSampleDataAfterMigration.sql` để bổ sung dữ liệu mẫu còn thiếu.
4. Chạy `03_VerifyDatabase.sql` để xác nhận kết quả cuối cùng.

Các script tạo/seed sẽ dừng nếu phát hiện bảng hoặc dữ liệu hiện có. Chúng không tự động xóa hay ghi đè CSDL cũ.

## Tài khoản demo

Khi tạo database mới bằng `01_CreateDatabase.sql` và `02_SeedData.sql`:

- Quản trị: `admin`
- Nhân viên: `ngoclan`, `hoangnam`, `thuha`, `quocbao`
- Tài khoản khóa để demo: `mylinh`
- Mật khẩu chung: `PnjDemo@123`

Đây chỉ là mật khẩu dành cho dữ liệu demo. CSDL lưu chuỗi băm BCrypt, không lưu mật khẩu rõ.

Khi nâng cấp database cũ, script giữ nguyên tài khoản và mật khẩu cũ. Các tài
khoản mẫu được bổ sung là `dangky`, `quocbao`, `mylinh` với mật khẩu
`PnjDemo@123`; trong đó `dangky` phải đổi mật khẩu ở lần đăng nhập đầu tiên và
`mylinh` bị khóa.

## Kết nối CSDL dùng chung qua Radmin VPN

Ứng dụng ưu tiên bốn biến môi trường dưới đây khi chạy:

- `PNJ_DB_SERVER`: địa chỉ máy SQL Server, có thể kèm cổng (ví dụ `<IP-Radmin>,1433`).
- `PNJ_DB_NAME`: tên database, mặc định là `QL_CuaHangDaQuy_PNJ`.
- `PNJ_DB_USER`: tài khoản SQL Server.
- `PNJ_DB_PASSWORD`: mật khẩu SQL Server.

Ví dụ cấu hình cho cửa sổ PowerShell hiện tại:

```powershell
$env:PNJ_DB_SERVER = '<IP-Radmin>,1433'
$env:PNJ_DB_NAME = 'QL_CuaHangDaQuy_PNJ'
$env:PNJ_DB_USER = '<tai-khoan-sql>'
$env:PNJ_DB_PASSWORD = '<mat-khau-sql>'
```

Không ghi mật khẩu thật vào `App.config` hay commit lên Git. Nếu không có
`PNJ_DB_SERVER`, ứng dụng dùng kết nối `localhost` với Windows Authentication
trong `App.config` để lập trình độc lập trên máy cá nhân.

## Import dữ liệu thu mua từ Excel

Mở menu **Thu mua Excel**. Admin có thể tải file mẫu trực tiếp từ ứng dụng,
điền dữ liệu, chọn file và nhấn **Đọc và kiểm tra**. Chỉ khi mọi dòng hợp lệ nút
**Import vào CSDL** mới được bật.

Các cột bắt buộc gồm `MaPhieuNguon`, `NgayThuMua`, `MaNhanVien`,
`SoDienThoaiKhachHang`, `TenChatLieu`, `TenSanPhamThu`, `TrongLuong`,
`DonViTinh`, `DonGiaThuMua`, `TrangThai`. Cột `MaSanPham` và `GhiChu` không bắt
buộc. Nhiều dòng có cùng `MaPhieuNguon` được gộp vào một phiếu. Chỉ mục duy
nhất trong CSDL ngăn import cùng mã nguồn lần hai.

## Demo QR sản phẩm

Mở menu **Sản phẩm**, chọn một dòng rồi mở tab **QR sản phẩm**. Ứng dụng sinh
QR từ mã hiển thị như `SP000001`. Nhấn **Lưu QR PNG**, sau đó nhấn **Đọc QR từ
ảnh** và chọn lại file vừa lưu. Nếu QR hợp lệ, ứng dụng giải mã, kiểm tra sản
phẩm trong CSDL và tự chọn đúng dòng. QR sai định dạng hoặc mã không tồn tại sẽ
được từ chối. Chức năng sử dụng gói `ZXing.Net` phiên bản `0.16.11`.

## Ảnh sản phẩm

Các đường dẫn trong dữ liệu mẫu có dạng `Resources\<ten-file>.png`. Khi có ảnh, đặt 10 file vào thư mục `FINAL_DotNet\Resources` với đúng tên đã khai báo. Script kiểm tra đường dẫn trong CSDL nhưng không yêu cầu file ảnh phải tồn tại trên máy.

Danh sách tên file bắt buộc:

- `bong_tai_kim_cuong_bach_kim.png`
- `bong_tai_ruby_vang_18k.png`
- `day_chuyen_bac_925.png`
- `day_chuyen_sapphire_bach_kim.png`
- `lac_chan_vang_14k.png`
- `nhan_emerald_vang_18k.png`
- `nhan_kim_cuong_18k.png`
- `nhan_vang_24k_tron.png`
- `vong_tay_bac_925_tron.png`
- `vong_tay_vang_24k.png`

Nên dùng ảnh PNG vuông, cùng kích thước (khuyến nghị 800 x 800 px), nền sáng
hoặc trong suốt. Project tự động chép các file PNG/JPG/JPEG trong `Resources`
sang `bin\Debug\Resources` và `bin\Release\Resources` khi build.
