using System;
using System.IO;
using System.Windows.Forms;
using Logistics.WinFormsUI.Forms;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Giu kich thuoc WinForms on dinh tren may co scale DPI khac nhau.
            Application.SetHighDpiMode(HighDpiMode.DpiUnaware);

            AppSettings.Initialize();
            AppPersonalizationSettings personalization = AppPersonalizationSettings.Load();
            ThemeManager.CurrentTheme = personalization.ThemeName == "Dark" ? AppTheme.Dark : AppTheme.Light;

            string dataFolder = AppSettings.DataDirectory;
            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }

            // Khoi tao repository va service mot lan truoc khi mo giao dien.
            DependencyContainer.Initialize(dataFolder);

            Application.Run(new FrmLogin(DependencyContainer.GetAuthService()));
        }
    }
}
