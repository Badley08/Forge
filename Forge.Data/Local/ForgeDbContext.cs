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
    /// Retrieves a single unseen quote influenced by weather hint and day order.
    /// Returns null when quotes for the given category and language are exhausted.
    /// </summary>
    public async Task<QuoteRecord?> GetNextUnseenQuoteAsync(string lang, string category, string weather)
    {
        await InitializeAsync();

        // 1. Primary query: match category, language, and specific weather hint (or 'any')
        var query = _db.Table<QuoteRecord>()
            .Where(q => !q.IsSeen && q.Lang == lang && q.Category == category && (q.WeatherHint == weather || q.WeatherHint == "any"))
            .OrderBy(q => q.Day);

        var quote = await query.FirstOrDefaultAsync();

        // 2. Fallback within the same category if no specific weather match exists
        if (quote == null)
        {
            quote = await _db.Table<QuoteRecord>()
                .Where(q => !q.IsSeen && q.Lang == lang && q.Category == category)
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
