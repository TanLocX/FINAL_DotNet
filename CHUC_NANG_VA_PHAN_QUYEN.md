# TÀI LIỆU CHI TIẾT CHỨC NĂNG VÀ MA TRẬN PHÂN QUYỀN HỆ THỐNG PNJ MANAGER

---

## I. TỔNG QUAN VỀ PHÂN QUYỀN HỆ THỐNG

Hệ thống **PNJ Manager (Hệ thống Quản lý Bán vàng bạc, đá quý và Vận hành Tiệm Kim hoàn)** chia người dùng thành **2 cấp độ phân quyền (Roles)** rõ ràng:

1. **Quản trị viên (`ADMIN`)**:
   - Có toàn quyền truy cập **100%** tất cả các phân hệ và tính năng trên hệ thống (bao gồm toàn bộ nghiệp vụ kinh doanh, vận hành và quản trị chuyên sâu).
   - Nắm quyền thiết lập danh mục nền tảng, điều chỉnh bảng giá chất liệu/vàng bạc, quản lý hồ sơ nhân viên, cấp phát và thu hồi tài khoản, phân quyền, sao lưu và phục hồi dữ liệu hệ thống.

2. **Nhân viên (`NHANVIEN`)**:
   - Được cấp quyền thực hiện các nghiệp vụ thường nhật: Bán hàng tại quầy, Lập & Tra cứu hóa đơn, Quản lý khách hàng, Quản lý sản phẩm & Thành phần, Nhập hàng, Thu mua hàng cũ, Tiếp nhận & Xử lý bảo hành, Gửi email chăm sóc khách hàng, và Xem báo cáo thống kê doanh thu.
   - **Bị khóa/ẩn hoàn toàn** nhóm menu **QUẢN TRỊ** (Nhân viên, Tài khoản, Danh mục, Chất liệu & Giá, Nhà cung cấp, Sao lưu / Phục hồi).
   - Nếu cố ý truy cập trực tiếp các form quản trị, hệ thống sẽ chặn tự động và thông báo: *"Bạn không có quyền sử dụng chức năng này"*.

---

## II. BẢNG MA TRẬN PHÂN QUYỀN (PERMISSION MATRIX)

| Nhóm chức năng | Màn hình / Tính năng | Mô tả nghiệp vụ | Nhân viên (`NHANVIEN`) | Quản trị viên (`ADMIN`) |
| :--- | :--- | :--- | :---: | :---: |
| **TỔNG QUAN** | **Tổng quan hệ thống** (`FrmTongQuan`) | Xem KPI doanh thu ngày/tháng, số đơn hàng, cảnh báo hàng tồn ít, top bán chạy. |  Cho phép |  Cho phép |
| **KINH DOANH** | **Bán hàng tại quầy** (`FrmBanHang`) | Tạo giỏ hàng, chọn sản phẩm (quét QR/Barcode), chọn khách, giảm giá, thanh toán. |  Cho phép |  Cho phép |
| | **Quản lý hóa đơn** (`FrmHoaDon`) | Lọc/tìm kiếm hóa đơn, xem chi tiết hóa đơn, hủy hóa đơn (hoàn kho), in hóa đơn. |  Cho phép |  Cho phép |
| | **Quản lý khách hàng** (`FrmKhachHang`) | Thêm/sửa/tìm kiếm khách hàng, xem lịch sử mua sắm, tích lũy điểm, xuất Excel. |  Cho phép |  Cho phép |
| **HÀNG HÓA & DỊCH VỤ** | **Quản lý sản phẩm** (`FrmSanPham`) | Thêm/sửa/tìm kiếm sản phẩm, thiết lập chi tiết chất liệu, sinh/quét QR, xuất/nhập Excel. |  Cho phép |  Cho phép |
| | **Nhập hàng** (`FrmNhapHang`) | Lập phiếu nhập từ NCC, tự động cộng kho, xem lịch sử nhập, hủy phiếu nhập, in báo cáo. |  Cho phép |  Cho phép |
| | **Thu mua Excel** (`FrmThuMua`) | Định giá thu mua trang sức/vàng cũ, nhập dữ liệu thu mua từ Excel, xuất báo cáo thu mua. |  Cho phép |  Cho phép |
| | **Quản lý bảo hành** (`FrmBaoHanh`) | Tiếp nhận bảo hành theo HĐ, xử lý tiến độ, ghi chú chi phí, hoàn trả, in phiếu bảo hành. |  Cho phép |  Cho phép |
| **VẬN HÀNH** | **Quản lý Email** (`FrmQuanLyEmail`) | Cấu hình SMTP, tạo mẫu email, gửi email đơn/hàng loạt (sinh nhật, tri ân), hẹn giờ gửi. |  Cho phép |  Cho phép |
| | **Báo cáo & Thống kê** (`FrmThongKe`) | Thống kê doanh thu, chi phí, lợi nhuận, biểu đồ kinh doanh, xuất Excel báo cáo. |  Cho phép |  Cho phép |
| **QUẢN TRỊ NỀN TẢNG** | **Quản lý nhân viên** (`FrmNhanVien`) | Thêm, sửa, xóa/ngừng việc nhân viên, quản lý chức vụ, lương, phòng ban. | ❌ Bị chặn |  Cho phép |
| | **Quản lý tài khoản** (`FrmTaiKhoan`) | Tạo tài khoản cho nhân viên, gán vai trò (ADMIN/NHANVIEN), đổi/reset mật khẩu, khóa nick. | ❌ Bị chặn |  Cho phép |
| | **Danh mục sản phẩm** (`FrmDanhMuc`) | Thêm, sửa, xóa các nhóm danh mục trang sức (Nhẫn, Dây chuyền, Lắc tay, Bông tai...). | ❌ Bị chặn |  Cho phép |
| | **Chất liệu & Bảng giá** (`FrmChatLieu`) | Quản lý vàng 24K, 18K, 14K, Bạc, Kim cương; cập nhật đơn giá mua/bán tham khảo. | ❌ Bị chặn |  Cho phép |
| | **Quản lý Nhà cung cấp** (`FrmNhaCungCap`)| Thêm, sửa, xóa thông tin nhà cung ứng vàng, đá quý, phôi trang sức. | ❌ Bị chặn |  Cho phép |
| | **Sao lưu & Phục hồi CSDL** (`FrmSaoLuuPhucHoi`)| Tạo file backup `.bak` của SQL Server và phục hồi cơ sở dữ liệu khi cần. | ❌ Bị chặn |  Cho phép |
| **TIỆN ÍCH CHUNG** | **Đổi mật khẩu** (`FrmDoiMatKhau`) | Tự đổi mật khẩu tài khoản đang đăng nhập hiện tại. |  Cho phép |  Cho phép |
| | **Đăng xuất** | Đăng xuất phiên làm việc hiện tại để bảo mật tài khoản. |  Cho phép |  Cho phép |

---

## III. CHI TIẾT CHỨC NĂNG THEO TỪNG PHÂN HỆ

### 1. Phân hệ Bán hàng & Lập hóa đơn (`FrmBanHang`)
- **Đối tượng sử dụng**: Nhân viên thu ngân, Nhân viên bán hàng, Admin.
- **Tính năng chi tiết**:
  - Tìm kiếm sản phẩm nhanh theo mã, tên hoặc quét trực tiếp **Mã QR / Barcode**.
  - Tự động hiển thị đơn giá bán chuẩn (tính từ giá nguyên liệu + tiền công + % tỷ lệ lợi nhuận).
  - Chọn số lượng, thêm vào giỏ hàng, cập nhật số lượng, xóa dòng sản phẩm.
  - Chọn khách hàng thành viên có sẵn hoặc thêm nhanh thông tin khách hàng mới.
  - Áp dụng chiết khấu / giảm giá (% hoặc số tiền) theo chương trình khuyến mãi.
  - Chọn phương thức thanh toán: **Tiền mặt**, **Chuyển khoản ngân hàng**, **Thẻ tín dụng**.
  - Lưu và hoàn tất đơn hàng: Tự động trừ số lượng hàng trong kho và cấp phát mã hóa đơn duy nhất.

### 2. Phân hệ Quản lý Hóa đơn (`FrmHoaDon`)
- **Đối tượng sử dụng**: Nhân viên, Admin.
- **Tính năng chi tiết**:
  - Bộ lọc tra cứu đa năng: Tìm theo mã HĐ, tên khách hàng, nhân viên lập, khoảng thời gian (Từ ngày - Đến ngày), trạng thái (Đã thanh toán / Đã hủy), khoảng giá trị hóa đơn.
  - Danh sách hóa đơn nửa trên: Hiển thị đầy đủ thông tin tóm tắt và trạng thái thanh toán.
  - Chi tiết hóa đơn nửa dưới: Xem danh sách các món hàng đã mua, số lượng, đơn giá, thời hạn bảo hành tương ứng.
  - **Hủy hóa đơn**: Hủy các hóa đơn sai sót hoặc khách trả hàng (hệ thống tự động cộng hoàn trả số lượng hàng vào kho).
  - **In & Xem báo cáo**: Xuất hóa đơn ra báo cáo đồ họa trực quan để in cho khách hàng (`FrmXemBaoCao`).

### 3. Phân hệ Quản lý Khách hàng (`FrmKhachHang`)
- **Đối tượng sử dụng**: Nhân viên CSKH, Thu ngân, Admin.
- **Tính năng chi tiết**:
  - Quản lý hồ sơ khách hàng: Mã KH, Họ tên, Số điện thoại, Email, Giới tính, Ngày sinh, Địa chỉ.
  - Tra cứu lịch sử toàn bộ các lần mua hàng của từng khách hàng.
  - Thống kê tổng số tiền khách đã chi tiêu để phân loại khách hàng VIP / Thân thiết.
  - Xuất danh bạ khách hàng ra định dạng Microsoft Excel (`.xlsx`).

### 4. Phân hệ Quản lý Sản phẩm (`FrmSanPham`)
- **Đối tượng sử dụng**: Nhân viên kho, Nhân viên bán hàng, Admin.
- **Tính năng chi tiết**:
  - Quản lý thông tin trang sức: Mã SP, Tên sản phẩm, Danh mục, Tiền công chế tác, Tỷ lệ lợi nhuận, Đơn vị tính, Số lượng tồn kho, Trạng thái kinh doanh, Hình ảnh sản phẩm.
  - **Cấu hình thành phần chất liệu (`ChiTietChatLieu`)**: Một sản phẩm có thể cấu thành từ nhiều chất liệu (Vàng 18K, Kim Cương, Đá CZ...) với trọng lượng riêng biệt.
  - **Mã QR Code**: Tự động sinh mã QR chứa thông tin sản phẩm và hỗ trợ quét mã QR để nhận diện nhanh.
  - **Excel**: Xuất danh sách sản phẩm ra Excel và Nhập hàng loạt sản phẩm từ file Excel.

### 5. Phân hệ Nhập hàng (`FrmNhapHang`)
- **Đối tượng sử dụng**: Nhân viên thủ kho, Admin.
- **Tính năng chi tiết**:
  - Tạo phiếu nhập kho: Chọn nhà cung cấp, chọn các sản phẩm cần nhập, nhập số lượng và đơn giá nhập thực tế.
  - Xác nhận nhập kho: Tự động tăng số lượng tồn kho tương ứng của sản phẩm.
  - Tra cứu lịch sử nhập hàng theo ngày, nhà cung cấp, mã phiếu nhập.
  - Hủy phiếu nhập khi có sai sót (tự động khấu trừ lại tồn kho đã cộng).
  - Xem và in phiếu nhập kho (`FrmXemBaoCao`).

### 6. Phân hệ Thu mua Trang sức cũ / Import Excel (`FrmThuMua`)
- **Đối tượng sử dụng**: Nhân viên thẩm định / thu mua, Admin.
- **Tính năng chi tiết**:
  - Lập phiếu thu mua trang sức cũ từ khách hàng vãng lai.
  - Công cụ định giá: Tự động tính tiền thu mua dựa trên loại chất liệu, trọng lượng thực tế và đơn giá thu mua niêm yết hiện hành.
  - Hỗ trợ Import file Excel bảng kê thu mua vàng bạc từ chi nhánh hoặc nguồn ngoài.
  - Xuất báo cáo danh sách thu mua ra file Excel.

### 7. Phân hệ Quản lý Bảo hành (`FrmBaoHanh`)
- **Đối tượng sử dụng**: Nhân viên kỹ thuật / CSKH, Admin.
- **Tính năng chi tiết**:
  - **Tiếp nhận bảo hành**: Tìm kiếm theo hóa đơn đã bán, kiểm tra hạn bảo hành của sản phẩm, ghi nhận tình trạng hỏng hóc/yêu cầu của khách (đánh bóng, gắn đá, chỉnh size, hàn chấu...) và hẹn ngày trả.
  - **Xử lý bảo hành**: Cập nhật tiến độ (`Đang xử lý` ➔ `Đã hoàn thành` ➔ `Đã trả khách`), ghi nhận chi phí phát sinh (nếu có) và ngày trả thực tế.
  - **In phiếu bảo hành**: Xuất phiếu tiếp nhận và phiếu bảo hành chính thức cho khách hàng.

### 8. Phân hệ Email Chăm sóc Khách hàng (`FrmQuanLyEmail`)
- **Đối tượng sử dụng**: Nhân viên Marketing / CSKH, Admin.
- **Tính năng chi tiết**:
  - **Cấu hình SMTP**: Thiết lập máy chủ gửi mail (Gmail, Outlook, Host riêng, Cổng, Email, Mật khẩu ứng dụng, SSL/TLS).
  - **Kho mẫu email (Email Templates)**: Tạo và quản lý các mẫu email soạn sẵn (Chúc mừng sinh nhật, Thông báo ưu đãi, Nhắc lịch nhận bảo hành, Thư cảm ơn sau mua hàng).
  - **Hỗ trợ thẻ Placeholder tự động**: `{TenKhachHang}`, `{SoDienThoai}`, `{MaHoaDon}`, `{TongTien}`, `{NgayHienTai}`.
  - **Gửi đơn lẻ & Gửi hàng loạt (Bulk Email)**: Gửi tới từng khách hàng hoặc gửi đồng loạt theo bộ lọc khách hàng.
  - **Hẹn giờ gửi email**: Lên lịch gửi tự động theo ngày giờ định trước.
  - **Nhật ký gửi mail**: Giám sát lịch sử gửi, trạng thái Thành công / Thất bại và chi tiết mã lỗi.

### 9. Phân hệ Báo cáo & Thống kê (`FrmThongKe`)
- **Đối tượng sử dụng**: Nhân viên quản lý, Admin.
- **Tính năng chi tiết**:
  - Thống kê doanh số bán hàng, chi phí nhập hàng, chi phí thu mua và lợi nhuận gộp theo mốc thời gian (Hôm nay, Tuần này, Tháng này, Năm nay hoặc khoảng ngày tùy chọn).
  - Biểu đồ trực quan hóa doanh thu theo thời gian.
  - Bảng xếp hạng Top sản phẩm bán chạy nhất và danh sách hàng tồn kho lâu/tồn ít.
  - Thống kê hiệu suất bán hàng theo từng nhân viên.
  - Xuất toàn bộ số liệu thống kê ra file Excel.

---

## IV. CÁC PHÂN HỆ ĐẶC QUYỀN QUẢN TRỊ (CHỈ DÀNH CHO ADMIN)

### 10. Quản lý Nhân viên (`FrmNhanVien`)
- **Đặc quyền**: **Chỉ `ADMIN`**
- **Nghiệp vụ**:
  - Quản lý toàn bộ danh sách nhân sự công ty: Mã nhân viên, Họ tên, Ngày sinh, Giới tính, Số điện thoại, Email, Địa chỉ, Chức vụ, Lương cơ bản, Ngày vào làm, Trạng thái (Đang làm việc / Đã nghỉ việc).
  - Thêm nhân viên mới, điều chỉnh thông tin nhân sự hoặc cho thôi việc.
  - Xuất danh sách nhân viên ra Excel.

### 11. Quản lý Tài khoản & Phân quyền (`FrmTaiKhoan`)
- **Đặc quyền**: **Chỉ `ADMIN`**
- **Nghiệp vụ**:
  - Tạo tài khoản đăng nhập hệ thống mới và liên kết với một hồ sơ nhân viên cụ thể.
  - **Phân quyền vai trò**: Thiết lập tài khoản là `ADMIN` (Quản trị viên) hoặc `NHANVIEN` (Nhân viên).
  - Khóa tài khoản tạm thời hoặc kích hoạt lại tài khoản.
  - Đặt lại mật khẩu (Reset password) cho nhân viên khi bị quên mật khẩu.

### 12. Quản lý Danh mục Sản phẩm (`FrmDanhMuc`)
- **Đặc quyền**: **Chỉ `ADMIN`**
- **Nghiệp vụ**:
  - Quản lý phân loại ngành hàng trang sức: Nhẫn cưới, Nhẫn kim tiền, Dây chuyền, Bông tai, Vòng tay phong thủy, Lắc tay, Mặt dây chuyền, Kiềng cổ...
  - Thêm mới, chỉnh sửa tên/mô tả và trạng thái hoạt động của danh mục.

### 13. Quản lý Chất liệu & Bảng giá tham khảo (`FrmChatLieu`)
- **Đặc quyền**: **Chỉ `ADMIN`**
- **Nghiệp vụ**:
  - Quản lý danh mục nguyên liệu kim hoàn: Vàng 24K (9999), Vàng 18K (750), Vàng 14K (585), Vàng 10K (416), Bạc 925, Bạch kim (Platinum), Kim cương, Đá quý...
  - **Cập nhật Bảng giá Vàng/Chất liệu niêm yết hàng ngày**: Giá mua vào và Giá bán ra trên từng đơn vị tính (Chỉ, Lượng, Gram, Carat).
  - *Lưu ý*: Giá bán ra của chất liệu là căn cứ để công thức hệ thống tự động tính giá bán lẻ trang sức tại quầy.

### 14. Quản lý Nhà cung cấp (`FrmNhaCungCap`)
- **Đặc quyền**: **Chỉ `ADMIN`**
- **Nghiệp vụ**:
  - Quản lý danh bạ đối tác cung ứng vàng thô, phôi trang sức, đá quý và phụ kiện.
  - Thông tin lưu trữ: Mã NCC, Tên công ty, Người liên hệ, Điện thoại, Email, Địa chỉ, Mã số thuế, Ghi chú hợp tác.

### 15. Sao lưu & Phục hồi Cơ sở dữ liệu (`FrmSaoLuuPhucHoi`)
- **Đặc quyền**: **Chỉ `ADMIN`**
- **Nghiệp vụ**:
  - **Backup Database**: Tạo bản sao lưu an toàn toàn bộ dữ liệu hệ thống ra tệp tin định dạng SQL Backup (`.bak`) với vị trí lưu trữ tùy chọn.
  - **Restore Database**: Khôi phục cơ sở dữ liệu từ file backup `.bak` trong các trường hợp gặp sự cố máy chủ hoặc chuyển đổi thiết bị.
