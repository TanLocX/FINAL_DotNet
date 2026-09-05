# PNJ Jewelry Store Management System

Phần mềm quản lý bán hàng và vận hành chuỗi cửa hàng vàng bạc, đá quý và trang sức PNJ. Hệ thống được phát triển trên nền tảng Windows Forms với C# và .NET Framework 4.7.2, tích hợp Entity Framework 6 và hệ quản trị cơ sở dữ liệu Microsoft SQL Server.

---

## 1. Công nghệ và Yêu cầu Môi trường

### Công nghệ sử dụng
- **Ngôn ngữ & Nền tảng:** C# 7.3, .NET Framework 4.7.2 (Windows Forms)
- **Hệ quản trị CSDL:** Microsoft SQL Server (tương thích SQL Server 2014 trở lên, LocalDB, SQL Express)
- **Truy cập dữ liệu (ORM):** Entity Framework 6.5.2 (Database First / EDMX)
- **Bộ điều khiển giao diện:** Guna.UI2.WinForms 2.0.4.8 & Guna Charts 1.1.0
- **Bảo mật & Mã hóa:** `BCrypt.Net-Next` (v4.2.0) — Băm mật khẩu một chiều kèm Salt ngẫu nhiên (Work Factor 11).
- **Mã vạch & QR Code:** `ZXing.Net` (v0.16.11) — Sinh và giải mã QR Code sản phẩm 2 chiều.
- **Xử lý tệp bảng tính (.xlsx):** Thuần chuẩn Office Open XML sử dụng các thư viện hệ thống `System.IO.Compression` và `System.Xml` (hoạt động độc lập, không yêu cầu cài Microsoft Excel hay package phụ thuộc bên ngoài).
- **Dịch vụ Email SMTP:** Thư viện chuẩn `System.Net.Mail` (`SmtpClient`, `MailMessage`) hỗ trợ bảo mật SSL/TLS và phân giải tham số giữ chỗ động.
- **Báo cáo & In ấn:** `Microsoft.ReportingServices.ReportViewerControl.Winforms` (v150.1652.0).

### Yêu cầu hệ thống tối thiểu
- Hệ điều hành: Windows 10 hoặc Windows 11 (x86/x64).
- Môi trường thực thi: Microsoft .NET Framework 4.7.2 (có sẵn trên Windows 10 bản cập nhật gần đây).
- Máy chủ CSDL: SQL Server LocalDB `(localdb)\MSSQLLocalDB` hoặc SQL Server Express `.\SQLEXPRESS`.

---

## 2. Các Phân Hệ Chức Năng Chính

1. **Bán hàng tại quầy (POS):**
   - Bố cục 2 cột: Tra cứu sản phẩm nhanh bên phải, quản lý giỏ hàng và thanh toán bên trái.
   - Tìm kiếm khách hàng thành viên theo số điện thoại hoặc gán khách vãng lai.
   - Quét mã QR sản phẩm qua webcam hoặc file ảnh (`F4`).
   - Hỗ trợ chiết khấu phần trăm, tính thuế và tiền thừa.
   - Trừ số lượng tồn kho theo giao dịch nguyên khối (Database Transaction).
   - Tự động sinh hạn bảo hành 12 tháng cho từng món trang sức khi thanh toán thành công (`F9`).
2. **Quản lý Sản phẩm & Định mức (BOM):**
   - Quản lý danh mục trang sức, giá vốn, giá bán niêm yết, số lượng tồn kho.
   - Thiết lập thành phần chất liệu (Ví dụ: trọng lượng vàng 18K và kích cỡ kim cương cấu thành).
   - Pipeline nén ảnh tự động: Nén nội suy Bicubic về chuẩn 500x500 px giúp giảm 85% dung lượng đĩa và giải phóng bộ nhớ RAM.
   - Hỗ trợ kéo thả (Drag & Drop) ảnh trực tiếp từ Windows Explorer vào khung ảnh sản phẩm.
   - Tạo mã QR tương ứng mã sản phẩm `SP000001` và xuất ảnh PNG.
3. **Quản lý Hóa đơn:**
   - Tra cứu hóa đơn theo khoảng thời gian, số điện thoại khách hàng, nhân viên lập.
   - Xem chi tiết từng dòng hàng và thời hạn bảo hành tương ứng.
   - Hỗ trợ hủy hóa đơn có hoàn nguyên (rollback) số lượng tồn kho về bảng sản phẩm.
   - In lại hóa đơn bán lẻ theo chuẩn phiếu in nhiệt/A5.
4. **Thu mua Trang sức cũ:**
   - Nghiệp vụ thu mua lại vàng bạc, đá quý cũ từ khách hàng theo trọng lượng và tuổi vàng.
   - Hỗ trợ nạp dữ liệu thu mua hàng loạt từ file Excel (.xlsx) với tính năng kiểm tra lỗi dữ liệu trước khi lưu.
5. **Dịch vụ Bảo hành:**
   - Tiếp nhận sản phẩm bảo hành dựa trên mã hóa đơn gốc.
   - Kiểm tra hạn bảo hành tự động; quản lý tiến độ xử lý (Tiếp nhận -> Đang xử lý -> Hoàn thành / Hủy).
   - In phiếu hẹn bảo hành cho khách hàng.
6. **Báo cáo & Thống kê:**
   - Biểu đồ trực quan doanh thu theo thời gian và cơ cấu chất liệu sản phẩm (Guna Chart).
   - Bảng xếp hạng sản phẩm bán chạy kèm hình ảnh đại diện.
   - Xuất báo cáo doanh thu, phiếu nhập, phiếu thu mua ra file Excel định dạng chuẩn.
7. **Sao lưu & Phục hồi CSDL:**
   - Sao lưu CSDL ra tệp `.bak` vật lý có kiểm tra tính toàn vẹn `CHECKSUM` và `COPY_ONLY`.
   - Cơ chế tự thích ứng: Tự động chuyển về chế độ không nén (`NO_COMPRESSION`) nếu máy chủ SQL Server Express/LocalDB không hỗ trợ nén (mã lỗi SQL 1844).
   - Phục hồi dữ liệu an toàn với lệnh ngắt kết nối độc quyền (`SINGLE_USER`).
8. **Bảo mật & Phân quyền (RBAC):**
   - Hệ thống quản lý 2 vai trò bảo mật đăng nhập (`VaiTro`): Quản trị viên (`ADMIN`) và Nhân viên (`NHANVIEN`).
   - Nhân sự được phân công theo 5 chức vụ nghiệp vụ cụ thể (`ChucVu`): Quản lý cửa hàng, Nhân viên bán hàng, Nhân viên kho, Chăm sóc khách hàng, Nhân viên thu mua.
   - Mật khẩu lưu trữ dưới dạng băm BCrypt (Work Factor 11); chức năng Đặt lại mật khẩu tự động kích hoạt cờ bắt buộc đổi mật khẩu ở lần đăng nhập tiếp theo.

---

## 3. Hướng Dẫn Cài Đặt và Khởi Chạy

### Bước 1: Khôi phục Cơ sở Dữ liệu
Dự án cung cấp sẵn tệp sao lưu CSDL chuẩn tại:
`Database/QL_CuaHangDaQuy_PNJ.bak` (Dung lượng 6.4 MB)

Có thể phục hồi bằng một trong hai cách:
- **Cách 1 (Dùng SQL Server Management Studio - SSMS):** Chuột phải vào Databases ➔ Restore Database ➔ Chọn Device ➔ Trỏ tới tệp `.bak` ➔ Đặt tên CSDL là `QL_CuaHangDaQuy_PNJ` ➔ Nhấn OK.
- **Cách 2 (Dùng T-SQL):**
```sql
RESTORE DATABASE [QL_CuaHangDaQuy_PNJ]
FROM DISK = 'C:\PNJ_Backups\QL_CuaHangDaQuy_PNJ.bak'
WITH REPLACE;
```

### Bước 2: Cấu hình Chuỗi Kết Nối (Nếu Cần)
Tệp cấu hình `FINAL_DotNet\App.config` mặc định trỏ về máy chủ LocalDB:
```xml
<connectionStrings>
  <add name="QL_CuaHangDaQuy_PNJEntities" 
       connectionString="metadata=res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=(localdb)\MSSQLLocalDB;initial catalog=QL_CuaHangDaQuy_PNJ;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" 
       providerName="System.Data.EntityClient" />
</connectionStrings>
```
Nếu sử dụng instance khác (ví dụ SQL Server Express), chỉnh sửa `data source=.\SQLEXPRESS`.

### Bước 3: Khởi chạy Ứng dụng
- **Chạy trực tiếp từ Visual Studio:** Mở file `FINAL_DotNet.slnx`, đặt cấu hình `Debug` hoặc `Release` và nhấn `F5`.
- **Chạy từ bản dựng sẵn (Portable):** Vào thư mục `Packaging/`, giải nén tệp `PNJ_Jewelry_Manager_v2.0_Portable.zip` và chạy file `Launch_App.bat` (hoặc `FINAL_DotNet.exe`).

---

## 4. Tài Khoản Đăng Nhập Mặc Định

Tất cả tài khoản demo trong CSDL được khởi tạo với mật khẩu mẫu an toàn đã qua băm BCrypt:

| Tên đăng nhập | Mật khẩu mẫu | Vai trò (`VaiTro`) | Chức vụ nhân sự (`ChucVu`) | Ghi chú kiểm thử nghiệp vụ |
|:---:|:---:|:---:|:---:|---|
| `admin` | `PnjDemo@123` | `ADMIN` | Quản lý cửa hàng | Toàn quyền truy cập tất cả các phân hệ quản trị và cấu hình |
| `ngoclan` | `PnjDemo@123` | `NHANVIEN` | Nhân viên bán hàng | Tài khoản nhân viên chuẩn: Mở POS, Hóa đơn, Khách hàng, Bảo hành |
| `hoangnam` | `PnjDemo@123` | `NHANVIEN` | Nhân viên kho | Tài khoản nhân viên nghiệp vụ kho hàng và nhập hàng |
| `thuha` | `PnjDemo@123` | `NHANVIEN` | Chăm sóc khách hàng | Tài khoản kiểm thử luồng **Bắt buộc đổi mật khẩu** ở lần đăng nhập đầu |
| `quocbao` | `PnjDemo@123` | `NHANVIEN` | Nhân viên thu mua | Tài khoản nhân viên nghiệp vụ thu mua kim hoàn và nhập Excel |
| `mylinh` | `PnjDemo@123` | `NHANVIEN` | Nhân viên bán hàng | Tài khoản kiểm thử phản hồi bảo mật: **Đã bị khóa / ngừng hoạt động** |

---

## 5. Phím Tắt Thao Tác Nhanh (POS)

| Phím tắt | Phạm vi | Mô tả chức năng |
|:---:|:---:|---|
| `F1` | Toàn hệ thống | Mở hộp thoại Tra cứu trợ giúp, danh sách phím tắt và thông tin kết nối CSDL |
| `F4` | Màn hình Bán hàng | Mở hộp thoại chọn ảnh hoặc quét mã QR sản phẩm đưa vào giỏ hàng |
| `F9` | Màn hình Bán hàng | Xác nhận thanh toán hóa đơn và gọi lệnh in phiếu bán lẻ |
| `ESC` | Toàn hệ thống | Đóng nhanh các hộp thoại modal đang mở |
| `Enter` | Màn hình Đăng nhập | Đăng nhập ngay sau khi nhập xong mật khẩu |

---

## 6. Cấu Trúc Thư Mục Dự Án

```text
FINAL_DotNet/
├── Database/
│   ├── QL_CuaHangDaQuy_PNJ.bak        # Tệp sao lưu CSDL SQL Server vật lý
│   └── 01_CreateDatabase.sql...       # Bộ script SQL tạo bảng và seed dữ liệu
├── docs/
│   ├── HANDOVER_SPECIFICATION.md      # Tài liệu đặc tả kỹ thuật chi tiết 13 phân hệ
│   └── legacy/                        # Tài liệu nháp cũ đã lưu trữ an toàn
├── FINAL_DotNet/                      # Mã nguồn dự án Windows Forms
│   ├── Form1.cs                       # Form Đăng nhập hệ thống (Auth & BCrypt)
│   ├── FrmMain.cs                     # Cửa sổ chính điều hướng & Dashboard
│   ├── FrmBanHang.cs / PosService.cs  # Điểm bán hàng POS và nghiệp vụ tính tiền
│   ├── ImageOptimizationHelper.cs     # Bộ tiện ích nén ảnh nội suy Bicubic
│   ├── XlsxImportService.cs           # Xử lý nạp bảng tính OpenXML chuẩn
│   ├── XlsxExportService.cs           # Xử lý xuất báo cáo OpenXML chuẩn
│   ├── QrCodeService.cs               # Động cơ sinh và đọc mã QR qua ZXing.Net
│   ├── EmailService.cs                # Động cơ gửi email qua System.Net.Mail
│   ├── SaoLuuPhucHoiService.cs        # Động cơ sao lưu/phục hồi SQL Server
│   ├── Model1.edmx                    # Mô hình Entity Framework 6.5.2 Database First
│   └── Resources/                     # Tài nguyên hình ảnh đã nén tối ưu
├── Packaging/
│   ├── PNJ_Jewelry_Manager_v2.0_Portable.zip # Gói chạy ngay không cần cài đặt
│   ├── PNJ_Setup.iss                  # Kịch bản đóng gói Inno Setup 6
│   └── Setup_Installer.bat            # Kịch bản kiểm tra môi trường và cài đặt nhanh
├── Doc.md                             # Tài liệu ôn tập Full-Stack và kịch bản bảo vệ đồ án
├── PROJECT_STRUCTURE.md               # Thuyết minh chi tiết cây thư mục và bối cảnh mã nguồn
└── README.md                          # Tài liệu tổng quan dự án
```

---

## 7. Tài Liệu Nghiên Cứu Chi Tiết

- Đề cương ôn tập Full-Stack, giải thích code và kịch bản phản biện: xem tại [`Doc.md`](Doc.md).
- Đặc tả kỹ thuật chi tiết toàn bộ 13 phân hệ và 17 bảng CSDL: xem tại [`docs/HANDOVER_SPECIFICATION.md`](docs/HANDOVER_SPECIFICATION.md).
- Thuyết minh và bản đồ chi tiết cây thư mục dự án: xem tại [`PROJECT_STRUCTURE.md`](PROJECT_STRUCTURE.md).
- Bảng chấm điểm đối chiếu tiêu chí đồ án: xem tại [`RUBRIC CHAM DIEM - THAM KHAO.xlsx`](RUBRIC%20CHAM%20DIEM%20-%20THAM%20KHAO.xlsx).