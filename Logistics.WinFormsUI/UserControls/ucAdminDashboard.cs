using System;
using System.Drawing;
using System.Windows.Forms;
using Logistics.WinFormsUI.Forms;

namespace Logistics.WinFormsUI.UserControls
{
    public partial class ucAdminDashboard : UserControl
    {
        public ucAdminDashboard()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        private void BtnSettings_Click(object? sender, EventArgs e) => OpenSettings(sender, e);
        private void BtnTransactions_Click(object? sender, EventArgs e) => OpenTransactions(sender, e);
        private void BtnProblemReports_Click(object? sender, EventArgs e) => OpenProblemReports(sender, e);
        private void BtnChangePassword_Click(object? sender, EventArgs e) => OpenChangePassword(sender, e);
        private void BtnAccounts_Click(object? sender, EventArgs e) => OpenAccounts(sender, e);
        private void BtnAuditLog_Click(object? sender, EventArgs e) => OpenAuditLog(sender, e);

        private static void OpenSettings(object? sender, EventArgs e)
        {
            using FrmSettings form = new FrmSettings();
            form.ShowDialog();
        }

        private static void OpenTransactions(object? sender, EventArgs e)
        {
            using FrmTransactionLedger form = new FrmTransactionLedger();
            form.ShowDialog();
        }

        private static void OpenAccounts(object? sender, EventArgs e)
        {
            using FrmAccountManagement form = new FrmAccountManagement();
            form.ShowDialog();
        }

        private static void OpenAuditLog(object? sender, EventArgs e)
        {
            using FrmAuditLog form = new FrmAuditLog();
            form.ShowDialog();
        }

        private static void OpenProblemReports(object? sender, EventArgs e)
        {
            using FrmProblemReportLedger form = new FrmProblemReportLedger();
            form.ShowDialog();
        }

        private static void OpenChangePassword(object? sender, EventArgs e)
        {
            using FrmChangePassword form = new FrmChangePassword();
            form.ShowDialog();
        }
    }
}
