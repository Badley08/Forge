using Forge.Data.Local;
using Forge.Data.Preferences;

namespace Forge.Core.Repositories;

public interface IUserRepository
{
    string UserName { get; }
    string UserLanguage { get; }
    bool IsOnboardingCompleted { get; }
    bool IsPremium { get; }

    Task SaveUserAsync(string name, string lang);
    Task CompleteOnboardingAsync();
    void SetPremiumStatus(bool isPremium);
}

public class UserRepository : IUserRepository
{
    private readonly ForgeDbContext _dbContext;
    private readonly IPreferencesService _preferences;

    public UserRepository(ForgeDbContext dbContext, IPreferencesService preferences)
    {
        _dbContext = dbContext;
        _preferences = preferences;
    }

    public string UserName => _preferences.UserName;
    public string UserLanguage => _preferences.UserLanguage;
    public bool IsOnboardingCompleted => _preferences.IsOnboardingCompleted;

    /// <summary>
    /// Premium mode is active either through in-app billing or the personal VIP key ("Karlito").
    /// </summary>
    public bool IsPremium => _preferences.IsPremium || _preferences.HasKarlitoCheatcode();

    public async Task SaveUserAsync(string name, string lang)
    {
        _preferences.UserName = name;
        _preferences.UserLanguage = lang;

        // If user enters "Karlito", automatically unlock lifetime premium
        if (_preferences.HasKarlitoCheatcode())
        {
            _preferences.IsPremium = true;
        }

        await _dbContext.SaveUserAsync(name, lang, IsPremium);
    }

    public Task CompleteOnboardingAsync()
    {
        _preferences.IsOnboardingCompleted = true;
        return Task.CompletedTask;
    }

    public void SetPremiumStatus(bool isPremium)
    {
        _preferences.IsPremium = isPremium;
    }
}
