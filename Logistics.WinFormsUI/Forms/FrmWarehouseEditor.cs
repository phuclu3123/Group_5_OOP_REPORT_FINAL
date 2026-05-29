using System;
using System.Drawing;
using System.Windows.Forms;
using Logistics.Core.Models.Infrastructure;
using Logistics.Core.Models.Common;
using Logistics.Core.Utilities;
using Logistics.WinFormsUI.Extensions;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmWarehouseEditor : Form
    {
        public bool IsEditMode { get; private set; }

        public FrmWarehouseEditor(string title = "Thêm Kho Bãi Mới")
        {
            InitializeComponent();
            lblTitle.Text = title;
            IsEditMode = false;
        }

        public FrmWarehouseEditor(Warehouse warehouse) : this("Sửa Thông Tin Kho Bãi")
        {
            IsEditMode = true;
            txtId.Text = warehouse.WarehouseID;
            txtId.Enabled = false;
            txtName.Text = warehouse.Name;
            txtAddress.Text = warehouse.Address;
            txtCapacity.Text = warehouse.TotalCapacity.ToString();
            cbType.SelectedItem = EnumTranslator.TranslateWarehouseType(warehouse.Type);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin bắt buộc.", "Thông báo");
                return;
            }

            if (!double.TryParse(txtCapacity.Text, out _))
            {
                MessageBox.Show("Sức chứa không hợp lệ.", "Thông báo");
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public string WarehouseID
        {
            get { return txtId.Text; }
        }

        public string WarehouseName
        {
            get { return txtName.Text; }
        }

        public string Address
        {
            get { return txtAddress.Text; }
        }

        public double Capacity
        {
            get { return double.Parse(txtCapacity.Text); }
        }

        public WarehouseType WarehouseType
        {
            get { return EnumTranslator.ParseWarehouseType(cbType.SelectedItem?.ToString() ?? string.Empty); }
        }
    }
}
