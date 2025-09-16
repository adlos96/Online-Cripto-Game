using static Server_Strategico.Gioco.Giocatori;

namespace Server_Strategico.Gioco
{
    public class Quest
    {
        private readonly object _lock = new();
        private readonly Player _player;

        
        public Quest(Player player)
        {
            _player = player;
        }
        public bool Premio_Quest_Mensile()
        {
            if (_player.Punti_Quest >= Convert.ToInt32(Variabili_Server.Quest_Reward.Normali_Montly.Points_1))
                if (_player.PremiNormali[0] == false)
                {
                    _player.PremiNormali[0] = true;
                    if (_player.Vip == true) _player.PremiVIP[0] = true;
                }

            if (_player.Punti_Quest >= Convert.ToInt32(Variabili_Server.Quest_Reward.Normali_Montly.Points_2))
                if (_player.PremiNormali[1] == false)
                {
                    _player.PremiNormali[1] = true;
                    if (_player.Vip == true) _player.PremiVIP[1] = true;
                }

            if (_player.Punti_Quest >= Convert.ToInt32(Variabili_Server.Quest_Reward.Normali_Montly.Points_3))
                if (_player.PremiNormali[2] == false)
                {
                    _player.PremiNormali[2] = true;
                    if (_player.Vip == true) _player.PremiVIP[2] = true;
                }

            if (_player.Punti_Quest >= Convert.ToInt32(Variabili_Server.Quest_Reward.Normali_Montly.Points_4))
                if (_player.PremiNormali[3] == false)
                {
                    _player.PremiNormali[3] = true;
                    if (_player.Vip == true) _player.PremiVIP[3] = true;
                }

            if (_player.Punti_Quest >= Convert.ToInt32(Variabili_Server.Quest_Reward.Normali_Montly.Points_5))
                if (_player.PremiNormali[4] == false)
                {
                    _player.PremiNormali[4] = true;
                    if (_player.Vip == true) _player.PremiVIP[4] = true;
                }

            if (_player.Punti_Quest >= Convert.ToInt32(Variabili_Server.Quest_Reward.Normali_Montly.Points_6))
                if (_player.PremiNormali[5] == false)
                {
                    _player.PremiNormali[5] = true;
                    if (_player.Vip == true) _player.PremiVIP[5] = true;
                }

            if (_player.Punti_Quest >= Convert.ToInt32(Variabili_Server.Quest_Reward.Normali_Montly.Points_7))
                if (_player.PremiNormali[6] == false)
                {
                    _player.PremiNormali[6] = true;
                    if (_player.Vip == true) _player.PremiVIP[6] = true;
                }

            if (_player.Punti_Quest >= Convert.ToInt32(Variabili_Server.Quest_Reward.Normali_Montly.Points_8))
                if (_player.PremiNormali[7] == false)
                {
                    _player.PremiNormali[7] = true;
                    if (_player.Vip == true) _player.PremiVIP[7] = true;
                }

            if (_player.Punti_Quest >= Convert.ToInt32(Variabili_Server.Quest_Reward.Normali_Montly.Points_9))
                if (_player.PremiNormali[8] == false)
                {
                    _player.PremiNormali[8] = true;
                    if (_player.Vip == true) _player.PremiVIP[8] = true;
                }

            if (_player.Punti_Quest >= Convert.ToInt32(Variabili_Server.Quest_Reward.Normali_Montly.Points_10))
                if (_player.PremiNormali[9] == false)
                {
                    _player.PremiNormali[9] = true;
                    if (_player.Vip == true) _player.PremiVIP[90] = true;
                }

            if (_player.Punti_Quest >= Convert.ToInt32(Variabili_Server.Quest_Reward.Normali_Montly.Points_11))
                if (_player.PremiNormali[10] == false)
                {
                    _player.PremiNormali[10] = true;
                    if (_player.Vip == true) _player.PremiVIP[10] = true;
                }

            if (_player.Punti_Quest >= Convert.ToInt32(Variabili_Server.Quest_Reward.Normali_Montly.Points_12))
                if (_player.PremiNormali[11] == false)
                {
                    _player.PremiNormali[11] = true;
                    if (_player.Vip == true) _player.PremiVIP[11] = true;
                }

            if (_player.Punti_Quest >= Convert.ToInt32(Variabili_Server.Quest_Reward.Normali_Montly.Points_13))
                if (_player.PremiNormali[12] == false)
                {
                    _player.PremiNormali[12] = true;
                    if (_player.Vip == true) _player.PremiVIP[12] = true;
                }

            if (_player.Punti_Quest >= Convert.ToInt32(Variabili_Server.Quest_Reward.Normali_Montly.Points_14))
                if (_player.PremiNormali[13] == false)
                {
                    _player.PremiNormali[13] = true;
                    if (_player.Vip == true) _player.PremiVIP[13] = true;
                }

            if (_player.Punti_Quest >= Convert.ToInt32(Variabili_Server.Quest_Reward.Normali_Montly.Points_15))
                if (_player.PremiNormali[14] == false)
                {
                    _player.PremiNormali[14] = true;
                    if (_player.Vip == true) _player.PremiVIP[14] = true;
                }

            if (_player.Punti_Quest >= Convert.ToInt32(Variabili_Server.Quest_Reward.Normali_Montly.Points_16))
                if (_player.PremiNormali[15] == false)
                {
                    _player.PremiNormali[15] = true;
                    if (_player.Vip == true) _player.PremiVIP[15] = true;
                }

            if (_player.Punti_Quest >= Convert.ToInt32(Variabili_Server.Quest_Reward.Normali_Montly.Points_17))
                if (_player.PremiNormali[16] == false)
                {
                    _player.PremiNormali[16] = true;
                    if (_player.Vip == true) _player.PremiVIP[16] = true;
                }

            if (_player.Punti_Quest >= Convert.ToInt32(Variabili_Server.Quest_Reward.Normali_Montly.Points_18))
                if (_player.PremiNormali[17] == false)
                {
                    _player.PremiNormali[17] = true;
                    if (_player.Vip == true) _player.PremiVIP[17] = true;
                }

            if (_player.Punti_Quest >= Convert.ToInt32(Variabili_Server.Quest_Reward.Normali_Montly.Points_19))
                if (_player.PremiNormali[18] == false)
                {
                    _player.PremiNormali[18] = true;
                    if (_player.Vip == true) _player.PremiVIP[18] = true;
                }

            if (_player.Punti_Quest >= Convert.ToInt32(Variabili_Server.Quest_Reward.Normali_Montly.Points_20))
                if (_player.PremiNormali[19] == false)
                {
                    _player.PremiNormali[19] = true;
                    if (_player.Vip == true) _player.PremiVIP[19] = true;
                }

            return false;
        }
    }
}
