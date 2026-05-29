using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Infrastructure;
using Logistics.Core.Services.Interfaces;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmBarcodeScanner : Form
    {
        private readonly IWarehouseService _warehouseService;
        private readonly IOrderService _orderService;

        private Package? _scenteredPackage;

        public FrmBarcodeScanner()
        {
            _warehouseService = DependencyContainer.GetWarehouseService();
            _orderService = DependencyContainer.GetOrderService();
            InitializeComponent();
            LoadWarehouses();
            ThemeManager.ApplyTheme(this);
        }

        private void BtnClose_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadWarehouses()
        {
            List<Warehouse> warehouses = _warehouseService.GetAllWarehouses();
            foreach (Warehouse w in warehouses)
            {
                cbWarehouse.Items.Add(w.WarehouseID + " - " + w.Name);
            }
            if (cbWarehouse.Items.Count > 0)
            {
                cbWarehouse.SelectedIndex = 0;
            }
        }

        private void CbWarehouse_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Do nothing special, warehouse ID is retrieved from selection
        }

        private void TxtBarcode_TextChanged(object? sender, EventArgs e)
        {
            string id = txtBarcode.Text.Trim();
            if (string.IsNullOrEmpty(id))
            {
                ClearPackageDetails();
                return;
            }

            // Find package by ID
            Package? pkg = FindPackageById(id);
            if (pkg != null)
            {
                _scenteredPackage = pkg;
                lblPkgDesc.Text = "Mo ta: " + pkg.Description;
                lblPkgWeight.Text = "Khoi luong: " + pkg.ActualWeight.ToString("N1") + " kg";
                lblPkgCategory.Text = "Phan loai: " + pkg.ItemCategory;
                lblPkgFragile.Text = "Hang de vo: " + (pkg.IsFragile ? "CO (Nguy co cao)" : "Khong");
                grpPackageDetails.ForeColor = pkg.IsFragile ? Color.Red : ThemeManager.TextColor;
            }
            else
            {
                ClearPackageDetails();
                lblPkgDesc.Text = "Mo ta: (Khong tim thay goi hang)";
            }
        }

        private void ClearPackageDetails()
        {
            _scenteredPackage = null;
            lblPkgDesc.Text = "Mo ta: (Chua co thong tin)";
            lblPkgWeight.Text = "Khoi luong: -";
            lblPkgCategory.Text = "Phan loai: -";
            lblPkgFragile.Text = "Hang de vo: -";
            grpPackageDetails.ForeColor = ThemeManager.TextColor;
        }

        private Package? FindPackageById(string packageId)
        {
            List<Order> orders = _orderService.GetAllOrders();
            foreach (Order order in orders)
            {
                List<Package> packages = _orderService.GetPackagesByOrder(order.TrackingNumber);
                foreach (Package pkg in packages)
                {
                    if (pkg.PackageID.Equals(packageId, StringComparison.OrdinalIgnoreCase) ||
                        pkg.OrderID.Equals(packageId, StringComparison.OrdinalIgnoreCase))
                    {
                        return pkg;
                    }
                }
            }
            return null;
        }

        private void BtnCheckIn_Click(object? sender, EventArgs e)
        {
            if (cbWarehouse.SelectedIndex < 0)
            {
                MessageBox.Show("Vui long chon kho bai truoc.", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_scenteredPackage == null)
            {
                MessageBox.Show("Vui long nhap ma goi hang hop le.", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string warehouseText = cbWarehouse.SelectedItem?.ToString() ?? string.Empty;
            string warehouseId = warehouseText.Split('-')[0].Trim();
            string location = txtLocation.Text.Trim();

            // Perform check-in
            bool success = _warehouseService.StorePackage(warehouseId, _scenteredPackage, location);
            if (success)
            {
                string log = DateTime.Now.ToString("HH:mm:ss") + " | NHAP KHO | " + _scenteredPackage.PackageID + " -> " + warehouseId + " (" + location + ")";
                lstHistory.Items.Insert(0, log);
                MessageBox.Show("Nhap kho kien hang " + _scenteredPackage.PackageID + " thanh cong!", "Nhap kho", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string log = DateTime.Now.ToString("HH:mm:ss") + " | LOI NHAP KHO | " + _scenteredPackage.PackageID + " khong duoc nhap.";
                lstHistory.Items.Insert(0, log);
                MessageBox.Show("Khong the nhap kho kien hang. Kiem tra dung tich kho hoac kien hang da ton tai trong kho.", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCheckOut_Click(object? sender, EventArgs e)
        {
            if (cbWarehouse.SelectedIndex < 0)
            {
                MessageBox.Show("Vui long chon kho bai truoc.", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_scenteredPackage == null)
            {
                MessageBox.Show("Vui long nhap ma goi hang hop le.", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string warehouseText = cbWarehouse.SelectedItem?.ToString() ?? string.Empty;
            string warehouseId = warehouseText.Split('-')[0].Trim();

            // Perform check-out
            bool success = _warehouseService.ReleasePackage(warehouseId, _scenteredPackage);
            if (success)
            {
                string log = DateTime.Now.ToString("HH:mm:ss") + " | XUAT KHO | " + _scenteredPackage.PackageID + " <- " + warehouseId;
                lstHistory.Items.Insert(0, log);
                MessageBox.Show("Xuat kho kien hang " + _scenteredPackage.PackageID + " thanh cong!", "Xuat kho", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string log = DateTime.Now.ToString("HH:mm:ss") + " | LOI XUAT KHO | " + _scenteredPackage.PackageID + " khong co trong kho.";
                lstHistory.Items.Insert(0, log);
                MessageBox.Show("Khong the xuat kho kien hang. Kiem tra xem kien hang co dung la dang o trong kho nay khong.", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
