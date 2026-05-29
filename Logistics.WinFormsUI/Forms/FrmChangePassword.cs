using System.Windows.Forms;
using Logistics.Core.Models.Account;
using Logistics.Core.Utilities;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmChangePassword : Form
    {
        private readonly User? _user;

        public FrmChangePassword()
        {
            InitializeComponent();
            _user = SessionManager.IsLoggedIn ? SessionManager.CurrentUser : null;
        }

        public FrmChangePassword(User user) : this()
        {
            _user = user;
        }

        private void BtnSave_Click(object? sender, System.EventArgs e)
        {
            if (_user == null)
            {
                MessageBox.Show("Không xác định được tài khoản đang đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text) ||
                string.IsNullOrWhiteSpace(txtNewPassword.Text) ||
                string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!PasswordValidator.IsValid(txtNewPassword.Text, out string error))
            {
                MessageBox.Show(error, "Mật khẩu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool changed = DependencyContainer.GetAuthService()
                .ChangePassword(_user.Username, txtCurrentPassword.Text, txtNewPassword.Text);

            if (!changed)
            {
                MessageBox.Show("Mật khẩu hiện tại không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Đổi mật khẩu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object? sender, System.EventArgs e)
        {
            Close();
        }
    }
}
