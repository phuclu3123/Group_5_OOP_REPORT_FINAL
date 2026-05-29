using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Logistics.Core.Models.Business;

namespace Logistics.WinFormsUI.UserControls
{
    /// <summary>
    /// Timeline UserControl showing order status history (like Shopee/GHTK tracking).
    /// </summary>
    public partial class ucOrderTimeline : UserControl
    {
        private List<OrderStatusHistory> _historyItems;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<OrderStatusHistory> HistoryItems
        {
            get { return _historyItems; }
            set
            {
                _historyItems = value;
                RenderTimeline();
            }
        }

        public ucOrderTimeline()
        {
            InitializeComponent();
            _historyItems = new List<OrderStatusHistory>();
        }

        private void RenderTimeline()
        {
            this.Controls.Clear();
            if (_historyItems == null)
            {
                return;
            }

            int yOffset = 10;
            foreach (OrderStatusHistory item in _historyItems)
            {
                System.Windows.Forms.Label label = new System.Windows.Forms.Label();
                label.Text = item.ChangedAt.ToString("dd/MM/yyyy HH:mm") + "  " + item.NewStatus.ToString() + "  " + item.Description;
                label.Location = new System.Drawing.Point(20, yOffset);
                label.AutoSize = true;
                this.Controls.Add(label);
                yOffset += 30;
            }
        }
    }
}
