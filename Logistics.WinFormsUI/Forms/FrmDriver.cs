using System.Windows.Forms;
using Logistics.WinFormsUI.UserControls;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmDriver : Form
    {
        public FrmDriver()
        {
            InitializeComponent();
            if (!DesignerHelper.IsInDesignMode(this))
            {
                Controls.Clear();
                ucStaffManagement control = new ucStaffManagement();
                control.Dock = DockStyle.Fill;
                Controls.Add(control);
            }
        }
    }
}
