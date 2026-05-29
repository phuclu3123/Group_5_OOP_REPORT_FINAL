using System;
using System.Windows.Forms;
using Logistics.Core.Models.Infrastructure;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmMaintenanceEditor : Form
    {
        public DateTime ServiceDate
        {
            get { return dtServiceDate.Value.Date; }
        }

        public decimal Cost
        {
            get
            {
                decimal value;
                if (decimal.TryParse(txtCost.Text.Trim(), out value))
                {
                    return value;
                }
                return 0;
            }
        }

        public string Description
        {
            get { return txtDescription.Text.Trim(); }
        }

        public string ServiceProvider
        {
            get { return txtProvider.Text.Trim(); }
        }

        public DateTime NextDueDate
        {
            get { return dtNextDueDate.Value.Date; }
        }

        public FrmMaintenanceEditor()
        {
            InitializeComponent();
            dtServiceDate.Value = DateTime.Today;
            dtNextDueDate.Value = DateTime.Today.AddMonths(3);
        }

        public FrmMaintenanceEditor(MaintenanceLog log) : this()
        {
            if (log == null)
            {
                return;
            }

            dtServiceDate.Value = log.ServiceDate;
            txtCost.Text = log.Cost.ToString("N0");
            txtDescription.Text = log.Description;
            txtProvider.Text = log.ServiceProvider;
            dtNextDueDate.Value = log.NextDueDate;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Vui long nhap noi dung bao tri.", "Bao tri", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtProvider.Text))
            {
                MessageBox.Show("Vui long nhap don vi bao tri.", "Bao tri", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtNextDueDate.Value.Date <= dtServiceDate.Value.Date)
            {
                MessageBox.Show("Ngay bao tri tiep theo phai sau ngay bao tri.", "Bao tri", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
