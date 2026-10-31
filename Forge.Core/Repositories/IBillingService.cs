namespace Forge.Core.Repositories;

public interface IBillingService
{
    Task<bool> CheckIsPremiumAsync();
    Task<bool> PurchasePremiumAsync();
}
