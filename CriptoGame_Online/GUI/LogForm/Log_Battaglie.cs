using Server_Strategico.ServerData.Moduli.Battaglie;
using Strategico_V2;
using System.Globalization;
using static Server_Strategico.ServerData.Moduli.Battaglie.Battaglia;

namespace Warrior_and_Wealth.GUI
{
    public partial class Log_Battaglie : Form
    {
        string fase_Selezionata = "Ingresso";
        string tipo_Battaglia = "Corpo a corpo";
        int livello_Truppa = 0;
        bool attaccante;
        Report temp_Report = new Report();
        CultureInfo cultura = new CultureInfo("it-IT");

        public Log_Battaglie(Battaglia.Report report)
        {
            InitializeComponent();

            Load_Data(report);
            temp_Report = report;
        }
        private async void Log_Battaglie_Load(object sender, EventArgs e)
        {
            AnimaNumero(lbl_Peso, 0, temp_Report.Battaglia.Risorse_Raccolte.Capacità_Carico_Usata, durataMs: 3500, suffisso: $"/{temp_Report.Battaglia.Risorse_Raccolte.Capacità_Carico.ToString("N0", cultura)}");

            AnimaNumero(lbl_Cibo, 0, temp_Report.Battaglia.Risorse_Raccolte.Cibo, durataMs: 3500);
            AnimaNumero(lbl_Legno, 0, temp_Report.Battaglia.Risorse_Raccolte.Legno, durataMs: 3500);
            AnimaNumero(lbl_Pietra, 0, temp_Report.Battaglia.Risorse_Raccolte.Pietra, durataMs: 3500);
            AnimaNumero(lbl_Ferro, 0, temp_Report.Battaglia.Risorse_Raccolte.Ferro, durataMs: 3500);
            AnimaNumero(lbl_Oro, 0, temp_Report.Battaglia.Risorse_Raccolte.Oro, durataMs: 3500);
            AnimaNumero(lbl_DIamanti_Viola, 0, temp_Report.Battaglia.Risorse_Raccolte.Diamanti_Viola, durataMs: 3500);
            AnimaNumero(lbl_DIamanti_Blu, 0, temp_Report.Battaglia.Risorse_Raccolte.Diamanti_Blu, durataMs: 3500);

            if (!attaccante)
            {
                AnimaNumero(lbl_Forza_Attaccante, (int)temp_Report.Battaglia.Forza_Attaccante, (int)temp_Report.Battaglia.Forza_Attaccante_Finale, durataMs: 3500, prefisso: $"Forza: ({temp_Report.Battaglia.Forza_Attaccante.ToString("N0", cultura)}) ");
                await AnimaNumero(lbl_Forza_Difensore, (int)temp_Report.Battaglia.Forza_Difensore, (int)temp_Report.Battaglia.Forza_Difensore_Finale, durataMs: 3500, prefisso: $"Forza: ({temp_Report.Battaglia.Forza_Difensore.ToString("N0", cultura)}) ");
            }
            else
            {
                AnimaNumero(lbl_Forza_Attaccante, (int)temp_Report.Battaglia.Forza_Difensore, (int)temp_Report.Battaglia.Forza_Difensore_Finale, durataMs: 3500, prefisso: $"Forza: ({temp_Report.Battaglia.Forza_Difensore.ToString("N0", cultura)}) ");
                await AnimaNumero(lbl_Forza_Difensore, (int)temp_Report.Battaglia.Forza_Attaccante, (int)temp_Report.Battaglia.Forza_Attaccante_Finale, durataMs: 3500, prefisso: $"Forza: ({temp_Report.Battaglia.Forza_Attaccante.ToString("N0", cultura)}) ");
            }
        }

        // Restituisce T[livello] oppure Sum() in base al livello selezionato
        int Risolvi(int[] array, int livello) => livello == -1 ? array.Sum() : array[livello];

        void Aggiungi_Righe(DataGridView grid, UnitGroup schierati, UnitGroup perdite, UnitGroup sopravvisuti, int livello)
        {
            grid.Rows.Clear();
            grid.Rows.Add("Guerrieri", Risolvi(schierati.Guerrieri, livello), Risolvi(perdite.Guerrieri, livello), Risolvi(sopravvisuti.Guerrieri, livello));
            grid.Rows.Add("Lancieri", Risolvi(schierati.Lancieri, livello), Risolvi(perdite.Lancieri, livello), Risolvi(sopravvisuti.Lancieri, livello));
            grid.Rows.Add("Arcieri", Risolvi(schierati.Arcieri, livello), Risolvi(perdite.Arcieri, livello), Risolvi(sopravvisuti.Arcieri, livello));
            grid.Rows.Add("Catapulte", Risolvi(schierati.Catapulte, livello), Risolvi(perdite.Catapulte, livello), Risolvi(sopravvisuti.Catapulte, livello));
        }

        async void Load_Header(Battaglia.Report report)
        {
            int frecceAttaccante = report.Battaglia.Fasi.Sum(f => f.Fase_Distanza.Attaccante_Frecce_Usate);
            int frecceDifensore = report.Battaglia.Fasi.Sum(f => f.Fase_Distanza.Difensore_Frecce_Usate);

            txt_Vittoria.Text = $"{(report.Battaglia.Vittoria_Attaccante ? "Vittoria" : "Sconfitta")}";
            if (!attaccante)
            {
                lbl_Nome_Attaccante.Text = $"Attaccante: {report.Battaglia.Nome_Attaccante}";
                lbl_Nome_Difensore.Text = $"Difensore: {report.Battaglia.Nome_Difensore}";
                txt_Esperienza.Text = $"Totale esperienza guadagnata: {report.Battaglia.Xp_Attaccante.ToString("N0", cultura)} xp - Frecce usate: {frecceAttaccante.ToString("N0", cultura)}";
                Carica_Bonus(lbl_Bonus_Valore_Attaccante, report.Battaglia.Bonus_Ricerca_Attacco);
                Carica_Bonus(lbl_Bonus_Valore_Difensore, report.Battaglia.Bonus_Ricerca_Difesa);
                if (report.Battaglia.Vittoria_Attaccante)
                {
                    txt_Vittoria.Text = $"Sconfitta";
                    txt_Vittoria.ForeColor = Color.OrangeRed;
                }
            }
            else
            {
                lbl_Nome_Attaccante.Text = $"Attaccante: {report.Battaglia.Nome_Difensore}";
                lbl_Nome_Difensore.Text = $"Difensore: {report.Battaglia.Nome_Attaccante}";
                txt_Esperienza.Text = $"Totale esperienza guadagnata: {report.Battaglia.Xp_Difensore.ToString("N0", cultura)} xp - Frecce usate: {frecceDifensore.ToString("N0", cultura)}";
                Carica_Bonus(lbl_Bonus_Valore_Attaccante, report.Battaglia.Bonus_Ricerca_Difesa);
                Carica_Bonus(lbl_Bonus_Valore_Difensore, report.Battaglia.Bonus_Ricerca_Attacco);
                if (report.Battaglia.Vittoria_Attaccante)
                {
                    txt_Vittoria.Text = $"Vittoria";
                    txt_Vittoria.ForeColor = Color.ForestGreen;
                }
            }

            lbl_Bonus_Testo_Attaccante.Text = lbl_Bonus_Testo_Difensore.Text =
                "[Unità] Salute:\n" +
                "[Unità] Difesa:\n" +
                "[Unità] Attacco:\n" +
                "[Villaggio] Salute:\n" +
                "[Villaggio] Difesa:\n" +
                "[Villaggio] Guarnigione:\n";
        }

        void Carica_Bonus(Label lbl, BonusRicerca bonus)
        {
            lbl.Text = $"{bonus.Bonus_Salute_Unità}\n" +
                $"{bonus.Bonus_Difesa_Unità}\n" +
                $"{bonus.Bonus_Salute_Strutture}\n" +
                $"{bonus.Bonus_Difesa_Strutture}\n" +
                $"{bonus.Bonus_Guarnigione_Strutture}\n";
        }

        void Load_Truppe(Battaglia.Report report)
        {
            Load_Header(report);
            UnitGroup truppe_Schierate_Difensore_Totale = new UnitGroup();
            UnitGroup truppe_Caduti_Difensore_Totale = new UnitGroup();
            UnitGroup truppe_Sopravvissuti_Difensore_Totale = new UnitGroup();

            UnitGroup truppe_Schierate_Attaccante_Totale = new UnitGroup();
            UnitGroup truppe_Caduti_Attaccante_Totale = new UnitGroup();
            UnitGroup truppe_Sopravvissuti_Attaccante_Totale = new UnitGroup();

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

            if (livello_Truppa == -1)
            {
                truppe_Schierate_Attaccante_Totale = report.Battaglia.Fasi[0].Fase_Distanza.Attaccante_Schierati;
                truppe_Sopravvissuti_Attaccante_Totale = report.Battaglia.Fasi[6].Attaccante.Sopravvisuti;
                foreach (var item in report.Battaglia.Fasi)
                    for (int i = 0; i < 4; i++)
                    {
                        truppe_Schierate_Difensore_Totale.Guerrieri[i] += item.Fase_Distanza.Difensore_Schierati.Guerrieri[i];
                        truppe_Schierate_Difensore_Totale.Lancieri[i] += item.Fase_Distanza.Difensore_Schierati.Lancieri[i];
                        truppe_Schierate_Difensore_Totale.Arcieri[i] += item.Fase_Distanza.Difensore_Schierati.Arcieri[i];
                        truppe_Schierate_Difensore_Totale.Catapulte[i] += item.Fase_Distanza.Difensore_Schierati.Catapulte[i];

                        truppe_Caduti_Difensore_Totale.Guerrieri[i] += item.Fase_Distanza.Difensore_Morti.Guerrieri[i] + item.Difensore.Perdite.Guerrieri[i];
                        truppe_Caduti_Difensore_Totale.Lancieri[i] += item.Fase_Distanza.Difensore_Morti.Lancieri[i] + item.Difensore.Perdite.Lancieri[i];
                        truppe_Caduti_Difensore_Totale.Arcieri[i] += item.Fase_Distanza.Difensore_Morti.Arcieri[i] + item.Difensore.Perdite.Arcieri[i];
                        truppe_Caduti_Difensore_Totale.Catapulte[i] += item.Fase_Distanza.Difensore_Morti.Catapulte[i] + item.Difensore.Perdite.Catapulte[i];

                        truppe_Sopravvissuti_Difensore_Totale.Guerrieri[i] += item.Difensore.Sopravvisuti.Guerrieri[i];
                        truppe_Sopravvissuti_Difensore_Totale.Lancieri[i] += item.Difensore.Sopravvisuti.Lancieri[i];
                        truppe_Sopravvissuti_Difensore_Totale.Arcieri[i] += item.Difensore.Sopravvisuti.Arcieri[i];
                        truppe_Sopravvissuti_Difensore_Totale.Catapulte[i] += item.Difensore.Sopravvisuti.Catapulte[i];


                        truppe_Caduti_Attaccante_Totale.Guerrieri[i] += item.Fase_Distanza.Attaccante_Morti.Guerrieri[i] + item.Attaccante.Perdite.Guerrieri[i];
                        truppe_Caduti_Attaccante_Totale.Lancieri[i] += item.Fase_Distanza.Attaccante_Morti.Lancieri[i] + item.Attaccante.Perdite.Lancieri[i];
                        truppe_Caduti_Attaccante_Totale.Arcieri[i] += item.Fase_Distanza.Attaccante_Morti.Arcieri[i] + item.Attaccante.Perdite.Arcieri[i];
                        truppe_Caduti_Attaccante_Totale.Catapulte[i] += item.Fase_Distanza.Attaccante_Morti.Catapulte[i] + item.Attaccante.Perdite.Catapulte[i];
                    }
                
            }   
            
            var fase = report.Battaglia.Fasi[faseIdx];
            int lv = livello_Truppa; // 0 = tutti i livelli sommati

            lbl_HP.Text = $"{report.Battaglia.Fasi[faseIdx].Struttura.Salute.ToString("N0", cultura)}/{report.Battaglia.Fasi[faseIdx].Struttura.SaluteMax.ToString("N0", cultura)}";
            lbl_DEF.Text = $"{report.Battaglia.Fasi[faseIdx].Struttura.Difesa.ToString("N0", cultura)}/{report.Battaglia.Fasi[faseIdx].Struttura.DifesaMax.ToString("N0", cultura)}";

            if (!attaccante)
            {
                lbl_Exp_Frecce_Fase_Testo_Attaccante.Text = $"Esperienza fase:\nFrecce usate:";
                lbl_Exp_Frecce_Fase_Valore_Attaccante.Text = $"{(report.Battaglia.Fasi[faseIdx].Xp_Attaccante + report.Battaglia.Fasi[faseIdx].Fase_Distanza.Attaccante_XP).ToString("N0", cultura)} XP\n" +
                $"{report.Battaglia.Fasi[faseIdx].Fase_Distanza.Attaccante_Frecce_Usate.ToString("N0", cultura)}";

                lbl_Exp_Frecce_Fase_Testo_Difensore.Text = $"Esperienza fase:\nFrecce usate:";
                lbl_Exp_Frecce_Fase_Valore_Difensore.Text = $"{(report.Battaglia.Fasi[faseIdx].Xp_Difensore + report.Battaglia.Fasi[faseIdx].Fase_Distanza.Difensore_XP).ToString("N0", cultura)} XP\n" +
                    $"{report.Battaglia.Fasi[faseIdx].Fase_Distanza.Difensore_Frecce_Usate.ToString("N0", cultura)}";

                if (tipo_Battaglia == "Corpo a corpo")
                {
                    Aggiungi_Righe(gridView_Truppe_Attaccante, fase.Attaccante.Schierati, fase.Attaccante.Perdite, fase.Attaccante.Sopravvisuti, lv);
                    Aggiungi_Righe(gridView_Truppe_Difensore, fase.Difensore.Schierati, fase.Difensore.Perdite, fase.Difensore.Sopravvisuti, lv);
                    if (livello_Truppa == -1)
                    {
                        Aggiungi_Righe(gridView_Truppe_Attaccante, truppe_Schierate_Attaccante_Totale, truppe_Caduti_Attaccante_Totale, truppe_Sopravvissuti_Attaccante_Totale, lv);
                        Aggiungi_Righe(gridView_Truppe_Difensore, truppe_Schierate_Difensore_Totale, truppe_Caduti_Difensore_Totale, truppe_Sopravvissuti_Difensore_Totale, lv);
                    }
                }
                else if (tipo_Battaglia == "Distanza")
                {
                    var fd = fase.Fase_Distanza;
                    Aggiungi_Righe(gridView_Truppe_Attaccante, fd.Attaccante_Schierati, fd.Attaccante_Morti, fd.Attaccante_Sopravvisuti, lv);
                    Aggiungi_Righe(gridView_Truppe_Difensore, fd.Difensore_Schierati, fd.Difensore_Morti, fd.Difensore_Sopravvisuti, lv);
                    if (livello_Truppa == -1)
                    {
                        Aggiungi_Righe(gridView_Truppe_Attaccante, truppe_Schierate_Difensore_Totale, truppe_Caduti_Difensore_Totale, truppe_Sopravvissuti_Difensore_Totale, lv);
                        Aggiungi_Righe(gridView_Truppe_Difensore, truppe_Schierate_Attaccante_Totale, truppe_Caduti_Attaccante_Totale, truppe_Sopravvissuti_Attaccante_Totale, lv);
                    }
                }
            }
            else
            {
                lbl_Exp_Frecce_Fase_Testo_Attaccante.Text = $"Esperienza fase:\nFrecce usate:";
                lbl_Exp_Frecce_Fase_Valore_Attaccante.Text = $"{(report.Battaglia.Fasi[faseIdx].Xp_Difensore + report.Battaglia.Fasi[faseIdx].Fase_Distanza.Difensore_XP).ToString("N0", cultura)} XP\n" +
                    $"{report.Battaglia.Fasi[faseIdx].Fase_Distanza.Difensore_Frecce_Usate.ToString("N0", cultura)}";

                lbl_Exp_Frecce_Fase_Testo_Difensore.Text = $"Esperienza fase:\nFrecce usate:";
                lbl_Exp_Frecce_Fase_Valore_Difensore.Text = $"{(report.Battaglia.Fasi[faseIdx].Xp_Attaccante + report.Battaglia.Fasi[faseIdx].Fase_Distanza.Attaccante_XP).ToString("N0", cultura)} XP\n" +
                    $"{report.Battaglia.Fasi[faseIdx].Fase_Distanza.Attaccante_Frecce_Usate.ToString("N0", cultura)}";

                if (tipo_Battaglia == "Corpo a corpo")
                {
                    Aggiungi_Righe(gridView_Truppe_Difensore, fase.Attaccante.Schierati, fase.Attaccante.Perdite, fase.Attaccante.Sopravvisuti, lv);
                    Aggiungi_Righe(gridView_Truppe_Attaccante, fase.Difensore.Schierati, fase.Difensore.Perdite, fase.Difensore.Sopravvisuti, lv);
                }
                else if (tipo_Battaglia == "Distanza")
                {
                    var fd = fase.Fase_Distanza;
                    Aggiungi_Righe(gridView_Truppe_Difensore, fd.Attaccante_Schierati, fd.Attaccante_Morti, fd.Attaccante_Sopravvisuti, lv);
                    Aggiungi_Righe(gridView_Truppe_Attaccante, fd.Difensore_Schierati, fd.Difensore_Morti, fd.Difensore_Sopravvisuti, lv);
                }
            }

        }
        void Load_Data(Report report)
        {
            attaccante = Variabili_Client.Utente.Username == report.Battaglia.Nome_Attaccante;
            Load_Truppe(report);
        }
        void ColoreSelezione()
        {
            lbl_Ingresso.ForeColor = Color.Yellow;
            lbl_Mura.ForeColor = Color.Yellow;
            lbl_Cancello.ForeColor = Color.Yellow;
            lbl_Torri.ForeColor = Color.Yellow;
            lbl_Centro.ForeColor = Color.Yellow;
            lbl_Castello.ForeColor = Color.Yellow;
            lbl_Player.ForeColor = Color.Yellow;
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
                    lbl_Player.ForeColor = Color.Turquoise;
                    break;
            }
            Load_Data(temp_Report);
        }
        async Task<bool> AnimaNumero(Label lbl, int da, int a, int durataMs = 1000, string prefisso = "", string suffisso = "")
        {
            var cultura = new CultureInfo("it-IT");
            int steps = 60;
            int delay = durataMs / steps;

            for (int i = 0; i <= steps; i++)
            {
                float t = (float)i / steps;
                // EaseOut: rallenta verso la fine
                float ease = 1f - (1f - t) * (1f - t);
                int valore = (int)(da + (a - da) * ease);

                lbl.Text = prefisso + valore.ToString("N0", cultura) + suffisso;
                await Task.Delay(delay);
            }

            lbl.Text = prefisso + a.ToString("N0", cultura) + suffisso; // assicura il valore finale esatto
            return true;
        }
        private void lbl_Livello_Truppe_1_MouseClick(object sender, MouseEventArgs e)
        {
            livello_Truppa = 0;
            Load_Data(temp_Report);
        }

        private void lbl_Livello_Truppe_2_MouseClick(object sender, MouseEventArgs e)
        {
            livello_Truppa = 1;
            Load_Data(temp_Report);
        }

        private void lbl_Livello_Truppe_3_MouseClick(object sender, MouseEventArgs e)
        {
            livello_Truppa = 2;
            Load_Data(temp_Report);
        }

        private void lbl_Livello_Truppe_4_MouseClick(object sender, MouseEventArgs e)
        {
            livello_Truppa = 3;
            Load_Data(temp_Report);
        }

        private void lbl_Livello_Truppe_5_MouseClick(object sender, MouseEventArgs e)
        {
            livello_Truppa = 4;
            Load_Data(temp_Report);
        }

        private void lbl_Corpo_Corpo_MouseClick(object sender, MouseEventArgs e)
        {
            tipo_Battaglia = "Corpo a corpo";
            lbl_Corpo_Corpo.ForeColor = Color.Turquoise;
            lbl_Distanza.ForeColor = Color.Yellow;
            Load_Data(temp_Report);
        }

        private void lbl_Distanza_MouseClick(object sender, MouseEventArgs e)
        {
            tipo_Battaglia = "Distanza";
            lbl_Corpo_Corpo.ForeColor = Color.Yellow;
            lbl_Distanza.ForeColor = Color.Turquoise;
            Load_Data(temp_Report);
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

        private void lbl_Player_MouseClick(object sender, MouseEventArgs e)
        {
            fase_Selezionata = "Giocatore";
            ColoreSelezione();
        }

        private void lbl_All_MouseClick(object sender, MouseEventArgs e)
        {
            livello_Truppa = -1;
            Load_Data(temp_Report);
        }
    }
}
