namespace Server_Strategico.Gioco
{
    internal class Esperienza
    {
        public static int exp_Level_Up = 200;
        public static int cosa = 0;
        public static double moltiplicatore = 0.62;

        public static async Task<bool> LevelUp(Giocatori.Player player)
        {
            Moltiplicatore(player);

            switch (player.Livello)
            {
                case 1:
                    if (player.Esperienza >= exp_Level_Up + exp_Level_Up * player.Livello * 0.35)
                    {
                        player.Esperienza -= Convert.ToInt32(exp_Level_Up + exp_Level_Up * player.Livello * 0.35);
                        player.Livello++;
                    }
                    break;
                case 2:
                    if (player.Esperienza >= exp_Level_Up + exp_Level_Up * player.Livello * 0.40)
                    {
                        player.Esperienza -= Convert.ToInt32(exp_Level_Up + exp_Level_Up * player.Livello * 0.40);
                        player.Livello++;
                    }
                    break;
                case 3:
                    if (player.Esperienza >= exp_Level_Up + exp_Level_Up * player.Livello * 0.45)
                    {
                        player.Esperienza -= Convert.ToInt32(exp_Level_Up + exp_Level_Up * player.Livello * 0.45);
                        player.Livello++;
                    }
                    break;
                default:
                    if (player.Esperienza >= exp_Level_Up + exp_Level_Up * player.Livello * moltiplicatore)
                    {
                        player.Esperienza -= Convert.ToInt32(exp_Level_Up + exp_Level_Up * player.Livello * moltiplicatore);
                        player.Livello++;
                        if (player.Livello >= 10 && player.Livello < 11) cosa = 1;
                        else if (player.Livello >= 20 && player.Livello < 21) cosa = 2;
                        else if (player.Livello >= 40 && player.Livello < 41) cosa = 3;
                    }
                    break;
            }
            return true;
        }
        public static void Moltiplicatore(Giocatori.Player player)
        {
            if (player.Livello >= 10 && player.Livello < 20 && cosa == 1)
            {
                moltiplicatore += 0.09;
                cosa = 0;
            }
            else if (player.Livello >= 20 && player.Livello < 40 && cosa == 2)
            {
                moltiplicatore += 0.13;
                cosa = 0;
            }
            else if (player.Livello >= 40 && cosa == 3)
            {
                moltiplicatore += 0.22;
                cosa = 0;
            }
        }
    }
}
