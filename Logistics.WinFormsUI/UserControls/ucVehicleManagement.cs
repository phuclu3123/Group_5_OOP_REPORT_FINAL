using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Logistics.Core.DataAccess.Repositories;
using Logistics.Core.DTOs;
using Logistics.Core.Mappings;
using Logistics.Core.Models.Infrastructure;
using Logistics.Core.Services.Interfaces;
using Logistics.Core.Utilities;
using Logistics.WinFormsUI.Extensions;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.UserControls
{
    public partial class ucVehicleManagement : UserControl
    {
        private IVehicleService? _vehicleService;

        private Panel pnlHeader = null!;
        private Label lblTitle = null!;
        private Button btnAddVehicle = null!;
        private Panel pnlSearch = null!;
        private TextBox txtSearch = null!;
        private ComboBox cbTypeFilter = null!;
        private DataGridView dgvVehicles = null!;

        public ucVehicleManagement()
        {
            InitializeComponent();
            if (DesignerHelper.IsInDesignMode(this))
            {
                return;
            }

            _vehicleService = DependencyContainer.GetVehicleService();
            UIStyleHelper.ApplyGridViewStyle(dgvVehicles);
            btnAddVehicle.SetRoundedBorder(10);
            NormalizeFilterItems();
            LoadVehicleData();
        }

        private void NormalizeFilterItems()
        {
            cbTypeFilter.Items.Clear();
            cbTypeFilter.Items.Add("Tất cả loại xe");
            cbTypeFilter.Items.Add(EnumTranslator.TranslateVehicleType(Logistics.Core.Models.Common.VehicleType.Motorbike));
            cbTypeFilter.Items.Add(EnumTranslator.TranslateVehicleType(Logistics.Core.Models.Common.VehicleType.Van));
            cbTypeFilter.Items.Add(EnumTranslator.TranslateVehicleType(Logistics.Core.Models.Common.VehicleType.Truck_1Ton));
            cbTypeFilter.Items.Add(EnumTranslator.TranslateVehicleType(Logistics.Core.Models.Common.VehicleType.Container_40ft));
            cbTypeFilter.Items.Add(EnumTranslator.TranslateVehicleType(Logistics.Core.Models.Common.VehicleType.ColdStorageTruck));
            cbTypeFilter.SelectedIndex = 0;
            txtSearch.PlaceholderText = "Tìm biển số xe...";
        }

        private void btnAddVehicle_Click(object sender, EventArgs e)
        {
            if (_vehicleService == null)
            {
                return;
            }

            using (Forms.FrmVehicleEditor editor = new Forms.FrmVehicleEditor())
            {
                if (editor.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Vehicle vehicle = new Vehicle(
                            editor.VehicleID,
                            editor.VehicleType,
                            editor.Capacity,
                            editor.VolumeCapacity,
                            editor.Dimensions,
                            editor.IsRefrigerated);
                        vehicle.UpdateOperationalDetails(editor.Dimensions, editor.IsRefrigerated, editor.FuelLevel, editor.CurrentOdometer);
                        vehicle.UpdateStatus(editor.Status);
                        _vehicleService.AddVehicle(vehicle);
                        LoadVehicleData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi khi thêm phương tiện", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            LoadVehicleData();
        }

        private void LoadVehicleData()
        {
            if (_vehicleService == null || DesignerHelper.IsInDesignMode(this))
            {
                return;
            }

            string searchText = txtSearch.Text.ToLower();
            string typeFilter = cbTypeFilter.SelectedItem?.ToString() ?? "Tất cả loại xe";
            List<Vehicle> allVehicles = _vehicleService.GetAllVehicles();
            List<VehicleDTO> filtered = new List<VehicleDTO>();

            foreach (Vehicle vehicle in allVehicles)
            {
                bool matchesSearch = string.IsNullOrEmpty(searchText) || vehicle.VehicleID.ToLower().Contains(searchText);
                bool matchesType = typeFilter == "Tất cả loại xe" || EnumTranslator.TranslateVehicleType(vehicle.VehicleType) == typeFilter;
                if (matchesSearch && matchesType)
                {
                    filtered.Add(vehicle.ToDTO());
                }
            }

            dgvVehicles.DataSource = filtered;
            ConfigureGridColumns();
        }

        private void ConfigureGridColumns()
        {
            if (dgvVehicles.Columns["VehicleId"] != null) dgvVehicles.Columns["VehicleId"]!.HeaderText = "Mã xe";
            if (dgvVehicles.Columns["VehicleType"] != null) dgvVehicles.Columns["VehicleType"]!.HeaderText = "Loại xe";
            if (dgvVehicles.Columns["MaxWeightCapacityKg"] != null) dgvVehicles.Columns["MaxWeightCapacityKg"]!.HeaderText = "Tải trọng (kg)";
            if (dgvVehicles.Columns["MaxVolumeCapacityM3"] != null) dgvVehicles.Columns["MaxVolumeCapacityM3"]!.HeaderText = "Thể tích (m3)";
            if (dgvVehicles.Columns["CurrentOdometerKm"] != null) dgvVehicles.Columns["CurrentOdometerKm"]!.HeaderText = "Số km";
            if (dgvVehicles.Columns["IsRefrigerated"] != null) dgvVehicles.Columns["IsRefrigerated"]!.HeaderText = "Xe lạnh";
            if (dgvVehicles.Columns["IsAvailable"] != null) dgvVehicles.Columns["IsAvailable"]!.HeaderText = "Sẵn sàng";
            if (dgvVehicles.Columns["StatusDisplay"] != null) dgvVehicles.Columns["StatusDisplay"]!.HeaderText = "Trạng thái";
            if (dgvVehicles.Columns["FuelLevel"] != null) dgvVehicles.Columns["FuelLevel"]!.HeaderText = "Nhiên liệu (%)";
            if (dgvVehicles.Columns["Dimensions"] != null) dgvVehicles.Columns["Dimensions"]!.HeaderText = "Kích thước";

            if (!dgvVehicles.Columns.Contains("btnEdit"))
            {
                DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
                btnEdit.Name = "btnEdit";
                btnEdit.HeaderText = "";
                btnEdit.Text = "Sửa";
                btnEdit.UseColumnTextForButtonValue = true;
                btnEdit.FlatStyle = FlatStyle.Flat;
                dgvVehicles.Columns.Add(btnEdit);

                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                btnDelete.Name = "btnDelete";
                btnDelete.HeaderText = "";
                btnDelete.Text = "Xóa";
                btnDelete.UseColumnTextForButtonValue = true;
                btnDelete.FlatStyle = FlatStyle.Flat;
                dgvVehicles.Columns.Add(btnDelete);
            }
        }

        private void dgvVehicles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_vehicleService == null || e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            string id = dgvVehicles.Rows[e.RowIndex].Cells["VehicleId"].Value?.ToString() ?? string.Empty;
            if (dgvVehicles.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                Vehicle vehicle = _vehicleService.GetVehicleById(id);
                if (vehicle != null)
                {
                    using (Forms.FrmVehicleEditor editor = new Forms.FrmVehicleEditor(vehicle))
                    {
                        if (editor.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                vehicle.UpdateDetails(editor.VehicleType, editor.Capacity, editor.VolumeCapacity);
                                vehicle.UpdateOperationalDetails(editor.Dimensions, editor.IsRefrigerated, editor.FuelLevel, editor.CurrentOdometer);
                                vehicle.UpdateStatus(editor.Status);
                                _vehicleService.UpdateVehicle(vehicle);
                                LoadVehicleData();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Lỗi khi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            else if (dgvVehicles.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                DialogResult result = MessageBox.Show("Xác nhận xóa phương tiện " + id + "?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        _vehicleService.DeleteVehicle(id);
                        LoadVehicleData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi khi xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
    }
}
