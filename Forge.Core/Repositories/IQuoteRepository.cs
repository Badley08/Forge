using Forge.Core.Models;
using Forge.Data.Local;
using Forge.Data.RemoteQuoteSource;

namespace Forge.Core.Repositories;

public interface IQuoteRepository
{
    Task<QuoteModel?> GetNextUnseenQuoteAsync(string lang, string category, string weather);
    Task MarkQuoteSeenAsync(int quoteId);
    Task SyncFromRemoteAsync();
}

public class QuoteRepository : IQuoteRepository
{
    private readonly ForgeDbContext _dbContext;
    private readonly IRemoteQuoteSyncService _remoteSyncService;

    public QuoteRepository(ForgeDbContext dbContext, IRemoteQuoteSyncService remoteSyncService)
    {
        _dbContext = dbContext;
        _remoteSyncService = remoteSyncService;
    }

    public async Task<QuoteModel?> GetNextUnseenQuoteAsync(string lang, string category, string weather)
    {
        var record = await _dbContext.GetNextUnseenQuoteAsync(lang, category, weather);
        if (record == null) return null;

        return new QuoteModel
        {
            Id = record.Id,
            Text = record.Text,
            Lang = record.Lang,
            Day = record.Day,
            IsSeen = record.IsSeen,
            Category = record.Category,
            WeatherHint = record.WeatherHint
        };
    }

    public async Task MarkQuoteSeenAsync(int quoteId)
    {
        await _dbContext.MarkQuoteAsSeenAsync(quoteId);
    }

    public Task SyncFromRemoteAsync()
    {
        return _remoteSyncService.SyncFromRemoteAsync();
    }
}
