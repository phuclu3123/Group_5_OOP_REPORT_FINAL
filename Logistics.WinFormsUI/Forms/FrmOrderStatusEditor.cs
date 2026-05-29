using System.Windows.Forms;
using Logistics.Core.Models.Common;
using Logistics.Core.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmOrderStatusEditor : Form
    {
        public OrderStatus SelectedStatus
        {
            get { return ParseOrderStatus(cbStatus.SelectedItem?.ToString() ?? string.Empty); }
        }

        public FrmOrderStatusEditor(string trackingNumber, OrderStatus currentStatus)
        {
            InitializeComponent();
            LoadStatusOptions();
            lblTitle.Text = "Doi trang thai " + trackingNumber;
            cbStatus.SelectedItem = EnumTranslator.TranslateOrderStatus(currentStatus);
            if (cbStatus.SelectedIndex < 0)
            {
                cbStatus.SelectedIndex = 0;
            }
        }

        private void LoadStatusOptions()
        {
            cbStatus.Items.Clear();
            cbStatus.Items.Add(EnumTranslator.TranslateOrderStatus(OrderStatus.Pending));
            cbStatus.Items.Add(EnumTranslator.TranslateOrderStatus(OrderStatus.WaitingPickup));
            cbStatus.Items.Add(EnumTranslator.TranslateOrderStatus(OrderStatus.PickedUp));
            cbStatus.Items.Add(EnumTranslator.TranslateOrderStatus(OrderStatus.ArrivedAtWarehouse));
            cbStatus.Items.Add(EnumTranslator.TranslateOrderStatus(OrderStatus.Sorting));
            cbStatus.Items.Add(EnumTranslator.TranslateOrderStatus(OrderStatus.ReadyForDispatch));
            cbStatus.Items.Add(EnumTranslator.TranslateOrderStatus(OrderStatus.InTransit));
            cbStatus.Items.Add(EnumTranslator.TranslateOrderStatus(OrderStatus.OutForDelivery));
            cbStatus.Items.Add(EnumTranslator.TranslateOrderStatus(OrderStatus.DeliveryAttemptFailed));
            cbStatus.Items.Add(EnumTranslator.TranslateOrderStatus(OrderStatus.Delivered));
            cbStatus.Items.Add(EnumTranslator.TranslateOrderStatus(OrderStatus.Cancelled));
            cbStatus.Items.Add(EnumTranslator.TranslateOrderStatus(OrderStatus.Returning));
            cbStatus.Items.Add(EnumTranslator.TranslateOrderStatus(OrderStatus.Returned));
            cbStatus.Items.Add(EnumTranslator.TranslateOrderStatus(OrderStatus.Failed));
        }

        private static OrderStatus ParseOrderStatus(string status)
        {
            if (status == EnumTranslator.TranslateOrderStatus(OrderStatus.WaitingPickup))
            {
                return OrderStatus.WaitingPickup;
            }
            if (status == EnumTranslator.TranslateOrderStatus(OrderStatus.PickedUp))
            {
                return OrderStatus.PickedUp;
            }
            if (status == EnumTranslator.TranslateOrderStatus(OrderStatus.ArrivedAtWarehouse))
            {
                return OrderStatus.ArrivedAtWarehouse;
            }
            if (status == EnumTranslator.TranslateOrderStatus(OrderStatus.Sorting))
            {
                return OrderStatus.Sorting;
            }
            if (status == EnumTranslator.TranslateOrderStatus(OrderStatus.ReadyForDispatch))
            {
                return OrderStatus.ReadyForDispatch;
            }
            if (status == EnumTranslator.TranslateOrderStatus(OrderStatus.OutForDelivery))
            {
                return OrderStatus.OutForDelivery;
            }
            if (status == EnumTranslator.TranslateOrderStatus(OrderStatus.DeliveryAttemptFailed))
            {
                return OrderStatus.DeliveryAttemptFailed;
            }
            if (status == EnumTranslator.TranslateOrderStatus(OrderStatus.Returning))
            {
                return OrderStatus.Returning;
            }
            if (status == EnumTranslator.TranslateOrderStatus(OrderStatus.InTransit))
            {
                return OrderStatus.InTransit;
            }
            if (status == EnumTranslator.TranslateOrderStatus(OrderStatus.Delivered))
            {
                return OrderStatus.Delivered;
            }
            if (status == EnumTranslator.TranslateOrderStatus(OrderStatus.Cancelled))
            {
                return OrderStatus.Cancelled;
            }
            if (status == EnumTranslator.TranslateOrderStatus(OrderStatus.Returned))
            {
                return OrderStatus.Returned;
            }
            if (status == EnumTranslator.TranslateOrderStatus(OrderStatus.Failed))
            {
                return OrderStatus.Failed;
            }

            return OrderStatus.Pending;
        }
    }
}
