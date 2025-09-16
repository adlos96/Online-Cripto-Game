using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Strategico.Gioco
{
    internal class Variabili_Server
    {
        public class Quest
        {
            public string Quest_Description { get; set; }
            public int Require { get; set; }
            public int Max_Complete { get; set; }

            public static Quest Terreno_Virtuale = new Quest
            {
                Quest_Description = "Aquista un terreno vistuale",
                Require = 1,
                Max_Complete = 4
            };
            public static Quest Addestra_Guerrieri = new Quest
            {
                Quest_Description = "Addestra guerrieri",
                Require = 50,
                Max_Complete = 3
            };
            public static Quest Addestra_Lancer = new Quest
            {
                Quest_Description = "Addestra lanceri",
                Require = 50,
                Max_Complete = 3
            };
            public static Quest Addestra_Arceri = new Quest
            {
                Quest_Description = "Addestra arceri",
                Require = 50,
                Max_Complete = 3
            };
            public static Quest Addestra_Catapulte = new Quest
            {
                Quest_Description = "Addestra catapulte",
                Require = 50,
                Max_Complete = 3
            };

            public static Quest Elimina_Guerrieri = new Quest
            {
                Quest_Description = "Elimina guerrieri",
                Require = 50,
                Max_Complete = 3
            };
            public static Quest Elimina_Lancer = new Quest
            {
                Quest_Description = "Elimina lanceri",
                Require = 50,
                Max_Complete = 3
            };
            public static Quest Elimina_Arceri = new Quest
            {
                Quest_Description = "Elimina arceri",
                Require = 50,
                Max_Complete = 3
            };
            public static Quest Elimina_Catapulte = new Quest
            {
                Quest_Description = "Elimina catapulte",
                Require = 50,
                Max_Complete = 3
            };

            public static Quest Elimina_Truppe = new Quest
            {
                Quest_Description = "Elimina qualsiasi unità",
                Require = 500,
                Max_Complete = 3
            };

            public static Quest Costruisci_Civile = new Quest
            {
                Quest_Description = "Costruisci qualsiasi struttura civile",
                Require = 15,
                Max_Complete = 4
            };

            public static Quest Costruisci_Fattorie = new Quest
            {
                Quest_Description = "Bisogno di grano, Costruisci fattorie",
                Require = 5,
                Max_Complete = 4
            };
            public static Quest Costruisci_Segherie = new Quest
            {
                Quest_Description = "Bisogno di legno, Costruisci segherie",
                Require = 5,
                Max_Complete = 4
            };
            public static Quest Costruisci_Cava_Pietra = new Quest
            {
                Quest_Description = "Bisogno di pietra, Costruisci cava di pietra",
                Require = 5,
                Max_Complete = 4
            };
            public static Quest Costruisci_Miniere_Ferro = new Quest
            {
                Quest_Description = "Bisogno di ferro, costruisci miniere ferro",
                Require = 5,
                Max_Complete = 4
            };
            public static Quest Costruisci_Miniere_Oro = new Quest
            {
                Quest_Description = "Bisogno di oro, costruisci miniere d'oro",
                Require = 5,
                Max_Complete = 4
            };
            public static Quest Costruisci_Case = new Quest
            {
                Quest_Description = "Bisogno di popolazione, costruisci case",
                Require = 5,
                Max_Complete = 4
            };

            public static Quest Costruisci_Militare = new Quest
            {
                Quest_Description = "Costruisci qualsiasi struttura civile",
                Require = 15,
                Max_Complete = 4
            };

        }
        public class Quest_Reward
        {
            #region Reward
            public string Reward_1 { get; set; }
            public string Reward_2 { get; set; }
            public string Reward_3 { get; set; }
            public string Reward_4 { get; set; }
            public string Reward_5 { get; set; }
            public string Reward_6 { get; set; }
            public string Reward_7 { get; set; }
            public string Reward_8 { get; set; }
            public string Reward_9 { get; set; }
            public string Reward_10 { get; set; }
            public string Reward_11 { get; set; }
            public string Reward_12 { get; set; }
            public string Reward_13 { get; set; }
            public string Reward_14 { get; set; }
            public string Reward_15 { get; set; }
            public string Reward_16 { get; set; }
            public string Reward_17 { get; set; }
            public string Reward_18 { get; set; }
            public string Reward_19 { get; set; }
            public string Reward_20 { get; set; }
            #endregion
            #region Punti reward
            public string Points_1 { get; set; }
            public string Points_2 { get; set; }
            public string Points_3 { get; set; }
            public string Points_4 { get; set; }
            public string Points_5 { get; set; }
            public string Points_6 { get; set; }
            public string Points_7 { get; set; }
            public string Points_8 { get; set; }
            public string Points_9 { get; set; }
            public string Points_10 { get; set; }
            public string Points_11 { get; set; }
            public string Points_12 { get; set; }
            public string Points_13 { get; set; }
            public string Points_14 { get; set; }
            public string Points_15 { get; set; }
            public string Points_16 { get; set; }
            public string Points_17 { get; set; }
            public string Points_18 { get; set; }
            public string Points_19 { get; set; }
            public string Points_20 { get; set; }
            #endregion
            #region Reward Claimed
            public bool Reward_Claim_1 { get; set; }
            public bool Reward_Claim_2 { get; set; }
            public bool Reward_Claim_3 { get; set; }
            public bool Reward_Claim_4 { get; set; }
            public bool Reward_Claim_5 { get; set; }
            public bool Reward_Claim_6 { get; set; }
            public bool Reward_Claim_7 { get; set; }
            public bool Reward_Claim_8 { get; set; }
            public bool Reward_Claim_9 { get; set; }
            public bool Reward_Claim_10 { get; set; }
            public bool Reward_Claim_11 { get; set; }
            public bool Reward_Claim_12 { get; set; }
            public bool Reward_Claim_13 { get; set; }
            public bool Reward_Claim_14 { get; set; }
            public bool Reward_Claim_15 { get; set; }
            public bool Reward_Claim_16 { get; set; }
            public bool Reward_Claim_17 { get; set; }
            public bool Reward_Claim_18 { get; set; }
            public bool Reward_Claim_19 { get; set; }
            public bool Reward_Claim_20 { get; set; }
            #endregion
            public static Quest_Reward Normali_Montly = new Quest_Reward //Quantità di ricompense x livello F2P. Es 1 = 15, 2 = 20, 3 = 25
            {
                Reward_1 = "5",     //Diamanti_Viola
                Reward_2 = "10",    //Diamanti_Blu
                Reward_3 = "7",     //Diamanti_Viola
                Reward_4 = "15",    //Diamanti_Blu
                Reward_5 = "9",     //Diamanti_Viola
                Reward_6 = "13",    //Diamanti_Viola
                Reward_7 = "19",    //Diamanti_Blu
                Reward_8 = "17",    //Diamanti_Viola
                Reward_9 = "25",    //Diamanti_Viola
                Reward_10 = "30",   //Diamanti_Viola
                Reward_11 = "23",   //Diamanti_Blu
                Reward_12 = "40",   //Diamanti_Viola
                Reward_13 = "50",   //Diamanti_Viola
                Reward_14 = "27",    //Diamanti_Blu
                Reward_15 = "60",   //Diamanti_Viola
                Reward_16 = "80",   //Diamanti_Viola
                Reward_17 = "34",    //Diamanti_Blu
                Reward_18 = "110",   //Diamanti_Viola
                Reward_19 = "130",   //Diamanti_Viola
                Reward_20 = "150"    //Diamanti_Viola
            };
            public static Quest_Reward Vip_Montly = new Quest_Reward //Quantità di ricompense x livello VIP. Es 1 = 15, 2 = 20, 3 = 25
            {
                Reward_1 = "1",    //Diamanti_Viola
                Reward_2 = "2",    //Diamanti_Viola
                Reward_3 = "60",    //Diamanti_Blu
                Reward_4 = "0",    //Diamanti_Viola
                Reward_5 = "0",    //Diamanti_Viola
                Reward_6 = "0",    //Diamanti_Viola
                Reward_7 = "0",    //Diamanti_Viola
                Reward_8 = "0",    //Diamanti_Viola
                Reward_9 = "0",    //Diamanti_Viola
                Reward_10 = "0",   //Diamanti_Viola
                Reward_11 = "0",   //Diamanti_Viola
                Reward_12 = "0",   //Diamanti_Viola
                Reward_13 = "0",   //Diamanti_Viola
                Reward_14 = "0",   //Diamanti_Viola
                Reward_15 = "0",   //Diamanti_Viola
                Reward_16 = "0",   //Diamanti_Viola
                Reward_17 = "0",   //Diamanti_Viola
                Reward_18 = "0",   //Diamanti_Viola
                Reward_19 = "0",   //Diamanti_Viola
                Reward_20 = "0"    //Special - non sò cosa
            };
            public static Quest_Reward Point_Montly = new Quest_Reward //Quantità punti richiesti per livello
            {
                Points_1 = "20",
                Points_2 = "60",
                Points_3 = "120",
                Points_4 = "180",
                Points_5 = "250",
                Points_6 = "320",
                Points_7 = "450",
                Points_8 = "680",
                Points_9 = "820",
                Points_10 = "980",
                Points_11 = "1130",
                Points_12 = "1250",
                Points_13 = "1400",
                Points_14 = "1680",
                Points_15 = "1810",
                Points_16 = "1920",
                Points_17 = "2150",
                Points_18 = "2375",
                Points_19 = "2500",
                Points_20 = "3000"
            };
            public static Quest_Reward Montly_Claim_Normal = new Quest_Reward //Quantità di ricompense x livello F2P. Es 1 = 15, 2 = 20, 3 = 25
            {
                Reward_Claim_1 = false,
                Reward_Claim_2 = false,
                Reward_Claim_3 = false,
                Reward_Claim_4 = false,
                Reward_Claim_5 = false,
                Reward_Claim_6 = false,
                Reward_Claim_7 = false,
                Reward_Claim_8 = false,
                Reward_Claim_9 = false,
                Reward_Claim_10 = false,
                Reward_Claim_11 = false,
                Reward_Claim_12 = false,
                Reward_Claim_13 = false,
                Reward_Claim_14 = false,
                Reward_Claim_15 = false,
                Reward_Claim_16 = false,
                Reward_Claim_17 = false,
                Reward_Claim_18 = false,
                Reward_Claim_19 = false,
                Reward_Claim_20 = false
            };
            public static Quest_Reward Montly_Claim_Vip = new Quest_Reward //Quantità di ricompense x livello F2P. Es 1 = 15, 2 = 20, 3 = 25
            {
                Reward_Claim_1 = false,
                Reward_Claim_2 = false,
                Reward_Claim_3 = false,
                Reward_Claim_4 = false,
                Reward_Claim_5 = false,
                Reward_Claim_6 = false,
                Reward_Claim_7 = false,
                Reward_Claim_8 = false,
                Reward_Claim_9 = false,
                Reward_Claim_10 = false,
                Reward_Claim_11 = false,
                Reward_Claim_12 = false,
                Reward_Claim_13 = false,
                Reward_Claim_14 = false,
                Reward_Claim_15 = false,
                Reward_Claim_16 = false,
                Reward_Claim_17 = false,
                Reward_Claim_18 = false,
                Reward_Claim_19 = false,
                Reward_Claim_20 = false
            };
        }
        public class Shop
        {
            public double Costo { get; set; }
            public int Reward { get; set; }

            public static Shop Vip_1 = new Shop
            {
                Costo = 500, //Diamanti_Viola
                Reward = 1 //VIP
            };
            public static Shop Vip_2 = new Shop
            {
                Costo = 14.99, //USDT
                Reward = 1 //VIP
            };

            public static Shop Pacchetto_1 = new Shop
            {
                Costo = 5.99, //USDT
                Reward = 150 //Diamanti_Viola
            };
            public static Shop Pacchetto_2 = new Shop
            {
                Costo = 14.99,
                Reward = 475
            };
            public static Shop Pacchetto_3 = new Shop
            {
                Costo = 24.99,
                Reward = 800
            };
            public static Shop Pacchetto_4 = new Shop
            {
                Costo = 49.99,
                Reward = 1500
            };


        }
        public class Terreni_Virtuali
        {
            public double Produzione { get; set; }
            public int Rarita { get; set; }
            public static Terreni_Virtuali Comune = new Terreni_Virtuali
            {
                Produzione = 0.00000000111,
                Rarita = 50
            };
            public static Terreni_Virtuali NonComune = new Terreni_Virtuali
            {
                Produzione = 0.00000000222,
                Rarita = 20
            };
            public static Terreni_Virtuali Raro = new Terreni_Virtuali
            {
                Produzione = 0.00000000333,
                Rarita = 15
            };
            public static Terreni_Virtuali Epico = new Terreni_Virtuali
            {
                Produzione = 0.00000000444,
                Rarita = 10
            };

            public static Terreni_Virtuali Leggendario = new Terreni_Virtuali
            {
                Produzione = 0.00000000555,
                Rarita = 5
            };
        }
    }
}
