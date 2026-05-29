using System.Windows.Forms;
using Logistics.WinFormsUI.Extensions;
using Logistics.Core.DTOs;

namespace Logistics.WinFormsUI.UserControls
{
    public partial class ucVehicleCard : UserControl
    {
        private VehicleDTO _vehicleData = null!;
        private Label lblLicensePlate = null!;
        private Label lblVehicleType = null!;
        private Label lblStatus = null!;
        private Panel pnlStatusColor = null!;
        private PictureBox picVehicle = null!;

        public ucVehicleCard()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.SetRoundedBorder(15);

            // Icon xe (Giả lập bằng panel nếu không có ảnh)
            picVehicle.Size = new Size(50, 50);
            picVehicle.Location = new Point(15, 25);
            picVehicle.BackColor = Color.FromArgb(240, 240, 240);
            picVehicle.BorderStyle = BorderStyle.None;
            this.Controls.Add(picVehicle);

            // Biển số xe
            lblLicensePlate.Text = "29C-123.45";
            lblLicensePlate.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblLicensePlate.Location = new Point(75, 20);
            lblLicensePlate.AutoSize = true;
            this.Controls.Add(lblLicensePlate);

            // Loại xe
            lblVehicleType.Text = "Xe tải 5 tấn";
            lblVehicleType.Font = new Font("Segoe UI", 9F);
            lblVehicleType.ForeColor = Color.Gray;
            lblVehicleType.Location = new Point(75, 45);
            lblVehicleType.AutoSize = true;
            this.Controls.Add(lblVehicleType);

            // Trạng thái (Chấm tròn màu)
            pnlStatusColor.Size = new Size(10, 10);
            pnlStatusColor.Location = new Point(78, 72);
            pnlStatusColor.BackColor = Color.LimeGreen;
            pnlStatusColor.SetRoundedBorder(5);
            this.Controls.Add(pnlStatusColor);

            lblStatus.Text = "Đang hoạt động";
            lblStatus.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            lblStatus.ForeColor = Color.DimGray;
            lblStatus.Location = new Point(92, 70);
            lblStatus.AutoSize = true;
            this.Controls.Add(lblStatus);
        }

        /// <summary>
        /// Kết nối dữ liệu phương tiện từ BUS/DTO
        /// </summary>
        public void BindData(VehicleDTO vehicle)
        {
            if (vehicle == null) return;
            _vehicleData = vehicle;

            lblLicensePlate.Text = vehicle.VehicleId;
            lblVehicleType.Text = vehicle.VehicleType + " (" + vehicle.MaxWeightCapacityKg + "kg)";
            lblStatus.Text = vehicle.StatusDisplay;

            // Đổi màu chấm trạng thái
            pnlStatusColor.BackColor = vehicle.IsAvailable ? System.Drawing.Color.LimeGreen : System.Drawing.Color.OrangeRed;
        }
    }
}
