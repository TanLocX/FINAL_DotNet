Khi bạn gọi lệnh BCrypt.HashPassword("admin123"), thuật toán BCrypt sẽ thực hiện một quy trình gồm 3 bước cực kỳ thông minh dưới đây:
  ### Bước 1: Sinh ra "Muối" (Salt) ngẫu nhiên

  Đầu tiên, BCrypt sẽ tự động tạo ra một chuỗi 22 ký tự ngẫu nhiên, ví dụ: R9h/c12P.kZfR... (đây gọi là Salt).
  Mục đích của việc này là để đảm bảo rằng dù 2 người dùng có đặt chung một mật khẩu là admin123, thì chuỗi kết quả tạo ra ở bước cuối cùng vẫn hoàn toàn khác nhau.

  ### Bước 2: Kéo dài thời gian băm (Cost Factor / Work Factor)
  số nhân 2ⁿ).
  BCrypt sử dụng một thông số gọi là Work Factor (thường mặc định là 10 hoặc 11, 12). Con số này quy định thuật toán sẽ phải lặp lại việc nhào nặn mật khẩu bao nhiêu vòng (theo cấp    
  • Nếu Factor là 10 → Lặp 2¹⁰ = 1.024 vòng.
  • Nếu Factor là 12 → Lặp 2¹² = 4.096 vòng.
  • Nếu bạn nhập admin123, nó sẽ lấy admin123 trộn với Salt ở bước 1, sau đó chạy qua thuật toán mã hóa phức tạp lặp đi lặp lại 1.024 lần (cố tình làm chậm CPU mất khoảng vài chục     
  mili-giây).
  ### Bước 3: Đóng gói thành chuỗi kết quả duy nhất

  Cuối cùng, BCrypt sẽ nối tất cả các thông tin lại thành 1 chuỗi dài duy nhất (khoảng 60 ký tự). Bạn sẽ lưu nguyên chuỗi này vào cột MatKhau trong Database.

  Ví dụ chuỗi kết quả băm của admin123 trông sẽ như thế này:
  │ $2a$11$R9h/c12P.kZfRwQk5Z...wQk5Z.J/0VqwQk5Z...
  Nhìn chuỗi này có vẻ lộn xộn, nhưng nó được chia làm 4 phần rất rõ ràng:

  1. $2a$: Phiên bản của thuật toán BCrypt.
  2. $11$: Chính là Work Factor (lặp 2¹¹ = 2048 vòng).
  3. R9h/c12P.kZfRwQk5Z... (22 ký tự tiếp theo): Chính là chuỗi Salt ngẫu nhiên đã tạo ở Bước 1.
  4. Phần còn lại: Là kết quả băm (Hash) thực sự của chữ admin123.
  ──────
  ### Vậy khi đăng nhập thì BCrypt kiểm tra "admin123" bằng cách nào?

  Đây là điểm ăn tiền nhất của BCrypt so với các phương pháp cũ: Bạn không cần phải tự lưu trữ chuỗi Salt.

  Khi người dùng gõ admin123 vào form đăng nhập, code của bạn sẽ gọi lệnh:
  BCrypt.Verify("admin123", chuoi_bam_trong_CSDL)

  Thuật toán sẽ làm các bước sau:

  1. Nó nhìn vào chuỗi $2a$11$R9h/c1... lấy từ CSDL.
  2. Nó tự động trích xuất ra được: "À, tài khoản này dùng Factor là 11 và Salt là R9h/c12P..."
  3. Nó mang chữ admin123 người dùng vừa nhập trên form, đem trộn với cái Salt vừa trích xuất được.
  4. Nó lặp lại quá trình tính toán 2048 vòng (Factor 11) giống y hệt lúc tạo.
  5. Nếu chuỗi kết quả vừa tính ra KHỚP Y HỆT với phần Hash cuối cùng của chuỗi trong CSDL → Mật khẩu đúng, cho phép đăng nhập!

  Chính nhờ cơ chế "chứa luôn công thức giải trong chuỗi băm" này mà BCrypt rất dễ sử dụng (chỉ cần 1 dòng code) nhưng lại bảo mật tuyệt đối trước các đợt tấn công dò mật khẩu của     
  hacker.