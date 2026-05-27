using Strategico_V2;
using Warrior_and_Wealth.Localization;

internal static class LocalizationManager
{
    private static readonly Dictionary<string, ILocalization> _lingue = new()
    {
        { "ITA", new ITA() },
        { "ENG", new ENG() },
    };

    // Oppure, se vuoi usare la lingua del player globale:
    public static ILocalization Current =>
        _lingue.TryGetValue(Variabili_Client.lingua_Selezionata, out var loc) ? loc : _lingue["ITA"];
}
