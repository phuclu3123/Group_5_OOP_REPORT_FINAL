using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.UserControls
{
    partial class ucDashboard
    {
        private void InitializeComponent()
        {
            flowPanel = new FlowLayoutPanel();
            pnlOrderCard = new Panel();
            pnlStaffCard = new Panel();
            pnlVehicleCard = new Panel();
            pnlWarehouseCard = new Panel();
            lblOrderValue = new Label();
            lblOrderTitle = new Label();
            lblStaffValue = new Label();
            lblStaffTitle = new Label();
            lblVehicleValue = new Label();
            lblVehicleTitle = new Label();
            lblWarehouseValue = new Label();
            lblWarehouseTitle = new Label();
            lblMainTitle = new Label();
            chartPanel = new Panel();
            flowPanel.SuspendLayout();
            SuspendLayout();
            // 
            // flowPanel
            // 
            flowPanel.Controls.Add(pnlOrderCard);
            flowPanel.Controls.Add(pnlStaffCard);
            flowPanel.Controls.Add(pnlVehicleCard);
            flowPanel.Controls.Add(pnlWarehouseCard);
            flowPanel.Dock = DockStyle.Top;
            flowPanel.Location = new Point(0, 0);
            flowPanel.Name = "flowPanel";
            flowPanel.Padding = new Padding(20);
            flowPanel.Size = new Size(895, 180);
            flowPanel.TabIndex = 2;
            // 
            // pnlOrderCard
            // 
            pnlOrderCard.Location = new Point(23, 23);
            pnlOrderCard.Name = "pnlOrderCard";
            pnlOrderCard.Size = new Size(200, 100);
            pnlOrderCard.TabIndex = 0;
            // 
            // pnlStaffCard
            // 
            pnlStaffCard.Location = new Point(229, 23);
            pnlStaffCard.Name = "pnlStaffCard";
            pnlStaffCard.Size = new Size(200, 100);
            pnlStaffCard.TabIndex = 1;
            // 
            // pnlVehicleCard
            // 
            pnlVehicleCard.Location = new Point(435, 23);
            pnlVehicleCard.Name = "pnlVehicleCard";
            pnlVehicleCard.Size = new Size(200, 100);
            pnlVehicleCard.TabIndex = 2;
            // 
            // pnlWarehouseCard
            // 
            pnlWarehouseCard.Location = new Point(641, 23);
            pnlWarehouseCard.Name = "pnlWarehouseCard";
            pnlWarehouseCard.Size = new Size(200, 100);
            pnlWarehouseCard.TabIndex = 3;
            // 
            // lblOrderValue
            // 
            lblOrderValue.Location = new Point(0, 0);
            lblOrderValue.Name = "lblOrderValue";
            lblOrderValue.Size = new Size(100, 23);
            lblOrderValue.TabIndex = 0;
            // 
            // lblOrderTitle
            // 
            lblOrderTitle.Location = new Point(0, 0);
            lblOrderTitle.Name = "lblOrderTitle";
            lblOrderTitle.Size = new Size(100, 23);
            lblOrderTitle.TabIndex = 0;
            // 
            // lblStaffValue
            // 
            lblStaffValue.Location = new Point(0, 0);
            lblStaffValue.Name = "lblStaffValue";
            lblStaffValue.Size = new Size(100, 23);
            lblStaffValue.TabIndex = 0;
            // 
            // lblStaffTitle
            // 
            lblStaffTitle.Location = new Point(0, 0);
            lblStaffTitle.Name = "lblStaffTitle";
            lblStaffTitle.Size = new Size(100, 23);
            lblStaffTitle.TabIndex = 0;
            // 
            // lblVehicleValue
            // 
            lblVehicleValue.Location = new Point(0, 0);
            lblVehicleValue.Name = "lblVehicleValue";
            lblVehicleValue.Size = new Size(100, 23);
            lblVehicleValue.TabIndex = 0;
            // 
            // lblVehicleTitle
            // 
            lblVehicleTitle.Location = new Point(0, 0);
            lblVehicleTitle.Name = "lblVehicleTitle";
            lblVehicleTitle.Size = new Size(100, 23);
            lblVehicleTitle.TabIndex = 0;
            // 
            // lblWarehouseValue
            // 
            lblWarehouseValue.Location = new Point(0, 0);
            lblWarehouseValue.Name = "lblWarehouseValue";
            lblWarehouseValue.Size = new Size(100, 23);
            lblWarehouseValue.TabIndex = 0;
            // 
            // lblWarehouseTitle
            // 
            lblWarehouseTitle.Location = new Point(0, 0);
            lblWarehouseTitle.Name = "lblWarehouseTitle";
            lblWarehouseTitle.Size = new Size(100, 23);
            lblWarehouseTitle.TabIndex = 0;
            // 
            // lblMainTitle
            // 
            lblMainTitle.AutoSize = true;
            lblMainTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblMainTitle.ForeColor = Color.DimGray;
            lblMainTitle.Location = new Point(30, 220);
            lblMainTitle.Name = "lblMainTitle";
            lblMainTitle.Size = new Size(311, 25);
            lblMainTitle.TabIndex = 1;
            lblMainTitle.Text = "BIỂU ĐỒ HOẠT ĐỘNG HỆ THỐNG";
            // 
            // chartPanel
            // 
            chartPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            chartPanel.BackColor = Color.White;
            chartPanel.Location = new Point(30, 260);
            chartPanel.Name = "chartPanel";
            chartPanel.Size = new Size(835, 300);
            chartPanel.TabIndex = 0;
            chartPanel.Paint += chartPanel_Paint;
            // 
            // ucDashboard
            // 
            BackColor = Color.WhiteSmoke;
            Controls.Add(chartPanel);
            Controls.Add(lblMainTitle);
            Controls.Add(flowPanel);
            Name = "ucDashboard";
            Size = new Size(895, 600);
            flowPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.FlowLayoutPanel flowPanel;
        private System.Windows.Forms.Panel pnlOrderCard;
        private System.Windows.Forms.Panel pnlStaffCard;
        private System.Windows.Forms.Panel pnlVehicleCard;
        private System.Windows.Forms.Panel pnlWarehouseCard;
        private System.Windows.Forms.Label lblOrderValue;
        private System.Windows.Forms.Label lblOrderTitle;
        private System.Windows.Forms.Label lblStaffValue;
        private System.Windows.Forms.Label lblStaffTitle;
        private System.Windows.Forms.Label lblVehicleValue;
        private System.Windows.Forms.Label lblVehicleTitle;
        private System.Windows.Forms.Label lblWarehouseValue;
        private System.Windows.Forms.Label lblWarehouseTitle;
        private System.Windows.Forms.Label lblMainTitle;
        private System.Windows.Forms.Panel chartPanel;
    }
}