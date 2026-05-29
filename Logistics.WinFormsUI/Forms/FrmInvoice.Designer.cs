namespace Logistics.WinFormsUI.Forms
{
    partial class FrmInvoice
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtOrderId;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnPrintPdf;
        private System.Windows.Forms.RichTextBox rtbInvoice;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlHeader = new System.Windows.Forms.Panel();
            lblTitle = new System.Windows.Forms.Label();
            txtOrderId = new System.Windows.Forms.TextBox();
            btnPreview = new System.Windows.Forms.Button();
            btnExport = new System.Windows.Forms.Button();
            btnPrintPdf = new System.Windows.Forms.Button();
            rtbInvoice = new System.Windows.Forms.RichTextBox();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            pnlHeader.BackColor = System.Drawing.Color.White;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(txtOrderId);
            pnlHeader.Controls.Add(btnPreview);
            pnlHeader.Controls.Add(btnExport);
            pnlHeader.Controls.Add(btnPrintPdf);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Size = new System.Drawing.Size(900, 92);
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(24, 18);
            lblTitle.Text = "Hoa don";
            txtOrderId.Location = new System.Drawing.Point(24, 56);
            txtOrderId.PlaceholderText = "Nhap ma don hang";
            txtOrderId.Size = new System.Drawing.Size(260, 23);
            btnPreview.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnPreview.ForeColor = System.Drawing.Color.White;
            btnPreview.Location = new System.Drawing.Point(304, 52);
            btnPreview.Size = new System.Drawing.Size(100, 30);
            btnPreview.Text = "Xem truoc";
            btnPreview.Click += new System.EventHandler(this.BtnPreview_Click);
            btnExport.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnExport.ForeColor = System.Drawing.Color.White;
            btnExport.Location = new System.Drawing.Point(420, 52);
            btnExport.Size = new System.Drawing.Size(100, 30);
            btnExport.Text = "Xuat";
            btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            btnPrintPdf.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            btnPrintPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnPrintPdf.ForeColor = System.Drawing.Color.White;
            btnPrintPdf.Location = new System.Drawing.Point(536, 52);
            btnPrintPdf.Size = new System.Drawing.Size(100, 30);
            btnPrintPdf.Text = "In PDF";
            btnPrintPdf.Click += new System.EventHandler(this.BtnPrintPdf_Click);
            rtbInvoice.BackColor = System.Drawing.Color.White;
            rtbInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            rtbInvoice.Font = new System.Drawing.Font("Consolas", 10F);
            rtbInvoice.Location = new System.Drawing.Point(0, 92);
            rtbInvoice.Name = "rtbInvoice";
            rtbInvoice.Size = new System.Drawing.Size(900, 608);
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(900, 700);
            Controls.Add(rtbInvoice);
            Controls.Add(pnlHeader);
            Name = "FrmInvoice";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Invoice Preview & Export";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);
        }
    }
}
