using System.Drawing;

namespace Logistics.WinFormsUI.Styles
{
    public static class Colors
    {
        // Brand palette
        public static readonly Color Primary = Color.FromArgb(66, 133, 244);
        public static readonly Color PrimaryDark = Color.FromArgb(25, 82, 185);
        public static readonly Color Accent = Color.FromArgb(234, 67, 53);
        public static readonly Color Success = Color.FromArgb(52, 168, 83);
        public static readonly Color Warning = Color.FromArgb(251, 188, 4);
        public static readonly Color Danger = Color.FromArgb(234, 67, 53);
        public static readonly Color Info = Color.FromArgb(23, 162, 184);

        // Backgrounds
        public static readonly Color Background = Color.FromArgb(245, 247, 250);
        public static readonly Color Surface = Color.White;
        public static readonly Color SidebarBg = Color.FromArgb(30, 30, 48);
        public static readonly Color CardBg = Color.White;

        // Text
        public static readonly Color TextPrimary = Color.FromArgb(33, 37, 41);
        public static readonly Color TextSecondary = Color.FromArgb(108, 117, 125);
        public static readonly Color TextMuted = Color.FromArgb(173, 181, 189);
        public static readonly Color TextOnDark = Color.White;

        // Order status colors
        public static readonly Color StatusPending = Color.FromArgb(251, 188, 4);
        public static readonly Color StatusProcessing = Color.FromArgb(66, 133, 244);
        public static readonly Color StatusDelivering = Color.FromArgb(23, 162, 184);
        public static readonly Color StatusDelivered = Color.FromArgb(52, 168, 83);
        public static readonly Color StatusCancelled = Color.FromArgb(220, 53, 69);
    }
}
