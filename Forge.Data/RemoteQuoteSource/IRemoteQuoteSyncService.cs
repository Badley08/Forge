namespace Forge.Data.RemoteQuoteSource;

public interface IRemoteQuoteSyncService
{
    /// <summary>
    /// Réservé pour la V2 : Téléchargement sécurisé des citations distantes depuis le dépôt privé GitHub.
    /// Signature vide en V1 conformément aux spécifications du projet.
    /// </summary>
    Task SyncFromRemoteAsync();
}

public class RemoteQuoteSyncService : IRemoteQuoteSyncService
{
    public Task SyncFromRemoteAsync()
    {
        // V1 : Prêt pour la V2, aucune synchronisation distante active pour le moment.
        return Task.CompletedTask;
    }
}
