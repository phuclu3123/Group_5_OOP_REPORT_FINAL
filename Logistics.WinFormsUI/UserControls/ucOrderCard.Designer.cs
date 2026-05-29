using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.UserControls
{
    partial class ucOrderCard
    {
        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblValue = new Label();
            pnlAccent = new Panel();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(100, 23);
            lblTitle.TabIndex = 0;
            // 
            // lblValue
            // 
            lblValue.Location = new Point(0, 0);
            lblValue.Name = "lblValue";
            lblValue.Size = new Size(100, 23);
            lblValue.TabIndex = 0;
            // 
            // pnlAccent
            // 
            pnlAccent.Location = new Point(0, 0);
            pnlAccent.Name = "pnlAccent";
            pnlAccent.Size = new Size(200, 100);
            pnlAccent.TabIndex = 0;
            // 
            // ucOrderCard
            // 
            BackColor = Color.White;
            Name = "ucOrderCard";
            Padding = new Padding(15);
            Size = new Size(509, 316);
            ResumeLayout(false);
        }
    }
}