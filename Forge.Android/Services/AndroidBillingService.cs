using Forge.Core.Repositories;
using Forge.Data.Preferences;
using Plugin.InAppBilling;

namespace Forge.Android.Services;

/// <summary>
/// Native Android In-App Billing provider wrapping Google Play Billing.
/// Retrieves real store pricing for forge_premium_lifetime ($3.99) and executes purchases.
/// </summary>
public class AndroidBillingService : IBillingService
{
    public const string ProductId = "forge_premium_lifetime";
    public const string DefaultPrice = "3,99 $";

    private readonly IPreferencesService _preferences;

    public AndroidBillingService(IPreferencesService preferences)
    {
        _preferences = preferences;
    }

    public async Task<string> GetProductPriceAsync()
    {
        try
        {
            var billing = CrossInAppBilling.Current;
            var connected = await billing.ConnectAsync();
            if (connected)
            {
                var items = await billing.GetProductInfoAsync(ItemType.InAppPurchase, ProductId);
                var item = items?.FirstOrDefault(i => i.ProductId == ProductId);
                if (!string.IsNullOrWhiteSpace(item?.LocalizedPrice))
                {
                    return item.LocalizedPrice;
                }
            }
        }
        catch
        {
            // Fallback to configured retail price
        }

        return DefaultPrice;
    }

    public async Task<bool> CheckIsPremiumAsync()
    {
        // VIP bypass key check
        if (_preferences.HasKarlitoCheatcode()) return true;

        try
        {
            var billing = CrossInAppBilling.Current;
            var connected = await billing.ConnectAsync();
            if (!connected) return _preferences.IsPremium;

            var purchases = await billing.GetPurchasesAsync(ItemType.InAppPurchase);
            var isPurchased = purchases?.Any(p => p.ProductId == ProductId && p.State == PurchaseState.Purchased) ?? false;

            if (isPurchased)
            {
                _preferences.IsPremium = true;
            }

            return _preferences.IsPremium;
        }
        catch
        {
            return _preferences.IsPremium;
        }
    }

    public async Task<bool> PurchasePremiumAsync()
    {
        // VIP bypass key check
        if (_preferences.HasKarlitoCheatcode())
        {
            _preferences.IsPremium = true;
            return true;
        }

        try
        {
            var billing = CrossInAppBilling.Current;
            var connected = await billing.ConnectAsync();
            if (!connected) return false;

            var purchase = await billing.PurchaseAsync(ProductId, ItemType.InAppPurchase);
            if (purchase != null && purchase.State == PurchaseState.Purchased)
            {
                _preferences.IsPremium = true;
                return true;
            }
        }
        catch
        {
            // Error handling during purchase workflow
        }

        return false;
    }
}
