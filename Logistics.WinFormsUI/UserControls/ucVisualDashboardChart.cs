using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.UserControls
{
    public class ucVisualDashboardChart : UserControl
    {
        private Dictionary<string, int> _data = new Dictionary<string, int>();

        public ucVisualDashboardChart()
        {
            this.DoubleBuffered = true;
            this.Paint += UcVisualDashboardChart_Paint;
        }

        public void SetData(Dictionary<string, int> data)
        {
            _data = data;
            this.Invalidate();
        }

        private void UcVisualDashboardChart_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Clear control using CardColor from theme
            g.Clear(ThemeManager.CardColor);

            if (_data == null || _data.Count == 0)
            {
                using (Font noDataFont = new Font("Segoe UI", 11F, FontStyle.Italic))
                using (SolidBrush textBrush = new SolidBrush(ThemeManager.SecondaryTextColor))
                {
                    g.DrawString("Chua co du lieu thong ke don hang.", noDataFont, textBrush, 32, 100);
                }
                return;
            }

            int paddingLeft = 60;
            int paddingRight = 30;
            int paddingTop = 40;
            int paddingBottom = 60;

            int width = this.Width - paddingLeft - paddingRight;
            int height = this.Height - paddingTop - paddingBottom;

            if (width <= 0 || height <= 0) return;

            // Find max value
            int maxValue = 1;
            foreach (int val in _data.Values)
            {
                if (val > maxValue)
                {
                    maxValue = val;
                }
            }

            // Draw Y-axis gridlines & labels
            int gridLines = 4;
            using (Pen gridPen = new Pen(ThemeManager.BorderColor, 1f))
            using (Font gridFont = new Font("Segoe UI", 9F))
            using (SolidBrush textBrush = new SolidBrush(ThemeManager.SecondaryTextColor))
            {
                gridPen.DashStyle = DashStyle.Dash;
                for (int i = 0; i <= gridLines; i++)
                {
                    int y = paddingTop + height - (i * height / gridLines);
                    int val = i * maxValue / gridLines;

                    // Draw line
                    g.DrawLine(gridPen, paddingLeft, y, paddingLeft + width, y);

                    // Draw label
                    g.DrawString(val.ToString(), gridFont, textBrush, 12, y - 7);
                }
            }

            // Colors for gradients
            Color[,] barColors = new Color[,]
            {
                { Color.FromArgb(245, 158, 11), Color.FromArgb(251, 191, 36) },  // Amber/Orange
                { Color.FromArgb(59, 130, 246), Color.FromArgb(96, 165, 250) },  // Blue
                { Color.FromArgb(16, 185, 129), Color.FromArgb(52, 211, 153) },  // Green
                { Color.FromArgb(239, 68, 68), Color.FromArgb(248, 113, 113) },   // Red
                { Color.FromArgb(139, 92, 246), Color.FromArgb(167, 139, 250) }  // Purple
            };

            // Draw Bars
            int index = 0;
            int barGap = 20;
            int totalBars = _data.Count;
            int barWidth = (width - (barGap * (totalBars + 1))) / totalBars;
            if (barWidth < 15) barWidth = 15;

            using (Font labelFont = new Font("Segoe UI", 8.5F))
            using (Font valueFont = new Font("Segoe UI", 9F, FontStyle.Bold))
            using (SolidBrush primaryTextBrush = new SolidBrush(ThemeManager.TextColor))
            using (SolidBrush secondaryTextBrush = new SolidBrush(ThemeManager.SecondaryTextColor))
            {
                foreach (KeyValuePair<string, int> item in _data)
                {
                    int x = paddingLeft + barGap + index * (barWidth + barGap);
                    int barHeight = (int)(height * ((double)item.Value / maxValue));
                    int y = paddingTop + height - barHeight;

                    // Draw Gradient Rounded Bar
                    if (barHeight > 0)
                    {
                        Rectangle rect = new Rectangle(x, y, barWidth, barHeight);
                        Color color1 = barColors[index % 5, 0];
                        Color color2 = barColors[index % 5, 1];

                        using (LinearGradientBrush brush = new LinearGradientBrush(rect, color1, color2, 90F))
                        {
                            using (GraphicsPath path = GetRoundedBarPath(rect, 6))
                            {
                                g.FillPath(brush, path);
                            }
                        }

                        // Draw value above bar
                        g.DrawString(item.Value.ToString(), valueFont, primaryTextBrush, x + (barWidth / 2) - 8, y - 20);
                    }

                    // Draw label below axis
                    RectangleF labelRect = new RectangleF(x - 8, paddingTop + height + 10, barWidth + 16, 45);
                    g.DrawString(item.Key, labelFont, secondaryTextBrush, labelRect);

                    index++;
                }
            }
        }

        private GraphicsPath GetRoundedBarPath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            if (radius <= 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            int diameter = radius * 2;
            Rectangle arc = new Rectangle(bounds.X, bounds.Y, diameter, diameter);

            path.AddArc(arc, 180, 90);

            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            path.AddLine(bounds.Right, bounds.Bottom, bounds.Left, bounds.Bottom);

            path.CloseFigure();
            return path;
        }
    }
}
