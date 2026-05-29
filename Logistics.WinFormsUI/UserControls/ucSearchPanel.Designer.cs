using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.UserControls
{
    partial class ucSearchPanel
    {
        private void InitializeComponent()
        {
            txtSearch = new TextBox();
            btnFilter = new Button();
            pnlContainer = new Panel();
            SuspendLayout();
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(0, 0);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(100, 23);
            txtSearch.TabIndex = 0;
            // 
            // btnFilter
            // 
            btnFilter.Location = new Point(0, 0);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(75, 23);
            btnFilter.TabIndex = 0;
            // 
            // pnlContainer
            // 
            pnlContainer.Location = new Point(0, 0);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(200, 100);
            pnlContainer.TabIndex = 0;
            // 
            // ucSearchPanel
            // 
            BackColor = Color.Transparent;
            Name = "ucSearchPanel";
            Size = new Size(500, 50);
            ResumeLayout(false);
        }
    }
}