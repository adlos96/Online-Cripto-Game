using static Server_Strategico.Gioco.Esercito;
using static Server_Strategico.Server.Server;

namespace Server_Strategico.Gioco
{
    internal class Ricerca
    {
        public class Tipi
        {
            public static CostoReclutamento Produzione = new CostoReclutamento
            {
                Cibo = 4500,
                Legno = 4250,
                Pietra = 4000,
                Ferro = 3750,
                Oro = 3500,
            };
            public static CostoReclutamento Costruzione = new CostoReclutamento
            {
                Cibo = 3500,
                Legno = 3250,
                Pietra = 3000,
                Ferro = 2750,
                Oro = 2550,
            };
            public static CostoReclutamento Addestramento = new CostoReclutamento
            {
                Cibo = 5500,
                Legno = 5000,
                Pietra = 4500,
                Ferro = 4250,
                Oro = 4000,
            };

            public static CostoReclutamento Incremento = new CostoReclutamento
            {
                Cibo = 0.12,
                Legno = 0.10,
                Pietra = 0.08,
                Ferro = 0.06,
                Oro = 0.04,
                Popolazione = 0.001
            };

        }
        public class Soldati
        {
            public static CostoReclutamento Salute = new CostoReclutamento
            {
                Cibo = 4000,
                Legno = 3750,
                Pietra = 3500,
                Ferro = 3250,
                Oro = 3000,
            };
            public static CostoReclutamento Difesa = new CostoReclutamento
            {
                Cibo = 3000,
                Legno = 2750,
                Pietra = 2500,
                Ferro = 2500,
                Oro = 2250,
            };
            public static CostoReclutamento Attacco = new CostoReclutamento
            {
                Cibo = 5000,
                Legno = 4500,
                Pietra = 4000,
                Ferro = 3750,
                Oro = 3500,
            };
            public static CostoReclutamento Livello = new CostoReclutamento
            {
                Cibo = 6000,
                Legno = 5750,
                Pietra = 5500,
                Ferro = 5250,
                Oro = 5000,
            };

            public static Unità Incremento = new Unità
            {
                Salute = 1,
                Difesa = 1,
                Attacco = 1
            };

        }

        public static async Task<bool> Ricerca_Produzione(Giocatori.Player player, Guid clientGuid)
        {
            int livello = player.Ricerca_Produzione + 1;
            if (player.Cibo >= Tipi.Produzione.Cibo * livello &&
                player.Legno >= Tipi.Produzione.Legno * livello &&
                player.Pietra >= Tipi.Produzione.Pietra * livello &&
                player.Ferro >= Tipi.Produzione.Ferro * livello &&
                player.Oro >= Tipi.Produzione.Oro * livello)
            {
                // Sottrai le risorse necessarie
                player.Cibo -= Tipi.Produzione.Cibo * livello;
                player.Legno -= Tipi.Produzione.Legno * livello;
                player.Pietra -= Tipi.Produzione.Pietra * livello;
                player.Ferro -= Tipi.Produzione.Ferro * livello;
                player.Oro -= Tipi.Produzione.Oro * livello;

                Send(clientGuid, $"Log_Server|Risorse utilizzate per la ricerca di [Produzione: {livello}]\r\n " +
                    $"Cibo= {Tipi.Produzione.Cibo * livello}," +
                    $" Legno= {Tipi.Produzione.Legno * livello}," +
                    $" Pietra= {Tipi.Produzione.Pietra * livello}," +
                    $" Ferro= {Tipi.Produzione.Ferro * livello}," +
                    $" Oro= {Tipi.Produzione.Oro * livello}\r\n");
                Console.WriteLine($"Risorse utilizzate per la ricerca di Produzione {livello}:\r\n " +
                    $"Cibo= {Tipi.Produzione.Cibo * livello}, " +
                    $"Legno= {Tipi.Produzione.Legno * livello}, " +
                    $"Pietra= {Tipi.Produzione.Pietra * livello}, " +
                    $"Ferro= {Tipi.Produzione.Ferro * livello}, " +
                    $"Oro= {Tipi.Produzione.Oro * livello}\r\n");

                player.Ricerca_Produzione++;
            }else
            {
                Send(clientGuid, $"Log_Server|Risorse insufficienti per la ricerca di [Produzione: {livello}]\r\n " +
                    $"Cibo= {Tipi.Produzione.Cibo * livello}," +
                    $" Legno= {Tipi.Produzione.Legno * livello}," +
                    $" Pietra= {Tipi.Produzione.Pietra * livello}," +
                    $" Ferro= {Tipi.Produzione.Ferro * livello}," +
                    $" Oro= {Tipi.Produzione.Oro * livello}\r\n");
                Console.WriteLine($"Risorse insufficienti per la ricerca di Produzione {livello}:\r\n " +
                    $"Cibo= {Tipi.Produzione.Cibo * livello}, " +
                    $"Legno= {Tipi.Produzione.Legno * livello}, " +
                    $"Pietra= {Tipi.Produzione.Pietra * livello}, " +
                    $"Ferro= {Tipi.Produzione.Ferro * livello}, " +
                    $"Oro= {Tipi.Produzione.Oro * livello}\r\n");
            }
            return false;
        }
        public static async Task<bool> Ricerca_Costruzione(Giocatori.Player player, Guid clientGuid)
        {
            int livello = player.Ricerca_Costruzione + 1;
            if (player.Cibo >= Tipi.Costruzione.Cibo        * livello &&
                player.Legno >= Tipi.Costruzione.Legno      * livello &&
                player.Pietra >= Tipi.Costruzione.Pietra    * livello &&
                player.Ferro >= Tipi.Costruzione.Ferro      * livello &&
                player.Oro >= Tipi.Costruzione.Oro          * livello)
            {
                // Sottrai le risorse necessarie
                player.Cibo -= Tipi.Costruzione.Cibo        * livello;
                player.Legno -= Tipi.Costruzione.Legno      * livello;
                player.Pietra -= Tipi.Costruzione.Pietra    * livello;
                player.Ferro -= Tipi.Costruzione.Ferro      * livello;
                player.Oro -= Tipi.Costruzione.Oro          * livello;

                Send(clientGuid, $"Log_Server|Risorse utilizzate per la ricerca di [Costruzione: {livello}]\r\n " +
                    $"Cibo= {Tipi.Costruzione.Cibo      * livello}," +
                    $" Legno= {Tipi.Costruzione.Legno * livello}," +
                    $" Pietra= {Tipi.Costruzione.Pietra * livello}," +
                    $" Ferro= {Tipi.Costruzione.Ferro * livello}," +
                    $" Oro= {Tipi.Costruzione.Oro * livello}\r\n");
                Console.WriteLine($"Risorse utilizzate per la ricerca di Costruzione {livello}:\r\n " +
                    $"Cibo= {Tipi.Costruzione.Cibo * livello}, " +
                    $"Legno= {Tipi.Costruzione.Legno * livello}, " +
                    $"Pietra= {Tipi.Costruzione.Pietra * livello}, " +
                    $"Ferro= {Tipi.Costruzione.Ferro * livello}, " +
                    $"Oro= {Tipi.Costruzione.Oro * livello}\r\n");

                player.Ricerca_Costruzione++;
                return true;
            }
            else
            {
                Send(clientGuid, $"Log_Server|Risorse insufficienti per la ricerca di [Costruzione: {livello}]\r\n " +
                    $"Cibo= {Tipi.Costruzione.Cibo * livello}," +
                    $" Legno= {Tipi.Costruzione.Legno * livello}," +
                    $" Pietra= {Tipi.Costruzione.Pietra * livello}," +
                    $" Ferro= {Tipi.Costruzione.Ferro * livello}," +
                    $" Oro= {Tipi.Costruzione.Oro * livello}\r\n");
                Console.WriteLine($"Risorse insufficienti per la ricerca di Costruzione {livello}:\r\n " +
                    $"Cibo= {Tipi.Costruzione.Cibo * livello}, " +
                    $"Legno= {Tipi.Costruzione.Legno * livello}, " +
                    $"Pietra= {Tipi.Costruzione.Pietra * livello}, " +
                    $"Ferro= {Tipi.Costruzione.Ferro * livello}, " +
                    $"Oro= {Tipi.Costruzione.Oro * livello}\r\n");
                return false;
            }
        }
        public static async Task<bool> Ricerca_Addestramento(Giocatori.Player player, Guid clientGuid)
        {
            int livello = player.Ricerca_Addestramento + 1;
            int valore = player.Ricerca_Addestramento * 3;

            if (player.Livello < valore)
            {
                Send(clientGuid, $"Log_Server|La ricerca [Addestramento {livello}], richiede che il giocatore sia almeno di livello: {valore}\r\n");
                Console.WriteLine($"La ricerca [Addestramento {livello}], richiede che il giocatore sia almeno di livello: {valore}\r\n");
                return false;
            }
            if (player.Cibo >= Tipi.Addestramento.Cibo * livello &&
                player.Legno >= Tipi.Addestramento.Legno * livello &&
                player.Pietra >= Tipi.Addestramento.Pietra * livello &&
                player.Ferro >= Tipi.Addestramento.Ferro * livello &&
                player.Oro >= Tipi.Addestramento.Oro * livello)
            {
                // Sottrai le risorse necessarie
                player.Cibo -= Tipi.Addestramento.Cibo * livello;
                player.Legno -= Tipi.Addestramento.Legno * livello;
                player.Pietra -= Tipi.Addestramento.Pietra * livello;
                player.Ferro -= Tipi.Addestramento.Ferro * livello;
                player.Oro -= Tipi.Addestramento.Oro * livello;

                Send(clientGuid, $"Log_Server|Risorse utilizzate per la ricerca di [Addestramento: {livello}]\r\n " +
                    $"Cibo= {Tipi.Addestramento.Cibo * livello}," +
                    $" Legno= {Tipi.Addestramento.Legno * livello}," +
                    $" Pietra= {Tipi.Addestramento.Pietra * livello}," +
                    $" Ferro= {Tipi.Addestramento.Ferro * livello}," +
                    $" Oro= {Tipi.Addestramento.Oro * livello}\r\n");
                Console.WriteLine($"Risorse utilizzate per la ricerca di [Addestramento: {livello}]\r\n " +
                    $"Cibo= {Tipi.Addestramento.Cibo * livello}, " +
                    $"Legno= {Tipi.Addestramento.Legno * livello}, " +
                    $"Pietra= {Tipi.Addestramento.Pietra * livello}, " +
                    $"Ferro= {Tipi.Addestramento.Ferro * livello}, " +
                    $"Oro= {Tipi.Addestramento.Oro * livello}\r\n");

                player.Ricerca_Addestramento++;
                return true;
            }
            else
            {
                Send(clientGuid, $"Log_Server|Risorse insufficienti per la ricerca di [Addestramento: {livello}]\r\n " +
                    $"Cibo= {Tipi.Addestramento.Cibo * livello}," +
                    $" Legno= {Tipi.Addestramento.Legno * livello}," +
                    $" Pietra= {Tipi.Addestramento.Pietra * livello}," +
                    $" Ferro= {Tipi.Addestramento.Ferro * livello}," +
                    $" Oro= {Tipi.Addestramento.Oro * livello}\r\n");
                Console.WriteLine($"Risorse insufficienti per la ricerca di Addestramento {livello}:\r\n " +
                    $"Cibo= {Tipi.Addestramento.Cibo * livello}, " +
                    $"Legno= {Tipi.Addestramento.Legno * livello}, " +
                    $"Pietra= {Tipi.Addestramento.Pietra * livello}, " +
                    $"Ferro= {Tipi.Addestramento.Ferro * livello}, " +
                    $"Oro= {Tipi.Addestramento.Oro * livello}\r\n");
                return false;
            }
        }

        public static async Task<bool> Ricerca_Truppe(Giocatori.Player player, Guid clientGuid, string tipo, string unità)
        {
            int livello = 0, valore = 0;
            switch (unità)
            {
                case "Guerriero":
                    if (tipo == "Salute")
                    {
                        livello = player.Guerriero_Salute + 1;
                        valore = livello;

                        if (livello == 0) livello = 1;
                        if (valore == 0) valore = 2;

                        if (player.Guerriero_Livello < valore * 2)
                        {
                            Send(clientGuid, $"Log_Server|La ricerca [{tipo} {unità} {livello}], richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            Console.WriteLine($"La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            return false;
                        }

                        if (player.Cibo >= Soldati.Salute.Cibo * livello &&
                            player.Legno >= Soldati.Salute.Legno * livello &&
                            player.Pietra >= Soldati.Salute.Pietra * livello &&
                            player.Ferro >= Soldati.Salute.Ferro * livello &&
                            player.Oro >= Soldati.Salute.Oro * livello)
                        {
                            // Sottrai le risorse necessarie
                            player.Cibo -= Soldati.Salute.Cibo * livello;
                            player.Legno -= Soldati.Salute.Legno * livello;
                            player.Pietra -= Soldati.Salute.Pietra * livello;
                            player.Ferro -= Soldati.Salute.Ferro * livello;
                            player.Oro -= Soldati.Salute.Oro * livello;

                            Send(clientGuid, $"Log_Server|Risorse utilizzate per la ricerca di: [{tipo} {unità} {livello}]\r\n " +
                                $"Cibo= {Soldati.Salute.Cibo * livello}," +
                                $" Legno= {Soldati.Salute.Legno * livello}," +
                                $" Pietra= {Soldati.Salute.Pietra * livello}," +
                                $" Ferro= {Soldati.Salute.Ferro * livello}," +
                                $" Oro= {Soldati.Salute.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Salute.Cibo * livello}, " +
                                $"Legno= {Soldati.Salute.Legno * livello}, " +
                                $"Pietra= {Soldati.Salute.Pietra * livello}, " +
                                $"Ferro= {Soldati.Salute.Ferro * livello}, " +
                                $"Oro= {Soldati.Salute.Oro * livello}\r\n");

                            player.Guerriero_Salute++;
                            return true;
                        }
                        else
                        {
                            Send(clientGuid, $"Log_Server|Risorse insufficienti per la ricerca di [{tipo} {unità} {livello}]\r\n " +
                                $"Cibo= {Soldati.Salute.Cibo * livello}," +
                                $" Legno= {Soldati.Salute.Legno * livello}," +
                                $" Pietra= {Soldati.Salute.Pietra * livello}," +
                                $" Ferro= {Soldati.Salute.Ferro * livello}," +
                                $" Oro= {Soldati.Salute.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Salute.Cibo * livello}, " +
                                $"Legno= {Soldati.Salute.Legno * livello}, " +
                                $"Pietra= {Soldati.Salute.Pietra * livello}, " +
                                $"Ferro= {Soldati.Salute.Ferro * livello}, " +
                                $"Oro= {Soldati.Salute.Oro * livello}\r\n");
                            return false;
                        }
                    }
                    else if (tipo == "Difesa")
                    {
                        livello = player.Guerriero_Difesa + 1;
                        valore = livello;

                        if (livello == 0) livello = 1;
                        if (valore == 0) valore = 2;

                        if (player.Guerriero_Livello < valore * 2)
                        {
                            Send(clientGuid, $"Log_Server|La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            Console.WriteLine($"La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            return false;
                        }

                        if (player.Cibo >= Soldati.Difesa.Cibo * livello &&
                            player.Legno >= Soldati.Difesa.Legno * livello &&
                            player.Pietra >= Soldati.Difesa.Pietra * livello &&
                            player.Ferro >= Soldati.Difesa.Ferro * livello &&
                            player.Oro >= Soldati.Difesa.Oro * livello)
                        {
                            // Sottrai le risorse necessarie
                            player.Cibo -= Soldati.Difesa.Cibo * livello;
                            player.Legno -= Soldati.Difesa.Legno * livello;
                            player.Pietra -= Soldati.Difesa.Pietra * livello;
                            player.Ferro -= Soldati.Difesa.Ferro * livello;
                            player.Oro -= Soldati.Difesa.Oro * livello;

                            Send(clientGuid, $"Log_Server|Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Difesa.Cibo * livello}," +
                                $" Legno= {Soldati.Difesa.Legno * livello}," +
                                $" Pietra= {Soldati.Difesa.Pietra * livello}," +
                                $" Ferro= {Soldati.Difesa.Ferro * livello}," +
                                $" Oro= {Soldati.Difesa.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Difesa.Cibo * livello}, " +
                                $"Legno= {Soldati.Difesa.Legno * livello}, " +
                                $"Pietra= {Soldati.Difesa.Pietra * livello}, " +
                                $"Ferro= {Soldati.Difesa.Ferro * livello}, " +
                                $"Oro= {Soldati.Difesa.Oro * livello}\r\n");

                            player.Guerriero_Difesa++;
                            return true;
                        }
                        else
                        {
                            Send(clientGuid, $"Log_Server|Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Difesa.Cibo * livello}," +
                                $" Legno= {Soldati.Difesa.Legno * livello}," +
                                $" Pietra= {Soldati.Difesa.Pietra * livello}," +
                                $" Ferro= {Soldati.Difesa.Ferro * livello}," +
                                $" Oro= {Soldati.Difesa.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Difesa.Cibo * livello}, " +
                                $"Legno= {Soldati.Difesa.Legno * livello}, " +
                                $"Pietra= {Soldati.Difesa.Pietra * livello}, " +
                                $"Ferro= {Soldati.Difesa.Ferro * livello}, " +
                                $"Oro= {Soldati.Difesa.Oro * livello}\r\n");
                            return false;
                        }
                    }
                    else if (tipo == "Attacco")
                    {
                        livello = player.Guerriero_Attacco + 1;
                        valore = livello;

                        if (livello == 0) livello = 1;
                        if (valore == 0) valore = 2;

                        if (player.Guerriero_Livello < valore * 2)
                        {
                            Send(clientGuid, $"Log_Server|La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            Console.WriteLine($"La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            return false;
                        }

                        if (player.Cibo >= Soldati.Attacco.Cibo * livello &&
                            player.Legno >= Soldati.Attacco.Legno * livello &&
                            player.Pietra >= Soldati.Attacco.Pietra * livello &&
                            player.Ferro >= Soldati.Attacco.Ferro * livello &&
                            player.Oro >= Soldati.Attacco.Oro * livello)
                        {
                            // Sottrai le risorse necessarie
                            player.Cibo -= Soldati.Attacco.Cibo * livello;
                            player.Legno -= Soldati.Attacco.Legno * livello;
                            player.Pietra -= Soldati.Attacco.Pietra * livello;
                            player.Ferro -= Soldati.Attacco.Ferro * livello;
                            player.Oro -= Soldati.Attacco.Oro * livello;

                            Send(clientGuid, $"Log_Server|Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Attacco.Cibo * livello}," +
                                $" Legno= {Soldati.Attacco.Legno * livello}," +
                                $" Pietra= {Soldati.Attacco.Pietra * livello}," +
                                $" Ferro= {Soldati.Attacco.Ferro * livello}," +
                                $" Oro= {Soldati.Attacco.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Attacco.Cibo * livello}, " +
                                $"Legno= {Soldati.Attacco.Legno * livello}, " +
                                $"Pietra= {Soldati.Attacco.Pietra * livello}, " +
                                $"Ferro= {Soldati.Attacco.Ferro * livello}, " +
                                $"Oro= {Soldati.Attacco.Oro * livello}\r\n");

                            player.Guerriero_Attacco++;
                            return true;
                        }
                        else
                        {
                            Send(clientGuid, $"Log_Server|Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Attacco.Cibo * livello}," +
                                $" Legno= {Soldati.Attacco.Legno * livello}," +
                                $" Pietra= {Soldati.Attacco.Pietra * livello}," +
                                $" Ferro= {Soldati.Attacco.Ferro * livello}," +
                                $" Oro= {Soldati.Attacco.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Attacco.Cibo * livello}, " +
                                $"Legno= {Soldati.Attacco.Legno * livello}, " +
                                $"Pietra= {Soldati.Attacco.Pietra * livello}, " +
                                $"Ferro= {Soldati.Attacco.Ferro * livello}, " +
                                $"Oro= {Soldati.Attacco.Oro * livello}\r\n");
                            return false;
                        }
                    }
                    else if (tipo == "Livello")
                    {
                        livello = player.Guerriero_Livello + 1;

                        if (player.Cibo >= Soldati.Livello.Cibo * livello &&
                            player.Legno >= Soldati.Livello.Legno * livello &&
                            player.Pietra >= Soldati.Livello.Pietra * livello &&
                            player.Ferro >= Soldati.Livello.Ferro * livello &&
                            player.Oro >= Soldati.Livello.Oro * livello)
                        {
                            // Sottrai le risorse necessarie
                            player.Cibo -= Soldati.Livello.Cibo * livello;
                            player.Legno -= Soldati.Livello.Legno * livello;
                            player.Pietra -= Soldati.Livello.Pietra * livello;
                            player.Ferro -= Soldati.Livello.Ferro * livello;
                            player.Oro -= Soldati.Livello.Oro * livello;

                            Send(clientGuid, $"Log_Server|Risorse utilizzate per la ricerca di {unità} {tipo} {livello}:\r\n " +
                                $"Cibo= {Soldati.Livello.Cibo * livello}," +
                                $" Legno= {Soldati.Livello.Legno * livello}," +
                                $" Pietra= {Soldati.Livello.Pietra * livello}," +
                                $" Ferro= {Soldati.Livello.Ferro * livello}," +
                                $" Oro= {Soldati.Livello.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse utilizzate per la ricerca di {unità} {tipo} {livello}:\r\n " +
                                $"Cibo= {Soldati.Livello.Cibo * livello}, " +
                                $"Legno= {Soldati.Livello.Legno * livello}, " +
                                $"Pietra= {Soldati.Livello.Pietra * livello}, " +
                                $"Ferro= {Soldati.Livello.Ferro * livello}, " +
                                $"Oro= {Soldati.Livello.Oro * livello}\r\n");

                            player.Guerriero_Livello++;
                            return true;
                        }
                        else
                        {
                            Send(clientGuid, $"Log_Server|Risorse insufficienti per la ricerca di {unità} {tipo} {livello}:\r\n " +
                                $"Cibo= {Soldati.Livello.Cibo * livello}," +
                                $" Legno= {Soldati.Livello.Legno * livello}," +
                                $" Pietra= {Soldati.Livello.Pietra * livello}," +
                                $" Ferro= {Soldati.Livello.Ferro * livello}," +
                                $" Oro= {Soldati.Livello.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse insufficienti per la ricerca di {unità} {tipo} {livello}:\r\n " +
                                $"Cibo= {Soldati.Livello.Cibo * livello}, " +
                                $"Legno= {Soldati.Livello.Legno * livello}, " +
                                $"Pietra= {Soldati.Livello.Pietra * livello}, " +
                                $"Ferro= {Soldati.Livello.Ferro * livello}, " +
                                $"Oro= {Soldati.Livello.Oro * livello}\r\n");
                            return false;
                        }
                    }
                    Console.WriteLine($"Ricerca {tipo} {unità} completata!");
                    break;
                case "Lancere":
                    if (tipo == "Salute")
                    {
                        livello = player.Lancere_Salute + 1;
                        valore = livello;
                        if (livello == 0) livello = 1;
                        if (valore == 0) valore = 2;

                        if (player.Lancere_Livello < valore * 2)
                        {
                            Send(clientGuid, $"Log_Server|La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            Console.WriteLine($"La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            return false;
                        }
                        if (player.Cibo >= Soldati.Salute.Cibo * livello &&
                            player.Legno >= Soldati.Salute.Legno * livello &&
                            player.Pietra >= Soldati.Salute.Pietra * livello &&
                            player.Ferro >= Soldati.Salute.Ferro * livello &&
                            player.Oro >= Soldati.Salute.Oro * livello)
                        {
                            // Sottrai le risorse necessarie
                            player.Cibo -= Soldati.Salute.Cibo * livello;
                            player.Legno -= Soldati.Salute.Legno * livello;
                            player.Pietra -= Soldati.Salute.Pietra * livello;
                            player.Ferro -= Soldati.Salute.Ferro * livello;
                            player.Oro -= Soldati.Salute.Oro * livello;

                            Send(clientGuid, $"Log_Server|Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Salute.Cibo * livello}," +
                                $" Legno= {Soldati.Salute.Legno * livello}," +
                                $" Pietra= {Soldati.Salute.Pietra * livello}," +
                                $" Ferro= {Soldati.Salute.Ferro * livello}," +
                                $" Oro= {Soldati.Salute.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Salute.Cibo * livello}, " +
                                $"Legno= {Soldati.Salute.Legno * livello}, " +
                                $"Pietra= {Soldati.Salute.Pietra * livello}, " +
                                $"Ferro= {Soldati.Salute.Ferro * livello}, " +
                                $"Oro= {Soldati.Salute.Oro * livello}\r\n");

                            player.Lancere_Salute++;
                            return true;
                        }
                        else
                        {
                            Send(clientGuid, $"Log_Server|Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Salute.Cibo * livello}," +
                                $" Legno= {Soldati.Salute.Legno * livello}," +
                                $" Pietra= {Soldati.Salute.Pietra * livello}," +
                                $" Ferro= {Soldati.Salute.Ferro * livello}," +
                                $" Oro= {Soldati.Salute.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Salute.Cibo * livello}, " +
                                $"Legno= {Soldati.Salute.Legno * livello}, " +
                                $"Pietra= {Soldati.Salute.Pietra * livello}, " +
                                $"Ferro= {Soldati.Salute.Ferro * livello}, " +
                                $"Oro= {Soldati.Salute.Oro * livello}\r\n");
                            return false;
                        }
                    }
                    else if (tipo == "Difesa")
                    {
                        livello = player.Lancere_Difesa + 1;
                        valore = livello;
                        if (livello == 0) livello = 1;
                        if (valore == 0) valore = 2;

                        if (player.Lancere_Livello < valore * 2)
                        {
                            Send(clientGuid, $"Log_Server|La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            Console.WriteLine($"La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            return false;
                        }
                        if (player.Cibo >= Soldati.Difesa.Cibo * livello &&
                            player.Legno >= Soldati.Difesa.Legno * livello &&
                            player.Pietra >= Soldati.Difesa.Pietra * livello &&
                            player.Ferro >= Soldati.Difesa.Ferro * livello &&
                            player.Oro >= Soldati.Difesa.Oro * livello)
                        {
                            // Sottrai le risorse necessarie
                            player.Cibo -= Soldati.Difesa.Cibo * livello;
                            player.Legno -= Soldati.Difesa.Legno * livello;
                            player.Pietra -= Soldati.Difesa.Pietra * livello;
                            player.Ferro -= Soldati.Difesa.Ferro * livello;
                            player.Oro -= Soldati.Difesa.Oro * livello;

                            Send(clientGuid, $"Log_Server|Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Difesa.Cibo * livello}," +
                                $" Legno= {Soldati.Difesa.Legno * livello}," +
                                $" Pietra= {Soldati.Difesa.Pietra * livello}," +
                                $" Ferro= {Soldati.Difesa.Ferro * livello}," +
                                $" Oro= {Soldati.Difesa.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Difesa.Cibo * livello}, " +
                                $"Legno= {Soldati.Difesa.Legno * livello}, " +
                                $"Pietra= {Soldati.Difesa.Pietra * livello}, " +
                                $"Ferro= {Soldati.Difesa.Ferro * livello}, " +
                                $"Oro= {Soldati.Difesa.Oro * livello}\r\n");

                            player.Lancere_Difesa++;
                            return true;
                        }
                        else
                        {
                            Send(clientGuid, $"Log_Server|Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Difesa.Cibo * livello}," +
                                $" Legno= {Soldati.Difesa.Legno * livello}," +
                                $" Pietra= {Soldati.Difesa.Pietra * livello}," +
                                $" Ferro= {Soldati.Difesa.Ferro * livello}," +
                                $" Oro= {Soldati.Difesa.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Difesa.Cibo * livello}, " +
                                $"Legno= {Soldati.Difesa.Legno * livello}, " +
                                $"Pietra= {Soldati.Difesa.Pietra * livello}, " +
                                $"Ferro= {Soldati.Difesa.Ferro * livello}, " +
                                $"Oro= {Soldati.Difesa.Oro * livello}\r\n");
                            return false;
                        }
                    }
                    else if (tipo == "Attacco")
                    {
                        livello = player.Lancere_Attacco + 1;
                        valore = livello;
                        if (livello == 0) livello = 1;
                        if (valore == 0) valore = 2;

                        if (player.Lancere_Livello < valore * 2)
                        {
                            Send(clientGuid, $"Log_Server|La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            Console.WriteLine($"La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            return false;
                        }
                        if (player.Cibo >= Soldati.Attacco.Cibo * livello &&
                            player.Legno >= Soldati.Attacco.Legno * livello &&
                            player.Pietra >= Soldati.Attacco.Pietra * livello &&
                            player.Ferro >= Soldati.Attacco.Ferro * livello &&
                            player.Oro >= Soldati.Attacco.Oro * livello)
                        {
                            // Sottrai le risorse necessarie
                            player.Cibo -= Soldati.Attacco.Cibo * livello;
                            player.Legno -= Soldati.Attacco.Legno * livello;
                            player.Pietra -= Soldati.Attacco.Pietra * livello;
                            player.Ferro -= Soldati.Attacco.Ferro * livello;
                            player.Oro -= Soldati.Attacco.Oro * livello;

                            Send(clientGuid, $"Log_Server|Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Attacco.Cibo * livello}," +
                                $" Legno= {Soldati.Attacco.Legno * livello}," +
                                $" Pietra= {Soldati.Attacco.Pietra * livello}," +
                                $" Ferro= {Soldati.Attacco.Ferro * livello}," +
                                $" Oro= {Soldati.Attacco.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Attacco.Cibo * livello}, " +
                                $"Legno= {Soldati.Attacco.Legno * livello}, " +
                                $"Pietra= {Soldati.Attacco.Pietra * livello}, " +
                                $"Ferro= {Soldati.Attacco.Ferro * livello}, " +
                                $"Oro= {Soldati.Attacco.Oro * livello}\r\n");

                            player.Lancere_Attacco++;
                            return true;
                        }
                        else
                        {
                            Send(clientGuid, $"Log_Server|Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Attacco.Cibo * livello}," +
                                $" Legno= {Soldati.Attacco.Legno * livello}," +
                                $" Pietra= {Soldati.Attacco.Pietra * livello}," +
                                $" Ferro= {Soldati.Attacco.Ferro * livello}," +
                                $" Oro= {Soldati.Attacco.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Attacco.Cibo * livello}, " +
                                $"Legno= {Soldati.Attacco.Legno * livello}, " +
                                $"Pietra= {Soldati.Attacco.Pietra * livello}, " +
                                $"Ferro= {Soldati.Attacco.Ferro * livello}, " +
                                $"Oro= {Soldati.Attacco.Oro * livello}\r\n");
                            return false;
                        }
                    }
                    else if (tipo == "Livello")
                    {
                        livello = player.Lancere_Livello + 1;

                        if (player.Cibo >= Soldati.Livello.Cibo * livello &&
                            player.Legno >= Soldati.Livello.Legno * livello &&
                            player.Pietra >= Soldati.Livello.Pietra * livello &&
                            player.Ferro >= Soldati.Livello.Ferro * livello &&
                            player.Oro >= Soldati.Livello.Oro * livello)
                        {
                            // Sottrai le risorse necessarie
                            player.Cibo -= Soldati.Livello.Cibo * livello;
                            player.Legno -= Soldati.Livello.Legno * livello;
                            player.Pietra -= Soldati.Livello.Pietra * livello;
                            player.Ferro -= Soldati.Livello.Ferro * livello;
                            player.Oro -= Soldati.Livello.Oro * livello;

                            Send(clientGuid, $"Log_Server|Risorse utilizzate per la ricerca di {unità} {tipo} {livello}:\r\n " +
                                $"Cibo= {Soldati.Livello.Cibo * livello}," +
                                $" Legno= {Soldati.Livello.Legno * livello}," +
                                $" Pietra= {Soldati.Livello.Pietra * livello}," +
                                $" Ferro= {Soldati.Livello.Ferro * livello}," +
                                $" Oro= {Soldati.Livello.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse utilizzate per la ricerca di {unità} {tipo} {livello}:\r\n " +
                                $"Cibo= {Soldati.Livello.Cibo * livello}, " +
                                $"Legno= {Soldati.Livello.Legno * livello}, " +
                                $"Pietra= {Soldati.Livello.Pietra * livello}, " +
                                $"Ferro= {Soldati.Livello.Ferro * livello}, " +
                                $"Oro= {Soldati.Livello.Oro * livello}\r\n");

                            player.Lancere_Livello++;
                            return true;
                        }
                        else
                        {
                            Send(clientGuid, $"Log_Server|Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Livello.Cibo * livello}," +
                                $" Legno= {Soldati.Livello.Legno * livello}," +
                                $" Pietra= {Soldati.Livello.Pietra * livello}," +
                                $" Ferro= {Soldati.Livello.Ferro * livello}," +
                                $" Oro= {Soldati.Livello.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Livello.Cibo * livello}, " +
                                $"Legno= {Soldati.Livello.Legno * livello}, " +
                                $"Pietra= {Soldati.Livello.Pietra * livello}, " +
                                $"Ferro= {Soldati.Livello.Ferro * livello}, " +
                                $"Oro= {Soldati.Livello.Oro * livello}\r\n");
                            return false;
                        }
                    }
                    Console.WriteLine($"Ricerca {tipo} {unità} completata!");
                    break;
                case "Arciere":
                    if (tipo == "Salute")
                    {
                        livello = player.Arcere_Salute + 1;
                        valore = livello;

                        if (livello == 0) livello = 1;
                        if (valore == 0) valore = 2;

                        if (player.Arcere_Livello < valore * 2)
                        {
                            Send(clientGuid, $"Log_Server|La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            Console.WriteLine($"La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            return false;
                        }

                        if (player.Cibo >= Soldati.Salute.Cibo * livello &&
                            player.Legno >= Soldati.Salute.Legno * livello &&
                            player.Pietra >= Soldati.Salute.Pietra * livello &&
                            player.Ferro >= Soldati.Salute.Ferro * livello &&
                            player.Oro >= Soldati.Salute.Oro * livello)
                        {
                            // Sottrai le risorse necessarie
                            player.Cibo -= Soldati.Salute.Cibo * livello;
                            player.Legno -= Soldati.Salute.Legno * livello;
                            player.Pietra -= Soldati.Salute.Pietra * livello;
                            player.Ferro -= Soldati.Salute.Ferro * livello;
                            player.Oro -= Soldati.Salute.Oro * livello;

                            Send(clientGuid, $"Log_Server|Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Salute.Cibo * livello}," +
                                $" Legno= {Soldati.Salute.Legno * livello}," +
                                $" Pietra= {Soldati.Salute.Pietra * livello}," +
                                $" Ferro= {Soldati.Salute.Ferro * livello}," +
                                $" Oro= {Soldati.Salute.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Salute.Cibo * livello}, " +
                                $"Legno= {Soldati.Salute.Legno * livello}, " +
                                $"Pietra= {Soldati.Salute.Pietra * livello}, " +
                                $"Ferro= {Soldati.Salute.Ferro * livello}, " +
                                $"Oro= {Soldati.Salute.Oro * livello}\r\n");

                            player.Arcere_Salute++;
                            return true;
                        }
                        else
                        {
                            Send(clientGuid, $"Log_Server|Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Salute.Cibo * livello}," +
                                $" Legno= {Soldati.Salute.Legno * livello}," +
                                $" Pietra= {Soldati.Salute.Pietra * livello}," +
                                $" Ferro= {Soldati.Salute.Ferro * livello}," +
                                $" Oro= {Soldati.Salute.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Salute.Cibo * livello}, " +
                                $"Legno= {Soldati.Salute.Legno * livello}, " +
                                $"Pietra= {Soldati.Salute.Pietra * livello}, " +
                                $"Ferro= {Soldati.Salute.Ferro * livello}, " +
                                $"Oro= {Soldati.Salute.Oro * livello}\r\n");
                            return false;
                        }
                    }
                    else if (tipo == "Difesa")
                    {
                        livello = player.Arcere_Difesa + 1;
                        valore = livello;

                        if (livello == 0) livello = 1;
                        if (valore == 0) valore = 2;

                        if (player.Arcere_Livello < valore * 2)
                        {
                            Send(clientGuid, $"Log_Server|La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            Console.WriteLine($"La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            return false;
                        }
                        if (player.Cibo >= Soldati.Difesa.Cibo * livello &&
                            player.Legno >= Soldati.Difesa.Legno * livello &&
                            player.Pietra >= Soldati.Difesa.Pietra * livello &&
                            player.Ferro >= Soldati.Difesa.Ferro * livello &&
                            player.Oro >= Soldati.Difesa.Oro * livello)
                        {
                            // Sottrai le risorse necessarie
                            player.Cibo -= Soldati.Difesa.Cibo * livello;
                            player.Legno -= Soldati.Difesa.Legno * livello;
                            player.Pietra -= Soldati.Difesa.Pietra * livello;
                            player.Ferro -= Soldati.Difesa.Ferro * livello;
                            player.Oro -= Soldati.Difesa.Oro * livello;

                            Send(clientGuid, $"Log_Server|Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Difesa.Cibo * livello}," +
                                $" Legno= {Soldati.Difesa.Legno * livello}," +
                                $" Pietra= {Soldati.Difesa.Pietra * livello}," +
                                $" Ferro= {Soldati.Difesa.Ferro * livello}," +
                                $" Oro= {Soldati.Difesa.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Difesa.Cibo * livello}, " +
                                $"Legno= {Soldati.Difesa.Legno * livello}, " +
                                $"Pietra= {Soldati.Difesa.Pietra * livello}, " +
                                $"Ferro= {Soldati.Difesa.Ferro * livello}, " +
                                $"Oro= {Soldati.Difesa.Oro * livello}\r\n");

                            player.Arcere_Difesa++;
                            return true;
                        }
                        else
                        {
                            Send(clientGuid, $"Log_Server|Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Difesa.Cibo * livello}," +
                                $" Legno= {Soldati.Difesa.Legno * livello}," +
                                $" Pietra= {Soldati.Difesa.Pietra * livello}," +
                                $" Ferro= {Soldati.Difesa.Ferro * livello}," +
                                $" Oro= {Soldati.Difesa.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Difesa.Cibo * livello}, " +
                                $"Legno= {Soldati.Difesa.Legno * livello}, " +
                                $"Pietra= {Soldati.Difesa.Pietra * livello}, " +
                                $"Ferro= {Soldati.Difesa.Ferro * livello}, " +
                                $"Oro= {Soldati.Difesa.Oro * livello}\r\n");
                            return false;
                        }
                    }
                    else if (tipo == "Attacco")
                    {
                        livello = player.Arcere_Attacco + 1;
                        valore = livello;

                        if (livello == 0) livello = 1;
                        if (valore == 0) valore = 2;

                        if (player.Arcere_Livello < valore * 2)
                        {
                            Send(clientGuid, $"Log_Server|La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            Console.WriteLine($"La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            return false;
                        }

                        if (player.Cibo >= Soldati.Attacco.Cibo * livello &&
                            player.Legno >= Soldati.Attacco.Legno * livello &&
                            player.Pietra >= Soldati.Attacco.Pietra * livello &&
                            player.Ferro >= Soldati.Attacco.Ferro * livello &&
                            player.Oro >= Soldati.Attacco.Oro * livello)
                        {
                            // Sottrai le risorse necessarie
                            player.Cibo -= Soldati.Attacco.Cibo * livello;
                            player.Legno -= Soldati.Attacco.Legno * livello;
                            player.Pietra -= Soldati.Attacco.Pietra * livello;
                            player.Ferro -= Soldati.Attacco.Ferro * livello;
                            player.Oro -= Soldati.Attacco.Oro * livello;

                            Send(clientGuid, $"Log_Server|Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Attacco.Cibo * livello}," +
                                $" Legno= {Soldati.Attacco.Legno * livello}," +
                                $" Pietra= {Soldati.Attacco.Pietra * livello}," +
                                $" Ferro= {Soldati.Attacco.Ferro * livello}," +
                                $" Oro= {Soldati.Attacco.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Attacco.Cibo * livello}, " +
                                $"Legno= {Soldati.Attacco.Legno * livello}, " +
                                $"Pietra= {Soldati.Attacco.Pietra * livello}, " +
                                $"Ferro= {Soldati.Attacco.Ferro * livello}, " +
                                $"Oro= {Soldati.Attacco.Oro * livello}\r\n");

                            player.Arcere_Attacco++;
                            return true;
                        }
                        else
                        {
                            Send(clientGuid, $"Log_Server|Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Attacco.Cibo * livello}," +
                                $" Legno= {Soldati.Attacco.Legno * livello}," +
                                $" Pietra= {Soldati.Attacco.Pietra * livello}," +
                                $" Ferro= {Soldati.Attacco.Ferro * livello}," +
                                $" Oro= {Soldati.Attacco.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Attacco.Cibo * livello}, " +
                                $"Legno= {Soldati.Attacco.Legno * livello}, " +
                                $"Pietra= {Soldati.Attacco.Pietra * livello}, " +
                                $"Ferro= {Soldati.Attacco.Ferro * livello}, " +
                                $"Oro= {Soldati.Attacco.Oro * livello}\r\n");
                            return false;
                        }
                    }
                    else if (tipo == "Livello")
                    {
                        livello = player.Arcere_Livello + 1;
                        if (player.Cibo >= Soldati.Livello.Cibo * livello &&
                            player.Legno >= Soldati.Livello.Legno * livello &&
                            player.Pietra >= Soldati.Livello.Pietra * livello &&
                            player.Ferro >= Soldati.Livello.Ferro * livello &&
                            player.Oro >= Soldati.Livello.Oro * livello)
                        {
                            // Sottrai le risorse necessarie
                            player.Cibo -= Soldati.Livello.Cibo * livello;
                            player.Legno -= Soldati.Livello.Legno * livello;
                            player.Pietra -= Soldati.Livello.Pietra * livello;
                            player.Ferro -= Soldati.Livello.Ferro * livello;
                            player.Oro -= Soldati.Livello.Oro * livello;

                            Send(clientGuid, $"Log_Server|Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Livello.Cibo * livello}," +
                                $" Legno= {Soldati.Livello.Legno * livello}," +
                                $" Pietra= {Soldati.Livello.Pietra * livello}," +
                                $" Ferro= {Soldati.Livello.Ferro * livello}," +
                                $" Oro= {Soldati.Livello.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Livello.Cibo * livello}, " +
                                $"Legno= {Soldati.Livello.Legno * livello}, " +
                                $"Pietra= {Soldati.Livello.Pietra * livello}, " +
                                $"Ferro= {Soldati.Livello.Ferro * livello}, " +
                                $"Oro= {Soldati.Livello.Oro * livello}\r\n");

                            player.Arcere_Livello++;
                            return true;
                        }
                        else
                        {
                            Send(clientGuid, $"Log_Server|Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Livello.Cibo * livello}," +
                                $" Legno= {Soldati.Livello.Legno * livello}," +
                                $" Pietra= {Soldati.Livello.Pietra * livello}," +
                                $" Ferro= {Soldati.Livello.Ferro * livello}," +
                                $" Oro= {Soldati.Livello.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Livello.Cibo * livello}, " +
                                $"Legno= {Soldati.Livello.Legno * livello}, " +
                                $"Pietra= {Soldati.Livello.Pietra * livello}, " +
                                $"Ferro= {Soldati.Livello.Ferro * livello}, " +
                                $"Oro= {Soldati.Livello.Oro * livello}\r\n");
                            return false;
                        }
                    }
                    Console.WriteLine($"Ricerca {tipo} {unità} completata!");
                    break;
                case "Catapulte":
                    if (tipo == "Salute")
                    {
                        livello = player.Catapulta_Salute + 1;
                        valore = livello;

                        if (livello == 0) livello = 1;
                        if (valore == 0) valore = 2;

                        if (player.Catapulta_Livello < valore * 2)
                        {
                            Send(clientGuid, $"Log_Server|La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            Console.WriteLine($"La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            return false;
                        }

                        if (player.Cibo >= Soldati.Salute.Cibo * livello &&
                            player.Legno >= Soldati.Salute.Legno * livello &&
                            player.Pietra >= Soldati.Salute.Pietra * livello &&
                            player.Ferro >= Soldati.Salute.Ferro * livello &&
                            player.Oro >= Soldati.Salute.Oro * livello)
                        {
                            // Sottrai le risorse necessarie
                            player.Cibo -= Soldati.Salute.Cibo * livello;
                            player.Legno -= Soldati.Salute.Legno * livello;
                            player.Pietra -= Soldati.Salute.Pietra * livello;
                            player.Ferro -= Soldati.Salute.Ferro * livello;
                            player.Oro -= Soldati.Salute.Oro * livello;

                            Send(clientGuid, $"Log_Server|Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Salute.Cibo * livello}," +
                                $" Legno= {Soldati.Salute.Legno * livello}," +
                                $" Pietra= {Soldati.Salute.Pietra * livello}," +
                                $" Ferro= {Soldati.Salute.Ferro * livello}," +
                                $" Oro= {Soldati.Salute.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Salute.Cibo * livello}, " +
                                $"Legno= {Soldati.Salute.Legno * livello}, " +
                                $"Pietra= {Soldati.Salute.Pietra * livello}, " +
                                $"Ferro= {Soldati.Salute.Ferro * livello}, " +
                                $"Oro= {Soldati.Salute.Oro * livello}\r\n");

                            player.Catapulta_Salute++;
                            return true;
                        }
                        else
                        {
                            Send(clientGuid, $"Log_Server|Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Salute.Cibo * livello}," +
                                $" Legno= {Soldati.Salute.Legno * livello}," +
                                $" Pietra= {Soldati.Salute.Pietra * livello}," +
                                $" Ferro= {Soldati.Salute.Ferro * livello}," +
                                $" Oro= {Soldati.Salute.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Salute.Cibo * livello}, " +
                                $"Legno= {Soldati.Salute.Legno * livello}, " +
                                $"Pietra= {Soldati.Salute.Pietra * livello}, " +
                                $"Ferro= {Soldati.Salute.Ferro * livello}, " +
                                $"Oro= {Soldati.Salute.Oro * livello}\r\n");
                            return false;
                        }
                    }
                    else if (tipo == "Difesa")
                    {
                        livello = player.Catapulta_Difesa + 1;
                        valore = livello;

                        if (livello == 0) livello = 1;
                        if (valore == 0) valore = 2;

                        if (player.Catapulta_Livello < valore * 2)
                        {
                            Send(clientGuid, $"Log_Server|La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            Console.WriteLine($"La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            return false;
                        }

                        if (player.Cibo >= Soldati.Difesa.Cibo * livello &&
                            player.Legno >= Soldati.Difesa.Legno * livello &&
                            player.Pietra >= Soldati.Difesa.Pietra * livello &&
                            player.Ferro >= Soldati.Difesa.Ferro * livello &&
                            player.Oro >= Soldati.Difesa.Oro * livello)
                        {
                            // Sottrai le risorse necessarie
                            player.Cibo -= Soldati.Difesa.Cibo * livello;
                            player.Legno -= Soldati.Difesa.Legno * livello;
                            player.Pietra -= Soldati.Difesa.Pietra * livello;
                            player.Ferro -= Soldati.Difesa.Ferro * livello;
                            player.Oro -= Soldati.Difesa.Oro * livello;

                            Send(clientGuid, $"Log_Server|Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Difesa.Cibo * livello}," +
                                $" Legno= {Soldati.Difesa.Legno * livello}," +
                                $" Pietra= {Soldati.Difesa.Pietra * livello}," +
                                $" Ferro= {Soldati.Difesa.Ferro * livello}," +
                                $" Oro= {Soldati.Difesa.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Difesa.Cibo * livello}, " +
                                $"Legno= {Soldati.Difesa.Legno * livello}, " +
                                $"Pietra= {Soldati.Difesa.Pietra * livello}, " +
                                $"Ferro= {Soldati.Difesa.Ferro * livello}, " +
                                $"Oro= {Soldati.Difesa.Oro * livello}\r\n");

                            player.Catapulta_Difesa++;
                            return true;
                        }
                        else
                        {
                            Send(clientGuid, $"Log_Server|Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Difesa.Cibo * livello}," +
                                $" Legno= {Soldati.Difesa.Legno * livello}," +
                                $" Pietra= {Soldati.Difesa.Pietra * livello}," +
                                $" Ferro= {Soldati.Difesa.Ferro * livello}," +
                                $" Oro= {Soldati.Difesa.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Difesa.Cibo * livello}, " +
                                $"Legno= {Soldati.Difesa.Legno * livello}, " +
                                $"Pietra= {Soldati.Difesa.Pietra * livello}, " +
                                $"Ferro= {Soldati.Difesa.Ferro * livello}, " +
                                $"Oro= {Soldati.Difesa.Oro * livello}\r\n");
                            return false;
                        }
                    }
                    else if (tipo == "Attacco")
                    {
                        livello = player.Catapulta_Attacco + 1;
                        valore = livello;

                        if (livello == 0) livello = 1;
                        if (valore == 0) valore = 2;

                        if (player.Catapulta_Livello < valore * 2)
                        {
                            Send(clientGuid, $"Log_Server|La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            Console.WriteLine($"La ricerca {tipo} {unità} {livello}, richiede che il {unità} sia almeno di livello: {valore * 2}\r\n");
                            return false;
                        }

                        if (player.Cibo >= Soldati.Attacco.Cibo * livello &&
                            player.Legno >= Soldati.Attacco.Legno * livello &&
                            player.Pietra >= Soldati.Attacco.Pietra * livello &&
                            player.Ferro >= Soldati.Attacco.Ferro * livello &&
                            player.Oro >= Soldati.Attacco.Oro * livello)
                        {
                            // Sottrai le risorse necessarie
                            player.Cibo -= Soldati.Attacco.Cibo * livello;
                            player.Legno -= Soldati.Attacco.Legno * livello;
                            player.Pietra -= Soldati.Attacco.Pietra * livello;
                            player.Ferro -= Soldati.Attacco.Ferro * livello;
                            player.Oro -= Soldati.Attacco.Oro * livello;

                            Send(clientGuid, $"Log_Server|Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Attacco.Cibo * livello}," +
                                $" Legno= {Soldati.Attacco.Legno * livello}," +
                                $" Pietra= {Soldati.Attacco.Pietra * livello}," +
                                $" Ferro= {Soldati.Attacco.Ferro * livello}," +
                                $" Oro= {Soldati.Attacco.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Attacco.Cibo * livello}, " +
                                $"Legno= {Soldati.Attacco.Legno * livello}, " +
                                $"Pietra= {Soldati.Attacco.Pietra * livello}, " +
                                $"Ferro= {Soldati.Attacco.Ferro * livello}, " +
                                $"Oro= {Soldati.Attacco.Oro * livello}\r\n");

                            player.Catapulta_Attacco++;
                            return true;
                        }
                        else
                        {
                            Send(clientGuid, $"Log_Server|Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Attacco.Cibo * livello}," +
                                $" Legno= {Soldati.Attacco.Legno * livello}," +
                                $" Pietra= {Soldati.Attacco.Pietra * livello}," +
                                $" Ferro= {Soldati.Attacco.Ferro * livello}," +
                                $" Oro= {Soldati.Attacco.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Attacco.Cibo * livello}, " +
                                $"Legno= {Soldati.Attacco.Legno * livello}, " +
                                $"Pietra= {Soldati.Attacco.Pietra * livello}, " +
                                $"Ferro= {Soldati.Attacco.Ferro * livello}, " +
                                $"Oro= {Soldati.Attacco.Oro * livello}\r\n");
                            return false;
                        }
                    }
                    else if (tipo == "Livello")
                    {
                        livello = player.Catapulta_Livello + 1;
                        if (player.Cibo >= Soldati.Livello.Cibo * livello &&
                            player.Legno >= Soldati.Livello.Legno * livello &&
                            player.Pietra >= Soldati.Livello.Pietra * livello &&
                            player.Ferro >= Soldati.Livello.Ferro * livello &&
                            player.Oro >= Soldati.Livello.Oro * livello)
                        {
                            // Sottrai le risorse necessarie
                            player.Cibo -= Soldati.Livello.Cibo * livello;
                            player.Legno -= Soldati.Livello.Legno * livello;
                            player.Pietra -= Soldati.Livello.Pietra * livello;
                            player.Ferro -= Soldati.Livello.Ferro * livello;
                            player.Oro -= Soldati.Livello.Oro * livello;

                            Send(clientGuid, $"Log_Server|Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Livello.Cibo * livello}," +
                                $" Legno= {Soldati.Livello.Legno * livello}," +
                                $" Pietra= {Soldati.Livello.Pietra * livello}," +
                                $" Ferro= {Soldati.Livello.Ferro * livello}," +
                                $" Oro= {Soldati.Livello.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse utilizzate per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Livello.Cibo * livello}, " +
                                $"Legno= {Soldati.Livello.Legno * livello}, " +
                                $"Pietra= {Soldati.Livello.Pietra * livello}, " +
                                $"Ferro= {Soldati.Livello.Ferro * livello}, " +
                                $"Oro= {Soldati.Livello.Oro * livello}\r\n");

                            player.Catapulta_Livello++;
                            return true;
                        }
                        else
                        {
                            Send(clientGuid, $"Log_Server|Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Livello.Cibo * livello}," +
                                $" Legno= {Soldati.Livello.Legno * livello}," +
                                $" Pietra= {Soldati.Livello.Pietra * livello}," +
                                $" Ferro= {Soldati.Livello.Ferro * livello}," +
                                $" Oro= {Soldati.Livello.Oro * livello}\r\n");
                            Console.WriteLine($"Risorse insufficienti per la ricerca di {tipo} {unità} {livello}:\r\n " +
                                $"Cibo= {Soldati.Livello.Cibo * livello}, " +
                                $"Legno= {Soldati.Livello.Legno * livello}, " +
                                $"Pietra= {Soldati.Livello.Pietra * livello}, " +
                                $"Ferro= {Soldati.Livello.Ferro * livello}, " +
                                $"Oro= {Soldati.Livello.Oro * livello}\r\n");
                            return false;
                        }
                    }
                    Console.WriteLine($"Ricerca {tipo} {unità} completata!");
                    break;
                // Aggiungi case per altri tipi di ricerche
                default:
                    Console.WriteLine($"Il tipo di ricerca {tipo} {unità} non supportata!");
                    break;
            }
            return false;
        }
    }
}
