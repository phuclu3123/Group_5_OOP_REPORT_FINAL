using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Logistics.Core.DTOs;
using Logistics.Core.Models.Infrastructure;
using Logistics.Core.Services.Interfaces;
using Logistics.Core.Utilities;
using Logistics.WinFormsUI.Forms;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.UserControls
{
    public partial class ucMaintenanceManagement : UserControl
    {
        private IMaintenanceService? _maintenanceService;

        public ucMaintenanceManagement()
        {
            InitializeComponent();
            if (DesignerHelper.IsInDesignMode(this))
            {
                return;
            }

            _maintenanceService = DependencyContainer.GetMaintenanceService();
            UIStyleHelper.ApplyGridViewStyle(dgvMaintenance);
            LoadVehicleChoices();
            LoadData();
        }

        private void LoadVehicleChoices()
        {
            cbVehicles.Items.Clear();
            foreach (Vehicle vehicle in DependencyContainer.GetVehicleService().GetAllVehicles())
            {
                cbVehicles.Items.Add(vehicle.VehicleID + " - " + EnumTranslator.TranslateVehicleType(vehicle.VehicleType));
            }

            if (cbVehicles.Items.Count > 0)
            {
                cbVehicles.SelectedIndex = 0;
            }
        }

        private void CbVehicles_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string selected = cbVehicles.SelectedItem?.ToString() ?? string.Empty;
            int dashIndex = selected.IndexOf(" - ", StringComparison.Ordinal);
            txtVehicleId.Text = dashIndex > 0 ? selected.Substring(0, dashIndex) : selected;
        }

        private void LoadData()
        {
            if (_maintenanceService == null)
            {
                return;
            }

            List<MaintenanceDTO> rows = chkDueOnly.Checked
                ? _maintenanceService.GetDueMaintenance()
                : _maintenanceService.GetAllMaintenanceLogs();

            dgvMaintenance.DataSource = rows;
            ConfigureGrid();
        }

        private void ConfigureGrid()
        {
            SetColumnText("VehicleId", "Mã xe");
            SetColumnText("LogId", "Mã bảo trì");
            SetColumnText("ServiceDate", "Ngày bảo trì");
            SetColumnText("Description", "Nội dung");
            SetColumnText("ServiceProvider", "Đơn vị");
            SetColumnText("Cost", "Chi phí");
            SetColumnText("NextDueDate", "Bảo trì tiếp");
            SetColumnText("IsDue", "Đến hạn");
            SetColumnText("VehicleStatus", "Trạng thái xe");
        }

        private void SetColumnText(string name, string text)
        {
            if (dgvMaintenance.Columns[name] != null)
            {
                dgvMaintenance.Columns[name]!.HeaderText = text;
            }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            if (_maintenanceService == null)
            {
                return;
            }

            string vehicleId = txtVehicleId.Text.Trim();
            if (string.IsNullOrWhiteSpace(vehicleId))
            {
                MessageBox.Show("Chọn xe hoặc nhập mã xe trước khi thêm bảo trì.", "Bảo trì", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using FrmMaintenanceEditor editor = new FrmMaintenanceEditor();
            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                MaintenanceLog log = _maintenanceService.CreateMaintenanceLog(vehicleId, editor.ServiceDate, editor.Cost, editor.Description, editor.ServiceProvider, editor.NextDueDate);
                bool saved = _maintenanceService.AddMaintenanceLog(vehicleId, log);
                if (!saved)
                {
                    MessageBox.Show("Không tìm thấy xe hoặc không lưu được lịch sử bảo trì.", "Bảo trì", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                LoadData();
            }
        }

        private void BtnSendMaintenance_Click(object? sender, EventArgs e)
        {
            if (_maintenanceService == null)
            {
                return;
            }

            string vehicleId = txtVehicleId.Text.Trim();
            if (string.IsNullOrWhiteSpace(vehicleId))
            {
                MessageBox.Show("Chọn xe hoặc nhập mã xe cần đưa vào bảo trì.", "Bảo trì", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _maintenanceService.SendVehicleToMaintenance(vehicleId);
            LoadData();
        }

        private void BtnComplete_Click(object? sender, EventArgs e)
        {
            if (_maintenanceService == null)
            {
                return;
            }

            string vehicleId = txtVehicleId.Text.Trim();
            if (string.IsNullOrWhiteSpace(vehicleId))
            {
                MessageBox.Show("Chọn xe hoặc nhập mã xe cần hoàn tất bảo trì.", "Bảo trì", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _maintenanceService.CompleteMaintenance(vehicleId);
            LoadData();
        }

        private void ChkDueOnly_CheckedChanged(object? sender, EventArgs e)
        {
            LoadData();
        }
    }
}
