using System.Windows.Forms;
using Logistics.WinFormsUI.Extensions;
using Logistics.Core.DTOs;

namespace Logistics.WinFormsUI.UserControls
{
    public partial class ucDriverCard : UserControl
    {
        private DriverDTO _driverData = null!;
        private Label lblDriverName = null!;
        private Label lblPhone = null!;
        private Label lblRating = null!;
        private PictureBox picAvatar = null!;

        public ucDriverCard()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.SetRoundedBorder(20);

            // Avatar
            picAvatar.Size = new Size(50, 50);
            picAvatar.Location = new Point(75, 15);
            picAvatar.BackColor = Color.FromArgb(220, 230, 245);
            picAvatar.SetRoundedBorder(25); 
            this.Controls.Add(picAvatar);

            // Tên tài xế
            lblDriverName.Text = "Nguyễn Văn A";
            lblDriverName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDriverName.ForeColor = Color.FromArgb(45, 52, 54);
            lblDriverName.TextAlign = ContentAlignment.MiddleCenter;
            lblDriverName.Location = new Point(0, 70);
            lblDriverName.Size = new Size(200, 25);
            this.Controls.Add(lblDriverName);

            // Rating
            lblRating.Text = "⭐⭐⭐⭐⭐";
            lblRating.ForeColor = Color.Orange;
            lblRating.Font = new Font("Segoe UI", 9F);
            lblRating.TextAlign = ContentAlignment.MiddleCenter;
            lblRating.Location = new Point(0, 95);
            lblRating.Size = new Size(200, 20);
            this.Controls.Add(lblRating);

            // Số điện thoại
            lblPhone.Text = "0901.234.567";
            lblPhone.Font = new Font("Segoe UI", 8F);
            lblPhone.ForeColor = Color.Gray;
            lblPhone.TextAlign = ContentAlignment.MiddleCenter;
            lblPhone.Location = new Point(0, 115);
            lblPhone.Size = new Size(200, 20);
            this.Controls.Add(lblPhone);
        }

        /// <summary>
        /// Kết nối dữ liệu từ BUS/DTO vào giao diện
        /// </summary>
        public void BindData(DriverDTO driver)
        {
            if (driver == null) return;
            _driverData = driver;

            lblDriverName.Text = driver.FullName;
            lblPhone.Text = driver.Phone;
            lblRating.Text = "⭐ " + (driver.DeliveryCount > 50 ? "5.0" : "4.8") + " (" + driver.DeliveryCount + " đơn)";
            
            // Có thể thay đổi màu sắc dựa trên trạng thái (WorkStatus)
            if (driver.WorkStatus == "Available")
                lblDriverName.ForeColor = Color.DarkGreen;
        }
    }
}
