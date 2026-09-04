# HỆ THỐNG QUẢN LÝ CỬA HÀNG ĐÁ QUÝ PNJ (ENTERPRISE EDITION v2.0)
## TÀI LIỆU KIẾN TRÚC & ĐẶC TẢ NGHIỆP VỤ HỆ THỐNG

> **Lưu ý:** Đây là phiên bản tóm lược kiến trúc của dự án. Tài liệu bàn giao chi tiết và đầy đủ nhất được lưu trữ tại:  
> 🔗 [`docs/HANDOVER_SPECIFICATION.md`](docs/HANDOVER_SPECIFICATION.md)

---

### 1. THÔNG TIN CHUNG
- **Tên dự án:** Hệ thống Quản trị Chuỗi Cửa hàng Đá quý & Kim hoàn PNJ
- **Nền tảng:** Windows Forms (.NET Framework 4.7.2), Ngôn ngữ C#
- **Cơ sở dữ liệu:** Microsoft SQL Server (17 bảng nghiệp vụ chuẩn 3NF)
- **Công nghệ ORM:** Entity Framework 6 (Database First / EDMX)
- **Giao diện:** Guna UI2 Enterprise Suite & Responsive Layout
- **Trạng thái:** Hoàn thiện 100%, Đạt 90/100 điểm Rubric môn học (10 điểm còn lại là quyển báo cáo Word)

---

### 2. SƠ ĐỒ PHÂN HỆ NGHIỆP VỤ
1. **Xác thực & Bảo mật:** Đăng nhập, băm mật khẩu BCrypt (Factor 11), kiểm tra trạng thái công tác, bắt buộc đổi mật khẩu sau reset.
2. **Điểm bán hàng (POS Terminal):** Giao diện 2 cột hiện đại, giỏ hàng, tra cứu sản phẩm, quét mã QR, chiết khấu, trừ kho tức thời và in hóa đơn.
3. **Quản lý Hóa đơn:** Tra cứu hóa đơn đa tiêu chí, xem chi tiết, hủy hóa đơn có rollback tồn kho, in lại hóa đơn.
4. **Quản lý Khách hàng (CRM):** Quản lý hồ sơ, số điện thoại, tích lũy điểm thưởng và lịch sử mua sắm.
5. **Quản lý Sản phẩm & Định mức (BOM):** CRUD sản phẩm, định mức thành phần vàng/đá quý, sinh & quét mã QR, nén ảnh tự động Bicubic 500x500px, Drag & Drop.
6. **Quản lý Nhập hàng:** Lập phiếu nhập kho từ nhà cung cấp, cập nhật giá vốn và tồn kho.
7. **Thu mua Kim hoàn cũ:** Thu mua vàng bạc đá quý cũ theo trọng lượng, nạp batch dữ liệu lớn từ file Excel (.xlsx).
8. **Dịch vụ Bảo hành:** Tiếp nhận bảo hành theo hóa đơn gốc, đối chiếu hạn bảo hành, quản lý tiến độ xử lý và in phiếu hẹn.
9. **Tiếp thị Email:** Động cơ SMTP gửi thư hàng loạt, quản lý mẫu thư điện tử, đính kèm tệp và ghi nhật ký gửi thư.
10. **Danh mục & Chất liệu:** Phân nhóm trang sức và bảng giá tham chiếu các loại Vàng 24K, 18K, 14K, Bạch kim, Bạc Ý, Kim cương, Ruby,...
11. **Quản lý Nhà cung cấp:** Danh bạ đối tác cung ứng vàng bạc đá quý.
12. **Quản trị Nhân sự & Phân quyền:** Quản lý nhân viên, phân quyền Quản trị (`ADMIN`) và Thu ngân (`NHANVIEN`).
13. **Báo cáo & Thống kê (BI):** Dashboard trực quan, đồ thị Guna Chart doanh thu, top sản phẩm bán chạy, xuất báo cáo ra Excel.
14. **Quản trị CSDL:** Sao lưu & phục hồi CSDL SQL Server với cơ chế fallback tự động thích ứng.

---

### 3. CÁC TÀI LIỆU LIÊN QUAN
- [Tài liệu Bàn giao Toàn diện](docs/HANDOVER_SPECIFICATION.md)
- [Cẩm nang Đóng gói & Bộ cài đặt](Packaging/README_PORTABLE.txt)
- [Bảng điểm Rubric Đánh giá](RUBRIC%20CHAM%20DIEM%20-%20THAM%20KHAO.xlsx)
- [Tài liệu cũ lưu trữ](docs/legacy/)