using SQLite;

namespace Forge.Data.Local;

public class ForgeDbContext
{
    private readonly SQLiteAsyncConnection _db;
    private bool _initialized;

    public ForgeDbContext(string dbPath)
    {
        _db = new SQLiteAsyncConnection(dbPath);
    }

    public async Task InitializeAsync()
    {
        if (_initialized) return;

        await _db.CreateTableAsync<QuoteRecord>();
        await _db.CreateTableAsync<UserRecord>();

        var count = await _db.Table<QuoteRecord>().CountAsync();
        if (count == 0)
        {
            var seed = InitialQuotesSeed.GetSeedQuotes();
            await _db.InsertAllAsync(seed);
        }

        _initialized = true;
    }

    /// <summary>
    /// Charge strictement 1 citation non vue, influencée par la météo et le jour sans dump en mémoire.
    /// </summary>
    public async Task<QuoteRecord?> GetNextUnseenQuoteAsync(string lang, string category, string weather)
    {
        await InitializeAsync();

        // 1. Cherche en priorité avec la météo correspondante
        var query = _db.Table<QuoteRecord>()
            .Where(q => !q.IsSeen && q.Lang == lang && q.Category == category && (q.WeatherHint == weather || q.WeatherHint == "any"))
            .OrderBy(q => q.Day);

        var quote = await query.FirstOrDefaultAsync();

        // 2. Si aucune citation météo spécifique, repli sur n'importe quelle citation non vue de la catégorie
        if (quote == null)
        {
            quote = await _db.Table<QuoteRecord>()
                .Where(q => !q.IsSeen && q.Lang == lang && q.Category == category)
                .OrderBy(q => q.Day)
                .FirstOrDefaultAsync();
        }

        // 3. Si toujours aucune citation dans cette catégorie, repli sur n'importe quelle catégorie
        if (quote == null)
        {
            quote = await _db.Table<QuoteRecord>()
                .Where(q => !q.IsSeen && q.Lang == lang)
                .OrderBy(q => q.Day)
                .FirstOrDefaultAsync();
        }

        return quote;
    }

    public async Task MarkQuoteAsSeenAsync(int quoteId)
    {
        await InitializeAsync();
        var quote = await _db.Table<QuoteRecord>().Where(q => q.Id == quoteId).FirstOrDefaultAsync();
        if (quote != null)
        {
            quote.IsSeen = true;
            await _db.UpdateAsync(quote);
        }
    }

    public async Task<UserRecord?> GetUserAsync()
    {
        await InitializeAsync();
        return await _db.Table<UserRecord>().FirstOrDefaultAsync();
    }

    public async Task SaveUserAsync(string name, string lang, bool isPremium)
    {
        await InitializeAsync();
        var user = await _db.Table<UserRecord>().FirstOrDefaultAsync();
        if (user == null)
        {
            await _db.InsertAsync(new UserRecord { Name = name, Lang = lang, IsPremium = isPremium });
        }
        else
        {
            user.Name = name;
            user.Lang = lang;
            user.IsPremium = isPremium;
            await _db.UpdateAsync(user);
        }
    }
}
