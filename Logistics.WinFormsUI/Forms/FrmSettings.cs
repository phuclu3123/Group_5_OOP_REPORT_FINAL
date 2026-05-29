using System.Windows.Forms;
using System.IO;
using System.Drawing;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmSettings : Form
    {
        private const string ThemeLightText = "Sáng";
        private const string ThemeDarkText = "Tối";
        private const string DensityComfortableText = "Thoáng";
        private const string DensityCompactText = "Gọn";

        private TextBox txtStandardRate = null!;
        private TextBox txtExpressRate = null!;
        private TextBox txtInstantRate = null!;
        private TextBox txtVatRate = null!;
        private TextBox txtVndPerPoint = null!;
        private TextBox txtVipPoints = null!;
        private TextBox txtEnterprisePoints = null!;
        private TextBox txtVipDiscount = null!;
        private TextBox txtEnterpriseDiscount = null!;
        private CheckBox chkHighContrastStatus = null!;
        private CheckBox chkHighlightProblemOrders = null!;
        private CheckBox chkConfirmDelivered = null!;
        private ComboBox cbOrderGridDensity = null!;

        public FrmSettings()
        {
            InitializeComponent();
            AddBusinessSettingsUi();
            LoadSettings();
            ThemeManager.ApplyTheme(this);
        }

        private void LoadSettings()
        {
            txtDataPath.Text = string.IsNullOrWhiteSpace(AppSettings.DataDirectory)
                ? Path.Combine(Application.StartupPath, "Data")
                : AppSettings.DataDirectory;
            chkHighDpi.Checked = true;
            cbTheme.Items.Clear();
            cbTheme.Items.Add(ThemeLightText);
            cbTheme.Items.Add(ThemeDarkText);

            AppPersonalizationSettings personalization = AppPersonalizationSettings.Load();
            cbTheme.SelectedItem = personalization.ThemeName == "Dark" ? ThemeDarkText : ThemeLightText;

            AppBusinessSettings business = AppBusinessSettings.Load();
            txtStandardRate.Text = business.StandardRatePerKg.ToString("0");
            txtExpressRate.Text = business.ExpressRatePerKg.ToString("0");
            txtInstantRate.Text = business.InstantRatePerKg.ToString("0");
            txtVatRate.Text = (business.VatRate * 100m).ToString("0.##");
            txtVndPerPoint.Text = business.VndPerPoint.ToString();
            txtVipPoints.Text = business.VipPointThreshold.ToString();
            txtEnterprisePoints.Text = business.EnterprisePointThreshold.ToString();
            txtVipDiscount.Text = (business.VipDiscountRate * 100m).ToString("0.##");
            txtEnterpriseDiscount.Text = (business.EnterpriseDiscountRate * 100m).ToString("0.##");

            chkHighContrastStatus.Checked = personalization.HighContrastStatusColors;
            chkHighlightProblemOrders.Checked = personalization.HighlightProblemOrders;
            chkConfirmDelivered.Checked = personalization.ConfirmBeforeDeliveryComplete;
            cbOrderGridDensity.SelectedItem = personalization.OrderGridDensity == "Compact" ? DensityCompactText : DensityComfortableText;
        }

        private void AddBusinessSettingsUi()
        {
            ClientSize = new Size(840, 835);
            btnSave.Location = new Point(606, 775);
            btnCancel.Location = new Point(712, 775);

            GroupBox grpPricing = new GroupBox
            {
                Text = "Cấu hình cước và khách hàng",
                Location = new Point(32, 365),
                Size = new Size(760, 260)
            };

            txtStandardRate = AddField(grpPricing, "Đơn giá Standard/kg", 20, 34);
            txtExpressRate = AddField(grpPricing, "Đơn giá Express/kg", 270, 34);
            txtInstantRate = AddField(grpPricing, "Đơn giá Instant/kg", 520, 34);
            txtVatRate = AddField(grpPricing, "VAT (%)", 20, 98);
            txtVndPerPoint = AddField(grpPricing, "VNĐ / 1 điểm", 270, 98);
            txtVipPoints = AddField(grpPricing, "Ngưỡng VIP (điểm)", 520, 98);
            txtEnterprisePoints = AddField(grpPricing, "Ngưỡng Enterprise", 20, 162);
            txtVipDiscount = AddField(grpPricing, "Chiết khấu VIP (%)", 270, 162);
            txtEnterpriseDiscount = AddField(grpPricing, "Chiết khấu Enterprise (%)", 520, 162);

            Controls.Add(grpPricing);

            GroupBox grpPersonalization = new GroupBox
            {
                Text = "Cá nhân hóa thao tác",
                Location = new Point(32, 635),
                Size = new Size(760, 120)
            };

            chkHighContrastStatus = new CheckBox
            {
                Text = "Màu trạng thái tương phản cao",
                Location = new Point(20, 30),
                Size = new Size(260, 24)
            };
            chkHighlightProblemOrders = new CheckBox
            {
                Text = "Tô nền đơn hủy / sự cố",
                Location = new Point(300, 30),
                Size = new Size(220, 24)
            };
            chkConfirmDelivered = new CheckBox
            {
                Text = "Hỏi xác nhận trước khi đánh dấu đã giao",
                Location = new Point(20, 68),
                Size = new Size(300, 24)
            };
            cbOrderGridDensity = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(520, 68),
                Size = new Size(180, 27)
            };
            cbOrderGridDensity.Items.Add(DensityComfortableText);
            cbOrderGridDensity.Items.Add(DensityCompactText);
            cbOrderGridDensity.SelectedIndex = 0;
            Label lblDensity = new Label
            {
                Text = "Mật độ bảng đơn hàng",
                Location = new Point(520, 44),
                Size = new Size(180, 20)
            };

            grpPersonalization.Controls.Add(chkHighContrastStatus);
            grpPersonalization.Controls.Add(chkHighlightProblemOrders);
            grpPersonalization.Controls.Add(chkConfirmDelivered);
            grpPersonalization.Controls.Add(lblDensity);
            grpPersonalization.Controls.Add(cbOrderGridDensity);
            Controls.Add(grpPersonalization);
        }

        private static TextBox AddField(Control parent, string label, int x, int y)
        {
            Label lbl = new Label
            {
                Text = label,
                Location = new Point(x, y),
                Size = new Size(210, 20)
            };
            TextBox txt = new TextBox
            {
                Location = new Point(x, y + 24),
                Size = new Size(190, 27)
            };
            parent.Controls.Add(lbl);
            parent.Controls.Add(txt);
            return txt;
        }

        private void BtnBrowse_Click(object? sender, System.EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Chọn thư mục dữ liệu JSON";
                dialog.SelectedPath = Directory.Exists(txtDataPath.Text) ? txtDataPath.Text : Application.StartupPath;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    txtDataPath.Text = dialog.SelectedPath;
                }
            }
        }

        private void BtnSave_Click(object? sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDataPath.Text))
            {
                MessageBox.Show("Vui lòng chọn thư mục dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(txtDataPath.Text))
            {
                MessageBox.Show("Thư mục dữ liệu không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AppSettings.SaveDataDirectory(txtDataPath.Text);
            AppBusinessSettings business = new AppBusinessSettings
            {
                StandardRatePerKg = ReadDecimal(txtStandardRate, 15000m),
                ExpressRatePerKg = ReadDecimal(txtExpressRate, 22000m),
                InstantRatePerKg = ReadDecimal(txtInstantRate, 30000m),
                VatRate = ReadDecimal(txtVatRate, 8m) / 100m,
                VndPerPoint = (int)ReadDecimal(txtVndPerPoint, 100000m),
                VipPointThreshold = (int)ReadDecimal(txtVipPoints, 300m),
                EnterprisePointThreshold = (int)ReadDecimal(txtEnterprisePoints, 1000m),
                VipDiscountRate = ReadDecimal(txtVipDiscount, 5m) / 100m,
                EnterpriseDiscountRate = ReadDecimal(txtEnterpriseDiscount, 8m) / 100m
            };
            business.Save();

            AppPersonalizationSettings personalization = new AppPersonalizationSettings
            {
                ThemeName = cbTheme.SelectedItem?.ToString() == ThemeDarkText ? "Dark" : "Light",
                HighContrastStatusColors = chkHighContrastStatus.Checked,
                HighlightProblemOrders = chkHighlightProblemOrders.Checked,
                ConfirmBeforeDeliveryComplete = chkConfirmDelivered.Checked,
                OrderGridDensity = cbOrderGridDensity.SelectedItem?.ToString() == DensityCompactText ? "Compact" : "Comfortable"
            };
            personalization.Save();
            ThemeManager.CurrentTheme = personalization.ThemeName == "Dark" ? AppTheme.Dark : AppTheme.Light;

            Utilities.DependencyContainer.Initialize(AppSettings.DataDirectory);
            MessageBox.Show("Đã lưu cấu hình và nạp lại dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
        }

        private static decimal ReadDecimal(TextBox textBox, decimal fallback)
        {
            decimal value;
            return decimal.TryParse(textBox.Text.Trim(), out value) ? value : fallback;
        }

        private void BtnCancel_Click(object? sender, System.EventArgs e)
        {
            Close();
        }
    }
}
