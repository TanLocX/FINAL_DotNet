using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    public partial class FrmBanHang : Form
    {
        private readonly List<PosCartItem> cartItems = new List<PosCartItem>();
        private List<ProductOption> productOptions = new List<ProductOption>();
        private List<CustomerOption> customerOptions = new List<CustomerOption>();
        private bool isInitializing;

        public FrmBanHang()
        {
            InitializeComponent();
            ConfigureCartGrid();
            cboPaymentMethod.SelectedIndex = 0;
            dtpProductWarranty.Value = DateTime.Today.AddYears(1);
            numDiscount.Maximum = 1000000000M;
            numCustomerCash.Maximum = 1000000000M;
            numProductQty.Maximum = 10000;
        }

        private void ConfigureCartGrid()
        {
            dgvCart.Columns.Clear();
            dgvCart.AutoGenerateColumns = false;

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColIndex",
                HeaderText = "STT",
                Width = 45,
                ReadOnly = true,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColProductCode",
                DataPropertyName = "ProductCode",
                HeaderText = "Mã SP",
                Width = 85,
                ReadOnly = true,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColProductName",
                DataPropertyName = "ProductName",
                HeaderText = "Tên món trang sức",
                Width = 240,
                ReadOnly = true
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColUnitPrice",
                DataPropertyName = "UnitPrice",
                HeaderText = "Đơn giá",
                Width = 110,
                ReadOnly = true,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" }
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColQuantity",
                DataPropertyName = "Quantity",
                HeaderText = "Số lượng",
                Width = 75,
                ReadOnly = true,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColLineTotal",
                DataPropertyName = "LineTotal",
                HeaderText = "Thành tiền",
                Width = 120,
                ReadOnly = true,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" }
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ColWarranty",
                DataPropertyName = "WarrantyDisplay",
                HeaderText = "Bảo hành",
                Width = 95,
                ReadOnly = true,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
        }

        private void FrmBanHang_Load(object sender, EventArgs e)
        {
            if (!CurrentUserSession.DaDangNhap)
            {
                MessageBox.Show("Phiên làm việc đã hết hạn. Vui lòng đăng nhập lại.", "Chưa đăng nhập",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BeginInvoke(new Action(Close));
                return;
            }

            isInitializing = true;
            try
            {
                LoadCustomersAndProducts();
                ResetOrderInternal();
            }
            finally
            {
                isInitializing = false;
            }
        }

        private void LoadCustomersAndProducts()
        {
            try
            {
                using (var db = DatabaseConnection.CreateContext())
                {
                    customerOptions = db.KhachHangs
                        .AsNoTracking()
                        .Where(kh => kh.DangHoatDong)
                        .OrderByDescending(kh => kh.HoTen == "Khách lẻ")
                        .ThenBy(kh => kh.HoTen)
                        .Select(kh => new CustomerOption
                        {
                            Id = kh.KhachHangId,
                            Name = kh.HoTen,
                            Phone = kh.SoDienThoai,
                            RewardPoints = kh.DiemTichLuy
                        })
                        .ToList();

                    cboCustomer.DisplayMember = "DisplayName";
                    cboCustomer.ValueMember = "Id";
                    cboCustomer.DataSource = customerOptions;

                    productOptions = db.SanPhams
                        .AsNoTracking()
                        .Where(sp => sp.DangKinhDoanh)
                        .OrderBy(sp => sp.TenSanPham)
                        .Select(sp => new ProductOption
                        {
                            Id = sp.SanPhamId,
                            Code = "SP" + sp.SanPhamId.ToString().PadLeft(6, '0'),
                            Name = sp.TenSanPham,
                            Price = sp.GiaBan,
                            InStock = sp.SoLuongTon
                        })
                        .ToList();

                    cboProductSelector.DisplayMember = "DisplayName";
                    cboProductSelector.ValueMember = "Id";
                    cboProductSelector.DataSource = productOptions;
                }
            }
            catch (Exception ex)
            {
                ShowNotification("Không thể tải danh mục: " + ex.Message, true);
            }
        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializing) return;
            var customer = cboCustomer.SelectedItem as CustomerOption;
            if (customer != null)
            {
                lblCustomerInfo.Text = $"SĐT: {customer.Phone} | Tích lũy: {customer.RewardPoints:N0} điểm";
            }
        }

        private void cboProductSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializing) return;
            var product = cboProductSelector.SelectedItem as ProductOption;
            if (product != null)
            {
                lblProductInStock.Text = $"Tồn kho: {product.InStock:N0} | Đơn giá: {product.Price:N0} đ";
            }
        }

        private void btnQuickAdd_Click(object sender, EventArgs e)
        {
            var product = cboProductSelector.SelectedItem as ProductOption;
            if (product == null)
            {
                ShowNotification("Vui lòng chọn một sản phẩm.", true);
                return;
            }

            int qtyToAdd = (int)numProductQty.Value;
            if (qtyToAdd <= 0)
            {
                ShowNotification("Số lượng thêm phải lớn hơn 0.", true);
                return;
            }

            DateTime? warrantyDate = dtpProductWarranty.Checked ? (DateTime?)dtpProductWarranty.Value.Date : null;
            if (warrantyDate.HasValue && warrantyDate.Value < DateTime.Today)
            {
                ShowNotification("Hạn bảo hành không được trước ngày hôm nay.", true);
                return;
            }

            AddProductToCart(product.Id, qtyToAdd, warrantyDate);
        }

        private void AddProductToCart(int productId, int quantity, DateTime? warrantyDate)
        {
            var product = productOptions.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                ShowNotification("Sản phẩm không có trong danh mục đang kinh doanh.", true);
                return;
            }

            var existingItem = cartItems.FirstOrDefault(item => item.ProductId == productId);
            int currentQtyInCart = existingItem != null ? existingItem.Quantity : 0;
            int requestedTotalQty = currentQtyInCart + quantity;

            if (product.InStock < requestedTotalQty)
            {
                ShowNotification($"Sản phẩm '{product.Name}' chỉ còn {product.InStock} trong kho (trong giỏ đã có {currentQtyInCart}).", true);
                return;
            }

            if (existingItem != null)
            {
                existingItem.Quantity = requestedTotalQty;
                if (warrantyDate.HasValue) existingItem.WarrantyExpiryDate = warrantyDate;
            }
            else
            {
                cartItems.Add(new PosCartItem
                {
                    ProductId = product.Id,
                    ProductCode = product.Code,
                    ProductName = product.Name,
                    CurrentStock = product.InStock,
                    UnitPrice = product.Price,
                    Quantity = quantity,
                    WarrantyExpiryDate = warrantyDate
                });
            }

            RefreshCartView();
            ShowNotification($"Đã thêm '{product.Name}' vào giỏ hàng.", false);
            numProductQty.Value = 1;
        }

        private void btnScanQr_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Chọn ảnh mã QR sản phẩm";
                dialog.Filter = "Ảnh QR Code (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
                if (dialog.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    using (var bitmap = new Bitmap(dialog.FileName))
                    {
                        string qrContent = QrCodeService.DocMaQr(bitmap);
                        if (string.IsNullOrWhiteSpace(qrContent))
                        {
                            ShowNotification("Không nhận diện được mã QR hợp lệ từ file ảnh đã chọn.", true);
                            return;
                        }

                        // Match product code (e.g. SP000001) or ID
                        var matchedProduct = productOptions.FirstOrDefault(p =>
                            string.Equals(p.Code, qrContent, StringComparison.OrdinalIgnoreCase) ||
                            p.Id.ToString() == qrContent ||
                            string.Equals(p.Name, qrContent, StringComparison.OrdinalIgnoreCase));

                        if (matchedProduct == null)
                        {
                            ShowNotification($"Mã QR '{qrContent}' không khớp với bất kỳ sản phẩm nào đang kinh doanh.", true);
                            return;
                        }

                        // Auto add 1 item to cart
                        AddProductToCart(matchedProduct.Id, 1, DateTime.Today.AddYears(1));
                        cboProductSelector.SelectedValue = matchedProduct.Id;
                    }
                }
                catch (Exception ex)
                {
                    ShowNotification("Lỗi khi đọc file ảnh QR: " + ex.Message, true);
                }
            }
        }

        private void RefreshCartView()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = cartItems.ToList();

            // Populate row numbers
            for (int i = 0; i < dgvCart.Rows.Count; i++)
            {
                dgvCart.Rows[i].Cells["ColIndex"].Value = (i + 1).ToString();
            }

            lblCartSummary.Text = $"Giỏ hàng: {cartItems.Sum(item => item.Quantity)} món ({cartItems.Count} dòng sản phẩm)";
            RecalculateTotals();
        }

        private void RecalculateTotals()
        {
            decimal subTotal = cartItems.Sum(item => item.LineTotal);
            decimal discount = numDiscount.Value;
            if (discount > subTotal)
            {
                discount = subTotal;
                numDiscount.Value = discount;
            }

            decimal totalPayable = Math.Max(0M, subTotal - discount);
            decimal customerCash = numCustomerCash.Value;
            decimal changeDue = Math.Max(0M, customerCash - totalPayable);

            lblSubTotalValue.Text = subTotal.ToString("N0") + " đ";
            lblTotalPayableValue.Text = totalPayable.ToString("N0") + " đ";
            lblChangeDueValue.Text = changeDue.ToString("N0") + " đ";
        }

        private void numDiscount_ValueChanged(object sender, EventArgs e) => RecalculateTotals();
        private void numCustomerCash_ValueChanged(object sender, EventArgs e) => RecalculateTotals();

        private void btnCashExact_Click(object sender, EventArgs e)
        {
            decimal subTotal = cartItems.Sum(item => item.LineTotal);
            decimal totalPayable = Math.Max(0M, subTotal - numDiscount.Value);
            numCustomerCash.Value = totalPayable;
        }

        private void btnCash500k_Click(object sender, EventArgs e) => numCustomerCash.Value = 500000M;
        private void btnCash1m_Click(object sender, EventArgs e) => numCustomerCash.Value = 1000000M;
        private void btnCash2m_Click(object sender, EventArgs e) => numCustomerCash.Value = 2000000M;

        private void btnIncreaseQty_Click(object sender, EventArgs e)
        {
            var selectedItem = GetSelectedCartItem();
            if (selectedItem == null) return;

            var product = productOptions.FirstOrDefault(p => p.Id == selectedItem.ProductId);
            if (product != null && selectedItem.Quantity >= product.InStock)
            {
                ShowNotification($"Không thể tăng số lượng. Tồn kho tối đa: {product.InStock}.", true);
                return;
            }

            selectedItem.Quantity++;
            RefreshCartView();
        }

        private void btnDecreaseQty_Click(object sender, EventArgs e)
        {
            var selectedItem = GetSelectedCartItem();
            if (selectedItem == null) return;

            selectedItem.Quantity--;
            if (selectedItem.Quantity <= 0)
            {
                cartItems.Remove(selectedItem);
            }

            RefreshCartView();
        }

        private void btnRemoveSelectedItem_Click(object sender, EventArgs e)
        {
            var selectedItem = GetSelectedCartItem();
            if (selectedItem == null)
            {
                ShowNotification("Vui lòng chọn dòng sản phẩm cần xóa.", true);
                return;
            }

            cartItems.Remove(selectedItem);
            RefreshCartView();
            ShowNotification($"Đã xóa '{selectedItem.ProductName}' khỏi giỏ hàng.", false);
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (cartItems.Count == 0) return;
            if (MessageBox.Show("Bạn có chắc chắn muốn làm sạch toàn bộ giỏ hàng?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cartItems.Clear();
                RefreshCartView();
                ShowNotification("Đã làm sạch giỏ hàng.", false);
            }
        }

        private PosCartItem GetSelectedCartItem()
        {
            if (dgvCart.CurrentRow == null) return null;
            return dgvCart.CurrentRow.DataBoundItem as PosCartItem;
        }

        private void dgvCart_SelectionChanged(object sender, EventArgs e)
        {
            var item = GetSelectedCartItem();
            if (item != null)
            {
                cboProductSelector.SelectedValue = item.ProductId;
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (!CurrentUserSession.DaDangNhap)
            {
                MessageBox.Show("Phiên đăng nhập đã kết thúc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var customer = cboCustomer.SelectedItem as CustomerOption;
            if (customer == null)
            {
                ShowNotification("Vui lòng chọn thông tin khách hàng.", true);
                return;
            }

            if (cartItems.Count == 0)
            {
                ShowNotification("Giỏ hàng đang trống. Hãy thêm sản phẩm trước khi thanh toán.", true);
                return;
            }

            decimal subTotal = cartItems.Sum(item => item.LineTotal);
            decimal discount = numDiscount.Value;
            decimal totalPayable = Math.Max(0M, subTotal - discount);
            decimal customerCash = numCustomerCash.Value;

            string paymentMethod = cboPaymentMethod.Text;
            if (paymentMethod == "Tiền mặt" && customerCash < totalPayable)
            {
                if (MessageBox.Show($"Khách đưa {customerCash:N0} đ, chưa đủ {totalPayable:N0} đ. Bạn có muốn tự động nhận đủ tiền mặt?",
                    "Xác nhận tiền mặt", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    customerCash = totalPayable;
                    numCustomerCash.Value = customerCash;
                }
                else
                {
                    return;
                }
            }

            var request = new PosCheckoutRequest
            {
                StaffId = CurrentUserSession.HienTai.NhanVienId,
                CustomerId = customer.Id,
                SubTotal = subTotal,
                DiscountAmount = discount,
                TotalPayable = totalPayable,
                CustomerPayment = customerCash,
                PaymentMethod = paymentMethod,
                Items = cartItems
            };

            var result = PosService.ProcessCheckout(request);
            if (!result.IsSuccess)
            {
                ShowNotification(result.ErrorMessage, true);
                MessageBox.Show(result.ErrorMessage, "Lỗi thanh toán", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int newInvoiceId = result.InvoiceId;
            string newInvoiceCode = result.InvoiceCode;

            // Reload products to refresh stock counts
            LoadCustomersAndProducts();
            ResetOrderInternal();

            var printChoice = MessageBox.Show(
                $"Thanh toán thành công đơn hàng {newInvoiceCode}!\nTổng tiền: {totalPayable:N0} đ\n\nBạn có muốn xem và in hóa đơn ngay không?",
                "Giao dịch thành công",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (printChoice == DialogResult.Yes)
            {
                try
                {
                    var reportConfig = BaoCaoService.TaoHoaDon(newInvoiceId);
                    using (var reportViewer = new FrmXemBaoCao(reportConfig))
                    {
                        reportViewer.ShowDialog(this);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể mở hóa đơn in: " + ex.Message, "Lỗi in ấn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnNewOrder_Click(object sender, EventArgs e)
        {
            if (cartItems.Count > 0)
            {
                if (MessageBox.Show("Hủy đơn hiện tại và tạo đơn mới?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
            }

            ResetOrderInternal();
            ShowNotification("Đã khởi tạo đơn mới.", false);
        }

        private void ResetOrderInternal()
        {
            cartItems.Clear();
            if (customerOptions.Count > 0)
            {
                cboCustomer.SelectedIndex = 0;
                var cust = cboCustomer.SelectedItem as CustomerOption;
                if (cust != null) lblCustomerInfo.Text = $"SĐT: {cust.Phone} | Tích lũy: {cust.RewardPoints:N0} điểm";
            }
            if (productOptions.Count > 0)
            {
                cboProductSelector.SelectedIndex = 0;
                var prod = cboProductSelector.SelectedItem as ProductOption;
                if (prod != null) lblProductInStock.Text = $"Tồn kho: {prod.InStock:N0} | Đơn giá: {prod.Price:N0} đ";
            }

            string staffName = CurrentUserSession.DaDangNhap ? CurrentUserSession.HienTai.HoTen : "--";
            lblCashierInfo.Text = $"{staffName} | {DateTime.Now:dd/MM/yyyy HH:mm}";

            cboPaymentMethod.SelectedIndex = 0;
            numProductQty.Value = 1;
            numDiscount.Value = 0M;
            numCustomerCash.Value = 0M;
            dtpProductWarranty.Checked = true;
            dtpProductWarranty.Value = DateTime.Today.AddYears(1);

            RefreshCartView();
            lblNotification.Text = string.Empty;
        }

        private void FrmBanHang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                btnCheckout_Click(this, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F4)
            {
                btnNewOrder_Click(this, EventArgs.Empty);
                e.Handled = true;
            }
        }

        private void ShowNotification(string message, bool isError)
        {
            lblNotification.ForeColor = isError ? Color.FromArgb(198, 40, 40) : Color.FromArgb(46, 125, 50);
            lblNotification.Text = message;
        }

        private sealed class CustomerOption
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public int RewardPoints { get; set; }
            public string DisplayName => string.IsNullOrWhiteSpace(Phone) ? Name : $"{Name} ({Phone})";
            public override string ToString() => DisplayName;
        }

        private sealed class ProductOption
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int InStock { get; set; }
            public string DisplayName => $"{Code} - {Name} ({Price:N0} đ | Kho: {InStock})";
            public override string ToString() => DisplayName;
        }
    }
}
