# BẢN ĐỒ VÀ THUYẾT MINH CÂY THƯ MỤC HỆ THỐNG
## Phân Tích Cấu Trúc Toàn Diện Từ Nhánh Gốc FINAL_DotNet\

> **Tài liệu này cung cấp:** Bản đồ phân loại, nguồn gốc phát sinh, mục đích kỹ thuật và vai trò vận hành của từng tệp tin và thư mục trong toàn bộ dự án `FINAL_DotNet\`.  
> **Mục đích:** Giúp lập trình viên, người tiếp nhận bàn giao và hội đồng đánh giá có cái nhìn toàn diện, khách quan về cấu trúc giải pháp mà không làm thay đổi hay xóa bỏ dữ liệu hiện hữu.

---

## 1. SƠ ĐỒ CÂY THƯ MỤC TỔNG THỂ

```text
FINAL_DotNet/
├── FINAL_DotNet.sln               # Tệp Solution Visual Studio chuẩn mực (tương thích VS 2017/2019/2022)
├── FINAL_DotNet.slnx              # Tệp Solution định dạng XML mới (Visual Studio 2022 Preview/Modern)
├── .gitignore                     # Cấu hình bỏ qua các tệp tạm, cache và build outputs của Git
├── README.md                      # Tài liệu tổng quan dự án, yêu cầu kỹ thuật và hướng dẫn vận hành
├── Doc.md                         # Tài liệu ôn tập Full-Stack, giải thích mã nguồn và câu hỏi phản biện
├── PROJECT_STRUCTURE.md           # [Tệp này] Thuyết minh chi tiết toàn bộ cây thư mục dự án
├── agents.md                      # Quy chuẩn kỹ thuật, công cụ biên dịch và nguyên tắc phát triển
├── CHANGELOG.md                   # Nhật ký theo dõi lịch sử cập nhật phiên bản (SemVer)
│
├── Database/                      # Toàn bộ tài nguyên liên quan đến Cơ sở Dữ liệu SQL Server
│   ├── QL_CuaHangDaQuy_PNJ.bak    # Bản sao lưu vật lý CSDL đầy đủ (17 bảng, dữ liệu mẫu hoàn chỉnh)
│   ├── 01_CreateDatabase.sql      # Script T-SQL khởi tạo cấu trúc bảng, khóa chính, khóa ngoại
│   ├── 02_SeedData.sql            # Script T-SQL nạp dữ liệu mẫu ban đầu (danh mục, sản phẩm, nhân sự)
│   ├── 03_VerifyDatabase.sql      # Script kiểm tra tính toàn vẹn và số lượng bản ghi của CSDL
│   ├── 04_MigrateLegacyToV2.sql   # Script chuyển đổi dữ liệu từ phiên bản nguyên mẫu cũ lên V2
│   ├── 05_CompleteSampleDataAfterMigration.sql # Script hoàn thiện dữ liệu mẫu phát sinh sau migration
│   ├── 06_AddMaPhieuNguonThuMua.sql # Script bổ sung trường mã phiếu nguồn cho nghiệp vụ thu mua
│   └── README.md                  # Hướng dẫn quy trình cài đặt và thứ tự thực thi script SQL
│
├── docs/                          # Hệ thống tài liệu kỹ thuật và bàn giao chuyên sâu
│   ├── HANDOVER_SPECIFICATION.md  # Tài liệu bàn giao và đặc tả kỹ thuật 13 phân hệ (32KB)
│   ├── PROJECT_STRUCTURE.md       # Bản sao lưu trữ của tài liệu thuyết minh cây thư mục
│   └── legacy/                    # Thư mục lưu trữ các bản thảo ghi chép ban đầu của nhóm phát triển
│       ├── Bcrypt_legacy.md       # Ghi chép ban đầu về thuật toán mã hóa mật khẩu BCrypt
│       ├── CHUC_NANG_VA_PHAN_QUYEN_legacy.md # Ghi chép phân loại chức năng và phân quyền sơ khai
│       └── Doc_legacy.md          # Bản thảo tài liệu kỹ thuật tổng hợp giai đoạn đầu
│
├── Packaging/                     # Các công cụ và gói phát hành phần mềm độc lập
│   ├── PNJ_Jewelry_Manager_v2.0_Portable.zip # Gói phân phối chạy ngay không cần cài đặt (18.17 MB)
│   ├── PNJ_Setup.iss              # Kịch bản biên dịch bộ cài đặt trình Wizard bằng Inno Setup 6
│   ├── Setup_Installer.bat        # Kịch bản kiểm tra môi trường, tạo shortcut Desktop và khởi chạy
│   └── Launch_App.bat             # Kịch bản khởi chạy ứng dụng nhanh gọn với đường dẫn tương đối
│
├── packages/                      # Thư mục chứa các gói thư viện NuGet phụ thuộc (EF, Guna, ZXing, BCrypt...)
│
└── FINAL_DotNet/                  # Dự án mã nguồn C# Windows Forms chính (.NET Framework 4.7.2)
    ├── FINAL_DotNet.csproj        # Tệp cấu hình dự án MSBuild, danh mục file biên dịch và tài nguyên
    ├── App.config                 # Cấu hình chuỗi kết nối CSDL và các thông số runtime
    ├── packages.config            # Danh mục định danh và phiên bản các gói NuGet được tham chiếu
    ├── Program.cs                 # Điểm khởi đầu ứng dụng (Main entry point)
    │
    ├── [Tầng Dịch vụ & Nghiệp vụ - Services & Helpers]
    │   ├── PosService.cs          # Nghiệp vụ bán hàng: giỏ hàng, trừ kho, transaction nguyên khối
    │   ├── ImageOptimizationHelper.cs # Pipeline nén ảnh nội suy Bicubic và quản lý đường dẫn tương đối
    │   ├── SaoLuuPhucHoiService.cs # Động cơ sao lưu/phục hồi SQL Server có thích ứng nén
    │   ├── BaoCaoService.cs       # Động cơ định dạng và cung cấp nguồn dữ liệu in ấn báo cáo
    │   ├── QrCodeService.cs       # Xử lý sinh mã QR và đọc mã qua ảnh/webcam hoàn toàn bằng ZXing.Net
    │   ├── EmailService.cs        # Động cơ gửi email SMTP chuẩn .NET (System.Net.Mail) với các thẻ thay thế động
    │   ├── XlsxImportService.cs   # Dịch vụ nạp dữ liệu từ file Excel chuẩn OpenXML (System.IO.Compression/Xml)
    │   ├── XlsxExportService.cs   # Dịch vụ kết xuất bảng tính Excel OpenXML chuẩn, không phụ thuộc bên ngoài
    │   ├── DatabaseConnection.cs  # Quản lý chuỗi kết nối và tạo DbContext theo chu kỳ ngắn hạn
    │   └── CurrentUserSession.cs  # Quản lý phiên làm việc, lưu trữ định danh và vai trò người dùng
    │
    ├── [Tầng Giao diện Người dùng - Presentation Windows Forms]
    │   ├── Form1.cs / .Designer.cs / .resx       # Màn hình Đăng nhập hệ thống
    │   ├── FormDangKy.cs / .Designer.cs / .resx # Màn hình Đăng ký tài khoản nội bộ
    │   ├── FormDoiMatKhau.cs                     # Cửa sổ bắt buộc đổi mật khẩu sau khi Admin reset
    │   ├── FrmMain.cs / .Designer.cs / .resx     # Khung làm việc chính (MDI container và menu điều hướng)
    │   ├── FrmBanHang.cs / .Designer.cs / .resx  # Điểm bán hàng thu ngân POS 2 cột hiện đại
    │   ├── FrmHoaDon.cs / .Designer.cs / .resx   # Quản lý, tra cứu, xem chi tiết và hủy hóa đơn
    │   ├── FrmKhachHang.cs / .Designer.cs / .resx # Quản lý hồ sơ và lịch sử mua sắm khách hàng
    │   ├── FrmSanPham.cs / .Designer.cs / .resx  # Quản lý sản phẩm, định mức chất liệu, ảnh và QR
    │   ├── FrmNhapHang.cs / .Designer.cs / .resx # Quản lý nhập hàng từ nhà cung cấp và tồn kho
    │   ├── FrmThuMua.cs / .Designer.cs / .resx   # Quản lý thu mua kim hoàn cũ và import Excel theo lô
    │   ├── FrmBaoHanh.cs / .Designer.cs          # Quản lý tiếp nhận và xử lý phiếu bảo hành
    │   ├── FrmQuanLyEmail.cs / .Designer.cs      # Quản lý cấu hình SMTP, mẫu thư và gửi email tiếp thị
    │   ├── FrmDanhMuc.cs / .Designer.cs / .resx  # Quản lý nhóm phân loại trang sức
    │   ├── FrmChatLieu.cs / .Designer.cs / .resx # Quản lý danh mục chất liệu và bảng giá thị trường
    │   ├── FrmNhaCungCap.cs / .Designer.cs       # Quản lý danh bạ nhà cung cấp hàng hóa
    │   ├── FrmNhanVien.cs / .Designer.cs / .resx # Quản lý hồ sơ nhân viên và trạng thái công tác
    │   ├── FrmTaiKhoan.cs / .Designer.cs / .resx # Quản lý tài khoản, phân quyền RBAC và reset mật khẩu
    │   ├── FrmThongKe.cs / .Designer.cs / .resx  # Dashboard thống kê doanh thu và biểu đồ Guna Chart
    │   ├── FrmSaoLuuPhucHoi.cs / .Designer.cs    # Màn hình quản trị sao lưu & phục hồi CSDL
    │   ├── FrmHelpDialog.cs                      # Hộp thoại tra cứu phím tắt POS và chẩn đoán kết nối
    │   └── FrmXemBaoCao.cs                       # Màn hình xem trước bản in hóa đơn và phiếu tiếp nhận
    │
    ├── [Bộ Điều khiển Giao diện & Chủ đề - Theme & Layout]
    │   ├── FluentDesktopLayout.cs  # Hỗ trợ co giãn và tái bố cục các panel giao diện theo độ phân giải
    │   ├── FluentSpecialLayouts.cs # Xử lý các bố cục bảng đặc thù cho lưới sản phẩm và giỏ hàng
    │   ├── LuxuryDarkGoldTheme.cs  # Định nghĩa các thông số màu sắc và phong cách thương hiệu PNJ
    │   └── LuxuryDarkGoldControls.cs # Định nghĩa các control tùy biến phong cách vàng kim
    │
    ├── [Tầng Dữ liệu Thực thể - Entity Framework 6 ORM]
    │   ├── Model1.edmx            # Tệp mô hình dữ liệu đồ họa (EDMX) ánh xạ 17 bảng CSDL
    │   ├── Model1.edmx.diagram    # Sơ đồ quan hệ thực thể trực quan giữa các bảng
    │   ├── Model1.Context.tt      # T4 template sinh mã lớp DbContext
    │   ├── Model1.Context.cs      # Lớp DbContext kế thừa: QL_CuaHangDaQuy_PNJEntities
    │   ├── Model1.tt              # T4 template sinh mã các lớp Entity POCO
    │   ├── Model1.cs / .Designer.cs # Tệp mã hỗ trợ mô hình Entity Framework
    │   └── [17 Lớp Thực thể POCO tương ứng 17 Bảng CSDL]:
    │       ├── NhanVien.cs, TaiKhoan.cs, KhachHang.cs, DanhMuc.cs, ChatLieu.cs,
    │       ├── SanPham.cs, ChiTietChatLieu.cs, NhaCungCap.cs, HoaDon.cs, ChiTietHoaDon.cs,
    │       ├── PhieuNhap.cs, ChiTietPhieuNhap.cs, PhieuThuMua.cs, ChiTietPhieuThuMua.cs,
    │       └── PhieuBaoHanh.cs, MauEmail.cs, NhatKyGuiEmail.cs
    │
    ├── [Tài nguyên Ứng dụng & Mẫu Báo cáo - Resources & Reports]
    │   ├── Reports/
    │   │   └── BaoCaoChung.rdlc   # Mẫu định dạng in ấn hóa đơn và phiếu bảo hành ReportViewer
    │   ├── Resources/             # Thư mục chứa 17 tệp ảnh tĩnh đã được tối ưu hóa dung lượng
    │   │   ├── a7.png, a71.png    # Logo thương hiệu PNJ (đã thu nhỏ chuẩn 256x256 px)
    │   │   ├── a7.jpg, a6.jpg, 99.jpg, hihi.jpg, Background.jpeg # Ảnh nền và banner trang trí
    │   │   └── [10 Ảnh Sản phẩm]: # Đã nén Bicubic về chuẩn 500x500 px trong suốt
    │   │       ├── bong_tai_kim_cuong_bach_kim.png, bong_tai_ruby_vang_18k.png
    │   │       ├── day_chuyen_bac_925.png, day_chuyen_sapphire_bach_kim.png
    │   │       ├── lac_chan_vang_14k.png, nhan_emerald_vang_18k.png
    │   │       ├── nhan_kim_cuong_18k.png, nhan_vang_24k_tron.png
    │   │       ├── vong_tay_bac_925_tron.png, vong_tay_vang_24k.png
    │   └── Properties/
    │       ├── AssemblyInfo.cs     # Khai báo thông tin phiên bản, bản quyền và metadata của phần mềm
    │       ├── Resources.resx / .Designer.cs # Bộ tài nguyên nhúng nội bộ
    │       └── Settings.settings / .Designer.cs # Lưu trữ cấu hình trạng thái người dùng
```

---

## 2. THUYẾT MINH CHI TIẾT TỪNG NHÓM TỆP VÀ NGUỒN GỐC PHÁT SINH

### 2.1. Nhóm tệp điều hành tại thư mục gốc (`FINAL_DotNet\`)

1. **`FINAL_DotNet.sln`:**
   - *Phân loại:* Tệp Solution tiêu chuẩn của Microsoft Visual Studio.
   - *Vai trò:* Định nghĩa danh sách các project thành phần, cấu hình build (`Debug`, `Release`), ánh xạ cấu trúc biên dịch. Đây là tệp tương thích rộng rãi với mọi phiên bản Visual Studio từ 2017, 2019 đến 2022.
2. **`FINAL_DotNet.slnx`:**
   - *Phân loại:* Tệp Solution định dạng XML hiện đại mới được Microsoft giới thiệu trên các bản cập nhật gần đây của Visual Studio 2022.
   - *Ngữ cảnh:* Được sinh ra tự động bởi môi trường phát triển hiện đại. Tồn tại song song với `.sln` để đảm bảo hệ thống mở được trên cả môi trường mới nhất lẫn các phiên bản Visual Studio truyền thống.
3. **`Doc.md`:**
   - *Phân loại:* Tài liệu ôn tập Full-Stack, giải thích mã nguồn và chuẩn bị bảo vệ đồ án.
   - *Vai trò:* Được thiết kế tập trung vào việc phục vụ thuyết trình, trả lời câu hỏi vấn đáp trước hội đồng phản biện. Bao gồm giải thích cặn kẽ nguyên lý kiến trúc 4 tầng, vòng đời `DbContext`, luồng dữ liệu khi thanh toán POS, giải thuật BCrypt, pipeline nén ảnh Bicubic và 10 câu hỏi cốt lõi hay gặp.
4. **`README.md`:**
   - *Phân loại:* Tài liệu giới thiệu tổng quan dự án theo chuẩn GitHub.
   - *Vai trò:* Cung cấp hướng dẫn nhanh cho lập trình viên hoặc người chấm bài: Công nghệ sử dụng, yêu cầu môi trường tối thiểu, hướng dẫn phục hồi CSDL bằng lệnh T-SQL, tài khoản kiểm thử mặc định và bảng phím tắt POS.
5. **`PROJECT_STRUCTURE.md`:**
   - *Phân loại:* Tài liệu thuyết minh cây thư mục giải pháp.
   - *Vai trò:* Phân tích tường minh vị trí, vai trò, nguồn gốc của từng tệp tin trong toàn bộ hệ thống để người đọc tự đánh giá cấu trúc dự án.
6. **`agents.md`:**
   - *Phân loại:* Tài liệu quy chuẩn kỹ thuật dành cho môi trường phát triển.
   - *Vai trò:* Ghi nhận các ràng buộc kỹ thuật của dự án: đường dẫn trình biên dịch MSBuild, cấu hình chuỗi kết nối LocalDB, nguyên tắc đặt tên biến bằng tiếng Anh và chính sách quản lý mã nguồn Git tại local.
7. **`CHANGELOG.md`:**
   - *Phân loại:* Nhật ký theo dõi phiên bản phát hành.
   - *Vai trò:* Ghi chép chi tiết các mốc cải tiến từ phiên bản nguyên mẫu v1.0.0 đến phiên bản hoàn thiện tái cấu trúc doanh nghiệp v2.0.0 theo chuẩn Semantic Versioning.
8. **`.gitignore`:**
   - *Phân loại:* Tệp cấu hình của hệ thống kiểm soát phiên bản Git.
   - *Vai trò:* Ngăn chặn việc đưa các tệp tạm phát sinh trong quá trình biên dịch (thư mục `bin/`, `obj/`, bộ nhớ đệm `.vs/`, cấu hình cá nhân `*.user`) vào kho lưu trữ Git.

---

### 2.2. Nhóm tài nguyên Cơ sở Dữ liệu (`Database\`)

1. **`QL_CuaHangDaQuy_PNJ.bak` (6.4 MB):**
   - *Phân loại:* Tệp sao lưu vật lý đầy đủ (Full Database Backup) của Microsoft SQL Server.
   - *Vai trò:* Đây là tệp cốt lõi để đưa CSDL vào hoạt động nhanh nhất. Chứa toàn bộ cấu trúc 17 bảng đạt chuẩn 3NF, các ràng buộc toàn vẹn khóa chính/khóa ngoại và tập dữ liệu mẫu hoàn chỉnh.
2. **`01_CreateDatabase.sql`:**
   - *Phân loại:* Script T-SQL tạo bảng (DDL - Data Definition Language).
   - *Vai trò:* Dành cho trường hợp người dùng muốn khởi tạo CSDL từ đầu bằng câu lệnh SQL thay vì restore file `.bak`. Định nghĩa chi tiết cấu trúc các bảng, kiểu dữ liệu, khóa chính tự tăng `IDENTITY` và các ràng buộc khóa ngoại.
3. **`02_SeedData.sql`:**
   - *Phân loại:* Script T-SQL nạp dữ liệu mẫu (DML - Data Manipulation Language).
   - *Vai trò:* Cung cấp dữ liệu ban đầu cho các bảng danh mục, chất liệu, tài khoản quản trị mặc định và danh sách các sản phẩm trang sức cơ bản.
4. **`03_VerifyDatabase.sql`:**
   - *Phân loại:* Script kiểm tra và xác minh CSDL.
   - *Vai trò:* Chứa các câu truy vấn `COUNT(*)`, kiểm tra tính toàn vẹn tham chiếu giữa các bảng và đối chiếu số lượng bản ghi mẫu sau khi tạo mới CSDL.
5. **`04_MigrateLegacyToV2.sql`, `05_CompleteSampleDataAfterMigration.sql`, `06_AddMaPhieuNguonThuMua.sql`:**
   - *Phân loại:* Các script nâng cấp lược đồ CSDL theo dòng thời gian (Database Migrations).
   - *Nguồn gốc phát sinh:* Trong quá trình phát triển dự án từ phiên bản sơ khai của nhóm lên phiên bản 2.0 hoàn chỉnh, cấu trúc CSDL cần bổ sung thêm các bảng chi tiết, tách bảng nhân viên - tài khoản và thêm cột nguồn thu mua. Các tệp này ghi nhận lại toàn bộ lịch sử các bước nâng cấp đó. Đối với người cài đặt mới từ tệp `.bak`, các script này đóng vai trò như tài liệu theo dõi tiến trình thay đổi cấu trúc CSDL.
6. **`Database/README.md`:**
   - *Phân loại:* Tài liệu hướng dẫn sử dụng thư mục CSDL.
   - *Vai trò:* Hướng dẫn thứ tự chạy các script SQL từ 01 đến 03 khi không sử dụng phương pháp Restore bằng tệp `.bak`.

---

### 2.3. Nhóm tài liệu chuyên sâu và lưu trữ lịch sử (`docs\`)

1. **`HANDOVER_SPECIFICATION.md` (32 KB):**
   - *Phân loại:* Tài liệu đặc tả kỹ thuật và bàn giao toàn diện hệ thống.
   - *Vai trò:* Mô tả chi tiết không gian nghiệp vụ của toàn bộ 13 phân hệ chức năng trong ứng dụng, ma trận phân quyền RBAC, từ điển dữ liệu chi tiết của 17 bảng quan hệ, nguyên lý các thuật toán nền tảng (Bicubic, Native OpenXML, Adaptive Backup) và runbook xử lý sự cố.
2. **Thư mục `docs/legacy/` (`Bcrypt_legacy.md`, `CHUC_NANG_VA_PHAN_QUYEN_legacy.md`, `Doc_legacy.md`):**
   - *Phân loại:* Tài liệu lưu trữ lịch sử (Historical Archive).
   - *Nguồn gốc:* Đây là các tệp ghi chép sơ thảo, rời rạc được tạo ra trong những ngày đầu tiên khi nhóm bắt đầu nghiên cứu đề tài. Khi hệ thống được nâng cấp lên phiên bản hoàn chỉnh, các tài liệu này được chuyển vào thư mục `legacy/` để bảo toàn lịch sử nghiên cứu ban đầu của nhóm, đồng thời nhường chỗ cho các bộ tài liệu chính thức (`Doc.md` và `HANDOVER_SPECIFICATION.md`) có cấu trúc mạch lạc và chuẩn mực hơn.

---

### 2.4. Nhóm công cụ đóng gói và phát hành (`Packaging\`)

1. **`PNJ_Jewelry_Manager_v2.0_Portable.zip` (18.17 MB):**
   - *Phân loại:* Bản phân phối độc lập chạy ngay (Portable Release Package).
   - *Nội dung:* Chứa toàn bộ tệp thực thi `FINAL_DotNet.exe` được biên dịch ở cấu hình `Release` tối ưu, toàn bộ các thư viện liên kết động DLL phụ thuộc, thư mục tài nguyên `Resources/`, bản sao lưu CSDL `.bak` và các script khởi chạy.
2. **`PNJ_Setup.iss`:**
   - *Phân loại:* Kịch bản đóng gói bộ cài đặt Inno Setup 6.
   - *Vai trò:* Định nghĩa quy trình đóng gói phần mềm thành một tệp cài đặt duy nhất dạng Setup Wizard (`.exe`). Kịch bản tích hợp sẵn hàm kiểm tra Registry để xác minh máy tính đích đã có .NET Framework 4.7.2 hay chưa, cấu hình tạo biểu tượng trên màn hình Desktop và thiết lập trình gỡ bỏ ứng dụng (Uninstaller).
3. **`Setup_Installer.bat`:**
   - *Phân loại:* Kịch bản tự động hóa triển khai môi trường (Deployment Batch Script).
   - *Vai trò:* Dành cho việc triển khai nhanh trên môi trường Windows mà không cần phần mềm tạo bộ cài đặt: Kiểm tra môi trường .NET, tạo thư mục sao lưu chuẩn `C:\PNJ_Backups`, tự động tạo shortcut `PNJ Jewelry Manager.lnk` trên Desktop thông qua Windows Script Host và hỏi người dùng khởi chạy ứng dụng.
4. **`Launch_App.bat`:**
   - *Phân loại:* Kịch bản khởi chạy ứng dụng nhanh.
   - *Vai trò:* Đảm bảo ứng dụng luôn chạy với thư mục làm việc (Working Directory) là thư mục chứa tệp thực thi, tránh lỗi không tìm thấy tài nguyên hình ảnh hoặc tệp cấu hình khi người dùng mở ứng dụng từ các lối tắt ngoài.

---

### 2.5. Dự án mã nguồn C# (`FINAL_DotNet\FINAL_DotNet\`)

#### Nhóm Tầng Dịch vụ (Service Layer):
1. **`PosService.cs`:** Tách biệt toàn bộ logic bán hàng ra khỏi giao diện: tính tổng tiền, chiết khấu, quản lý giỏ hàng, mở `db.Database.BeginTransaction()` để trừ tồn kho trong bảng `SanPham` và tạo bản ghi `HoaDon`, `ChiTietHoaDon` kèm hạn bảo hành tự động.
2. **`ImageOptimizationHelper.cs`:** Chứa thuật toán nén ảnh nội suy chất lượng cao `HighQualityBicubic` đưa ảnh về kích thước chuẩn tối đa $500 \times 500$ px, tự động sinh tên tệp theo cú pháp `sp_{ten}_{timestamp}.png`, lưu vào thư mục `Resources/` và tự động đồng bộ sang thư mục thực thi nhị phân.
3. **`SaoLuuPhucHoiService.cs`:** Thực thi các câu lệnh T-SQL sao lưu với `COPY_ONLY` và `CHECKSUM`. Tích hợp logic tự động phát hiện mã lỗi SQL 1844 để chuyển đổi linh hoạt giữa chế độ có nén và không nén (`NO_COMPRESSION`); thực thi lệnh `SET SINGLE_USER WITH ROLLBACK IMMEDIATE` khi phục hồi dữ liệu.
4. **`BaoCaoService.cs`:** Chuẩn bị mô hình dữ liệu (Report Model) và định dạng chuỗi tiền tệ theo văn hóa Việt Nam (`vi-VN`) cho các phiếu in hóa đơn bán lẻ, phiếu nhập kho, phiếu thu mua và phiếu tiếp nhận bảo hành.
5. **`QrCodeService.cs`:** Sử dụng thư viện `ZXing.Net` (`BarcodeWriter` với `BarcodeFormat.QR_CODE`) để render mã QR dưới dạng `Bitmap` và `ZXing.BarcodeReader` để quét và giải mã chuỗi mã sản phẩm từ hình ảnh.
6. **`EmailService.cs`:** Kết nối máy chủ SMTP qua giao thức bảo mật SSL/TLS của thư viện chuẩn .NET `System.Net.Mail` (`SmtpClient`), hỗ trợ gửi thư điện tử đơn lẻ hoặc hàng loạt kèm tệp đính kèm và tự động thay thế các tham số giữ chỗ như `{HoTen}`, `{TongTien}`, `{HanBaoHanh}`.
7. **`XlsxImportService.cs` & `XlsxExportService.cs`:** Đọc và ghi tệp bảng tính `.xlsx` chuẩn Office Open XML sử dụng các lớp hệ thống chuẩn `System.IO.Compression` và `System.Xml`. Hỗ trợ kiểm tra dữ liệu dòng lỗi khi import và định dạng tiêu đề, màu nền, đường viền khi export báo cáo mà không cần phụ thuộc gói ngoài hay cài Office.
8. **`DatabaseConnection.cs`:** Điểm tập trung khởi tạo đối tượng `QL_CuaHangDaQuy_PNJEntities` để đảm bảo chuỗi kết nối luôn nhất quán và dễ dàng bảo trì khi thay đổi máy chủ.
9. **`CurrentUserSession.cs`:** Lớp tĩnh lưu trữ thông tin phiên làm việc hiện hành của người dùng (Mã tài khoản, Mã nhân viên, Họ tên, Quyền hạn `ADMIN` hoặc `NHANVIEN`) trong suốt vòng đời ứng dụng.

#### Nhóm Tầng Giao diện (Presentation Forms):
- **Xác thực:** `Form1` (Đăng nhập), `FormDangKy` (Đăng ký tài khoản nội bộ), `FormDoiMatKhau` (Bắt buộc đổi mật khẩu sau khi Admin reset).
- **Điều hướng:** `FrmMain` (Thanh điều hướng Menu bên trái, quản lý quyền hiển thị theo vai trò, khu vực Header chứa nhãn phiên làm việc, nút Trợ giúp F1 và nút Thoát an toàn).
- **Nghiệp vụ cốt lõi:**
  - `FrmBanHang`: Màn hình bán hàng POS bố cục 2 cột, hỗ trợ phím tắt `F4` (Quét QR), `F9` (Thanh toán).
  - `FrmHoaDon`: Quản lý danh sách hóa đơn, chi tiết sản phẩm đã bán và hủy hóa đơn có hoàn trả tồn kho.
  - `FrmKhachHang`: Quản lý danh bạ khách hàng, phân loại thành viên và lịch sử tích lũy doanh số.
  - `FrmSanPham`: Quản lý sản phẩm, thành phần định mức chất liệu (BOM), nén ảnh tự động và kéo thả ảnh.
  - `FrmNhapHang`: Lập phiếu nhập kho từ nhà cung cấp, cập nhật giá vốn và tự động tăng số lượng tồn kho.
  - `FrmThuMua`: Nghiệp vụ thu mua vàng bạc, đá quý cũ từ khách hàng và import dữ liệu lớn từ Excel.
  - `FrmBaoHanh`: Quản lý tiếp nhận bảo hành trang sức, kiểm tra thời hạn và theo dõi tiến độ xử lý.
  - `FrmQuanLyEmail`: Cấu hình máy chủ SMTP, soạn thảo mẫu thư và gửi email tiếp thị / thông báo.
  - `FrmThongKe`: Phân tích doanh thu và tồn kho bằng đồ thị trực quan Guna Chart.
  - `FrmSaoLuuPhucHoi`: Quản trị sao lưu định kỳ và phục hồi cơ sở dữ liệu.
  - `FrmHelpDialog`: Modal tra cứu phím tắt thu ngân và chẩn đoán trạng thái kết nối máy chủ CSDL.
  - `FrmXemBaoCao`: Cửa sổ xem trước bản in hóa đơn và chứng từ qua Microsoft ReportViewer.
- **Danh mục nền tảng:** `FrmDanhMuc`, `FrmChatLieu`, `FrmNhaCungCap`, `FrmNhanVien`, `FrmTaiKhoan`.

#### Nhóm Giao diện Tùy biến (Theme & Layout Helpers):
- `FluentDesktopLayout.cs` & `FluentSpecialLayouts.cs`: Các lớp trợ giúp tính toán tỷ lệ, sắp xếp lại các cột trên `TableLayoutPanel` và `FlowLayoutPanel` để giao diện co giãn hài hòa trên các màn hình có độ phân giải khác nhau.
- `LuxuryDarkGoldTheme.cs` & `LuxuryDarkGoldControls.cs`: Lưu trữ định nghĩa bảng màu nhận diện thương hiệu PNJ (màu xanh Navy `#1B2735` và vàng kim `#B48C3C`).

#### Nhóm Thực thể Dữ liệu (Entity Framework 6):
- `Model1.edmx`: Tệp sơ đồ ánh xạ giữa 17 bảng trong SQL Server và các lớp đối tượng trong mã nguồn C#.
- 17 tệp lớp thực thể tương ứng (`SanPham.cs`, `HoaDon.cs`, `ChiTietHoaDon.cs`, `ChatLieu.cs`,...): Chứa các thuộc tính ánh xạ các cột trong bảng CSDL cùng các thuộc tính điều hướng (Navigation Properties) biểu diễn quan hệ khóa ngoại 1 - N và N - N.

#### Nhóm Tài nguyên & Mẫu in (Resources & Reports):
- `Reports/BaoCaoChung.rdlc`: Mẫu thiết kế báo cáo in ấn định dạng chuẩn chứa tiêu đề, thông tin doanh nghiệp, bảng chi tiết các dòng sản phẩm và chữ ký kế toán.
- `Resources/`: Chứa toàn bộ các hình ảnh phục vụ giao diện (logo PNJ, hình nền) và 10 bức ảnh trang sức đại diện (đã qua tối ưu hóa Bicubic về chuẩn $500 \times 500$ px để đảm bảo tốc độ nạp nhanh và tiết kiệm dung lượng bộ nhớ).

---

## 3. TỔNG KẾT

Cấu trúc trên phản ánh toàn bộ tiến trình kỹ thuật của hệ thống từ giai đoạn sơ khởi đến phiên bản hoàn chỉnh hiện tại. Mọi thành phần đều có vai trò cụ thể, bổ trợ cho nhau giữa các tầng kiến trúc (Giao diện ➔ Dịch vụ ➔ Truy cập Dữ liệu ➔ CSDL) và đảm bảo tính tương thích cao với các môi trường triển khai thực tế.