using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Logistics.WinFormsUI
{
    /// <summary>
    /// Luu cac tuy chon hien thi va thao tac ca nhan cua nguoi dung.
    /// File nay chi dung Newtonsoft.Json theo yeu cau do an.
    /// </summary>
    public class AppPersonalizationSettings
    {
        public string ThemeName { get; set; } = "Light";
        public bool HighContrastStatusColors { get; set; } = true;
        public bool HighlightProblemOrders { get; set; } = true;
        public bool ConfirmBeforeDeliveryComplete { get; set; } = true;
        public string OrderGridDensity { get; set; } = "Comfortable";

        public static string SettingsPath
        {
            get { return Path.Combine(Application.StartupPath, "app.personalization.settings.json"); }
        }

        public static AppPersonalizationSettings Load()
        {
            if (!File.Exists(SettingsPath))
            {
                return new AppPersonalizationSettings();
            }

            try
            {
                string json = File.ReadAllText(SettingsPath, System.Text.Encoding.UTF8);
                AppPersonalizationSettings? settings = JsonConvert.DeserializeObject<AppPersonalizationSettings>(json);
                if (settings == null)
                {
                    return new AppPersonalizationSettings();
                }

                settings.Normalize();
                return settings;
            }
            catch
            {
                return new AppPersonalizationSettings();
            }
        }

        public void Save()
        {
            Normalize();
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(SettingsPath, json, System.Text.Encoding.UTF8);
        }

        private void Normalize()
        {
            if (ThemeName != "Dark")
            {
                ThemeName = "Light";
            }

            if (OrderGridDensity != "Compact")
            {
                OrderGridDensity = "Comfortable";
            }
        }
    }
}
