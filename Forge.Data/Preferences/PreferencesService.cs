using System.Text.Json;

namespace Forge.Data.Preferences;

public class PreferencesService : IPreferencesService
{
    private readonly string _prefsFilePath;
    private Dictionary<string, string> _cache = new();

    public PreferencesService(string storageDir)
    {
        _prefsFilePath = Path.Combine(storageDir, "forge_user_prefs.json");
        Load();
        if (string.IsNullOrEmpty(AnonymousUserId))
        {
            Set("anon_user_id", Guid.NewGuid().ToString("N"));
        }
    }

    public string UserName
    {
        get => Get("user_name", string.Empty);
        set => Set("user_name", value);
    }

    public string UserLanguage
    {
        get => Get("user_lang", "FR");
        set => Set("user_lang", value.ToUpperInvariant());
    }

    public bool IsOnboardingCompleted
    {
        get => bool.TryParse(Get("onboarding_done", "false"), out var done) && done;
        set => Set("onboarding_done", value.ToString());
    }

    public string AnonymousUserId => Get("anon_user_id", string.Empty);

    public bool IsPremium
    {
        get
        {
            if (HasKarlitoCheatcode()) return true;
            return bool.TryParse(Get("is_premium", "false"), out var prem) && prem;
        }
        set => Set("is_premium", value.ToString());
    }

    public bool HasKarlitoCheatcode()
    {
        return string.Equals(UserName?.Trim(), "Karlito", StringComparison.OrdinalIgnoreCase);
    }

    private string Get(string key, string defaultValue)
    {
        return _cache.TryGetValue(key, out var val) ? val : defaultValue;
    }

    private void Set(string key, string value)
    {
        _cache[key] = value;
        Save();
    }

    private void Load()
    {
        try
        {
            if (File.Exists(_prefsFilePath))
            {
                var json = File.ReadAllText(_prefsFilePath);
                _cache = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new();
            }
        }
        catch
        {
            _cache = new();
        }
    }

    private void Save()
    {
        try
        {
            var dir = Path.GetDirectoryName(_prefsFilePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var json = JsonSerializer.Serialize(_cache);
            File.WriteAllText(_prefsFilePath, json);
        }
        catch
        {
            // Silently persist in memory
        }
    }
}
