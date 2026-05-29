using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Logistics.Core.Models.Account;
using Logistics.Core.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmUserProfileModal : Form
    {
        private User _user;

        public FrmUserProfileModal()
        {
            InitializeComponent();
            _user = new User
            {
                Username = "designer",
                Role = UserRole.Admin
            };

            if (!Utilities.DesignerHelper.IsInDesignMode(this))
            {
                LoadUserData();
            }
        }

        public FrmUserProfileModal(User user)
        {
            InitializeComponent();
            _user = user;
            LoadUserData();
        }

        private void LoadUserData()
        {
            Text = "Hồ sơ cá nhân";
            lblUsername.Text = "Tài khoản: " + _user.Username;
            lblRole.Text = EnumTranslator.TranslateUserRole(_user.Role);

            if (_user.Person != null)
            {
                lblFullName.Text = _user.Person.FullName;
                lblEmail.Text = "Email: " + _user.Person.Email;
                lblPhone.Text = "Điện thoại: " + _user.Person.PhoneNumber;
                lblAddress.Text = "Địa chỉ: " + _user.Person.HomeAddress.ToString();
            }
            else
            {
                lblFullName.Text = "Chưa cập nhật hồ sơ";
                lblEmail.Text = "Email: Chưa có";
                lblPhone.Text = "Điện thoại: Chưa có";
                lblAddress.Text = "Địa chỉ: Chưa có";
            }

            if (!string.IsNullOrEmpty(_user.AvatarPath) && System.IO.File.Exists(_user.AvatarPath))
            {
                try { picAvatar.Image = Image.FromFile(_user.AvatarPath); } catch { }
            }

            using GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, picAvatar.Width, picAvatar.Height);
            picAvatar.Region = new Region(path);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
