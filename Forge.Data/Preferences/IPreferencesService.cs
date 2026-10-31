namespace Forge.Data.Preferences;

public interface IPreferencesService
{
    string UserName { get; set; }
    string UserLanguage { get; set; }
    bool IsOnboardingCompleted { get; set; }
    string AnonymousUserId { get; }
    bool IsPremium { get; set; }

    /// <summary>
    /// Vérifie si l'utilisateur bénéficie du cheatcode VIP "Karlito"
    /// </summary>
    bool HasKarlitoCheatcode();
}
