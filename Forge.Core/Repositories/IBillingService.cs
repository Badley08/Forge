namespace Forge.Core.Repositories;

/// <summary>
/// Abstraction for Google Play In-App Billing queries and purchase flow.
/// </summary>
public interface IBillingService
{
    Task<bool> CheckIsPremiumAsync();
    Task<bool> PurchasePremiumAsync();
    Task<string> GetProductPriceAsync();
}
