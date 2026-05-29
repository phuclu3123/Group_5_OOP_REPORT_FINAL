using System.Windows.Forms;
using System.Drawing;

namespace Logistics.WinFormsUI.Styles
{
    public static class Themes
    {
        public static void ApplyDefault(Form form)
        {
            form.BackColor = Colors.Background;
            form.Font = Fonts.Body;
            form.ForeColor = Colors.TextPrimary;
        }

        public static void ApplyButtonPrimary(Button button)
        {
            button.BackColor = Colors.Primary;
            button.ForeColor = Color.White;
            button.Font = Fonts.Button;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
        }

        public static void ApplyButtonDanger(Button button)
        {
            button.BackColor = Colors.Danger;
            button.ForeColor = Color.White;
            button.Font = Fonts.Button;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
        }

        public static void ApplyButtonSecondary(Button button)
        {
            button.BackColor = Color.FromArgb(233, 236, 239);
            button.ForeColor = Colors.TextPrimary;
            button.Font = Fonts.Button;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
        }
    }
}
