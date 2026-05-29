using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.Forms
{
    partial class FrmOrderEditor
    {
        private Label lblTitle;
        private Label lblSender;
        private TextBox txtSenderId;
        private Label lblReceiver;
        private TextBox txtReceiverId;
        private Label lblPickup;
        private TextBox txtPickup;
        private Label lblDelivery;
        private TextBox txtDelivery;
        private Label lblService;
        private ComboBox cbServiceType;
        private Button btnSave;
        private Button btnCancel;

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblSender = new Label();
            txtSenderId = new TextBox();
            lblReceiver = new Label();
            txtReceiverId = new TextBox();
            lblPickup = new Label();
            txtPickup = new TextBox();
            lblDelivery = new Label();
            txtDelivery = new TextBox();
            lblService = new Label();
            cbServiceType = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            ClientSize = new Size(450, 500);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.White;
            Text = "Tạo đơn hàng mới";
            lblTitle.Text = "Tạo đơn hàng mới";
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Size = new Size(400, 40);
            lblSender.Text = "Mã khách hàng gửi:";
            lblSender.Location = new Point(30, 80);
            lblSender.AutoSize = true;
            txtSenderId.Location = new Point(30, 105);
            txtSenderId.Size = new Size(390, 30);
            lblReceiver.Text = "Mã khách hàng nhận:";
            lblReceiver.Location = new Point(30, 145);
            lblReceiver.AutoSize = true;
            txtReceiverId.Location = new Point(30, 170);
            txtReceiverId.Size = new Size(390, 30);
            lblPickup.Text = "Địa chỉ lấy hàng:";
            lblPickup.Location = new Point(30, 210);
            lblPickup.AutoSize = true;
            txtPickup.Location = new Point(30, 235);
            txtPickup.Size = new Size(390, 30);
            lblDelivery.Text = "Địa chỉ giao hàng:";
            lblDelivery.Location = new Point(30, 275);
            lblDelivery.AutoSize = true;
            txtDelivery.Location = new Point(30, 300);
            txtDelivery.Size = new Size(390, 30);
            lblService.Text = "Loại dịch vụ:";
            lblService.Location = new Point(30, 340);
            lblService.AutoSize = true;
            cbServiceType.Location = new Point(30, 365);
            cbServiceType.Size = new Size(390, 30);
            cbServiceType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbServiceType.Items.Add("Standard");
            cbServiceType.Items.Add("Express");
            cbServiceType.Items.Add("Instant");
            cbServiceType.SelectedIndex = 0;
            btnSave.Text = "Tạo đơn";
            btnSave.BackColor = Color.FromArgb(46, 204, 113);
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Location = new Point(220, 430);
            btnSave.Size = new Size(100, 35);
            btnSave.Click += BtnSave_Click;
            btnCancel.Text = "Hủy";
            btnCancel.BackColor = Color.FromArgb(189, 195, 199);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Location = new Point(330, 430);
            btnCancel.Size = new Size(90, 35);
            btnCancel.Click += BtnCancel_Click;
            Controls.Add(lblTitle);
            Controls.Add(lblSender);
            Controls.Add(txtSenderId);
            Controls.Add(lblReceiver);
            Controls.Add(txtReceiverId);
            Controls.Add(lblPickup);
            Controls.Add(txtPickup);
            Controls.Add(lblDelivery);
            Controls.Add(txtDelivery);
            Controls.Add(lblService);
            Controls.Add(cbServiceType);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
