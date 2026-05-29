namespace Warrior_and_Wealth.Strumenti
{
    using System.Drawing.Drawing2D;

    public class GameTextBox : Panel
    {
        public class Segment
        {
            public string Text { get; set; }
            public Color Color { get; set; }
            public Image? Icon { get; set; }
            public bool IsIcon { get; set; }
        }

        private class Line
        {
            public List<Segment> Segments { get; set; } = new();
            public int RenderedHeight { get; set; }
        }

        private readonly List<Line> lines = new();
        private int lineHeight;
        private int totalContentHeight = 0;
        private const int iconSize = 17;
        private const int scrollBarWidth = 8;
        private const int fadeHeight = 28;

        // Scrollbar custom
        private bool _isDragging = false;
        private int _dragStartY = 0;
        private int _dragStartScroll = 0;
        private int _scrollOffset = 0;

        public GameTextBox()
        {
            DoubleBuffered = true;
            AutoScroll = false; // Gestiamo noi lo scroll
            BackColor = Color.FromArgb(32, 26, 14);
            ForeColor = Color.White;
            Font = new Font("Consolas", 8.5f, FontStyle.Bold);
            lineHeight = (int)Font.GetHeight() + 5;

            // Intercetta la rotella del mouse
            MouseWheel += OnMouseWheel;
            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
            MouseUp += OnMouseUp;
        }

        // -------------------------------------------------------
        // SCROLL
        // -------------------------------------------------------

        private int MaxScroll => Math.Max(0, totalContentHeight - ClientSize.Height);

        private void SetScroll(int value)
        {
            _scrollOffset = Math.Max(0, Math.Min(value, MaxScroll));
            Invalidate();
        }

        private void ScrollToBottom() => SetScroll(MaxScroll);

        private void OnMouseWheel(object? s, MouseEventArgs e)
        {
            SetScroll(_scrollOffset - e.Delta / 3);
        }

        private void OnMouseDown(object? s, MouseEventArgs e)
        {
            Rectangle thumb = GetThumbRect();
            if (thumb.Contains(e.Location))
            {
                _isDragging = true;
                _dragStartY = e.Y;
                _dragStartScroll = _scrollOffset;
            }
        }

        private void OnMouseMove(object? s, MouseEventArgs e)
        {
            if (!_isDragging) return;

            int trackHeight = ClientSize.Height - 8; // 4px margine top + bottom
            float ratio = MaxScroll / (float)Math.Max(1, trackHeight - GetThumbHeight());
            int delta = e.Y - _dragStartY;
            SetScroll(_dragStartScroll + (int)(delta * ratio));
        }

        private void OnMouseUp(object? s, MouseEventArgs e)
        {
            _isDragging = false;
        }

        private int GetThumbHeight()
        {
            if (totalContentHeight <= 0) return ClientSize.Height;
            float ratio = (float)ClientSize.Height / totalContentHeight;
            return Math.Max(24, (int)(ClientSize.Height * ratio));
        }

        private Rectangle GetThumbRect()
        {
            int trackHeight = ClientSize.Height - 8;
            int thumbH = GetThumbHeight();
            float scrollRatio = MaxScroll > 0 ? (float)_scrollOffset / MaxScroll : 0f;
            int thumbY = 4 + (int)(scrollRatio * (trackHeight - thumbH));

            return new Rectangle(
                ClientSize.Width - scrollBarWidth - 2,
                thumbY,
                scrollBarWidth,
                thumbH
            );
        }

        // -------------------------------------------------------
        // AGGIUNTA RIGHE
        // -------------------------------------------------------

        public void AddLine(List<Segment> segments)
        {
            var line = new Line();
            line.Segments.AddRange(segments);
            lines.Add(line);

            CalculateLineHeight(line);
            totalContentHeight += line.RenderedHeight;

            ScrollToBottom();
        }

        public void AddLineFromServer(string serverMessage)
        {
            var segments = LogSupport.Parse(serverMessage);
            AddLine(segments);
        }

        public void AddLine(string text, Color color)
        {
            var segments = new List<Segment>
            {
                new Segment { Text = text, Color = color, IsIcon = false }
            };
            AddLine(segments);
        }

        private void CalculateLineHeight(Line line)
        {
            int maxWidth = ClientSize.Width - scrollBarWidth - 20;
            float x = 5f;
            int wrappedLines = 1;

            using (var g = CreateGraphics())
            {
                foreach (var seg in line.Segments)
                {
                    if (seg.IsIcon && seg.Icon != null)
                    {
                        float iconWidth = iconSize + 4;
                        if (x + iconWidth > maxWidth) { wrappedLines++; x = 5f; }
                        x += iconWidth;
                    }
                    else if (!string.IsNullOrEmpty(seg.Text))
                    {
                        string[] words = seg.Text.Split(' ');
                        foreach (var word in words)
                        {
                            if (string.IsNullOrWhiteSpace(word)) continue;
                            string drawWord = word + " ";
                            float wordWidth = g.MeasureString(drawWord, Font).Width;
                            if (x + wordWidth > maxWidth) { wrappedLines++; x = 5f; }
                            x += wordWidth;
                        }
                    }
                }
            }

            line.RenderedHeight = wrappedLines * lineHeight;
        }

        // -------------------------------------------------------
        // PAINT
        // -------------------------------------------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // --- TESTO ---
            int y = -_scrollOffset + 4;
            int maxWidth = ClientSize.Width - scrollBarWidth - 20;

            foreach (var line in lines)
            {
                // Skip righe fuori dal viewport
                if (y + line.RenderedHeight < 0) { y += line.RenderedHeight; continue; }
                if (y > ClientSize.Height) break;

                float x = 8f;

                foreach (var seg in line.Segments)
                {
                    if (seg.IsIcon && seg.Icon != null)
                    {
                        if (x + iconSize > maxWidth) { y += lineHeight; x = 8f; }
                        int iconY = y + (lineHeight - iconSize) / 2 - 2;
                        g.DrawImage(seg.Icon, new Rectangle((int)x, iconY, iconSize, iconSize));
                        x += iconSize;
                    }
                    else if (!string.IsNullOrEmpty(seg.Text))
                    {
                        string[] words = seg.Text.Split(' ');
                        using var brush = new SolidBrush(seg.Color);

                        foreach (var word in words)
                        {
                            if (string.IsNullOrWhiteSpace(word)) continue;
                            string drawWord = word + " ";
                            float wordWidth = g.MeasureString(drawWord, Font).Width;
                            if (x + wordWidth > maxWidth) { y += lineHeight; x = 8f; }
                            g.DrawString(drawWord, Font, brush, new PointF(x, y));
                            x += wordWidth;
                        }
                    }
                }

                y += lineHeight;
            }

            // --- FADE TOP ---
            Rectangle fadeTop = new(0, 0, ClientSize.Width - scrollBarWidth - 4, fadeHeight);
            using (LinearGradientBrush fadeT = new(
                fadeTop,
                Color.FromArgb(255, BackColor),
                Color.FromArgb(0, BackColor),
                90f))
            {
                g.FillRectangle(fadeT, fadeTop);
            }

            // --- FADE BOTTOM ---
            //Rectangle fadeBot = new(0, ClientSize.Height - fadeHeight, ClientSize.Width - scrollBarWidth - 4, fadeHeight);
            //using (LinearGradientBrush fadeBrush = new(
            //    fadeBot,
            //    Color.FromArgb(0, BackColor),
            //    Color.FromArgb(255, BackColor),
            //    90f))
            //{
            //    g.FillRectangle(fadeBrush, fadeBot);
            //}

            // --- SCROLLBAR TRACK ---
            Rectangle track = new(
                ClientSize.Width - scrollBarWidth - 2,
                4,
                scrollBarWidth,
                ClientSize.Height - 8);

            using (SolidBrush trackBrush = new(Color.FromArgb(50, 255, 255, 255)))
                g.FillRoundedRectangle(trackBrush, track, 4);

            // --- SCROLLBAR THUMB ---
            if (MaxScroll > 0)
            {
                Rectangle thumb = GetThumbRect();

                using (LinearGradientBrush thumbBrush = new(
                    thumb,
                    Color.FromArgb(180, 160, 120, 50),
                    Color.FromArgb(180, 100, 70, 20),
                    90f))
                {
                    g.FillRoundedRectangle(thumbBrush, thumb, 4);
                }

                // Bordo thumb
                using (Pen thumbPen = new(Color.FromArgb(120, 180, 140, 60), 1f))
                    g.DrawRoundedRectangle(thumbPen, thumb, 4);

                // Righette centrali decorative
                int midX = thumb.X + thumb.Width / 2;
                int midY = thumb.Y + thumb.Height / 2;
                using (Pen linePen = new(Color.FromArgb(100, 220, 180, 80), 1f))
                {
                    for (int i = -2; i <= 2; i += 2)
                    {
                        g.DrawLine(linePen,
                            thumb.X + 2, midY + i,
                            thumb.Right - 2, midY + i);
                    }
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            totalContentHeight = 0;
            foreach (var line in lines)
            {
                CalculateLineHeight(line);
                totalContentHeight += line.RenderedHeight;
            }
            SetScroll(_scrollOffset);
            Invalidate();
        }

        public void Clear()
        {
            lines.Clear();
            totalContentHeight = 0;
            _scrollOffset = 0;
            Invalidate();
        }
    }
}