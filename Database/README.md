# Cơ sở dữ liệu QL_CuaHangDaQuy_PNJ

Chạy các script bằng SQL Server Management Studio theo đúng thứ tự:

1. `01_CreateDatabase.sql`: tạo CSDL và 17 bảng nghiệp vụ.
2. `02_SeedData.sql`: thêm dữ liệu mẫu cho toàn bộ bảng.
3. `03_VerifyDatabase.sql`: kiểm tra số dòng, tổng tiền và các trạng thái demo.

Các script tạo/seed sẽ dừng nếu phát hiện bảng hoặc dữ liệu hiện có. Chúng không tự động xóa hay ghi đè CSDL cũ.

## Tài khoản demo

- Quản trị: `admin`
- Nhân viên: `ngoclan`, `hoangnam`, `thuha`, `quocbao`
- Tài khoản khóa để demo: `mylinh`
- Mật khẩu chung: `PnjDemo@123`

Đây chỉ là mật khẩu dành cho dữ liệu demo. CSDL lưu chuỗi băm BCrypt, không lưu mật khẩu rõ.

## Ảnh sản phẩm

Các đường dẫn trong dữ liệu mẫu có dạng `Resources\<ten-file>.png`. Khi có ảnh, đặt 10 file vào thư mục `FINAL_DotNet\Resources` với đúng tên đã khai báo. Script kiểm tra đường dẫn trong CSDL nhưng không yêu cầu file ảnh phải tồn tại trên máy.
