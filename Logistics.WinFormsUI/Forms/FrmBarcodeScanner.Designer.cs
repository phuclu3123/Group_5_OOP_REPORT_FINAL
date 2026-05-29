namespace Logistics.WinFormsUI.Forms
{
    partial class FrmBarcodeScanner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblWarehouse = new System.Windows.Forms.Label();
            this.cbWarehouse = new System.Windows.Forms.ComboBox();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.grpPackageDetails = new System.Windows.Forms.GroupBox();
            this.lblPkgDesc = new System.Windows.Forms.Label();
            this.lblPkgWeight = new System.Windows.Forms.Label();
            this.lblPkgCategory = new System.Windows.Forms.Label();
            this.lblPkgFragile = new System.Windows.Forms.Label();
            this.btnCheckIn = new System.Windows.Forms.Button();
            this.btnCheckOut = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpHistory = new System.Windows.Forms.GroupBox();
            this.lstHistory = new System.Windows.Forms.ListBox();
            this.grpPackageDetails.SuspendLayout();
            this.grpHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblWarehouse
            // 
            this.lblWarehouse.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblWarehouse.Location = new System.Drawing.Point(20, 20);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new System.Drawing.Size(120, 23);
            this.lblWarehouse.TabIndex = 0;
            this.lblWarehouse.Text = "Chọn Kho Bãi:";
            // 
            // cbWarehouse
            // 
            this.cbWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWarehouse.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbWarehouse.FormattingEnabled = true;
            this.cbWarehouse.Location = new System.Drawing.Point(150, 18);
            this.cbWarehouse.Name = "cbWarehouse";
            this.cbWarehouse.Size = new System.Drawing.Size(430, 25);
            this.cbWarehouse.TabIndex = 1;
            this.cbWarehouse.SelectedIndexChanged += new System.EventHandler(this.CbWarehouse_SelectedIndexChanged);
            // 
            // lblBarcode
            // 
            this.lblBarcode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBarcode.Location = new System.Drawing.Point(20, 60);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(120, 23);
            this.lblBarcode.TabIndex = 2;
            this.lblBarcode.Text = "Quét Mã Vạch / ID:";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtBarcode.Location = new System.Drawing.Point(150, 58);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(430, 27);
            this.txtBarcode.TabIndex = 3;
            this.txtBarcode.TextChanged += new System.EventHandler(this.TxtBarcode_TextChanged);
            // 
            // lblLocation
            // 
            this.lblLocation.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLocation.Location = new System.Drawing.Point(20, 100);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(120, 23);
            this.lblLocation.TabIndex = 4;
            this.lblLocation.Text = "Vị trí kệ (Shelf):";
            // 
            // txtLocation
            // 
            this.txtLocation.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLocation.Location = new System.Drawing.Point(150, 98);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(430, 25);
            this.txtLocation.TabIndex = 5;
            this.txtLocation.Text = "SHELF-A1";
            // 
            // grpPackageDetails
            // 
            this.grpPackageDetails.Controls.Add(this.lblPkgDesc);
            this.grpPackageDetails.Controls.Add(this.lblPkgWeight);
            this.grpPackageDetails.Controls.Add(this.lblPkgCategory);
            this.grpPackageDetails.Controls.Add(this.lblPkgFragile);
            this.grpPackageDetails.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpPackageDetails.Location = new System.Drawing.Point(20, 140);
            this.grpPackageDetails.Name = "grpPackageDetails";
            this.grpPackageDetails.Size = new System.Drawing.Size(560, 120);
            this.grpPackageDetails.TabIndex = 6;
            this.grpPackageDetails.TabStop = false;
            this.grpPackageDetails.Text = "Thông Tin Kiện Hàng Quét Được";
            // 
            // lblPkgDesc
            // 
            this.lblPkgDesc.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPkgDesc.Location = new System.Drawing.Point(15, 25);
            this.lblPkgDesc.Name = "lblPkgDesc";
            this.lblPkgDesc.Size = new System.Drawing.Size(530, 20);
            this.lblPkgDesc.TabIndex = 0;
            this.lblPkgDesc.Text = "Mô tả: (Chưa có thông tin)";
            // 
            // lblPkgWeight
            // 
            this.lblPkgWeight.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPkgWeight.Location = new System.Drawing.Point(15, 48);
            this.lblPkgWeight.Name = "lblPkgWeight";
            this.lblPkgWeight.Size = new System.Drawing.Size(250, 20);
            this.lblPkgWeight.TabIndex = 1;
            this.lblPkgWeight.Text = "Khối lượng: -";
            // 
            // lblPkgCategory
            // 
            this.lblPkgCategory.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPkgCategory.Location = new System.Drawing.Point(280, 48);
            this.lblPkgCategory.Name = "lblPkgCategory";
            this.lblPkgCategory.Size = new System.Drawing.Size(250, 20);
            this.lblPkgCategory.TabIndex = 2;
            this.lblPkgCategory.Text = "Phân loại: -";
            // 
            // lblPkgFragile
            // 
            this.lblPkgFragile.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPkgFragile.Location = new System.Drawing.Point(15, 72);
            this.lblPkgFragile.Name = "lblPkgFragile";
            this.lblPkgFragile.Size = new System.Drawing.Size(530, 20);
            this.lblPkgFragile.TabIndex = 3;
            this.lblPkgFragile.Text = "Hàng dễ vỡ: -";
            // 
            // btnCheckIn
            // 
            this.btnCheckIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnCheckIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckIn.FlatAppearance.BorderSize = 0;
            this.btnCheckIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckIn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCheckIn.ForeColor = System.Drawing.Color.White;
            this.btnCheckIn.Location = new System.Drawing.Point(20, 275);
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.Size = new System.Drawing.Size(185, 38);
            this.btnCheckIn.TabIndex = 7;
            this.btnCheckIn.Tag = "KeepStyle";
            this.btnCheckIn.Text = "Nhập Kho (Check-in) 📥";
            this.btnCheckIn.UseVisualStyleBackColor = false;
            this.btnCheckIn.Click += new System.EventHandler(this.BtnCheckIn_Click);
            // 
            // btnCheckOut
            // 
            this.btnCheckOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnCheckOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckOut.FlatAppearance.BorderSize = 0;
            this.btnCheckOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckOut.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCheckOut.ForeColor = System.Drawing.Color.White;
            this.btnCheckOut.Location = new System.Drawing.Point(220, 275);
            this.btnCheckOut.Name = "btnCheckOut";
            this.btnCheckOut.Size = new System.Drawing.Size(185, 38);
            this.btnCheckOut.TabIndex = 8;
            this.btnCheckOut.Tag = "KeepStyle";
            this.btnCheckOut.Text = "Xuất Kho (Check-out) 📤";
            this.btnCheckOut.UseVisualStyleBackColor = false;
            this.btnCheckOut.Click += new System.EventHandler(this.BtnCheckOut_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(420, 275);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 38);
            this.btnClose.TabIndex = 9;
            this.btnClose.Tag = "KeepStyle";
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // grpHistory
            // 
            this.grpHistory.Controls.Add(this.lstHistory);
            this.grpHistory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpHistory.Location = new System.Drawing.Point(20, 330);
            this.grpHistory.Name = "grpHistory";
            this.grpHistory.Size = new System.Drawing.Size(560, 180);
            this.grpHistory.TabIndex = 10;
            this.grpHistory.TabStop = false;
            this.grpHistory.Text = "Lịch Sử Quét Mã Gần Đây";
            // 
            // lstHistory
            // 
            this.lstHistory.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lstHistory.FormattingEnabled = true;
            this.lstHistory.ItemHeight = 17;
            this.lstHistory.Location = new System.Drawing.Point(15, 25);
            this.lstHistory.Name = "lstHistory";
            this.lstHistory.Size = new System.Drawing.Size(530, 140);
            this.lstHistory.TabIndex = 0;
            // 
            // FrmBarcodeScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 521);
            this.Controls.Add(this.grpHistory);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCheckOut);
            this.Controls.Add(this.btnCheckIn);
            this.Controls.Add(this.grpPackageDetails);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.lblBarcode);
            this.Controls.Add(this.cbWarehouse);
            this.Controls.Add(this.lblWarehouse);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBarcodeScanner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trình Mô Phỏng Quét Mã Vạch Kho Bãi";
            this.grpPackageDetails.ResumeLayout(false);
            this.grpHistory.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWarehouse;
        private System.Windows.Forms.ComboBox cbWarehouse;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.GroupBox grpPackageDetails;
        private System.Windows.Forms.Label lblPkgDesc;
        private System.Windows.Forms.Label lblPkgWeight;
        private System.Windows.Forms.Label lblPkgCategory;
        private System.Windows.Forms.Label lblPkgFragile;
        private System.Windows.Forms.Button btnCheckIn;
        private System.Windows.Forms.Button btnCheckOut;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox grpHistory;
        private System.Windows.Forms.ListBox lstHistory;
    }
}
