using System.ComponentModel;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.Utilities
{
    public static class DesignerHelper
    {
        public static bool IsInDesignMode(Control? control = null)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return true;
            }

            while (control != null)
            {
                if (control.Site?.DesignMode == true)
                {
                    return true;
                }

                control = control.Parent;
            }

            return false;
        }
    }
}
