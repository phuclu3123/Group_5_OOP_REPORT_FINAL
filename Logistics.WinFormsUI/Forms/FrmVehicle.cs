using System.Windows.Forms;
using Logistics.WinFormsUI.UserControls;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmVehicle : Form
    {
        public FrmVehicle()
        {
            InitializeComponent();
            if (!DesignerHelper.IsInDesignMode(this))
            {
                Controls.Clear();
                ucVehicleManagement control = new ucVehicleManagement();
                control.Dock = DockStyle.Fill;
                Controls.Add(control);
            }
        }
    }
}
