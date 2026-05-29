using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.Extensions
{
    public static class ControlExtensions
    {
        private sealed class PlaceholderState
        {
            public string Text { get; set; } = string.Empty;
        }

        public static void SetPlaceholder(this TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;
            textBox.Tag = new PlaceholderState { Text = placeholder };
            textBox.GotFocus += TextBox_GotFocus;
            textBox.LostFocus += TextBox_LostFocus;
        }

        private static void TextBox_GotFocus(object? sender, System.EventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            if (textBox == null)
            {
                return;
            }

            PlaceholderState? state = textBox.Tag as PlaceholderState;
            if (state != null && textBox.Text == state.Text)
            {
                textBox.Text = string.Empty;
                textBox.ForeColor = Color.Black;
            }
        }

        private static void TextBox_LostFocus(object? sender, System.EventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            if (textBox == null)
            {
                return;
            }

            PlaceholderState? state = textBox.Tag as PlaceholderState;
            if (state != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = state.Text;
                textBox.ForeColor = Color.Gray;
            }
        }

        public static void SetRoundedBorder(this Control control, int radius)
        {
            control.Tag = radius;
            control.Paint += Control_PaintRoundedBorder;
        }

        private static void Control_PaintRoundedBorder(object? sender, PaintEventArgs e)
        {
            Control? control = sender as Control;
            if (control == null)
            {
                return;
            }

            int radius = control.Tag is int storedRadius ? storedRadius : 8;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            control.Region = new Region(path);
        }
    }
}
