using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Logistics.Core.DTOs;
using Logistics.Core.Mappings;
using Logistics.Core.Models.Business;
using Logistics.Core.Services.Interfaces;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmOrderPackages : Form
    {
        private readonly string _trackingNumber;
        private IOrderService? _orderService;

        public FrmOrderPackages()
        {
            _trackingNumber = string.Empty;
            InitializeComponent();
        }

        public FrmOrderPackages(string trackingNumber) : this()
        {
            _trackingNumber = trackingNumber;
            Text = "Packages - " + trackingNumber;
            lblTitle.Text = "Kien hang cua don " + trackingNumber;

            if (!DesignerHelper.IsInDesignMode(this))
            {
                _orderService = DependencyContainer.GetOrderService();
                LoadPackages();
            }
        }

        private void LoadPackages()
        {
            if (_orderService == null)
            {
                return;
            }

            List<Package> packages = _orderService.GetPackagesByOrder(_trackingNumber);
            List<PackageDTO> rows = new List<PackageDTO>();
            for (int i = 0; i < packages.Count; i++)
            {
                rows.Add(packages[i].ToDTO());
            }

            dgvPackages.DataSource = rows;
            ConfigureGrid();
            UpdateSummary(packages);
        }

        private void ConfigureGrid()
        {
            if (dgvPackages.Columns["PackageId"] != null) dgvPackages.Columns["PackageId"]!.HeaderText = "Ma kien";
            if (dgvPackages.Columns["Description"] != null) dgvPackages.Columns["Description"]!.HeaderText = "Mo ta";
            if (dgvPackages.Columns["ActualWeightKg"] != null) dgvPackages.Columns["ActualWeightKg"]!.HeaderText = "KL thuc";
            if (dgvPackages.Columns["VolumeWeightKg"] != null) dgvPackages.Columns["VolumeWeightKg"]!.HeaderText = "KL quy doi";
            if (dgvPackages.Columns["ChargeableWeightKg"] != null) dgvPackages.Columns["ChargeableWeightKg"]!.HeaderText = "KL tinh cuoc";
            if (dgvPackages.Columns["Dimensions"] != null) dgvPackages.Columns["Dimensions"]!.HeaderText = "Kich thuoc";
            if (dgvPackages.Columns["ItemCategory"] != null) dgvPackages.Columns["ItemCategory"]!.HeaderText = "Nhom";
            if (dgvPackages.Columns["IsFragile"] != null) dgvPackages.Columns["IsFragile"]!.HeaderText = "De vo";
            if (dgvPackages.Columns["Value"] != null) dgvPackages.Columns["Value"]!.HeaderText = "Gia tri";
            if (dgvPackages.Columns["HandlingInstructions"] != null) dgvPackages.Columns["HandlingInstructions"]!.HeaderText = "Xu ly";
            if (dgvPackages.Columns["OrderId"] != null) dgvPackages.Columns["OrderId"]!.Visible = false;

            if (!dgvPackages.Columns.Contains("btnEdit"))
            {
                DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
                btnEdit.Name = "btnEdit";
                btnEdit.Text = "Sua";
                btnEdit.UseColumnTextForButtonValue = true;
                dgvPackages.Columns.Add(btnEdit);

                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                btnDelete.Name = "btnDelete";
                btnDelete.Text = "Xoa";
                btnDelete.UseColumnTextForButtonValue = true;
                dgvPackages.Columns.Add(btnDelete);
            }
        }

        private void UpdateSummary(List<Package> packages)
        {
            double actualWeight = 0;
            double chargeableWeight = 0;
            decimal declaredValue = 0;
            for (int i = 0; i < packages.Count; i++)
            {
                actualWeight += packages[i].ActualWeight;
                chargeableWeight += packages[i].GetChargeableWeight();
                declaredValue += packages[i].Value;
            }

            lblSummary.Text = "So kien: " + packages.Count + " | KL thuc: " + actualWeight.ToString("N2") + " kg | KL tinh cuoc: " + chargeableWeight.ToString("N2") + " kg | Gia tri: " + declaredValue.ToString("N0") + " VND";
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            if (_orderService == null)
            {
                return;
            }

            using (FrmPackageEditor editor = new FrmPackageEditor())
            {
                if (editor.ShowDialog(this) == DialogResult.OK)
                {
                    Package package = _orderService.CreatePackage(_trackingNumber, editor.Description, editor.ActualWeight, editor.Dimensions, editor.IsFragile, editor.DeclaredValue, editor.ItemCategory, editor.HandlingInstructions);
                    _orderService.AddPackageToOrder(_trackingNumber, package);
                    _orderService.CalculateOrderCost(_trackingNumber, 12000m);
                    LoadPackages();
                }
            }
        }

        private void DgvPackages_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (_orderService == null || e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            string packageId = dgvPackages.Rows[e.RowIndex].Cells["PackageId"].Value?.ToString() ?? string.Empty;
            Package package = FindPackage(packageId);
            if (package == null)
            {
                return;
            }

            string columnName = dgvPackages.Columns[e.ColumnIndex].Name;
            if (columnName == "btnEdit")
            {
                EditPackage(package);
            }
            else if (columnName == "btnDelete")
            {
                DeletePackage(package.PackageID);
            }
        }

        private Package FindPackage(string packageId)
        {
            if (_orderService == null)
            {
                return null!;
            }

            List<Package> packages = _orderService.GetPackagesByOrder(_trackingNumber);
            for (int i = 0; i < packages.Count; i++)
            {
                if (packages[i].PackageID == packageId)
                {
                    return packages[i];
                }
            }
            return null!;
        }

        private void EditPackage(Package package)
        {
            if (_orderService == null)
            {
                return;
            }

            using (FrmPackageEditor editor = new FrmPackageEditor(package))
            {
                if (editor.ShowDialog(this) == DialogResult.OK)
                {
                    _orderService.UpdatePackage(_trackingNumber, package.PackageID, editor.Description, editor.ActualWeight, editor.Dimensions, editor.IsFragile, editor.DeclaredValue, editor.ItemCategory, editor.HandlingInstructions);
                    _orderService.CalculateOrderCost(_trackingNumber, 12000m);
                    LoadPackages();
                }
            }
        }

        private void DeletePackage(string packageId)
        {
            if (_orderService == null)
            {
                return;
            }

            DialogResult result = MessageBox.Show("Xoa kien hang " + packageId + "?", "Xac nhan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                _orderService.RemovePackageFromOrder(_trackingNumber, packageId);
                _orderService.CalculateOrderCost(_trackingNumber, 12000m);
                LoadPackages();
            }
        }
    }
}
