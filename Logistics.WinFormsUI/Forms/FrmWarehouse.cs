using System.Windows.Forms;
using Logistics.WinFormsUI.UserControls;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmWarehouse : Form
    {
        public FrmWarehouse()
        {
            InitializeComponent();
            if (!DesignerHelper.IsInDesignMode(this))
            {
                Controls.Clear();
                ucWarehouseManagement control = new ucWarehouseManagement();
                control.Dock = DockStyle.Fill;
                Controls.Add(control);
            }
        }
    }
}
