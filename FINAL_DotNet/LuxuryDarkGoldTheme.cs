using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FINAL_DotNet
{
    /// <summary>
    /// Giao diện dùng chung theo design system Luxury Dark Gold.
    /// Lớp này chỉ thay đổi cách trình bày, không can thiệp dữ liệu hay nghiệp vụ.
    /// </summary>
    internal static class LuxuryDarkGoldTheme
    {
        internal static readonly Color Background = Color.FromArgb(30, 27, 24);
        internal static readonly Color Card = Color.FromArgb(42, 38, 34);
        internal static readonly Color Selection = Color.FromArgb(58, 52, 46);
        internal static readonly Color Gold = Color.FromArgb(212, 175, 55);
        internal static readonly Color GoldHover = Color.FromArgb(232, 195, 75);
        internal static readonly Color Border = Color.FromArgb(58, 52, 46);
        internal static readonly Color InputBorder = Color.FromArgb(70, 64, 57);
        internal static readonly Color InputHoverBorder = Color.FromArgb(180, 174, 168);
        internal static readonly Color TextPrimary = Color.FromArgb(245, 240, 235);
        internal static readonly Color TextSecondary = Color.FromArgb(180, 174, 168);
        internal static readonly Color Danger = Color.FromArgb(198, 40, 40);
        internal static readonly Color DangerHover = Color.FromArgb(229, 57, 53);
        internal static readonly Color Success = Color.FromArgb(46, 125, 50);
        internal static readonly Color Warning = Color.FromArgb(230, 81, 0);

        private const int ProgressBarSetBarColor = 0x0409;
        private const int ProgressBarSetBackgroundColor = 0x2001;
        private static readonly ConditionalWeakTable<Control, ThemeState> States =
            new ConditionalWeakTable<Control, ThemeState>();

        internal static bool IsThemeEnabled
        {
            get => false;
            set { /* Disabled: Clean native Guna2 luxury theme is enforced */ }
        }

        internal static void Apply(Form form)
        {
            // Neutralized: Form styling is handled natively by Guna2 controls
            return;
        }

        internal static void ActivateNavigation(Button selected, Control root)
        {
            if (!IsThemeEnabled) return;

            if (selected == null || root == null)
            {
                return;
            }

            foreach (Button button in Descendants(root).OfType<Button>())
            {
                ThemeState state = States.GetOrCreateValue(button);
                if (state.ButtonRole != ButtonRole.Navigation)
                {
                    continue;
                }

                state.NavigationActive = button == selected;
                ApplyButtonPalette(button, state, false);
                button.Invalidate();
            }
        }

        private static System.Collections.Generic.IEnumerable<Control> Descendants(Control root)
        {
            foreach (Control child in root.Controls)
            {
                yield return child;
                foreach (Control descendant in Descendants(child))
                {
                    yield return descendant;
                }
            }
        }

        private static bool IsExcluded(Form form)
        {
            string name = form.GetType().Name;
            return name == "Form1" || name == "FormDangKy" || name == "FormDoiMatKhau";
        }

        private static void ApplyControl(Control control)
        {
            ThemeState state = States.GetOrCreateValue(control);
            if (!state.Initialized)
            {
                state.Initialized = true;
                StyleControl(control, state);
                control.ControlAdded += Control_ControlAdded;
            }

            foreach (Control child in control.Controls)
            {
                ApplyControl(child);
            }
        }

        private static void Control_ControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control != null)
            {
                ApplyControl(e.Control);
            }
        }

        private static void StyleControl(Control control, ThemeState state)
        {
            if (control is Form)
            {
                return;
            }

            DataGridView grid = control as DataGridView;
            if (grid != null)
            {
                StyleGrid(grid);
                return;
            }

            TabControl tabs = control as TabControl;
            if (tabs != null)
            {
                StyleTabs(tabs);
                return;
            }

            Button button = control as Button;
            if (button != null)
            {
                StyleButton(button, state);
                return;
            }

            TextBoxBase textBox = control as TextBoxBase;
            if (textBox != null)
            {
                StyleTextBox(textBox);
                return;
            }

            ComboBox comboBox = control as ComboBox;
            if (comboBox != null)
            {
                StyleComboBox(comboBox);
                return;
            }

            NumericUpDown numeric = control as NumericUpDown;
            if (numeric != null)
            {
                StyleNumeric(numeric);
                return;
            }

            DateTimePicker dateTimePicker = control as DateTimePicker;
            if (dateTimePicker != null)
            {
                StyleDateTimePicker(dateTimePicker);
                return;
            }

            CheckBox checkBox = control as CheckBox;
            if (checkBox != null)
            {
                StyleCheckBox(checkBox);
                return;
            }

            ListBox listBox = control as ListBox;
            if (listBox != null)
            {
                StyleListBox(listBox);
                return;
            }

            ProgressBar progressBar = control as ProgressBar;
            if (progressBar != null)
            {
                StyleProgressBar(progressBar);
                return;
            }

            Chart chart = control as Chart;
            if (chart != null)
            {
                StyleChart(chart);
                return;
            }

            ToolStrip toolStrip = control as ToolStrip;
            if (toolStrip != null)
            {
                StyleToolStrip(toolStrip);
                return;
            }

            Label label = control as Label;
            if (label != null)
            {
                StyleLabel(label);
                return;
            }

            SplitContainer splitContainer = control as SplitContainer;
            if (splitContainer != null)
            {
                splitContainer.BackColor = Border;
                splitContainer.ForeColor = TextPrimary;
                splitContainer.SplitterWidth = Math.Max(5, splitContainer.SplitterWidth);
                return;
            }

            TabPage tabPage = control as TabPage;
            if (tabPage != null)
            {
                tabPage.BackColor = Background;
                tabPage.ForeColor = TextPrimary;
                tabPage.Font = FontOf(9.75F);
                return;
            }

            Panel panel = control as Panel;
            if (panel != null)
            {
                StylePanel(panel);
                return;
            }

            GroupBox groupBox = control as GroupBox;
            if (groupBox != null)
            {
                groupBox.BackColor = Card;
                groupBox.ForeColor = Gold;
                groupBox.Font = FontOf(11.25F, FontStyle.Bold);
                return;
            }

            PictureBox pictureBox = control as PictureBox;
            if (pictureBox != null)
            {
                pictureBox.BackColor = Background;
                return;
            }

            control.BackColor = control.Parent == null ? Background : control.Parent.BackColor;
            control.ForeColor = TextPrimary;
            control.Font = FontOf(9.75F);
        }

        private static void StylePanel(Panel panel)
        {
            string name = Normalize(panel.Name);
            bool isMainSurface = name.Contains("content") || name.Contains("sidebar") ||
                                 name.Contains("flowmenu") || name.Contains("fluent") ||
                                 name == "panel1" || name == "panel2";
            bool isBar = name.Contains("boloc") || name.Contains("thaotac") ||
                         name.Contains("chan") || name.Contains("header");
            panel.BackColor = isMainSurface ? Background : (isBar ? Card : Background);
            panel.ForeColor = TextPrimary;
        }

        private static void CardPanel_Paint(object sender, PaintEventArgs e)
        {
            Control panel = sender as Control;
            if (panel == null || panel.Width < 3 || panel.Height < 3)
            {
                return;
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath path = RoundedPath(new Rectangle(0, 0, panel.Width - 1, panel.Height - 1), 8))
            using (Pen pen = new Pen(Border))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        private static void RoundedPanel_Resize(object sender, EventArgs e)
        {
            SetRoundedRegion(sender as Control, 8);
        }

        private static void StyleLabel(Label label)
        {
            string name = Normalize(label.Name);
            bool isStatus = name.Contains("trangthai") || name.Contains("thongbao") ||
                            name.Contains("tientrinh") || name.Contains("ketqua") || name.Contains("loi");
            bool isTitle = name.Contains("tieude") || name.Contains("thuonghieu") ||
                           name.Contains("noidungchinh") ||
                           (label.Font.Bold && label.Font.Size >= 11F);

            label.BackColor = Color.Transparent;
            label.ForeColor = name.Contains("loi") ? Danger : (isTitle ? TextPrimary : TextSecondary);
            if (name.Contains("thuonghieu"))
            {
                label.ForeColor = Gold;
                label.Font = FontOf(15F, FontStyle.Bold);
            }
            else if (isTitle)
            {
                label.Font = FontOf(Math.Max(11.25F, label.Font.Size), FontStyle.Bold);
            }
            else if (isStatus)
            {
                label.Font = FontOf(9F, FontStyle.Italic);
            }
            else
            {
                label.Font = FontOf(9.75F, label.Font.Bold ? FontStyle.Bold : FontStyle.Regular);
            }

            if (name.Contains("vaitro"))
            {
                label.BackColor = Selection;
                label.ForeColor = Gold;
                label.Font = FontOf(9.75F, FontStyle.Bold);
            }
        }

        private static void StyleTextBox(TextBoxBase textBox)
        {
            textBox.BackColor = Background;
            textBox.ForeColor = TextPrimary;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = FontOf(textBox.Multiline ? 9F : 9.75F);
            textBox.Enter += Input_Enter;
            textBox.Leave += Input_Leave;
            textBox.MouseEnter += Input_MouseEnter;
            textBox.MouseLeave += Input_MouseLeave;
        }

        private static void StyleComboBox(ComboBox comboBox)
        {
            comboBox.BackColor = Background;
            comboBox.ForeColor = TextPrimary;
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.Font = FontOf(9.75F);
            comboBox.Enter += Input_Enter;
            comboBox.Leave += Input_Leave;
            comboBox.MouseEnter += Input_MouseEnter;
            comboBox.MouseLeave += Input_MouseLeave;
        }

        private static void StyleNumeric(NumericUpDown numeric)
        {
            numeric.BackColor = Background;
            numeric.ForeColor = TextPrimary;
            numeric.BorderStyle = BorderStyle.FixedSingle;
            numeric.Font = FontOf(9.75F);
            numeric.Enter += Input_Enter;
            numeric.Leave += Input_Leave;
        }

        private static void StyleDateTimePicker(DateTimePicker picker)
        {
            picker.BackColor = Gold;
            picker.ForeColor = Background;
            picker.CalendarMonthBackground = Background;
            picker.CalendarForeColor = TextPrimary;
            picker.CalendarTitleBackColor = Gold;
            picker.CalendarTitleForeColor = Background;
            picker.Font = FontOf(9.75F, FontStyle.Bold);
            DisableNativeTheme(picker);
            picker.HandleCreated += delegate { DisableNativeTheme(picker); };
        }

        private static void DisableNativeTheme(Control control)
        {
            if (control.IsHandleCreated)
            {
                SetWindowTheme(control.Handle, string.Empty, string.Empty);
            }
        }

        private static void StyleCheckBox(CheckBox checkBox)
        {
            checkBox.BackColor = Color.Transparent;
            checkBox.ForeColor = TextPrimary;
            checkBox.Font = FontOf(9.75F, FontStyle.Bold);
            checkBox.FlatStyle = FlatStyle.Flat;
            checkBox.FlatAppearance.BorderColor = Gold;
            checkBox.FlatAppearance.CheckedBackColor = Gold;
            checkBox.FlatAppearance.MouseOverBackColor = Selection;
        }

        private static void StyleListBox(ListBox listBox)
        {
            listBox.BackColor = Background;
            listBox.ForeColor = TextPrimary;
            listBox.BorderStyle = BorderStyle.FixedSingle;
            listBox.Font = FontOf(9.75F);
        }

        private static void StyleGrid(DataGridView grid)
        {
            grid.EnableHeadersVisualStyles = false;
            grid.BackgroundColor = Card;
            grid.BorderStyle = BorderStyle.None;
            grid.GridColor = Border;
            grid.RowHeadersVisible = false;
            OptimizeGridColumns(grid);
            grid.DataBindingComplete += Grid_DataBindingComplete;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grid.ColumnHeadersHeight = Math.Max(40, grid.ColumnHeadersHeight);
            grid.ColumnHeadersDefaultCellStyle.BackColor = Background;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Gold;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Background;
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Gold;
            grid.ColumnHeadersDefaultCellStyle.Font = FontOf(9.75F, FontStyle.Bold);
            grid.DefaultCellStyle.BackColor = Card;
            grid.DefaultCellStyle.ForeColor = TextPrimary;
            grid.DefaultCellStyle.SelectionBackColor = Selection;
            grid.DefaultCellStyle.SelectionForeColor = Gold;
            grid.DefaultCellStyle.Font = FontOf(9F);
            grid.DefaultCellStyle.Padding = new Padding(4, 2, 4, 2);
            grid.AlternatingRowsDefaultCellStyle.BackColor = Card;
            grid.AlternatingRowsDefaultCellStyle.ForeColor = TextPrimary;
            grid.AlternatingRowsDefaultCellStyle.SelectionBackColor = Selection;
            grid.AlternatingRowsDefaultCellStyle.SelectionForeColor = Gold;
            grid.RowTemplate.Height = Math.Max(40, grid.RowTemplate.Height);
        }

        private static void Grid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            OptimizeGridColumns(sender as DataGridView);
        }

        private static void OptimizeGridColumns(DataGridView grid)
        {
            if (grid == null)
            {
                return;
            }

            grid.AutoSizeColumnsMode = grid.Columns.Count > 6
                ? DataGridViewAutoSizeColumnsMode.None
                : DataGridViewAutoSizeColumnsMode.Fill;

            if (grid.Columns.Count <= 6)
            {
                return;
            }

            foreach (DataGridViewColumn column in grid.Columns)
            {
                column.Width = Math.Max(88, Math.Min(220, column.Width));
                string name = Normalize(column.Name + column.DataPropertyName);
                if (name.Contains("gia") || name.Contains("tien") || name.Contains("tong") ||
                    name.Contains("diem") || name.Contains("soluong"))
                {
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
        }

        private static void StyleTabs(TabControl tabs)
        {
            tabs.BackColor = Background;
            tabs.ForeColor = TextSecondary;
            tabs.Font = FontOf(9.75F, FontStyle.Bold);
            tabs.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabs.Padding = new Point(14, 6);
            tabs.DrawItem += Tabs_DrawItem;
            tabs.SelectedIndexChanged += Tabs_SelectedIndexChanged;
        }

        private static void Tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            Control tabs = sender as Control;
            if (tabs != null)
            {
                tabs.Invalidate();
            }
        }

        private static void Tabs_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tabs = sender as TabControl;
            if (tabs == null || e.Index < 0 || e.Index >= tabs.TabPages.Count)
            {
                return;
            }

            bool selected = e.Index == tabs.SelectedIndex;
            Rectangle bounds = e.Bounds;
            using (SolidBrush background = new SolidBrush(selected ? Card : Background))
            using (SolidBrush foreground = new SolidBrush(selected ? Gold : TextSecondary))
            using (Pen accent = new Pen(Gold, 3F))
            using (StringFormat format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                e.Graphics.FillRectangle(background, bounds);
                e.Graphics.DrawString(tabs.TabPages[e.Index].Text, tabs.Font, foreground, bounds, format);
                if (selected)
                {
                    e.Graphics.DrawLine(accent, bounds.Left + 4, bounds.Bottom - 2, bounds.Right - 4, bounds.Bottom - 2);
                }
            }
        }

        private static void StyleButton(Button button, ThemeState state)
        {
            state.ButtonRole = GetButtonRole(button);
            button.Cursor = Cursors.Hand;
            button.FlatStyle = FlatStyle.Flat;
            button.UseVisualStyleBackColor = false;
            button.Font = FontOf(state.ButtonRole == ButtonRole.Primary ? 11.25F :
                (state.ButtonRole == ButtonRole.Group ? 8.5F : 9.75F), FontStyle.Bold);
            bool menuButton = state.ButtonRole == ButtonRole.Navigation || state.ButtonRole == ButtonRole.Group;
            button.Padding = menuButton ? new Padding(15, 0, 0, 0) : button.Padding;
            button.TextAlign = menuButton ? ContentAlignment.MiddleLeft : button.TextAlign;
            ApplyButtonPalette(button, state, false);
            button.MouseEnter += Button_MouseEnter;
            button.MouseLeave += Button_MouseLeave;
            button.EnabledChanged += Button_EnabledChanged;
            button.Resize += RoundedButton_Resize;
            SetRoundedRegion(button, state.ButtonRole == ButtonRole.Navigation ? 0 : 6);

            if (state.ButtonRole == ButtonRole.Navigation)
            {
                button.Click += NavigationButton_Click;
                button.Paint += NavigationButton_Paint;
                state.NavigationActive = Normalize(button.Text) == "tongquan";
                ApplyButtonPalette(button, state, false);
            }
        }

        private static ButtonRole GetButtonRole(Button button)
        {
            string name = Normalize(button.Name);
            string semantic = string.IsNullOrEmpty(name) ? Normalize(button.Text) : name;
            string parentName = button.Parent == null ? string.Empty : Normalize(button.Parent.Name);
            if (semantic.StartsWith("btnnhom"))
            {
                return ButtonRole.Group;
            }
            if (parentName.Contains("flowmenu") || parentName.Contains("nhommenu") || semantic.StartsWith("btnnav"))
            {
                return ButtonRole.Navigation;
            }

            if (semantic.Contains("xoahoactrangthai") || semantic.Contains("huyhoadon") ||
                semantic.Contains("huyphieu") || semantic.Contains("phuchoi") ||
                semantic.Contains("khoamau") || semantic.Contains("dangxuat"))
            {
                return ButtonRole.Danger;
            }

            bool isAuxiliary = semantic.Contains("tim") || semantic.Contains("tailai") ||
                               semantic.Contains("lammoi") || semantic.Contains("moi") ||
                               semantic.Contains("chon") || semantic.Contains("xem") ||
                               semantic.StartsWith("btnin") || semantic.Contains("inhoadon") ||
                               semantic.Contains("inphieu") || semantic.Contains("xuat") ||
                               semantic.Contains("tep") || semantic.Contains("file") ||
                               semantic.Contains("themdong") || semantic.Contains("xoadong") ||
                               semantic.Contains("moidong") || semantic.Contains("xoathanhphan") ||
                               semantic.Contains("moithanhphan") || semantic.Contains("taoten") ||
                               semantic.Contains("taimau") || semantic.Contains("tainguoinhan") ||
                               semantic.Contains("doitrangthai") || semantic.Contains("kiemtra");
            if (isAuxiliary)
            {
                return ButtonRole.Secondary;
            }

            if (semantic.Contains("them") || semantic.Contains("capnhat") || semantic.Contains("luu") ||
                semantic.Contains("gui") || semantic.Contains("import") || semantic.Contains("tiepnhan") ||
                semantic.Contains("captaikhoan") || semantic.Contains("saoluu"))
            {
                return ButtonRole.Primary;
            }

            return ButtonRole.Secondary;
        }

        private static void Button_MouseEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null || !button.Enabled)
            {
                return;
            }

            ApplyButtonPalette(button, States.GetOrCreateValue(button), true);
        }

        private static void Button_MouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                ApplyButtonPalette(button, States.GetOrCreateValue(button), false);
            }
        }

        private static void Button_EnabledChanged(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                ApplyButtonPalette(button, States.GetOrCreateValue(button), false);
            }
        }

        private static void NavigationButton_Click(object sender, EventArgs e)
        {
            Button selected = sender as Button;
            if (selected == null || selected.Parent == null)
            {
                return;
            }

            foreach (Button button in selected.Parent.Controls.OfType<Button>())
            {
                ThemeState state = States.GetOrCreateValue(button);
                state.NavigationActive = button == selected;
                ApplyButtonPalette(button, state, false);
                button.Invalidate();
            }
        }

        private static void NavigationButton_Paint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            if (button == null || !States.GetOrCreateValue(button).NavigationActive)
            {
                return;
            }

            using (SolidBrush accent = new SolidBrush(Gold))
            {
                e.Graphics.FillRectangle(accent, 0, 0, 4, button.Height);
            }
        }

        private static void ApplyButtonPalette(Button button, ThemeState state, bool hovered)
        {
            if (!button.Enabled)
            {
                button.BackColor = InputBorder;
                button.ForeColor = TextSecondary;
                button.FlatAppearance.BorderColor = InputBorder;
                button.FlatAppearance.BorderSize = 1;
                return;
            }

            switch (state.ButtonRole)
            {
                case ButtonRole.Primary:
                    button.BackColor = hovered ? GoldHover : Gold;
                    button.ForeColor = Background;
                    button.FlatAppearance.BorderColor = hovered ? GoldHover : Gold;
                    button.FlatAppearance.BorderSize = 1;
                    break;
                case ButtonRole.Danger:
                    button.BackColor = hovered ? DangerHover : Danger;
                    button.ForeColor = Color.White;
                    button.FlatAppearance.BorderColor = hovered ? DangerHover : Danger;
                    button.FlatAppearance.BorderSize = 1;
                    break;
                case ButtonRole.Navigation:
                    button.BackColor = state.NavigationActive || hovered ? Card : Background;
                    button.ForeColor = state.NavigationActive || hovered ? Gold : TextSecondary;
                    button.FlatAppearance.BorderColor = Background;
                    button.FlatAppearance.BorderSize = 0;
                    break;
                case ButtonRole.Group:
                    button.BackColor = hovered ? Card : Background;
                    button.ForeColor = hovered ? Gold : TextSecondary;
                    button.FlatAppearance.BorderColor = Background;
                    button.FlatAppearance.BorderSize = 0;
                    break;
                default:
                    button.BackColor = hovered ? Selection : Card;
                    button.ForeColor = hovered ? GoldHover : Gold;
                    button.FlatAppearance.BorderColor = hovered ? GoldHover : Gold;
                    button.FlatAppearance.BorderSize = 1;
                    break;
            }

            button.FlatAppearance.MouseOverBackColor = button.BackColor;
            button.FlatAppearance.MouseDownBackColor = Selection;
        }

        private static void RoundedButton_Resize(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
            {
                return;
            }

            ThemeState state = States.GetOrCreateValue(button);
            SetRoundedRegion(button, state.ButtonRole == ButtonRole.Navigation ? 0 : 6);
        }

        private static void Input_Enter(object sender, EventArgs e)
        {
            Control input = sender as Control;
            if (input != null)
            {
                input.BackColor = Selection;
            }
        }

        private static void Input_Leave(object sender, EventArgs e)
        {
            Control input = sender as Control;
            if (input != null)
            {
                input.BackColor = Background;
            }
        }

        private static void Input_MouseEnter(object sender, EventArgs e)
        {
            Control input = sender as Control;
            if (input != null && !input.Focused)
            {
                input.BackColor = Color.FromArgb(38, 34, 30);
            }
        }

        private static void Input_MouseLeave(object sender, EventArgs e)
        {
            Control input = sender as Control;
            if (input != null && !input.Focused)
            {
                input.BackColor = Background;
            }
        }

        private static void StyleProgressBar(ProgressBar progressBar)
        {
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.BackColor = Background;
            progressBar.ForeColor = Gold;
            SetProgressBarColors(progressBar);
            progressBar.HandleCreated += delegate { SetProgressBarColors(progressBar); };
        }

        private static void SetProgressBarColors(ProgressBar progressBar)
        {
            if (progressBar.IsHandleCreated)
            {
                SendMessage(progressBar.Handle, ProgressBarSetBackgroundColor, IntPtr.Zero, ColorRef(Background));
                SendMessage(progressBar.Handle, ProgressBarSetBarColor, IntPtr.Zero, ColorRef(Gold));
            }
        }

        private static IntPtr ColorRef(Color color)
        {
            return (IntPtr)(color.R | (color.G << 8) | (color.B << 16));
        }

        private static void StyleChart(Chart chart)
        {
            chart.BackColor = Card;
            chart.ForeColor = TextPrimary;
            chart.Palette = ChartColorPalette.None;
            chart.PaletteCustomColors = new[] { Gold, GoldHover, Success, Warning, Danger };
            foreach (ChartArea area in chart.ChartAreas)
            {
                area.BackColor = Card;
                area.AxisX.LineColor = Border;
                area.AxisY.LineColor = Border;
                area.AxisX.MajorGrid.LineColor = Border;
                area.AxisY.MajorGrid.LineColor = Border;
                area.AxisX.LabelStyle.ForeColor = TextSecondary;
                area.AxisY.LabelStyle.ForeColor = TextSecondary;
                area.AxisX.LabelStyle.Font = FontOf(8.25F);
                area.AxisY.LabelStyle.Font = FontOf(8.25F);
            }

            foreach (Legend legend in chart.Legends)
            {
                legend.BackColor = Card;
                legend.ForeColor = TextSecondary;
                legend.Font = FontOf(8.25F);
            }
        }

        private static void StyleToolStrip(ToolStrip toolStrip)
        {
            toolStrip.BackColor = Card;
            toolStrip.ForeColor = TextPrimary;
            toolStrip.Font = FontOf(9F);
            toolStrip.RenderMode = ToolStripRenderMode.Professional;
            toolStrip.Renderer = new ToolStripProfessionalRenderer(new DarkGoldColorTable());
        }

        private static void SetRoundedRegion(Control control, int radius)
        {
            if (control == null || radius <= 0 || control.Width < 2 || control.Height < 2)
            {
                if (control != null && radius <= 0)
                {
                    control.Region = null;
                }
                return;
            }

            using (GraphicsPath path = RoundedPath(new Rectangle(0, 0, control.Width, control.Height), radius))
            {
                Region oldRegion = control.Region;
                control.Region = new Region(path);
                if (oldRegion != null)
                {
                    oldRegion.Dispose();
                }
            }
        }

        private static GraphicsPath RoundedPath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = Math.Min(radius * 2, Math.Min(bounds.Width, bounds.Height));
            if (diameter <= 1)
            {
                path.AddRectangle(bounds);
                return path;
            }

            Rectangle arc = new Rectangle(bounds.Location, new Size(diameter, diameter));
            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }

        private static Font FontOf(float size, FontStyle style = FontStyle.Regular)
        {
            return new Font("Segoe UI", size, style, GraphicsUnit.Point);
        }

        private static string Normalize(string value)
        {
            string source = (value ?? string.Empty).Replace("_", string.Empty).ToLowerInvariant();
            string decomposed = source.Normalize(NormalizationForm.FormD);
            StringBuilder result = new StringBuilder(decomposed.Length);
            foreach (char character in decomposed)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(character) != UnicodeCategory.NonSpacingMark)
                {
                    result.Append(character == 'đ' ? 'd' : character);
                }
            }
            return result.ToString().Normalize(NormalizationForm.FormC);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr handle, int message, IntPtr wParam, IntPtr lParam);

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr handle, string subAppName, string subIdList);

        private enum ButtonRole
        {
            Primary,
            Secondary,
            Danger,
            Navigation,
            Group
        }

        private sealed class ThemeState
        {
            internal bool Initialized;
            internal bool NavigationActive;
            internal ButtonRole ButtonRole;
        }

        private sealed class DarkGoldColorTable : ProfessionalColorTable
        {
            public override Color ToolStripGradientBegin { get { return Card; } }
            public override Color ToolStripGradientMiddle { get { return Card; } }
            public override Color ToolStripGradientEnd { get { return Card; } }
            public override Color MenuItemSelected { get { return Selection; } }
            public override Color MenuItemBorder { get { return Gold; } }
            public override Color ButtonSelectedHighlight { get { return Selection; } }
            public override Color ButtonSelectedBorder { get { return Gold; } }
            public override Color ButtonPressedHighlight { get { return Selection; } }
            public override Color ToolStripBorder { get { return Border; } }
            public override Color SeparatorDark { get { return Border; } }
            public override Color SeparatorLight { get { return InputBorder; } }
        }
    }
}
