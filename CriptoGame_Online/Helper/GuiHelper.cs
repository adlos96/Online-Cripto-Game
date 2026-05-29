
using Strategico_V2;

namespace Warrior_and_Wealth.Helper
{
    public static class GuiHelper
    {
        /// <summary>
        /// Esegue un'azione su un controllo WinForms in modo sicuro,
        /// gestendo dispose e cancellazione. Ritorna false se il controllo
        /// non è più disponibile.
        /// </summary>
        public static bool SafeInvoke(Control control, Action action)
        {
            try
            {
                if (control == null || control.IsDisposed || !control.IsHandleCreated)
                    return false;

                control.Invoke(action);
                return true;
            }
            catch (ObjectDisposedException) { return false; }
            catch (InvalidOperationException) { return false; }
        }

        /// <summary>
        /// Loop di aggiornamento UI standard. Da usare in TUTTI i form.
        /// Gestisce cancellazione, dispose e delay in modo corretto.
        /// </summary>
        public static async Task UiUpdateLoop(
            Control anchor,
            Action updateAction,
            CancellationToken token,
            int intervalMs = 250)
        {
            while (!token.IsCancellationRequested)
            {
                bool ok = SafeInvoke(anchor, updateAction);
                if (!ok) break; // form distrutto, usciamo

                try
                {
                    await Task.Delay(intervalMs, token);
                }
                catch (TaskCanceledException) { break; }
            }
        }

        /// Utilizzo esempio

        //public partial class Statistiche : Form
        //{
        //    private CancellationTokenSource _cts = new();
        //
        //    private void Statistiche_Load(object sender, EventArgs e)
        //    {
        //        Task.Run(() => GuiHelper.UiUpdateLoop(
        //            anchor: lbl_Giocatore_Testo,   // controllo "sentinella"
        //            updateAction: AggiornaDati,     // il tuo metodo di update
        //            token: _cts.Token,
        //            intervalMs: 250               //Tempo tra un aggiornamento e l'altro
        //        ));
        //    }
        //
        //    private void AggiornaDati()
        //    {
        //        // Qui scrivi SOLO la logica di aggiornamento, senza
        //        // nessun Invoke, nessun try/catch, nessun token.
        //        // Tutto il boilerplate è nel GuiHelper.
        //
        //        lbl_Giocatore_Testo.Text = $"VIP: {Variabili_Client.Utente.User_Vip_Tempo}\n...";
        //        lbl_Statistiche.Text = $"Strutture: ...";
        //        // ecc.
        //    }
        //
        //    private void Statistiche_FormClosing(object sender, FormClosingEventArgs e)
        //    {
        //        _cts.Cancel();
        //    }
        //}
    }
}
