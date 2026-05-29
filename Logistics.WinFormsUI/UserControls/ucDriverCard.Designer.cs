using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.UserControls
{
    partial class ucDriverCard
    {
        private void InitializeComponent()
        {
            lblDriverName = new Label();
            lblPhone = new Label();
            lblRating = new Label();
            picAvatar = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            SuspendLayout();
            // 
            // lblDriverName
            // 
            lblDriverName.Location = new Point(0, 0);
            lblDriverName.Name = "lblDriverName";
            lblDriverName.Size = new Size(100, 23);
            lblDriverName.TabIndex = 0;
            // 
            // lblPhone
            // 
            lblPhone.Location = new Point(0, 0);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(100, 23);
            lblPhone.TabIndex = 0;
            // 
            // lblRating
            // 
            lblRating.Location = new Point(0, 0);
            lblRating.Name = "lblRating";
            lblRating.Size = new Size(100, 23);
            lblRating.TabIndex = 0;
            // 
            // picAvatar
            // 
            picAvatar.Location = new Point(0, 0);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(100, 50);
            picAvatar.TabIndex = 0;
            picAvatar.TabStop = false;
            // 
            // ucDriverCard
            // 
            BackColor = Color.White;
            Name = "ucDriverCard";
            Size = new Size(510, 350);
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            ResumeLayout(false);
        }
    }
}