using System;
using System.Windows.Forms;
using Logistics.Core.Models.Business;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmPackageEditor : Form
    {
        public string Description
        {
            get { return txtDescription.Text.Trim(); }
        }

        public double ActualWeight
        {
            get
            {
                double value;
                if (double.TryParse(txtWeight.Text.Trim(), out value))
                {
                    return value;
                }
                return 0;
            }
        }

        public string Dimensions
        {
            get { return txtDimensions.Text.Trim(); }
        }

        public bool IsFragile
        {
            get { return chkFragile.Checked; }
        }

        public decimal DeclaredValue
        {
            get
            {
                decimal value;
                if (decimal.TryParse(txtValue.Text.Trim(), out value))
                {
                    return value;
                }
                return 0;
            }
        }

        public string ItemCategory
        {
            get { return txtCategory.Text.Trim(); }
        }

        public string HandlingInstructions
        {
            get { return txtHandling.Text.Trim(); }
        }

        public FrmPackageEditor()
        {
            InitializeComponent();
        }

        public FrmPackageEditor(Package package) : this()
        {
            if (package == null)
            {
                return;
            }

            txtDescription.Text = package.Description;
            txtWeight.Text = package.ActualWeight.ToString("N2");
            txtDimensions.Text = package.Dimensions;
            chkFragile.Checked = package.IsFragile;
            txtValue.Text = package.Value.ToString("N0");
            txtCategory.Text = package.ItemCategory;
            txtHandling.Text = package.HandlingInstructions;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Vui long nhap mo ta kien hang.", "Kien hang", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ActualWeight <= 0)
            {
                MessageBox.Show("Khoi luong phai lon hon 0.", "Kien hang", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDimensions.Text))
            {
                MessageBox.Show("Vui long nhap kich thuoc theo dang DxRxC cm, vi du 30x20x15.", "Kien hang", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
