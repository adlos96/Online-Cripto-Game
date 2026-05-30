using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Warrior_and_Wealth.Strumenti
{
    public class CustomScrollPanel : Panel
    {
        private int scrollOffset = 0;      // coordinata originale del bordo superiore visibile
        private int scrollOffsetX = 0;     // coordinata originale del bordo sinistro visibile

        private int minScrollV, maxScrollV;
        private int minScrollH, maxScrollH;

        private StyledScrollBarV vScrollBar;
        private StyledScrollBarH hScrollBar;

        private const int scrollBarSize = 8;
        private const int cornerRadius = 4;
        private const int scrollBarMargin = 2;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size AutoScrollMinSize { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Point AutoScrollPosition
        {
            get => new Point(-scrollOffsetX, -scrollOffset);
            set { SetScrollV(-value.Y); SetScrollH(-value.X); }
        }

        public CustomScrollPanel()
        {
            DoubleBuffered = true;
            AutoScroll = false;
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw, true);

            vScrollBar = new StyledScrollBarV();
            hScrollBar = new StyledScrollBarH();
            Controls.Add(vScrollBar);
            Controls.Add(hScrollBar);

            vScrollBar.Scroll += (s, e) =>
            {
                int newValue = (int)(minScrollV + e.NewValue);
                SetScrollV(newValue);
            };
            hScrollBar.Scroll += (s, e) =>
            {
                int newValue = (int)(minScrollH + e.NewValue);
                SetScrollH(newValue);
            };

            Resize += (s, e) => UpdateScrollAndLayout();
            ControlAdded += (s, e) => UpdateScrollAndLayout();
            ControlRemoved += (s, e) => UpdateScrollAndLayout();
            Layout += (s, e) => UpdateScrollAndLayout();
        }

        // Area realmente disponibile per i controlli, al netto delle barre
        private Rectangle ContentArea
        {
            get
            {
                int w = ClientSize.Width;
                int h = ClientSize.Height;
                if (vScrollBar?.Visible == true) w -= (scrollBarSize + scrollBarMargin * 2);
                if (hScrollBar?.Visible == true) h -= (scrollBarSize + scrollBarMargin * 2);
                return new Rectangle(0, 0, Math.Max(1, w), Math.Max(1, h));
            }
        }

        // Calcola i limiti originali (al netto dello scroll) di tutti i controlli (escluse le barre)
        private (int minTop, int maxBottom) GetVerticalBounds()
        {
            int minTop = int.MaxValue;
            int maxBottom = int.MinValue;
            foreach (Control c in Controls)
            {
                if (c == vScrollBar || c == hScrollBar) continue;
                int originalTop = c.Top + scrollOffset;
                int originalBottom = c.Bottom + scrollOffset;
                minTop = Math.Min(minTop, originalTop);
                maxBottom = Math.Max(maxBottom, originalBottom);
            }
            if (minTop == int.MaxValue) minTop = 0;
            if (maxBottom == int.MinValue) maxBottom = 0;
            return (minTop, maxBottom);
        }

        private (int minLeft, int maxRight) GetHorizontalBounds()
        {
            int minLeft = int.MaxValue;
            int maxRight = int.MinValue;
            foreach (Control c in Controls)
            {
                if (c == vScrollBar || c == hScrollBar) continue;
                int originalLeft = c.Left + scrollOffsetX;
                int originalRight = c.Right + scrollOffsetX;
                minLeft = Math.Min(minLeft, originalLeft);
                maxRight = Math.Max(maxRight, originalRight);
            }
            if (minLeft == int.MaxValue) minLeft = 0;
            if (maxRight == int.MinValue) maxRight = 0;
            return (minLeft, maxRight);
        }

        // Aggiorna i range di scroll e la visibilità delle barre
        private void UpdateScrollAndLayout()
        {
            if (vScrollBar == null || hScrollBar == null) return;

            // 1. Calcola i limiti del contenuto (coordinate originali)
            var (minTopOrg, maxBottomOrg) = GetVerticalBounds();
            var (minLeftOrg, maxRightOrg) = GetHorizontalBounds();

            // Applica il padding: lo spazio aggiuntivo viene aggiunto ai bordi
            minTopOrg = Math.Min(minTopOrg, 0) - Padding.Top;
            maxBottomOrg = Math.Max(maxBottomOrg, ContentArea.Height) + Padding.Bottom;
            minLeftOrg = Math.Min(minLeftOrg, 0) - Padding.Left;
            maxRightOrg = Math.Max(maxRightOrg, ContentArea.Width) + Padding.Right;

            // 2. Determina i limiti effettivi di scroll (coordinate originali)
            minScrollV = minTopOrg;
            maxScrollV = maxBottomOrg - ContentArea.Height;
            minScrollH = minLeftOrg;
            maxScrollH = maxRightOrg - ContentArea.Width;

            // Se il contenuto è più piccolo dell'area, i limiti si equivalgono
            if (maxScrollV < minScrollV) maxScrollV = minScrollV;
            if (maxScrollH < minScrollH) maxScrollH = minScrollH;

            // 3. Stabilisce se servono le barre
            bool needV = maxScrollV > minScrollV;
            bool needH = maxScrollH > minScrollH;

            // 4. Prima abilita/disabilita le barre (influenza ContentArea)
            vScrollBar.Visible = needV;
            hScrollBar.Visible = needH;

            // 5. Ricalcola i limiti perché ContentArea potrebbe essere cambiata (le barre occupano spazio)
            Rectangle content = ContentArea;
            // Ricalcola i limiti con la nuova area visibile
            minTopOrg = Math.Min(minTopOrg, 0) - Padding.Top;
            maxBottomOrg = Math.Max(maxBottomOrg, content.Height) + Padding.Bottom;
            minLeftOrg = Math.Min(minLeftOrg, 0) - Padding.Left;
            maxRightOrg = Math.Max(maxRightOrg, content.Width) + Padding.Right;

            minScrollV = minTopOrg;
            maxScrollV = maxBottomOrg - content.Height;
            minScrollH = minLeftOrg;
            maxScrollH = maxRightOrg - content.Width;

            if (maxScrollV < minScrollV) maxScrollV = minScrollV;
            if (maxScrollH < minScrollH) maxScrollH = minScrollH;

            needV = maxScrollV > minScrollV;
            needH = maxScrollH > minScrollH;
            vScrollBar.Visible = needV;
            hScrollBar.Visible = needH;

            // 6. Posiziona le barre
            content = ContentArea;
            if (vScrollBar.Visible)
            {
                int barRight = ClientSize.Width - scrollBarMargin;
                vScrollBar.Bounds = new Rectangle(
                    barRight - scrollBarSize,
                    scrollBarMargin,
                    scrollBarSize,
                    content.Height);
            }
            if (hScrollBar.Visible)
            {
                int barBottom = ClientSize.Height - scrollBarMargin;
                hScrollBar.Bounds = new Rectangle(
                    scrollBarMargin,
                    barBottom - scrollBarSize,
                    content.Width,
                    scrollBarSize);
            }

            // 7. Configura le barre (ma gli intervalli sono mappati su 0..Maximum)
            if (vScrollBar.Visible)
            {
                vScrollBar.Maximum = Math.Max(0, maxScrollV - minScrollV);
                vScrollBar.LargeChange = content.Height;
                vScrollBar.SetValue(scrollOffset - minScrollV);
            }
            if (hScrollBar.Visible)
            {
                hScrollBar.Maximum = Math.Max(0, maxScrollH - minScrollH);
                hScrollBar.LargeChange = content.Width;
                hScrollBar.SetValue(scrollOffsetX - minScrollH);
            }

            // Porta le barre in primo piano
            if (vScrollBar.Visible) vScrollBar.BringToFront();
            if (hScrollBar.Visible) hScrollBar.BringToFront();

            // Assicura che lo scroll corrente sia nei limiti
            int oldScrollV = scrollOffset;
            int oldScrollH = scrollOffsetX;
            scrollOffset = Math.Max(minScrollV, Math.Min(scrollOffset, maxScrollV));
            scrollOffsetX = Math.Max(minScrollH, Math.Min(scrollOffsetX, maxScrollH));

            if (oldScrollV != scrollOffset || oldScrollH != scrollOffsetX)
            {
                // Riapplica la posizione corretta
                if (oldScrollV != scrollOffset)
                {
                    int deltaV = oldScrollV - scrollOffset;
                    foreach (Control c in Controls)
                    {
                        if (c == vScrollBar || c == hScrollBar) continue;
                        c.Top += deltaV;
                    }
                    vScrollBar?.SetValue(scrollOffset - minScrollV);
                }
                if (oldScrollH != scrollOffsetX)
                {
                    int deltaH = oldScrollH - scrollOffsetX;
                    foreach (Control c in Controls)
                    {
                        if (c == vScrollBar || c == hScrollBar) continue;
                        c.Left += deltaH;
                    }
                    hScrollBar?.SetValue(scrollOffsetX - minScrollH);
                }
                Invalidate();
            }

            Invalidate();
        }

        private void SetScrollV(int value)
        {
            int newValue = Math.Max(minScrollV, Math.Min(value, maxScrollV));
            if (newValue == scrollOffset) return;

            int old = scrollOffset;
            scrollOffset = newValue;
            int delta = old - scrollOffset; // muovi i controlli verso l'alto se delta>0
            foreach (Control c in Controls)
            {
                if (c == vScrollBar || c == hScrollBar) continue;
                var anchor = c.Anchor;
                c.Anchor = AnchorStyles.None;
                c.Top += delta;
                c.Anchor = anchor;
            }
            if (vScrollBar.Visible)
                vScrollBar.SetValue(scrollOffset - minScrollV);
            Invalidate();
            OnScroll(new ScrollEventArgs(ScrollEventType.ThumbTrack, old, scrollOffset, ScrollOrientation.VerticalScroll));
        }

        private void SetScrollH(int value)
        {
            int newValue = Math.Max(minScrollH, Math.Min(value, maxScrollH));
            if (newValue == scrollOffsetX) return;

            int old = scrollOffsetX;
            scrollOffsetX = newValue;
            int delta = old - scrollOffsetX;
            foreach (Control c in Controls)
            {
                if (c == vScrollBar || c == hScrollBar) continue;
                var anchor = c.Anchor;
                c.Anchor = AnchorStyles.None;
                c.Left += delta;
                c.Anchor = anchor;
            }
            if (hScrollBar.Visible)
                hScrollBar.SetValue(scrollOffsetX - minScrollH);
            Invalidate();
            OnScroll(new ScrollEventArgs(ScrollEventType.ThumbTrack, old, scrollOffsetX, ScrollOrientation.HorizontalScroll));
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (ModifierKeys == Keys.Shift || (!vScrollBar.Visible && hScrollBar.Visible))
                SetScrollH(scrollOffsetX - e.Delta / 3);
            else
                SetScrollV(scrollOffset - e.Delta / 3);
        }

        public event ScrollEventHandler? Scroll;
        protected virtual void OnScroll(ScrollEventArgs e) => Scroll?.Invoke(this, e);

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

    // ---------- Scrollbar verticale personalizzata ----------
    internal class StyledScrollBarV : Control
    {
        private const int cornerRadius = 4;
        private int thumbHeight = 24;
        private float value = 0;
        private int maximum = 0;
        private int largeChange = 100;
        private bool isDragging = false;
        private int dragStartY;
        private float dragStartValue;

        public event ScrollEventHandler? Scroll;

        public StyledScrollBarV()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
            BackColor = Color.Gray;

            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
            MouseUp += OnMouseUp;
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Maximum
        {
            get => maximum;
            set { maximum = value; RecalcThumb(); Invalidate(); }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int LargeChange
        {
            get => largeChange;
            set { largeChange = value; RecalcThumb(); Invalidate(); }
        }

        private float Value
        {
            get => value;
            set
            {
                float newVal = Math.Max(0, Math.Min(value, maximum));
                if (Math.Abs(newVal - this.value) > 0.001f)
                {
                    this.value = newVal;
                    Invalidate();
                    Scroll?.Invoke(this, new ScrollEventArgs(ScrollEventType.ThumbTrack, (int)newVal, (int)newVal, ScrollOrientation.VerticalScroll));
                }
            }
        }

        public void SetValue(int val) => Value = val;

        private void RecalcThumb()
        {
            if (maximum <= 0 || Height <= 0)
            {
                thumbHeight = 24;
                return;
            }
            float ratio = (float)largeChange / (maximum + largeChange);
            thumbHeight = Math.Max(24, (int)(Height * ratio));
        }

        private Rectangle ThumbRect
        {
            get
            {
                if (maximum <= 0) return new Rectangle(0, 0, Width, thumbHeight);
                float range = Height - thumbHeight;
                float ratio = value / maximum;
                int y = (int)(ratio * range);
                return new Rectangle(0, y, Width, thumbHeight);
            }
        }

        private void OnMouseDown(object? sender, MouseEventArgs e)
        {
            if (ThumbRect.Contains(e.Location))
            {
                isDragging = true;
                dragStartY = e.Y;
                dragStartValue = value;
            }
            else
            {
                if (e.Y < ThumbRect.Y) Value -= largeChange;
                else if (e.Y > ThumbRect.Bottom) Value += largeChange;
            }
        }

        private void OnMouseMove(object? sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int deltaY = e.Y - dragStartY;
                float range = Height - thumbHeight;
                if (range > 0)
                {
                    float ratio = deltaY / range;
                    Value = dragStartValue + ratio * maximum;
                }
            }
        }

        private void OnMouseUp(object? sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle track = new(0, 0, Width - 1, Height - 1);
            using (SolidBrush b = new(Color.FromArgb(50, 255, 255, 255)))
                e.Graphics.FillRoundedRectangle(b, track, cornerRadius);

            Rectangle thumb = ThumbRect;
            using (LinearGradientBrush b = new(thumb, Color.FromArgb(180, 160, 120, 50), Color.FromArgb(180, 100, 70, 20), 90f))
                e.Graphics.FillRoundedRectangle(b, thumb, cornerRadius);
            using (Pen p = new(Color.FromArgb(120, 180, 140, 60), 1f))
                e.Graphics.DrawRoundedRectangle(p, thumb, cornerRadius);

            int midY = thumb.Y + thumb.Height / 2;
            using (Pen p = new(Color.FromArgb(100, 220, 180, 80), 1f))
                for (int i = -2; i <= 2; i += 2)
                    e.Graphics.DrawLine(p, thumb.X + 2, midY + i, thumb.Right - 2, midY + i);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RecalcThumb();
        }
    }

    // ---------- Scrollbar orizzontale personalizzata ----------
    internal class StyledScrollBarH : Control
    {
        private const int cornerRadius = 4;
        private int thumbWidth = 24;
        private float value = 0;
        private int maximum = 0;
        private int largeChange = 100;
        private bool isDragging = false;
        private int dragStartX;
        private float dragStartValue;

        public event ScrollEventHandler? Scroll;

        public StyledScrollBarH()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
            BackColor = Color.Gray;

            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
            MouseUp += OnMouseUp;
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Maximum
        {
            get => maximum;
            set { maximum = value; RecalcThumb(); Invalidate(); }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int LargeChange
        {
            get => largeChange;
            set { largeChange = value; RecalcThumb(); Invalidate(); }
        }

        private float Value
        {
            get => value;
            set
            {
                float newVal = Math.Max(0, Math.Min(value, maximum));
                if (Math.Abs(newVal - this.value) > 0.001f)
                {
                    this.value = newVal;
                    Invalidate();
                    Scroll?.Invoke(this, new ScrollEventArgs(ScrollEventType.ThumbTrack, (int)newVal, (int)newVal, ScrollOrientation.HorizontalScroll));
                }
            }
        }

        public void SetValue(int val) => Value = val;

        private void RecalcThumb()
        {
            if (maximum <= 0 || Width <= 0)
            {
                thumbWidth = 24;
                return;
            }
            float ratio = (float)largeChange / (maximum + largeChange);
            thumbWidth = Math.Max(24, (int)(Width * ratio));
        }

        private Rectangle ThumbRect
        {
            get
            {
                if (maximum <= 0) return new Rectangle(0, 0, thumbWidth, Height);
                float range = Width - thumbWidth;
                float ratio = value / maximum;
                int x = (int)(ratio * range);
                return new Rectangle(x, 0, thumbWidth, Height);
            }
        }

        private void OnMouseDown(object? sender, MouseEventArgs e)
        {
            if (ThumbRect.Contains(e.Location))
            {
                isDragging = true;
                dragStartX = e.X;
                dragStartValue = value;
            }
            else
            {
                if (e.X < ThumbRect.X) Value -= largeChange;
                else if (e.X > ThumbRect.Right) Value += largeChange;
            }
        }

        private void OnMouseMove(object? sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int deltaX = e.X - dragStartX;
                float range = Width - thumbWidth;
                if (range > 0)
                {
                    float ratio = deltaX / range;
                    Value = dragStartValue + ratio * maximum;
                }
            }
        }

        private void OnMouseUp(object? sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle track = new(0, 0, Width - 1, Height - 1);
            using (SolidBrush b = new(Color.FromArgb(50, 255, 255, 255)))
                e.Graphics.FillRoundedRectangle(b, track, cornerRadius);

            Rectangle thumb = ThumbRect;
            using (LinearGradientBrush b = new(thumb, Color.FromArgb(180, 160, 120, 50), Color.FromArgb(180, 100, 70, 20), 0f))
                e.Graphics.FillRoundedRectangle(b, thumb, cornerRadius);
            using (Pen p = new(Color.FromArgb(120, 180, 140, 60), 1f))
                e.Graphics.DrawRoundedRectangle(p, thumb, cornerRadius);

            int midX = thumb.X + thumb.Width / 2;
            using (Pen p = new(Color.FromArgb(100, 220, 180, 80), 1f))
                for (int i = -2; i <= 2; i += 2)
                    e.Graphics.DrawLine(p, midX + i, thumb.Y + 2, midX + i, thumb.Bottom - 2);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RecalcThumb();
        }
    }

    // ---------- Estensioni per il disegno arrotondato ----------
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