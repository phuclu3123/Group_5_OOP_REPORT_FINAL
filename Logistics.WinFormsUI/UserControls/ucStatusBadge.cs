using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.UserControls
{
    /// <summary>
    /// Displays a colored status badge label.
    /// </summary>
    public partial class ucStatusBadge : UserControl
    {
        private string _status = string.Empty;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                UpdateAppearance();
            }
        }

        public ucStatusBadge()
        {
            InitializeComponent();
        }

        private void UpdateAppearance()
        {
            // Color mapping per status - extend as needed
        }
    }
}
