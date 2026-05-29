using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.Forms
{
    partial class FrmOrderStatusEditor
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblStatus;
        private ComboBox cbStatus;
        private Button btnSave;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblStatus = new Label();
            cbStatus = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Text = "Đổi trạng thái đơn hàng";

            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(20, 58);
            lblStatus.Name = "lblStatus";
            lblStatus.Text = "Trạng thái mới";

            cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatus.Items.Add("Chờ xử lý");
            cbStatus.Items.Add("Đang vận chuyển");
            cbStatus.Items.Add("Đã giao");
            cbStatus.Items.Add("Đã hủy");
            cbStatus.Items.Add("Hoàn trả");
            cbStatus.Items.Add("Giao thất bại");
            cbStatus.Location = new Point(20, 84);
            cbStatus.Name = "cbStatus";
            cbStatus.Size = new Size(315, 23);

            btnSave.BackColor = Color.FromArgb(46, 204, 113);
            btnSave.DialogResult = DialogResult.OK;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(160, 132);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(82, 32);
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = false;

            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(252, 132);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(82, 32);
            btnCancel.Text = "Hủy";

            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(360, 186);
            Controls.Add(lblTitle);
            Controls.Add(lblStatus);
            Controls.Add(cbStatus);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmOrderStatusEditor";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Đổi trạng thái";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
