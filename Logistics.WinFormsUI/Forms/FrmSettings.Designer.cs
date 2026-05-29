namespace Logistics.WinFormsUI.Forms
{
    partial class FrmSettings
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpData;
        private System.Windows.Forms.Label lblDataPath;
        private System.Windows.Forms.TextBox txtDataPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.GroupBox grpDisplay;
        private System.Windows.Forms.CheckBox chkHighDpi;
        private System.Windows.Forms.ComboBox cbTheme;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new System.Windows.Forms.Label();
            grpData = new System.Windows.Forms.GroupBox();
            lblDataPath = new System.Windows.Forms.Label();
            txtDataPath = new System.Windows.Forms.TextBox();
            btnBrowse = new System.Windows.Forms.Button();
            grpDisplay = new System.Windows.Forms.GroupBox();
            chkHighDpi = new System.Windows.Forms.CheckBox();
            cbTheme = new System.Windows.Forms.ComboBox();
            btnSave = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            grpData.SuspendLayout();
            grpDisplay.SuspendLayout();
            SuspendLayout();
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(28, 24);
            lblTitle.Text = "Cài đặt";
            grpData.Controls.Add(lblDataPath);
            grpData.Controls.Add(txtDataPath);
            grpData.Controls.Add(btnBrowse);
            grpData.Location = new System.Drawing.Point(32, 82);
            grpData.Size = new System.Drawing.Size(650, 120);
            grpData.Text = "Dữ liệu";
            lblDataPath.AutoSize = true;
            lblDataPath.Location = new System.Drawing.Point(20, 34);
            lblDataPath.Text = "Thư mục dữ liệu";
            txtDataPath.Location = new System.Drawing.Point(20, 60);
            txtDataPath.Size = new System.Drawing.Size(500, 23);
            btnBrowse.Location = new System.Drawing.Point(534, 58);
            btnBrowse.Size = new System.Drawing.Size(88, 27);
            btnBrowse.Text = "Chọn";
            btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            grpDisplay.Controls.Add(chkHighDpi);
            grpDisplay.Controls.Add(cbTheme);
            grpDisplay.Location = new System.Drawing.Point(32, 224);
            grpDisplay.Size = new System.Drawing.Size(650, 120);
            grpDisplay.Text = "Hiển thị";
            chkHighDpi.AutoSize = true;
            chkHighDpi.Location = new System.Drawing.Point(20, 36);
            chkHighDpi.Text = "Bật tương thích High DPI";
            cbTheme.Location = new System.Drawing.Point(20, 70);
            cbTheme.Size = new System.Drawing.Size(220, 23);
            cbTheme.Text = "Giao diện";
            btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.Location = new System.Drawing.Point(486, 398);
            btnSave.Size = new System.Drawing.Size(90, 34);
            btnSave.Text = "Lưu";
            btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            btnCancel.BackColor = System.Drawing.Color.FromArgb(189, 195, 199);
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancel.ForeColor = System.Drawing.Color.White;
            btnCancel.Location = new System.Drawing.Point(592, 398);
            btnCancel.Size = new System.Drawing.Size(90, 34);
            btnCancel.Text = "Hủy";
            btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(720, 500);
            Controls.Add(lblTitle);
            Controls.Add(grpData);
            Controls.Add(grpDisplay);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Name = "FrmSettings";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Cài đặt";
            grpData.ResumeLayout(false);
            grpData.PerformLayout();
            grpDisplay.ResumeLayout(false);
            grpDisplay.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
