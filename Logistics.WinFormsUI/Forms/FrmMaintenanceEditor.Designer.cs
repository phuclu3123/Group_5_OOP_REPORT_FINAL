using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.Forms
{
    partial class FrmMaintenanceEditor
    {
        private Label lblTitle = null!;
        private Label lblServiceDate = null!;
        private DateTimePicker dtServiceDate = null!;
        private Label lblCost = null!;
        private TextBox txtCost = null!;
        private Label lblDescription = null!;
        private TextBox txtDescription = null!;
        private Label lblProvider = null!;
        private TextBox txtProvider = null!;
        private Label lblNextDue = null!;
        private DateTimePicker dtNextDueDate = null!;
        private Button btnSave = null!;
        private Button btnCancel = null!;

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblServiceDate = new Label();
            dtServiceDate = new DateTimePicker();
            lblCost = new Label();
            txtCost = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblProvider = new Label();
            txtProvider = new TextBox();
            lblNextDue = new Label();
            dtNextDueDate = new DateTimePicker();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            BackColor = Color.White;
            ClientSize = new Size(480, 430);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Bảo trì phương tiện";
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(24, 20);
            lblTitle.Size = new Size(420, 36);
            lblTitle.Text = "Lịch sử bảo trì xe";
            lblServiceDate.Location = new Point(30, 80);
            lblServiceDate.Size = new Size(160, 22);
            lblServiceDate.Text = "Ngày bảo trì:";
            dtServiceDate.Location = new Point(30, 105);
            dtServiceDate.Size = new Size(190, 27);
            lblCost.Location = new Point(250, 80);
            lblCost.Size = new Size(160, 22);
            lblCost.Text = "Chi phí:";
            txtCost.Location = new Point(250, 105);
            txtCost.Size = new Size(190, 27);
            lblDescription.Location = new Point(30, 150);
            lblDescription.Size = new Size(160, 22);
            lblDescription.Text = "Nội dung bảo trì:";
            txtDescription.Location = new Point(30, 175);
            txtDescription.Multiline = true;
            txtDescription.Size = new Size(410, 80);
            lblProvider.Location = new Point(30, 270);
            lblProvider.Size = new Size(160, 22);
            lblProvider.Text = "Đơn vị bảo trì:";
            txtProvider.Location = new Point(30, 295);
            txtProvider.Size = new Size(190, 27);
            lblNextDue.Location = new Point(250, 270);
            lblNextDue.Size = new Size(180, 22);
            lblNextDue.Text = "Ngày bảo trì tiếp theo:";
            dtNextDueDate.Location = new Point(250, 295);
            dtNextDueDate.Size = new Size(190, 27);
            btnSave.BackColor = Color.FromArgb(46, 204, 113);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(240, 360);
            btnSave.Size = new Size(95, 36);
            btnSave.Text = "Lưu";
            btnSave.Click += BtnSave_Click;
            btnCancel.BackColor = Color.FromArgb(149, 165, 166);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(345, 360);
            btnCancel.Size = new Size(95, 36);
            btnCancel.Text = "Đóng";
            btnCancel.Click += BtnCancel_Click;
            Controls.Add(lblTitle);
            Controls.Add(lblServiceDate);
            Controls.Add(dtServiceDate);
            Controls.Add(lblCost);
            Controls.Add(txtCost);
            Controls.Add(lblDescription);
            Controls.Add(txtDescription);
            Controls.Add(lblProvider);
            Controls.Add(txtProvider);
            Controls.Add(lblNextDue);
            Controls.Add(dtNextDueDate);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
