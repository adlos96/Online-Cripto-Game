namespace CriptoGame_Online.Strumenti
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    public class GameTextBox : Panel
    {
        private class Segment
        {
            public string Text { get; set; }
            public Color Color { get; set; }
        }

        private class Line
        {
            public List<Segment> Segments { get; set; } = new();
            public Image Img { get; set; }
        }

        private readonly List<Line> lines = new();
        private int lineHeight;

        public GameTextBox()
        {
            DoubleBuffered = true; // evita flicker
            AutoScroll = true;
            BackColor = Color.FromArgb(32, 26, 14);
            ForeColor = Color.White;
            Font = new Font("Consolas", 8, FontStyle.Bold);
            lineHeight = (int)Font.GetHeight() + 5; //Distanza tra le linee, Asse Y
        }

        /// <summary>
        /// Aggiunge una riga con segmenti di testo multicolore
        /// </summary>
        public void AddLine(IEnumerable<(string text, Color color)> segments, Image img = null)
        {
            var line = new Line();
            foreach (var seg in segments)
            {
                line.Segments.Add(new Segment { Text = seg.text, Color = seg.color });
            }
            line.Img = img;

            lines.Add(line);

            // aggiorna lo scroll massimo
            AutoScrollMinSize = new Size(0, lines.Count * lineHeight);

            // scorre verso il basso
            VerticalScroll.Value = VerticalScroll.Maximum;

            Invalidate();
        }

        /// <summary>
        /// Comodità: aggiunge una riga con un solo colore
        /// </summary>
        public void AddLine(string text, Color color, Image img = null)
        {
            AddLine(new[] { (text, color) }, img);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);

            int y = 0;
            foreach (var line in lines)
            {
                // trovo il numero massimo di sottorighe tra i segmenti
                int maxParts = 1;
                var partsList = new List<string[]>();
                foreach (var seg in line.Segments)
                {
                    var parts = seg.Text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                    partsList.Add(parts);
                    if (parts.Length > maxParts) maxParts = parts.Length;
                }

                // disegno tutte le sottorighe
                for (int sub = 0; sub < maxParts; sub++)
                {
                    float x = 5f;
                    for (int s = 0; s < line.Segments.Count; s++)
                    {
                        var seg = line.Segments[s];
                        string part = partsList[s].Length > sub ? partsList[s][sub] : "";
                        if (!string.IsNullOrEmpty(part))
                        {
                            using var brush = new SolidBrush(seg.Color);
                            e.Graphics.DrawString(part, Font, brush, new PointF(x, y));
                            x += e.Graphics.MeasureString(part, Font).Width;
                        }
                    }
                    y += lineHeight; // vai a capo per ogni sottoriga
                }

                // disegno immagine accanto all’ultima sottoriga
                if (line.Img != null)
                {
                    int iconSize = 18;
                    int iconY = y - lineHeight + (lineHeight - iconSize) / 2 - 3;
                    float blockWidth = 5f;
                    foreach (var seg in line.Segments)
                    {
                        var textWidth = e.Graphics.MeasureString(seg.Text, Font).Width;
                        blockWidth += textWidth;
                    }
                    e.Graphics.DrawImage(line.Img, new Rectangle((int)blockWidth + 2, iconY, iconSize, iconSize));
                }
            }
        }
    }

}
