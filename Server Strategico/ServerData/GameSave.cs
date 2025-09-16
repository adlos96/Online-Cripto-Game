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
                    Username = player.Username,
                    Password = player.Password,
                    Livello = player.Livello,
                    Esperienza = player.Esperienza,
                    
                    // Risorse
                    Cibo = player.Cibo,
                    Legno = player.Legno,
                    Pietra = player.Pietra,
                    Ferro = player.Ferro,
                    Oro = player.Oro,
                    Popolazione = player.Popolazione,

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

                    ProduzioneSpade = player.Workshop_Spade,
                    ProduzioneLance = player.Workshop_Lance,
                    ProduzioneArchi = player.Workshop_Archi,
                    ProduzioneScudi = player.Workshop_Scudi,
                    ProduzioneArmature = player.Workshop_Armature,
                    ProduzioneFrecce = player.Workshop_Frecce,

                    CasermaGuerrieri = player.Caserma_Guerrieri,
                    CasermaLancieri = player.Caserma_Lancieri,
                    CasermaArceri = player.Caserma_Arceri,
                    CasermaCatapulte = player.Caserma_Catapulte,

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

                    // Aggiungi queste proprietà per le code
                    BuildingQueues = player.GetQueuedBuildings(),
                    RecruitmentQueues = player.GetQueuedUnits(),
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
                    player.Livello = playerData.Livello;
                    player.Esperienza = playerData.Esperienza;
                    
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
                    player.SetBuildings(
                        playerData.Fattoria,
                        playerData.Segheria, 
                        playerData.CavaPietra,
                        playerData.MinieraFerro,
                        playerData.MinieraOro,
                        playerData.Abitazioni,
                        playerData.ProduzioneSpade,
                        playerData.ProduzioneLance,
                        playerData.ProduzioneArchi,
                        playerData.ProduzioneScudi,
                        playerData.ProduzioneArmature,
                        playerData.ProduzioneFrecce,
                        playerData.CasermaGuerrieri,
                        playerData.CasermaLancieri,
                        playerData.CasermaArceri,
                        playerData.CasermaCatapulte
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
                    foreach (var building in playerData.BuildingQueues)
                    {
                        if (building.Value > 0)
                            BuildingManager.Costruzione_1(building.Key, building.Value, player);
                    }
                    if (playerData.BuildingQueues.Count() != 0)
                    {
                        Server.Send(player.guid_Player, $"Log_Server|Strutture in Coda ripristinate\r\n");
                        Console.WriteLine($"Log_Server|Strutture in Coda ripristinate\r\n");
                    }

                    foreach (var unit in playerData.RecruitmentQueues)
                    {
                        if (unit.Value > 0)
                            player.LoadQueueTrainUnits(unit.Key, unit.Value, player);
                    }
                    if (playerData.RecruitmentQueues.Count() != 0)
                    {
                        Server.Send(player.guid_Player, $"Log_Server|Unità in coda ripristinate\r\n");
                        Console.WriteLine($"Log_Server|Unità in coda ripristinate\r\n");
                    }

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
            public string Username { get; set; }
            public string Password { get; set; }
            public int Livello { get; set; }
            public int Esperienza { get; set; }
            
            // Risorse
            public double Cibo { get; set; }
            public double Legno { get; set; }
            public double Pietra { get; set; }
            public double Ferro { get; set; }
            public double Oro { get; set; }
            public double Popolazione { get; set; }

            public double Spade { get; set; }
            public double Lance { get; set; }
            public double Archi { get; set; }
            public double Scudi { get; set; }
            public double Armature { get; set; }
            public double Frecce { get; set; }

            // Edifici
            public int Fattoria { get; set; }
            public int Segheria { get; set; }
            public int CavaPietra { get; set; }
            public int MinieraFerro { get; set; }
            public int MinieraOro { get; set; }
            public int Abitazioni { get; set; }

            public int ProduzioneSpade { get; set; }
            public int ProduzioneLance { get; set; }
            public int ProduzioneArchi { get; set; }
            public int ProduzioneScudi { get; set; }
            public int ProduzioneArmature { get; set; }
            public int ProduzioneFrecce { get; set; }

            public int CasermaGuerrieri { get; set; }
            public int CasermaLancieri { get; set; }
            public int CasermaArceri { get; set; }
            public int CasermaCatapulte { get; set; }

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

            // Aggiungi queste proprietà per le code
            public Dictionary<string, int> BuildingQueues { get; set; }
            public Dictionary<string, int> RecruitmentQueues { get; set; }
        }

    }
} 