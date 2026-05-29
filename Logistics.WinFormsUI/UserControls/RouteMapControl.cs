using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.UserControls
{
    public enum RouteMapPointState
    {
        Pending,
        Current,
        Completed,
        Problem
    }

    public class RouteMapPoint
    {
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public RouteMapPointState State { get; set; }
    }

    public class RouteMapControl : Control
    {
        private readonly List<RouteMapPoint> _points = new List<RouteMapPoint>();
        private string _statusText = "Chua co du lieu tuyen";
        private string _progressText = string.Empty;

        public RouteMapControl()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            ForeColor = Color.FromArgb(33, 37, 41);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(320, 180);
        }

        public void SetRoute(IEnumerable<RouteMapPoint> points, string statusText, string progressText)
        {
            _points.Clear();
            if (points != null)
            {
                _points.AddRange(points);
            }

            _statusText = string.IsNullOrWhiteSpace(statusText) ? "Chua co du lieu tuyen" : statusText;
            _progressText = progressText ?? string.Empty;
            Invalidate();
        }

        public void ClearRoute(string message)
        {
            _points.Clear();
            _statusText = string.IsNullOrWhiteSpace(message) ? "Chua co du lieu tuyen" : message;
            _progressText = string.Empty;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle bounds = ClientRectangle;
            if (bounds.Width <= 0 || bounds.Height <= 0)
            {
                return;
            }

            using (SolidBrush background = new SolidBrush(BackColor))
            {
                e.Graphics.FillRectangle(background, bounds);
            }

            DrawHeader(e.Graphics, bounds);

            if (_points.Count == 0)
            {
                DrawEmptyState(e.Graphics, bounds);
                return;
            }

            DrawGrid(e.Graphics, bounds);
            DrawRoute(e.Graphics, bounds);
        }

        private void DrawHeader(Graphics graphics, Rectangle bounds)
        {
            using (Font titleFont = new Font(Font.FontFamily, 11F, FontStyle.Bold))
            using (SolidBrush titleBrush = new SolidBrush(Color.FromArgb(20, 33, 61)))
            using (SolidBrush metaBrush = new SolidBrush(Color.FromArgb(92, 99, 112)))
            {
                graphics.DrawString("So do tuyen duong", titleFont, titleBrush, 18, 14);
                graphics.DrawString(_statusText, Font, metaBrush, 18, 40);

                if (!string.IsNullOrWhiteSpace(_progressText))
                {
                    SizeF size = graphics.MeasureString(_progressText, Font);
                    graphics.DrawString(_progressText, Font, metaBrush, bounds.Width - size.Width - 18, 40);
                }
            }
        }

        private static void DrawGrid(Graphics graphics, Rectangle bounds)
        {
            using (Pen gridPen = new Pen(Color.FromArgb(238, 241, 245), 1))
            {
                int top = 74;
                int bottom = bounds.Bottom - 14;
                for (int y = top; y < bottom; y += 34)
                {
                    graphics.DrawLine(gridPen, 18, y, bounds.Right - 18, y);
                }
            }
        }

        private void DrawRoute(Graphics graphics, Rectangle bounds)
        {
            int count = _points.Count;
            int left = 48;
            int right = Math.Max(left + 1, bounds.Width - 48);
            int routeY = Math.Max(116, bounds.Height / 2);
            int labelTop = Math.Min(routeY + 26, bounds.Height - 76);
            int labelBottom = Math.Max(78, routeY - 62);

            PointF[] positions = new PointF[count];
            for (int i = 0; i < count; i++)
            {
                float x = count == 1 ? bounds.Width / 2f : left + ((right - left) * i / (float)(count - 1));
                positions[i] = new PointF(x, routeY);
            }

            using (Pen pendingPen = new Pen(Color.FromArgb(203, 213, 225), 4))
            using (Pen completedPen = new Pen(Color.FromArgb(46, 125, 50), 4))
            using (Pen currentPen = new Pen(Color.FromArgb(21, 101, 192), 4))
            {
                pendingPen.StartCap = LineCap.Round;
                pendingPen.EndCap = LineCap.Round;
                completedPen.StartCap = LineCap.Round;
                completedPen.EndCap = LineCap.Round;
                currentPen.StartCap = LineCap.Round;
                currentPen.EndCap = LineCap.Round;

                for (int i = 0; i < count - 1; i++)
                {
                    Pen pen = GetSegmentPen(i, pendingPen, completedPen, currentPen);
                    graphics.DrawLine(pen, positions[i], positions[i + 1]);
                }
            }

            for (int i = 0; i < count; i++)
            {
                DrawPoint(graphics, _points[i], positions[i], i % 2 == 0 ? labelTop : labelBottom, i % 2 != 0);
            }
        }

        private Pen GetSegmentPen(int index, Pen pendingPen, Pen completedPen, Pen currentPen)
        {
            RouteMapPoint current = _points[index];
            RouteMapPoint next = _points[index + 1];
            if (current.State == RouteMapPointState.Problem || next.State == RouteMapPointState.Problem)
            {
                return currentPen;
            }

            if (current.State == RouteMapPointState.Completed &&
                (next.State == RouteMapPointState.Completed || next.State == RouteMapPointState.Current))
            {
                return completedPen;
            }

            if (current.State == RouteMapPointState.Current || next.State == RouteMapPointState.Current)
            {
                return currentPen;
            }

            return pendingPen;
        }

        private void DrawPoint(Graphics graphics, RouteMapPoint point, PointF center, int labelY, bool labelAbove)
        {
            Color fill = GetPointColor(point.State);
            int radius = point.State == RouteMapPointState.Current ? 11 : 9;
            RectangleF circle = new RectangleF(center.X - radius, center.Y - radius, radius * 2, radius * 2);

            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(42, 15, 23, 42)))
            using (SolidBrush fillBrush = new SolidBrush(fill))
            using (Pen outlinePen = new Pen(Color.White, 3))
            {
                graphics.FillEllipse(shadowBrush, center.X - radius + 2, center.Y - radius + 3, radius * 2, radius * 2);
                graphics.FillEllipse(fillBrush, circle);
                graphics.DrawEllipse(outlinePen, circle);
            }

            if (point.State == RouteMapPointState.Current)
            {
                using (Pen ringPen = new Pen(Color.FromArgb(90, fill), 2))
                {
                    graphics.DrawEllipse(ringPen, center.X - radius - 6, center.Y - radius - 6, (radius + 6) * 2, (radius + 6) * 2);
                }
            }

            float connectorEnd = labelAbove ? labelY + 44 : labelY - 6;
            using (Pen connectorPen = new Pen(Color.FromArgb(203, 213, 225), 1))
            {
                graphics.DrawLine(connectorPen, center.X, center.Y + (labelAbove ? -radius : radius), center.X, connectorEnd);
            }

            DrawLabel(graphics, point, center.X, labelY);
        }

        private void DrawLabel(Graphics graphics, RouteMapPoint point, float x, int y)
        {
            int labelWidth = Math.Min(170, Math.Max(110, Width / Math.Max(3, _points.Count)));
            RectangleF titleRect = new RectangleF(x - labelWidth / 2f, y, labelWidth, 20);
            RectangleF subtitleRect = new RectangleF(x - labelWidth / 2f, y + 20, labelWidth, 42);

            using (Font labelFont = new Font(Font.FontFamily, 8.5F, FontStyle.Bold))
            using (SolidBrush titleBrush = new SolidBrush(Color.FromArgb(30, 41, 59)))
            using (SolidBrush subtitleBrush = new SolidBrush(Color.FromArgb(100, 116, 139)))
            using (StringFormat centered = new StringFormat())
            {
                centered.Alignment = StringAlignment.Center;
                centered.LineAlignment = StringAlignment.Near;
                centered.Trimming = StringTrimming.EllipsisWord;
                centered.FormatFlags = StringFormatFlags.LineLimit;

                graphics.DrawString(point.Title, labelFont, titleBrush, titleRect, centered);
                graphics.DrawString(point.Subtitle, Font, subtitleBrush, subtitleRect, centered);
            }
        }

        private static Color GetPointColor(RouteMapPointState state)
        {
            switch (state)
            {
                case RouteMapPointState.Completed:
                    return Color.FromArgb(46, 125, 50);
                case RouteMapPointState.Current:
                    return Color.FromArgb(21, 101, 192);
                case RouteMapPointState.Problem:
                    return Color.FromArgb(198, 40, 40);
                default:
                    return Color.FromArgb(148, 163, 184);
            }
        }

        private void DrawEmptyState(Graphics graphics, Rectangle bounds)
        {
            using (Font emptyFont = new Font(Font.FontFamily, 10F, FontStyle.Bold))
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(100, 116, 139)))
            using (StringFormat centered = new StringFormat())
            {
                centered.Alignment = StringAlignment.Center;
                centered.LineAlignment = StringAlignment.Center;
                graphics.DrawString(_statusText, emptyFont, brush, bounds, centered);
            }
        }
    }
}
