using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Logistics.Core.DTOs;
using Logistics.Core.Mappings;
using Logistics.Core.Models.Actors;
using Logistics.Core.Services.Interfaces;
using Logistics.WinFormsUI.Extensions;
using Logistics.WinFormsUI.Forms;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.UserControls
{
    public partial class ucCustomerManagement : UserControl
    {
        private ICustomerService? _customerService;
        private IOrderService? _orderService;
        private Button btnExportCsv = null!;

        private Panel pnlHeader = null!;
        private Label lblTitle = null!;
        private Button btnAddCustomer = null!;
        private Panel pnlSearch = null!;
        private TextBox txtSearch = null!;
        private DataGridView dgvCustomers = null!;

        public ucCustomerManagement()
        {
            InitializeComponent();
            if (DesignerHelper.IsInDesignMode(this))
            {
                return;
            }

            _customerService = DependencyContainer.GetCustomerService();
            _orderService = DependencyContainer.GetOrderService();
            UIStyleHelper.ApplyGridViewStyle(dgvCustomers);
            btnAddCustomer.SetRoundedBorder(10);
            InitializeExportButton();
            LoadCustomerData();
        }

        private void InitializeExportButton()
        {
            btnExportCsv = new Button();
            btnExportCsv.Location = new Point(360, 14);
            btnExportCsv.Size = new Size(130, 28);
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
                CsvExporter.ExportGrid(dgvCustomers, "DanhSachKhachHang.csv");
            };

            pnlSearch.Controls.Add(btnExportCsv);
        }

        private void BtnAddCustomer_Click(object? sender, EventArgs e)
        {
            if (_customerService == null)
            {
                return;
            }

            using (FrmCustomerEditor editor = new FrmCustomerEditor())
            {
                if (editor.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _customerService.AddCustomer(editor.FullName, editor.Phone, editor.Email, editor.Address, editor.CustomerType, editor.CreditLimit);
                        LoadCustomerData();
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message, "Du lieu khong hop le", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            if (_customerService == null || DesignerHelper.IsInDesignMode(this))
            {
                return;
            }

            string keyword = txtSearch.Text.ToLower();
            List<Customer> customers = _customerService.GetAllCustomers();
            List<CustomerDTO> rows = new List<CustomerDTO>();

            foreach (Customer customer in customers)
            {
                bool matched = string.IsNullOrEmpty(keyword) ||
                               customer.Id.ToLower().Contains(keyword) ||
                               customer.FullName.ToLower().Contains(keyword) ||
                               customer.PhoneNumber.ToLower().Contains(keyword);
                if (matched)
                {
                    CustomerDTO dto = customer.ToDTO();
                    if (_orderService != null)
                    {
                        dto.TotalOrders = _orderService.GetOrdersByCustomer(customer.Id).Count;
                    }
                    rows.Add(dto);
                }
            }

            dgvCustomers.DataSource = rows;
            ConfigureGridColumns();
        }

        private void ConfigureGridColumns()
        {
            if (dgvCustomers.Columns["CustomerId"] != null) dgvCustomers.Columns["CustomerId"]!.HeaderText = "Mã khách";
            if (dgvCustomers.Columns["FullName"] != null) dgvCustomers.Columns["FullName"]!.HeaderText = "Họ tên";
            if (dgvCustomers.Columns["Phone"] != null) dgvCustomers.Columns["Phone"]!.HeaderText = "Điện thoại";
            if (dgvCustomers.Columns["Email"] != null) dgvCustomers.Columns["Email"]!.HeaderText = "Email";
            if (dgvCustomers.Columns["Address"] != null) dgvCustomers.Columns["Address"]!.HeaderText = "Địa chỉ";
            if (dgvCustomers.Columns["CustomerType"] != null) dgvCustomers.Columns["CustomerType"]!.HeaderText = "Loại khách";
            if (dgvCustomers.Columns["LoyaltyPoints"] != null) dgvCustomers.Columns["LoyaltyPoints"]!.HeaderText = "Điểm";
            if (dgvCustomers.Columns["CreditLimit"] != null) dgvCustomers.Columns["CreditLimit"]!.HeaderText = "Hạn mức";
            if (dgvCustomers.Columns["TotalOrders"] != null) dgvCustomers.Columns["TotalOrders"]!.HeaderText = "Số đơn";

            if (!dgvCustomers.Columns.Contains("btnEdit"))
            {
                DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
                btnEdit.Name = "btnEdit";
                btnEdit.HeaderText = "";
                btnEdit.Text = "Sửa";
                btnEdit.UseColumnTextForButtonValue = true;
                dgvCustomers.Columns.Add(btnEdit);

                DataGridViewButtonColumn btnPoints = new DataGridViewButtonColumn();
                btnPoints.Name = "btnPoints";
                btnPoints.HeaderText = "";
                btnPoints.Text = "+10 điểm";
                btnPoints.UseColumnTextForButtonValue = true;
                dgvCustomers.Columns.Add(btnPoints);

                DataGridViewButtonColumn btnMinusPoints = new DataGridViewButtonColumn();
                btnMinusPoints.Name = "btnMinusPoints";
                btnMinusPoints.HeaderText = "";
                btnMinusPoints.Text = "-10 điểm";
                btnMinusPoints.UseColumnTextForButtonValue = true;
                dgvCustomers.Columns.Add(btnMinusPoints);

                DataGridViewButtonColumn btnResetPoints = new DataGridViewButtonColumn();
                btnResetPoints.Name = "btnResetPoints";
                btnResetPoints.HeaderText = "";
                btnResetPoints.Text = "Reset điểm";
                btnResetPoints.UseColumnTextForButtonValue = true;
                dgvCustomers.Columns.Add(btnResetPoints);

                DataGridViewButtonColumn btnPolicy = new DataGridViewButtonColumn();
                btnPolicy.Name = "btnPolicy";
                btnPolicy.HeaderText = "";
                btnPolicy.Text = "Quy định";
                btnPolicy.UseColumnTextForButtonValue = true;
                dgvCustomers.Columns.Add(btnPolicy);

                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                btnDelete.Name = "btnDelete";
                btnDelete.HeaderText = "";
                btnDelete.Text = "Xóa";
                btnDelete.UseColumnTextForButtonValue = true;
                dgvCustomers.Columns.Add(btnDelete);
            }
        }

        private void DgvCustomers_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (_customerService == null || e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            string customerId = dgvCustomers.Rows[e.RowIndex].Cells["CustomerId"].Value?.ToString() ?? string.Empty;
            Customer customer = _customerService.GetCustomerById(customerId);
            if (customer == null)
            {
                return;
            }

            string columnName = dgvCustomers.Columns[e.ColumnIndex].Name;
            if (columnName == "btnEdit")
            {
                EditCustomer(customer);
            }
            else if (columnName == "btnPoints")
            {
                _customerService.AddLoyaltyPoints(customerId, 10);
                LoadCustomerData();
            }
            else if (columnName == "btnMinusPoints")
            {
                _customerService.AdjustLoyaltyPoints(customerId, -10);
                LoadCustomerData();
            }
            else if (columnName == "btnResetPoints")
            {
                DialogResult result = MessageBox.Show("Reset điểm tích lũy của " + customer.FullName + " về 0?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    _customerService.ResetLoyaltyPoints(customerId);
                    LoadCustomerData();
                }
            }
            else if (columnName == "btnPolicy")
            {
                MessageBox.Show(_customerService.GetCustomerPolicySummary(customer), "Quy định hạng khách hàng", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (columnName == "btnDelete")
            {
                DeleteCustomer(customer);
            }
        }

        private void EditCustomer(Customer customer)
        {
            if (_customerService == null)
            {
                return;
            }

            using (FrmCustomerEditor editor = new FrmCustomerEditor(customer))
            {
                if (editor.ShowDialog() == DialogResult.OK)
                {
                    if (!_customerService.UpdateCustomerContact(customer.Id, editor.Phone, editor.Email, editor.Address))
                    {
                        MessageBox.Show("Khong the cap nhat khach hang vi du lieu lien he khong hop le.", "Du lieu khong hop le", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!_customerService.UpdateCustomerPolicy(customer.Id, editor.CustomerType, editor.CreditLimit))
                    {
                        MessageBox.Show("Khong the cap nhat khach hang vi chinh sach khong hop le.", "Du lieu khong hop le", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    LoadCustomerData();
                }
            }
        }

        private void DeleteCustomer(Customer customer)
        {
            if (_customerService == null)
            {
                return;
            }

            DialogResult result = MessageBox.Show("Xóa khách hàng " + customer.FullName + "?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                _customerService.DeleteCustomer(customer.Id);
                LoadCustomerData();
            }
        }
    }
}
