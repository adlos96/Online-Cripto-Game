using Server_Strategico.ServerData.Moduli.Battaglie;
using System.Globalization;
using Warrior_and_Wealth.Strumenti;
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
            fase_Selezionata = "Ingresso";
            struttura = "Civile";
            lbl_Ricerca_Civile.Text = "Ricerca Civile";
            temp_Report = report;
            lbl_Villaggio.Text = $"Villaggio: {report.Spionaggio.Giocatore.Nome}\n" +
                $"Livello: {report.Spionaggio.Giocatore.Livello} | Esperienza: {report.Spionaggio.Giocatore.Esperienza}\n";

            lbl_Spionaggio_Testo.Text = $"Contro-Spionaggio:\nSpionaggio:";
            lbl_Spionaggio_Valore.Text = $"{report.Spionaggio.Ricerca_Civile.Contro_Spionaggio}\n{report.Spionaggio.Ricerca_Civile.Spionaggio}";
        }

        private void Log_Esplorazione_Load(object sender, EventArgs e)
        {
            int livello = temp_Report.Spionaggio.Stadio;

            panel_Fasi.Visible = false;
            panel_Risorse.Visible = false;
            panel_Unità.Visible = false;
            panel_Fasi_Stats.Visible = false;
            panel_Strutture.Visible = false;
            panel_Caserme.Visible = false;
            panel_Ricerca.Visible = false;
            panel_Bonus.Visible = false;
            panel_Villaggio.Visible = false;

            btn_Strutture_Civile_Militare.Visible = false;
            lbl_Strutture_Civile_Militare.Visible = false;
            lbl_Caserme.Visible = false;
            Ricerca_Civ_Mil.Visible = false;
            lbl_Ricerca_Civile.Visible = false;
            lbl_Ricerca_Villaggio.Visible = false;
            lbl_Bonus.Visible = false;

            if (temp_Report.Spionaggio.Spionaggio_Riuscito == false)
            {
                lbl_Spionaggio_Fallito.Visible = true;
                lbl_Spionaggio_Fallito_Desc.Visible = true;
                lbl_Spionaggio_Fallito.ForeColor = Color.Red;
                lbl_Spionaggio_Fallito_Desc.ForeColor = Color.DarkGray;

                lbl_Spionaggio_Fallito.Text = "Spionaggio Fallito!";
                lbl_Spionaggio_Fallito_Desc.Text = "Il villaggio è troppo ben difeso o il tuo esploratore è stato scoperto.\n" +
                    "Non hai ottenuto alcuna informazione.";
            }

            if (livello >= 1)
            {
                panel_Risorse.Visible = true;
                Load_Risorse(temp_Report.Spionaggio);
            }
            if (livello >= 2)
            {
                panel_Fasi.Visible = true;
                panel_Unità.Visible = true;
                panel_Fasi_Stats.Visible = true;
                Stats_Fase(); //Stats truppe + truppe + fase
            }
            if (livello >= 3)
            {
                panel_Strutture.Visible = true;
                btn_Strutture_Civile_Militare.Visible = true;
                lbl_Strutture_Civile_Militare.Visible = true;
                Edifici_Civili();
            }
            if (livello >= 4)
            {
                panel_Caserme.Visible = true;
                lbl_Caserme.Visible = true;
                Load_Caserme(temp_Report.Spionaggio.Caserme);
            }
            if (livello >= 5)
            {
                panel_Ricerca.Visible = true;
                Ricerca_Civ_Mil.Visible = true;
                lbl_Ricerca_Civile.Visible = true;
                Load_Ricerca_Civile(temp_Report.Spionaggio.Ricerca_Civile);
                Load_Ricerca_Militare(temp_Report.Spionaggio.Ricerca_Militare);
            }
            if (livello >= 6)
            {
                panel_Bonus.Visible = true;
                panel_Villaggio.Visible = true;
                lbl_Ricerca_Villaggio.Visible = true;
                lbl_Bonus.Visible = true;
                Load_Bonus(temp_Report.Spionaggio.Bonus);
            }
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
            ico_Edificio_1.BackgroundImage = Properties.Resources.Fattoria_V2;
            ico_Edificio_2.BackgroundImage = Properties.Resources.Segheria_V2;
            ico_Edificio_3.BackgroundImage = Properties.Resources.CavaDiPietra_V2;
            ico_Edificio_4.BackgroundImage = Properties.Resources.MinieraFerro_V2;
            ico_Edificio_5.BackgroundImage = Properties.Resources.MinieraOro_V2;
            ico_Edificio_6.BackgroundImage = Properties.Resources.Abitazioni_V2;

            if (temp_Report.Spionaggio.Strutture_Civili.Fattoria.Reale != -1)
            {
                lbl_Fattoria.Text = temp_Report.Spionaggio.Strutture_Civili.Fattoria.Reale.ToString();
                lbl_Segheria.Text = temp_Report.Spionaggio.Strutture_Civili.Segheria.Reale.ToString();
                lbl_Cava.Text = temp_Report.Spionaggio.Strutture_Civili.Cava.Reale.ToString();
                lbl_Miniera_Ferro.Text = temp_Report.Spionaggio.Strutture_Civili.Miniera_Ferrro.Reale.ToString();
                lbl_Miniera_Oro.Text = temp_Report.Spionaggio.Strutture_Civili.Miniera_Oro.Reale.ToString();
                lbl_Abitazioni.Text = temp_Report.Spionaggio.Strutture_Civili.Abitazioni.Reale.ToString();
            }
            else
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
            ico_Edificio_1.BackgroundImage = Properties.Resources.Workshop_Spade_V2;
            ico_Edificio_2.BackgroundImage = Properties.Resources.Workshop_Lance_V2;
            ico_Edificio_3.BackgroundImage = Properties.Resources.Workshop_Archi_V2;
            ico_Edificio_4.BackgroundImage = Properties.Resources.Workshop_Scudi_V2;
            ico_Edificio_5.BackgroundImage = Properties.Resources.Workshop_Armature_V2;
            ico_Edificio_6.BackgroundImage = Properties.Resources.Workshop_Frecce_V2;

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
            if (fase_Selezionata == "Ingresso" || fase_Selezionata == "Centro" || fase_Selezionata == "Giocatore")
                lbl_Rcerca_Villaggio_Testo.Text =
                    $"\n" +
                    $"\n" +
                    "Guarnigione:\n";
            else
                lbl_Rcerca_Villaggio_Testo.Text =
                    "HP:\n" +
                    "DEF:\n" +
                    "Guarnigione:";

            switch (fase_Selezionata)
            {
                case "Ingresso":
                    lbl_Fase_Valore.Text =
                        $"\n" +
                        $"\n" +
                        $"{temp_Report.Spionaggio.Fasi[0].Struttura.Guarnigione.Reale}/{temp_Report.Spionaggio.Fasi[0].Struttura.Guarnigione_Max}";
                    break;
                case "Mura":
                    lbl_Fase_Valore.Text =
                        $"{temp_Report.Spionaggio.Fasi[1].Struttura.Salute}/{temp_Report.Spionaggio.Fasi[1].Struttura.SaluteMax}\n" +
                        $"{temp_Report.Spionaggio.Fasi[1].Struttura.Difesa}/{temp_Report.Spionaggio.Fasi[1].Struttura.DifesaMax}\n" +
                        $"{temp_Report.Spionaggio.Fasi[1].Struttura.Guarnigione.Reale}/{temp_Report.Spionaggio.Fasi[1].Struttura.Guarnigione_Max}";
                    break;
                case "Cancello":
                    lbl_Fase_Valore.Text =
                        $"{temp_Report.Spionaggio.Fasi[2].Struttura.Salute}/{temp_Report.Spionaggio.Fasi[2].Struttura.SaluteMax}\n" +
                        $"{temp_Report.Spionaggio.Fasi[2].Struttura.Difesa}/{temp_Report.Spionaggio.Fasi[2].Struttura.DifesaMax}\n" +
                        $"{temp_Report.Spionaggio.Fasi[2].Struttura.Guarnigione.Reale}/{temp_Report.Spionaggio.Fasi[2].Struttura.Guarnigione_Max}";
                    break;
                case "Torri":
                    lbl_Fase_Valore.Text =
                        $"{temp_Report.Spionaggio.Fasi[3].Struttura.Salute}/{temp_Report.Spionaggio.Fasi[3].Struttura.SaluteMax}\n" +
                        $"{temp_Report.Spionaggio.Fasi[3].Struttura.Difesa}/{temp_Report.Spionaggio.Fasi[3].Struttura.DifesaMax}\n" +
                        $"{temp_Report.Spionaggio.Fasi[3].Struttura.Guarnigione.Reale}/{temp_Report.Spionaggio.Fasi[3].Struttura.Guarnigione_Max}";
                    break;
                case "Centro":
                    lbl_Fase_Valore.Text =
                        $"\n" +
                        $"\n" +
                        $"{temp_Report.Spionaggio.Fasi[4].Struttura.Guarnigione.Reale}/{temp_Report.Spionaggio.Fasi[4].Struttura.Guarnigione_Max}";
                    break;
                case "Castello":
                    lbl_Fase_Valore.Text =
                        $"{temp_Report.Spionaggio.Fasi[5].Struttura.Salute}/{temp_Report.Spionaggio.Fasi[5].Struttura.SaluteMax}\n" +
                        $"{temp_Report.Spionaggio.Fasi[5].Struttura.Difesa}/{temp_Report.Spionaggio.Fasi[5].Struttura.DifesaMax}\n" +
                        $"{temp_Report.Spionaggio.Fasi[5].Struttura.Guarnigione.Reale}/{temp_Report.Spionaggio.Fasi[5].Struttura.Guarnigione_Max}";
                    break;
                case "Giocatore":
                    lbl_Fase_Valore.Text =
                      $"\n" +
                      $"\n" +
                      $"{temp_Report.Spionaggio.Fasi[6].Struttura.Guarnigione.Reale}/{temp_Report.Spionaggio.Fasi[6].Struttura.Guarnigione_Max}";
                    break;
            }
            Unità_Militari(livello_Truppa);
        }
        void Load_Risorse(RisultatoSpionaggio report)
        {
            lbl_Cibo.Text = $"{report.Risorse_Civili.Cibo}";
            lbl_Legno.Text = $"{report.Risorse_Civili.Legno}";
            lbl_Pietra.Text = $"{report.Risorse_Civili.Pietra}";
            lbl_Ferro.Text = $"{report.Risorse_Civili.Ferro}";
            lbl_Oro.Text = $"{report.Risorse_Civili.Oro}";
            lbl_Spade.Text = $"{report.Risorse_Militari.Spade}";
            lbl_Lance.Text = $"{report.Risorse_Militari.Lance}";
            lbl_Archi.Text = $"{report.Risorse_Militari.Archi}";
            lbl_Frecce.Text = $"{report.Risorse_Militari.Frecce}";
            lbl_Popolazione.Text = $"{report.Risorse_Civili.Popolazione}";

            lbl_Diamanti_Blu.Text = $"{report.Risorse_Speciali.Diamanti_Blu}";
            lbl_Diamanti_Viola.Text = $"{report.Risorse_Speciali.Diamanti_Viola}";
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
                "Guerrieri Livello:\n\n" +

                "Lanceri Salute:\n" +
                "Lanceri Difesa:\n" +
                "Lanceri Attacco:\n" +
                "Lanceri Livello:\n\n" +

                "Arcieri Salute:\n" +
                "Arcieri Difesa:\n" +
                "Arcieri Attacco:\n" +
                "Arcieri Livello:\n\n" +

                "Catapulte Salute:\n" +
                "Catapulte Difesa:\n" +
                "Catapulte Attacco:\n" +
                "Catapulte Livello:";

            lbl_Ricerca_Base_Valore.Text =
                $"{report.Guerrieri.Salute}\n" +
                $"{report.Guerrieri.Difesa}\n" +
                $"{report.Guerrieri.Attacco}\n" +
                $"{report.Guerrieri.Livello}\n\n" +

                $"{report.Lanceri.Salute}\n" +
                $"{report.Lanceri.Difesa}\n" +
                $"{report.Lanceri.Attacco}\n" +
                $"{report.Lanceri.Livello}\n\n" +

                $"{report.Arcieri.Salute}\n" +
                $"{report.Arcieri.Difesa}\n" +
                $"{report.Arcieri.Attacco}\n" +
                $"{report.Arcieri.Livello}\n\n" +

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
                "Guerrieri Livello:\n\n" +

                "Lanceri Salute:\n" +
                "Lanceri Difesa:\n" +
                "Lanceri Attacco:\n" +
                "Lanceri Livello:\n\n" +

                "Arcieri Salute:\n" +
                "Arcieri Difesa:\n" +
                "Arcieri Attacco:\n" +
                "Arcieri Livello:\n\n" +

                "Catapulte Salute:\n" +
                "Catapulte Difesa:\n" +
                "Catapulte Attacco:\n" +
                "Catapulte Livello:\n\n" +

                "Strutture Salute:\n" +
                "Strutture Difesa:\n" +
                "Strutture Guarnigione:\n\n" +

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
                $"{report.Guerrieri.Livello}\n\n" +

                $"{report.Lanceri.Salute}\n" +
                $"{report.Lanceri.Difesa}\n" +
                $"{report.Lanceri.Attacco}\n" +
                $"{report.Lanceri.Livello}\n\n" +

                $"{report.Arceri.Salute}\n" +
                $"{report.Arceri.Difesa}\n" +
                $"{report.Arceri.Attacco}\n" +
                $"{report.Arceri.Livello}\n\n" +

                $"{report.Catapulte.Salute}\n" +
                $"{report.Catapulte.Difesa}\n" +
                $"{report.Catapulte.Attacco}\n" +
                $"{report.Catapulte.Livello}\n\n" +

                $"{report.Salute_Strutture}\n" +
                $"{report.Difesa_Strutture}\n" +
                $"{report.Guarnigione_Strutture}\n\n" +

                $"{report.Produzione_Risorse}\n" +
                $"{report.Costruzione}\n" +
                $"{report.Addestramento}\n" +
                $"{report.Capacità_Trasporto}\n" +
                $"{report.Ricerca}\n" +
                $"{report.Riparazione}\n" +
                $"{report.Spionaggio}\n" +
                $"{report.Contro_Spionaggio}";
        }
        void Load_Caserme(Caserme report)
        {
            ico_Caserma_1.BackgroundImage = Properties.Resources.Caserma_Guerieri_V2;
            ico_Caserma_2.BackgroundImage = Properties.Resources.Caserma_Lanceri_V2;
            ico_Caserma_3.BackgroundImage = Properties.Resources.Caserma_Arcieri_V2;
            ico_Caserma_4.BackgroundImage = Properties.Resources.Caserma_Catapulte_V2;

            if (temp_Report.Spionaggio.Strutture_Civili.Fattoria.Reale != -1)
            {
                lbl_Caserma_Guerrieri.Text = report.Guerrieri.Reale.ToString();
                lbl_Caserma_Lanceri.Text = report.Lanceri.Reale.ToString();
                lbl_Caserma_Arceri.Text = report.Arcieri.Reale.ToString();
                lbl_Caserma_Catapulte.Text = report.Catapulte.Reale.ToString();
            }
            else
            {
                lbl_Caserma_Guerrieri.Text = $"{report.Guerrieri.Min.ToString()} - {report.Guerrieri.Max.ToString()}";
                lbl_Caserma_Lanceri.Text = $"{report.Lanceri.Min.ToString()} - {report.Lanceri.Max.ToString()}";
                lbl_Caserma_Arceri.Text = $"{report.Arcieri.Min.ToString()} - {report.Arcieri.Max.ToString()}";
                lbl_Caserma_Catapulte.Text = $"{report.Catapulte.Min.ToString()} - {report.Catapulte.Max.ToString()}";
            }
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
            ColoreSelezione();
        }

        private void lbl_Mura_MouseClick(object sender, MouseEventArgs e)
        {
            fase_Selezionata = "Mura";
            ColoreSelezione();
        }

        private void lbl_Cancello_MouseClick(object sender, MouseEventArgs e)
        {
            fase_Selezionata = "Cancello";
            ColoreSelezione();
        }

        private void lbl_Torri_MouseClick(object sender, MouseEventArgs e)
        {
            fase_Selezionata = "Torri";
            ColoreSelezione();
        }

        private void lbl_Centro_MouseClick(object sender, MouseEventArgs e)
        {
            fase_Selezionata = "Centro";
            ColoreSelezione();
        }

        private void lbl_Castello_MouseClick(object sender, MouseEventArgs e)
        {
            fase_Selezionata = "Castello";
            ColoreSelezione();
        }

        private void lbl_Giocatore_MouseClick(object sender, MouseEventArgs e)
        {
            fase_Selezionata = "Giocatore";
            ColoreSelezione();
        }

        private void btn_Strutture_Civile_Militare_Click(object sender, EventArgs e)
        {
            if (struttura == "Militare")
            {
                struttura = "Civile";
                Edifici_Militari();
            }
            else
            {
                struttura = "Militare";
                Edifici_Civili();
            }
        }

        private void Ricerca_Civ_Mil_Click(object sender, EventArgs e)
        {
            if (lbl_Ricerca_Civile.Text == "Ricerca Civile")
            {
                lbl_Ricerca_Civile.Text = "Ricerca Militare";
                Load_Ricerca_Militare(temp_Report.Spionaggio.Ricerca_Militare);
            }
            else
            {
                lbl_Ricerca_Civile.Text = "Ricerca Civile";
                Load_Ricerca_Civile(temp_Report.Spionaggio.Ricerca_Civile);
            }
        }
        void ColoreSelezione()
        {
            lbl_Ingresso.ForeColor = Color.Yellow;
            lbl_Mura.ForeColor = Color.Yellow;
            lbl_Cancello.ForeColor = Color.Yellow;
            lbl_Torri.ForeColor = Color.Yellow;
            lbl_Centro.ForeColor = Color.Yellow;
            lbl_Castello.ForeColor = Color.Yellow;
            lbl_Giocatore.ForeColor = Color.Yellow;
            switch (fase_Selezionata)
            {
                case "Ingresso":
                    lbl_Ingresso.ForeColor = Color.Turquoise;
                    break;
                case "Mura":
                    lbl_Mura.ForeColor = Color.Turquoise;
                    break;
                case "Cancello":
                    lbl_Cancello.ForeColor = Color.Turquoise;
                    break;
                case "Torri":
                    lbl_Torri.ForeColor = Color.Turquoise;
                    break;
                case "Centro":
                    lbl_Centro.ForeColor = Color.Turquoise;
                    break;
                case "Castello":
                    lbl_Castello.ForeColor = Color.Turquoise;
                    break;
                case "Giocatore":
                    lbl_Giocatore.ForeColor = Color.Turquoise;
                    break;
            }
            Stats_Fase();
        }
    }
}
