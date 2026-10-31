namespace Forge.Data.Preferences;

public interface IPreferencesService
{
    string UserName { get; set; }
    string UserLanguage { get; set; }
    bool IsOnboardingCompleted { get; set; }
    string AnonymousUserId { get; }
    bool IsPremium { get; set; }

    /// <summary>
    /// Checks if the username matches the VIP bypass key ("Karlito").
    /// </summary>
    bool HasKarlitoCheatcode();
}
