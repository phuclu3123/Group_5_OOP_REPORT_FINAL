using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Logistics.Core.DTOs;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmAccountManagement : Form
    {
        public FrmAccountManagement()
        {
            InitializeComponent();
            LoadAccounts();
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            List<UserDTO> accounts = DependencyContainer.GetAccountManagementService().GetAllAccounts();
            dgvAccounts.DataSource = accounts;
        }

        private string? GetSelectedUsername()
        {
            if (dgvAccounts.CurrentRow == null || dgvAccounts.CurrentRow.DataBoundItem is not UserDTO dto)
            {
                MessageBox.Show("Vui long chon mot tai khoan.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            return dto.Username;
        }

        private void BtnResetPassword_Click(object? sender, EventArgs e)
        {
            string? username = GetSelectedUsername();
            if (username == null)
            {
                return;
            }

            AccountProvisionResultDTO result = DependencyContainer.GetAccountManagementService().ResetEmployeePassword(username);
            if (!result.Success)
            {
                MessageBox.Show(result.Message, "Khong the reset", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadAccounts();
            MessageBox.Show("Mat khau tam thoi cua " + result.Username + ": " + result.TemporaryPassword,
                "Reset thanh cong", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnDeactivate_Click(object? sender, EventArgs e)
        {
            string? username = GetSelectedUsername();
            if (username == null)
            {
                return;
            }

            DependencyContainer.GetAccountManagementService().DeactivateAccount(username);
            LoadAccounts();
        }

        private void BtnReactivate_Click(object? sender, EventArgs e)
        {
            string? username = GetSelectedUsername();
            if (username == null)
            {
                return;
            }

            DependencyContainer.GetAccountManagementService().ReactivateAccount(username);
            LoadAccounts();
        }
    }
}
