# Cơ sở dữ liệu QL_CuaHangDaQuy_PNJ

Chạy các script bằng SQL Server Management Studio theo đúng thứ tự:

1. `01_CreateDatabase.sql`: tạo CSDL và 17 bảng nghiệp vụ.
2. `02_SeedData.sql`: thêm dữ liệu mẫu cho toàn bộ bảng.
3. `03_VerifyDatabase.sql`: kiểm tra số dòng, tổng tiền và các trạng thái demo.

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

## Ảnh sản phẩm

Các đường dẫn trong dữ liệu mẫu có dạng `Resources\<ten-file>.png`. Khi có ảnh, đặt 10 file vào thư mục `FINAL_DotNet\Resources` với đúng tên đã khai báo. Script kiểm tra đường dẫn trong CSDL nhưng không yêu cầu file ảnh phải tồn tại trên máy.
