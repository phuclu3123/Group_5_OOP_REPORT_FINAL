using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Logistics.Core.Models.Business;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmAuditLog : Form
    {
        public FrmAuditLog()
        {
            InitializeComponent();

            List<AuditLog> logs = DependencyContainer.GetAuditService().GetRecentLogs(500);
            grid.DataSource = logs;
        }
    }
}
