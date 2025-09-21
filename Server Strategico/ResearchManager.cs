
using Server_Strategico.Gioco;
using static Server_Strategico.Gioco.Giocatori;
using static Server_Strategico.Gioco.Ricerca;

namespace Server_Strategico
{
    public class ResearchManager
    {
        public static void Ricerca(string research, Guid clientGuid, Player player)
        {
            var manager = new ResearchManager();
            manager.StartResearch(research, clientGuid, player);
        }
        public void StartResearch(string researchType, Guid clientGuid, Player player)
        {
            // Calcola il livello successivo
            int livello = researchType switch
            {
                "Ricerca_Addestramento" => player.Ricerca_Addestramento + 1,
                "Ricerca_Costruzione" => player.Ricerca_Costruzione + 1,
                "Ricerca_Produzione" => player.Ricerca_Produzione + 1,
                _ => 1
            };

            // Controllo livello per Addestramento
            if (researchType == "Ricerca_Addestramento" && player.Livello < livello * 3)
            {
                Server.Server.Send(clientGuid, $"Log_Server|La ricerca [Addestramento {livello}] richiede livello minimo {livello * 3}");
                return;
            }

            // Ottieni costi dinamici
            var researchCost = GetResearchCost(researchType, livello);
            if (researchCost == null)
            {
                Server.Server.Send(clientGuid, $"Log_Server|Tipo ricerca {researchType} non valido!");
                return;
            }

            // Controllo risorse
            if (player.Cibo >= researchCost.Cibo &&
                player.Legno >= researchCost.Legno &&
                player.Pietra >= researchCost.Pietra &&
                player.Ferro >= researchCost.Ferro &&
                player.Oro >= researchCost.Oro)
            {
                // Scala risorse
                player.Cibo -= researchCost.Cibo;
                player.Legno -= researchCost.Legno;
                player.Pietra -= researchCost.Pietra;
                player.Ferro -= researchCost.Ferro;
                player.Oro -= researchCost.Oro;

                Server.Server.Send(clientGuid, $"Log_Server|Risorse utilizzate per la ricerca {researchType} livello {livello}...");
                Console.WriteLine($"Risorse utilizzate per {researchType} livello {livello}: " +
                                  $"Cibo={researchCost.Cibo}, Legno={researchCost.Legno}, Pietra={researchCost.Pietra}, " +
                                  $"Ferro={researchCost.Ferro}, Oro={researchCost.Oro}");

                int tempoRicercaInSecondi = Math.Max(1, Convert.ToInt32(researchCost.TempoRicerca));

                // Inserisci nella coda
                player.research_Queue.Enqueue(new BuildingManager.ConstructionTask(researchType, tempoRicercaInSecondi));

                StartNextResearch(player, clientGuid);
            }
            else
            {
                Server.Server.Send(clientGuid, $"Log_Server|Risorse insufficienti per la ricerca {researchType} livello {livello}.");
            }
        }

        // Avvia i task finché ci sono slot liberi
        private static void StartNextResearch(Player player, Guid clientGuid)
        {
            int maxSlots = 1; // Parametrizzabile
            player.Ricerca_Attiva = true;// blocca i pulsanti ricerca del client
            while (player.currentTasks_Research.Count < maxSlots && player.research_Queue.Count > 0)
            {
                var nextTask = player.research_Queue.Dequeue();
                nextTask.Start();
                player.currentTasks_Research.Add(nextTask);

                Console.WriteLine($"Ricerca di {nextTask.Type} iniziata, durata {nextTask.DurationInSeconds}s");
                Server.Server.Send(clientGuid, $"Log_Server|Ricerca di {nextTask.Type} iniziata.");
            }
        }

        // Completa le ricerche terminate
        public static void CompleteResearch(Guid clientGuid, Player player)
        {
            for (int i = player.currentTasks_Research.Count - 1; i >= 0; i--)
            {
                var task = player.currentTasks_Research[i];
                if (task.IsComplete())
                {
                    ApplyResearchEffects(task.Type, player);
                    player.Ricerca_Attiva = false; // sblocca i pulsanti ricerca del client
                    Console.WriteLine($"Ricerca completata: {task.Type}");
                    Server.Server.Send(clientGuid, $"Log_Server|Ricerca completata: {task.Type}");

                    player.currentTasks_Research.RemoveAt(i);
                }
            }

            StartNextResearch(player, clientGuid);
        }

        // Funzione dove applichi gli effetti della ricerca
        private static void ApplyResearchEffects(string researchType, Player player)
        {
            switch (researchType)
            {
                case "Ricerca_Addestramento":
                    player.Ricerca_Addestramento++;
                    break;
                case "Ricerca_Costruzione":
                    player.Ricerca_Costruzione++;
                    break;
                case "Ricerca_Produzione":
                    player.Ricerca_Produzione++;
                    break;
                default:
                    Console.WriteLine($"Ricerca {researchType} non definita negli effetti!");
                    break;
            }
        }

        // Totale tempo ricerche in coda + in corso
        public static string GetTotalResearchTime(Player player)
        {
            double total = 0;
            foreach (var task in player.currentTasks_Research)
                total += task.GetRemainingTime();
            foreach (var task in player.research_Queue)
                total += task.DurationInSeconds;
            return TimeSpan.FromSeconds(total).ToString(@"hh\:mm\:ss");
        }
        private ResearchCost GetResearchCost(string researchType, int livello)
        {
            return researchType switch
            {
                "Ricerca_Addestramento" => new ResearchCost
                {
                    Cibo = Tipi.Addestramento.Cibo * livello,
                    Legno = Tipi.Addestramento.Legno * livello,
                    Pietra = Tipi.Addestramento.Pietra * livello,
                    Ferro = Tipi.Addestramento.Ferro * livello,
                    Oro = Tipi.Addestramento.Oro * livello,
                    TempoRicerca = 60 // o calcola dinamicamente
                },
                "Ricerca_Costruzione" => new ResearchCost
                {
                    Cibo = Tipi.Costruzione.Cibo * livello,
                    Legno = Tipi.Costruzione.Legno * livello,
                    Pietra = Tipi.Costruzione.Pietra * livello,
                    Ferro = Tipi.Costruzione.Ferro * livello,
                    Oro = Tipi.Costruzione.Oro * livello,
                    TempoRicerca = 60
                },
                "Ricerca_Produzione" => new ResearchCost
                {
                    Cibo = Tipi.Produzione.Cibo * livello,
                    Legno = Tipi.Produzione.Legno * livello,
                    Pietra = Tipi.Produzione.Pietra * livello,
                    Ferro = Tipi.Produzione.Ferro * livello,
                    Oro = Tipi.Produzione.Oro * livello,
                    TempoRicerca = 60
                },
                _ => null
            };
        }
        public class ResearchCost
        {
            public double Cibo { get; set; }
            public double Legno { get; set; }
            public double Pietra { get; set; }
            public double Ferro { get; set; }
            public double Oro { get; set; }
            public double TempoRicerca { get; set; } = 60; // default 60 secondi
        }
    }
}
