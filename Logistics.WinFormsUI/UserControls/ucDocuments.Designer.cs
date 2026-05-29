namespace Logistics.WinFormsUI.UserControls
{
    partial class ucDocuments
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.title = new System.Windows.Forms.Label();
            this.subtitle = new System.Windows.Forms.Label();
            this.toolbar = new System.Windows.Forms.Panel();
            this.lblOrder = new System.Windows.Forms.Label();
            this.txtTrackingNumber = new System.Windows.Forms.TextBox();
            this.btnPreviewInvoice = new System.Windows.Forms.Button();
            this.btnPrintInvoice = new System.Windows.Forms.Button();
            this.btnOpenInvoice = new System.Windows.Forms.Button();
            this.btnReceipts = new System.Windows.Forms.Button();
            this.btnIssues = new System.Windows.Forms.Button();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.rtbPreview = new System.Windows.Forms.RichTextBox();
            this.toolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(72)))));
            this.title.Location = new System.Drawing.Point(24, 24);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(262, 32);
            this.title.TabIndex = 0;
            this.title.Text = "QUẢN LÝ CHỨNG TỪ";
            // 
            // subtitle
            // 
            this.subtitle.AutoSize = true;
            this.subtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.subtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(105)))), ((int)(((byte)(120)))));
            this.subtitle.Location = new System.Drawing.Point(26, 60);
            this.subtitle.Name = "subtitle";
            this.subtitle.Size = new System.Drawing.Size(325, 19);
            this.subtitle.TabIndex = 1;
            this.subtitle.Text = "Hóa đơn, phiếu thu và biên bản sự cố vận chuyển";
            // 
            // toolbar
            // 
            this.toolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolbar.BackColor = System.Drawing.Color.White;
            this.toolbar.Controls.Add(this.lblOrder);
            this.toolbar.Controls.Add(this.txtTrackingNumber);
            this.toolbar.Controls.Add(this.btnPreviewInvoice);
            this.toolbar.Controls.Add(this.btnPrintInvoice);
            this.toolbar.Controls.Add(this.btnOpenInvoice);
            this.toolbar.Controls.Add(this.btnReceipts);
            this.toolbar.Controls.Add(this.btnIssues);
            this.toolbar.Location = new System.Drawing.Point(24, 98);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(1040, 78);
            this.toolbar.TabIndex = 2;
            // 
            // lblOrder
            // 
            this.lblOrder.AutoSize = true;
            this.lblOrder.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblOrder.Location = new System.Drawing.Point(16, 12);
            this.lblOrder.Name = "lblOrder";
            this.lblOrder.Size = new System.Drawing.Size(91, 17);
            this.lblOrder.TabIndex = 0;
            this.lblOrder.Text = "Mã vận đơn";
            // 
            // txtTrackingNumber
            // 
            this.txtTrackingNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTrackingNumber.Location = new System.Drawing.Point(16, 38);
            this.txtTrackingNumber.Name = "txtTrackingNumber";
            this.txtTrackingNumber.Size = new System.Drawing.Size(210, 25);
            this.txtTrackingNumber.TabIndex = 1;
            // 
            // btnPreviewInvoice
            // 
            this.btnPreviewInvoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnPreviewInvoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreviewInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreviewInvoice.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnPreviewInvoice.ForeColor = System.Drawing.Color.White;
            this.btnPreviewInvoice.Location = new System.Drawing.Point(244, 32);
            this.btnPreviewInvoice.Name = "btnPreviewInvoice";
            this.btnPreviewInvoice.Size = new System.Drawing.Size(120, 34);
            this.btnPreviewInvoice.TabIndex = 2;
            this.btnPreviewInvoice.Text = "Xem hóa đơn";
            this.btnPreviewInvoice.UseVisualStyleBackColor = false;
            this.btnPreviewInvoice.Click += new System.EventHandler(this.BtnPreviewInvoice_Click);
            // 
            // btnPrintInvoice
            // 
            this.btnPrintInvoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnPrintInvoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintInvoice.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnPrintInvoice.ForeColor = System.Drawing.Color.White;
            this.btnPrintInvoice.Location = new System.Drawing.Point(384, 32);
            this.btnPrintInvoice.Name = "btnPrintInvoice";
            this.btnPrintInvoice.Size = new System.Drawing.Size(150, 34);
            this.btnPrintInvoice.TabIndex = 3;
            this.btnPrintInvoice.Text = "In PDF hóa đơn";
            this.btnPrintInvoice.UseVisualStyleBackColor = false;
            this.btnPrintInvoice.Click += new System.EventHandler(this.BtnPrintInvoice_Click);
            // 
            // btnOpenInvoice
            // 
            this.btnOpenInvoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnOpenInvoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenInvoice.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnOpenInvoice.ForeColor = System.Drawing.Color.White;
            this.btnOpenInvoice.Location = new System.Drawing.Point(550, 32);
            this.btnOpenInvoice.Name = "btnOpenInvoice";
            this.btnOpenInvoice.Size = new System.Drawing.Size(160, 34);
            this.btnOpenInvoice.TabIndex = 4;
            this.btnOpenInvoice.Text = "Mở mẫu hóa đơn";
            this.btnOpenInvoice.UseVisualStyleBackColor = false;
            this.btnOpenInvoice.Click += new System.EventHandler(this.BtnOpenInvoice_Click);
            // 
            // btnReceipts
            // 
            this.btnReceipts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnReceipts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReceipts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReceipts.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnReceipts.ForeColor = System.Drawing.Color.White;
            this.btnReceipts.Location = new System.Drawing.Point(735, 32);
            this.btnReceipts.Name = "btnReceipts";
            this.btnReceipts.Size = new System.Drawing.Size(120, 34);
            this.btnReceipts.TabIndex = 5;
            this.btnReceipts.Text = "Sổ phiếu thu";
            this.btnReceipts.UseVisualStyleBackColor = false;
            this.btnReceipts.Click += new System.EventHandler(this.BtnReceipts_Click);
            // 
            // btnIssues
            // 
            this.btnIssues.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnIssues.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIssues.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIssues.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnIssues.ForeColor = System.Drawing.Color.White;
            this.btnIssues.Location = new System.Drawing.Point(870, 32);
            this.btnIssues.Name = "btnIssues";
            this.btnIssues.Size = new System.Drawing.Size(120, 34);
            this.btnIssues.TabIndex = 6;
            this.btnIssues.Text = "Sổ sự cố";
            this.btnIssues.UseVisualStyleBackColor = false;
            this.btnIssues.Click += new System.EventHandler(this.BtnIssues_Click);
            // 
            // dgvOrders
            // 
            this.dgvOrders.AllowUserToAddRows = false;
            this.dgvOrders.AllowUserToDeleteRows = false;
            this.dgvOrders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrders.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOrders.Location = new System.Drawing.Point(24, 196);
            this.dgvOrders.MultiSelect = false;
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.RowHeadersVisible = false;
            this.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new System.Drawing.Size(1040, 230);
            this.dgvOrders.TabIndex = 3;
            this.dgvOrders.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvOrders_CellClick);
            // 
            // rtbPreview
            // 
            this.rtbPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbPreview.BackColor = System.Drawing.Color.White;
            this.rtbPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbPreview.Font = new System.Drawing.Font("Consolas", 10F);
            this.rtbPreview.Location = new System.Drawing.Point(24, 448);
            this.rtbPreview.Name = "rtbPreview";
            this.rtbPreview.ReadOnly = true;
            this.rtbPreview.Size = new System.Drawing.Size(1040, 300);
            this.rtbPreview.TabIndex = 4;
            this.rtbPreview.Text = "";
            // 
            // ucDocuments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.rtbPreview);
            this.Controls.Add(this.dgvOrders);
            this.Controls.Add(this.toolbar);
            this.Controls.Add(this.subtitle);
            this.Controls.Add(this.title);
            this.Name = "ucDocuments";
            this.Padding = new System.Windows.Forms.Padding(24);
            this.Size = new System.Drawing.Size(1088, 772);
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label subtitle;
        private System.Windows.Forms.Panel toolbar;
        private System.Windows.Forms.Label lblOrder;
        private System.Windows.Forms.TextBox txtTrackingNumber;
        private System.Windows.Forms.Button btnPreviewInvoice;
        private System.Windows.Forms.Button btnPrintInvoice;
        private System.Windows.Forms.Button btnOpenInvoice;
        private System.Windows.Forms.Button btnReceipts;
        private System.Windows.Forms.Button btnIssues;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.RichTextBox rtbPreview;
    }
}
