
using Strategico_V2;

namespace Warrior_and_Wealth
{
    internal class ComandiInvio
    {
        public static void Acquista_TerrenoVirtuale()
        {
            ClientConnection.TestClient.Send($"Costruzione_Terreni|{Variabili_Client.access_Token}");
        }
        public static void Scambia_DiamantiViola(string diamanti)
        {
            ClientConnection.TestClient.Send($"Scambia_Diamanti|{Variabili_Client.access_Token}|{diamanti}");
        }
        public static void Scambia_Tributi(string diamanti)
        {
            ClientConnection.TestClient.Send($"Scambia_Tributi|{Variabili_Client.access_Token}|{diamanti}");
        }
        public static void Costruzione(string Fattoria, string Segheria, string Cava, string MinieraFerro, string MinieraOro, string Case,
             string Workshop_Spade, string Workshop_Lance, string Workshop_Archi, string Workshop_Scudi, string Workshop_Armature, string Workshop_Frecce,
             string Caserme_Guerrieri, string Caserme_Arceri, string Caserme_Lanceri, string Caserme_Catapulte)
        {
            ClientConnection.TestClient.Send($"Costruzione|{Variabili_Client.access_Token}|" +
                $"{Fattoria}|" +
                $"{Segheria}|" +
                $"{Cava}|" +
                $"{MinieraFerro}|" +
                $"{MinieraOro}|" +
                $"{Case}|" +
                $"{Workshop_Spade}|" +
                $"{Workshop_Lance}|" +
                $"{Workshop_Archi}|" +
                $"{Workshop_Scudi}|" +
                $"{Workshop_Armature}|" +
                $"{Workshop_Frecce}|" +
                $"{Caserme_Guerrieri}|" +
                $"{Caserme_Arceri}|" +
                $"{Caserme_Lanceri}|" +
                $"{Caserme_Catapulte}");
        }
        public static void Addestramento(int livello, string Guerrieri, string Arceri, string Lanceri, string Catapulte)
        {
            ClientConnection.TestClient.Send($"Reclutamento|{Variabili_Client.access_Token}|" +
                $"{livello}|" +
                $"{Guerrieri}|" +
                $"{Arceri}|" +
                $"{Lanceri}|" +
                $"{Catapulte}");
        }
        public static void Ricerca(string Ricerca)
        {
            ClientConnection.TestClient.Send($"Ricerca|{Variabili_Client.access_Token}|" +
                $"{Ricerca}");
        }
        public static void Shop(string Comando)
        {
            ClientConnection.TestClient.Send($"Shop|{Variabili_Client.access_Token}|" +
                $"{Comando}");
        }
        public static void Spostamento_Truppe(int Livello, string From, string To, string Guerrieri, string Arceri, string Lanceri, string Catapulte)
        {
            ClientConnection.TestClient.Send($"SpostamentoTruppe|{Variabili_Client.access_Token}|" +
                $"{From}|" +
                $"{To}|" +
                $"{Guerrieri}|" +
                $"{Arceri}|" +
                $"{Lanceri}|" +
                $"{Catapulte}|" +
                $"{Livello}");
        }
        public static void Velocizza(string Comando, string Quantità)
        {
            ClientConnection.TestClient.Send($"Velocizza_Diamanti|{Variabili_Client.access_Token}|" +
                $"{Comando}|" +
                $"{Quantità}");
        }
        public static void Quest_Reward(string Tipo, int Quest)
        {
            ClientConnection.TestClient.Send($"Quest_Reward|{Variabili_Client.access_Token}|" +
                $"{Tipo}|" +
                $"{Quest}");
        }
        public static void AutoLogin(string accessToken, string refreshToken)
        {
            ClientConnection.TestClient.Send($"AutoLogin|{Variabili_Client.access_Token}|" +
                $"{accessToken}|" +
                $"{refreshToken}");
        }
        public static void Login(string Email, string Username, string Pssw, string Lingua)
        {
            ClientConnection.TestClient.Send($"Login|{Variabili_Client.access_Token}|" +
                $"{Username}|" +
                $"{Pssw}|" +
                $"{Lingua}|" +
                $"{Email}");
        }
        public static void NewGame(string Username, string Pssw, string Lingua, string Email)
        {
            ClientConnection.TestClient.Send($"New Player|{Variabili_Client.access_Token}|" +
                $"{Username}|" +
                $"{Pssw}|" +
                $"{Lingua}|" +
                $"{Email}");
        }
        public static void GamePass_DailyReward()
        {
            ClientConnection.TestClient.Send($"GamePass DailyReward|{Variabili_Client.access_Token}");
        }
        public static void Riparazione(string StrutturaDifensiva, string ElementoStatistica)
        {
            ClientConnection.TestClient.Send($"Ripara|{Variabili_Client.access_Token}|" +
                $"{StrutturaDifensiva}|" +
                $"{ElementoStatistica}");
        }
        public static void RiparaTutto(string Dato)
        {
            ClientConnection.TestClient.Send($"Ripara|{Variabili_Client.access_Token}|" +
                $"{Dato}");
        }
    }
}
