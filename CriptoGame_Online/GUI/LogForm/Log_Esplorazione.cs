using Server_Strategico.ServerData.Moduli.Battaglie;
using System.Globalization;
using static Server_Strategico.ServerData.Moduli.Battaglie.Battaglia;

namespace Warrior_and_Wealth.GUI
{
    public partial class Log_Esplorazione : Form
    {
        string fase_Selezionata = "Ingresso";
        string struttura = "Civile";
        int livello_Truppa = 0;
        Report temp_Report = new Report();
        CultureInfo cultura = new CultureInfo("it-IT");

        public Log_Esplorazione(Battaglia.Report report)
        {
            InitializeComponent();
            temp_Report = report;
            lbl_Villaggio.Text = $"Villaggio: {report.Spionaggio.Giocatore.Nome}\n" +
                $"Livello: {report.Spionaggio.Giocatore.Livello} | Esperienza: {report.Spionaggio.Giocatore.Esperienza}\n";

            lbl_Spionaggio_Testo.Text = $"Contro-Spionaggio:\nSpionaggio:";
            lbl_Spionaggio_Valore.Text = $"{report.Spionaggio.Ricerca_Civile.Contro_Spionaggio}\n{report.Spionaggio.Ricerca_Civile.Spionaggio}";
        }

        private void Log_Esplorazione_Load(object sender, EventArgs e)
        {
            Unità_Militari(livello_Truppa);
            Load_Ricerca_Civile(temp_Report.Spionaggio.Ricerca_Civile);
            //Load_Ricerca_Militare(temp_Report.Spionaggio.Ricerca_Militare);
            Load_Bonus(temp_Report.Spionaggio.Bonus);
        }

        void Unità_Militari(int livello)
        {
            int faseIdx = fase_Selezionata switch
            {
                "Ingresso" => 0,
                "Mura" => 1,
                "Cancello" => 2,
                "Torri" => 3,
                "Centro" => 4,
                "Castello" => 5,
                "Giocatore" => 6,
                _ => 0
            };

            var esercito = temp_Report.Spionaggio.Fasi[faseIdx];

            var stats = temp_Report.Spionaggio.Stats_Unità;
            int valore = temp_Report.Spionaggio.Fasi[faseIdx].Guerrieri[livello].Reale;

            if (valore == -1)
            {
                lbl_Fase_Unità_Valore.Text =
                    $"{esercito.Guerrieri[livello].Min} - {esercito.Guerrieri[livello].Max}\n" +
                    $"{esercito.Lanceri[livello].Min} - {esercito.Lanceri[livello].Max}\n" +
                    $"{esercito.Arcieri[livello].Min} - {esercito.Arcieri[livello].Max}\n" +
                    $"{esercito.Catapulte[livello].Min} - {esercito.Catapulte[livello].Max}";
            }
            else
            {
                lbl_Fase_Unità_Valore.Text =
                    $"{esercito.Guerrieri[livello].Reale}\n" +
                    $"{esercito.Lanceri[livello].Reale}\n" +
                    $"{esercito.Arcieri[livello].Reale}\n" +
                    $"{esercito.Catapulte[livello].Reale}";
            }

            lbl_Fase_Unità_Testo.Text =
                "Guerriero:\n" +
                "Lanciere:\n" +
                "Arciere:\n" +
                "Catapulta:";

            lbl_Fase_Statistiche.Text =
                    $"|HP: {stats.Guerrieri[livello].Salute}|DEF: {stats.Guerrieri[livello].Salute}|ATK: {stats.Guerrieri[livello].Attacco}\n" +
                    $"|HP: {stats.Lanceri[livello].Salute}|DEF: {stats.Lanceri[livello].Salute}|ATK: {stats.Lanceri[livello].Attacco}\n" +
                    $"|HP: {stats.Arcieri[livello].Salute}|DEF: {stats.Arcieri[livello].Salute}|ATK: {stats.Arcieri[livello].Attacco}\n" +
                    $"|HP: {stats.Catapulte[livello].Salute}|DEF: {stats.Catapulte[livello].Salute}|ATK: {stats.Catapulte[livello].Attacco}\n";
        }

        void Edifici_Civili()
        {
            //Caricare immagini corrette
            if (temp_Report.Spionaggio.Strutture_Civili.Fattoria.Reale != -1)
            {
                lbl_Fattoria.Text = temp_Report.Spionaggio.Strutture_Civili.Fattoria.Reale.ToString();
                lbl_Segheria.Text = temp_Report.Spionaggio.Strutture_Civili.Segheria.Reale.ToString();
                lbl_Cava.Text = temp_Report.Spionaggio.Strutture_Civili.Cava.Reale.ToString();
                lbl_Miniera_Ferro.Text = temp_Report.Spionaggio.Strutture_Civili.Miniera_Ferrro.Reale.ToString();
                lbl_Miniera_Oro.Text = temp_Report.Spionaggio.Strutture_Civili.Miniera_Oro.Reale.ToString();
                lbl_Abitazioni.Text = temp_Report.Spionaggio.Strutture_Civili.Abitazioni.Reale.ToString();
            }else
            {
                lbl_Fattoria.Text = $"{temp_Report.Spionaggio.Strutture_Civili.Fattoria.Min.ToString()} - {temp_Report.Spionaggio.Strutture_Civili.Fattoria.Max.ToString()}";
                lbl_Segheria.Text = $"{temp_Report.Spionaggio.Strutture_Civili.Segheria.Min.ToString()} - {temp_Report.Spionaggio.Strutture_Civili.Segheria.Max.ToString()}";
                lbl_Cava.Text = $"{temp_Report.Spionaggio.Strutture_Civili.Cava.Min.ToString()} - {temp_Report.Spionaggio.Strutture_Civili.Cava.Max.ToString()}";
                lbl_Miniera_Ferro.Text = $"{temp_Report.Spionaggio.Strutture_Civili.Miniera_Ferrro.Min.ToString()} - {temp_Report.Spionaggio.Strutture_Civili.Miniera_Ferrro.Max.ToString()}";
                lbl_Miniera_Oro.Text = $"{temp_Report.Spionaggio.Strutture_Civili.Miniera_Oro.Min.ToString()} - {temp_Report.Spionaggio.Strutture_Civili.Miniera_Oro.Max.ToString()}";
                lbl_Abitazioni.Text = $"{temp_Report.Spionaggio.Strutture_Civili.Abitazioni.Min.ToString()} - {temp_Report.Spionaggio.Strutture_Civili.Abitazioni.Max.ToString()}";
            }
        }
        void Edifici_Militari()
        {
            //Caricare immagini corrette
            if (temp_Report.Spionaggio.Strutture_Civili.Fattoria.Reale != -1)
            {
                lbl_Fattoria.Text = temp_Report.Spionaggio.Workshop.Spade.Reale.ToString();
                lbl_Segheria.Text = temp_Report.Spionaggio.Workshop.Lance.Reale.ToString();
                lbl_Cava.Text = temp_Report.Spionaggio.Workshop.Archi.Reale.ToString();
                lbl_Miniera_Ferro.Text = temp_Report.Spionaggio.Workshop.Scudi.Reale.ToString();
                lbl_Miniera_Oro.Text = temp_Report.Spionaggio.Workshop.Armature.Reale.ToString();
                lbl_Abitazioni.Text = temp_Report.Spionaggio.Workshop.Frecce.Reale.ToString();
            }
            else
            {
                lbl_Fattoria.Text = $"{temp_Report.Spionaggio.Workshop.Spade.Min.ToString()} - {temp_Report.Spionaggio.Workshop.Spade.Max.ToString()}";
                lbl_Segheria.Text = $"{temp_Report.Spionaggio.Workshop.Lance.Min.ToString()} - {temp_Report.Spionaggio.Workshop.Lance.Max.ToString()}";
                lbl_Cava.Text = $"{temp_Report.Spionaggio.Workshop.Archi.Min.ToString()} - {temp_Report.Spionaggio.Workshop.Archi.Max.ToString()}";
                lbl_Miniera_Ferro.Text = $"{temp_Report.Spionaggio.Workshop.Scudi.Min.ToString()} - {temp_Report.Spionaggio.Workshop.Scudi.Max.ToString()}";
                lbl_Miniera_Oro.Text = $"{temp_Report.Spionaggio.Workshop.Armature.Min.ToString()} - {temp_Report.Spionaggio.Workshop.Armature.Max.ToString()}";
                lbl_Abitazioni.Text = $"{temp_Report.Spionaggio.Workshop.Frecce.Min.ToString()} - {temp_Report.Spionaggio.Workshop.Frecce.Max.ToString()}";
            }
        }
        void Stats_Fase()
        {
            switch (fase_Selezionata)
            {
                case "Ingresso":
                    if (temp_Report.Spionaggio.Fasi[0].Struttura.Guarnigione.Reale != -1)
                        lbl_Fase_Valore.Text = 
                            $"\n" +
                            $"\n" +
                            $"{temp_Report.Spionaggio.Fasi[0].Struttura.Guarnigione.Reale}/{temp_Report.Spionaggio.Fasi[0].Struttura.Guarnigione.Reale}\n";
                    else
                        lbl_Fase_Valore.Text =
                            $"\n" +
                            $"\n" +
                            $"{temp_Report.Spionaggio.Fasi[0].Struttura.Guarnigione.Min}/{temp_Report.Spionaggio.Fasi[0].Struttura.Guarnigione.Max}\n";

                    lbl_Rcerca_Villaggio_Testo.Text =
                        $"\n" +
                        $"\n" +
                        "Guarnigione:\n";
                    
                    break;
                case "Mura":
                    if (temp_Report.Spionaggio.Fasi[1].Struttura.Guarnigione.Reale != -1)
                        lbl_Fase_Valore.Text =
                            $"{temp_Report.Spionaggio.Fasi[1].Struttura.Salute}/{temp_Report.Spionaggio.Fasi[1].Struttura.SaluteMax}\n" +
                            $"{temp_Report.Spionaggio.Fasi[1].Struttura.Difesa}/{temp_Report.Spionaggio.Fasi[1].Struttura.DifesaMax}\n" +
                            $"{temp_Report.Spionaggio.Fasi[1].Struttura.Guarnigione.Reale}/{temp_Report.Spionaggio.Fasi[1].Struttura.Guarnigione.Reale}\n";
                    else
                        lbl_Fase_Valore.Text =
                            $"{temp_Report.Spionaggio.Fasi[1].Struttura.Salute}/{temp_Report.Spionaggio.Fasi[1].Struttura.SaluteMax}\n" +
                            $"{temp_Report.Spionaggio.Fasi[1].Struttura.Difesa}/{temp_Report.Spionaggio.Fasi[1].Struttura.DifesaMax}\n" +
                            $"{temp_Report.Spionaggio.Fasi[1].Struttura.Guarnigione.Min}/{temp_Report.Spionaggio.Fasi[1].Struttura.Guarnigione.Max}\n";

                    lbl_Rcerca_Villaggio_Testo.Text =
                        "HP:\n" +
                        "DEF:\n" +
                        "Guarnigione:";
                    break;
                case "Cancello":
                    if (temp_Report.Spionaggio.Fasi[2].Struttura.Guarnigione.Reale != -1)
                        lbl_Fase_Valore.Text =
                            $"{temp_Report.Spionaggio.Fasi[2].Struttura.Salute}/{temp_Report.Spionaggio.Fasi[2].Struttura.SaluteMax}\n" +
                            $"{temp_Report.Spionaggio.Fasi[2].Struttura.Difesa}/{temp_Report.Spionaggio.Fasi[2].Struttura.DifesaMax}\n" +
                            $"{temp_Report.Spionaggio.Fasi[2].Struttura.Guarnigione.Reale}/{temp_Report.Spionaggio.Fasi[2].Struttura.Guarnigione.Reale}\n";
                    else
                        lbl_Fase_Valore.Text =
                            $"{temp_Report.Spionaggio.Fasi[2].Struttura.Salute}/{temp_Report.Spionaggio.Fasi[2].Struttura.SaluteMax}\n" +
                            $"{temp_Report.Spionaggio.Fasi[2].Struttura.Difesa}/{temp_Report.Spionaggio.Fasi[2].Struttura.DifesaMax}\n" +
                            $"{temp_Report.Spionaggio.Fasi[2].Struttura.Guarnigione.Min}/{temp_Report.Spionaggio.Fasi[2].Struttura.Guarnigione.Max}\n";

                    lbl_Rcerca_Villaggio_Testo.Text =
                        "HP:\n" +
                        "DEF:\n" +
                        "Guarnigione:";
                    break;
                case "Torri":
                    if (temp_Report.Spionaggio.Fasi[3].Struttura.Guarnigione.Reale != -1)
                        lbl_Fase_Valore.Text =
                            $"{temp_Report.Spionaggio.Fasi[3].Struttura.Salute}/{temp_Report.Spionaggio.Fasi[3].Struttura.SaluteMax}\n" +
                            $"{temp_Report.Spionaggio.Fasi[3].Struttura.Difesa}/{temp_Report.Spionaggio.Fasi[3].Struttura.DifesaMax}\n" +
                            $"{temp_Report.Spionaggio.Fasi[3].Struttura.Guarnigione.Reale}/{temp_Report.Spionaggio.Fasi[3].Struttura.Guarnigione.Reale}\n";
                    else
                        lbl_Fase_Valore.Text =
                            $"{temp_Report.Spionaggio.Fasi[3].Struttura.Salute}/{temp_Report.Spionaggio.Fasi[3].Struttura.SaluteMax}\n" +
                            $"{temp_Report.Spionaggio.Fasi[3].Struttura.Difesa}/{temp_Report.Spionaggio.Fasi[3].Struttura.DifesaMax}\n" +
                            $"{temp_Report.Spionaggio.Fasi[3].Struttura.Guarnigione.Min}/{temp_Report.Spionaggio.Fasi[3].Struttura.Guarnigione.Max}\n";

                    lbl_Rcerca_Villaggio_Testo.Text =
                        "HP:\n" +
                        "DEF:\n" +
                        "Guarnigione:";
                    break;
                case "Centro":
                    if (temp_Report.Spionaggio.Fasi[4].Struttura.Guarnigione.Reale != -1)
                        lbl_Fase_Valore.Text =
                            $"\n" +
                            $"\n" +
                            $"{temp_Report.Spionaggio.Fasi[4].Struttura.Guarnigione.Reale}/{temp_Report.Spionaggio.Fasi[4].Struttura.Guarnigione.Reale}\n";
                    else
                        lbl_Fase_Valore.Text =
                            $"\n" +
                            $"\n" +
                            $"{temp_Report.Spionaggio.Fasi[4].Struttura.Guarnigione.Min}/{temp_Report.Spionaggio.Fasi[4].Struttura.Guarnigione.Max}\n";

                    lbl_Rcerca_Villaggio_Testo.Text =
                        $"\n" +
                        $"\n" +
                        "Guarnigione:";
                    break;
                case "Castello":
                    if (temp_Report.Spionaggio.Fasi[5].Struttura.Guarnigione.Reale != -1)
                        lbl_Fase_Valore.Text =
                            $"{temp_Report.Spionaggio.Fasi[5].Struttura.Salute}/{temp_Report.Spionaggio.Fasi[5].Struttura.SaluteMax}\n" +
                            $"{temp_Report.Spionaggio.Fasi[5].Struttura.Difesa}/{temp_Report.Spionaggio.Fasi[5].Struttura.DifesaMax}\n" +
                            $"{temp_Report.Spionaggio.Fasi[5].Struttura.Guarnigione.Reale}/{temp_Report.Spionaggio.Fasi[5].Struttura.Guarnigione.Reale}\n";
                    else
                        lbl_Fase_Valore.Text =
                            $"{temp_Report.Spionaggio.Fasi[5].Struttura.Salute}/{temp_Report.Spionaggio.Fasi[5].Struttura.SaluteMax}\n" +
                            $"{temp_Report.Spionaggio.Fasi[5].Struttura.Difesa}/{temp_Report.Spionaggio.Fasi[5].Struttura.DifesaMax}\n" +
                            $"{temp_Report.Spionaggio.Fasi[5].Struttura.Guarnigione.Min}/{temp_Report.Spionaggio.Fasi[5].Struttura.Guarnigione.Max}\n";

                    lbl_Rcerca_Villaggio_Testo.Text =
                        $"HP:\n" +
                        $"DEF:\n" +
                        "Guarnigione:";
                    break;
                case "Giocatore":
                    if (temp_Report.Spionaggio.Fasi[6].Struttura.Guarnigione.Reale != -1)
                        lbl_Fase_Valore.Text =
                            $"\n" +
                            $"\n" +
                            $"\n";

                    lbl_Rcerca_Villaggio_Testo.Text =
                        $"\n" +
                        $"\n" +
                        "";
                    break;
            }
            Unità_Militari(livello_Truppa);
        }
        void Load_Ricerca_Civile(RicercaCivile report)
        {
            lbl_Ricerca_Base_Testo.Text =
                "Produzione:\n" +
                "Costruzione:\n" +
                "Addestramento:\n" +
                "Popolazione:\n" +
                "Trasporto:\n" +
                "Riparazione:";

            lbl_Ricerca_Base_Valore.Text =
                $"{report.Produzione}\n" +
                $"{report.Costruzione}\n" +
                $"{report.Addestramento}\n" +
                $"{report.Popolazione}\n" +
                $"{report.Trasporto}\n" +
                $"{report.Riparazione}";
        }
        void Load_Ricerca_Militare(RicercaMilitare report)
        {
            lbl_Ricerca_Base_Testo.Text =
                "Guerrieri Salute:\n" +
                "Guerrieri Difesa:\n" +
                "Guerrieri Attacco:\n" +
                "Guerrieri Livello:\n" +

                "Lanceri Salute:\n" +
                "Lanceri Difesa:\n" +
                "Lanceri Attacco:\n" +
                "Lanceri Livello:\n" +

                "Arcieri Salute:\n" +
                "Arcieri Difesa:\n" +
                "Arcieri Attacco:\n" +
                "Arcieri Livello:\n" +

                "Catapulte Salute:\n" +
                "Catapulte Difesa:\n" +
                "Catapulte Attacco:\n" +
                "Catapulte Livello:";

            lbl_Ricerca_Base_Valore.Text =
                $"{report.Guerrieri.Salute}\n" +
                $"{report.Guerrieri.Difesa}\n" +
                $"{report.Guerrieri.Attacco}\n" +
                $"{report.Guerrieri.Livello}\n" +

                $"{report.Lanceri.Salute}\n" +
                $"{report.Lanceri.Difesa}\n" +
                $"{report.Lanceri.Attacco}\n" +
                $"{report.Lanceri.Livello}\n" +

                $"{report.Arcieri.Salute}\n" +
                $"{report.Arcieri.Difesa}\n" +
                $"{report.Arcieri.Attacco}\n" +
                $"{report.Arcieri.Livello}\n" +

                $"{report.Catapulte.Salute}\n" +
                $"{report.Catapulte.Difesa}\n" +
                $"{report.Catapulte.Attacco}\n" +
                $"{report.Catapulte.Livello}";
        }
        void Load_Bonus(Bonus report)
        {
            lbl_Bonus_Testo.Text =
                "Guerrieri Salute:\n" +
                "Guerrieri Difesa:\n" +
                "Guerrieri Attacco:\n" +
                "Guerrieri Livello:\n" +

                "Lanceri Salute:\n" +
                "Lanceri Difesa:\n" +
                "Lanceri Attacco:\n" +
                "Lanceri Livello:\n" +

                "Arcieri Salute:\n" +
                "Arcieri Difesa:\n" +
                "Arcieri Attacco:\n" +
                "Arcieri Livello:\n" +

                "Catapulte Salute:\n" +
                "Catapulte Difesa:\n" +
                "Catapulte Attacco:\n" +
                "Catapulte Livello:\n" +

                "Strutture Salute:\n" +
                "Strutture Difesa:\n" +
                "Strutture Guarnigione:\n" +

                "Produzione Risorse:\n" +
                "Costruzione:\n" +
                "Addestramento:\n" +
                "Capacità Trasporto:\n" +
                "Ricerca:\n" +
                "Riparazione:\n" +
                "Spionaggio:\n" +
                "Contro-Spionaggio:";

            lbl_Bonus_Valore.Text =
                $"{report.Guerrieri.Salute}\n" +
                $"{report.Guerrieri.Difesa}\n" +
                $"{report.Guerrieri.Attacco}\n" +
                $"{report.Guerrieri.Livello}\n" +

                $"{report.Lanceri.Salute}\n" +
                $"{report.Lanceri.Difesa}\n" +
                $"{report.Lanceri.Attacco}\n" +
                $"{report.Lanceri.Livello}\n" +

                $"{report.Arceri.Salute}\n" +
                $"{report.Arceri.Difesa}\n" +
                $"{report.Arceri.Attacco}\n" +
                $"{report.Arceri.Livello}\n" +

                $"{report.Catapulte.Salute}\n" +
                $"{report.Catapulte.Difesa}\n" +
                $"{report.Catapulte.Attacco}\n" +
                $"{report.Catapulte.Livello}\n" +

                $"{report.Salute_Strutture}\n" +
                $"{report.Difesa_Strutture}\n" +
                $"{report.Guarnigione_Strutture}\n" +

                $"{report.Produzione_Risorse}\n" +
                $"{report.Costruzione}\n" +
                $"{report.Addestramento}\n" +
                $"{report.Capacità_Trasporto}\n" +
                $"{report.Ricerca}\n" +
                $"{report.Riparazione}\n" +
                $"{report.Spionaggio}\n" +
                $"{report.Contro_Spionaggio}";
        }

        private void lbl_Livello_Truppe_1_MouseClick(object sender, MouseEventArgs e)
        {
            livello_Truppa = 0;
            Unità_Militari(livello_Truppa);
        }

        private void lbl_Livello_Truppe_2_MouseClick(object sender, MouseEventArgs e)
        {
            livello_Truppa = 1;
            Unità_Militari(livello_Truppa);
        }

        private void lbl_Livello_Truppe_3_MouseClick(object sender, MouseEventArgs e)
        {
            livello_Truppa = 2;
            Unità_Militari(livello_Truppa);
        }

        private void lbl_Livello_Truppe_4_MouseClick(object sender, MouseEventArgs e)
        {
            livello_Truppa = 3;
            Unità_Militari(livello_Truppa);
        }

        private void lbl_Livello_Truppe_5_MouseClick(object sender, MouseEventArgs e)
        {
            livello_Truppa = 4;
            Unità_Militari(livello_Truppa);
        }

        private void lbl_Livello_Truppe_All_MouseClick(object sender, MouseEventArgs e)
        {
            livello_Truppa = -1;
        }

        private void lbl_Ingresso_MouseClick(object sender, MouseEventArgs e)
        {
            fase_Selezionata = "Ingresso";
            Stats_Fase();
        }

        private void lbl_Mura_MouseClick(object sender, MouseEventArgs e)
        {
            fase_Selezionata = "Mura";
            Stats_Fase();
        }

        private void lbl_Cancello_MouseClick(object sender, MouseEventArgs e)
        {
            fase_Selezionata = "Cancello";
            Stats_Fase();
        }

        private void lbl_Torri_MouseClick(object sender, MouseEventArgs e)
        {
            fase_Selezionata = "Torri";
            Stats_Fase();
        }

        private void lbl_Centro_MouseClick(object sender, MouseEventArgs e)
        {
            fase_Selezionata = "Centro";
            Stats_Fase();
        }

        private void lbl_Castello_MouseClick(object sender, MouseEventArgs e)
        {
            fase_Selezionata = "Castello";
            Stats_Fase();
        }

        private void lbl_Giocatore_MouseClick(object sender, MouseEventArgs e)
        {
            fase_Selezionata = "Giocatore";
            Stats_Fase();
        }

        private void btn_Strutture_Civile_Militare_Click(object sender, EventArgs e)
        {
            if (struttura == "Militare")
                Edifici_Militari();
            else
                Edifici_Civili();
            
        }
    }
}
