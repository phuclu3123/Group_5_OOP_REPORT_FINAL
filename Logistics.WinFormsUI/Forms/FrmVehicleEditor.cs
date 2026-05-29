using System;
using System.Drawing;
using System.Windows.Forms;
using Logistics.Core.Models.Common;
using Logistics.Core.Models.Infrastructure;
using Logistics.Core.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmVehicleEditor : Form
    {
        public bool IsEditMode { get; private set; }
        private TextBox txtVolume = null!;
        private TextBox txtDimensions = null!;
        private TextBox txtOdometer = null!;
        private TextBox txtFuelLevel = null!;
        private CheckBox chkRefrigerated = null!;

        public FrmVehicleEditor(string title = "Thêm phương tiện mới")
        {
            InitializeComponent();
            lblTitle.Text = title;
            IsEditMode = false;
            AddExtendedFields();
        }

        public FrmVehicleEditor(Vehicle vehicle) : this("Sửa thông tin phương tiện")
        {
            IsEditMode = true;
            txtVehicleId.Text = vehicle.VehicleID;
            txtVehicleId.Enabled = false;
            cbType.SelectedItem = EnumTranslator.TranslateVehicleType(vehicle.VehicleType);
            txtCapacity.Text = vehicle.MaxWeightCapacity.ToString();
            cbStatus.SelectedItem = EnumTranslator.TranslateVehicleStatus(vehicle.Status);
            txtVolume.Text = vehicle.MaxVolumeCapacity.ToString();
            txtDimensions.Text = vehicle.Dimensions;
            txtOdometer.Text = vehicle.CurrentOdometer.ToString();
            txtFuelLevel.Text = vehicle.FuelLevel.ToString();
            chkRefrigerated.Checked = vehicle.IsRefrigerated;
        }

        private void AddExtendedFields()
        {
            ClientSize = new Size(460, 650);
            btnSave.Location = new Point(250, 590);
            btnCancel.Location = new Point(350, 590);

            Label lblVolume = CreateLabel("Thể tích thùng hàng (m3):", 340);
            txtVolume = CreateTextBox(365, "VD: 12");
            Label lblDimensions = CreateLabel("Kích thước thùng:", 405);
            txtDimensions = CreateTextBox(430, "VD: 3.2m x 1.8m x 1.9m");
            Label lblOdometer = CreateLabel("Số km hiện tại:", 470);
            txtOdometer = CreateTextBox(495, "0");
            txtOdometer.Width = 180;
            Label lblFuel = CreateLabel("Nhiên liệu (%):", 470);
            lblFuel.Location = new Point(250, 470);
            txtFuelLevel = CreateTextBox(495, "100");
            txtFuelLevel.Location = new Point(250, 495);
            txtFuelLevel.Width = 180;
            chkRefrigerated = new CheckBox
            {
                Text = "Xe lạnh / có thùng điều nhiệt",
                Location = new Point(30, 535),
                Size = new Size(300, 24),
                Font = new Font("Segoe UI", 9.5F)
            };

            Controls.Add(lblVolume);
            Controls.Add(txtVolume);
            Controls.Add(lblDimensions);
            Controls.Add(txtDimensions);
            Controls.Add(lblOdometer);
            Controls.Add(txtOdometer);
            Controls.Add(lblFuel);
            Controls.Add(txtFuelLevel);
            Controls.Add(chkRefrigerated);
        }

        private static Label CreateLabel(string text, int y)
        {
            return new Label { Text = text, Location = new Point(30, y), AutoSize = true, Font = new Font("Segoe UI", 9.5F) };
        }

        private static TextBox CreateTextBox(int y, string placeholder)
        {
            return new TextBox { Location = new Point(30, y), Size = new Size(400, 27), Font = new Font("Segoe UI", 10F), PlaceholderText = placeholder };
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtVehicleId.Text))
            {
                MessageBox.Show("Vui lòng nhập biển số xe.", "Thông báo");
                return;
            }

            if (!double.TryParse(txtCapacity.Text, out _) || !double.TryParse(txtVolume.Text, out _) ||
                !double.TryParse(txtOdometer.Text, out _) || !double.TryParse(txtFuelLevel.Text, out _))
            {
                MessageBox.Show("Tải trọng, thể tích, số km hoặc nhiên liệu không hợp lệ.", "Thông báo");
                return;
            }

            DialogResult = DialogResult.OK;
        }

        public string VehicleID
        {
            get { return txtVehicleId.Text; }
        }

        public VehicleType VehicleType
        {
            get { return EnumTranslator.ParseVehicleType(cbType.SelectedItem?.ToString() ?? string.Empty); }
        }

        public double Capacity
        {
            get { return double.Parse(txtCapacity.Text); }
        }

        public double VolumeCapacity
        {
            get { return double.Parse(txtVolume.Text); }
        }

        public string Dimensions
        {
            get { return txtDimensions.Text.Trim(); }
        }

        public bool IsRefrigerated
        {
            get { return chkRefrigerated.Checked; }
        }

        public double CurrentOdometer
        {
            get { return double.Parse(txtOdometer.Text); }
        }

        public double FuelLevel
        {
            get { return double.Parse(txtFuelLevel.Text); }
        }

        public VehicleStatus Status
        {
            get { return EnumTranslator.ParseVehicleStatus(cbStatus.SelectedItem?.ToString() ?? string.Empty); }
        }
    }
}
