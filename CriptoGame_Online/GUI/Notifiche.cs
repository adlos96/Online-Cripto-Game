
using Strategico_V2;

namespace Warrior_and_Wealth.GUI
{
    public partial class Notifiche : Form
    {
        private int clickedRow = -1;
        public Notifiche()
        {
            InitializeComponent();
        }
        private void AggiungiRiga(string tipo, string destinazione, string data, string dettagli, bool mostraBottone = true)
        {
            int rowIndex = dataGridView1.Rows.Add(tipo, destinazione, data, dettagli);

            if (!mostraBottone) // Sostituisce il bottone con una cella testo vuota
                dataGridView1.Rows[rowIndex].Cells[4] = new DataGridViewTextBoxCell();
            
        }
        private void Notifiche_Load(object sender, EventArgs e)
        {
            if (Variabili_Client.Report.Count == 0) return;

            foreach (var report in Variabili_Client.Report)
            {
                if (report.Tipo == "Spionaggio")
                    AggiungiRiga(report.Tipo, report.Spionaggio.Giocatore.Nome, report.Data, "Dettagli");
                else
                    AggiungiRiga(report.Tipo, "Battaglia", report.Data, "Dettagli");
            }
            
            AggiungiRiga("Esplorazione", "Città Barbaro", "01-01-2026", "Dettagli");
            AggiungiRiga("Esplorazione", "Villaggio Barbaro", "01-01-2026", "Dettagli");
            AggiungiRiga("Attacco", "Adlos", "01-01-2026", "Dettagli");
            AggiungiRiga("Difesa", "Franco", "01-01-2026", "Dettagli");

            //dataGridView1.Rows.Add("Spionaggio/Battaglia", "PVP/PVE", "Città Barbaro/Giocatore", "01-01-2026", "Dettagli");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dataGridView1.Columns[e.ColumnIndex].Name != "Col_Bottone") return;

            clickedRow = e.RowIndex;
            var report = Variabili_Client.Report[clickedRow];

            switch (report.Tipo)
            {
                case "Battaglia":
                    var formBattaglia = new Log_Battaglie(report);
                    formBattaglia.Show();
                    break;

                case "Spionaggio":
                    var formEsplorazione = new Log_Esplorazione(report);
                    formEsplorazione.Show();
                    break;
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }
    }
}
