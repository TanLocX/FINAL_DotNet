# TÀI LIỆU CHỨC NĂNG VÀ PHÂN QUYỀN HIỆN CÓ CỦA PNJ MANAGER

> Phạm vi tài liệu: mô tả theo mã nguồn hiện tại. Những chức năng chưa được cài đặt không được xem là chức năng của sản phẩm.

---

## I. Phân quyền hiện tại

Hệ thống có hai vai trò cố định:

1. **Quản trị viên (`ADMIN`)**: sử dụng các nghiệp vụ chung và các form quản trị.
2. **Nhân viên (`NHANVIEN`)**: sử dụng các nghiệp vụ bán hàng, hóa đơn, khách hàng, sản phẩm, nhập hàng, bảo hành, email, thống kê và phần tra cứu/xuất dữ liệu thu mua.

Nhóm menu quản trị được ẩn với `NHANVIEN`. Các form quản trị cũng kiểm tra lại vai trò khi mở, gồm:

- `FrmNhanVien`
- `FrmTaiKhoan`
- `FrmDanhMuc`
- `FrmChatLieu`
- `FrmNhaCungCap`
- `FrmSaoLuuPhucHoi`

Phân quyền hiện tại chỉ theo hai vai trò trên, chưa tách quyền chi tiết theo từng thao tác như xem, thêm, sửa, xóa hoặc in.

---

## II. Ma trận phân quyền

| Nhóm | Form/chức năng | `NHANVIEN` | `ADMIN` | Phạm vi thực tế |
|---|---|:---:|:---:|---|
| Chung | `FrmMain` | Có | Có | Mở các form con và đăng xuất |
| Kinh doanh | `FrmBanHang` | Có | Có | Lập hóa đơn từ khách hàng và sản phẩm có sẵn |
| Kinh doanh | `FrmHoaDon` | Có | Có | Tìm, xem chi tiết, hủy hóa đơn và xem Report |
| Kinh doanh | `FrmKhachHang` | Có | Có | Thêm, sửa, tìm kiếm và đổi trạng thái khách hàng |
| Hàng hóa | `FrmSanPham` | Có | Có | Quản lý sản phẩm, thành phần chất liệu, ảnh và QR sản phẩm |
| Hàng hóa | `FrmNhapHang` | Có | Có | Lập/hủy phiếu nhập, cập nhật kho và xem Report |
| Thu mua | `FrmThuMua` | Tra cứu/xuất | Đầy đủ | Nhân viên không được chọn, kiểm tra hoặc import file; Admin được import Excel |
| Dịch vụ | `FrmBaoHanh` | Có | Có | Tiếp nhận, cập nhật trạng thái và xem Report bảo hành |
| Vận hành | `FrmQuanLyEmail` | Có | Có | Cấu hình SMTP, mẫu email, gửi mail và xem nhật ký |
| Thống kê | `FrmThongKe` | Có | Có | Thống kê bán/nhập/bảo hành/email/tồn kho và xuất Excel |
| Quản trị | `FrmNhanVien` | Không | Có | Quản lý hồ sơ và trạng thái làm việc |
| Quản trị | `FrmTaiKhoan` | Không | Có | Tạo/cập nhật/khóa tài khoản, gán vai trò và reset mật khẩu |
| Quản trị | `FrmDanhMuc` | Không | Có | Quản lý danh mục sản phẩm |
| Quản trị | `FrmChatLieu` | Không | Có | Quản lý chất liệu và giá mua/bán tham khảo |
| Quản trị | `FrmNhaCungCap` | Không | Có | Quản lý nhà cung cấp |
| Quản trị | `FrmSaoLuuPhucHoi` | Không | Có | Backup/Restore SQL Server bằng file `.bak` |
| Đăng nhập | `FormDoiMatKhau` | Khi bị yêu cầu | Khi bị yêu cầu | Xuất hiện sau đăng nhập nếu tài khoản có `PhaiDoiMatKhau = 1` |

Không có menu đổi mật khẩu chủ động cho người dùng trong `FrmMain`. Form đổi mật khẩu hiện phục vụ luồng bắt buộc đổi mật khẩu, thường sau khi Admin reset.

---

## III. Chức năng theo từng phân hệ

### 1. Đăng nhập và tài khoản

- `Form1` kiểm tra tên đăng nhập, mật khẩu BCrypt, trạng thái tài khoản và trạng thái làm việc của nhân viên.
- Chỉ tài khoản đang hoạt động và nhân viên đang làm việc mới đăng nhập được.
- Nút đăng ký công khai được ẩn; tài khoản do Admin tạo và liên kết với nhân viên.
- Vai trò tài khoản là `ADMIN` hoặc `NHANVIEN`.
- Admin có thể reset mật khẩu. Sau reset, cờ `PhaiDoiMatKhau` buộc người dùng đổi mật khẩu ở lần đăng nhập tiếp theo.
- Không có chức năng người dùng tự yêu cầu reset mật khẩu qua email/OTP.

### 2. Bán hàng (`FrmBanHang`)

- Chọn khách hàng có sẵn bằng ComboBox.
- Chọn sản phẩm đang kinh doanh và còn tồn bằng ComboBox.
- Nhập số lượng, ngày hết hạn bảo hành và thêm sản phẩm vào giỏ hàng.
- Đơn giá lấy từ `SanPham.GiaBan` hiện tại.
- Giảm giá là **số tiền cố định**, không phải phần trăm.
- Chọn phương thức thanh toán, lưu hóa đơn và chi tiết hóa đơn trong transaction.
- Khi hoàn tất, hệ thống kiểm tra tồn kho và trừ kho; nếu lỗi thì rollback.

Form này hiện **không** tìm/quét QR hoặc Barcode, không thêm nhanh khách hàng và không tự tính giá bán từ chất liệu, tiền công hay tỷ lệ lợi nhuận.

### 3. Hóa đơn (`FrmHoaDon`)

- Lọc theo từ khóa, khoảng ngày, trạng thái và khoảng tiền.
- Từ khóa có thể đối chiếu các thông tin như mã hóa đơn, khách hàng, nhân viên hoặc sản phẩm.
- Xem danh sách chi tiết sản phẩm, số lượng, đơn giá và hạn bảo hành.
- Hủy hóa đơn đã thanh toán và hoàn lại tồn kho trong transaction.
- Xem/in hóa đơn bằng RDLC/ReportViewer.

### 4. Khách hàng (`FrmKhachHang`)

- Quản lý họ tên, số điện thoại, email, địa chỉ, ngày sinh, điểm tích lũy, quyền nhận email và trạng thái hoạt động.
- Thêm, cập nhật, tìm kiếm theo từ khóa/trạng thái/quyền nhận email và bật/tắt trạng thái.
- Có kiểm tra dữ liệu đầu vào và trùng số điện thoại.

Form hiện không có giới tính, màn hình lịch sử mua hàng, tính tổng chi tiêu/phân hạng VIP hoặc nút xuất Excel khách hàng.

### 5. Sản phẩm (`FrmSanPham`)

- Quản lý tên sản phẩm, danh mục, giá vốn, giá bán, số lượng tồn, hình ảnh và trạng thái kinh doanh.
- Quản lý nhiều thành phần chất liệu của một sản phẩm, gồm chất liệu, trọng lượng và đơn vị.
- Tìm kiếm/lọc theo mã hoặc tên, danh mục, chất liệu, khoảng giá, tồn kho và trạng thái.
- Sinh QR chứa mã hiển thị dạng `SP000001`, lưu QR thành ảnh PNG và đọc ảnh QR để chọn lại sản phẩm.

Mã QR hiện dùng để nhận diện sản phẩm trong chính form sản phẩm. Không có trường Barcode riêng, không chứa toàn bộ thông tin sản phẩm trong QR và không có nhập/xuất Excel ngay tại form này. Giá bán được nhập trực tiếp, chưa tự tính từ bảng giá chất liệu.

### 6. Nhập hàng (`FrmNhapHang`)

- Chọn nhà cung cấp và các sản phẩm có sẵn, nhập số lượng và đơn giá nhập.
- Lưu phiếu nhập/chi tiết, cộng tồn kho và cập nhật giá vốn trong transaction.
- Tra cứu lịch sử theo các tiêu chí trên form.
- Hủy phiếu hoàn thành khi tồn kho đủ để hoàn tác; hệ thống trừ lại kho và khôi phục giá vốn phù hợp.
- Xem/in phiếu nhập bằng RDLC/ReportViewer.

### 7. Thu mua từ Excel (`FrmThuMua`)

- Tải file mẫu `.xlsx`.
- Admin chọn file, xem trước/kiểm tra lỗi và import dữ liệu thu mua hợp lệ.
- Import kiểm tra cấu trúc cột, dữ liệu tham chiếu, số liệu, trạng thái và mã nguồn trùng; dữ liệu được lưu bằng transaction `Serializable`.
- Tra cứu dữ liệu đã import theo từ khóa, khoảng ngày và trạng thái.
- Hiển thị số phiếu, tổng trọng lượng, tổng tiền và số khách hàng của dữ liệu hoàn thành theo bộ lọc.
- Xuất danh sách đang lọc ra Excel và xem Report của phiếu được chọn.
- `NHANVIEN` có thể tải mẫu, tra cứu, thống kê, xuất Excel và xem Report nhưng không được chọn/kiểm tra/import file.

Form hiện không có chức năng nhập tay/lập phiếu thu mua hoặc tự định giá từ chất liệu và bảng giá.

### 8. Bảo hành (`FrmBaoHanh`)

- Tìm hóa đơn/sản phẩm đã bán và kiểm tra hạn bảo hành.
- Tiếp nhận yêu cầu, ghi tình trạng, ghi chú, ngày hẹn trả và ngày trả thực tế.
- Cập nhật theo luồng `TIEP_NHAN` → `DANG_XU_LY` → `HOAN_THANH` → `DA_TRA`.
- Tìm kiếm/lọc danh sách và xem/in phiếu bảo hành bằng RDLC/ReportViewer.

Form hiện không có trường hoặc nghiệp vụ ghi nhận chi phí bảo hành.

### 9. Email (`FrmQuanLyEmail`)

- Lưu cấu hình SMTP vào biến môi trường của người dùng máy tính, không lưu mật khẩu SMTP vào CSDL.
- Quản lý mẫu email, gửi đơn lẻ hoặc hàng loạt và ghi nhật ký thành công/thất bại.
- Các token được mã nguồn hỗ trợ: `HoTen`, `TenSanPham`, `Sdt`, `Email`, `TongTien`, `ThanhTien`, `NgayMua`, `HanBaoHanh`, `MaHoaDon`, `GhiChu`. Có thể viết dạng `{Token}` hoặc `{{Token}}`.
- Có hẹn thời điểm gửi bằng `System.Windows.Forms.Timer`.

Lịch hẹn chỉ tồn tại trong bộ nhớ của form, không được lưu vào CSDL và không phải tác vụ nền. Muốn gửi đúng hẹn thì form và ứng dụng phải còn mở; đóng form/ứng dụng sẽ mất lịch đang chờ.

### 10. Báo cáo và thống kê (`FrmThongKe`)

- Doanh thu, số hóa đơn và giá trị trung bình hóa đơn trong khoảng ngày.
- Doanh thu theo thời gian, sản phẩm, danh mục, chất liệu và nhân viên.
- Danh sách sản phẩm tồn thấp và thư viện ảnh sản phẩm.
- Tổng nhập theo nhà cung cấp/tháng và số lượng nhập theo sản phẩm.
- Bảo hành theo trạng thái, các mục sắp hết hạn.
- Kết quả gửi email theo trạng thái và mẫu email.
- Xuất Excel: sản phẩm, hóa đơn đã thanh toán, phiếu nhập hoàn thành, bảo hành và nhật ký email.

Form hiện không tính chi phí thu mua hoặc lợi nhuận gộp. Thống kê thu mua nằm riêng trong `FrmThuMua`, không nằm trong `FrmThongKe`.

---

## IV. Chức năng quản trị

### 11. Nhân viên (`FrmNhanVien`)

- Quản lý họ tên, giới tính, ngày sinh, số điện thoại, email, địa chỉ, chức vụ và trạng thái làm việc.
- Thêm, cập nhật, tìm kiếm và đổi trạng thái làm việc.
- Không có trường lương, phòng ban, ngày vào làm hoặc xuất Excel tại form này.

### 12. Tài khoản (`FrmTaiKhoan`)

- Tạo tài khoản và liên kết với nhân viên.
- Cập nhật tên đăng nhập, vai trò, trạng thái và cờ bắt buộc đổi mật khẩu.
- Khóa/kích hoạt tài khoản và reset mật khẩu.
- Không xóa vật lý tài khoản trong nghiệp vụ hiện tại.

### 13. Danh mục (`FrmDanhMuc`)

- Quản lý tên, mô tả và trạng thái hoạt động của danh mục sản phẩm.
- Thêm, cập nhật, tìm kiếm và đổi trạng thái.

### 14. Chất liệu (`FrmChatLieu`)

- Quản lý tên chất liệu, giá mua vào, giá bán ra tham khảo và trạng thái hoạt động.
- Giá này chưa được dùng để tự động tính giá bán sản phẩm tại quầy.

### 15. Nhà cung cấp (`FrmNhaCungCap`)

- Quản lý tên, người liên hệ, điện thoại, email, địa chỉ và trạng thái hoạt động.
- Không có trường mã số thuế hoặc ghi chú hợp tác trong model hiện tại.

### 16. Sao lưu và phục hồi (`FrmSaoLuuPhucHoi`)

- Tạo SQL Server backup dạng `.bak`, hỗ trợ copy-only, checksum và tùy chọn nén.
- Kiểm tra file backup và phục hồi CSDL từ `.bak`.
- Đây là chức năng Backup/Restore thực tế, khác với việc chỉ nộp sẵn một file `.bak`.

---

## V. Giới hạn cần trình bày đúng khi báo cáo

- Chỉ có hai vai trò, chưa có phân quyền chi tiết theo chức năng.
- QR chỉ có ở form sản phẩm; bán hàng chưa quét QR/Barcode.
- Chỉ phân hệ thu mua đọc Excel; các file Excel khác được xuất từ thống kê hoặc form thu mua.
- Chưa có lợi nhuận gộp, tự động định giá thu mua hoặc tự động tính giá bán theo chất liệu.
- Hẹn gửi email phụ thuộc form đang mở và không được lưu lại.
- Không có đăng ký công khai, quên mật khẩu qua email/OTP hoặc menu tự đổi mật khẩu.
- Xóa dữ liệu ở nhiều danh mục được thể hiện bằng đổi trạng thái thay vì xóa vật lý.
