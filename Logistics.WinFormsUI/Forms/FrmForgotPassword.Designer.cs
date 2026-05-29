using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.Forms
{
    partial class FrmForgotPassword
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblInstructions = new System.Windows.Forms.Label();
            
            // Step 1: Username
            this.lblUser = new System.Windows.Forms.Label();
            this.pnlUser = new System.Windows.Forms.Panel();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.btnCheckUser = new System.Windows.Forms.Button();

            // Step 2: Verification
            this.pnlVerification = new System.Windows.Forms.Panel();
            this.lblQuestionTitle = new System.Windows.Forms.Label();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.pnlAnswer = new System.Windows.Forms.Panel();
            this.txtAnswer = new System.Windows.Forms.TextBox();
            this.btnVerifyAnswer = new System.Windows.Forms.Button();

            // Step 3: Reset
            this.pnlReset = new System.Windows.Forms.Panel();
            this.lblNewPass = new System.Windows.Forms.Label();
            this.pnlNewPass = new System.Windows.Forms.Panel();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPass = new System.Windows.Forms.Label();
            this.pnlConfirmPass = new System.Windows.Forms.Panel();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.btnResetPassword = new System.Windows.Forms.Button();
            this.btnToggleNewPass = new System.Windows.Forms.Button();
            this.btnToggleConfirmPass = new System.Windows.Forms.Button();

            this.lblBack = new System.Windows.Forms.LinkLabel();

            this.panelTop.SuspendLayout();
            this.pnlUser.SuspendLayout();
            this.pnlVerification.SuspendLayout();
            this.pnlAnswer.SuspendLayout();
            this.pnlReset.SuspendLayout();
            this.pnlNewPass.SuspendLayout();
            this.pnlConfirmPass.SuspendLayout();
            this.SuspendLayout();

            // Form Properties
            this.BackColor = System.Drawing.Color.FromArgb(30, 32, 39);
            this.ClientSize = new System.Drawing.Size(476, 550);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            // panelTop
            this.panelTop.Controls.Add(this.btnExit);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 40;
            this.panelTop.Location = new System.Drawing.Point(0, 0);

            // btnExit
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Text = "x";
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(420, 0);
            this.btnExit.Size = new System.Drawing.Size(56, 40);
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);

            // lblTitle
            this.lblTitle.Text = "QUÊN MẬT KHẨU";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(77, 168, 218);
            this.lblTitle.Location = new System.Drawing.Point(50, 60);
            this.lblTitle.Size = new System.Drawing.Size(376, 45);
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblInstructions
            this.lblInstructions.Text = "Vui lòng nhập tên đăng nhập để khôi phục";
            this.lblInstructions.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblInstructions.ForeColor = System.Drawing.Color.LightGray;
            this.lblInstructions.Location = new System.Drawing.Point(50, 110);
            this.lblInstructions.Size = new System.Drawing.Size(376, 30);
            this.lblInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // --- STEP 1: USERNAME ---
            this.lblUser.Text = "Tên đăng nhập:";
            this.lblUser.ForeColor = System.Drawing.Color.White;
            this.lblUser.Location = new System.Drawing.Point(50, 160);
            this.lblUser.AutoSize = true;

            this.pnlUser.BackColor = System.Drawing.Color.White;
            this.pnlUser.Location = new System.Drawing.Point(50, 185);
            this.pnlUser.Size = new System.Drawing.Size(373, 35);
            this.pnlUser.Controls.Add(this.txtUsername);

            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsername.Location = new System.Drawing.Point(10, 10);
            this.txtUsername.Size = new System.Drawing.Size(350, 16);

            this.btnCheckUser.BackColor = System.Drawing.Color.FromArgb(77, 168, 218);
            this.btnCheckUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckUser.FlatAppearance.BorderSize = 0;
            this.btnCheckUser.ForeColor = System.Drawing.Color.White;
            this.btnCheckUser.Text = "Tiếp theo";
            this.btnCheckUser.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCheckUser.Location = new System.Drawing.Point(50, 235);
            this.btnCheckUser.Size = new System.Drawing.Size(373, 40);
            this.btnCheckUser.Click += new System.EventHandler(this.BtnCheckUser_Click);

            // --- STEP 2: VERIFICATION ---
            this.pnlVerification.Visible = false;
            this.pnlVerification.Location = new System.Drawing.Point(0, 150);
            this.pnlVerification.Size = new System.Drawing.Size(476, 200);
            this.pnlVerification.Controls.Add(this.lblQuestionTitle);
            this.pnlVerification.Controls.Add(this.lblQuestion);
            this.pnlVerification.Controls.Add(this.pnlAnswer);
            this.pnlVerification.Controls.Add(this.btnVerifyAnswer);

            this.lblQuestionTitle.Text = "Câu hỏi bảo mật:";
            this.lblQuestionTitle.ForeColor = System.Drawing.Color.White;
            this.lblQuestionTitle.Location = new System.Drawing.Point(50, 10);
            this.lblQuestionTitle.AutoSize = true;

            this.lblQuestion.Text = "[Câu hỏi sẽ hiển thị ở đây]";
            this.lblQuestion.ForeColor = System.Drawing.Color.FromArgb(77, 168, 218);
            this.lblQuestion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblQuestion.Location = new System.Drawing.Point(50, 35);
            this.lblQuestion.Size = new System.Drawing.Size(376, 40);

            this.pnlAnswer.BackColor = System.Drawing.Color.White;
            this.pnlAnswer.Location = new System.Drawing.Point(50, 80);
            this.pnlAnswer.Size = new System.Drawing.Size(373, 35);
            this.pnlAnswer.Controls.Add(this.txtAnswer);

            this.txtAnswer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAnswer.Location = new System.Drawing.Point(10, 10);
            this.txtAnswer.Size = new System.Drawing.Size(350, 16);

            this.btnVerifyAnswer.BackColor = System.Drawing.Color.FromArgb(77, 168, 218);
            this.btnVerifyAnswer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerifyAnswer.FlatAppearance.BorderSize = 0;
            this.btnVerifyAnswer.ForeColor = System.Drawing.Color.White;
            this.btnVerifyAnswer.Text = "Xác thực";
            this.btnVerifyAnswer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnVerifyAnswer.Location = new System.Drawing.Point(50, 130);
            this.btnVerifyAnswer.Size = new System.Drawing.Size(373, 40);
            this.btnVerifyAnswer.Click += new System.EventHandler(this.BtnVerifyAnswer_Click);

            // --- STEP 3: RESET ---
            this.pnlReset.Visible = false;
            this.pnlReset.Location = new System.Drawing.Point(0, 150);
            this.pnlReset.Size = new System.Drawing.Size(476, 250);
            this.pnlReset.Controls.Add(this.lblNewPass);
            this.pnlReset.Controls.Add(this.pnlNewPass);
            this.pnlReset.Controls.Add(this.lblConfirmPass);
            this.pnlReset.Controls.Add(this.pnlConfirmPass);
            this.pnlReset.Controls.Add(this.btnResetPassword);

            this.lblNewPass.Text = "Mật khẩu mới:";
            this.lblNewPass.ForeColor = System.Drawing.Color.White;
            this.lblNewPass.Location = new System.Drawing.Point(50, 10);
            this.lblNewPass.AutoSize = true;

            this.pnlNewPass.BackColor = System.Drawing.Color.White;
            this.pnlNewPass.Location = new System.Drawing.Point(50, 35);
            this.pnlNewPass.Size = new System.Drawing.Size(373, 35);
            this.pnlNewPass.Controls.Add(this.btnToggleNewPass);
            this.pnlNewPass.Controls.Add(this.txtNewPassword);

            this.txtNewPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNewPassword.Location = new System.Drawing.Point(10, 10);
            this.txtNewPassword.Size = new System.Drawing.Size(320, 16);
            this.txtNewPassword.PasswordChar = '●';

            this.btnToggleNewPass.BackColor = System.Drawing.Color.White;
            this.btnToggleNewPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleNewPass.FlatAppearance.BorderSize = 0;
            this.btnToggleNewPass.Image = (Image)Properties.Resources.ResourceManager.GetObject("eye_show");
            this.btnToggleNewPass.Location = new System.Drawing.Point(340, 4);
            this.btnToggleNewPass.Size = new System.Drawing.Size(28, 26);
            this.btnToggleNewPass.Click += new System.EventHandler(this.BtnToggleNewPass_Click);

            this.lblConfirmPass.Text = "Xác nhận mật khẩu:";
            this.lblConfirmPass.ForeColor = System.Drawing.Color.White;
            this.lblConfirmPass.Location = new System.Drawing.Point(50, 85);
            this.lblConfirmPass.AutoSize = true;

            this.pnlConfirmPass.BackColor = System.Drawing.Color.White;
            this.pnlConfirmPass.Location = new System.Drawing.Point(50, 110);
            this.pnlConfirmPass.Size = new System.Drawing.Size(373, 35);
            this.pnlConfirmPass.Controls.Add(this.btnToggleConfirmPass);
            this.pnlConfirmPass.Controls.Add(this.txtConfirmPassword);

            this.txtConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtConfirmPassword.Location = new System.Drawing.Point(10, 10);
            this.txtConfirmPassword.Size = new System.Drawing.Size(320, 16);
            this.txtConfirmPassword.PasswordChar = '●';

            this.btnToggleConfirmPass.BackColor = System.Drawing.Color.White;
            this.btnToggleConfirmPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleConfirmPass.FlatAppearance.BorderSize = 0;
            this.btnToggleConfirmPass.Image = (Image)Properties.Resources.ResourceManager.GetObject("eye_show");
            this.btnToggleConfirmPass.Location = new System.Drawing.Point(340, 4);
            this.btnToggleConfirmPass.Size = new System.Drawing.Size(28, 26);
            this.btnToggleConfirmPass.Click += new System.EventHandler(this.BtnToggleConfirmPass_Click);

            this.btnResetPassword.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnResetPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetPassword.FlatAppearance.BorderSize = 0;
            this.btnResetPassword.ForeColor = System.Drawing.Color.White;
            this.btnResetPassword.Text = "Đặt lại mật khẩu";
            this.btnResetPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnResetPassword.Location = new System.Drawing.Point(50, 160);
            this.btnResetPassword.Size = new System.Drawing.Size(373, 40);
            this.btnResetPassword.Click += new System.EventHandler(this.BtnResetPassword_Click);

            // lblBack
            this.lblBack.Text = "Quay lại đăng nhập";
            this.lblBack.LinkColor = System.Drawing.Color.White;
            this.lblBack.Location = new System.Drawing.Point(50, 480);
            this.lblBack.Size = new System.Drawing.Size(376, 30);
            this.lblBack.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBack.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LblBack_LinkClicked);

            // Add to form
            this.Controls.Add(this.lblBack);
            this.Controls.Add(this.pnlReset);
            this.Controls.Add(this.pnlVerification);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.pnlUser);
            this.Controls.Add(this.btnCheckUser);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.panelTop);

            this.panelTop.ResumeLayout(false);
            this.pnlUser.ResumeLayout(false);
            this.pnlUser.PerformLayout();
            this.pnlVerification.ResumeLayout(false);
            this.pnlVerification.PerformLayout();
            this.pnlAnswer.ResumeLayout(false);
            this.pnlAnswer.PerformLayout();
            this.pnlReset.ResumeLayout(false);
            this.pnlReset.PerformLayout();
            this.pnlNewPass.ResumeLayout(false);
            this.pnlNewPass.PerformLayout();
            this.pnlConfirmPass.ResumeLayout(false);
            this.pnlConfirmPass.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Panel pnlUser;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnCheckUser;
        private System.Windows.Forms.Panel pnlVerification;
        private System.Windows.Forms.Label lblQuestionTitle;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Panel pnlAnswer;
        private System.Windows.Forms.TextBox txtAnswer;
        private System.Windows.Forms.Button btnVerifyAnswer;
        private System.Windows.Forms.Panel pnlReset;
        private System.Windows.Forms.Label lblNewPass;
        private System.Windows.Forms.Panel pnlNewPass;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label lblConfirmPass;
        private System.Windows.Forms.Panel pnlConfirmPass;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Button btnResetPassword;
        private System.Windows.Forms.Button btnToggleNewPass;
        private System.Windows.Forms.Button btnToggleConfirmPass;
        private System.Windows.Forms.LinkLabel lblBack;
    }
}
