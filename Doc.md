# TÀI LIỆU ÔN TẬP VÀ THUYẾT TRÌNH TOÀN DIỆN HỆ THỐNG PNJ MANAGER
## Hướng Dẫn Nghiên Cứu Full-Stack, Giải Thích Mã Nguồn & Kịch Bản Phản Biện Đồ Án

> **Tài liệu phục vụ:** Ôn tập kiến thức kiến trúc, hiểu sâu mã nguồn, chuẩn bị nội dung slide thuyết trình và trả lời câu hỏi vấn đáp khi bảo vệ đồ án môn học Lập trình .NET.  
> **Phiên bản hệ thống:** 2.0 (Enterprise Refactored)  
> **Ngôn ngữ & Nền tảng:** C# (.NET Framework 4.7.2), Windows Forms, Entity Framework 6, Microsoft SQL Server.  

---

## MỤC LỤC

1. [Bài toán nghiệp vụ và bối cảnh ngành kim hoàn](#1-bài-toán-nghiệp-vụ-và-bối-cảnh-ngành-kim-hoàn)
2. [Kiến trúc Full-Stack và luồng dữ liệu (Data Flow)](#2-kiến-trúc-full-stack-và-luồng-dữ-liệu-data-flow)
3. [Thiết kế Cơ sở Dữ liệu 17 Bảng và các quyết định kiến trúc](#3-thiết-kế-cơ-sở-dữ-liệu-17-bảng-và-các-quyết-định-kiến-trúc)
4. [Bóc tách mã nguồn và giải thích chi tiết các module cốt lõi](#4-bóc-tách-mã-nguồn-và-giải-thích-chi-tiết-các-module-cốt-lõi)
   - 4.1. Phân hệ Bán hàng POS và Quản lý Transaction (`FrmBanHang.cs`, `PosService.cs`)
   - 4.2. Bảo mật, Băm mật khẩu BCrypt và Quản lý Phiên (`Form1.cs`, `CurrentUserSession.cs`)
   - 4.3. Pipeline nén ảnh nội suy Bicubic và Kéo thả (`ImageOptimizationHelper.cs`, `FrmSanPham.cs`)
   - 4.4. Động cơ Sao lưu & Phục hồi CSDL tự thích ứng (`SaoLuuPhucHoiService.cs`, `FrmSaoLuuPhucHoi.cs`)
   - 4.5. Động cơ Mã QR và Mã vạch (`QrCodeService.cs`)
   - 4.6. Xử lý xuất/nhập Excel với OpenXML (`XlsxImportService.cs`, `XlsxExportService.cs`)
5. [Bộ câu hỏi phản biện thường gặp khi bảo vệ đồ án (Defense FAQ)](#5-bộ-câu-hỏi-phản-biện-thường-gặp-khi-bảo-vệ-đồ-án-defense-faq)
6. [Kịch bản Demo chuẩn 5 phút trước hội đồng](#6-kịch-bản-demo-chuẩn-5-phút-trước-hội-đồng)

---

## 1. BÀI TOÁN NGHIỆP VỤ VÀ BỐI CẢNH NGÀNH KIM HOÀN

### 1.1. Sự khác biệt giữa bán lẻ trang sức và bán lẻ thông thường
Nhiều sinh viên nhầm lẫn giữa hệ thống bán hàng tạp hóa (siêu thị, cửa hàng tiện lợi) và bán lẻ trang sức quý. Trong ngành kim hoàn:
1. **Định mức chất liệu phức tạp (Bill of Materials):** Một chiếc nhẫn kim cương không chỉ là một mã hàng đơn lẻ, mà giá trị cấu thành từ trọng lượng vàng (chỉ, lượng, gram) cộng với trọng lượng và nước của viên đá chủ/đá tấm (carat).
2. **Biến động giá và nghiệp vụ thu mua lại (Buyback):** Khách hàng có nhu cầu bán lại vàng cũ, trang sức đã qua sử dụng. Giá thu mua phụ thuộc vào tuổi vàng (24K, 18K, 14K) và giá thị trường tại thời điểm giao dịch. Do đó hệ thống phải có phân hệ **Thu mua kim hoàn** riêng biệt với bảng giá tham chiếu thị trường.
3. **Bảo hành dài hạn và định kỳ:** Trang sức có giá trị cao đòi hỏi làm mới, gắn lại đá rơi, đánh bóng định kỳ. Hệ thống phải liên kết chặt chẽ từng dòng sản phẩm bán ra (`ChiTietHoaDon`) với thời hạn bảo hành (`HanBaoHanh`) và lịch sử các lần tiếp nhận bảo hành (`PhieuBaoHanh`).
4. **Mã định danh duy nhất (QR Code):** Nhãn trang sức nhỏ, khó dán mã vạch 1D truyền thống dạng dài. Mã QR 2D hình vuông nhỏ gọn là giải pháp chuẩn công nghiệp.

---

## 2. KIẾN TRÚC FULL-STACK VÀ LUỒNG DỮ LIỆU (DATA FLOW)

### 2.1. Sơ đồ kiến trúc 4 tầng

```
[ Tầng 1: Giao diện (Presentation Layer) ]
  - Giao diện người dùng: Windows Forms (.NET Framework 4.7.2) kết hợp Guna.UI2.
  - Shell chính: FrmMain (điều hướng menu, kiểm tra quyền, gắn form con vào pnlNoiDung).
  - Các màn hình nghiệp vụ: FrmBanHang, FrmSanPham, FrmHoaDon, FrmThuMua,...
         │
         ▼ (Gọi hàm nghiệp vụ / DTO)
[ Tầng 2: Dịch vụ Nghiệp vụ (Business & Service Layer) ]
  - PosService.cs: Xử lý giỏ hàng, trừ tồn kho, tính tiền, mở Database Transaction.
  - ImageOptimizationHelper.cs: Thuật toán nén ảnh nội suy Bicubic, đồng bộ thư mục.
  - SaoLuuPhucHoiService.cs: Thực thi T-SQL Backup/Restore, fallback compression.
  - QrCodeService.cs: Sinh mã QR và giải mã hình ảnh qua ZXing.Net.
  - Xlsx Services: Đọc/ghi file Excel chuẩn Office Open XML (System.IO.Compression & System.Xml).
         │
         ▼ (LINQ to Entities / Entity Object)
[ Tầng 3: Truy cập Dữ liệu (Data Access Layer - ORM) ]
  - Entity Framework 6 (Database First qua Model1.edmx).
  - Lớp DbContext trung tâm: QL_CuaHangDaQuy_PNJEntities.
  - DatabaseConnection.cs: Quản lý chuỗi kết nối an toàn.
         │
         ▼ (T-SQL Queries / Connection Pool)
[ Tầng 4: Cơ sở Dữ liệu (Database Storage) ]
  - Microsoft SQL Server (LocalDB hoặc SQL Server Express).
  - Tên CSDL: QL_CuaHangDaQuy_PNJ (17 bảng chuẩn 3NF).
```

### 2.2. Vòng đời của DbContext (Context Lifecycle)
Trong lập trình Windows Forms với Entity Framework, một lỗi phổ biến của người mới bắt đầu là khởi tạo một biến `DbContext` duy nhất dùng chung cho toàn bộ ứng dụng hoặc cho cả vòng đời của Form:
```csharp
// CÁCH LÀM SAI (Anti-pattern):
public partial class FrmSanPham : Form
{
    private QL_CuaHangDaQuy_PNJEntities db = new QL_CuaHangDaQuy_PNJEntities();
    // Gây ra: Dữ liệu bị cache cũ, xung đột khi chạy đa luồng, rò rỉ bộ nhớ (memory leak).
}
```

Hệ thống PNJ Manager áp dụng nguyên tắc **Short-lived Context (Unit of Work)**: Mỗi thao tác (Tải danh sách, Thêm mới, Cập nhật, Xóa) mở một DbContext riêng biệt trong khối `using`:
```csharp
// CÁCH LÀM ĐÚNG (Áp dụng xuyên suốt trong hệ thống):
using (var db = DatabaseConnection.CreateContext())
{
    var sanPham = db.SanPhams.Find(id);
    // Thao tác nghiệp vụ...
    db.SaveChanges();
} // DbContext được Dispose ngay lập tức, giải phóng kết nối về Connection Pool.
```
**Lợi ích khi trả lời câu hỏi hội đồng:**
- Đảm bảo dữ liệu luôn mới nhất từ CSDL, không bị hiện tượng đọc dữ liệu cũ lưu trong bộ nhớ tạm (Stale Data).
- Giải phóng kết nối SQL ngay khi kết thúc truy vấn, giúp ứng dụng nhẹ và không chiếm dụng tài nguyên máy chủ.

### 2.3. Luồng dữ liệu nghiệp vụ: Từ click chuột trên UI xuống CSDL
Khi thu ngân nhấn nút **Thanh toán (`F9`)** trên màn hình Bán hàng:
1. **Validation tại Form:** `FrmBanHang` kiểm tra giỏ hàng có rỗng không, số lượng mua có vượt tồn kho không.
2. **Đóng gói DTO:** Dữ liệu được đóng gói thành các đối tượng không phụ thuộc Entity Framework: danh sách `CartItem`, `khachHangId`, `giamGiaPhanTram`, `phuongThucThanhToan`.
3. **Gọi Service Layer:** `PosService.XuLyThanhToan(...)` nhận dữ liệu.
4. **Mở Transaction:** `using (var transaction = db.Database.BeginTransaction())` được kích hoạt.
5. **Ghi CSDL tuần tự:**
   - Tạo bản ghi `HoaDon` mới, tính `TongTien`, `GiamGia`, `ThanhTien`.
   - Vòng lặp duyệt từng sản phẩm trong giỏ: Trừ `SoLuongTon` trong bảng `SanPham`; Tạo bản ghi `ChiTietHoaDon` kèm hạn bảo hành tự động `DateTime.Now.AddMonths(12)`.
   - Gọi `db.SaveChanges()`.
6. **Commit:** `transaction.Commit()` xác nhận toàn bộ thay đổi thành công vĩnh viễn. Nếu có bất kỳ lỗi nào (ví dụ mất mạng, lỗi khóa ngoại), toàn bộ thay đổi được Rollback tự động, kho hàng không bị trừ sai lệch.
7. **Phản hồi UI:** Trả về mã hóa đơn `HD000001`, xóa trắng giỏ hàng, hiển thị thông báo thành công và gọi màn hình in hóa đơn.

---

## 3. THIẾT KẾ CƠ SỞ DỮ LIỆU 17 BẢNG VÀ CÁC QUYẾT ĐỊNH KIẾN TRÚC

### 3.1. Danh mục 17 bảng dữ liệu và vai trò
Hệ thống gồm 17 bảng phân theo 5 nhóm nghiệp vụ rõ ràng:
1. **Nhóm Xác thực & Nhân sự:** `NhanVien`, `TaiKhoan`.
2. **Nhóm Danh mục & Định mức:** `DanhMuc`, `ChatLieu`, `SanPham`, `ChiTietChatLieu`, `NhaCungCap`.
3. **Nhóm Bán hàng & Dịch vụ:** `KhachHang`, `HoaDon`, `ChiTietHoaDon`, `PhieuBaoHanh`.
4. **Nhóm Kho & Thu mua:** `PhieuNhap`, `ChiTietPhieuNhap`, `PhieuThuMua`, `ChiTietPhieuThuMua`.
5. **Nhóm Tiếp thị & Vận hành:** `MauEmail`, `NhatKyGuiEmail`.

### 3.2. Ba quyết định thiết kế CSDL quan trọng nhất cần ghi nhớ

#### Quyết định 1: Tại sao tách riêng `NhanVien` và `TaiKhoan`?
- `NhanVien` đại diện cho con người ngoài đời thực (Họ tên, ngày sinh, số điện thoại, trạng thái đang làm việc hay đã nghỉ).
- `TaiKhoan` đại diện cho quyền truy cập phần mềm (Tên đăng nhập, mật khẩu băm, vai trò ADMIN/NHANVIEN, cờ khóa tài khoản).
- **Lý do kỹ thuật:** Khi một nhân viên nghỉ việc, ta chỉ cần vô hiệu hóa tài khoản (`DangHoatDong = false`) hoặc xóa tài khoản. Tất cả hóa đơn, phiếu nhập kho, phiếu bảo hành mà nhân viên này từng lập trong quá khứ vẫn giữ nguyên vẹn khóa ngoại tham chiếu đến `NhanVienId`. Nếu gộp hai bảng làm một, việc xóa nhân viên sẽ làm hỏng lịch sử kế toán.

#### Quyết định 2: Bảng trung gian `ChiTietChatLieu` giải quyết bài toán gì?
- Đây là mối quan hệ Nhiều - Nhiều (N - N) giữa `SanPham` và `ChatLieu`.
- Một món trang sức có thể gồm nhiều chất liệu: Vàng 18K (3.5g) và Kim cương (0.5 ct). Ngược lại, chất liệu Vàng 18K có mặt ở hàng trăm sản phẩm khác nhau.
- Bảng trung gian `ChiTietChatLieu` lưu thêm hai thuộc tính bổ sung quan trọng: `TrongLuong` và `DonViTinh` (gram, chỉ, carat).

#### Quyết định 3: Chiến lược Soft Delete (Xóa mềm) vs Hard Delete (Xóa cứng)
- Trong các bảng danh mục (`SanPham`, `KhachHang`, `DanhMuc`, `ChatLieu`, `NhaCungCap`), nếu người dùng nhấn nút Xóa:
  - **Trường hợp 1 (Xóa cứng):** Nếu bản ghi đó **chưa từng phát sinh giao dịch** (chưa từng xuất hiện trong hóa đơn, phiếu nhập, phiếu thu mua), hệ thống cho phép `db.SanPhams.Remove(...)` xóa vĩnh viễn khỏi CSDL.
  - **Trường hợp 2 (Xóa mềm):** Nếu bản ghi đó **đã phát sinh lịch sử giao dịch**, CSDL có ràng buộc khóa ngoại (Foreign Key Constraint). Việc xóa cứng sẽ văng lỗi `DbUpdateException`. Hệ thống tự động chuyển sang cơ chế Xóa mềm bằng cách cập nhật cờ `DangKinhDoanh = false` hoặc `DangHoatDong = false`. Sản phẩm sẽ ẩn khỏi danh sách bán hàng nhưng dữ liệu báo cáo kế toán quá khứ vẫn bảo toàn 100%.

---

## 4. BÓC TÁCH MÃ NGUỒN VÀ GIẢI THÍCH CHI TIẾT CÁC MODULE CỐT LÕI

### 4.1. Phân hệ Bán hàng POS và Quản lý Transaction (`PosService.cs`)

**Đoạn mã cốt lõi xử lý thanh toán và trừ kho an toàn:**
```csharp
public static KetQuaThanhToan XuLyThanhToan(
    int? khachHangId, 
    int nhanVienId, 
    List<DongGioHang> gioHang, 
    decimal giamGiaPhanTram, 
    string phuongThucThanhToan)
{
    using (var db = DatabaseConnection.CreateContext())
    using (var transaction = db.Database.BeginTransaction())
    {
        try
        {
            // 1. Tính toán tổng tiền
            decimal tongTien = gioHang.Sum(i => i.ThanhTien);
            decimal tienGiam = Math.Round(tongTien * (giamGiaPhanTram / 100M), 0);
            decimal thanhTien = tongTien - tienGiam;

            // 2. Tạo bản ghi Hóa đơn
            var hoaDon = new HoaDon
            {
                KhachHangId = khachHangId,
                NhanVienId = nhanVienId,
                NgayLap = DateTime.Now,
                TongTien = tongTien,
                GiamGia = tienGiam,
                ThanhTien = thanhTien,
                PhuongThucThanhToan = phuongThucThanhToan,
                TrangThai = "DA_THANH_TOAN"
            };
            db.HoaDons.Add(hoaDon);
            db.SaveChanges(); // Lấy HoaDonId tự sinh

            // 3. Trừ kho và tạo ChiTietHoaDon kèm Hạn bảo hành
            foreach (var item in gioHang)
            {
                var sanPham = db.SanPhams.Single(sp => sp.SanPhamId == item.SanPhamId);
                if (sanPham.SoLuongTon < item.SoLuong)
                    throw new InvalidOperationException($"Sản phẩm '{sanPham.TenSanPham}' không đủ tồn kho.");

                sanPham.SoLuongTon -= item.SoLuong; // Trừ kho tức thời

                var chiTiet = new ChiTietHoaDon
                {
                    HoaDonId = hoaDon.HoaDonId,
                    SanPhamId = item.SanPhamId,
                    SoLuong = item.SoLuong,
                    DonGiaBan = item.DonGiaBan,
                    ThanhTien = item.ThanhTien,
                    HanBaoHanh = DateTime.Now.AddMonths(12) // Mặc định 1 năm bảo hành
                };
                db.ChiTietHoaDons.Add(chiTiet);
            }

            db.SaveChanges();
            transaction.Commit(); // Hoàn tất giao dịch

            return new KetQuaThanhToan { ThanhCong = true, HoaDonId = hoaDon.HoaDonId };
        }
        catch (Exception ex)
        {
            transaction.Rollback(); // Hoàn nguyên dữ liệu nếu có bất kỳ lỗi nào
            return new KetQuaThanhToan { ThanhCong = false, ThongBaoLoi = ex.Message };
        }
    }
}
```

**Kỹ thuật sửa lỗi rỗng ComboBox trong WinForms:**
Khi liên kết dữ liệu với ComboBox trong C#, nếu gán `DataSource` trước khi gán `DisplayMember`, ComboBox sẽ kích hoạt sự kiện `SelectedIndexChanged` ngay lập tức khi thuộc tính hiển thị chưa được định hình, dẫn đến giá trị hiển thị bị rỗng hoặc gọi sai hàm `ToString()`:
```csharp
// Kỹ thuật gán chuẩn trong FrmBanHang.cs:
cboSanPham.DisplayMember = "DisplayName"; // Gán thuộc tính hiển thị trước
cboSanPham.ValueMember = "Id";             // Gán thuộc tính giá trị trước
cboSanPham.DataSource = danhSachSanPham;  // Cuối cùng mới gán nguồn dữ liệu
```

---

### 4.2. Bảo mật, Băm mật khẩu BCrypt và Quản lý Phiên (`Form1.cs`, `CurrentUserSession.cs`)

#### Thuật toán BCrypt hoạt động như thế nào?
BCrypt là thuật toán băm mật khẩu chuẩn bảo mật quốc tế dựa trên thuật toán mã hóa Blowfish. Khi lưu mật khẩu `PnjDemo@123`:
1. **Sinh Salt ngẫu nhiên:** BCrypt tự tạo chuỗi Salt 128-bit (22 ký tự Base64). Vì vậy, hai tài khoản dù đặt cùng mật khẩu `PnjDemo@123` thì chuỗi băm lưu trong CSDL vẫn hoàn toàn khác biệt.
2. **Work Factor (Cost = 11):** Thuật toán thực hiện $2^{11} = 2048$ vòng lặp tính toán nhào nặn chuỗi. Điều này cố tình tiêu tốn khoảng 50 - 100ms CPU của máy chủ, khiến kẻ tấn công không thể dùng kỹ thuật Brute-force hàng triệu mật khẩu/giây hoặc dùng bảng Rainbow Table tra cứu ngược.
3. **Cấu trúc chuỗi băm 60 ký tự:**
   Ví dụ: `$2a$11$R9h...wQk5Z...`
   - `$2a$`: Phiên bản thuật toán BCrypt.
   - `$11$`: Chỉ số Work Factor ($2^{11}$ vòng).
   - `22 ký tự tiếp theo`: Chính là chuỗi Salt.
   - `31 ký tự cuối`: Kết quả băm thực sự.
4. **Tại sao không cần cột Salt trong CSDL?**
   Khi người dùng đăng nhập, hàm `BCrypt.Verify(inputPassword, hashInDb)` tự động trích xuất Salt và Work Factor từ chính chuỗi hash trong CSDL ra để băm thử nghiệm mật khẩu người dùng vừa nhập, sau đó so sánh kết quả. Do đó CSDL chỉ cần duy nhất 1 cột `MatKhau nvarchar(255)`.

#### Phiên làm việc Singleton (`CurrentUserSession.cs`)
```csharp
public static class CurrentUserSession
{
    public static int TaiKhoanId { get; private set; }
    public static string TenDangNhap { get; private set; }
    public static string VaiTro { get; private set; } // "ADMIN" hoặc "NHANVIEN"
    public static int NhanVienId { get; private set; }
    public static string HoTenNhanVien { get; private set; }

    public static bool DaDangNhap => TaiKhoanId > 0;
    public static bool LaQuanTriVien => string.Equals(VaiTro, "ADMIN", StringComparison.OrdinalIgnoreCase);

    public static void ThietLap(TaiKhoan tk, NhanVien nv)
    {
        TaiKhoanId = tk.TaiKhoanId;
        TenDangNhap = tk.TenDangNhap;
        VaiTro = tk.VaiTro;
        NhanVienId = nv.NhanVienId;
        HoTenNhanVien = nv.HoTen;
    }

    public static void DangXuat()
    {
        TaiKhoanId = 0;
        TenDangNhap = null;
        VaiTro = null;
    }
}
```

---

### 4.3. Pipeline nén ảnh nội suy Bicubic và Kéo thả (`ImageOptimizationHelper.cs`, `FrmSanPham.cs`)

#### Vấn đề gặp phải ban đầu
Dự án gốc chứa các tệp ảnh chụp trang sức và logo kích thước $2048 \times 2048$ và $4096 \times 4096$ nặng tới 36 MB trong thư mục `Resources/`. Hai logo 4K bị nhúng trực tiếp vào tệp nhị phân thông qua `Resources.resx`, khiến tệp thực thi `FINAL_DotNet.exe` phình to lên tới **12.8 MB**. Khi ứng dụng mở form sản phẩm, việc nạp 10 bức ảnh 2K vào các `PictureBox` kích thước chỉ $150 \times 150$ px làm ứng dụng ngốn hơn 400 MB RAM và gây giật lag giao diện.

#### Giải pháp kỹ thuật: Pipeline nén ảnh nội suy
Hệ thống xây dựng lớp tiện ích `ImageOptimizationHelper`:
```csharp
public static Bitmap CreateOptimizedThumbnail(Image sourceImage, int maxDimension = 500)
{
    int originalWidth = sourceImage.Width;
    int originalHeight = sourceImage.Height;

    // Giữ nguyên nếu ảnh đã nhỏ hơn kích thước chuẩn
    if (originalWidth <= maxDimension && originalHeight <= maxDimension)
        return new Bitmap(sourceImage);

    // Tính toán tỷ lệ khung hình (Aspect Ratio)
    float ratio = Math.Min((float)maxDimension / originalWidth, (float)maxDimension / originalHeight);
    int newWidth = Math.Max(1, (int)(originalWidth * ratio));
    int newHeight = Math.Max(1, (int)(originalHeight * ratio));

    var destinationBitmap = new Bitmap(newWidth, newHeight, PixelFormat.Format32bppArgb);
    using (var graphics = Graphics.FromImage(destinationBitmap))
    {
        // Thiết lập bộ lọc nội suy chất lượng cao nhất của GDI+
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

        using (var wrapMode = new ImageAttributes())
        {
            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
            graphics.DrawImage(sourceImage, new Rectangle(0, 0, newWidth, newHeight), 
                               0, 0, originalWidth, originalHeight, GraphicsUnit.Pixel, wrapMode);
        }
    }
    return destinationBitmap;
}
```
**Kết quả đạt được:**
- Thư mục `Resources/` giảm từ **35.97 MB** xuống **5.33 MB** (giảm 85.2%).
- Tệp `FINAL_DotNet.exe` giảm từ **12.78 MB** xuống còn **1.23 MB** (giảm 90.4%).
- Khi hiển thị, hình ảnh trang sức vẫn giữ độ sắc nét viền đá quý nhờ thuật toán Bicubic.

#### Hỗ trợ Kéo - Thả (Drag & Drop) trực tiếp trên `picSanPham`:
```csharp
picSanPham.AllowDrop = true;

picSanPham.DragEnter += (sender, e) => {
    if (e.Data.GetDataPresent(DataFormats.FileDrop))
        e.Effect = DragDropEffects.Copy;
};

picSanPham.DragDrop += (sender, e) => {
    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
    if (files != null && files.Length > 0)
    {
        string relativePath = ImageOptimizationHelper.SaveOptimizedProductImage(files[0], TimThuMucDuAn());
        txtDuongDanAnh.Text = relativePath;
        HienThiAnh(relativePath);
    }
};
```

---

### 4.4. Động cơ Sao lưu & Phục hồi CSDL tự thích ứng (`SaoLuuPhucHoiService.cs`)

#### Cơ chế tự thích ứng nén (Compression Fallback)
Khi thực hiện lệnh sao lưu:
```sql
BACKUP DATABASE [QL_CuaHangDaQuy_PNJ] TO DISK = @path WITH FORMAT, COPY_ONLY, CHECKSUM, COMPRESSION
```
Trên các bản SQL Server Express hoặc LocalDB, tùy chọn `COMPRESSION` bị cấm và SQL Server sẽ trả về lỗi **Msg 1844: "BACKUP DATABASE WITH COMPRESSION is not supported on Express Edition"**.
Hệ thống giải quyết bằng cơ chế bẫy mã lỗi tự động:
```csharp
try
{
    // Thử sao lưu có nén để tiết kiệm đĩa cứng
    ThucThiBackup(db, duongDanFile, true);
}
catch (SqlException ex) when (ex.Number == 1844 || ex.Message.Contains("COMPRESSION"))
{
    // Tự động chuyển về chế độ không nén nếu bản SQL không hỗ trợ
    ThucThiBackup(db, duongDanFile, false);
}
```

#### Cơ chế phục hồi an toàn (Ngắt kết nối độc quyền)
Nếu đang có kết nối khác mở CSDL, lệnh `RESTORE` sẽ bị treo hoặc văng lỗi "Database is in use". Code xử lý bằng cách ép CSDL về chế độ đơn người dùng trước khi khôi phục:
```sql
ALTER DATABASE [QL_CuaHangDaQuy_PNJ] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
RESTORE DATABASE [QL_CuaHangDaQuy_PNJ] FROM DISK = @path WITH REPLACE;
ALTER DATABASE [QL_CuaHangDaQuy_PNJ] SET MULTI_USER;
```

---

### 4.5. Động cơ Mã QR và Mã vạch (`QrCodeService.cs`)
- **Sinh mã QR:** Thư viện `ZXing.Net` (`BarcodeWriter` với định dạng `BarcodeFormat.QR_CODE`) mã hóa chuỗi `SP000001` thành ma trận nhị phân và vẽ thành đối tượng `Bitmap`:
```csharp
var writer = new BarcodeWriter
{
    Format = BarcodeFormat.QR_CODE,
    Options = new QrCodeEncodingOptions
    {
        Width = kichThuoc,
        Height = kichThuoc,
        Margin = 2,
        CharacterSet = "UTF-8",
        ErrorCorrection = ErrorCorrectionLevel.M
    }
};
return writer.Write(noiDung.Trim());
```
- **Đọc mã QR:** Sử dụng `ZXing.BarcodeReader` để quét và trích xuất chuỗi mã sản phẩm từ hình ảnh:
```csharp
public static string DocMaQr(Bitmap bitmap)
{
    var reader = new BarcodeReader
    {
        AutoRotate = true,
        Options = new DecodingOptions
        {
            TryHarder = true,
            PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE }
        }
    };
    var result = reader.Decode(bitmap);
    return result?.Text;
}
```

---

### 4.6. Xử lý xuất/nhập Excel với OpenXML (`XlsxImportService.cs`, `XlsxExportService.cs`)
Thay vì dùng thư viện `Microsoft.Office.Interop.Excel` (vốn chạy chậm, dễ bị treo tiến trình `EXCEL.EXE` ngầm và bắt buộc máy client phải cài Microsoft Office), hệ thống sử dụng **cơ chế xử lý chuẩn Office Open XML thuần**:
- Thao tác trực tiếp trên cấu trúc tệp nén ZIP và các lược đồ XML theo đặc tả OpenXML của Microsoft thông qua `System.IO.Compression` (ZipArchive) và `System.Xml` (XmlDocument / XmlWriter).
- Định dạng tiêu đề cột in đậm, tô màu nền, viền ô và định dạng kiểu dữ liệu tự động (chuỗi, số nguyên, số thực, ngày giờ, tiền tệ).
- Tốc độ xử lý nhanh, vận hành độc lập và hoàn toàn không phụ thuộc vào Microsoft Office hay bất kỳ package bên thứ ba nào.

---

## 5. BỘ CÂU HỎI PHẢN BIỆN THƯỜNG GẶP KHI BẢO VỆ ĐỒ ÁN (DEFENSE FAQ)

Dưới đây là 10 câu hỏi cốt lõi mà giảng viên trong hội đồng chấm đồ án .NET thường xuyên đặt ra, cùng câu trả lời mẫu ngắn gọn, chính xác:

#### Câu 1: Em hãy giải thích mô hình Entity Framework đang dùng trong dự án là gì? Ưu điểm so với ADO.NET thuần?
> **Trả lời:** Dự án sử dụng mô hình **Entity Framework 6 Database First (EDMX)**.  
> - So với ADO.NET thuần: EF6 tự động ánh xạ (mapping) các bảng SQL thành các lớp thực thể (Entity Classes) C#, giúp lập trình viên thao tác với dữ liệu thông qua ngôn ngữ hướng đối tượng và truy vấn LINQ to Entities thay vì phải nối chuỗi SQL thủ công (`SqlCommand`, `SqlDataReader`).  
> - EF6 giúp mã nguồn an toàn trước lỗ hổng **SQL Injection** nhờ cơ chế tham số hóa tự động (Parameterized Queries).

#### Câu 2: Tại sao trong nghiệp vụ bán hàng, em phải dùng `db.Database.BeginTransaction()`?
> **Trả lời:** Nghiệp vụ thanh toán bao gồm nhiều thao tác ghi dữ liệu liên hoàn: Tạo hóa đơn ➔ Thêm từng dòng chi tiết hóa đơn ➔ Trừ số lượng tồn kho từng sản phẩm.  
> Nếu không dùng Transaction, giả sử hệ thống trừ kho thành công món thứ nhất nhưng đến món thứ hai bị lỗi hoặc mất điện, CSDL sẽ rơi vào trạng thái dữ liệu rác (kho bị trừ nhưng không có hóa đơn tương ứng). Transaction đảm bảo tính chất **ACID (Atomicity - Tính nguyên tố)**: Hoặc toàn bộ thao tác cùng thành công, hoặc nếu có lỗi thì hoàn nguyên (`Rollback`) dữ liệu về trạng thái ban đầu.

#### Câu 3: Mật khẩu người dùng được lưu trữ như thế nào trong CSDL? Nếu em đánh cắp file CSDL thì có xem được mật khẩu không?
> **Trả lời:** Mật khẩu được băm một chiều bằng thuật toán **BCrypt** với Work Factor là 11. Chuỗi lưu trong CSDL là chuỗi băm 60 ký tự gồm phiên bản, số vòng lặp, muối (Salt) và kết quả mã hóa. Kẻ tấn công dù có toàn bộ file CSDL cũng không thể giải mã ngược chuỗi này ra mật khẩu gốc. Việc kiểm tra đăng nhập chỉ có thể thực hiện thông qua hàm `BCrypt.Verify()`.

#### Câu 4: Làm thế nào em xử lý tình trạng xóa một danh mục hay sản phẩm mà nó đã từng được bán trong hóa đơn?
> **Trả lời:** Em áp dụng kết hợp **Ràng buộc toàn vẹn khóa ngoại (FK)** và cơ chế **Xóa mềm (Soft Delete)**. Khi người dùng bấm xóa, code kiểm tra xem ID sản phẩm đã xuất hiện trong bảng `ChiTietHoaDon` hay `ChiTietPhieuNhap` chưa. Nếu chưa từng phát sinh, cho phép xóa cứng khỏi CSDL. Nếu đã có dữ liệu lịch sử, hệ thống từ chối xóa cứng và chỉ đổi cờ `DangKinhDoanh = false` để ẩn khỏi quầy bán hàng mà không vi phạm toàn vẹn dữ liệu.

#### Câu 5: Tại sao khi Restore CSDL lại phải có lệnh `SET SINGLE_USER WITH ROLLBACK IMMEDIATE`?
> **Trả lời:** SQL Server không cho phép phục hồi đè lên một CSDL đang có kết nối hoạt động (Database in use). Lệnh `SET SINGLE_USER WITH ROLLBACK IMMEDIATE` sẽ cưỡng chế ngắt toàn bộ các phiên làm việc hiện tại, rollback các tác vụ dở dang và trao quyền truy cập độc quyền cho tiến trình Restore, giúp quá trình khôi phục diễn ra an toàn mà không bị lỗi lock database.

#### Câu 6: Trong form Bán hàng, làm thế nào để khi quét mã QR thì giỏ hàng tự động cập nhật sản phẩm?
> **Trả lời:** Khi người dùng quét mã QR, thư viện ZXing giải mã ảnh ra chuỗi văn bản (ví dụ `SP000001`). Hệ thống lấy chuỗi này tra cứu trong bộ sưu tập sản phẩm đang hiển thị hoặc truy vấn CSDL để tìm `SanPhamId` tương ứng. Nếu sản phẩm tồn tại và còn hàng trong kho, hệ thống gọi hàm `ThemVaoGioHang(sanPhamId, soLuong: 1)`, sau đó kích hoạt hàm `RefreshCartView()` để vẽ lại giỏ hàng và tính lại tổng tiền.

#### Câu 7: Giả sử cửa hàng có 2 thu ngân cùng mở ứng dụng và cùng bán sản phẩm cuối cùng trong kho tại một thời điểm, hệ thống xử lý ra sao?
> **Trả lời:** Hệ thống kiểm tra tồn kho tại 2 tầng:
> 1. Tầng giao diện: Khi chọn sản phẩm, kiểm tra `numSoLuong <= sanPham.SoLuongTon`.
> 2. Tầng dịch vụ (`PosService.cs`): Nằm bên trong khối `BeginTransaction()`, hệ thống truy vấn lại dòng sản phẩm từ CSDL và kiểm tra `if (sanPham.SoLuongTon < item.SoLuong) throw new InvalidOperationException(...)`. Do nằm trong Transaction, người thu ngân xác nhận sau sẽ bị bẫy lỗi "Sản phẩm không đủ tồn kho", transaction rollback và kho hàng không bị âm.

#### Câu 8: Tại sao em không lưu trực tiếp tệp ảnh sản phẩm vào cột kiểu `IMAGE` hoặc `VARBINARY(MAX)` trong SQL Server?
> **Trả lời:** Lưu ảnh nhị phân trực tiếp vào CSDL khiến dung lượng file `.mdf` phình to rất nhanh, làm chậm tiến trình sao lưu/phục hồi CSDL và ngốn tài nguyên SQL Server khi query danh sách. Giải pháp chuẩn doanh nghiệp là: Lưu tệp ảnh vào thư mục hệ thống tệp (`Resources/`) sau khi đã nén chuẩn $500 \times 500$ px, và trong CSDL bảng `SanPham` chỉ lưu chuỗi đường dẫn tương đối (`DuongDanAnh` kiểu `NVARCHAR(255)`).

#### Câu 9: Cơ chế phân quyền trong ứng dụng được thực hiện như thế nào?
> **Trả lời:** Hệ thống lưu vai trò (`VaiTro`) trong bảng `TaiKhoan` gồm `ADMIN` và `NHANVIEN`. Khi đăng nhập thành công, vai trò được nạp vào lớp tĩnh `CurrentUserSession`. Tại form chính `FrmMain`, nếu người dùng là `NHANVIEN`, hệ thống tự động ẩn toàn bộ các nút menu quản trị (Nhân viên, Tài khoản, Danh mục, Chất liệu, Nhà cung cấp, Sao lưu phục hồi). Đồng thời tại hàm khởi tạo của từng form quản trị đều có lệnh kiểm tra lại `if (!CurrentUserSession.LaQuanTriVien) { Close(); }` để ngăn chặn việc mở form trái phép. Nhân sự còn có trường `ChucVu` trong bảng `NhanVien` để ghi nhận vai trò chuyên môn trong tiệm vàng (Quản lý, Bán hàng, Kho, Thu mua, Chăm sóc KH).

#### Câu 10: Điểm khác biệt giữa xuất/nhập Excel theo chuẩn OpenXML thuần và Microsoft.Office.Interop.Excel là gì?
> **Trả lời:** Interop Excel thực chất là gọi ứng dụng Excel chạy ngầm qua giao tiếp COM. Nó yêu cầu máy tính phải cài bộ Microsoft Office, chạy chậm và nếu ứng dụng gặp sự cố sẽ để lại tiến trình rác `EXCEL.EXE` chiếm CPU. Trong khi đó, giải pháp OpenXML thuần trong dự án thao tác trực tiếp với các tệp nén XML (.xlsx) thông qua các lớp chuẩn của .NET Framework (`System.IO.Compression` và `System.Xml`), chạy hoàn toàn độc lập, tốc độ xử lý nhanh và không đòi hỏi máy client cài đặt Microsoft Office hay bất kỳ gói thư viện bên ngoài nào.

---

## 6. KỊCH BẢN DEMO CHUẨN 5 PHÚT TRƯỚC HỘI ĐỒNG

Khi bước lên thuyết trình, hãy tự tin thao tác theo đúng kịch bản 6 bước logic dưới đây:

1. **Bước 1: Đăng nhập & Xác thực (1 phút)**
   - Đăng nhập bằng tài khoản nhân viên `ngoclan / PnjDemo@123`.
   - Chỉ cho hội đồng thấy: Menu Quản trị (Nhân viên, Tài khoản, CSDL) bị ẩn tự động theo đúng phân quyền `NHANVIEN`.
   - Đăng xuất và đăng nhập lại bằng `admin / PnjDemo@123`. Tất cả chức năng mở đầy đủ (`ADMIN`).
2. **Bước 2: Nghiệp vụ Bán hàng POS tại quầy (1.5 phút)**
   - Mở màn hình Bán hàng (giao diện 2 cột hiện đại).
   - Bấm nút **Quét QR (`F4`)** hoặc chọn một món trang sức (Ví dụ: Nhẫn kim cương vàng 18K).
   - Tăng số lượng lên 1. Thử nhập số lượng vượt tồn kho để thấy thông báo chặn lỗi.
   - Nhập chiết khấu 10%, chọn phương thức "Chuyển khoản".
   - Nhấn **Thanh toán (`F9`)**: Hệ thống commit Transaction, in hóa đơn bán lẻ, trừ tồn kho ngay lập tức.
3. **Bước 3: Quản lý Sản phẩm & Nén ảnh thông minh (1 phút)**
   - Mở màn hình Sản phẩm, chọn sản phẩm vừa bán để kiểm tra số lượng tồn đã bị trừ đúng 1 đơn vị.
   - Trình bày định mức chất liệu (BOM): Chiếc nhẫn gồm bao nhiêu chỉ vàng, bao nhiêu viên kim cương.
   - Trình diễn kéo-thả một bức ảnh từ Desktop vào khung ảnh `picSanPham`: Hệ thống tự động nén Bicubic về 500x500 px và lưu vào `Resources/`.
4. **Bước 4: Nghiệp vụ Bảo hành & Thu mua (0.5 phút)**
   - Mở màn hình Bảo hành: Tra cứu hóa đơn vừa lập ở Bước 2, cho thấy hạn bảo hành tự động được cộng thêm 12 tháng.
   - Mở màn hình Thu mua: Trình diễn việc nhập giá mua vàng cũ theo tuổi vàng.
5. **Bước 5: Thống kê & Xuất Báo cáo Excel (0.5 phút)**
   - Mở màn hình Thống kê: Cho hội đồng xem đồ thị doanh thu trực quan, biểu đồ tròn cơ cấu chất liệu.
   - Nhấn nút "Xuất Excel": Mở ngay tệp `.xlsx` được tạo ra với tiêu đề định dạng chuẩn kế toán.
6. **Bước 6: Sao lưu CSDL an toàn (0.5 phút)**
   - Mở tab Sao lưu / Phục hồi: Bấm "Sao lưu CSDL". Chỉ ra file `.bak` được tạo thành công với kiểm tra toàn vẹn `CHECKSUM` và cơ chế fallback nén thông minh.
   - Kết thúc phần trình bày và sẵn sàng nhận câu hỏi phản biện.