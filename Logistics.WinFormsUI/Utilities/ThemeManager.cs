using System;
using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.Utilities
{
    public enum AppTheme
    {
        Light,
        Dark
    }

    public static class ThemeManager
    {
        private static AppTheme _currentTheme = AppTheme.Light;
        public static event EventHandler? ThemeChanged;

        public static AppTheme CurrentTheme
        {
            get { return _currentTheme; }
            set
            {
                if (_currentTheme != value)
                {
                    _currentTheme = value;
                    if (ThemeChanged != null)
                    {
                        ThemeChanged(null, EventArgs.Empty);
                    }
                }
            }
        }

        // Bang mau dung chung cho toan bo WinForms UI.
        public static Color BackgroundColor
        {
            get { return _currentTheme == AppTheme.Light ? Color.FromArgb(248, 250, 252) : Color.FromArgb(15, 23, 42); }
        }

        public static Color CardColor
        {
            get { return _currentTheme == AppTheme.Light ? Color.White : Color.FromArgb(30, 41, 59); }
        }

        public static Color TextColor
        {
            get { return _currentTheme == AppTheme.Light ? Color.FromArgb(15, 23, 42) : Color.FromArgb(241, 245, 249); }
        }

        public static Color SecondaryTextColor
        {
            get { return _currentTheme == AppTheme.Light ? Color.FromArgb(71, 85, 105) : Color.FromArgb(148, 163, 184); }
        }

        public static Color BorderColor
        {
            get { return _currentTheme == AppTheme.Light ? Color.FromArgb(226, 232, 240) : Color.FromArgb(51, 65, 85); }
        }

        public static Color HeaderColor
        {
            get { return _currentTheme == AppTheme.Light ? Color.White : Color.FromArgb(30, 41, 59); }
        }

        public static Color HeaderHoverColor
        {
            get { return _currentTheme == AppTheme.Light ? Color.FromArgb(241, 245, 249) : Color.FromArgb(51, 65, 85); }
        }

        public static Color HeaderTextColor
        {
            get { return _currentTheme == AppTheme.Light ? Color.FromArgb(15, 23, 42) : Color.FromArgb(241, 245, 249); }
        }

        public static Color SidebarColor
        {
            get { return _currentTheme == AppTheme.Light ? Color.FromArgb(15, 23, 42) : Color.FromArgb(2, 6, 23); }
        }

        public static Color AccentColor
        {
            get { return Color.FromArgb(37, 99, 235); }
        }

        public static Color SuccessColor
        {
            get { return _currentTheme == AppTheme.Light ? Color.FromArgb(22, 163, 74) : Color.FromArgb(74, 222, 128); }
        }

        public static void ApplyTheme(Control control)
        {
            if (control == null) return;

            string? tagStr = control.Tag?.ToString();

            // Mot so control co mau nghiep vu rieng, vi du status chip tren dashboard.
            if (tagStr != null && tagStr.Contains("KeepStyle"))
            {
                return;
            }

            if (control is Form form)
            {
                form.BackColor = BackgroundColor;
            }
            else if (control is Panel panel)
            {
                if (tagStr != null && tagStr.Contains("Sidebar"))
                {
                    panel.BackColor = SidebarColor;
                }
                else if (tagStr != null && tagStr.Contains("Header"))
                {
                    panel.BackColor = HeaderColor;
                }
                else
                {
                    panel.BackColor = CardColor;
                }
            }
            else if (control is UserControl)
            {
                control.BackColor = BackgroundColor;
            }
            else if (control is GroupBox groupBox)
            {
                groupBox.BackColor = CardColor;
                groupBox.ForeColor = TextColor;
            }
            else if (control is DataGridView dgv)
            {
                dgv.BackgroundColor = CardColor;
                dgv.GridColor = BorderColor;

                DataGridViewCellStyle headerStyle = dgv.ColumnHeadersDefaultCellStyle;
                headerStyle.BackColor = _currentTheme == AppTheme.Light ? Color.FromArgb(241, 245, 249) : Color.FromArgb(15, 23, 42);
                headerStyle.ForeColor = TextColor;
                headerStyle.SelectionBackColor = headerStyle.BackColor;
                headerStyle.SelectionForeColor = headerStyle.ForeColor;

                DataGridViewCellStyle rowStyle = dgv.DefaultCellStyle;
                rowStyle.BackColor = CardColor;
                rowStyle.ForeColor = TextColor;
                rowStyle.SelectionBackColor = _currentTheme == AppTheme.Light ? Color.FromArgb(239, 246, 255) : Color.FromArgb(30, 58, 138);
                rowStyle.SelectionForeColor = TextColor;
            }
            else if (control is Label lbl)
            {
                if (tagStr != null && tagStr.Contains("Success"))
                {
                    lbl.ForeColor = SuccessColor;
                }
                else if (tagStr != null && tagStr.Contains("HeaderText"))
                {
                    lbl.ForeColor = HeaderTextColor;
                }
                else if (tagStr != null && tagStr.Contains("Title"))
                {
                    lbl.ForeColor = AccentColor;
                }
                else if (tagStr != null && tagStr.Contains("Sub"))
                {
                    lbl.ForeColor = SecondaryTextColor;
                }
                else
                {
                    lbl.ForeColor = TextColor;
                }
            }
            else if (control is TextBox txt)
            {
                txt.BackColor = _currentTheme == AppTheme.Light ? Color.White : Color.FromArgb(15, 23, 42);
                txt.ForeColor = TextColor;
                txt.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (control is ComboBox cb)
            {
                cb.BackColor = _currentTheme == AppTheme.Light ? Color.White : Color.FromArgb(15, 23, 42);
                cb.ForeColor = TextColor;
            }
            else if (control is CheckBox chk)
            {
                chk.BackColor = CardColor;
                chk.ForeColor = TextColor;
            }
            else if (control is RadioButton radio)
            {
                radio.BackColor = CardColor;
                radio.ForeColor = TextColor;
            }
            else if (control is NumericUpDown numeric)
            {
                numeric.BackColor = _currentTheme == AppTheme.Light ? Color.White : Color.FromArgb(15, 23, 42);
                numeric.ForeColor = TextColor;
            }
            else if (control is DateTimePicker datePicker)
            {
                datePicker.CalendarMonthBackground = CardColor;
                datePicker.CalendarForeColor = TextColor;
            }
            else if (control is Button btn)
            {
                if (tagStr != null && tagStr.Contains("HeaderButton"))
                {
                    btn.UseVisualStyleBackColor = false;
                    btn.BackColor = HeaderColor;
                    btn.ForeColor = HeaderTextColor;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                }
                else if (tagStr != null && tagStr.Contains("Accent"))
                {
                    btn.UseVisualStyleBackColor = false;
                    btn.BackColor = AccentColor;
                    btn.ForeColor = Color.White;
                }
                else if (tagStr != null && tagStr.Contains("Secondary"))
                {
                    btn.UseVisualStyleBackColor = false;
                    btn.BackColor = _currentTheme == AppTheme.Light ? Color.FromArgb(226, 232, 240) : Color.FromArgb(51, 65, 85);
                    btn.ForeColor = TextColor;
                }
            }
            else if (control is TabControl tab)
            {
                tab.BackColor = BackgroundColor;
            }

            foreach (Control child in control.Controls)
            {
                ApplyTheme(child);
            }
        }
    }
}
