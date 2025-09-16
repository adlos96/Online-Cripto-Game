using static Server_Strategico.Gioco.Giocatori;
using static Server_Strategico.Gioco.Giocatori.Player;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Server_Strategico
{
    public class BuildingManager
    {
        public static void Costruzione(string buildingType, int count, Guid clientGuid, Player player)
        {
            var manager = new BuildingManager();
            manager.Coda_Costruzioni(buildingType, count, clientGuid, player);
        }

        public static void Costruzione_1(string buildingType, int count, Player player)
        {
            var manager = new BuildingManager();
            manager.Coda_Costruzioni(buildingType, count, player.guid_Player, player);
        }
        public void Coda_Costruzioni(string buildingType, int count, Guid clientGuid, Player player)
        {
            // Ottieni i costi di costruzione dell'edificio
            var buildingCost = player.GetBuildingCost(buildingType);

            // Verifica se il giocatore ha abbastanza risorse
            if (player.Cibo >= buildingCost.Cibo * count &&
                player.Legno >= buildingCost.Legno * count &&
                player.Pietra >= buildingCost.Pietra * count &&
                player.Ferro >= buildingCost.Ferro * count &&
                player.Oro >= buildingCost.Oro * count)
            {
                // Sottrai le risorse necessarie
                player.Cibo -= buildingCost.Cibo * count;
                player.Legno -= buildingCost.Legno * count;
                player.Pietra -= buildingCost.Pietra * count;
                player.Ferro -= buildingCost.Ferro * count;
                player.Oro -= buildingCost.Oro * count;

                Server.Server.Send(clientGuid, $"Log_Server|Risorse utilizzate per {count} costruzione/i di {buildingType}:\r\n " +
                    $"Cibo= {buildingCost.Cibo * count}, " +
                    $"Legno= {buildingCost.Legno * count}, " +
                    $"Pietra= {buildingCost.Pietra * count}, " +
                    $"Ferro= {buildingCost.Ferro * count}, " +
                    $"Oro= {buildingCost.Oro * count}\r\n");
                Console.WriteLine($"Risorse consumate per {count} costruzione/i di {buildingType}:\r\n Cibo: {buildingCost.Cibo * count}, Legno: {buildingCost.Legno * count}, Pietra: {buildingCost.Pietra * count}, Ferro: {buildingCost.Ferro * count}, Oro: {buildingCost.Oro * count}\r\n");

                // Verifica se la coda di costruzione esiste per questo tipo di edificio, altrimenti creala
                if (!player.constructionQueues.ContainsKey(buildingType))
                    player.constructionQueues[buildingType] = new Queue<Player.ConstructionTask>();

                // Aggiungi i task di costruzione alla coda
                int tempoCostruzioneInSecondi = Convert.ToInt32(buildingCost.TempoCostruzione - player.Ricerca_Costruzione);
                for (int i = 0; i < count; i++)
                    player.constructionQueues[buildingType].Enqueue(new Player.ConstructionTask(buildingType, tempoCostruzioneInSecondi));

                // Inizializza l'entry in currentTasks se non esiste
                if (!player.currentTasks.ContainsKey(buildingType))
                    player.currentTasks[buildingType] = null;

                // Se non c'è nessuna costruzione in corso per questo tipo, inizia la prima
                if (player.currentTasks[buildingType] == null)
                    player.StartNextConstruction(buildingType);
            }
            else
            {
                Server.Server.Send(clientGuid, $"Log_Server|Risorse insufficienti per costruire {count} {buildingType}.");
                Console.WriteLine($"Risorse insufficienti per costruire {count} {buildingType}.");
            }
        }
        public void Load_Coda_Costruzioni(string buildingType, int count, Player player)
        {
            // Ottieni i costi di costruzione dell'edificio
            var buildingCost = player.GetBuildingCost(buildingType);

            if (!player.constructionQueues.ContainsKey(buildingType)) // Verifica se la coda di costruzione esiste per questo tipo di edificio, altrimenti creala
                player.constructionQueues[buildingType] = new Queue<ConstructionTask>();

            // Aggiungi i task di costruzione alla coda
            int tempoCostruzioneInSecondi = Convert.ToInt32(buildingCost.TempoCostruzione - player.Ricerca_Costruzione);
            for (int i = 0; i < count; i++)
                player.constructionQueues[buildingType].Enqueue(new ConstructionTask(buildingType, tempoCostruzioneInSecondi));

            if (!player.currentTasks.ContainsKey(buildingType)) // Inizializza l'entry in currentTasks se non esiste
                player.currentTasks[buildingType] = null;

            if (player.currentTasks[buildingType] == null)  // Se non c'è nessuna costruzione in corso per questo tipo, inizia la prima
                player.StartNextConstruction(buildingType);
        }
    }
}
