using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Warrior_and_Wealth.Strumenti
{
    public class CustomScrollPanel : Panel
    {
        private int scrollOffset = 0;
        private bool isDragging = false;
        private int dragStartY = 0;
        private int dragStartScroll = 0;

        private const int scrollBarWidth = 8;
        private const int cornerRadius = 4;

        // Nascondiamo le proprietà problematiche al designer
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size AutoScrollMinSize { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Point AutoScrollPosition
        {
            get => new Point(0, -scrollOffset);
            set => SetScroll(-value.Y);
        }

        public CustomScrollPanel()
        {
            DoubleBuffered = true;
            AutoScroll = false;
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw, true);

            MouseWheel += OnMouseWheel;
            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
            MouseUp += OnMouseUp;
            Resize += (s, e) => UpdateScrollAndLayout();
            ControlAdded += (s, e) => UpdateScrollAndLayout();
            ControlRemoved += (s, e) => UpdateScrollAndLayout();
            Layout += (s, e) => UpdateScrollAndLayout();
        }

        private int TotalContentHeight
        {
            get
            {
                if (Controls.Count == 0) return ClientSize.Height;
                int maxBottom = 0;
                foreach (Control c in Controls)
                    maxBottom = Math.Max(maxBottom, c.Bottom);
                return Math.Max(ClientSize.Height, maxBottom + Padding.Bottom);
            }
        }

        private int MaxScroll => Math.Max(0, TotalContentHeight - ClientSize.Height);

        private void SetScroll(int value)
        {
            int oldOffset = scrollOffset;
            scrollOffset = Math.Max(0, Math.Min(value, MaxScroll));
            if (oldOffset != scrollOffset)
            {
                int delta = oldOffset - scrollOffset;
                foreach (Control c in Controls)
                    c.Top += delta;
                Invalidate();
                OnScroll(new ScrollEventArgs(ScrollEventType.ThumbTrack, oldOffset, scrollOffset, ScrollOrientation.VerticalScroll));
            }
        }

        private void UpdateScrollAndLayout()
        {
            if (scrollOffset > MaxScroll)
                SetScroll(MaxScroll);
            else
                Invalidate();
        }

        private void OnMouseWheel(object? sender, MouseEventArgs e)
        {
            SetScroll(scrollOffset - e.Delta / 3);
        }

        private void OnMouseDown(object? sender, MouseEventArgs e)
        {
            Rectangle thumb = GetThumbRect();
            if (thumb.Contains(e.Location))
            {
                isDragging = true;
                dragStartY = e.Y;
                dragStartScroll = scrollOffset;
            }
        }

        private void OnMouseMove(object? sender, MouseEventArgs e)
        {
            if (!isDragging) return;
            int trackHeight = ClientSize.Height - 8;
            float ratio = MaxScroll / (float)Math.Max(1, trackHeight - GetThumbHeight());
            int delta = e.Y - dragStartY;
            SetScroll(dragStartScroll + (int)(delta * ratio));
        }

        private void OnMouseUp(object? sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private int GetThumbHeight()
        {
            if (TotalContentHeight <= 0) return ClientSize.Height;
            float ratio = (float)ClientSize.Height / TotalContentHeight;
            return Math.Max(24, (int)(ClientSize.Height * ratio));
        }

        private Rectangle GetThumbRect()
        {
            int trackHeight = ClientSize.Height - 8;
            int thumbH = GetThumbHeight();
            float scrollRatio = MaxScroll > 0 ? (float)scrollOffset / MaxScroll : 0f;
            int thumbY = 4 + (int)(scrollRatio * (trackHeight - thumbH));
            return new Rectangle(
                ClientSize.Width - scrollBarWidth - 2,
                thumbY,
                scrollBarWidth,
                thumbH
            );
        }

        public event ScrollEventHandler? Scroll;
        protected virtual void OnScroll(ScrollEventArgs e) => Scroll?.Invoke(this, e);

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (MaxScroll <= 0) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle track = new(ClientSize.Width - scrollBarWidth - 2, 4, scrollBarWidth, ClientSize.Height - 8);
            using (SolidBrush trackBrush = new(Color.FromArgb(50, 255, 255, 255)))
                g.FillRoundedRectangle(trackBrush, track, cornerRadius);

            Rectangle thumb = GetThumbRect();
            using (LinearGradientBrush thumbBrush = new(thumb, Color.FromArgb(180, 160, 120, 50), Color.FromArgb(180, 100, 70, 20), 90f))
                g.FillRoundedRectangle(thumbBrush, thumb, cornerRadius);

            using (Pen thumbPen = new(Color.FromArgb(120, 180, 140, 60), 1f))
                g.DrawRoundedRectangle(thumbPen, thumb, cornerRadius);

            int midX = thumb.X + thumb.Width / 2;
            int midY = thumb.Y + thumb.Height / 2;
            using (Pen linePen = new(Color.FromArgb(100, 220, 180, 80), 1f))
                for (int i = -2; i <= 2; i += 2)
                    g.DrawLine(linePen, thumb.X + 2, midY + i, thumb.Right - 2, midY + i);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            UpdateScrollAndLayout();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateScrollAndLayout();
        }
    }

    // Estensioni per rettangoli arrotondati
    public static class GraphicsExtensions
    {
        public static void FillRoundedRectangle(this Graphics g, Brush brush, Rectangle rect, int radius)
        {
            using var path = GetRoundedRectanglePath(rect, radius);
            g.FillPath(brush, path);
        }
        public static void DrawRoundedRectangle(this Graphics g, Pen pen, Rectangle rect, int radius)
        {
            using var path = GetRoundedRectanglePath(rect, radius);
            g.DrawPath(pen, path);
        }
        private static GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}