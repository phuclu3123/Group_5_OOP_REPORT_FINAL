using System.Windows.Forms;
using Logistics.WinFormsUI.UserControls;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmOrder : Form
    {
        public FrmOrder()
        {
            InitializeComponent();
            if (!DesignerHelper.IsInDesignMode(this))
            {
                Controls.Clear();
                ucOrderManagement control = new ucOrderManagement();
                control.Dock = DockStyle.Fill;
                Controls.Add(control);
            }
        }
    }
}
