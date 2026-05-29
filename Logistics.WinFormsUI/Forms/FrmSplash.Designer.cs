namespace Logistics.WinFormsUI.Forms
{
    partial class FrmSplash
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar progressBar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblAppName = new System.Windows.Forms.Label();
            lblStatus = new System.Windows.Forms.Label();
            progressBar = new System.Windows.Forms.ProgressBar();
            SuspendLayout();
            lblAppName.AutoSize = true;
            lblAppName.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            lblAppName.ForeColor = System.Drawing.Color.FromArgb(45, 35, 70);
            lblAppName.Location = new System.Drawing.Point(78, 72);
            lblAppName.Text = "Logistics System";
            lblStatus.AutoSize = true;
            lblStatus.Location = new System.Drawing.Point(86, 158);
            lblStatus.Text = "Dang tai du lieu...";
            progressBar.Location = new System.Drawing.Point(86, 192);
            progressBar.Size = new System.Drawing.Size(328, 18);
            progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(500, 300);
            Controls.Add(lblAppName);
            Controls.Add(lblStatus);
            Controls.Add(progressBar);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "FrmSplash";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Loading...";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
