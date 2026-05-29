using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.Forms
{
    partial class FrmVehicleEditor
    {
        private Label lblTitle;
        private Label lblVehicleId;
        private TextBox txtVehicleId;
        private Label lblType;
        private ComboBox cbType;
        private Label lblCapacity;
        private TextBox txtCapacity;
        private Label lblStatus;
        private ComboBox cbStatus;
        private Button btnSave;
        private Button btnCancel;

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblVehicleId = new Label();
            txtVehicleId = new TextBox();
            lblType = new Label();
            cbType = new ComboBox();
            lblCapacity = new Label();
            txtCapacity = new TextBox();
            lblStatus = new Label();
            cbStatus = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();

            ClientSize = new Size(400, 450);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.White;

            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Size = new Size(360, 40);

            lblVehicleId.Text = "Biển số xe:";
            lblVehicleId.Location = new Point(30, 80);
            lblVehicleId.AutoSize = true;
            txtVehicleId.Location = new Point(30, 105);
            txtVehicleId.Size = new Size(340, 30);
            txtVehicleId.Font = new Font("Segoe UI", 10F);

            lblType.Text = "Loại phương tiện:";
            lblType.Location = new Point(30, 145);
            lblType.AutoSize = true;
            cbType.Location = new Point(30, 170);
            cbType.Size = new Size(340, 30);
            cbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbType.Items.Add("Xe máy");
            cbType.Items.Add("Xe tải nhỏ");
            cbType.Items.Add("Xe tải 1 tấn");
            cbType.Items.Add("Xe Container 40ft");
            cbType.Items.Add("Xe tải đông lạnh");
            cbType.SelectedIndex = 0;

            lblCapacity.Text = "Tải trọng (kg):";
            lblCapacity.Location = new Point(30, 210);
            lblCapacity.AutoSize = true;
            txtCapacity.Location = new Point(30, 235);
            txtCapacity.Size = new Size(340, 30);
            txtCapacity.Font = new Font("Segoe UI", 10F);

            lblStatus.Text = "Trạng thái:";
            lblStatus.Location = new Point(30, 275);
            lblStatus.AutoSize = true;
            cbStatus.Location = new Point(30, 300);
            cbStatus.Size = new Size(340, 30);
            cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatus.Items.Add("Sẵn sàng");
            cbStatus.Items.Add("Bảo trì");
            cbStatus.Items.Add("Đang vận chuyển");
            cbStatus.Items.Add("Đang hỏng");
            cbStatus.SelectedIndex = 0;

            btnSave.Text = "Lưu";
            btnSave.BackColor = Color.FromArgb(46, 204, 113);
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Location = new Point(180, 380);
            btnSave.Size = new Size(90, 35);
            btnSave.Click += BtnSave_Click;

            btnCancel.Text = "Hủy";
            btnCancel.BackColor = Color.FromArgb(189, 195, 199);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Location = new Point(280, 380);
            btnCancel.Size = new Size(90, 35);
            btnCancel.Click += BtnCancel_Click;

            Controls.Add(lblTitle);
            Controls.Add(lblVehicleId);
            Controls.Add(txtVehicleId);
            Controls.Add(lblType);
            Controls.Add(cbType);
            Controls.Add(lblCapacity);
            Controls.Add(txtCapacity);
            Controls.Add(lblStatus);
            Controls.Add(cbStatus);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
