using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.UserControls
{
    partial class ucVehicleCard
    {
        private void InitializeComponent()
        {
            lblLicensePlate = new Label();
            lblVehicleType = new Label();
            lblStatus = new Label();
            pnlStatusColor = new Panel();
            picVehicle = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picVehicle).BeginInit();
            SuspendLayout();
            // 
            // lblLicensePlate
            // 
            lblLicensePlate.Location = new Point(0, 0);
            lblLicensePlate.Name = "lblLicensePlate";
            lblLicensePlate.Size = new Size(100, 23);
            lblLicensePlate.TabIndex = 0;
            // 
            // lblVehicleType
            // 
            lblVehicleType.Location = new Point(0, 0);
            lblVehicleType.Name = "lblVehicleType";
            lblVehicleType.Size = new Size(100, 23);
            lblVehicleType.TabIndex = 0;
            // 
            // lblStatus
            // 
            lblStatus.Location = new Point(0, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(100, 23);
            lblStatus.TabIndex = 0;
            // 
            // pnlStatusColor
            // 
            pnlStatusColor.Location = new Point(0, 0);
            pnlStatusColor.Name = "pnlStatusColor";
            pnlStatusColor.Size = new Size(200, 100);
            pnlStatusColor.TabIndex = 0;
            // 
            // picVehicle
            // 
            picVehicle.Location = new Point(0, 0);
            picVehicle.Name = "picVehicle";
            picVehicle.Size = new Size(100, 50);
            picVehicle.TabIndex = 0;
            picVehicle.TabStop = false;
            // 
            // ucVehicleCard
            // 
            BackColor = Color.White;
            Name = "ucVehicleCard";
            Size = new Size(803, 401);
            ((System.ComponentModel.ISupportInitialize)picVehicle).EndInit();
            ResumeLayout(false);
        }
    }
}