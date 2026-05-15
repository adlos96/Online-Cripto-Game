
using System.ComponentModel;

namespace Warrior_and_Wealth.Strumenti
{
    internal class Transparenttextbox
    {
        /// <summary>
        /// TextBox personalizzata con supporto alla trasparenza dello sfondo.
        /// Eredita da TextBox e sovrascrive solo il necessario.
        /// 
        /// USO:
        ///   1. Compila il progetto → appare automaticamente nella Casella degli strumenti
        ///   2. Trascinala sul form come una normale TextBox
        ///   3. Imposta BackColor = Transparent dall'inspector o da codice
        /// 
        /// NOTA: In WinForms la "trasparenza vera" non esiste — il controllo
        /// ridisegna il parent sotto di sé, simulandola perfettamente.
        /// </summary>
        public class TransparentTextBox : TextBox
        {
            // ---------------------------------------------------------------
            // COSTRUTTORE
            // ---------------------------------------------------------------
            public TransparentTextBox()
            {
                // Abilita il supporto al BackColor trasparente
                SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                BackColor = Color.Transparent;

                // Bordo piatto di default (più adatto a sfondi custom)
                BorderStyle = BorderStyle.None;
            }

            // ---------------------------------------------------------------
            // TRASPARENZA: forza il ridisegno del parent prima del controllo
            // ---------------------------------------------------------------
            protected override CreateParams CreateParams
            {
                get
                {
                    // WS_EX_TRANSPARENT (0x20): il controllo non oscura
                    // quello che c'è sotto — il parent viene ridisegnato prima
                    CreateParams cp = base.CreateParams;
                    cp.ExStyle |= 0x00000020;
                    return cp;
                }
            }

            // ---------------------------------------------------------------
            // SFONDO: dipinge il parent al posto di un colore solido
            // ---------------------------------------------------------------
            protected override void OnPaintBackground(PaintEventArgs e)
            {
                // Se BackColor NON è Transparent, comportamento standard
                if (BackColor != Color.Transparent)
                {
                    base.OnPaintBackground(e);
                    return;
                }

                // Ridisegna il parent nella zona occupata da questo controllo,
                // creando l'effetto di trasparenza
                if (Parent != null)
                {
                    // Calcola l'offset del controllo rispetto al parent
                    Rectangle rect = new Rectangle(Left, Top, Width, Height);
                    e.Graphics.TranslateTransform(-Left, -Top);
                    using (PaintEventArgs pea = new PaintEventArgs(e.Graphics, rect))
                    {
                        InvokePaintBackground(Parent, pea);
                        InvokePaint(Parent, pea);
                    }
                    e.Graphics.TranslateTransform(Left, Top);
                }
            }

            // ---------------------------------------------------------------
            // OPZIONALE: colore placeholder personalizzato
            // ---------------------------------------------------------------
            private string _placeholderText = "";
            private Color _placeholderColor = Color.Gray;

            /// <summary>Testo suggerito mostrato quando la TextBox è vuota.</summary>
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            public string PlaceholderText
            {
                get => _placeholderText;
                set { _placeholderText = value; Invalidate(); }
            }

            /// <summary>Colore del testo placeholder.</summary>
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            public Color PlaceholderColor
            {
                get => _placeholderColor;
                set { _placeholderColor = value; Invalidate(); }
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                // Disegna il placeholder solo se la box è vuota e non ha il focus
                if (!string.IsNullOrEmpty(_placeholderText) && string.IsNullOrEmpty(Text) && !Focused)
                {
                    using var brush = new SolidBrush(_placeholderColor);
                    var sf = new StringFormat
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Center
                    };
                    e.Graphics.DrawString(_placeholderText, Font, brush, ClientRectangle, sf);
                }
            }

            // ---------------------------------------------------------------
            // FIX: forza il refresh quando il controllo guadagna/perde focus
            // (evita artefatti grafici)
            // ---------------------------------------------------------------
            protected override void OnGotFocus(EventArgs e)
            {
                base.OnGotFocus(e);
                Invalidate();
            }

            protected override void OnLostFocus(EventArgs e)
            {
                base.OnLostFocus(e);
                Invalidate();
            }

            // ---------------------------------------------------------------
            // FIX: ridisegna quando il testo cambia (aggiorna placeholder)
            // ---------------------------------------------------------------
            protected override void OnTextChanged(EventArgs e)
            {
                base.OnTextChanged(e);
                Invalidate();
            }
        }

    }
}
