using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace FINAL_DotNet
{
    /// <summary>
    /// TabControl tự vẽ để WinForms không chèn các dải nền sáng của visual style hệ điều hành.
    /// </summary>
    internal sealed class DarkGoldTabControl : TabControl
    {
        internal DarkGoldTabControl()
        {
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(LuxuryDarkGoldTheme.Background);

            Rectangle contentBounds = DisplayRectangle;
            if (contentBounds.Width > 0 && contentBounds.Height > 0)
            {
                using (SolidBrush content = new SolidBrush(LuxuryDarkGoldTheme.Background))
                using (Pen border = new Pen(LuxuryDarkGoldTheme.Border))
                {
                    e.Graphics.FillRectangle(content, contentBounds);
                    e.Graphics.DrawRectangle(border, contentBounds.X, contentBounds.Y,
                        contentBounds.Width - 1, contentBounds.Height - 1);
                }
            }

            for (int index = 0; index < TabPages.Count; index++)
            {
                DrawTab(e.Graphics, index, index == SelectedIndex);
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            Invalidate();
        }

        private void DrawTab(Graphics graphics, int index, bool selected)
        {
            Rectangle bounds = GetTabRect(index);
            if (bounds.Width <= 0 || bounds.Height <= 0)
            {
                return;
            }

            Color backColor = selected ? LuxuryDarkGoldTheme.Card : LuxuryDarkGoldTheme.Background;
            Color textColor = selected ? LuxuryDarkGoldTheme.Gold : LuxuryDarkGoldTheme.TextSecondary;
            using (SolidBrush background = new SolidBrush(backColor))
            using (Pen border = new Pen(LuxuryDarkGoldTheme.Border))
            using (SolidBrush accent = new SolidBrush(LuxuryDarkGoldTheme.Gold))
            {
                graphics.FillRectangle(background, bounds);
                graphics.DrawRectangle(border, bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
                if (selected)
                {
                    graphics.FillRectangle(accent, bounds.Left + 2, bounds.Bottom - 3, bounds.Width - 4, 3);
                }
            }

            TextRenderer.DrawText(
                graphics,
                TabPages[index].Text,
                Font,
                bounds,
                textColor,
                TextFormatFlags.HorizontalCenter |
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.EndEllipsis |
                TextFormatFlags.NoPadding);
        }
    }

    /// <summary>
    /// DateTimePicker giữ nguyên hành vi native nhưng phủ lớp hiển thị Gold theo design system.
    /// </summary>
    internal sealed class DarkGoldDateTimePicker : DateTimePicker
    {
        private const int WmPaint = 0x000F;
        private const int WmPrint = 0x0317;
        private const int WmPrintClient = 0x0318;

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);
            if (message.Msg == WmPaint)
            {
                using (Graphics graphics = Graphics.FromHwnd(Handle))
                {
                    DrawGoldSurface(graphics);
                }
            }
            else if ((message.Msg == WmPrint || message.Msg == WmPrintClient) && message.WParam != IntPtr.Zero)
            {
                using (Graphics graphics = Graphics.FromHdc(message.WParam))
                {
                    DrawGoldSurface(graphics);
                }
            }
        }

        protected override void OnValueChanged(EventArgs eventargs)
        {
            base.OnValueChanged(eventargs);
            Invalidate();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }

        private void DrawGoldSurface(Graphics graphics)
        {
            Rectangle bounds = ClientRectangle;
            if (bounds.Width <= 1 || bounds.Height <= 1)
            {
                return;
            }

            Color background = Enabled ? LuxuryDarkGoldTheme.Gold : LuxuryDarkGoldTheme.InputBorder;
            Color foreground = Enabled ? LuxuryDarkGoldTheme.Background : LuxuryDarkGoldTheme.TextSecondary;
            Color borderColor = Focused ? LuxuryDarkGoldTheme.GoldHover : LuxuryDarkGoldTheme.Gold;
            using (SolidBrush backgroundBrush = new SolidBrush(background))
            using (SolidBrush buttonBrush = new SolidBrush(Enabled ? LuxuryDarkGoldTheme.GoldHover : LuxuryDarkGoldTheme.Border))
            using (Pen border = new Pen(borderColor))
            using (Pen glyph = new Pen(foreground, 1.4F))
            {
                graphics.FillRectangle(backgroundBrush, bounds);
                graphics.DrawRectangle(border, 0, 0, bounds.Width - 1, bounds.Height - 1);

                int buttonWidth = Math.Min(24, Math.Max(18, bounds.Height));
                Rectangle buttonBounds = new Rectangle(bounds.Right - buttonWidth, 1, buttonWidth - 1, bounds.Height - 2);
                graphics.FillRectangle(buttonBrush, buttonBounds);
                graphics.DrawLine(glyph, buttonBounds.Left, buttonBounds.Top, buttonBounds.Left, buttonBounds.Bottom);

                Point center = new Point(buttonBounds.Left + buttonBounds.Width / 2, buttonBounds.Top + buttonBounds.Height / 2 + 1);
                graphics.DrawLine(glyph, center.X - 4, center.Y - 2, center.X, center.Y + 2);
                graphics.DrawLine(glyph, center.X, center.Y + 2, center.X + 4, center.Y - 2);
            }

            int textLeft = 7;
            if (ShowCheckBox)
            {
                Rectangle checkBounds = new Rectangle(5, Math.Max(3, (Height - 13) / 2), 13, 13);
                ControlPaint.DrawCheckBox(graphics, checkBounds,
                    Checked ? ButtonState.Checked : ButtonState.Normal);
                textLeft = 23;
            }

            Rectangle textBounds = new Rectangle(textLeft, 1, Math.Max(1, bounds.Width - textLeft - Math.Min(24, Math.Max(18, bounds.Height)) - 3), bounds.Height - 2);
            TextRenderer.DrawText(
                graphics,
                DisplayText(),
                Font,
                textBounds,
                foreground,
                TextFormatFlags.Left |
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.EndEllipsis |
                TextFormatFlags.NoPadding);
        }

        private string DisplayText()
        {
            switch (Format)
            {
                case DateTimePickerFormat.Long:
                    return Value.ToLongDateString();
                case DateTimePickerFormat.Time:
                    return Value.ToLongTimeString();
                case DateTimePickerFormat.Custom:
                    return Value.ToString(CustomFormat, CultureInfo.CurrentCulture);
                default:
                    return Value.ToShortDateString();
            }
        }
    }
}
