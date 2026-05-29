using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Logistics.Core.DTOs;
using Logistics.Core.Mappings;
using Logistics.Core.Models.Common;
using Logistics.Core.Models.Infrastructure;
using Logistics.Core.Services.Interfaces;
using Logistics.WinFormsUI.Forms;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.UserControls
{
    public partial class ucWarehouseManagement : UserControl
    {
        private IWarehouseService? _warehouseService;
        private List<WarehouseDTO> _allWarehouses = new List<WarehouseDTO>();
        private Button btnExportCsv = null!;
        private Button btnScanSimulate = null!;

        public ucWarehouseManagement()
        {
            InitializeComponent();
            if (DesignerHelper.IsInDesignMode(this))
            {
                return;
            }

            _warehouseService = DependencyContainer.GetWarehouseService();
            UIStyleHelper.ApplyGridViewStyle(dgvWarehouses);
            txtSearch.PlaceholderText = "Tìm kiếm kho bãi theo mã, tên hoặc địa chỉ...";
            InitializeExportButton();
            InitializeScanButton();
            LoadData();
        }

        private void InitializeScanButton()
        {
            btnScanSimulate = new Button();
            btnScanSimulate.Location = new Point(530, 9);
            btnScanSimulate.Size = new Size(160, 29);
            btnScanSimulate.Text = "Mô phỏng Quét mã 🔍";
            btnScanSimulate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnScanSimulate.BackColor = Color.FromArgb(46, 204, 113);
            btnScanSimulate.ForeColor = Color.White;
            btnScanSimulate.FlatStyle = FlatStyle.Flat;
            btnScanSimulate.FlatAppearance.BorderSize = 0;
            btnScanSimulate.Cursor = Cursors.Hand;
            btnScanSimulate.Tag = "KeepStyle";

            btnScanSimulate.Click += (sender, e) =>
            {
                using (FrmBarcodeScanner scanner = new FrmBarcodeScanner())
                {
                    scanner.ShowDialog(this);
                }
                LoadData();
            };

            pnlSearch.Controls.Add(btnScanSimulate);
        }

        private void InitializeExportButton()
        {
            btnExportCsv = new Button();
            btnExportCsv.Location = new Point(390, 9);
            btnExportCsv.Size = new Size(130, 29);
            btnExportCsv.Text = "Xuất CSV 📄";
            btnExportCsv.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExportCsv.BackColor = Color.FromArgb(52, 152, 219);
            btnExportCsv.ForeColor = Color.White;
            btnExportCsv.FlatStyle = FlatStyle.Flat;
            btnExportCsv.FlatAppearance.BorderSize = 0;
            btnExportCsv.Cursor = Cursors.Hand;
            btnExportCsv.Tag = "KeepStyle";

            btnExportCsv.Click += (sender, e) =>
            {
                CsvExporter.ExportGrid(dgvWarehouses, "DanhSachKhoBai.csv");
            };

            pnlSearch.Controls.Add(btnExportCsv);
        }

        private void LoadData()
        {
            if (_warehouseService == null || DesignerHelper.IsInDesignMode(this))
            {
                return;
            }

            try
            {
                List<Warehouse> warehouses = _warehouseService.GetAllWarehouses();
                _allWarehouses = new List<WarehouseDTO>();
                foreach (Warehouse warehouse in warehouses)
                {
                    _allWarehouses.Add(warehouse.ToDTO());
                }

                dgvWarehouses.DataSource = new BindingSource { DataSource = _allWarehouses };
                EnsureActionColumns();
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu kho bãi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnsureActionColumns()
        {
            if (!dgvWarehouses.Columns.Contains("btnEdit"))
            {
                DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
                btnEdit.Name = "btnEdit";
                btnEdit.HeaderText = "";
                btnEdit.Text = "Sửa";
                btnEdit.UseColumnTextForButtonValue = true;
                btnEdit.FlatStyle = FlatStyle.Flat;
                dgvWarehouses.Columns.Add(btnEdit);

                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                btnDelete.Name = "btnDelete";
                btnDelete.HeaderText = "";
                btnDelete.Text = "Xóa";
                btnDelete.UseColumnTextForButtonValue = true;
                btnDelete.FlatStyle = FlatStyle.Flat;
                dgvWarehouses.Columns.Add(btnDelete);
            }
        }

        private void FormatGrid()
        {
            if (dgvWarehouses.Columns.Count == 0)
            {
                return;
            }

            if (dgvWarehouses.Columns["WarehouseId"] != null) dgvWarehouses.Columns["WarehouseId"]!.HeaderText = "Mã kho";
            if (dgvWarehouses.Columns["Name"] != null) dgvWarehouses.Columns["Name"]!.HeaderText = "Tên kho";
            if (dgvWarehouses.Columns["WarehouseType"] != null) dgvWarehouses.Columns["WarehouseType"]!.HeaderText = "Loại kho";
            if (dgvWarehouses.Columns["Address"] != null) dgvWarehouses.Columns["Address"]!.HeaderText = "Địa chỉ";
            if (dgvWarehouses.Columns["TotalCapacityM3"] != null) dgvWarehouses.Columns["TotalCapacityM3"]!.HeaderText = "Sức chứa (m3)";
            if (dgvWarehouses.Columns["UsedCapacityM3"] != null) dgvWarehouses.Columns["UsedCapacityM3"]!.HeaderText = "Đã dùng";
            if (dgvWarehouses.Columns["AvailableCapacityM3"] != null) dgvWarehouses.Columns["AvailableCapacityM3"]!.HeaderText = "Còn trống";
            if (dgvWarehouses.Columns["UtilisationPercent"] != null) dgvWarehouses.Columns["UtilisationPercent"]!.HeaderText = "% sử dụng";
            if (dgvWarehouses.Columns["TotalLocations"] != null) dgvWarehouses.Columns["TotalLocations"]!.HeaderText = "Vị trí lưu trữ";
            if (dgvWarehouses.Columns["UtilisationPercent"] != null) dgvWarehouses.Columns["UtilisationPercent"]!.DefaultCellStyle.Format = "N1";
            if (dgvWarehouses.Columns["TotalCapacityM3"] != null) dgvWarehouses.Columns["TotalCapacityM3"]!.DefaultCellStyle.Format = "N0";
            if (dgvWarehouses.Columns["btnEdit"] != null) dgvWarehouses.Columns["btnEdit"]!.DisplayIndex = dgvWarehouses.Columns.Count - 2;
            if (dgvWarehouses.Columns["btnDelete"] != null) dgvWarehouses.Columns["btnDelete"]!.DisplayIndex = dgvWarehouses.Columns.Count - 1;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (_warehouseService == null)
            {
                return;
            }

            using (FrmWarehouseEditor editor = new FrmWarehouseEditor())
            {
                if (editor.ShowDialog() == DialogResult.OK)
                {
                    Warehouse newWarehouse = new Warehouse(
                        editor.WarehouseID,
                        editor.WarehouseName,
                        editor.Address,
                        new GeoPoint(0, 0),
                        editor.WarehouseType,
                        editor.Capacity,
                        "08:00 - 18:00",
                        null);

                    _warehouseService.AddWarehouse(newWarehouse);
                    LoadData();
                }
            }
        }

        private void DgvWarehouses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_warehouseService == null || e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            string warehouseId = dgvWarehouses.Rows[e.RowIndex].Cells["WarehouseId"].Value?.ToString() ?? string.Empty;
            Warehouse warehouse = _warehouseService.GetWarehouseById(warehouseId);
            if (warehouse == null)
            {
                return;
            }

            if (dgvWarehouses.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                using (FrmWarehouseEditor editor = new FrmWarehouseEditor(warehouse))
                {
                    if (editor.ShowDialog() == DialogResult.OK)
                    {
                        warehouse.UpdateInfo(editor.WarehouseName, editor.Address, editor.Capacity, editor.WarehouseType);
                        _warehouseService.UpdateWarehouse(warehouse);
                        LoadData();
                    }
                }
            }
            else if (dgvWarehouses.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa kho '" + warehouse.Name + "'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    _warehouseService.DeleteWarehouse(warehouseId);
                    LoadData();
                }
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.ToLower();
            List<WarehouseDTO> filtered = new List<WarehouseDTO>();
            foreach (WarehouseDTO warehouse in _allWarehouses)
            {
                if (warehouse.WarehouseId.ToLower().Contains(keyword) ||
                    warehouse.Name.ToLower().Contains(keyword) ||
                    warehouse.Address.ToLower().Contains(keyword))
                {
                    filtered.Add(warehouse);
                }
            }

            dgvWarehouses.DataSource = new BindingSource { DataSource = filtered };
            FormatGrid();
        }
    }
}
