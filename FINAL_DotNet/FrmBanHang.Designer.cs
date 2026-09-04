namespace FINAL_DotNet
{
    partial class FrmBanHang
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlCheckoutContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.lblCheckoutHeader = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblCustomerTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cboCustomer = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblCustomerInfo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblCashierTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblCashierInfo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pnlDivider1 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblSubTotalTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblSubTotalValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblDiscountTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.numDiscount = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.pnlDivider2 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTotalPayableTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblTotalPayableValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblCustomerCashTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.numCustomerCash = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.pnlQuickCash = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCashExact = new Guna.UI2.WinForms.Guna2Button();
            this.btnCash500k = new Guna.UI2.WinForms.Guna2Button();
            this.btnCash1m = new Guna.UI2.WinForms.Guna2Button();
            this.btnCash2m = new Guna.UI2.WinForms.Guna2Button();
            this.lblChangeDueTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblChangeDueValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblPaymentMethodTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cboPaymentMethod = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnCheckout = new Guna.UI2.WinForms.Guna2Button();
            this.btnNewOrder = new Guna.UI2.WinForms.Guna2Button();
            this.lblNotification = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pnlCartContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlProductBar = new Guna.UI2.WinForms.Guna2Panel();
            this.lblSelectProduct = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cboProductSelector = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblProductQty = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.numProductQty = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.lblWarrantyDate = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.dtpProductWarranty = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btnQuickAdd = new Guna.UI2.WinForms.Guna2Button();
            this.btnScanQr = new Guna.UI2.WinForms.Guna2Button();
            this.lblProductInStock = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.dgvCart = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlCartFooter = new Guna.UI2.WinForms.Guna2Panel();
            this.lblCartSummary = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnIncreaseQty = new Guna.UI2.WinForms.Guna2Button();
            this.btnDecreaseQty = new Guna.UI2.WinForms.Guna2Button();
            this.btnRemoveSelectedItem = new Guna.UI2.WinForms.Guna2Button();
            this.btnClearCart = new Guna.UI2.WinForms.Guna2Button();
            this.pnlCheckoutContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCustomerCash)).BeginInit();
            this.pnlQuickCash.SuspendLayout();
            this.pnlCartContainer.SuspendLayout();
            this.pnlProductBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProductQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.pnlCartFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCheckoutContainer
            // 
            this.pnlCheckoutContainer.BackColor = System.Drawing.Color.White;
            this.pnlCheckoutContainer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlCheckoutContainer.BorderThickness = 1;
            this.pnlCheckoutContainer.Controls.Add(this.lblCheckoutHeader);
            this.pnlCheckoutContainer.Controls.Add(this.lblCustomerTitle);
            this.pnlCheckoutContainer.Controls.Add(this.cboCustomer);
            this.pnlCheckoutContainer.Controls.Add(this.lblCustomerInfo);
            this.pnlCheckoutContainer.Controls.Add(this.lblCashierTitle);
            this.pnlCheckoutContainer.Controls.Add(this.lblCashierInfo);
            this.pnlCheckoutContainer.Controls.Add(this.pnlDivider1);
            this.pnlCheckoutContainer.Controls.Add(this.lblSubTotalTitle);
            this.pnlCheckoutContainer.Controls.Add(this.lblSubTotalValue);
            this.pnlCheckoutContainer.Controls.Add(this.lblDiscountTitle);
            this.pnlCheckoutContainer.Controls.Add(this.numDiscount);
            this.pnlCheckoutContainer.Controls.Add(this.pnlDivider2);
            this.pnlCheckoutContainer.Controls.Add(this.lblTotalPayableTitle);
            this.pnlCheckoutContainer.Controls.Add(this.lblTotalPayableValue);
            this.pnlCheckoutContainer.Controls.Add(this.lblCustomerCashTitle);
            this.pnlCheckoutContainer.Controls.Add(this.numCustomerCash);
            this.pnlCheckoutContainer.Controls.Add(this.pnlQuickCash);
            this.pnlCheckoutContainer.Controls.Add(this.lblChangeDueTitle);
            this.pnlCheckoutContainer.Controls.Add(this.lblChangeDueValue);
            this.pnlCheckoutContainer.Controls.Add(this.lblPaymentMethodTitle);
            this.pnlCheckoutContainer.Controls.Add(this.cboPaymentMethod);
            this.pnlCheckoutContainer.Controls.Add(this.btnCheckout);
            this.pnlCheckoutContainer.Controls.Add(this.btnNewOrder);
            this.pnlCheckoutContainer.Controls.Add(this.lblNotification);
            this.pnlCheckoutContainer.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlCheckoutContainer.Location = new System.Drawing.Point(670, 0);
            this.pnlCheckoutContainer.Name = "pnlCheckoutContainer";
            this.pnlCheckoutContainer.Padding = new System.Windows.Forms.Padding(12);
            this.pnlCheckoutContainer.Size = new System.Drawing.Size(350, 680);
            this.pnlCheckoutContainer.TabIndex = 1;
            // 
            // lblCheckoutHeader
            // 
            this.lblCheckoutHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblCheckoutHeader.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblCheckoutHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            this.lblCheckoutHeader.Location = new System.Drawing.Point(12, 10);
            this.lblCheckoutHeader.Name = "lblCheckoutHeader";
            this.lblCheckoutHeader.Size = new System.Drawing.Size(185, 23);
            this.lblCheckoutHeader.TabIndex = 0;
            this.lblCheckoutHeader.Text = "THÔNG TIN THANH TOÁN";
            // 
            // lblCustomerTitle
            // 
            this.lblCustomerTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblCustomerTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblCustomerTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblCustomerTitle.Location = new System.Drawing.Point(12, 38);
            this.lblCustomerTitle.Name = "lblCustomerTitle";
            this.lblCustomerTitle.Size = new System.Drawing.Size(73, 15);
            this.lblCustomerTitle.TabIndex = 1;
            this.lblCustomerTitle.Text = "Khách hàng *";
            // 
            // cboCustomer
            // 
            this.cboCustomer.BackColor = System.Drawing.Color.Transparent;
            this.cboCustomer.BorderRadius = 6;
            this.cboCustomer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCustomer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboCustomer.ItemHeight = 28;
            this.cboCustomer.Location = new System.Drawing.Point(12, 56);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(326, 34);
            this.cboCustomer.TabIndex = 2;
            this.cboCustomer.SelectedIndexChanged += new System.EventHandler(this.cboCustomer_SelectedIndexChanged);
            // 
            // lblCustomerInfo
            // 
            this.lblCustomerInfo.AutoSize = false;
            this.lblCustomerInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblCustomerInfo.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblCustomerInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCustomerInfo.Location = new System.Drawing.Point(12, 93);
            this.lblCustomerInfo.Name = "lblCustomerInfo";
            this.lblCustomerInfo.Size = new System.Drawing.Size(326, 18);
            this.lblCustomerInfo.TabIndex = 3;
            this.lblCustomerInfo.Text = "SĐT: 0000000000 | Tích lũy: 0 điểm";
            // 
            // lblCashierTitle
            // 
            this.lblCashierTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblCashierTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblCashierTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblCashierTitle.Location = new System.Drawing.Point(12, 114);
            this.lblCashierTitle.Name = "lblCashierTitle";
            this.lblCashierTitle.Size = new System.Drawing.Size(120, 15);
            this.lblCashierTitle.TabIndex = 4;
            this.lblCashierTitle.Text = "Thu ngân & Thời điểm";
            // 
            // lblCashierInfo
            // 
            this.lblCashierInfo.AutoSize = false;
            this.lblCashierInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblCashierInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCashierInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblCashierInfo.Location = new System.Drawing.Point(12, 131);
            this.lblCashierInfo.Name = "lblCashierInfo";
            this.lblCashierInfo.Size = new System.Drawing.Size(326, 20);
            this.lblCashierInfo.TabIndex = 5;
            this.lblCashierInfo.Text = "Nhân viên: -- | 01/01/2026 00:00";
            // 
            // pnlDivider1
            // 
            this.pnlDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlDivider1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlDivider1.Location = new System.Drawing.Point(12, 156);
            this.pnlDivider1.Name = "pnlDivider1";
            this.pnlDivider1.Size = new System.Drawing.Size(326, 1);
            this.pnlDivider1.TabIndex = 6;
            // 
            // lblSubTotalTitle
            // 
            this.lblSubTotalTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSubTotalTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubTotalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSubTotalTitle.Location = new System.Drawing.Point(12, 164);
            this.lblSubTotalTitle.Name = "lblSubTotalTitle";
            this.lblSubTotalTitle.Size = new System.Drawing.Size(95, 17);
            this.lblSubTotalTitle.TabIndex = 7;
            this.lblSubTotalTitle.Text = "Tổng tiền hàng:";
            // 
            // lblSubTotalValue
            // 
            this.lblSubTotalValue.AutoSize = false;
            this.lblSubTotalValue.BackColor = System.Drawing.Color.Transparent;
            this.lblSubTotalValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSubTotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSubTotalValue.Location = new System.Drawing.Point(150, 163);
            this.lblSubTotalValue.Name = "lblSubTotalValue";
            this.lblSubTotalValue.Size = new System.Drawing.Size(188, 20);
            this.lblSubTotalValue.TabIndex = 8;
            this.lblSubTotalValue.Text = "0 đ";
            this.lblSubTotalValue.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiscountTitle
            // 
            this.lblDiscountTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblDiscountTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDiscountTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblDiscountTitle.Location = new System.Drawing.Point(12, 192);
            this.lblDiscountTitle.Name = "lblDiscountTitle";
            this.lblDiscountTitle.Size = new System.Drawing.Size(89, 17);
            this.lblDiscountTitle.TabIndex = 9;
            this.lblDiscountTitle.Text = "Chiết khấu (đ):";
            // 
            // numDiscount
            // 
            this.numDiscount.BackColor = System.Drawing.Color.Transparent;
            this.numDiscount.BorderRadius = 6;
            this.numDiscount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numDiscount.Location = new System.Drawing.Point(150, 188);
            this.numDiscount.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numDiscount.Name = "numDiscount";
            this.numDiscount.Size = new System.Drawing.Size(188, 26);
            this.numDiscount.TabIndex = 10;
            this.numDiscount.ThousandsSeparator = true;
            this.numDiscount.UpDownButtonFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(182)))), ((int)(((byte)(116)))));
            this.numDiscount.ValueChanged += new System.EventHandler(this.numDiscount_ValueChanged);
            // 
            // pnlDivider2
            // 
            this.pnlDivider2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlDivider2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlDivider2.Location = new System.Drawing.Point(12, 222);
            this.pnlDivider2.Name = "pnlDivider2";
            this.pnlDivider2.Size = new System.Drawing.Size(326, 1);
            this.pnlDivider2.TabIndex = 11;
            // 
            // lblTotalPayableTitle
            // 
            this.lblTotalPayableTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalPayableTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalPayableTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTotalPayableTitle.Location = new System.Drawing.Point(12, 230);
            this.lblTotalPayableTitle.Name = "lblTotalPayableTitle";
            this.lblTotalPayableTitle.Size = new System.Drawing.Size(117, 19);
            this.lblTotalPayableTitle.TabIndex = 12;
            this.lblTotalPayableTitle.Text = "KHÁCH CẦN TRẢ:";
            // 
            // lblTotalPayableValue
            // 
            this.lblTotalPayableValue.AutoSize = false;
            this.lblTotalPayableValue.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalPayableValue.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotalPayableValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(120)))), ((int)(((byte)(20)))));
            this.lblTotalPayableValue.Location = new System.Drawing.Point(135, 226);
            this.lblTotalPayableValue.Name = "lblTotalPayableValue";
            this.lblTotalPayableValue.Size = new System.Drawing.Size(203, 28);
            this.lblTotalPayableValue.TabIndex = 13;
            this.lblTotalPayableValue.Text = "0 đ";
            this.lblTotalPayableValue.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCustomerCashTitle
            // 
            this.lblCustomerCashTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblCustomerCashTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCustomerCashTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblCustomerCashTitle.Location = new System.Drawing.Point(12, 263);
            this.lblCustomerCashTitle.Name = "lblCustomerCashTitle";
            this.lblCustomerCashTitle.Size = new System.Drawing.Size(96, 17);
            this.lblCustomerCashTitle.TabIndex = 14;
            this.lblCustomerCashTitle.Text = "Tiền khách đưa:";
            // 
            // numCustomerCash
            // 
            this.numCustomerCash.BackColor = System.Drawing.Color.Transparent;
            this.numCustomerCash.BorderRadius = 6;
            this.numCustomerCash.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.numCustomerCash.Location = new System.Drawing.Point(135, 258);
            this.numCustomerCash.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numCustomerCash.Name = "numCustomerCash";
            this.numCustomerCash.Size = new System.Drawing.Size(203, 30);
            this.numCustomerCash.TabIndex = 15;
            this.numCustomerCash.ThousandsSeparator = true;
            this.numCustomerCash.UpDownButtonFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(182)))), ((int)(((byte)(116)))));
            this.numCustomerCash.ValueChanged += new System.EventHandler(this.numCustomerCash_ValueChanged);
            // 
            // pnlQuickCash
            // 
            this.pnlQuickCash.Controls.Add(this.btnCashExact);
            this.pnlQuickCash.Controls.Add(this.btnCash500k);
            this.pnlQuickCash.Controls.Add(this.btnCash1m);
            this.pnlQuickCash.Controls.Add(this.btnCash2m);
            this.pnlQuickCash.Location = new System.Drawing.Point(12, 294);
            this.pnlQuickCash.Name = "pnlQuickCash";
            this.pnlQuickCash.Size = new System.Drawing.Size(326, 32);
            this.pnlQuickCash.TabIndex = 16;
            // 
            // btnCashExact
            // 
            this.btnCashExact.BorderRadius = 4;
            this.btnCashExact.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCashExact.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnCashExact.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.btnCashExact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnCashExact.Location = new System.Drawing.Point(0, 0);
            this.btnCashExact.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.btnCashExact.Name = "btnCashExact";
            this.btnCashExact.Size = new System.Drawing.Size(76, 28);
            this.btnCashExact.TabIndex = 0;
            this.btnCashExact.Text = "Đủ tiền";
            this.btnCashExact.Click += new System.EventHandler(this.btnCashExact_Click);
            // 
            // btnCash500k
            // 
            this.btnCash500k.BorderRadius = 4;
            this.btnCash500k.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCash500k.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnCash500k.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.btnCash500k.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnCash500k.Location = new System.Drawing.Point(80, 0);
            this.btnCash500k.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.btnCash500k.Name = "btnCash500k";
            this.btnCash500k.Size = new System.Drawing.Size(76, 28);
            this.btnCash500k.TabIndex = 1;
            this.btnCash500k.Text = "500.000 đ";
            this.btnCash500k.Click += new System.EventHandler(this.btnCash500k_Click);
            // 
            // btnCash1m
            // 
            this.btnCash1m.BorderRadius = 4;
            this.btnCash1m.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCash1m.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnCash1m.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.btnCash1m.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnCash1m.Location = new System.Drawing.Point(160, 0);
            this.btnCash1m.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.btnCash1m.Name = "btnCash1m";
            this.btnCash1m.Size = new System.Drawing.Size(76, 28);
            this.btnCash1m.TabIndex = 2;
            this.btnCash1m.Text = "1.000.000 đ";
            this.btnCash1m.Click += new System.EventHandler(this.btnCash1m_Click);
            // 
            // btnCash2m
            // 
            this.btnCash2m.BorderRadius = 4;
            this.btnCash2m.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCash2m.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnCash2m.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.btnCash2m.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnCash2m.Location = new System.Drawing.Point(240, 0);
            this.btnCash2m.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.btnCash2m.Name = "btnCash2m";
            this.btnCash2m.Size = new System.Drawing.Size(80, 28);
            this.btnCash2m.TabIndex = 3;
            this.btnCash2m.Text = "2.000.000 đ";
            this.btnCash2m.Click += new System.EventHandler(this.btnCash2m_Click);
            // 
            // lblChangeDueTitle
            // 
            this.lblChangeDueTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblChangeDueTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblChangeDueTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblChangeDueTitle.Location = new System.Drawing.Point(12, 335);
            this.lblChangeDueTitle.Name = "lblChangeDueTitle";
            this.lblChangeDueTitle.Size = new System.Drawing.Size(109, 19);
            this.lblChangeDueTitle.TabIndex = 17;
            this.lblChangeDueTitle.Text = "Tiền thối lại khách:";
            // 
            // lblChangeDueValue
            // 
            this.lblChangeDueValue.AutoSize = false;
            this.lblChangeDueValue.BackColor = System.Drawing.Color.Transparent;
            this.lblChangeDueValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblChangeDueValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(125)))), ((int)(((byte)(96)))));
            this.lblChangeDueValue.Location = new System.Drawing.Point(135, 332);
            this.lblChangeDueValue.Name = "lblChangeDueValue";
            this.lblChangeDueValue.Size = new System.Drawing.Size(203, 24);
            this.lblChangeDueValue.TabIndex = 18;
            this.lblChangeDueValue.Text = "0 đ";
            this.lblChangeDueValue.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPaymentMethodTitle
            // 
            this.lblPaymentMethodTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblPaymentMethodTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblPaymentMethodTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPaymentMethodTitle.Location = new System.Drawing.Point(12, 368);
            this.lblPaymentMethodTitle.Name = "lblPaymentMethodTitle";
            this.lblPaymentMethodTitle.Size = new System.Drawing.Size(126, 15);
            this.lblPaymentMethodTitle.TabIndex = 19;
            this.lblPaymentMethodTitle.Text = "Phương thức thanh toán";
            // 
            // cboPaymentMethod
            // 
            this.cboPaymentMethod.BackColor = System.Drawing.Color.Transparent;
            this.cboPaymentMethod.BorderRadius = 6;
            this.cboPaymentMethod.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboPaymentMethod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboPaymentMethod.ItemHeight = 28;
            this.cboPaymentMethod.Items.AddRange(new object[] {
            "Tiền mặt",
            "Chuyển khoản",
            "Thẻ ngân hàng"});
            this.cboPaymentMethod.Location = new System.Drawing.Point(12, 387);
            this.cboPaymentMethod.Name = "cboPaymentMethod";
            this.cboPaymentMethod.Size = new System.Drawing.Size(326, 34);
            this.cboPaymentMethod.TabIndex = 20;
            // 
            // btnCheckout
            // 
            this.btnCheckout.Animated = true;
            this.btnCheckout.BorderRadius = 8;
            this.btnCheckout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckout.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(182)))), ((int)(((byte)(116)))));
            this.btnCheckout.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.btnCheckout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            this.btnCheckout.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(198)))), ((int)(((byte)(130)))));
            this.btnCheckout.Location = new System.Drawing.Point(12, 436);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(326, 48);
            this.btnCheckout.TabIndex = 21;
            this.btnCheckout.Text = "THANH TOÁN (F9)";
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
            // 
            // btnNewOrder
            // 
            this.btnNewOrder.Animated = true;
            this.btnNewOrder.BorderRadius = 6;
            this.btnNewOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewOrder.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnNewOrder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNewOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnNewOrder.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnNewOrder.Location = new System.Drawing.Point(12, 492);
            this.btnNewOrder.Name = "btnNewOrder";
            this.btnNewOrder.Size = new System.Drawing.Size(326, 36);
            this.btnNewOrder.TabIndex = 22;
            this.btnNewOrder.Text = "HỦY / TẠO ĐƠN MỚI (F4)";
            this.btnNewOrder.Click += new System.EventHandler(this.btnNewOrder_Click);
            // 
            // lblNotification
            // 
            this.lblNotification.AutoSize = false;
            this.lblNotification.BackColor = System.Drawing.Color.Transparent;
            this.lblNotification.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular);
            this.lblNotification.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblNotification.Location = new System.Drawing.Point(12, 536);
            this.lblNotification.Name = "lblNotification";
            this.lblNotification.Size = new System.Drawing.Size(326, 40);
            this.lblNotification.TabIndex = 23;
            this.lblNotification.Text = "";
            // 
            // pnlCartContainer
            // 
            this.pnlCartContainer.Controls.Add(this.dgvCart);
            this.pnlCartContainer.Controls.Add(this.pnlProductBar);
            this.pnlCartContainer.Controls.Add(this.pnlCartFooter);
            this.pnlCartContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCartContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlCartContainer.Name = "pnlCartContainer";
            this.pnlCartContainer.Size = new System.Drawing.Size(670, 680);
            this.pnlCartContainer.TabIndex = 0;
            // 
            // pnlProductBar
            // 
            this.pnlProductBar.BackColor = System.Drawing.Color.White;
            this.pnlProductBar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlProductBar.BorderThickness = 1;
            this.pnlProductBar.Controls.Add(this.lblSelectProduct);
            this.pnlProductBar.Controls.Add(this.cboProductSelector);
            this.pnlProductBar.Controls.Add(this.lblProductQty);
            this.pnlProductBar.Controls.Add(this.numProductQty);
            this.pnlProductBar.Controls.Add(this.lblWarrantyDate);
            this.pnlProductBar.Controls.Add(this.dtpProductWarranty);
            this.pnlProductBar.Controls.Add(this.btnQuickAdd);
            this.pnlProductBar.Controls.Add(this.btnScanQr);
            this.pnlProductBar.Controls.Add(this.lblProductInStock);
            this.pnlProductBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProductBar.Location = new System.Drawing.Point(0, 0);
            this.pnlProductBar.Name = "pnlProductBar";
            this.pnlProductBar.Padding = new System.Windows.Forms.Padding(8);
            this.pnlProductBar.Size = new System.Drawing.Size(670, 76);
            this.pnlProductBar.TabIndex = 0;
            // 
            // lblSelectProduct
            // 
            this.lblSelectProduct.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectProduct.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblSelectProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSelectProduct.Location = new System.Drawing.Point(8, 6);
            this.lblSelectProduct.Name = "lblSelectProduct";
            this.lblSelectProduct.Size = new System.Drawing.Size(126, 15);
            this.lblSelectProduct.TabIndex = 0;
            this.lblSelectProduct.Text = "Chọn / Tìm trang sức *";
            // 
            // cboProductSelector
            // 
            this.cboProductSelector.BackColor = System.Drawing.Color.Transparent;
            this.cboProductSelector.BorderRadius = 6;
            this.cboProductSelector.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboProductSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProductSelector.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboProductSelector.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboProductSelector.ItemHeight = 28;
            this.cboProductSelector.Location = new System.Drawing.Point(8, 25);
            this.cboProductSelector.Name = "cboProductSelector";
            this.cboProductSelector.Size = new System.Drawing.Size(240, 34);
            this.cboProductSelector.TabIndex = 1;
            this.cboProductSelector.SelectedIndexChanged += new System.EventHandler(this.cboProductSelector_SelectedIndexChanged);
            // 
            // lblProductQty
            // 
            this.lblProductQty.BackColor = System.Drawing.Color.Transparent;
            this.lblProductQty.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblProductQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblProductQty.Location = new System.Drawing.Point(256, 6);
            this.lblProductQty.Name = "lblProductQty";
            this.lblProductQty.Size = new System.Drawing.Size(51, 15);
            this.lblProductQty.TabIndex = 2;
            this.lblProductQty.Text = "Số lượng";
            // 
            // numProductQty
            // 
            this.numProductQty.BackColor = System.Drawing.Color.Transparent;
            this.numProductQty.BorderRadius = 6;
            this.numProductQty.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numProductQty.Location = new System.Drawing.Point(256, 25);
            this.numProductQty.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numProductQty.Name = "numProductQty";
            this.numProductQty.Size = new System.Drawing.Size(64, 30);
            this.numProductQty.TabIndex = 3;
            this.numProductQty.UpDownButtonFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(182)))), ((int)(((byte)(116)))));
            this.numProductQty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblWarrantyDate
            // 
            this.lblWarrantyDate.BackColor = System.Drawing.Color.Transparent;
            this.lblWarrantyDate.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblWarrantyDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblWarrantyDate.Location = new System.Drawing.Point(328, 6);
            this.lblWarrantyDate.Name = "lblWarrantyDate";
            this.lblWarrantyDate.Size = new System.Drawing.Size(77, 15);
            this.lblWarrantyDate.TabIndex = 4;
            this.lblWarrantyDate.Text = "Hạn bảo hành";
            // 
            // dtpProductWarranty
            // 
            this.dtpProductWarranty.BorderRadius = 6;
            this.dtpProductWarranty.Checked = true;
            this.dtpProductWarranty.CustomFormat = "dd/MM/yyyy";
            this.dtpProductWarranty.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dtpProductWarranty.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.dtpProductWarranty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            this.dtpProductWarranty.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpProductWarranty.Location = new System.Drawing.Point(328, 25);
            this.dtpProductWarranty.Name = "dtpProductWarranty";
            this.dtpProductWarranty.ShowCheckBox = true;
            this.dtpProductWarranty.Size = new System.Drawing.Size(120, 30);
            this.dtpProductWarranty.TabIndex = 5;
            // 
            // btnQuickAdd
            // 
            this.btnQuickAdd.Animated = true;
            this.btnQuickAdd.BorderRadius = 6;
            this.btnQuickAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuickAdd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(125)))), ((int)(((byte)(96)))));
            this.btnQuickAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnQuickAdd.ForeColor = System.Drawing.Color.White;
            this.btnQuickAdd.Location = new System.Drawing.Point(456, 25);
            this.btnQuickAdd.Name = "btnQuickAdd";
            this.btnQuickAdd.Size = new System.Drawing.Size(95, 30);
            this.btnQuickAdd.TabIndex = 6;
            this.btnQuickAdd.Text = "+ Thêm giỏ";
            this.btnQuickAdd.Click += new System.EventHandler(this.btnQuickAdd_Click);
            // 
            // btnScanQr
            // 
            this.btnScanQr.Animated = true;
            this.btnScanQr.BorderRadius = 6;
            this.btnScanQr.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnScanQr.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            this.btnScanQr.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnScanQr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(182)))), ((int)(((byte)(116)))));
            this.btnScanQr.Location = new System.Drawing.Point(558, 25);
            this.btnScanQr.Name = "btnScanQr";
            this.btnScanQr.Size = new System.Drawing.Size(104, 30);
            this.btnScanQr.TabIndex = 7;
            this.btnScanQr.Text = "📷 Quét QR";
            this.btnScanQr.Click += new System.EventHandler(this.btnScanQr_Click);
            // 
            // lblProductInStock
            // 
            this.lblProductInStock.AutoSize = false;
            this.lblProductInStock.BackColor = System.Drawing.Color.Transparent;
            this.lblProductInStock.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblProductInStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblProductInStock.Location = new System.Drawing.Point(8, 59);
            this.lblProductInStock.Name = "lblProductInStock";
            this.lblProductInStock.Size = new System.Drawing.Size(240, 15);
            this.lblProductInStock.TabIndex = 8;
            this.lblProductInStock.Text = "Tồn kho: 0 | Đơn giá: 0 đ";
            // 
            // dgvCart
            // 
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AllowUserToDeleteRows = false;
            this.dgvCart.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvCart.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCart.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCart.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCart.ColumnHeadersHeight = 36;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCart.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCart.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvCart.Location = new System.Drawing.Point(0, 76);
            this.dgvCart.MultiSelect = false;
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.ReadOnly = true;
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.RowTemplate.Height = 32;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(670, 560);
            this.dgvCart.TabIndex = 1;
            this.dgvCart.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvCart.ThemeStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.dgvCart.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvCart.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            this.dgvCart.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvCart.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvCart.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvCart.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvCart.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCart.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvCart.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvCart.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvCart.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            this.dgvCart.SelectionChanged += new System.EventHandler(this.dgvCart_SelectionChanged);
            // 
            // pnlCartFooter
            // 
            this.pnlCartFooter.BackColor = System.Drawing.Color.White;
            this.pnlCartFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlCartFooter.BorderThickness = 1;
            this.pnlCartFooter.Controls.Add(this.lblCartSummary);
            this.pnlCartFooter.Controls.Add(this.btnIncreaseQty);
            this.pnlCartFooter.Controls.Add(this.btnDecreaseQty);
            this.pnlCartFooter.Controls.Add(this.btnRemoveSelectedItem);
            this.pnlCartFooter.Controls.Add(this.btnClearCart);
            this.pnlCartFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCartFooter.Location = new System.Drawing.Point(0, 636);
            this.pnlCartFooter.Name = "pnlCartFooter";
            this.pnlCartFooter.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.pnlCartFooter.Size = new System.Drawing.Size(670, 44);
            this.pnlCartFooter.TabIndex = 2;
            // 
            // lblCartSummary
            // 
            this.lblCartSummary.BackColor = System.Drawing.Color.Transparent;
            this.lblCartSummary.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCartSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblCartSummary.Location = new System.Drawing.Point(12, 12);
            this.lblCartSummary.Name = "lblCartSummary";
            this.lblCartSummary.Size = new System.Drawing.Size(126, 17);
            this.lblCartSummary.TabIndex = 0;
            this.lblCartSummary.Text = "Giỏ hàng: 0 sản phẩm";
            // 
            // btnIncreaseQty
            // 
            this.btnIncreaseQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIncreaseQty.Animated = true;
            this.btnIncreaseQty.BorderRadius = 4;
            this.btnIncreaseQty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIncreaseQty.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnIncreaseQty.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnIncreaseQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnIncreaseQty.Location = new System.Drawing.Point(290, 7);
            this.btnIncreaseQty.Name = "btnIncreaseQty";
            this.btnIncreaseQty.Size = new System.Drawing.Size(64, 30);
            this.btnIncreaseQty.TabIndex = 1;
            this.btnIncreaseQty.Text = "+ SL";
            this.btnIncreaseQty.Click += new System.EventHandler(this.btnIncreaseQty_Click);
            // 
            // btnDecreaseQty
            // 
            this.btnDecreaseQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDecreaseQty.Animated = true;
            this.btnDecreaseQty.BorderRadius = 4;
            this.btnDecreaseQty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDecreaseQty.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnDecreaseQty.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDecreaseQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnDecreaseQty.Location = new System.Drawing.Point(360, 7);
            this.btnDecreaseQty.Name = "btnDecreaseQty";
            this.btnDecreaseQty.Size = new System.Drawing.Size(64, 30);
            this.btnDecreaseQty.TabIndex = 2;
            this.btnDecreaseQty.Text = "- SL";
            this.btnDecreaseQty.Click += new System.EventHandler(this.btnDecreaseQty_Click);
            // 
            // btnRemoveSelectedItem
            // 
            this.btnRemoveSelectedItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveSelectedItem.Animated = true;
            this.btnRemoveSelectedItem.BorderRadius = 4;
            this.btnRemoveSelectedItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemoveSelectedItem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnRemoveSelectedItem.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnRemoveSelectedItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnRemoveSelectedItem.Location = new System.Drawing.Point(432, 7);
            this.btnRemoveSelectedItem.Name = "btnRemoveSelectedItem";
            this.btnRemoveSelectedItem.Size = new System.Drawing.Size(110, 30);
            this.btnRemoveSelectedItem.TabIndex = 3;
            this.btnRemoveSelectedItem.Text = "🗑 Xóa dòng";
            this.btnRemoveSelectedItem.Click += new System.EventHandler(this.btnRemoveSelectedItem_Click);
            // 
            // btnClearCart
            // 
            this.btnClearCart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearCart.Animated = true;
            this.btnClearCart.BorderRadius = 4;
            this.btnClearCart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearCart.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnClearCart.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnClearCart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnClearCart.Location = new System.Drawing.Point(550, 7);
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Size = new System.Drawing.Size(112, 30);
            this.btnClearCart.TabIndex = 4;
            this.btnClearCart.Text = "✕ Làm sạch giỏ";
            this.btnClearCart.Click += new System.EventHandler(this.btnClearCart_Click);
            // 
            // FrmBanHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1020, 680);
            this.Controls.Add(this.pnlCartContainer);
            this.Controls.Add(this.pnlCheckoutContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.KeyPreview = true;
            this.Name = "FrmBanHang";
            this.Text = "Bán Hàng POS";
            this.Load += new System.EventHandler(this.FrmBanHang_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmBanHang_KeyDown);
            this.pnlCheckoutContainer.ResumeLayout(false);
            this.pnlCheckoutContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCustomerCash)).EndInit();
            this.pnlQuickCash.ResumeLayout(false);
            this.pnlCartContainer.ResumeLayout(false);
            this.pnlProductBar.ResumeLayout(false);
            this.pnlProductBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProductQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.pnlCartFooter.ResumeLayout(false);
            this.pnlCartFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        private Guna.UI2.WinForms.Guna2Panel pnlCheckoutContainer;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCheckoutHeader;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCustomerTitle;
        private Guna.UI2.WinForms.Guna2ComboBox cboCustomer;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCustomerInfo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCashierTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCashierInfo;
        private Guna.UI2.WinForms.Guna2Panel pnlDivider1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubTotalTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubTotalValue;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDiscountTitle;
        private Guna.UI2.WinForms.Guna2NumericUpDown numDiscount;
        private Guna.UI2.WinForms.Guna2Panel pnlDivider2;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTotalPayableTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTotalPayableValue;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCustomerCashTitle;
        private Guna.UI2.WinForms.Guna2NumericUpDown numCustomerCash;
        private System.Windows.Forms.FlowLayoutPanel pnlQuickCash;
        private Guna.UI2.WinForms.Guna2Button btnCashExact;
        private Guna.UI2.WinForms.Guna2Button btnCash500k;
        private Guna.UI2.WinForms.Guna2Button btnCash1m;
        private Guna.UI2.WinForms.Guna2Button btnCash2m;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblChangeDueTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblChangeDueValue;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPaymentMethodTitle;
        private Guna.UI2.WinForms.Guna2ComboBox cboPaymentMethod;
        private Guna.UI2.WinForms.Guna2Button btnCheckout;
        private Guna.UI2.WinForms.Guna2Button btnNewOrder;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNotification;
        private Guna.UI2.WinForms.Guna2Panel pnlCartContainer;
        private Guna.UI2.WinForms.Guna2Panel pnlProductBar;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSelectProduct;
        private Guna.UI2.WinForms.Guna2ComboBox cboProductSelector;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblProductQty;
        private Guna.UI2.WinForms.Guna2NumericUpDown numProductQty;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblWarrantyDate;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpProductWarranty;
        private Guna.UI2.WinForms.Guna2Button btnQuickAdd;
        private Guna.UI2.WinForms.Guna2Button btnScanQr;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblProductInStock;
        private Guna.UI2.WinForms.Guna2DataGridView dgvCart;
        private Guna.UI2.WinForms.Guna2Panel pnlCartFooter;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCartSummary;
        private Guna.UI2.WinForms.Guna2Button btnIncreaseQty;
        private Guna.UI2.WinForms.Guna2Button btnDecreaseQty;
        private Guna.UI2.WinForms.Guna2Button btnRemoveSelectedItem;
        private Guna.UI2.WinForms.Guna2Button btnClearCart;
    }
}
