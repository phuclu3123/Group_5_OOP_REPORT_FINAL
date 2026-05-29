using System.Windows.Forms;
using Logistics.WinFormsUI.UserControls;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmDashboard : Form
    {
        public FrmDashboard()
        {
            InitializeComponent();
            if (!DesignerHelper.IsInDesignMode(this))
            {
                Controls.Clear();
                ucDashboard control = new ucDashboard(
                    DependencyContainer.GetReportService(),
                    DependencyContainer.GetOrderService());
                control.Dock = DockStyle.Fill;
                Controls.Add(control);
            }
        }
    }
}
