using Forge.Core.Repositories;
using Forge.Data.Preferences;
using Plugin.InAppBilling;

namespace Forge.Android.Services;

public class AndroidBillingService : IBillingService
{
    public const string ProductId = "forge_premium_lifetime";
    private readonly IPreferencesService _preferences;

    public AndroidBillingService(IPreferencesService preferences)
    {
        _preferences = preferences;
    }

    public async Task<bool> CheckIsPremiumAsync()
    {
        // Cheatcode VIP Karlito
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
        // Cheatcode VIP Karlito
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
            // Error handling
        }

        return false;
    }
}
