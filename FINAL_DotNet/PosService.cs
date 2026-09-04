using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace FINAL_DotNet
{
    public class PosCartItem
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int CurrentStock { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal => Quantity * UnitPrice;
        public DateTime? WarrantyExpiryDate { get; set; }
        public string WarrantyDisplay => WarrantyExpiryDate.HasValue 
            ? WarrantyExpiryDate.Value.ToString("dd/MM/yyyy") 
            : "Không";
    }

    public class PosCheckoutRequest
    {
        public int StaffId { get; set; }
        public int CustomerId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalPayable { get; set; }
        public decimal CustomerPayment { get; set; }
        public decimal ChangeDue => Math.Max(0M, CustomerPayment - TotalPayable);
        public string PaymentMethod { get; set; }
        public List<PosCartItem> Items { get; set; } = new List<PosCartItem>();
    }

    public class PosCheckoutResult
    {
        public bool IsSuccess { get; set; }
        public int InvoiceId { get; set; }
        public string InvoiceCode { get; set; }
        public string ErrorMessage { get; set; }

        public static PosCheckoutResult Success(int invoiceId)
        {
            return new PosCheckoutResult
            {
                IsSuccess = true,
                InvoiceId = invoiceId,
                InvoiceCode = $"HD{invoiceId:000000}"
            };
        }

        public static PosCheckoutResult Fail(string message)
        {
            return new PosCheckoutResult
            {
                IsSuccess = false,
                ErrorMessage = message
            };
        }
    }

    public static class PosService
    {
        public static PosCheckoutResult ProcessCheckout(PosCheckoutRequest request)
        {
            if (request == null)
            {
                return PosCheckoutResult.Fail("Dữ liệu thanh toán không hợp lệ.");
            }

            if (request.Items == null || request.Items.Count == 0)
            {
                return PosCheckoutResult.Fail("Giỏ hàng đang trống. Vui lòng chọn ít nhất một sản phẩm.");
            }

            if (request.CustomerId <= 0)
            {
                return PosCheckoutResult.Fail("Vui lòng chọn khách hàng hợp lệ.");
            }

            if (request.StaffId <= 0)
            {
                return PosCheckoutResult.Fail("Phiên nhân viên không hợp lệ.");
            }

            if (request.DiscountAmount < 0)
            {
                return PosCheckoutResult.Fail("Giảm giá không được là số âm.");
            }

            if (string.IsNullOrWhiteSpace(request.PaymentMethod))
            {
                request.PaymentMethod = "Tiền mặt";
            }

            using (var db = DatabaseConnection.CreateContext())
            using (var transaction = db.Database.BeginTransaction(IsolationLevel.RepeatableRead))
            {
                try
                {
                    var staff = db.NhanViens.Find(request.StaffId);
                    if (staff == null || !staff.DangLamViec)
                    {
                        return PosCheckoutResult.Fail("Nhân viên thu ngân không tồn tại hoặc đã nghỉ việc.");
                    }

                    var customer = db.KhachHangs.Find(request.CustomerId);
                    if (customer == null || !customer.DangHoatDong)
                    {
                        return PosCheckoutResult.Fail("Khách hàng không tồn tại hoặc đã bị ngừng giao dịch.");
                    }

                    // Validate stock and prepare invoice details
                    decimal calculatedSubTotal = 0M;
                    var productIds = request.Items.Select(item => item.ProductId).Distinct().ToList();
                    var productsInDb = db.SanPhams
                        .Where(p => productIds.Contains(p.SanPhamId))
                        .ToDictionary(p => p.SanPhamId);

                    var invoiceDetails = new List<ChiTietHoaDon>();

                    foreach (var cartItem in request.Items)
                    {
                        if (cartItem.Quantity <= 0)
                        {
                            return PosCheckoutResult.Fail($"Số lượng sản phẩm '{cartItem.ProductName}' phải lớn hơn 0.");
                        }

                        if (!productsInDb.TryGetValue(cartItem.ProductId, out var product))
                        {
                            return PosCheckoutResult.Fail($"Sản phẩm mã {cartItem.ProductCode} không tồn tại trong hệ thống.");
                        }

                        if (!product.DangKinhDoanh)
                        {
                            return PosCheckoutResult.Fail($"Sản phẩm '{product.TenSanPham}' đã ngừng kinh doanh.");
                        }

                        if (product.SoLuongTon < cartItem.Quantity)
                        {
                            return PosCheckoutResult.Fail($"Sản phẩm '{product.TenSanPham}' không đủ tồn kho (Còn: {product.SoLuongTon}, Yêu cầu: {cartItem.Quantity}).");
                        }

                        // Deduct inventory
                        product.SoLuongTon -= cartItem.Quantity;

                        decimal lineTotal = cartItem.Quantity * product.GiaBan;
                        calculatedSubTotal += lineTotal;

                        invoiceDetails.Add(new ChiTietHoaDon
                        {
                            SanPhamId = product.SanPhamId,
                            SoLuong = cartItem.Quantity,
                            DonGiaBan = product.GiaBan,
                            HanBaoHanh = cartItem.WarrantyExpiryDate
                        });
                    }

                    decimal calculatedDiscount = Math.Min(request.DiscountAmount, calculatedSubTotal);
                    decimal finalPayable = Math.Max(0M, calculatedSubTotal - calculatedDiscount);

                    var invoice = new HoaDon
                    {
                        NhanVienId = request.StaffId,
                        KhachHangId = request.CustomerId,
                        NgayLap = DateTime.Now,
                        TongTien = calculatedSubTotal,
                        GiamGia = calculatedDiscount,
                        ThanhTien = finalPayable,
                        PhuongThucThanhToan = request.PaymentMethod,
                        TrangThai = "DA_THANH_TOAN"
                    };

                    db.HoaDons.Add(invoice);
                    db.SaveChanges(); // Persist to get generated HoaDonId

                    foreach (var detail in invoiceDetails)
                    {
                        detail.HoaDonId = invoice.HoaDonId;
                        db.ChiTietHoaDons.Add(detail);
                    }

                    // Optional customer reward points (1 point per 100,000 VND)
                    if (customer.KhachHangId != 1 && customer.SoDienThoai != "0000000000")
                    {
                        int pointsEarned = (int)(finalPayable / 100000M);
                        customer.DiemTichLuy += pointsEarned;
                    }

                    db.SaveChanges();
                    transaction.Commit();

                    return PosCheckoutResult.Success(invoice.HoaDonId);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return PosCheckoutResult.Fail("Lỗi hệ thống khi thanh toán: " + ex.Message);
                }
            }
        }
    }
}
