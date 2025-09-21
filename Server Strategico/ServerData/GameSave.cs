using Server_Strategico.Gioco;
using System.Text.Json;
using static Server_Strategico.Gioco.Giocatori;

namespace Server_Strategico.Server
{
    internal class GameSave
    {
        private static readonly string SavePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
            "Server Strategico",
            "Saves"
        );

        public static void Initialize()
        {
            if (!Directory.Exists(SavePath)) // Crea la directory se non esiste
                Directory.CreateDirectory(SavePath);
        }

        public static async Task SavePlayer(Player player)
        {
            try
            {
                var playerData = new PlayerSaveData
                {
                    //Giocatori
                    Username = player.Username,
                    Password = player.Password,
                    Livello = player.Livello,
                    Esperienza = player.Esperienza,
                    Vip = player.Vip,
                    ScudoDellaPace = player.ScudoDellaPace,
                    Punti_Quest = player.Punti_Quest,
                    Code_Costruzione = player.Code_Costruzione,
                    Code_Reclutamento = player.Code_Reclutamento,
                    Code_Ricerca = player.Code_Ricerca,

                    // Risorse
                    Cibo = player.Cibo,
                    Legno = player.Legno,
                    Pietra = player.Pietra,
                    Ferro = player.Ferro,
                    Oro = player.Oro,
                    Popolazione = player.Popolazione,

                    Diamanati_Blu = player.Diamanati_Blu,
                    Diamanti_Viola = player.Diamanti_Viola,
                    Dollari_Virtuali = player.Dollari_Virtuali,

                    Spade = player.Spade,
                    Lance = player.Lance,
                    Archi = player.Archi,
                    Scudi = player.Scudi,
                    Armature = player.Armature,
                    Frecce = player.Frecce,

                    // Edifici
                    Fattoria = player.Fattoria,
                    Segheria = player.Segheria,
                    CavaPietra = player.CavaPietra,
                    MinieraFerro = player.MinieraFerro,
                    MinieraOro = player.MinieraOro,
                    Abitazioni = player.Abitazioni,

                    Workshop_Spade = player.Workshop_Spade,
                    Workshop_Lance = player.Workshop_Lance,
                    Workshop_Archi = player.Workshop_Archi,
                    Workshop_Scudi = player.Workshop_Scudi,
                    Workshop_Armature = player.Workshop_Armature,
                    Workshop_Frecce = player.Workshop_Frecce,

                    Caserma_Guerrieri = player.Caserma_Guerrieri,
                    Caserma_Lancieri = player.Caserma_Lancieri,
                    Caserma_Arceri = player.Caserma_Arceri,
                    Caserma_Catapulte = player.Caserma_Catapulte,

                    Terreno_Comune = player.Terreno_Comune,
                    Terreno_NonComune = player.Terreno_NonComune,
                    Terreno_Raro = player.Terreno_Raro,
                    Terreno_Epico = player.Terreno_Epico,
                    Terreno_Leggendario = player.Terreno_Leggendario,

                    // Esercito
                    Guerrieri = player.Guerrieri[0],
                    Lancieri = player.Lanceri[0],
                    Arceri = player.Arceri[0],
                    Catapulte = player.Catapulte[0],

                    // Caserme
                    GuerrieriMax = player.GuerrieriMax,
                    LancieriMax = player.LancieriMax,
                    ArceriMax = player.ArceriMax,
                    CatapulteMax = player.CatapulteMax,

                    //Ricerche
                    Ricerca_Produzione = player.Ricerca_Produzione,
                    Ricerca_Costruzione = player.Ricerca_Costruzione,
                    Ricerca_Riparazione = player.Ricerca_Riparazione,
                    Ricerca_Addestramento = player.Ricerca_Addestramento,
                
                    Guerriero_Livello = player.Guerriero_Livello,
                    Guerriero_Salute = player.Guerriero_Salute,
                    Guerriero_Difesa = player.Guerriero_Difesa,
                    Guerriero_Attacco = player.Guerriero_Attacco,

                    Lanciere_Livello = player.Lancere_Livello,
                    Lanciere_Salute = player.Lancere_Salute,
                    Lanciere_Difesa = player.Lancere_Difesa,
                    Lanciere_Attacco = player.Lancere_Attacco,

                    Arciere_Livello = player.Arcere_Livello,
                    Arciere_Salute = player.Arcere_Salute,
                    Arciere_Difesa = player.Arcere_Difesa,
                    Arciere_Attacco = player.Arcere_Attacco,

                    catapulta_Livello = player.Catapulta_Livello,
                    catapulta_Salute = player.Catapulta_Salute,
                    catapulta_Difesa = player.Catapulta_Difesa,
                    catapulta_Attacco = player.Catapulta_Attacco,

                    //Castello
                    Salute_Cancello = player.Salute_Cancello,
                    Salute_CancelloMax = player.Salute_CancelloMax,
                    Salute_Mura = player.Salute_Mura,
                    Salute_MuraMax = player.Salute_MuraMax,
                    Salute_Torri = player.Salute_Torri,
                    Salute_TorriMax = player.Salute_TorriMax,
                    Salute_Castello = player.Salute_Castello,
                    Salute_CastelloMax = player.Salute_CastelloMax,

                    // --- Code Costruzione ---
                    CurrentBuildingTasks = player.currentTasks_Building
                    .Select(t => new SavedTask
                    {
                        Type = t.Type,
                        DurationInSeconds = t.DurationInSeconds,
                        RemainingSeconds = t.GetRemainingTime(),
                        IsInProgress = true
                    })
                    .ToList(),

                    QueuedBuildingTasks = player.building_Queue
                    .Select(t => new SavedTask
                    {
                        Type = t.Type,
                        DurationInSeconds = t.DurationInSeconds,
                        RemainingSeconds = t.DurationInSeconds,
                        IsInProgress = false
                    })
                    .ToList(),

                    // --- Reclutamento ---
                    CurrentRecruitTasks = player.currentTasks_Recruit
                    .Select(t => new SavedTask
                    {
                        Type = t.Type,
                        DurationInSeconds = t.DurationInSeconds,
                        RemainingSeconds = t.GetRemainingTime(),
                        IsInProgress = true
                    })
                    .ToList(),

                                    QueuedRecruitTasks = player.recruit_Queue
                    .Select(t => new SavedTask
                    {
                        Type = t.Type,
                        DurationInSeconds = t.DurationInSeconds,
                        RemainingSeconds = t.DurationInSeconds,
                        IsInProgress = false
                    })
                    .ToList(),

                                    // --- Ricerca ---
                                    CurrentResearchTasks = player.currentTasks_Research
                    .Select(t => new SavedTask
                    {
                        Type = t.Type,
                        DurationInSeconds = t.DurationInSeconds,
                        RemainingSeconds = t.GetRemainingTime(),
                        IsInProgress = true
                    })
                    .ToList(),

                                    QueuedResearchTasks = player.research_Queue
                    .Select(t => new SavedTask
                    {
                        Type = t.Type,
                        DurationInSeconds = t.DurationInSeconds,
                        RemainingSeconds = t.DurationInSeconds,
                        IsInProgress = false
                    })
                    .ToList(),
                };

                string fileName = Path.Combine(SavePath, $"{player.Username}.json");
                string jsonString = JsonSerializer.Serialize(playerData, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(fileName, jsonString);

                Console.WriteLine($"[GameSave] Salvati i dati del giocatore {player.Username}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GameSave] Errore durante il salvataggio: {ex.Message}");
            }
        }
        public static async Task<bool> LoadPlayer(string username, string password)
        {
            try
            {
                string fileName = Path.Combine(SavePath, $"{username}.json");
                if (!File.Exists(fileName))
                {
                    Console.WriteLine($"[GameSave] Nessun salvataggio trovato per {username}");
                    return false;
                }

                string jsonString = await File.ReadAllTextAsync(fileName);
                var playerData = JsonSerializer.Deserialize<PlayerSaveData>(jsonString);

                if (playerData.Password != password && password != "Auto")
                {
                    Console.WriteLine($"[GameSave] Password non valida per {username}");
                    return false;
                }

                // Aggiorna il giocatore esistente con i dati salvati
                var player = Server.servers_.GetPlayer(username, password);
                if (player != null)
                {

                    //Giocatori
                    player.Username = playerData.Username;
                    player.Password = playerData.Password;
                    player.Livello = playerData.Livello;
                    player.Esperienza = playerData.Esperienza;
                    player.Vip = playerData.Vip;
                    player.ScudoDellaPace = playerData.ScudoDellaPace;
                    player.Punti_Quest = playerData.Punti_Quest;
                    player.Code_Costruzione = playerData.Code_Costruzione;
                    player.Code_Reclutamento = playerData.Code_Reclutamento;
                    player.Code_Ricerca = playerData.Code_Ricerca;

                    player.Diamanati_Blu = playerData.Diamanati_Blu;
                    player.Diamanti_Viola = playerData.Diamanti_Viola;
                    player.Dollari_Virtuali = playerData.Dollari_Virtuali;

                    player.Terreno_Comune = playerData.Terreno_Comune;
                    player.Terreno_NonComune = playerData.Terreno_NonComune;
                    player.Terreno_Raro = playerData.Terreno_Raro;
                    player.Terreno_Epico = playerData.Terreno_Epico;
                    player.Terreno_Leggendario = playerData.Terreno_Leggendario;

                    // Risorse
                    player.Cibo = playerData.Cibo;
                    player.Legno = playerData.Legno;
                    player.Pietra = playerData.Pietra;
                    player.Ferro = playerData.Ferro;
                    player.Oro = playerData.Oro;
                    player.Popolazione = playerData.Popolazione;

                    player.Spade = playerData.Spade;
                    player.Lance = playerData.Lance;
                    player.Archi = playerData.Archi;
                    player.Scudi = playerData.Scudi;
                    player.Armature = playerData.Armature;
                    player.Frecce = playerData.Frecce;

                    // Edifici
                    BuildingManager.SetBuildings(
                        playerData.Fattoria,
                        playerData.Segheria, 
                        playerData.CavaPietra,
                        playerData.MinieraFerro,
                        playerData.MinieraOro,
                        playerData.Abitazioni,
                        playerData.Workshop_Spade,
                        playerData.Workshop_Lance,
                        playerData.Workshop_Archi,
                        playerData.Workshop_Scudi,
                        playerData.Workshop_Armature,
                        playerData.Workshop_Frecce,
                        playerData.Caserma_Guerrieri,
                        playerData.Caserma_Lancieri,
                        playerData.Caserma_Arceri,
                        playerData.Caserma_Catapulte, 
                        player
                    );

                    // Esercito
                    player.Guerrieri[0] = playerData.Guerrieri;
                    player.Lanceri[0] = playerData.Lancieri;
                    player.Arceri[0] = playerData.Arceri;
                    player.Catapulte[0] = playerData.Catapulte;

                    // Caserme
                    player.GuerrieriMax = playerData.GuerrieriMax;
                    player.LancieriMax = playerData.LancieriMax;
                    player.ArceriMax = playerData.ArceriMax;
                    player.CatapulteMax = playerData.CatapulteMax;

                    //Ricerche
                    player.Ricerca_Produzione = playerData.Ricerca_Produzione;
                    player.Ricerca_Costruzione = playerData.Ricerca_Costruzione;
                    player.Ricerca_Riparazione = playerData.Ricerca_Riparazione;
                    player.Ricerca_Addestramento = playerData.Ricerca_Addestramento;

                    player.Guerriero_Livello = playerData.Guerriero_Livello;
                    player.Guerriero_Salute = playerData.Guerriero_Salute;
                    player.Guerriero_Difesa = playerData.Guerriero_Difesa;
                    player.Guerriero_Attacco = playerData.Guerriero_Attacco;

                    player.Lancere_Livello = playerData.Lanciere_Livello;
                    player.Lancere_Salute = playerData.Lanciere_Salute;
                    player.Lancere_Difesa = playerData.Lanciere_Difesa;
                    player.Lancere_Attacco = playerData.Lanciere_Attacco;

                    player.Arcere_Livello = playerData.Arciere_Livello;
                    player.Arcere_Salute = playerData.Arciere_Salute;
                    player.Arcere_Difesa = playerData.Arciere_Difesa;
                    player.Arcere_Attacco = playerData.Arciere_Attacco;

                    player.Catapulta_Livello = playerData.catapulta_Livello;
                    player.Catapulta_Salute = playerData.catapulta_Salute;
                    player.Catapulta_Difesa = playerData.catapulta_Difesa;
                    player.Catapulta_Attacco = playerData.catapulta_Attacco;

                    //Castello
                    player.Salute_Cancello = playerData.Salute_Cancello;
                    player.Salute_CancelloMax = playerData.Salute_CancelloMax;
                    player.Salute_Mura = playerData.Salute_Mura;
                    player.Salute_MuraMax = playerData.Salute_MuraMax;
                    player.Salute_Torri = playerData.Salute_Torri;
                    player.Salute_TorriMax = playerData.Salute_TorriMax;
                    player.Salute_Castello = playerData.Salute_Castello;
                    player.Salute_CastelloMax = playerData.Salute_CastelloMax;

                    // Ripristina le code
                    player.currentTasks_Building = playerData.CurrentBuildingTasks
                        .Select(t =>
                        {
                            var task = new BuildingManager.ConstructionTask(t.Type, t.DurationInSeconds);
                            if (t.IsInProgress) task.Start();
                            return task;
                        })
                        .ToList();

                    // --- Ripristino costruzioni in coda ---
                    player.building_Queue = new Queue<BuildingManager.ConstructionTask>(
                        playerData.QueuedBuildingTasks.Select(t =>
                            new BuildingManager.ConstructionTask(t.Type, t.DurationInSeconds)
                        )
                    );

                    // --- Reclutamento ---
                    player.currentTasks_Recruit = playerData.CurrentRecruitTasks
                        .Select(t => {
                            var task = new BuildingManager.ConstructionTask(t.Type, t.DurationInSeconds);
                            if (t.IsInProgress) task.Start();
                            return task;
                        }).ToList();

                    player.recruit_Queue = new Queue<BuildingManager.ConstructionTask>(
                        playerData.QueuedRecruitTasks.Select(t => new BuildingManager.ConstructionTask(t.Type, t.DurationInSeconds))
                    );

                    // --- Ricerca ---
                    player.currentTasks_Research = playerData.CurrentResearchTasks
                        .Select(t => {
                            var task = new BuildingManager.ConstructionTask(t.Type, t.DurationInSeconds);
                            if (t.IsInProgress) task.Start();
                            return task;
                        }).ToList();

                    player.research_Queue = new Queue<BuildingManager.ConstructionTask>(
                        playerData.QueuedResearchTasks.Select(t => new BuildingManager.ConstructionTask(t.Type, t.DurationInSeconds))
                    );

                    Console.WriteLine($"[GameSave] Caricati i dati del giocatore {username}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GameSave] Errore durante il caricamento: {ex.Message}");
            }
            return false;
        }
        public static async Task Load_Player_Data_Auto()
        {
            try
            {
                if (!Directory.Exists(SavePath))
                {
                    Console.WriteLine("[GameSave] Directory dei salvataggi non trovata");
                    return;
                }

                string[] saveFiles = Directory.GetFiles(SavePath, "*.json");

                foreach (string file in saveFiles)
                {
                    if (Path.GetFileName(file) == "BarbariPVP.json") // Salta il file dei barbari PVP
                        continue;

                    string username = Path.GetFileNameWithoutExtension(file);
                    Console.WriteLine($"[GameSave] Caricamento automatico per {username}");

                    try
                    {
                        // Leggi il file JSON per estrarre la password
                        string jsonString = await File.ReadAllTextAsync(file);
                        var playerData = JsonSerializer.Deserialize<PlayerSaveData>(jsonString);

                        string password = playerData.Password; // Estrai la password e carica i dati del giocatore
                        Console.WriteLine($"[GameSave] Password estratta per {username}");

                        // Carica i dati del giocatore con la password estratta dal file
                        bool success = await ServerConnection.Load_User_Auto(username, password);
                        if (success) Console.WriteLine($"[GameSave] Caricamento automatico completato per {username}");
                        else Console.WriteLine($"[GameSave] Caricamento automatico fallito per {username}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[GameSave] Errore durante l'estrazione della password per {username}: {ex.Message}");
                    }
                }

                Console.WriteLine("[GameSave] Caricamento automatico completato per tutti i giocatori");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GameSave] Errore durante il caricamento automatico: {ex.Message}");
            }
        }
        public static async Task SaveBarbariPVP()
        {
            var barbariData = new
            {
                Giocatori.Barbari.PVP.Guerrieri,
                Giocatori.Barbari.PVP.Lancieri,
                Giocatori.Barbari.PVP.Arceri,
                Giocatori.Barbari.PVP.Catapulte
            };

            string fileName = Path.Combine(SavePath, "BarbariPVP.json");
            string jsonString = JsonSerializer.Serialize(barbariData, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(fileName, jsonString);

            Console.WriteLine("[GameSave] Dati dei barbari PVP salvati.");
        }
        public static async Task<bool> LoadBarbariPVP()
        {
            try
            {
                string fileName = Path.Combine(SavePath, "BarbariPVP.json");
                if (!File.Exists(fileName))
                {
                    Console.WriteLine("[GameSave] Nessun salvataggio trovato per i Barbari PVP");
                    return false;
                }

                string jsonString = await File.ReadAllTextAsync(fileName);
                var barbariData = JsonSerializer.Deserialize<BarbariPVPData>(jsonString);

                // Aggiorna i dati dei barbari con i dati caricati
                Giocatori.Barbari.PVP.Guerrieri = barbariData.Guerrieri;
                Giocatori.Barbari.PVP.Lancieri = barbariData.Lancieri;
                Giocatori.Barbari.PVP.Arceri = barbariData.Arceri;
                Giocatori.Barbari.PVP.Catapulte = barbariData.Catapulte;
                Giocatori.Barbari.PVP.Livello = barbariData.Livello;

                Console.WriteLine("[GameSave] Dati dei barbari PVP caricati.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GameSave] Errore durante il caricamento dei barbari PVP: {ex.Message}");
            }
            return false;
        }
        public class SavedTask
        {
            public string Type { get; set; }
            public int DurationInSeconds { get; set; }
            public double RemainingSeconds { get; set; }
            public bool IsInProgress { get; set; }
        }
        private class BarbariPVPData
        {
            public int Guerrieri { get; set; }
            public int Lancieri { get; set; }
            public int Arceri { get; set; }
            public int Catapulte { get; set; }
            public int Livello { get; set; }
        }

        private class PlayerSaveData
        {
            // --- Code costruzione ---
            public List<SavedTask> CurrentBuildingTasks { get; set; } = new();
            public List<SavedTask> QueuedBuildingTasks { get; set; } = new();

            public List<SavedTask> CurrentRecruitTasks { get; set; } = new();
            public List<SavedTask> QueuedRecruitTasks { get; set; } = new();

            public List<SavedTask> CurrentResearchTasks { get; set; } = new();
            public List<SavedTask> QueuedResearchTasks { get; set; } = new();

            // Giocatori
            public string Username { get; set; }
            public string Password { get; set; }
            public Guid guid_Player { get; set; }

            // Esperienza e VIP
            public int Esperienza { get; set; }
            public int Livello { get; set; }
            public int Punti_Quest { get; set; }
            public bool Vip { get; set; }
            public bool Ricerca_Attiva { get; set; }
            public int Code_Reclutamento { get; set; }
            public int Code_Costruzione { get; set; }
            public int Code_Ricerca { get; set; }
            public int ScudoDellaPace { get; set; }

            // Forza esercito
            public double forza_Esercito { get; set; }

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

            public int Caserma_Guerrieri { get; set; }
            public int Caserma_Lancieri { get; set; }
            public int Caserma_Arceri { get; set; }
            public int Caserma_Catapulte { get; set; }

            // Risorse militari
            public double Spade { get; set; }
            public double Lance { get; set; }
            public double Archi { get; set; }
            public double Scudi { get; set; }
            public double Armature { get; set; }
            public double Frecce { get; set; }

            // Statistiche di combattimento
            public int Unità_Uccise { get; set; }
            public int Guerrieri_Uccisi { get; set; }
            public int Lanceri_Uccisi { get; set; }
            public int Arceri_Uccisi { get; set; }
            public int Catapulte_Uccisi { get; set; }
            public int Unita_Perse { get; set; }
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
            public int Citta_Barbare_Sconfitte { get; set; }
            public int Missioni_Completate { get; set; }
            public int Attacchi_Subiti_PVP { get; set; }
            public int Attacchi_Effettuati_PVP { get; set; }

            public int Unita_Addestrate { get; set; }
            public int Risorse_Utilizzate { get; set; }
            public int Tempo_Addestramento_Risparmiato { get; set; }
            public int Tempo_Costruzione_Risparmiato { get; set; }

            public int Consumo_Cibo_Esercito { get; set; }
            public int Consumo_Oro_Esercito { get; set; }

            // Esercito
            public int Guerrieri { get; set; }
            public int Lancieri { get; set; }
            public int Arceri { get; set; }
            public int Catapulte { get; set; }

            //Ricerca
            public int Ricerca_Produzione { get; set; }
            public int Ricerca_Costruzione { get; set; }
            public int Ricerca_Addestramento { get; set; }
            public int Ricerca_Riparazione { get; set; }

            public int Guerriero_Livello { get; set; }
            public int Guerriero_Salute { get; set; }
            public int Guerriero_Difesa { get; set; }
            public int Guerriero_Attacco { get; set; }

            public int Lanciere_Livello { get; set; }
            public int Lanciere_Salute { get; set; }
            public int Lanciere_Difesa { get; set; }
            public int Lanciere_Attacco { get; set; }

            public int Arciere_Livello { get; set; }
            public int Arciere_Salute { get; set; }
            public int Arciere_Difesa { get; set; }
            public int Arciere_Attacco { get; set; }

            public int catapulta_Livello { get; set; }
            public int catapulta_Salute { get; set; }
            public int catapulta_Difesa { get; set; }
            public int catapulta_Attacco { get; set; }


            // Nuovi dati da salvare
            public int GuerrieriMax { get; set; }
            public int LancieriMax { get; set; }
            public int ArceriMax { get; set; }
            public int CatapulteMax { get; set; }

            public int Salute_Cancello { get; set; }
            public int Salute_CancelloMax { get; set; }
            public int Salute_Mura { get; set; }
            public int Salute_MuraMax { get; set; }
            public int Salute_Torri { get; set; }
            public int Salute_TorriMax { get; set; }
            public int Salute_Castello { get; set; }
            public int Salute_CastelloMax { get; set; }

        }

    }
} 