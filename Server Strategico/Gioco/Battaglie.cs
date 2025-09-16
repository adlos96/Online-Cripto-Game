using static Server_Strategico.Gioco.Giocatori;
using static Server_Strategico.Server.Server;


namespace Server_Strategico.Gioco
{
    internal class Battaglie
    {
        public static async Task<bool> Battaglia_Barbari(Player player, Guid clientGuid, string tipo)
        {
            await Battaglia_Distanza(tipo, player, clientGuid); //Pre battaglia, attaccano le unità a distanza ed i mezzi d'assedio

            int guerrieri   = player.Guerrieri[0];
            int picchieri   = player.Lanceri[0];
            int arcieri     = player.Arceri[0];
            int catapulte   = player.Catapulte[0];

            int guerrieri_Enemy = 0;
            int picchieri_Enemy = 0;
            int arcieri_Enemy = 0;
            int catapulte_Enemy = 0;

            guerrieri_Enemy = Giocatori.Barbari.PVP.Guerrieri;
            picchieri_Enemy = Giocatori.Barbari.PVP.Lancieri;
            arcieri_Enemy = Giocatori.Barbari.PVP.Arceri;
            catapulte_Enemy = Giocatori.Barbari.PVP.Catapulte;
            

            int tipi_Di_Unità = ContareTipiDiUnità(guerrieri, picchieri, arcieri, catapulte);
            int tipi_Di_Unità_Att = ContareTipiDiUnità(guerrieri_Enemy, picchieri_Enemy, arcieri_Enemy, catapulte_Enemy);

            // Calcolo del danno per il giocatore e il nemico
            double dannoInflittoDalNemico = CalcolareDanno_Invasore(arcieri_Enemy, catapulte_Enemy, guerrieri_Enemy, picchieri_Enemy, player) / tipi_Di_Unità;
            double dannoInflitto = CalcolareDanno_Giocatore(arcieri, catapulte, guerrieri, picchieri, player, clientGuid) / tipi_Di_Unità_Att;

            // Applicare il danno alle unità del giocatore
            int guerrieri_Temp  = RidurreNumeroSoldati(guerrieri, dannoInflittoDalNemico, (Esercito.Unità.Guerrieri_1.Difesa + Ricerca.Soldati.Incremento.Difesa * player.Guerriero_Difesa) * guerrieri, Esercito.Unità.Guerrieri_1.Salute + Ricerca.Soldati.Incremento.Salute * player.Guerriero_Salute);
            int picchieri_Temp  = RidurreNumeroSoldati(picchieri, dannoInflittoDalNemico, (Esercito.Unità.Lanceri_1.Difesa + Ricerca.Soldati.Incremento.Difesa * player.Lancere_Difesa) * picchieri, Esercito.Unità.Lanceri_1.Salute + Ricerca.Soldati.Incremento.Salute * player.Lancere_Salute);
            int arcieri_Temp    = RidurreNumeroSoldati(arcieri, dannoInflittoDalNemico * 0.70, (Esercito.Unità.Arceri_1.Difesa + Ricerca.Soldati.Incremento.Difesa * player.Arcere_Difesa) * arcieri, Esercito.Unità.Arceri_1.Salute + Ricerca.Soldati.Incremento.Salute * player.Arcere_Salute);
            int catapulte_Temp  = RidurreNumeroSoldati(catapulte, dannoInflittoDalNemico, (Esercito.Unità.Catapulte_1.Difesa + Ricerca.Soldati.Incremento.Difesa * player.Catapulta_Difesa) * catapulte, Esercito.Unità.Catapulte_1.Salute + Ricerca.Soldati.Incremento.Salute * player.Catapulta_Salute);

            // Applicare il danno alle unità nemiche
            int guerrieri_Enemy_Temp = RidurreNumeroSoldati(guerrieri_Enemy, dannoInflitto, Esercito.EsercitoNemico.Guerrieri_1.Difesa * guerrieri_Enemy, Esercito.EsercitoNemico.Guerrieri_1.Salute);
            int picchieri_Enemy_Temp = RidurreNumeroSoldati(picchieri_Enemy, dannoInflitto, Esercito.EsercitoNemico.Lanceri_1.Difesa * picchieri_Enemy, Esercito.EsercitoNemico.Lanceri_1.Salute);
            int arcieri_Enemy_Temp   = RidurreNumeroSoldati(arcieri_Enemy, dannoInflitto, Esercito.EsercitoNemico.Arceri_1.Difesa * arcieri_Enemy, Esercito.EsercitoNemico.Arceri_1.Salute);
            int catapulte_Enemy_Temp = RidurreNumeroSoldati(catapulte_Enemy, dannoInflitto, Esercito.EsercitoNemico.Catapulte_1.Difesa * catapulte_Enemy, Esercito.EsercitoNemico.Catapulte_1.Salute);

            int esperienza = (guerrieri_Enemy - guerrieri_Enemy_Temp) * Esercito.EsercitoNemico.Guerrieri_1.Esperienza +
                                 (picchieri_Enemy - picchieri_Enemy_Temp) * Esercito.EsercitoNemico.Lanceri_1.Esperienza +
                                 (arcieri_Enemy - arcieri_Enemy_Temp) * Esercito.EsercitoNemico.Arceri_1.Esperienza +
                                 (catapulte_Enemy - catapulte_Enemy_Temp) * Esercito.EsercitoNemico.Arceri_1.Esperienza;

            Send(clientGuid, $"Log_Server|Danno inflitto dal giocatore: {(dannoInflitto * tipi_Di_Unità_Att++).ToString("0.00")}\r\n");
            Send(clientGuid, $"Log_Server|Danno inflitto dal nemico: {(dannoInflittoDalNemico * tipi_Di_Unità++).ToString("0.00")}");
            Send(clientGuid, $"Log_Server|Guerrieri: {guerrieri - guerrieri_Temp}/{guerrieri}\r\n Lancieri: {picchieri - picchieri_Temp}/{picchieri}\r\n Arcieri: {arcieri - arcieri_Temp}/{arcieri}\r\n Catapulte: {catapulte - catapulte_Temp}/{catapulte}\r\n");
            Send(clientGuid, $"Log_Server|Soldati persi dal giocatore [{player.Username}]:");

            Send(clientGuid, $"Log_Server|Guerrieri: {guerrieri_Enemy - guerrieri_Enemy_Temp}/{guerrieri_Enemy}\r\n Lancieri: {picchieri_Enemy - picchieri_Enemy_Temp}/{picchieri_Enemy}\r\n Arcieri: {arcieri_Enemy - arcieri_Enemy_Temp}/{arcieri_Enemy}\r\n Catapulte: {catapulte_Enemy - catapulte_Enemy_Temp}/{catapulte_Enemy}\r\n");
            Send(clientGuid, $"Log_Server|Soldati persi dal nemico:");
            Send(clientGuid, $"Log_Server|Battaglia PVE Completata\r\n");

            player.Esperienza += esperienza;

            Console.WriteLine($"Danno inflitto dal nemico: {(dannoInflittoDalNemico * tipi_Di_Unità++).ToString("0.00")}");
            Console.WriteLine($"Danno inflitto dal giocatore: {(dannoInflitto * tipi_Di_Unità_Att++).ToString("0.00")}");

            Console.WriteLine($"Guerrieri: {guerrieri - guerrieri_Temp}\r\n Lancieri: {picchieri - picchieri_Temp}\r\n Arcieri: {arcieri - arcieri_Temp}\r\n Catapulte: {catapulte - catapulte_Temp}");
            Console.WriteLine($"Soldati persi dal giocatore:");
            
            Console.WriteLine($"Guerrieri: {guerrieri_Enemy - guerrieri_Enemy_Temp} \r\n Lancieri:  {picchieri_Enemy - picchieri_Enemy_Temp} \r\n Arcieri:  {arcieri_Enemy - arcieri_Enemy_Temp} \r\n Catapulte:  {catapulte_Enemy - catapulte_Enemy_Temp}\r\n");
            Console.WriteLine($"Soldati persi dal nemico:");
            Console.WriteLine($"Battaglia PVE Completata");

            // Aggiornare le quantità delle unità
            player.Guerrieri[0] = guerrieri_Temp;
            player.Lanceri[0] = picchieri_Temp;
            player.Arceri[0] = arcieri_Temp;
            player.Catapulte[0] = catapulte_Temp;

            Giocatori.Barbari.PVP.Guerrieri = guerrieri_Enemy_Temp;
            Giocatori.Barbari.PVP.Lancieri = picchieri_Enemy_Temp;
            Giocatori.Barbari.PVP.Arceri = arcieri_Enemy_Temp;
            Giocatori.Barbari.PVP.Catapulte = catapulte_Enemy_Temp;
            
            return false;
        }
        public static async Task<bool> Battaglia_PVP(Player player, Guid clientGuid, Player player2, Guid clientGuid2)
        {
            await Battaglia_Distanza(player, clientGuid, player2, clientGuid2); //Pre battaglia, attaccano le unità a distanza ed i mezzi d'assedio

            int guerrieri = player.Guerrieri[0];                   //Giocatore attaccante
            int picchieri = player.Lanceri[0];                    //Giocatore attaccante
            int arcieri = player.Arceri[0];                        //Giocatore attaccante
            int catapulte = player.Catapulte[0];                   //Giocatore attaccante

            int guerrieri_Enemy = player2.Guerrieri[0];     //GIcoatore in difesa
            int picchieri_Enemy = player2.Lanceri[0];      //GIcoatore in difesa
            int arcieri_Enemy = player2.Arceri[0];          //GIcoatore in difesa
            int catapulte_Enemy = player2.Catapulte[0];     //GIcoatore in difesa

            int tipi_Di_Unità = ContareTipiDiUnità(guerrieri, picchieri, arcieri, catapulte);
            int tipi_Di_Unità_Att = ContareTipiDiUnità(guerrieri_Enemy, picchieri_Enemy, arcieri_Enemy, catapulte_Enemy);

            // Calcolo del danno per il giocatore e il nemico
            double dannoInflittoDalNemico = CalcolareDanno_Giocatore(arcieri_Enemy, catapulte_Enemy, guerrieri_Enemy, picchieri_Enemy, player, clientGuid2) / tipi_Di_Unità; //Difensore
            double dannoInflitto = CalcolareDanno_Giocatore(arcieri, catapulte, guerrieri, picchieri, player, clientGuid) / tipi_Di_Unità_Att; //Attaccante

            // Applicare il danno alle unità del giocatore
            int guerrieri_Temp = RidurreNumeroSoldati(guerrieri, dannoInflittoDalNemico, Esercito.Unità.Guerrieri_1.Difesa + Ricerca.Soldati.Incremento.Difesa * player.Guerriero_Difesa * guerrieri, Esercito.Unità.Guerrieri_1.Salute + Ricerca.Soldati.Incremento.Salute * player.Guerriero_Salute);
            int picchieri_Temp = RidurreNumeroSoldati(picchieri, dannoInflittoDalNemico, Esercito.Unità.Lanceri_1.Difesa + Ricerca.Soldati.Incremento.Difesa * player.Lancere_Difesa * picchieri, Esercito.Unità.Lanceri_1.Salute + Ricerca.Soldati.Incremento.Salute * player.Lancere_Salute);
            int arcieri_Temp = RidurreNumeroSoldati(arcieri, dannoInflittoDalNemico * 0.70, Esercito.Unità.Arceri_1.Difesa + Ricerca.Soldati.Incremento.Difesa * player.Arcere_Difesa * arcieri, Esercito.Unità.Arceri_1.Salute + Ricerca.Soldati.Incremento.Salute * player.Arcere_Salute);
            int catapulte_Temp = RidurreNumeroSoldati(catapulte, dannoInflittoDalNemico, Esercito.Unità.Catapulte_1.Difesa + Ricerca.Soldati.Incremento.Difesa * player.Catapulta_Difesa * catapulte, Esercito.Unità.Catapulte_1.Salute + Ricerca.Soldati.Incremento.Salute * player.Catapulta_Salute);

            // Applicare il danno alle unità nemiche
            int guerrieri_Enemy_Temp = RidurreNumeroSoldati(guerrieri_Enemy, dannoInflitto, Esercito.Unità.Guerrieri_1.Difesa + Ricerca.Soldati.Incremento.Difesa * player2.Guerriero_Difesa * guerrieri_Enemy, Esercito.Unità.Guerrieri_1.Salute + Ricerca.Soldati.Incremento.Salute * player2.Guerriero_Salute);
            int picchieri_Enemy_Temp = RidurreNumeroSoldati(picchieri_Enemy, dannoInflitto, Esercito.Unità.Lanceri_1.Difesa + Ricerca.Soldati.Incremento.Difesa * player2.Lancere_Difesa * picchieri_Enemy, Esercito.Unità.Lanceri_1.Salute + Ricerca.Soldati.Incremento.Salute * player2.Lancere_Salute);
            int arcieri_Enemy_Temp = RidurreNumeroSoldati(arcieri_Enemy, dannoInflitto * 0.70, Esercito.Unità.Arceri_1.Difesa + Ricerca.Soldati.Incremento.Difesa * player2.Arcere_Difesa * arcieri_Enemy, Esercito.Unità.Arceri_1.Salute + Ricerca.Soldati.Incremento.Salute * player2.Arcere_Salute);
            int catapulte_Enemy_Temp = RidurreNumeroSoldati(catapulte_Enemy, dannoInflitto, Esercito.Unità.Catapulte_1.Difesa + Ricerca.Soldati.Incremento.Difesa * player2.Catapulta_Difesa * catapulte_Enemy, Esercito.Unità.Catapulte_1.Salute + Ricerca.Soldati.Incremento.Salute * player2.Catapulta_Salute);

            Send(clientGuid, $"Log_Server|Danno inflitto dal giocatore [{player.Username}]: {(dannoInflitto * tipi_Di_Unità_Att++).ToString("0.00")}\r\n");
            Send(clientGuid, $"Log_Server|Danno inflitto dal giocatore [{player2.Username}]: {(dannoInflittoDalNemico * tipi_Di_Unità++).ToString("0.00")}");

            Send(clientGuid, $"Log_Server|Guerrieri: {guerrieri - guerrieri_Temp}\r\n Lancieri: {picchieri - picchieri_Temp}\r\n Arcieri: {arcieri - arcieri_Temp}\r\n Catapulte: {catapulte - catapulte_Temp}\r\n");
            Send(clientGuid, $"Log_Server|Soldati persi dal giocatore [{player.Username}]:");

            Send(clientGuid, $"Log_Server|Guerrieri: {guerrieri_Enemy - guerrieri_Enemy_Temp}\r\n Lancieri: {picchieri_Enemy - picchieri_Enemy_Temp}\r\n Arcieri: {arcieri_Enemy - arcieri_Enemy_Temp}\r\n Catapulte: {catapulte_Enemy - catapulte_Enemy_Temp}\r\n");
            Send(clientGuid, $"Log_Server|Soldati persi dal giocatore [{player2.Username}]:");
            Send(clientGuid, $"Log_Server|Battaglia PVP Completata\r\n");

            Send(clientGuid2, $"Log_Server|Danno inflitto dal giocatore [{player.Username}]: {(dannoInflitto * tipi_Di_Unità_Att++).ToString("0.00")}\r\n");
            Send(clientGuid2, $"Log_Server|Danno inflitto dal giocatore [{player2.Username}]: {(dannoInflittoDalNemico * tipi_Di_Unità++).ToString("0.00")}");

            Send(clientGuid2, $"Log_Server|Guerrieri: {guerrieri - guerrieri_Temp}\r\n Lancieri: {picchieri - picchieri_Temp}\r\n Arcieri: {arcieri - arcieri_Temp}\r\n Catapulte: {catapulte - catapulte_Temp}\r\n");
            Send(clientGuid2, $"Log_Server|Soldati persi dal giocatore [{player.Username}]:");

            int esperienza1 = (guerrieri_Enemy - guerrieri_Enemy_Temp) * Esercito.Unità.Guerrieri_1.Esperienza +
                                 (picchieri_Enemy - picchieri_Enemy_Temp) * Esercito.Unità.Lanceri_1.Esperienza +
                                 (arcieri_Enemy - arcieri_Enemy_Temp) * Esercito.Unità.Arceri_1.Esperienza +
                                 (catapulte_Enemy - catapulte_Enemy_Temp) * Esercito.Unità.Catapulte_1.Esperienza;

            int esperienza2 = (guerrieri - guerrieri_Temp) * Esercito.Unità.Guerrieri_1.Esperienza +
                                  (picchieri - picchieri_Temp) * Esercito.Unità.Lanceri_1.Esperienza +
                                  (arcieri - arcieri_Temp) * Esercito.Unità.Arceri_1.Esperienza +
                                  (catapulte - catapulte_Temp) * Esercito.Unità.Catapulte_1.Esperienza;

            player.Esperienza += esperienza1;
            player2.Esperienza += esperienza2;

            Send(clientGuid, $"Log_Server|Esperienza ottenuta: [{esperienza1}] exp");
            Send(clientGuid2, $"Log_Server|Esperienza ottenuta: [{esperienza2}] exp");

            Send(clientGuid2, $"Log_Server|Guerrieri: {guerrieri_Enemy - guerrieri_Enemy_Temp}\r\n Lancieri: {picchieri_Enemy - picchieri_Enemy_Temp}\r\n Arcieri: {arcieri_Enemy - arcieri_Enemy_Temp}\r\n Catapulte: {catapulte_Enemy - catapulte_Enemy_Temp}\r\n");
            Send(clientGuid2, $"Log_Server|Soldati persi dal giocatore [{player2.Username}]:");
            Send(clientGuid2, $"Log_Server|Battaglia PVP Completata\r\n");

            Console.WriteLine($"Danno inflitto dal giocatore [{player.Username}]: {(dannoInflitto * tipi_Di_Unità_Att++).ToString("0.00")}");
            Console.WriteLine($"Danno inflitto dal giocatore [{player2.Username}]: {(dannoInflittoDalNemico * tipi_Di_Unità++).ToString("0.00")}");

            Console.WriteLine($"Soldati persi dal giocatore [{player.Username}]:");
            Console.WriteLine($"Guerrieri: {guerrieri - guerrieri_Temp}\r\n Lancieri: {picchieri - picchieri_Temp}\r\n Arcieri: {arcieri - arcieri_Temp}\r\n Catapulte: {catapulte - catapulte_Temp}");

            Console.WriteLine($"Soldati persi dal giocatore [{player2.Username}]:");
            Console.WriteLine($"Guerrieri: {guerrieri_Enemy - guerrieri_Enemy_Temp}\r\n Lancieri: {picchieri_Enemy - picchieri_Enemy_Temp}\r\n Arcieri: {arcieri_Enemy - arcieri_Enemy_Temp}\r\n Catapulte: {catapulte_Enemy - catapulte_Enemy_Temp}");
            Console.WriteLine($"Battaglia PVP Completata");

            // Aggiornare le quantità delle unità
            player.Guerrieri[0] = guerrieri_Temp;
            player.Lanceri[0] = picchieri_Temp;
            player.Arceri[0] = arcieri_Temp;
            player.Catapulte[0] = catapulte_Temp;

            player2.Guerrieri[0] = guerrieri_Enemy_Temp;
            player2.Lanceri[0] = picchieri_Enemy_Temp;
            player2.Arceri[0] = arcieri_Enemy_Temp;
            player2.Catapulte[0] = catapulte_Enemy_Temp;
            return false;
        }

        public static double CalcolareDanno_Giocatore(int arcieri, int catapulte, int guerrieri, int picchieri, Player player, Guid guid)
        {
            double dannoArcieri = 0;  // supponiamo che ogni arciere infligga 5 danni
            double dannoCatapulte = 0;  // supponiamo che ogni catapulta infligga 15 danni
            if (player.Frecce < arcieri * Esercito.Unità.Arceri_1.Componente_Lancio + catapulte * Esercito.Unità.Catapulte_1.Componente_Lancio)
            {
                Send(guid, $"Log_Server|Gli arceri e le catapulte del giocatore subiscono una riduzione del danno per mancanza di frecce [{arcieri * Esercito.Unità.Arceri_1.Componente_Lancio + catapulte * Esercito.Unità.Catapulte_1.Componente_Lancio}/{player.Frecce}]:");
                dannoArcieri = 0.33 * arcieri * (Esercito.Unità.Arceri_1.Attacco + Ricerca.Soldati.Incremento.Attacco * player.Arcere_Attacco);  // supponiamo che ogni arciere infligga 5 danni
                dannoCatapulte = 0.33 * catapulte * (Esercito.Unità.Catapulte_1.Attacco + Ricerca.Soldati.Incremento.Attacco * player.Catapulta_Attacco);  // supponiamo che ogni catapulta infligga 15 danni
                player.Frecce = 0;
            }
            else
            {
                player.Frecce -= arcieri * Esercito.Unità.Arceri_1.Componente_Lancio + catapulte * Esercito.Unità.Catapulte_1.Componente_Lancio;
                dannoArcieri = arcieri * (Esercito.Unità.Arceri_1.Attacco + Ricerca.Soldati.Incremento.Attacco * player.Arcere_Attacco);  // supponiamo che ogni arciere infligga 5 danni
                dannoCatapulte = catapulte * (Esercito.Unità.Catapulte_1.Attacco + Ricerca.Soldati.Incremento.Attacco * player.Catapulta_Attacco);  // supponiamo che ogni catapulta infligga 15 danni
                Send(guid, $"Log_Server|Frecce utilizzate: {arcieri * Esercito.Unità.Arceri_1.Componente_Lancio + catapulte * Esercito.Unità.Catapulte_1.Componente_Lancio}/{player.Frecce}");
            }

            // Esempio di calcolo del danno combinato, può essere esteso con logiche più complesse
            double dannoGuerrieri = guerrieri * (Esercito.Unità.Guerrieri_1.Attacco + Ricerca.Soldati.Incremento.Attacco * player.Guerriero_Attacco);  // supponiamo che ogni Guerrieri_1 infligga 10 danni
            double dannoPicchieri = picchieri * (Esercito.Unità.Lanceri_1.Attacco + Ricerca.Soldati.Incremento.Attacco * player.Lancere_Attacco);  // supponiamo che ogni picchiere infligga 8 danni

            return dannoArcieri + dannoCatapulte + dannoGuerrieri + dannoPicchieri;
        }
        public static double CalcolareDanno_Invasore(int arcieri, int catapulte, int guerrieri, int picchieri, Player player)
        {
            // Esempio di calcolo del danno combinato, può essere esteso con logiche più complesse
            double dannoArcieri = arcieri * (Esercito.EsercitoNemico.Arceri_1.Attacco);  // supponiamo che ogni arciere infligga 5 danni
            double dannoCatapulte = catapulte * (Esercito.EsercitoNemico.Catapulte_1.Attacco);  // supponiamo che ogni catapulta infligga 15 danni
            double dannoGuerrieri = guerrieri * (Esercito.EsercitoNemico.Guerrieri_1.Attacco);  // supponiamo che ogni Guerrieri_1 infligga 10 danni
            double dannoPicchieri = picchieri * (Esercito.EsercitoNemico.Lanceri_1.Attacco);  // supponiamo che ogni picchiere infligga 8 danni

            return dannoArcieri + dannoCatapulte + dannoGuerrieri + dannoPicchieri;
        }
        public static int RidurreNumeroSoldati(int numeroSoldati, double danno, double difesa, double salutePerSoldato)
        {
            // Calcolare il danno effettivo tenendo conto della difesa
            double dannoEffettivo = danno - difesa;
            dannoEffettivo = Math.Max(0, dannoEffettivo); // Assicurarsi che il danno non sia negativo

            int soldatiPersi = Convert.ToInt32(dannoEffettivo / salutePerSoldato);
            numeroSoldati -= soldatiPersi;
            return numeroSoldati < 0 ? 0 : numeroSoldati;
        }
        public static int RidurreNumeroSoldati_OLD(int numeroSoldati, double danno, double difesa, double salutePerSoldato)
        {
            // Calcolare il danno effettivo tenendo conto della difesa
            double dannoEffettivo = Math.Max(0, danno - difesa);
            dannoEffettivo = Math.Max(0, dannoEffettivo); // Assicurarsi che il danno non sia negativo

            // Aggiungere una variazione casuale per simulare l'imprevedibilità della battaglia
            Random random = new Random();
            double variationFactor = 1 + (random.NextDouble() * 0.2 - 0.1); // ±10% di variazione
            dannoEffettivo *= variationFactor;

            int soldatiPersi = (int)Math.Ceiling(dannoEffettivo / salutePerSoldato); // Calcolare i soldati persi con una logica più sofisticata
            soldatiPersi = Math.Min(soldatiPersi, numeroSoldati); // Assicurarsi che il numero di soldati persi non superi il numero totale di soldati

            return soldatiPersi;
        }
        public static int ContareTipiDiUnità(int guerrieri, int picchieri, int arcieri, int catapulte)
        {
            int tipiDiUnità = 0;

            if (guerrieri > 0) tipiDiUnità++;
            if (picchieri > 0) tipiDiUnità++;
            if (arcieri > 0) tipiDiUnità++;
            if (catapulte > 0) tipiDiUnità++;

            // Se non ci sono unità, forza a 1 per evitare divisioni per zero
            return tipiDiUnità == 0 ? 1 : tipiDiUnità;
        }
        public static async Task<bool> Battaglia_Distanza(string struttura, Player player, Guid clientGuid)
        {
            int guerrieri_Morti = 0, lancieri_Morti = 0, guerrieri_Morti_Att = 0, lancieri_Morti_Att = 0;
            int guerrieri_Enemy = 0, picchieri_Enemy = 0, arcieri_Enemy = 0, catapulte_Enemy = 0;

            int guerrieri = player.Guerrieri[0];
            int picchieri = player.Lanceri[0];
            int arcieri = player.Arceri[0];
            int catapulte = player.Catapulte[0];

            guerrieri_Enemy = Giocatori.Barbari.PVP.Guerrieri;
            picchieri_Enemy = Giocatori.Barbari.PVP.Lancieri;
            arcieri_Enemy = Giocatori.Barbari.PVP.Arceri;
            catapulte_Enemy = Giocatori.Barbari.PVP.Catapulte;

            int arcieri_Temp = arcieri * 2 / 3;
            int catapulte_Temp = catapulte * 2 / 3;

            int arcieri_Enemy_Temp = arcieri_Enemy * 2 / 3;
            int catapulte_Enemy_Temp = catapulte_Enemy * 2 / 3;

            if (player.Frecce < arcieri * Esercito.Unità.Arceri_1.Componente_Lancio + catapulte * Esercito.Unità.Catapulte_1.Componente_Lancio)
            {
                Send(clientGuid, $"Log_Server|Gli arceri e le catapulte del giocatore subiscono una riduzione del danno per mancanza di frecce [{arcieri * Esercito.Unità.Arceri_1.Componente_Lancio + catapulte * Esercito.Unità.Catapulte_1.Componente_Lancio}/{player.Frecce}]:");
                arcieri_Temp = arcieri_Temp / 3;
                catapulte_Temp = catapulte_Temp / 3;
                player.Frecce = 0;
            }else
                player.Frecce -= arcieri * Esercito.Unità.Arceri_1.Componente_Lancio + catapulte * Esercito.Unità.Catapulte_1.Componente_Lancio;
            

            #region Se poche unità a distanza e d'assedio
            if (arcieri <= 10) arcieri_Temp = arcieri * 2;
            if (catapulte <= 5) catapulte_Temp = catapulte * 6;
            if (arcieri == 0) arcieri_Temp = 0;
            if (catapulte == 0) catapulte_Temp = 0;

            if (arcieri_Enemy <= 10) arcieri_Enemy_Temp = arcieri_Enemy * 2;
            if (catapulte_Enemy <= 5) catapulte_Enemy_Temp = catapulte_Enemy * 6;
            if (arcieri_Enemy == 0) arcieri_Enemy_Temp = 0;
            if (catapulte_Enemy == 0) catapulte_Enemy_Temp = 0;
            #endregion

            if (arcieri > 0 || catapulte > 0)
            {
                int attacco = (catapulte_Temp + arcieri_Temp) * 4 / 5;
                if (guerrieri_Enemy > 0 && picchieri_Enemy > 0)
                {
                    if (guerrieri_Enemy > picchieri_Enemy)
                    {
                        guerrieri_Morti_Att = attacco * 2 / 3; //Danno 2/3 contro guerrieri
                        lancieri_Morti_Att = attacco / 3; //Danno 1/3 contro lancieri
                    }
                    else
                    {
                        guerrieri_Morti_Att = attacco / 3; //Danno 2/3 contro guerrieri
                        lancieri_Morti_Att = attacco * 2 / 3; //Danno 1/3 contro lancieri
                    }
                }   
                else if (guerrieri_Enemy > 0)
                    guerrieri_Morti_Att = attacco * 4 / 5;
                
                else if (picchieri_Enemy > 0)
                    lancieri_Morti_Att = attacco * 4 / 5;

                if (guerrieri_Morti_Att > guerrieri_Enemy)
                {
                    guerrieri_Morti_Att = guerrieri_Enemy;
                    guerrieri_Enemy = 0;
                }
                else
                    guerrieri_Enemy -= guerrieri_Morti_Att;

                if (lancieri_Morti_Att > picchieri_Enemy)
                {
                    lancieri_Morti_Att = picchieri_Enemy;
                    picchieri_Enemy = 0;
                }
                else
                    picchieri_Enemy -= lancieri_Morti_Att;

                int esperienza = guerrieri_Morti_Att * Esercito.EsercitoNemico.Guerrieri_1.Esperienza + lancieri_Morti_Att * Esercito.EsercitoNemico.Lanceri_1.Esperienza;

                Console.WriteLine($"({struttura}) Gli arceri e le catapulte del giocatore hanno causato:");
                Console.WriteLine($"({struttura}) Guerrieri morti: {guerrieri_Morti_Att} Lancieri morti: {lancieri_Morti_Att}\r\n");

                player.Esperienza += esperienza;
            }

            if (arcieri_Enemy > 0 || catapulte_Enemy > 0)
            {
                int attacco = (catapulte_Enemy_Temp + arcieri_Enemy_Temp) * 4 / 5;
                if (guerrieri > 0 && picchieri > 0)
                {
                    if (guerrieri > picchieri)
                    {
                        guerrieri_Morti = attacco * 2 / 3;
                        lancieri_Morti = attacco / 3;
                    }
                    else
                    {
                        guerrieri_Morti = attacco / 3;
                        lancieri_Morti = attacco * 2 / 3;
                    }
                }
                else if (guerrieri > 0) guerrieri_Morti = attacco * 4 / 5;
                else if (picchieri > 0) lancieri_Morti = attacco * 4 / 5;

                if (guerrieri_Morti > guerrieri)
                {
                    guerrieri_Morti = guerrieri;
                    guerrieri = 0;
                }
                else guerrieri -= guerrieri_Morti;

                if (lancieri_Morti > picchieri)
                {
                    lancieri_Morti = picchieri;
                    picchieri = 0;
                }
                else picchieri -= lancieri_Morti;

                Send(clientGuid, $"Log_Server|Guerrieri morti: {guerrieri_Morti}/{player.Guerrieri} Lancieri morti:  {lancieri_Morti}/{player.Lanceri}\r\n");
                Send(clientGuid, $"Log_Server|Gli arceri e le catapulte barbare hanno causato:");

                Console.WriteLine($"({struttura})Gli arceri e le catapulte barbare hanno causato:");
                Console.WriteLine($"({struttura})Guerrieri morti: {guerrieri_Morti}/{player.Guerrieri} Lancieri morti:  {lancieri_Morti}/{player.Lanceri}");

                player.Guerrieri[0] = guerrieri;
                player.Lanceri[0] = picchieri;
            }
            return true;
        } // Arcieri e Mezzi d'assedio attaccano prima della battaglia (giocatore-barbari)
        public static async Task<bool> Battaglia_Distanza(Player player, Guid clientGuid, Player player2, Guid clientGuid2)
        {
            int guerrieri_Morti = 0, lancieri_Morti = 0, guerrieri_Morti_Att = 0, lancieri_Morti_Att = 0;

            int guerrieri = player.Guerrieri[0];
            int picchieri = player.Lanceri[0];
            int arcieri = player.Arceri[0];
            int catapulte = player.Catapulte[0];

            int guerrieri_Enemy = player2.Guerrieri[0];
            int picchieri_Enemy = player2.Lanceri[0];
            int arcieri_Enemy = player2.Arceri[0];
            int catapulte_Enemy = player2.Catapulte[0];

            int arcieri_Temp = arcieri * 2 / 3;
            int catapulte_Temp = catapulte * 2 / 3;
            int arcieri_Enemy_Temp = arcieri_Enemy * 2 / 3;
            int catapulte_Enemy_Temp = catapulte_Enemy * 2 / 3;

            if (player.Frecce < arcieri * Esercito.Unità.Arceri_1.Componente_Lancio + catapulte * Esercito.Unità.Catapulte_1.Componente_Lancio)
            {
                Send(clientGuid, $"Log_Server|Gli arceri e le catapulte del giocatore [{player.Username}] subiscono una riduzione del danno per mancanza di frecce [{arcieri * Esercito.Unità.Arceri_1.Componente_Lancio + catapulte * Esercito.Unità.Catapulte_1.Componente_Lancio}/{player.Frecce}]:");
                arcieri_Temp = arcieri_Temp / 3;
                catapulte_Temp = catapulte_Temp / 3;
                player.Frecce = 0;
            }
            else
                player.Frecce -= arcieri * Esercito.Unità.Arceri_1.Componente_Lancio + catapulte * Esercito.Unità.Catapulte_1.Componente_Lancio;

            if (player2.Frecce < arcieri_Enemy * Esercito.Unità.Arceri_1.Componente_Lancio + catapulte_Enemy * Esercito.Unità.Catapulte_1.Componente_Lancio)
            {
                Send(clientGuid, $"Log_Server|Gli arceri e le catapulte del giocatore [{player2.Username}] subiscono una riduzione del danno per mancanza di frecce [{arcieri_Enemy * Esercito.Unità.Arceri_1.Componente_Lancio + catapulte_Enemy * Esercito.Unità.Catapulte_1.Componente_Lancio}/{player.Frecce}]:");
                arcieri_Enemy_Temp = arcieri_Enemy_Temp / 3;
                catapulte_Enemy_Temp = catapulte_Enemy_Temp / 3;
                player2.Frecce = 0;
            }
            else
                player2.Frecce -= arcieri_Enemy * Esercito.Unità.Arceri_1.Componente_Lancio + catapulte_Enemy * Esercito.Unità.Catapulte_1.Componente_Lancio;

            #region Se poche unità a distanza e d'assedio
            if (arcieri <= 10) arcieri_Temp = arcieri * 2;
            if (catapulte <= 5) catapulte_Temp = catapulte * 6;
            if (arcieri == 0) arcieri_Temp = 0;
            if (catapulte == 0) catapulte_Temp = 0;

            if (arcieri_Enemy <= 10) arcieri_Enemy_Temp = arcieri_Enemy * 2;
            if (catapulte_Enemy <= 5) catapulte_Enemy_Temp = catapulte_Enemy * 6;
            if (arcieri_Enemy == 0) arcieri_Enemy_Temp = 0;
            if (catapulte_Enemy == 0) catapulte_Enemy_Temp = 0;
            #endregion

            if (arcieri > 0 || catapulte > 0)
            {
                int attacco = (catapulte_Temp + arcieri_Temp) * 4 / 5;
                if (guerrieri_Enemy > 0 && picchieri_Enemy > 0)
                {
                    if (guerrieri_Enemy > picchieri_Enemy)
                    {
                        guerrieri_Morti_Att = attacco * 2 / 3; //Danno 2/3 contro guerrieri
                        lancieri_Morti_Att = attacco / 3; //Danno 1/3 contro lancieri
                    } else
                    {
                        guerrieri_Morti_Att = attacco / 3; //Danno 2/3 contro guerrieri
                        lancieri_Morti_Att = attacco * 2 / 3; //Danno 1/3 contro lancieri
                    }
                }
                else if (guerrieri_Enemy > 0) guerrieri_Morti_Att = attacco * 4 / 5;
                else if (picchieri_Enemy > 0) lancieri_Morti_Att = attacco * 4 / 5;

                if (guerrieri_Morti_Att > guerrieri_Enemy)
                {
                    guerrieri_Morti_Att = guerrieri_Enemy;
                    guerrieri_Enemy = 0;
                }
                else
                    guerrieri_Enemy -= guerrieri_Morti_Att;

                if (lancieri_Morti_Att > picchieri_Enemy)
                {
                    lancieri_Morti_Att = picchieri_Enemy;
                    picchieri_Enemy = 0;
                }
                else
                    picchieri_Enemy -= lancieri_Morti_Att;

                player.Esperienza += guerrieri_Morti_Att * Esercito.Unità.Guerrieri_1.Esperienza + lancieri_Morti_Att * Esercito.Unità.Lanceri_1.Esperienza;
                int esperienza2 = guerrieri_Morti_Att * Esercito.Unità.Guerrieri_1.Esperienza + lancieri_Morti_Att * Esercito.Unità.Lanceri_1.Esperienza;

                Send(clientGuid2, $"Log_Server|Guerrieri morti: {guerrieri_Morti}/{player2.Guerrieri} Lancieri morti:  {lancieri_Morti}/{player2.Lanceri}\r\n Esperienza:  {esperienza2}\r\n");
                Send(clientGuid2, $"Log_Server|Gli arceri e le catapulte del giocatore [{player2.Username}] hanno causato:");
                Send(clientGuid2, $"Log_Server|Frecce utilizzate: {arcieri_Enemy * Esercito.Unità.Arceri_1.Componente_Lancio + catapulte_Enemy * Esercito.Unità.Catapulte_1.Componente_Lancio}");

                player2.Guerrieri[0] = guerrieri_Enemy;
                player2.Lanceri[0] = picchieri_Enemy;

            }
            Console.WriteLine($"Gli arceri e le catapulte del giocatore [{player.Username}] hanno causato:");
            Console.WriteLine($"Guerrieri morti: {guerrieri_Morti_Att} Lancieri morti:  {lancieri_Morti_Att}");

            Send(clientGuid, $"Log_Server|Guerrieri morti: {guerrieri_Morti_Att} Lancieri morti:  {lancieri_Morti_Att}\r\n");
            Send(clientGuid, $"Log_Server|Gli arceri e le catapulte del giocatore [{player.Username}] hanno causato:");
            Send(clientGuid2, $"Log_Server|Guerrieri morti: {guerrieri_Morti_Att} Lancieri morti:  {lancieri_Morti_Att}\r\n");
            Send(clientGuid2, $"Log_Server|Gli arceri e le catapulte del giocatore [{player.Username}] hanno causato:");

            if (arcieri_Enemy > 0 || catapulte_Enemy > 0)
            {
                int attacco = (catapulte_Enemy_Temp + arcieri_Enemy_Temp) * 4 / 5;
                if (guerrieri > 0 && picchieri > 0)
                {
                    if (guerrieri > picchieri)
                    {
                        guerrieri_Morti = attacco * 2 / 3;
                        lancieri_Morti = attacco / 3;
                    } else
                    {
                        guerrieri_Morti = attacco / 3;
                        lancieri_Morti = attacco * 2 / 3;
                    }
                }
                else if (guerrieri > 0) guerrieri_Morti = attacco * 4 / 5;
                else if (picchieri > 0) lancieri_Morti = attacco * 4 / 5;

                if (guerrieri_Morti > guerrieri)
                {
                    guerrieri_Morti = guerrieri;
                    guerrieri = 0;
                }
                else guerrieri -= guerrieri_Morti;

                if (lancieri_Morti > picchieri)
                {
                    lancieri_Morti = picchieri;
                    picchieri = 0;
                }
                else picchieri -= lancieri_Morti;

                player2.Esperienza += guerrieri_Morti * Esercito.Unità.Guerrieri_1.Esperienza + lancieri_Morti * Esercito.Unità.Lanceri_1.Esperienza;
                int esperienza1 = guerrieri_Morti * Esercito.Unità.Guerrieri_1.Esperienza + lancieri_Morti * Esercito.Unità.Lanceri_1.Esperienza;

                Send(clientGuid, $"Log_Server|Guerrieri morti: {guerrieri_Morti}/{player.Guerrieri} Lancieri morti:  {lancieri_Morti}/{player.Lanceri}\r\n Esperienza:  {esperienza1}\r\n");
                Send(clientGuid, $"Log_Server|Gli arceri e le catapulte del giocatore [{player2.Username}] hanno causato:");
                Send(clientGuid, $"Log_Server|Frecce utilizzate: {arcieri * Esercito.Unità.Arceri_1.Componente_Lancio + catapulte * Esercito.Unità.Catapulte_1.Componente_Lancio}");

                player.Guerrieri[0] = guerrieri;
                player.Lanceri[0] = picchieri;
            }

            Console.WriteLine($"Gli arceri e le catapulte del giocatore [{player.Username}] hanno causato:");
            Console.WriteLine($"Guerrieri morti: {guerrieri_Morti} Lancieri morti:  {lancieri_Morti}");
            return true;
        } // Arcieri e Mezzi d'assedio attaccano prima della battaglia (giocatore-giocatore)
    }
}
