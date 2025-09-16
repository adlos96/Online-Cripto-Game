using static Server_Strategico.Server.Server;

namespace Server_Strategico.Gioco
{
    public class Dati
    {
        public static string Difficoltà = "1";
        public static string Versione = "0.1.3";
        public static string Server = "Italy";

        public static double forza_Esercito_Att_PVP = 0;
    }

    public class Giocatori
    {
        public class Barbari
        {
            public int Guerrieri { get; set; }
            public int Lancieri { get; set; }
            public int Arceri { get; set; }
            public int Catapulte { get; set; }
            public int Livello { get; set; }
            public static Barbari PVP = new Barbari
            {
                Guerrieri = 0,
                Lancieri = 0,
                Arceri = 0,
                Catapulte = 0
            };
        }
        public class Player
        {
            // Identità
            public string Username { get; set; }
            public string Password { get; set; }
            public Guid guid_Player { get; set; }

            // Esperienza e VIP
            public int Esperienza { get; set; }
            public int Livello { get; set; }
            public int Punti_Quest { get; set; }
            public bool Vip { get; set; }
            public bool Ricerca_Attiva { get; set; }

            // Forza esercito
            public double forza_Esercito { get; set; }
            public double forza_Esercito_PVE { get; set; }

            // Terreni virtuali
            public int Terreno_Comune { get; set; }
            public int Terreno_NonComune { get; set; }
            public int Terreno_Raro { get; set; }
            public int Terreno_Epico { get; set; }
            public int Terreno_Leggendario { get; set; }

            // Edifici civili
            public int Fattoria { get; set; }
            public int Segheria { get; set; }
            public int CavaPietra { get; set; }
            public int MinieraFerro { get; set; }
            public int MinieraOro { get; set; }
            public int Abitazioni { get; set; }

            // Edifici militari
            public int Workshop_Spade { get; set; }
            public int Workshop_Lance { get; set; }
            public int Workshop_Archi { get; set; }
            public int Workshop_Scudi { get; set; }
            public int Workshop_Armature { get; set; }
            public int Workshop_Frecce { get; set; }

            // Risorse civili
            public double Cibo { get; set; }
            public double Legno { get; set; }
            public double Pietra { get; set; }
            public double Ferro { get; set; }
            public double Oro { get; set; }
            public double Popolazione { get; set; }

            // Risorse speciali
            public int Diamanati_Blu { get; set; }
            public int Diamanti_Viola { get; set; }
            public decimal Dollari_Virtuali { get; set; }

            // Risorse militari
            public double Spade { get; set; }
            public double Lance { get; set; }
            public double Archi { get; set; }
            public double Scudi { get; set; }
            public double Armature { get; set; }
            public double Frecce { get; set; }

            public int Consumo_Cibo_Esercito { get; set; }
            public int Consumo_Oro_Esercito { get; set; }

            // Statistiche di combattimento
            public int Unità_Uccise { get; set; }
            public int Guerrieri_Uccisi { get; set; }
            public int Lanceri_Uccisi { get; set; }
            public int Arceri_Uccisi { get; set; }
            public int Catapulte_Uccisi { get; set; }
            public int Unità_Perse { get; set; }
            public int Guerrieri_Persi { get; set; }
            public int Lanceri_Persi { get; set; }
            public int Arceri_Persi { get; set; }
            public int Catapulte_Persi { get; set; }
            public int Risorse_Razziate { get; set; }

            public int Frecce_Utilizzate { get; set; }
            public int Battaglie_Vinte { get; set; }
            public int Battaglie_Perse { get; set; }
            public int Barbari_Sconfitti { get; set; }
            public int Accampamenti_Barbari_Sconfitti { get; set; }
            public int Città_Barbare_Sconfitte { get; set; }
            public int Missioni_Completate { get; set; }
            public int Attacchi_Subiti_PVP { get; set; }
            public int Attacchi_Effettuati_PVP { get; set; }

            public int Unità_Addestrate { get; set; }
            public int Risorse_Utilizzate { get; set; }
            public int Tempo_Addestramento_Risparmiato { get; set; }
            public int Tempo_Costruzione_Risparmiato { get; set; }

            // Esercito
            public int[] Guerrieri = new int[5];
            public int[] Lanceri = new int[5];
            public int[] Arceri = new int[5];
            public int[] Catapulte = new int[5];

            public int Caserma_Guerrieri { get; set; }
            public int Caserma_Lancieri { get; set; }
            public int Caserma_Arceri { get; set; }
            public int Caserma_Catapulte { get; set; }

            public int GuerrieriMax { get; set; }
            public int LancieriMax { get; set; }
            public int ArceriMax { get; set; }
            public int CatapulteMax { get; set; }

            // Ricerche
            public int Ricerca_Produzione { get; set; }
            public int Ricerca_Costruzione { get; set; }
            public int Ricerca_Addestramento { get; set; }
            public int Ricerca_Popolazione { get; set; }
            public int Ricerca_Riparazione { get; set; }

            //Ricerca Città

            public int Ricerca_Ingresso_Guarnigione { get; set; }
            public int Ricerca_Citta_Guarnigione { get; set; }

            public int Ricerca_Cancello_Salute { get; set; }
            public int Ricerca_Cancello_Difesa { get; set; }
            public int Ricerca_Cancello_Guarnigione { get; set; }

            public int Ricerca_Mura_Salute { get; set; }
            public int Ricerca_Mura_Difesa { get; set; }
            public int Ricerca_Mura_Guarnigione { get; set; }

            public int Ricerca_Torri_Salute { get; set; }
            public int Ricerca_Torri_Difesa { get; set; }
            public int Ricerca_Torri_Guarnigione { get; set; }

            public int Ricerca_Castello_Salute { get; set; }
            public int Ricerca_Castello_Difesa { get; set; }
            public int Ricerca_Castello_Guarnigione { get; set; }

            // Livelli unità
            public int Guerriero_Livello { get; set; }
            public int Guerriero_Salute { get; set; }
            public int Guerriero_Difesa { get; set; }
            public int Guerriero_Attacco { get; set; }

            public int Lancere_Livello { get; set; }
            public int Lancere_Salute { get; set; }
            public int Lancere_Difesa { get; set; }
            public int Lancere_Attacco { get; set; }

            public int Arcere_Livello { get; set; }
            public int Arcere_Salute { get; set; }
            public int Arcere_Difesa { get; set; }
            public int Arcere_Attacco { get; set; }

            public int Catapulta_Livello { get; set; }
            public int Catapulta_Salute { get; set; }
            public int Catapulta_Difesa { get; set; }
            public int Catapulta_Attacco { get; set; }

            // Premi
            public bool[] PremiNormali { get; set; } = new bool[20];
            public bool[] PremiVIP { get; set; } = new bool[20];

            // Coda e scudi
            public int Code_Reclutamento { get; set; }
            public int Code_Costruzione { get; set; }
            public int ScudoDellaPace { get; set; }

            // Città - Ingresso
            public int Guarnigione_Ingresso { get; set; }
            public int Guarnigione_IngressoMax { get; set; }
            public int[] Guerrieri_Ingresso { get; set; } = new int[5];
            public int[] Lanceri_Ingresso { get; set; } = new int[5];
            public int[] Arceri_Ingresso { get; set; } = new int[5];
            public int[] Catapulte_Ingresso { get; set; } = new int[5];

            // Cancello
            public int Guarnigione_Cancello { get; set; }
            public int Guarnigione_CancelloMax { get; set; }
            public int[] Guerrieri_Cancello { get; set; } = new int[5];
            public int[] Lanceri_Cancello { get; set; } = new int[5];
            public int[] Arceri_Cancello { get; set; } = new int[5];
            public int[] Catapulte_Cancello { get; set; } = new int[5];
            public int Salute_Cancello { get; set; }
            public int Salute_CancelloMax { get; set; }
            public int Difesa_Cancello { get; set; }
            public int Difesa_CancelloMax { get; set; }

            // Mura
            public int Guarnigione_Mura { get; set; }
            public int Guarnigione_MuraMax { get; set; }
            public int[] Guerrieri_Mura { get; set; } = new int[5];
            public int[] Lanceri_Mura { get; set; } = new int[5];
            public int[] Arceri_Mura { get; set; } = new int[5];
            public int[] Catapulte_Mura { get; set; } = new int[5];
            public int Salute_Mura { get; set; }
            public int Salute_MuraMax { get; set; }
            public int Difesa_Mura { get; set; }
            public int Difesa_MuraMax { get; set; }

            // Torri
            public int Guarnigione_Torri { get; set; }
            public int Guarnigione_TorriMax { get; set; }
            public int[] Guerrieri_Torri { get; set; } = new int[5];
            public int[] Lanceri_Torri { get; set; } = new int[5];
            public int[] Arceri_Torri { get; set; } = new int[5];
            public int[] Catapulte_Torri { get; set; } = new int[5];
            public int Salute_Torri { get; set; }
            public int Salute_TorriMax { get; set; }
            public int Difesa_Torri { get; set; }
            public int Difesa_TorriMax { get; set; }

            // Castello
            public int Guarnigione_Castello { get; set; }
            public int Guarnigione_CastelloMax { get; set; }
            public int[] Guerrieri_Castello { get; set; } = new int[5];
            public int[] Lanceri_Castello { get; set; } = new int[5];
            public int[] Arceri_Castello { get; set; } = new int[5];
            public int[] Catapulte_Castello { get; set; } = new int[5];
            public int Salute_Castello { get; set; }
            public int Salute_CastelloMax { get; set; }
            public int Difesa_Castello { get; set; }
            public int Difesa_CastelloMax { get; set; }

            // Città
            public int Guarnigione_Citta { get; set; }
            public int Guarnigione_CittaMax { get; set; }
            public int[] Guerrieri_Citta { get; set; } = new int[5];
            public int[] Lanceri_Citta { get; set; } = new int[5];
            public int[] Arceri_Citta { get; set; } = new int[5];
            public int[] Catapulte_Citta { get; set; } = new int[5];


            public Dictionary<string, Queue<ConstructionTask>> constructionQueues; // Dizionario per memorizzare le code di costruzione per ogni tipo di edificio
            public Dictionary<string, ConstructionTask> currentTasks; // Dizionario per memorizzare il task di costruzione attuale per ogni tipo di edificio

            public Dictionary<string, Queue<RecruitTask>> recruitQueues;  // Dizionario per memorizzare le code di reclutamento per ogni tipo di unità
            public Dictionary<string, RecruitTask> currentRecruitTasks;  // Dizionario per memorizzare il task di reclutamento attuale per ogni tipo di unità

            public Player(string username, string password, Guid guid_Client)
            {
                //Statistiche
                Tempo_Addestramento_Risparmiato = 0;
                Tempo_Costruzione_Risparmiato = 0;

                //Dati Giocatore
                Username = username;
                Password = password;
                guid_Player = guid_Client;
                ScudoDellaPace = 0;
                Code_Costruzione = 1;
                Code_Reclutamento = 1;

                Livello = 1;
                Esperienza = 0;
                Punti_Quest = 0;
                Vip = false;
                Ricerca_Attiva = false;
                Diamanati_Blu = 0;
                Diamanti_Viola = 0;
                Dollari_Virtuali = 0;
                forza_Esercito = 0;

                //Terreni Virtuali
                Terreno_Comune = 0;
                Terreno_NonComune = 0;
                Terreno_Raro = 0;
                Terreno_Epico = 0;
                Terreno_Leggendario = 0;

                //Strutture Civile
                Fattoria = 0; //Produce cibo
                Segheria = 0;
                CavaPietra = 0;
                MinieraFerro = 0;
                MinieraOro = 0;
                Abitazioni = 0; //Aumenta il numero abitanti/s

                //Strutture Militare
                Workshop_Spade = 0; //Produce Spade
                Workshop_Lance = 0;
                Workshop_Archi = 0;
                Workshop_Scudi = 0;
                Workshop_Armature = 0;
                Workshop_Frecce = 0;

                Caserma_Guerrieri = 0; //Numero Caserme
                Caserma_Lancieri = 0;
                Caserma_Arceri = 0;
                Caserma_Catapulte = 0;

                //Risorse Civile
                Cibo = 0;
                Legno = 0;
                Pietra = 0;
                Ferro = 0;
                Oro = 0;
                Popolazione = 0;

                //Risorse Militare
                Spade = 0;
                Lance = 0;
                Archi = 0;
                Scudi = 0;
                Armature = 0;
                Frecce = 0;

                //Esercito
                for (int i = 0; i < 5; i++)
                {
                    Guerrieri[i] = 0;
                    Lanceri[i] = 0;
                    Arceri[i] = 0;
                    Catapulte[i] = 0;
                }

                //Limite x caserma
                GuerrieriMax = 35;
                LancieriMax = 25;
                ArceriMax = 10;
                CatapulteMax = 5;

                //Città
                Guarnigione_Ingresso = 0;
                Guarnigione_IngressoMax = 100;

                for (int i = 0; i < 5; i++)
                {
                    Guerrieri_Ingresso[i] = 0;
                    Lanceri_Ingresso[i] = 0;
                    Arceri_Ingresso[i] = 0;
                    Catapulte_Ingresso[i] = 0;
                }

                Guarnigione_Cancello = 0;
                Guarnigione_CancelloMax = 50;
                for (int i = 0; i < 5; i++)
                {
                    Guerrieri_Cancello[i] = 0;
                    Lanceri_Cancello[i] = 0;
                    Arceri_Cancello[i] = 0;
                    Catapulte_Cancello[i] = 0;
                }
                Salute_Cancello = 50;
                Salute_CancelloMax = 50;
                Difesa_Cancello = 50;
                Difesa_CancelloMax = 50;

                Guarnigione_Mura = 0;
                Guarnigione_MuraMax = 50;
                for (int i = 0; i < 5; i++)
                {
                    Guerrieri_Mura[i] = 0;
                    Lanceri_Mura[i] = 0;
                    Arceri_Mura[i] = 0;
                    Catapulte_Mura[i] = 0;
                }
                Salute_Mura = 50;
                Salute_MuraMax = 50;
                Difesa_Mura = 50;
                Difesa_MuraMax = 50;

                Guarnigione_Torri = 0;
                Guarnigione_TorriMax = 50;
                for (int i = 0; i < 5; i++)
                {
                    Guerrieri_Torri[i] = 0;
                    Lanceri_Torri[i] = 0;
                    Arceri_Torri[i] = 0;
                    Catapulte_Torri[i] = 0;
                }
                Salute_Torri = 50;
                Salute_TorriMax = 50;
                Difesa_Torri = 50;
                Difesa_TorriMax = 50;

                Guarnigione_Castello = 0;
                Guarnigione_CastelloMax = 75;
                for (int i = 0; i < 5; i++)
                {
                    Guerrieri_Castello[i] = 0;
                    Lanceri_Castello[i] = 0;
                    Arceri_Castello[i] = 0;
                    Catapulte_Castello[i] = 0;
                }
                Salute_Castello = 75;
                Salute_CastelloMax = 75;
                Difesa_Castello = 75;
                Difesa_CastelloMax = 75;

                Guarnigione_Citta = 0;
                Guarnigione_CittaMax = 200;
                for (int i = 0; i < 5; i++)
                {
                    Guerrieri_Citta[i] = 0;
                    Lanceri_Citta[i] = 0;
                    Arceri_Citta[i] = 0;
                    Catapulte_Citta[i] = 0;
                }

                //Ricerche
                Ricerca_Produzione = 0;
                Ricerca_Costruzione = 0;
                Ricerca_Riparazione = 0;
                Ricerca_Addestramento = 0;
                Ricerca_Popolazione = 0;

                //Ricerca CIttà
                Ricerca_Ingresso_Guarnigione = 0;
                Ricerca_Citta_Guarnigione = 0;

                Ricerca_Cancello_Salute = 0;
                Ricerca_Cancello_Difesa = 0;
                Ricerca_Cancello_Guarnigione = 0;

                Ricerca_Mura_Salute = 0;
                Ricerca_Mura_Difesa = 0;
                Ricerca_Mura_Guarnigione = 0;

                Ricerca_Torri_Salute = 0;
                Ricerca_Torri_Difesa = 0;
                Ricerca_Torri_Guarnigione = 0;

                Ricerca_Castello_Salute = 0;
                Ricerca_Castello_Difesa = 0;
                Ricerca_Castello_Guarnigione = 0;

                //Livelli unità
                Guerriero_Livello = 0;
                Guerriero_Salute = 0;
                Guerriero_Difesa = 0;
                Guerriero_Attacco = 0;

                Lancere_Livello = 0;
                Lancere_Salute = 0;
                Lancere_Difesa = 0;
                Lancere_Attacco = 0;

                Arcere_Livello = 0;
                Arcere_Salute = 0;
                Arcere_Difesa = 0;
                Arcere_Attacco = 0;

                Catapulta_Livello = 0;
                Catapulta_Salute = 0;
                Catapulta_Difesa = 0;
                Catapulta_Attacco = 0;

                constructionQueues = new Dictionary<string, Queue<ConstructionTask>>();
                currentTasks = new Dictionary<string, ConstructionTask>();

                recruitQueues = new Dictionary<string, Queue<RecruitTask>>();
                currentRecruitTasks = new Dictionary<string, RecruitTask>();
            }

            public bool ValidatePassword(string password)
            {
                return Password == password;
            }

            public void ProduceResources() //produzione risorse
            {
                Cibo += Fattoria * (Strutture.Edifici.Fattoria.Produzione + Ricerca_Produzione * Ricerca.Tipi.Incremento.Cibo);
                Legno += Segheria * (Strutture.Edifici.Segheria.Produzione + Ricerca_Produzione * Ricerca.Tipi.Incremento.Legno);
                Pietra += CavaPietra * (Strutture.Edifici.CavaPietra.Produzione + Ricerca_Produzione * Ricerca.Tipi.Incremento.Pietra);
                Ferro += MinieraFerro * (Strutture.Edifici.MinieraFerro.Produzione + Ricerca_Produzione * Ricerca.Tipi.Incremento.Ferro);
                Oro += MinieraOro * (Strutture.Edifici.MinieraOro.Produzione + Ricerca_Produzione * Ricerca.Tipi.Incremento.Oro);
                Popolazione += Abitazioni * (Strutture.Edifici.Case.Produzione + Ricerca_Produzione * Ricerca.Tipi.Incremento.Popolazione);

                Spade += Workshop_Spade * Strutture.Edifici.ProduzioneSpade.Produzione;
                Lance += Workshop_Lance * Strutture.Edifici.ProduzioneLance.Produzione;
                Archi += Workshop_Archi * Strutture.Edifici.ProduzioneArchi.Produzione;
                Scudi += Workshop_Scudi * Strutture.Edifici.ProduzioneScudi.Produzione;
                Armature += Workshop_Armature * Strutture.Edifici.ProduzioneArmature.Produzione;
                Frecce += Workshop_Frecce * Strutture.Edifici.ProduzioneFrecce.Produzione;
            }
            public void ManutenzioneEsercito() //produzione risorse
            {
                Cibo -= Guerrieri[0] * Esercito.Unità.Guerrieri_1.Cibo + Lanceri[0] * Esercito.Unità.Lanceri_1.Cibo + Arceri[0] * Esercito.Unità.Arceri_1.Cibo + Catapulte[0] * Esercito.Unità.Catapulte_1.Cibo;
                Oro -= Guerrieri[0] * Esercito.Unità.Guerrieri_1.Salario + Lanceri[0] * Esercito.Unità.Lanceri_1.Salario + Arceri[0] * Esercito.Unità.Arceri_1.Salario + Catapulte[0] * Esercito.Unità.Catapulte_1.Salario;
                if (Cibo <= 0) Cibo = 0;
                if (Oro <= 0) Oro = 0;
            }
            public Strutture.Edifici GetBuildingCost(string buildingType)
            {
                // Restituisci i costi dell'edificio in base al tipo
                return buildingType switch
                {
                    "Fattoria" => Strutture.Edifici.Fattoria,
                    "Segheria" => Strutture.Edifici.Segheria,
                    "CavaPietra" => Strutture.Edifici.CavaPietra,
                    "MinieraFerro" => Strutture.Edifici.MinieraFerro,
                    "MinieraOro" => Strutture.Edifici.MinieraOro,
                    "Case" => Strutture.Edifici.Case,

                    "ProduzioneSpade" => Strutture.Edifici.ProduzioneSpade,
                    "ProduzioneLancie" => Strutture.Edifici.ProduzioneLance,
                    "ProduzioneArchi" => Strutture.Edifici.ProduzioneArchi,
                    "ProduzioneScudi" => Strutture.Edifici.ProduzioneScudi,
                    "ProduzioneArmature" => Strutture.Edifici.ProduzioneArmature,
                    "ProduzioneFrecce" => Strutture.Edifici.ProduzioneFrecce,

                    "CasermaGuerrieri" => Strutture.Edifici.CasermaGuerrieri,
                    "CasermaLancieri" => Strutture.Edifici.CasermaLanceri,
                    "CasermaArcieri" => Strutture.Edifici.CasermaArceri,
                    "CasermaCatapulte" => Strutture.Edifici.CasermaCatapulte,
                    // Aggiungi altri edifici se necessario
                    _ => null,
                };
            }
            public void StartNextConstruction(string buildingType) // Metodo per avviare la prossima costruzione per un tipo specifico di edificio
            {
                if (constructionQueues[buildingType].Count > 0)
                {
                    currentTasks[buildingType] = constructionQueues[buildingType].Dequeue();
                    currentTasks[buildingType].Start();
                    Console.WriteLine($"Costruzione di una {buildingType} iniziata, completamento previsto in {currentTasks[buildingType].DurationInSeconds} secondi.");
                }
                else
                    currentTasks[buildingType] = null; // Nessuna costruzione in corso

            }
            public void CompleteBuilds(Guid clientGuid) // Metodo per completare le costruzioni in corso
            {
                foreach (var buildingType in currentTasks.Keys)
                {
                    var currentTask = currentTasks[buildingType];
                    if (currentTask != null && currentTask.IsComplete())
                    {
                        switch (buildingType)
                        {
                            case "Fattoria":
                                Fattoria++;
                                Console.WriteLine($"Costruzione completata {buildingType} costruita!");
                                break;
                            case "Segheria":
                                Segheria++;
                                Console.WriteLine($"Costruzione completata {buildingType} costruita!");
                                break;
                            case "CavaPietra":
                                CavaPietra++;
                                Console.WriteLine($"Costruzione completata {buildingType} costruita!");
                                break;
                            case "MinieraFerro":
                                MinieraFerro++;
                                Console.WriteLine($"Costruzione completata {buildingType} costruita!");
                                break;
                            case "MinieraOro":
                                MinieraOro++;
                                Console.WriteLine($"Costruzione completata {buildingType} costruita!");
                                break;
                            case "Case":
                                Abitazioni++;
                                Console.WriteLine($"Costruzione completata {buildingType} costruita!");
                                break;
                            case "ProduzioneSpade":
                                Workshop_Spade++;
                                Console.WriteLine($"Costruzione completata {buildingType} costruita!");
                                break;
                            case "ProduzioneLancie":
                                Workshop_Lance++;
                                Console.WriteLine($"Costruzione completata {buildingType} costruita!");
                                break;
                            case "ProduzioneArchi":
                                Workshop_Archi++;
                                Console.WriteLine($"Costruzione completata {buildingType} costruita!");
                                break;
                            case "ProduzioneScudi":
                                Workshop_Scudi++;
                                Console.WriteLine($"Costruzione completata {buildingType} costruita!");
                                break;
                            case "ProduzioneArmature":
                                Workshop_Armature++;
                                Console.WriteLine($"Costruzione completata {buildingType} costruita!");
                                break;
                            case "ProduzioneFrecce":
                                Workshop_Frecce++;
                                Console.WriteLine($"Costruzione completata {buildingType} costruita!");
                                break;
                            case "CasermaGuerrieri":
                                Caserma_Guerrieri++;
                                Console.WriteLine($"Costruzione completata {buildingType} costruita!");
                                break;
                            case "CasermaLancieri":
                                Caserma_Lancieri++;
                                Console.WriteLine($"Costruzione completata {buildingType} costruita!");
                                break;
                            case "CasermaArcieri":
                                Caserma_Arceri++;
                                Console.WriteLine($"Costruzione completata {buildingType} costruita!");
                                break;
                            case "CasermaCatapulte":
                                Caserma_Catapulte++;
                                Console.WriteLine($"Costruzione completata {buildingType} costruita!");
                                break;
                            // Aggiungi case per altri tipi di costruzioni
                            default:
                                Console.WriteLine($"Costruzione {buildingType} non valida!");
                                break;
                        }
                        // Avvia la prossima costruzione per questo tipo di edificio
                        Send(clientGuid, $"Log_Server|Costruzione completata {buildingType} costruita!\n\r");
                        StartNextConstruction(buildingType);
                    }
                }
            }
            public Dictionary<string, string> GetRemainingConstructions()
            {
                var remaining = new Dictionary<string, string>();

                foreach (var task in currentTasks)
                {
                    if (task.Value != null)
                    {
                        double timeLeft = task.Value.GetRemainingTime();
                        remaining[task.Key] = FormatTime(timeLeft);
                    }
                }

                return remaining;
            }

            public class ConstructionTask // Classe privata per rappresentare un task di costruzione
            {
                public string Type { get; }
                public int DurationInSeconds { get; }
                public  DateTime startTime;

                public ConstructionTask(string type, int durationInSeconds)
                {
                    Type = type;
                    DurationInSeconds = durationInSeconds;
                }
                public void Start()
                {
                    startTime = DateTime.Now;
                }
                public bool IsComplete()
                {
                    return DateTime.Now >= startTime.AddSeconds(DurationInSeconds);
                }
                public double GetRemainingTime()
                {
                    if (startTime == default) return DurationInSeconds; // se non è ancora partito
                    double elapsed = (DateTime.Now - startTime).TotalSeconds;
                    return Math.Max(0, DurationInSeconds - elapsed);
                }
            }
            public class RecruitTask // Classe privata per rappresentare un task di reclutamento
            {
                public string Type { get; }
                public int DurationInSeconds { get; }
                private DateTime startTime;

                public RecruitTask(string type, int durationInSeconds)
                {
                    Type = type;
                    DurationInSeconds = durationInSeconds;
                }

                public void Start()
                {
                    startTime = DateTime.Now;
                }
                public bool IsComplete()
                {
                    return DateTime.Now >= startTime.AddSeconds(DurationInSeconds);
                }
                public double GetRemainingTime()
                {
                    if (startTime == default) return DurationInSeconds; // se non è ancora partito
                    double elapsed = (DateTime.Now - startTime).TotalSeconds;
                    return Math.Max(0, DurationInSeconds - elapsed);
                }
            }
            public Dictionary<string, string> GetRemainingTrainings()
            {
                var remaining = new Dictionary<string, string>();

                foreach (var task in currentRecruitTasks)
                {
                    if (task.Value != null)
                    {
                        double timeLeft = task.Value.GetRemainingTime();
                        remaining[task.Key] = FormatTime(timeLeft);
                    }
                }

                return remaining;
            }

            private string FormatTime(double seconds)
            {
                var ts = TimeSpan.FromSeconds(seconds);

                int days = ts.Days;
                int hours = ts.Hours;
                int minutes = ts.Minutes;
                int secs = ts.Seconds;

                string result = "";
                if (days > 0) result += $"{days}d ";
                if (hours > 0 || days > 0) result += $"{hours}h ";
                if (minutes > 0 || hours > 0 || days > 0) result += $"{minutes}m ";
                result += $"{secs}s";

                return result.Trim();
            }
            public async void QueueTrainUnits(string unitType, int count, Guid clientGuid, Player player)
            {
                var unitCost = GetUnitCost(unitType);
                int ridurre_Addestramento = 0;

                if (unitType == "Guerrieri_1") ridurre_Addestramento = 1;
                if (unitType == "Lanceri_1") ridurre_Addestramento = 1;
                if (unitType == "Arceri_1") ridurre_Addestramento = 2;
                if (unitType == "Catapulta") ridurre_Addestramento = 3;

                if (unitType == "Guerriero" && count + player.Guerrieri[0] > player.GuerrieriMax * player.Caserma_Guerrieri)
                {
                    Send(clientGuid, $"Log_Server|Limite raggiunto per addestrare {count} {unitType}. [Limite: {player.GuerrieriMax * player.Caserma_Guerrieri}]");
                    Console.WriteLine($"Limite raggiunto per addestrare {count} {unitType}. [Limite: {player.GuerrieriMax * player.Caserma_Guerrieri}]");
                    return;
                }
                else if (unitType == "Lanceri_1" && count + player.Lanceri[0] > player.LancieriMax * player.Caserma_Lancieri)
                {
                    Send(clientGuid, $"Log_Server|Limite raggiunto per addestrare {count} {unitType}. [Limite: {player.LancieriMax * player.Caserma_Lancieri}]");
                    Console.WriteLine($"Limite raggiunto per addestrare {count} {unitType}.[Limite: {player.LancieriMax * player.Caserma_Lancieri}]");
                    return;
                }
                else if (unitType == "Arceri_1" && count + player.Arceri[0] > player.ArceriMax * player.Caserma_Arceri)
                {
                    Send(clientGuid, $"Log_Server|Limite raggiunto per addestrare {count} {unitType}. [Limite: {player.ArceriMax * player.Caserma_Arceri}]");
                    Console.WriteLine($"Limite raggiunto per addestrare {count} {unitType}. [Limite: {player.ArceriMax * player.Caserma_Arceri}]");
                    return;
                }
                else if (unitType == "Catapulta" && count + player.Catapulte[0] > player.CatapulteMax * player.Caserma_Catapulte)
                {
                    Send(clientGuid, $"Log_Server|Limite raggiunto per addestrare {count} {unitType}. [Limite: {player.CatapulteMax * player.Caserma_Catapulte}]");
                    Console.WriteLine($"Limite raggiunto per addestrare {count} {unitType}. [Limite: {player.CatapulteMax * player.Caserma_Catapulte}]");
                    return;
                }

                if (Cibo >= unitCost.Cibo * count &&
                    Legno >= unitCost.Legno * count &&
                    Pietra >= unitCost.Pietra * count &&
                    Ferro >= unitCost.Ferro * count &&
                    Oro >= unitCost.Oro * count &&
                    Popolazione >= unitCost.Popolazione * count &&
                    Spade >= unitCost.Spade * count &&
                    Lance >= unitCost.Lance * count &&
                    Archi >= unitCost.Archi * count &&
                    Scudi >= unitCost.Scudi * count &&
                    Armature >= unitCost.Armature * count)
                {
                    Cibo -= unitCost.Cibo * count;
                    Legno -= unitCost.Legno * count;
                    Pietra -= unitCost.Pietra * count;
                    Ferro -= unitCost.Ferro * count;
                    Oro -= unitCost.Oro * count;
                    Popolazione -= unitCost.Popolazione * count;
                    Spade -= unitCost.Spade * count;
                    Lance -= unitCost.Lance * count;
                    Archi -= unitCost.Archi * count;
                    Scudi -= unitCost.Scudi * count;
                    Armature -= unitCost.Armature * count;

                    Send(clientGuid, $"Log_Server|Risorse utilizzate per l'addestramento di {count} {unitType}:\r\n " +
                        $"Cibo: {unitCost.Cibo * count}, " +
                        $"Legno: {unitCost.Legno * count}, " +
                        $"Pietra: {unitCost.Pietra * count}, " +
                        $"Ferro: {unitCost.Ferro * count}, " +
                        $"Oro: {unitCost.Oro * count}, " +
                        $"Spade: {unitCost.Spade * count}, " +
                        $"Lance: {unitCost.Lance * count}, " +
                        $"Archi: {unitCost.Archi * count}, " +
                        $"Scudi: {unitCost.Scudi * count}, " +
                        $"Armature: {unitCost.Armature * count}\r\n");
                    Console.WriteLine($"Risorse utilizzate per l'addestramento di {count} {unitType}:\r\n " +
                        $"Cibo: {unitCost.Cibo * count}, " +
                        $"Legno: {unitCost.Legno * count}, " +
                        $"Pietra: {unitCost.Pietra * count}, " +
                        $"Ferro: {unitCost.Ferro * count}, " +
                        $"Oro: {unitCost.Oro * count}, " +
                        $"Spade: {unitCost.Spade * count}, " +
                        $"Lance: {unitCost.Lance * count}, " +
                        $"Archi: {unitCost.Archi * count}, " +
                        $"Scudi: {unitCost.Scudi * count}, " +
                        $"Armature: {unitCost.Armature * count}\r\n");

                    if (!recruitQueues.ContainsKey(unitType))
                        recruitQueues[unitType] = new Queue<RecruitTask>();

                    int tempoAddestramentoInSecondi = Convert.ToInt32(unitCost.TempoReclutamento - (player.Ricerca_Addestramento * ridurre_Addestramento));
                    Console.WriteLine($"[Server] Tempo addestramento - Base {unitCost.TempoReclutamento}s/{unitCost.TempoReclutamento - (player.Ricerca_Addestramento * ridurre_Addestramento)}s | Livello: {player.Ricerca_Addestramento}");
                    
                    for (int i = 0; i < count; i++)
                        recruitQueues[unitType].Enqueue(new RecruitTask(unitType, tempoAddestramentoInSecondi));

                    if (!currentRecruitTasks.ContainsKey(unitType))
                        currentRecruitTasks[unitType] = null;
                    
                    if (currentRecruitTasks[unitType] == null)
                        StartNextRecruitment(unitType);
                }
                else
                {
                    Send(clientGuid, $"Log_Server|Risorse insufficienti per addestrare {count} {unitType}.");
                    Console.WriteLine($"Risorse insufficienti per addestrare {count} {unitType}.");
                }
            }
            public async void LoadQueueTrainUnits(string unitType, int count, Player player)
            {
                var unitCost = GetUnitCost(unitType);
                if (!recruitQueues.ContainsKey(unitType))
                    recruitQueues[unitType] = new Queue<RecruitTask>();
                
                int tempoAddestramentoInSecondi = Convert.ToInt32(unitCost.TempoReclutamento - player.Ricerca_Addestramento);
                for (int i = 0; i < count; i++)
                    recruitQueues[unitType].Enqueue(new RecruitTask(unitType, tempoAddestramentoInSecondi));
                
                if (!currentRecruitTasks.ContainsKey(unitType))
                    currentRecruitTasks[unitType] = null;

                if (currentRecruitTasks[unitType] == null)
                    StartNextRecruitment(unitType);
            }
            private void StartNextRecruitment(string unitType)
            {
                if (recruitQueues[unitType].Count > 0)
                {
                    currentRecruitTasks[unitType] = recruitQueues[unitType].Dequeue();
                    currentRecruitTasks[unitType].Start();
                    //Console.WriteLine($"Addestramento di un'unità {unitType} iniziato, completamento previsto in {currentRecruitTasks[unitType].DurationInSeconds} secondi.");
                }
                else
                    currentRecruitTasks[unitType] = null;
            }
            public void CompleteRecruitment(Guid clientGuid)
            {
                foreach (var unitType in currentRecruitTasks.Keys)
                {
                    var currentTask = currentRecruitTasks[unitType];
                    if (currentTask != null && currentTask.IsComplete())
                    {
                        switch (unitType)
                        {
                            case "Arceri_1":
                                Arceri[0]++;
                                break;
                            case "Guerrieri_1":
                                Guerrieri[0]++;
                                break;
                            case "Lanceri_1":
                                Lanceri[0]++;
                                break;
                            case "Catapulta":
                                Catapulte[0]++;
                                break;
                            default:
                                Console.WriteLine($"{unitType} addestrato!");
                                break;
                        }
                        Send(clientGuid, $"Log_Server|{unitType} addestrato!\n\r");
                        StartNextRecruitment(unitType);
                    }
                }
            }
            private Esercito.CostoReclutamento GetUnitCost(string unitType)
            {
                return unitType switch
                {
                    "Guerrieri_1" => Esercito.CostoReclutamento.Guerrieri_1,
                    "Lanceri_1" => Esercito.CostoReclutamento.Lanceri_1,
                    "Arceri_1" => Esercito.CostoReclutamento.Arceri_1,
                    "Catapulta" => Esercito.CostoReclutamento.Catapulte_1,
                    _ => null,
                };
            }
            public void SetBuildings(int fattoria, int segheria, int cavaPietra, int mineraFerro, int mineraOro, int abitazioni, int ProdSp, int ProdLan, int ProdArc, int ProdScud, int ProdArmat, int ProdFrecce, int cas_Gu, int cas_Lan, int cas_Arc, int cas_Cat)
            {
                Fattoria = fattoria;
                Segheria = segheria;
                CavaPietra = cavaPietra;
                MinieraFerro = mineraFerro;
                MinieraOro = mineraOro;
                Abitazioni = abitazioni;
                Workshop_Spade = ProdSp;
                Workshop_Lance = ProdLan;
                Workshop_Archi = ProdArc;
                Workshop_Scudi = ProdScud;
                Workshop_Armature = ProdArmat;
                Workshop_Frecce = ProdFrecce;
                Caserma_Guerrieri = cas_Gu;
                Caserma_Lancieri = cas_Lan;
                Caserma_Arceri = cas_Arc;
                Caserma_Catapulte = cas_Cat;
            }
            public Dictionary<string, int> GetQueuedBuildings()
            {
                var queuedBuildings = new Dictionary<string, int>();
                foreach (var queue in constructionQueues)
                {
                    queuedBuildings[queue.Key] = queue.Value.Count;
                }
                return queuedBuildings;
            }
            public Dictionary<string, int> GetQueuedUnits()
            {
                var queuedUnits = new Dictionary<string, int>();
                foreach (var queue in recruitQueues)
                {
                    queuedUnits[queue.Key] = queue.Value.Count;
                }
                return queuedUnits;
            }
        }
        
    }
}

