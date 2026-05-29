using System;
using System.Windows.Forms;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Common;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmCustomerEditor : Form
    {
        private readonly Customer? _customer;

        public FrmCustomerEditor()
        {
            InitializeComponent();
            cbCustomerType.SelectedIndex = 0;
        }

        public FrmCustomerEditor(Customer customer) : this()
        {
            _customer = customer;
            Text = "Sua khach hang";
            lblTitle.Text = "Sua khach hang";
            txtFullName.Text = customer.FullName;
            txtFullName.Enabled = false;
            txtPhone.Text = customer.PhoneNumber;
            txtEmail.Text = customer.Email;
            txtAddress.Text = customer.HomeAddress != null ? customer.HomeAddress.ToString() : string.Empty;
            cbCustomerType.SelectedItem = customer.CustomerType.ToString();
            txtCreditLimit.Text = customer.CreditLimit.ToString();
        }

        public string FullName
        {
            get { return txtFullName.Text.Trim(); }
        }

        public string Phone
        {
            get { return txtPhone.Text.Trim(); }
        }

        public string Email
        {
            get { return txtEmail.Text.Trim(); }
        }

        public Address Address
        {
            get { return new Address(txtAddress.Text.Trim(), string.Empty, string.Empty, string.Empty, "000000", "Vietnam"); }
        }

        public CustomerType CustomerType
        {
            get
            {
                CustomerType type;
                if (Enum.TryParse(cbCustomerType.SelectedItem?.ToString(), out type))
                {
                    return type;
                }

                return CustomerType.Standard;
            }
        }

        public decimal CreditLimit
        {
            get
            {
                decimal value;
                if (decimal.TryParse(txtCreditLimit.Text, out value))
                {
                    return value;
                }

                return 0;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullName) || string.IsNullOrWhiteSpace(Phone))
            {
                MessageBox.Show("Vui long nhap ten va so dien thoai.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CreditLimit < 0)
            {
                MessageBox.Show("Han muc tin dung khong hop le.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
