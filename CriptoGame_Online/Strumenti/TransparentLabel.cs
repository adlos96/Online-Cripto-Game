using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Warrior_and_Wealth.Strumenti
{
    public class TransparentLabel : Control
    {
        public TransparentLabel()
        {
            SetStyle(
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint,
                true);

            BackColor = Color.Transparent;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020;
                return cp;
            }
        }

        // --- Allineamento testo ---
        private ContentAlignment _textAlign = ContentAlignment.MiddleCenter;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ContentAlignment TextAlign
        {
            get => _textAlign;
            set { _textAlign = value; Invalidate(); }
        }

        // --- Multiline ---
        private bool _multiline = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Multiline
        {
            get => _multiline;
            set { _multiline = value; Invalidate(); }
        }

        // --- Testo ---
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get => base.Text;
            set { base.Text = value; Invalidate(); }
        }

        // --- Rendering ---
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (string.IsNullOrEmpty(Text)) return;

            var sf = new StringFormat();

            sf.Alignment = _textAlign switch
            {
                ContentAlignment.TopLeft or ContentAlignment.MiddleLeft or ContentAlignment.BottomLeft
                    => StringAlignment.Near,
                ContentAlignment.TopRight or ContentAlignment.MiddleRight or ContentAlignment.BottomRight
                    => StringAlignment.Far,
                _ => StringAlignment.Center
            };

            sf.LineAlignment = _textAlign switch
            {
                ContentAlignment.TopLeft or ContentAlignment.TopCenter or ContentAlignment.TopRight
                    => StringAlignment.Near,
                ContentAlignment.BottomLeft or ContentAlignment.BottomCenter or ContentAlignment.BottomRight
                    => StringAlignment.Far,
                _ => StringAlignment.Center
            };

            sf.FormatFlags = _multiline
                ? sf.FormatFlags & ~StringFormatFlags.NoWrap
                : sf.FormatFlags | StringFormatFlags.NoWrap;

            using var brush = new SolidBrush(ForeColor);
            e.Graphics.DrawString(Text, Font, brush, ClientRectangle, sf);
        }

        // Trasparenza: ridisegna il parent sotto
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (Parent != null)
            {
                e.Graphics.TranslateTransform(-Left, -Top);
                using var pea = new PaintEventArgs(e.Graphics, new Rectangle(Left, Top, Width, Height));
                InvokePaintBackground(Parent, pea);
                InvokePaint(Parent, pea);
                e.Graphics.TranslateTransform(Left, Top);
            }
        }
    }
}