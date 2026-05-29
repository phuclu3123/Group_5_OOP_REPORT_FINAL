namespace Logistics.WinFormsUI.Forms
{
    partial class FrmWarehouseEditor
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblCapacity;
        private System.Windows.Forms.TextBox txtCapacity;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblCapacity = new System.Windows.Forms.Label();
            this.txtCapacity = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // Form Settings
            this.ClientSize = new System.Drawing.Size(400, 550);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.White;

            // lblTitle
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Size = new System.Drawing.Size(360, 40);
            this.lblTitle.Text = "Thêm Kho Bãi Mới";

            // lblId
            this.lblId.Text = "Mã kho bãi:";
            this.lblId.Location = new System.Drawing.Point(30, 80);
            this.lblId.AutoSize = true;
            this.txtId.Location = new System.Drawing.Point(30, 105);
            this.txtId.Size = new Size(340, 30);
            this.txtId.Font = new System.Drawing.Font("Segoe UI", 10F);

            // lblName
            this.lblName.Text = "Tên kho bãi:";
            this.lblName.Location = new System.Drawing.Point(30, 145);
            this.lblName.AutoSize = true;
            this.txtName.Location = new System.Drawing.Point(30, 170);
            this.txtName.Size = new Size(340, 30);
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);

            // lblType
            this.lblType.Text = "Loại kho bãi:";
            this.lblType.Location = new System.Drawing.Point(30, 210);
            this.lblType.AutoSize = true;
            this.cbType.Location = new System.Drawing.Point(30, 235);
            this.cbType.Size = new Size(340, 30);
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.Items.Add("Trung tâm phân loại");
            this.cbType.Items.Add("Trung tâm điều phối");
            this.cbType.Items.Add("Điểm trung chuyển");
            this.cbType.Items.Add("FulfillmentCenter");
            this.cbType.Items.Add("SortingCenter");
            this.cbType.Items.Add("TransshipmentPoint");
            this.cbType.Items.Add("ColdStorage");
            this.cbType.SelectedIndex = 0;

            // lblCapacity
            this.lblCapacity.Text = "Sức chứa (m3):";
            this.lblCapacity.Location = new System.Drawing.Point(30, 275);
            this.lblCapacity.AutoSize = true;
            this.txtCapacity.Location = new System.Drawing.Point(30, 300);
            this.txtCapacity.Size = new Size(340, 30);
            this.txtCapacity.Font = new System.Drawing.Font("Segoe UI", 10F);

            // lblAddress
            this.lblAddress.Text = "Địa chỉ:";
            this.lblAddress.Location = new System.Drawing.Point(30, 340);
            this.lblAddress.AutoSize = true;
            this.txtAddress.Location = new System.Drawing.Point(30, 365);
            this.txtAddress.Size = new Size(340, 60);
            this.txtAddress.Multiline = true;
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 10F);

            // btnSave
            this.btnSave.Text = "Lưu";
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(180, 480);
            this.btnSave.Size = new System.Drawing.Size(90, 35);
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            // btnCancel
            this.btnCancel.Text = "Hủy";
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(189, 195, 199);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(280, 480);
            this.btnCancel.Size = new System.Drawing.Size(90, 35);
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);

            // Controls
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblId);
            this.Controls.Add(txtId);
            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblType);
            this.Controls.Add(cbType);
            this.Controls.Add(lblCapacity);
            this.Controls.Add(txtCapacity);
            this.Controls.Add(lblAddress);
            this.Controls.Add(txtAddress);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);

            this.ResumeLayout(false);
            this.PerformLayout();
            
        }
    }
}
