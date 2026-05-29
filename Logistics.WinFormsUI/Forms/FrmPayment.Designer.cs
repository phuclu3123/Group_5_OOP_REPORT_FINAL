using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.Forms
{
    partial class FrmPayment
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblRemaining;
        private NumericUpDown nudAmount;
        private ComboBox cbMethod;
        private Button btnSave;
        private Button btnPrint;
        private TextBox txtReceipt;

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
            lblRemaining = new Label();
            nudAmount = new NumericUpDown();
            cbMethod = new ComboBox();
            btnSave = new Button();
            btnPrint = new Button();
            txtReceipt = new TextBox();
            ((System.ComponentModel.ISupportInitialize)nudAmount).BeginInit();
            SuspendLayout();

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Text = "Ghi nhận thanh toán";

            nudAmount.Location = new Point(20, 70);
            nudAmount.Maximum = 1000000000m;
            nudAmount.Name = "nudAmount";
            nudAmount.Size = new Size(220, 27);
            nudAmount.ThousandsSeparator = true;

            cbMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMethod.Location = new Point(260, 70);
            cbMethod.Name = "cbMethod";
            cbMethod.Size = new Size(180, 27);

            btnSave.BackColor = Color.FromArgb(46, 204, 113);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(460, 68);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 32);
            btnSave.Text = "Thu tiền";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;

            btnPrint.BackColor = Color.FromArgb(52, 152, 219);
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.ForeColor = Color.White;
            btnPrint.Location = new Point(575, 68);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(100, 32);
            btnPrint.Text = "In PDF";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += BtnPrint_Click;

            lblRemaining.ForeColor = Color.DimGray;
            lblRemaining.Location = new Point(20, 100);
            lblRemaining.Name = "lblRemaining";
            lblRemaining.Size = new Size(420, 22);
            lblRemaining.Text = "Còn lại: 0 VND";

            txtReceipt.Font = new Font("Consolas", 10F);
            txtReceipt.Location = new Point(20, 132);
            txtReceipt.Multiline = true;
            txtReceipt.Name = "txtReceipt";
            txtReceipt.ScrollBars = ScrollBars.Vertical;
            txtReceipt.Size = new Size(700, 368);

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(744, 521);
            Controls.Add(lblTitle);
            Controls.Add(nudAmount);
            Controls.Add(cbMethod);
            Controls.Add(btnSave);
            Controls.Add(btnPrint);
            Controls.Add(lblRemaining);
            Controls.Add(txtReceipt);
            Name = "FrmPayment";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thanh toán đơn hàng";
            ((System.ComponentModel.ISupportInitialize)nudAmount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
