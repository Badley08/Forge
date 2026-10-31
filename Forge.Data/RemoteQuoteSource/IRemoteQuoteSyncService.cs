namespace Forge.Data.RemoteQuoteSource;

public interface IRemoteQuoteSyncService
{
    /// <summary>
    /// Reserved for V2: Secure remote quote synchronization from a private index.
    /// In V1, this remains a stub implementation as specified.
    /// </summary>
    Task SyncFromRemoteAsync();
}

public class RemoteQuoteSyncService : IRemoteQuoteSyncService
{
    public Task SyncFromRemoteAsync()
    {
        // V1: Remote synchronization is deferred to V2
        return Task.CompletedTask;
    }
}
