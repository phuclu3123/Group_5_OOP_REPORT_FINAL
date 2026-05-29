using System.Windows.Forms;

namespace Logistics.WinFormsUI.Utilities
{
    public static class FormHelper
    {
        public static void ShowForm(Form form)
        {
            form.Show();
        }

        public static void ShowFormDialog(Form form)
        {
            form.ShowDialog();
        }

        public static void CenterForm(Form form)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
        }

        public static void CloseAndOpen(Form currentForm, Form newForm)
        {
            newForm.Show();
            currentForm.Hide();
        }

        public static void SwitchForm(Form currentForm, Form newForm)
        {
            newForm.Show();
            currentForm.Close();
        }
    }
}
