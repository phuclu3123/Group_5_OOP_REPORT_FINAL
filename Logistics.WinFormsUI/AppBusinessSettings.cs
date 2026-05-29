using System.IO;
using System.Windows.Forms;
using Logistics.Core.Configuration;
using Newtonsoft.Json;

namespace Logistics.WinFormsUI
{
    /// <summary>
    /// Luu cau hinh nghiep vu co the dieu chinh tu man hinh Settings.
    /// Cac gia tri nay duoc chuyen ve BusinessRules khi tinh cuoc va chinh sach khach hang.
    /// </summary>
    public class AppBusinessSettings
    {
        public decimal StandardRatePerKg { get; set; } = 15000m;
        public decimal ExpressRatePerKg { get; set; } = 22000m;
        public decimal InstantRatePerKg { get; set; } = 30000m;
        public decimal VatRate { get; set; } = 0.08m;
        public int VndPerPoint { get; set; } = 100000;
        public int VipPointThreshold { get; set; } = 300;
        public int EnterprisePointThreshold { get; set; } = 1000;
        public decimal VipDiscountRate { get; set; } = 0.05m;
        public decimal EnterpriseDiscountRate { get; set; } = 0.08m;

        public static string SettingsPath
        {
            get { return Path.Combine(Application.StartupPath, "app.business.settings.json"); }
        }

        public static AppBusinessSettings Load()
        {
            if (!File.Exists(SettingsPath))
            {
                return new AppBusinessSettings();
            }

            try
            {
                string json = File.ReadAllText(SettingsPath, System.Text.Encoding.UTF8);
                AppBusinessSettings? settings = JsonConvert.DeserializeObject<AppBusinessSettings>(json);
                return settings ?? new AppBusinessSettings();
            }
            catch
            {
                return new AppBusinessSettings();
            }
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(SettingsPath, json, System.Text.Encoding.UTF8);
        }

        public BusinessRules ToBusinessRules()
        {
            BusinessRules rules = new BusinessRules();
            rules.StandardRatePerKg = StandardRatePerKg;
            rules.ExpressRatePerKg = ExpressRatePerKg;
            rules.InstantRatePerKg = InstantRatePerKg;
            rules.VatRate = VatRate;
            rules.VndPerPoint = VndPerPoint;
            rules.VipPointThreshold = VipPointThreshold;
            rules.EnterprisePointThreshold = EnterprisePointThreshold;
            rules.VipDiscountRate = VipDiscountRate;
            rules.EnterpriseDiscountRate = EnterpriseDiscountRate;
            return rules;
        }
    }
}
