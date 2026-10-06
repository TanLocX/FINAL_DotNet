# Kiểm thử mã tự tăng

Chạy tại thư mục gốc repository trong Developer PowerShell của Visual Studio,
sau khi các package NuGet của ứng dụng đã được khôi phục:

```powershell
$testOutput = Join-Path (Get-Location).Path 'tests\bin\Debug\'
MSBuild.exe tests\IdentityCodeGenerationTests.csproj /t:Build /p:Configuration=Debug "/p:OutDir=$testOutput" /v:minimal /nologo
if ($LASTEXITCODE -ne 0) { throw 'Build kiểm thử thất bại.' }
& (Join-Path $testOutput 'IdentityCodeGenerationTests.exe')
if ($LASTEXITCODE -ne 0) { throw 'Kiểm thử thất bại.' }
```

Mặc định kết nối `(localdb)\MSSQLLocalDB` bằng Windows Authentication. Có thể
truyền instance SQL Server khác làm đối số đầu tiên của chương trình kiểm thử.
Tài khoản chạy cần quyền tạo/xóa database. Bộ kiểm thử tạo database riêng có
tên `PNJ_CodeGenerationTests_<GUID>` từ `Database/01_CreateDatabase.sql`, rồi
xóa database đó trong `finally`; không dùng database nghiệp vụ.

Thư mục build riêng giúp chạy kiểm thử khi file Debug của ứng dụng đang bị
Visual Studio hoặc phiên ứng dụng hiện tại khóa.

Các trường hợp kiểm thử gồm 16 bảng có `IDENTITY`: thêm mới, xóa mã lớn nhất
và dùng lại, xóa ở giữa và không lấp mã trống. Kiểm tra thêm bảng rỗng, xóa
hết, `TRUNCATE`, bản ghi ngừng hoạt động, ID `BIGINT`, rollback, INSERT bị lỗi
ràng buộc, lưu nhiều chi tiết trong giao dịch cùng bảng khóa ghép, lưu async
và bốn kết nối thêm đồng thời. Chương trình trả mã thoát 0 khi tất cả kiểm tra
thành công, mã 1 khi có lỗi.
