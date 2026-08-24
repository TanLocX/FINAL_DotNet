using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    /// <summary>
    /// Chuẩn hóa bố cục theo Fluent Desktop. Lớp chỉ sắp xếp lại control có sẵn,
    /// không thay đổi event, binding dữ liệu hay xử lý nghiệp vụ của biểu mẫu.
    /// </summary>
    internal static class FluentDesktopLayout
    {
        private const int Space4 = 4;
        private const int Space8 = 8;
        private const int Space12 = 12;
        private const int Space16 = 16;
        private const int Space24 = 24;
        private const int InputHeight = 36;
        private const int ButtonHeight = 40;

        private static readonly ConditionalWeakTable<Form, LayoutState> States =
            new ConditionalWeakTable<Form, LayoutState>();

        internal static void Apply(Form form)
        {
            if (form == null || States.GetOrCreateValue(form).Applied)
            {
                return;
            }

            States.GetOrCreateValue(form).Applied = true;
            form.AutoScaleMode = AutoScaleMode.Dpi;
            form.SuspendLayout();

            ConfigureFilterBars(form);
            ConfigureCrudLayout(form);
            ConfigureCommonControls(form);

            form.ResumeLayout(true);
        }

        private static void ConfigureFilterBars(Form form)
        {
            foreach (Panel filter in FindControls<Panel>(form).Where(item =>
                string.Equals(item.Name, "pnlBoLoc", StringComparison.OrdinalIgnoreCase)))
            {
                ReflowFilterBar(filter);
            }
        }

        private static void ReflowFilterBar(Panel filter)
        {
            Control[] original = filter.Controls.Cast<Control>().ToArray();
            if (original.Length == 0 || original.Any(item => item.Name == "flpFluentBoLoc"))
            {
                return;
            }

            List<Control> inputs = original
                .Where(IsFilterInput)
                .OrderBy(item => item.Top)
                .ThenBy(item => item.Left)
                .ToList();
            List<Button> buttons = original.OfType<Button>()
                .OrderBy(item => item.Left)
                .ToList();
            List<Label> labels = original.OfType<Label>().ToList();

            filter.Controls.Clear();
            filter.Padding = new Padding(Space16, Space8, Space16, Space8);
            filter.Height = inputs.Count > 5 ? 144 : 88;

            FlowLayoutPanel flow = new FlowLayoutPanel
            {
                Name = "flpFluentBoLoc",
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.Transparent,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };

            HashSet<Label> usedLabels = new HashSet<Label>();
            foreach (Control input in inputs)
            {
                Label label = FindLabelForInput(input, labels, usedLabels);
                if (label == null)
                {
                    label = new Label { Text = FriendlyLabel(input.Name) };
                }
                else
                {
                    usedLabels.Add(label);
                }

                flow.Controls.Add(CreateFilterField(label, input));
            }

            foreach (Button button in buttons)
            {
                button.Size = new Size(Math.Max(92, button.Width), ButtonHeight);
                button.Margin = new Padding(Space4, 24, Space4, 0);
                flow.Controls.Add(button);
            }

            Label result = labels.FirstOrDefault(item =>
                Normalize(item.Name).Contains("soketqua") ||
                Normalize(item.Name).Contains("trangthaitai"));
            if (result != null && !usedLabels.Contains(result))
            {
                result.AutoSize = false;
                result.Size = new Size(170, ButtonHeight);
                result.Margin = new Padding(Space12, 24, Space4, 0);
                result.TextAlign = ContentAlignment.MiddleRight;
                flow.Controls.Add(result);
                usedLabels.Add(result);
            }

            foreach (Label label in labels.Where(item => !usedLabels.Contains(item)))
            {
                label.AutoSize = false;
                label.Size = new Size(170, ButtonHeight);
                label.Margin = new Padding(Space8, 24, Space4, 0);
                label.TextAlign = ContentAlignment.MiddleLeft;
                flow.Controls.Add(label);
            }

            filter.Controls.Add(flow);
        }

        private static bool IsFilterInput(Control control)
        {
            return control is TextBoxBase || control is ComboBox ||
                   control is DateTimePicker || control is NumericUpDown;
        }

        private static Label FindLabelForInput(Control input, IEnumerable<Label> labels, ISet<Label> used)
        {
            return labels
                .Where(label => !used.Contains(label) &&
                                !Normalize(label.Name).Contains("soketqua") &&
                                !Normalize(label.Name).Contains("trangthai") &&
                                label.Top <= input.Top &&
                                Math.Abs(label.Left - input.Left) <= 24)
                .OrderBy(label => input.Top - label.Bottom)
                .FirstOrDefault();
        }

        private static Control CreateFilterField(Label label, Control input)
        {
            int width = FilterFieldWidth(input);
            TableLayoutPanel field = new TableLayoutPanel
            {
                ColumnCount = 1,
                RowCount = 2,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 0, Space12, Space4),
                Padding = Padding.Empty,
                Size = new Size(width, 62)
            };
            field.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            field.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            field.RowStyles.Add(new RowStyle(SizeType.Absolute, InputHeight));

            label.AutoSize = false;
            label.Dock = DockStyle.Fill;
            label.Margin = Padding.Empty;
            label.TextAlign = ContentAlignment.MiddleLeft;
            input.Dock = DockStyle.Fill;
            input.Margin = Padding.Empty;

            field.Controls.Add(label, 0, 0);
            field.Controls.Add(input, 0, 1);
            return field;
        }

        private static int FilterFieldWidth(Control input)
        {
            string name = Normalize(input.Name);
            if (name.Contains("tukhoa") || name.Contains("tim")) return 220;
            if (input is DateTimePicker) return 132;
            if (input is NumericUpDown) return 110;
            if (name.Contains("tien") || name.Contains("gia")) return 120;
            if (input is ComboBox) return 154;
            return 150;
        }

        private static string FriendlyLabel(string controlName)
        {
            string name = Regex.Replace(controlName ?? string.Empty, "^(txt|cbo|dtp|nud|num)", string.Empty,
                RegexOptions.IgnoreCase);
            Dictionary<string, string> known = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "TuKhoa", "Từ khóa" }, { "TimNhatKy", "Từ khóa" },
                { "TuNgay", "Từ ngày" }, { "DenNgay", "Đến ngày" },
                { "TuNgayNhatKy", "Từ ngày" }, { "DenNgayNhatKy", "Đến ngày" },
                { "LocTrangThai", "Trạng thái" }, { "LocTrangThaiNhatKy", "Trạng thái" },
                { "LocKhachHang", "Khách hàng" }, { "LocNhaCungCap", "Nhà cung cấp" },
                { "LocDanhMuc", "Danh mục" }, { "LocChatLieu", "Chất liệu" },
                { "LocTonKho", "Tồn kho" }, { "LocHanBaoHanh", "Hạn bảo hành" },
                { "LocLoaiGui", "Loại gửi" }, { "LocMauNhatKy", "Mẫu email" },
                { "TienTu", "Giá trị từ" }, { "TienDen", "Giá trị đến" },
                { "GiaTu", "Giá từ" }, { "GiaDen", "Giá đến" },
                { "KhoangThoiGian", "Khoảng thời gian" }, { "NguongTon", "Tồn thấp ≤" }
            };

            string value;
            if (known.TryGetValue(name, out value)) return value;
            return Regex.Replace(name, "([a-z0-9])([A-Z])", "$1 $2");
        }

        private static void ConfigureCrudLayout(Form form)
        {
            SplitContainer split = FindControls<SplitContainer>(form)
                .FirstOrDefault(item => string.Equals(item.Name, "splitContainer", StringComparison.OrdinalIgnoreCase));
            TableLayoutPanel fields = FindControls<TableLayoutPanel>(form)
                .FirstOrDefault(item => string.Equals(item.Name, "tableBieuMau", StringComparison.OrdinalIgnoreCase));
            if (split == null || fields == null)
            {
                return;
            }

            split.Orientation = Orientation.Vertical;
            split.SplitterWidth = Space8;
            split.Panel1MinSize = 360;
            split.Panel2MinSize = 400;
            split.Panel1.Padding = new Padding(Space16, Space16, Space8, Space16);
            split.Panel2.Padding = new Padding(Space8, Space16, Space16, Space16);
            SetCrudSplitter(split);
            split.SizeChanged += delegate { SetCrudSplitter(split); };

            ReflowCrudFields(fields);
            Panel actionPanel = FindControls<Panel>(form)
                .FirstOrDefault(item => string.Equals(item.Name, "pnlThaoTac", StringComparison.OrdinalIgnoreCase));
            if (actionPanel != null)
            {
                ReflowCrudActions(actionPanel);
            }

            Label title = FindControls<Label>(form)
                .FirstOrDefault(item => string.Equals(item.Name, "lblTieuDeBieuMau", StringComparison.OrdinalIgnoreCase));
            if (title != null)
            {
                title.Height = 44;
                title.Padding = new Padding(Space16, 0, Space16, 0);
                title.TextAlign = ContentAlignment.MiddleLeft;
            }
        }

        private static void SetCrudSplitter(SplitContainer split)
        {
            if (split.Width < split.Panel1MinSize + split.Panel2MinSize + split.SplitterWidth)
            {
                return;
            }

            int target = (int)(split.Width * 0.58F);
            int maximum = split.Width - split.Panel2MinSize - split.SplitterWidth;
            split.SplitterDistance = Math.Max(split.Panel1MinSize, Math.Min(maximum, target));
        }

        private static void ReflowCrudFields(TableLayoutPanel table)
        {
            List<FieldPair> pairs = new List<FieldPair>();
            int originalRows = Math.Max(1, table.RowCount);
            for (int row = 0; row < originalRows; row++)
            {
                List<Control> controls = table.Controls.Cast<Control>()
                    .Where(control => table.GetRow(control) == row)
                    .OrderBy(control => table.GetColumn(control))
                    .ToList();
                for (int index = 0; index < controls.Count; index++)
                {
                    Label label = controls[index] as Label;
                    if (label == null) continue;
                    Control input = controls.Skip(index + 1).FirstOrDefault(item => !(item is Label));
                    if (input != null)
                    {
                        pairs.Add(new FieldPair(label, input));
                        controls.Remove(input);
                    }
                }
            }

            if (pairs.Count == 0)
            {
                return;
            }

            table.SuspendLayout();
            table.Controls.Clear();
            table.ColumnStyles.Clear();
            table.RowStyles.Clear();
            table.ColumnCount = 2;
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            table.Padding = new Padding(Space8);
            table.AutoScroll = false;

            int rowIndex = 0;
            int columnIndex = 0;
            foreach (FieldPair pair in pairs)
            {
                bool multiline = pair.Input is TextBoxBase && ((TextBoxBase)pair.Input).Multiline;
                EnsureRow(table, rowIndex, multiline ? 72F : 58F);
                Control host = CreateCrudField(pair);
                table.Controls.Add(host, columnIndex, rowIndex);
                if (columnIndex == 0)
                {
                    columnIndex = 1;
                }
                else
                {
                    rowIndex++;
                    columnIndex = 0;
                }
            }

            table.RowCount = Math.Max(1, rowIndex + (columnIndex > 0 ? 1 : 0));
            table.ResumeLayout(true);
        }

        private static void EnsureRow(TableLayoutPanel table, int row, float height)
        {
            while (table.RowStyles.Count <= row)
            {
                table.RowStyles.Add(new RowStyle(SizeType.Absolute, height));
            }
            if (table.RowStyles[row].Height < height)
            {
                table.RowStyles[row].Height = height;
            }
        }

        private static Control CreateCrudField(FieldPair pair)
        {
            TableLayoutPanel host = new TableLayoutPanel
            {
                ColumnCount = 1,
                RowCount = 2,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Margin = new Padding(Space8, Space4, Space8, Space4),
                Padding = Padding.Empty
            };
            host.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            host.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            host.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            pair.Label.AutoSize = false;
            pair.Label.Dock = DockStyle.Fill;
            pair.Label.Margin = Padding.Empty;
            pair.Label.TextAlign = ContentAlignment.MiddleLeft;
            pair.Input.Dock = DockStyle.Fill;
            pair.Input.Margin = Padding.Empty;
            pair.Input.MinimumSize = new Size(0, InputHeight);

            host.Controls.Add(pair.Label, 0, 0);
            host.Controls.Add(pair.Input, 0, 1);
            return host;
        }

        private static void ReflowCrudActions(Panel panel)
        {
            Button[] buttons = panel.Controls.OfType<Button>()
                .OrderByDescending(item => item.Left)
                .ToArray();
            CheckBox[] checks = panel.Controls.OfType<CheckBox>()
                .OrderBy(item => item.Top)
                .ToArray();
            if (buttons.Length == 0)
            {
                return;
            }

            foreach (Control control in buttons.Cast<Control>().Concat(checks))
            {
                panel.Controls.Remove(control);
            }

            panel.Height = 152;
            panel.Padding = new Padding(Space12);

            FlowLayoutPanel checkFlow = new FlowLayoutPanel
            {
                Name = "flpFluentTrangThai",
                Dock = DockStyle.Top,
                Height = checks.Length == 0 ? 0 : 32,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                BackColor = Color.Transparent,
                Padding = new Padding(Space4, Space4, 0, 0)
            };
            foreach (CheckBox check in checks)
            {
                check.Margin = new Padding(0, 0, Space16, 0);
                checkFlow.Controls.Add(check);
            }

            FlowLayoutPanel buttonFlow = new FlowLayoutPanel
            {
                Name = "flpFluentThaoTac",
                Dock = DockStyle.Bottom,
                Height = 96,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = true,
                BackColor = Color.Transparent,
                Padding = Padding.Empty
            };
            foreach (Button button in buttons)
            {
                button.Anchor = AnchorStyles.None;
                button.Size = new Size(108, ButtonHeight);
                button.Margin = new Padding(Space4);
                buttonFlow.Controls.Add(button);
            }

            panel.Controls.Add(buttonFlow);
            panel.Controls.Add(checkFlow);
        }

        private static void ConfigureCommonControls(Form form)
        {
            foreach (DataGridView grid in FindControls<DataGridView>(form))
            {
                grid.ColumnHeadersHeight = 40;
                grid.RowTemplate.Height = 40;
                grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            }

            foreach (TabControl tabs in FindControls<TabControl>(form))
            {
                tabs.Padding = new Point(Space16, Space8);
                tabs.ItemSize = new Size(Math.Max(110, tabs.ItemSize.Width), 38);
                tabs.SizeMode = TabSizeMode.Fixed;
            }

            foreach (Panel footer in FindControls<Panel>(form).Where(item =>
                string.Equals(item.Name, "pnlChan", StringComparison.OrdinalIgnoreCase)))
            {
                footer.Padding = new Padding(Space16, Space8, Space16, Space8);
                footer.Height = Math.Max(64, footer.Height);
            }

            foreach (Control control in FindControls<Control>(form))
            {
                if (control is Button)
                {
                    Button button = (Button)control;
                    if (!IsNavigationButton(button)) button.MinimumSize = new Size(88, ButtonHeight);
                }
                else if (control is TextBoxBase || control is ComboBox ||
                         control is DateTimePicker || control is NumericUpDown)
                {
                    control.MinimumSize = new Size(0, InputHeight);
                }
            }
        }

        private static bool IsNavigationButton(Button button)
        {
            return button.Parent != null &&
                   (Normalize(button.Parent.Name).Contains("flowmenu") ||
                    Normalize(button.Parent.Name).Contains("nhommenu"));
        }

        private static IEnumerable<T> FindControls<T>(Control root) where T : Control
        {
            foreach (Control child in root.Controls)
            {
                T match = child as T;
                if (match != null) yield return match;
                foreach (T descendant in FindControls<T>(child)) yield return descendant;
            }
        }

        private static string Normalize(string value)
        {
            return (value ?? string.Empty).Replace("_", string.Empty).ToLowerInvariant();
        }

        private sealed class LayoutState
        {
            internal bool Applied;
        }

        private sealed class FieldPair
        {
            internal FieldPair(Label label, Control input)
            {
                Label = label;
                Input = input;
            }

            internal Label Label { get; private set; }
            internal Control Input { get; private set; }
        }
    }
}
