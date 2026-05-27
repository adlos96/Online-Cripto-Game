using System.Drawing.Drawing2D;
using Warrior_and_Wealth.Strumenti;

public class CustomToolTip
{
    private readonly Dictionary<Control, string> _tips = new();
    private TooltipForm? _activeTip;
    private int _currentTooltipId;

    public int InitialDelay { get; set; } = 300;
    public int AutoPopDelay { get; set; } = 5000;

    public void SetToolTip(Control control, string text)
    {
        if (!_tips.ContainsKey(control))
        {
            _tips[control] = text;

            control.MouseEnter += async (s, e) => await ShowTooltipAsync(control);
            control.MouseLeave += (s, e) => HideTooltip();
            control.MouseMove += (s, e) => MoveTooltip();
        }
        else
        {
            _tips[control] = text;
        }
    }

    private async Task ShowTooltipAsync(Control ctl)
    {
        var tooltipId = ++_currentTooltipId;

        await Task.Delay(InitialDelay);

        if (tooltipId != _currentTooltipId || !ctl.ClientRectangle.Contains(ctl.PointToClient(Cursor.Position)))
            return;

        HideTooltip();

        _activeTip = new TooltipForm(_tips[ctl]);

        _activeTip.Location = new Point(-9999, -9999);
        _activeTip.Show();
        _activeTip.Location = GetSafeLocation(Cursor.Position, _activeTip);

        _ = AutoHideAsync(tooltipId);
    }

    private async Task AutoHideAsync(int tooltipId)
    {
        await Task.Delay(AutoPopDelay);

        if (tooltipId == _currentTooltipId)
            HideTooltip();
    }

    private void MoveTooltip()
    {
        if (_activeTip != null)
            _activeTip.Location = GetSafeLocation(Cursor.Position, _activeTip);
    }

    private void HideTooltip()
    {
        _currentTooltipId++;

        if (_activeTip != null)
        {
            var tipToClose = _activeTip;
            _activeTip = null; // Subito null così non blocca nuovi tooltip

            _ = tipToClose.FadeOutAndCloseAsync();
        }
    }

    private static Point GetSafeLocation(Point cursor, Form tip)
    {
        const int offset = 12;
        Screen screen = Screen.FromPoint(cursor);
        Rectangle bounds = screen.WorkingArea;

        int x = cursor.X + offset;
        int y = cursor.Y + offset;

        if (x + tip.Width > bounds.Right)
            x = cursor.X - tip.Width - offset;

        if (y + tip.Height > bounds.Bottom)
            y = cursor.Y - tip.Height - offset;

        x = Math.Max(bounds.Left, x);
        y = Math.Max(bounds.Top, y);

        return new Point(x, y);
    }

    // -------------------------------------------------------------
    // INTERNAL FORM: Medieval Tooltip con colori e icone
    // -------------------------------------------------------------
    private class TooltipForm : Form
    {
        private readonly List<GameTextBox.Segment> _segments;
        private CancellationTokenSource? _fadeCts;
        private const int iconSize = 18;
        private static readonly Font TooltipFont = new Font("Georgia", 11, FontStyle.Regular);

        public TooltipForm(string text)
        {
            this.AutoScaleMode = AutoScaleMode.None;
            _segments = LogSupport.Parse(text);

            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            DoubleBuffered = true;
            TopMost = true;
            BackColor = Color.White;

            CalculateSize();

            Opacity = 0;
            _ = FadeInAsync();
        }

        private async Task FadeInAsync()
        {
            _fadeCts?.Cancel();
            _fadeCts = new CancellationTokenSource();
            var token = _fadeCts.Token;

            for (int i = 0; i <= 10; i++)
            {
                if (IsDisposed || token.IsCancellationRequested) return;
                try { Opacity = i / 10.0; }
                catch (ObjectDisposedException) { return; }
                await Task.Delay(15);
            }
        }

        public async Task FadeOutAndCloseAsync()
        {
            _fadeCts?.Cancel();
            _fadeCts = new CancellationTokenSource();
            var token = _fadeCts.Token;

            for (int i = 10; i >= 0; i--)
            {
                if (IsDisposed || token.IsCancellationRequested) return;
                try { Opacity = i / 10.0; }
                catch (ObjectDisposedException) { return; }
                await Task.Delay(15);
            }

            if (!IsDisposed && !token.IsCancellationRequested)
            {
                try { Close(); }
                catch (ObjectDisposedException) { }
            }
        }

        private void CalculateSize()
        {
            using (Bitmap bmp = new Bitmap(1, 1))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                int maxLineWidth = 400;
                float currentX = 0;
                float totalHeight = TooltipFont.GetHeight(g);
                float maxWidth = 0;
                float lineHeight = TooltipFont.GetHeight(g);

                foreach (var seg in _segments)
                {
                    if (seg.IsIcon && seg.Icon != null)
                    {
                        float iconWidth = iconSize + 4;

                        if (currentX + iconWidth > maxLineWidth)
                        {
                            maxWidth = Math.Max(maxWidth, currentX);
                            currentX = 0;
                            totalHeight += lineHeight;
                        }

                        currentX += iconWidth;
                    }
                    else if (!string.IsNullOrEmpty(seg.Text))
                    {
                        string[] lines = seg.Text.Split('\n');

                        for (int i = 0; i < lines.Length; i++)
                        {
                            if (i > 0)
                            {
                                maxWidth = Math.Max(maxWidth, currentX);
                                currentX = 0;
                                totalHeight += lineHeight;
                            }

                            string line = lines[i];
                            string[] words = line.Split(' ');

                            foreach (var word in words)
                            {
                                if (string.IsNullOrWhiteSpace(word)) continue;

                                string drawWord = word + " ";
                                SizeF wordSize = g.MeasureString(drawWord, TooltipFont);

                                if (currentX + wordSize.Width > maxLineWidth)
                                {
                                    maxWidth = Math.Max(maxWidth, currentX);
                                    currentX = 0;
                                    totalHeight += lineHeight;
                                }

                                currentX += wordSize.Width;
                            }
                        }
                    }
                }

                maxWidth = Math.Max(maxWidth, currentX);

                Width = Math.Max(190, (int)maxWidth + 30);
                Height = Math.Max(30, (int)totalHeight + 30);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new(0, 0, Width - 1, Height - 1);

            DrawParchmentBackground(g, rect);

            // Bordo doppio: oro esterno + marrone interno
            using (Pen pOro = new(Color.FromArgb(180, 150, 60), 3f))
                g.DrawRoundedRectangle(pOro, rect, 12);

            Rectangle rectInner = new(3, 3, Width - 7, Height - 7);
            using (Pen pMarrone = new(Color.FromArgb(90, 55, 20), 1.5f))
                g.DrawRoundedRectangle(pMarrone, rectInner, 10);

            DrawCornerOrnaments(g, rect);

            // Rendering testo e icone
            float x = 15f;
            float y = 12f;
            float lineHeight = TooltipFont.GetHeight(g);
            int maxWidth = Width - 30;

            foreach (var seg in _segments)
            {
                if (seg.IsIcon && seg.Icon != null)
                {
                    float iconWidth = iconSize + 0;

                    if (x + iconWidth > maxWidth)
                    {
                        x = 15f;
                        y += lineHeight;
                    }

                    int iconY = (int)(y + (lineHeight - iconSize) / 2 + 2);
                    g.DrawImage(seg.Icon, new Rectangle((int)x, iconY, iconSize, iconSize));
                    x += iconWidth;
                }
                else if (!string.IsNullOrEmpty(seg.Text))
                {
                    string[] lines = seg.Text.Split('\n');

                    using (Brush brush = new SolidBrush(seg.Color))
                    {
                        foreach (var line in lines)
                        {
                            if (line != lines[0])
                            {
                                x = 15f;
                                y += lineHeight;
                            }

                            string[] words = line.Split(' ');
                            foreach (var word in words)
                            {
                                if (string.IsNullOrWhiteSpace(word)) continue;

                                string drawWord = word + " ";
                                SizeF wordSize = g.MeasureString(drawWord, TooltipFont);

                                if (x + wordSize.Width > maxWidth)
                                {
                                    x = 15f;
                                    y += lineHeight;
                                }

                                g.DrawString(drawWord, TooltipFont, brush, new PointF(x, y));
                                x += wordSize.Width;
                            }
                        }
                    }
                }
            }
        }

        private void DrawParchmentBackground(Graphics g, Rectangle rect)
        {
            // 1. Ombra esterna (offset rect leggermente più grande e scura)
            Rectangle shadowRect = new(rect.X + 4, rect.Y + 4, rect.Width, rect.Height);
            using (SolidBrush shadow = new(Color.FromArgb(80, 0, 0, 0)))
                g.FillRoundedRectangle(shadow, shadowRect, 12);

            // 2. Base pergamena — colore caldo, vicino al resto dell'UI
            using (LinearGradientBrush bg = new(rect,
                Color.FromArgb(235, 210, 160),   // ocra chiaro in alto
                Color.FromArgb(215, 188, 135),   // ocra più saturo in basso
                90f))
            {
                g.FillRoundedRectangle(bg, rect, 12);
            }

            // 3. Venature sottilissime
            var rng = new Random(42);
            for (int y = 8; y < rect.Height - 8; y += rng.Next(4, 9))
            {
                int alpha = rng.Next(5, 14);
                int darkness = rng.Next(0, 12);
                Color lineColor = Color.FromArgb(alpha, 80 - darkness, 48 - darkness, 15);

                using Pen vena = new(lineColor, 1f);

                int xMid = rect.Width / 2;
                int yOffset = rng.Next(-1, 2);

                g.DrawLine(vena, new Point(rect.Left + 12, y), new Point(xMid, y + yOffset));
                g.DrawLine(vena, new Point(xMid, y + yOffset), new Point(rect.Right - 12, y));
            }

            // 4. Vignettatura bordi
            using (LinearGradientBrush vigTop = new(
                new Rectangle(rect.X, rect.Y, rect.Width, 24),
                Color.FromArgb(40, 60, 30, 5),
                Color.FromArgb(0, 60, 30, 5), 90f))
            {
                g.FillRectangle(vigTop, rect.X + 12, rect.Y, rect.Width - 24, 24);
            }

            using (LinearGradientBrush vigBottom = new(
                new Rectangle(rect.X, rect.Bottom - 24, rect.Width, 24),
                Color.FromArgb(0, 60, 30, 5),
                Color.FromArgb(40, 60, 30, 5), 90f))
            {
                g.FillRectangle(vigBottom, rect.X + 12, rect.Bottom - 24, rect.Width - 24, 24);
            }

            // 5. Highlight sottile in alto (simula luce che colpisce la pergamena)
            using (LinearGradientBrush highlight = new(
                new Rectangle(rect.X, rect.Y, rect.Width, 12),
                Color.FromArgb(60, 255, 255, 255),
                Color.FromArgb(0, 255, 255, 255), 90f))
            {
                g.FillRectangle(highlight, rect.X + 12, rect.Y + 2, rect.Width - 24, 12);
            }
        }
        private void DrawCornerOrnaments(Graphics g, Rectangle rect)
        {
            int size = 12; // dimensione ornamento
            int margin = 5; // distanza dal bordo esterno

            // I 4 angoli
            Point[] corners = new Point[]
            {
        new Point(rect.Left + margin, rect.Top + margin),       // top-left
        new Point(rect.Right - margin - size, rect.Top + margin),    // top-right
        new Point(rect.Left + margin, rect.Bottom - margin - size),  // bottom-left
        new Point(rect.Right - margin - size, rect.Bottom - margin - size) // bottom-right
            };

            using Pen penOro = new(Color.FromArgb(200, 160, 60), 1.5f);
            using Pen penScuro = new(Color.FromArgb(100, 70, 20), 1f);
            using SolidBrush brushOro = new(Color.FromArgb(220, 170, 70));

            foreach (var c in corners)
            {
                int cx = c.X + size / 2;
                int cy = c.Y + size / 2;
                int r1 = size / 2;       // raggio cerchio esterno
                int r2 = size / 5;       // raggio cerchio interno

                // Cerchio esterno dorato
                g.DrawEllipse(penOro, c.X, c.Y, size, size);

                // Cerchio interno pieno
                g.FillEllipse(brushOro,
                    cx - r2, cy - r2,
                    r2 * 2, r2 * 2);

                // 4 piccoli "petali" a croce attorno al centro
                int petalLen = size / 3;
                int[] dx = { 0, 0, -1, 1 };
                int[] dy = { -1, 1, 0, 0 };

                foreach (var (ox, oy) in dx.Zip(dy))
                {
                    int px = cx + ox * (r2 + 1);
                    int py = cy + oy * (r2 + 1);
                    int ex = cx + ox * (r2 + petalLen);
                    int ey = cy + oy * (r2 + petalLen);

                    g.DrawLine(penOro, px, py, ex, ey);

                    // Pallino alla fine del petalo
                    g.FillEllipse(brushOro, ex - 2, ey - 2, 4, 4);
                }

                // Quadratino ruotato 45° al centro (rombo)
                PointF[] rombo = new PointF[]
                {
            new PointF(cx,        cy - r2),
            new PointF(cx + r2,   cy),
            new PointF(cx,        cy + r2),
            new PointF(cx - r2,   cy),
                };
                g.FillPolygon(new SolidBrush(Color.FromArgb(140, 100, 30)), rombo);
                g.DrawPolygon(penScuro, rombo);
            }
        }
    }
}

// -------------------------------------------------------------
// EXTENSION METHOD per bordi arrotondati
// -------------------------------------------------------------
public static class GraphicsExtensions
{
    public static void FillRoundedRectangle(this Graphics g, Brush brush, Rectangle bounds, int radius)
    {
        using GraphicsPath path = RoundedRect(bounds, radius);
        g.FillPath(brush, path);
    }

    public static void DrawRoundedRectangle(this Graphics g, Pen pen, Rectangle bounds, int radius)
    {
        using GraphicsPath path = RoundedRect(bounds, radius);
        g.DrawPath(pen, path);
    }

    private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
    {
        int d = radius * 2;
        GraphicsPath path = new();
        path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
        path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
        path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
        path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
        path.CloseFigure();
        return path;
    }
}