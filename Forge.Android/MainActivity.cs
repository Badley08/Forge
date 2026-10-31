using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Avalonia;
using Avalonia.Android;
using Forge.Android.Services;
using Forge.Core.Repositories;
using Forge.Core.ViewModels;
using Forge.Data.Local;
using Forge.Data.Preferences;
using Forge.Data.RemoteQuoteSource;
using Forge.UI;
using Microsoft.Extensions.DependencyInjection;

namespace Forge.Android;

[Activity(
    Label = "Forge",
    Theme = "@android:style/Theme.DeviceDefault.NoActionBar",
    Icon = "@mipmap/ic_launcher",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        // Enable FLAG_SECURE across the application to prevent screen captures and recordings
        Window?.SetFlags(WindowManagerFlags.Secure, WindowManagerFlags.Secure);

        base.OnCreate(savedInstanceState);
    }

    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
    {
        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        if (requestCode == AndroidLocationService.LocationPermissionRequestCode)
        {
            if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
            {
                if (App.Current is App forgeApp && forgeApp.Services != null)
                {
                    var mainVm = forgeApp.Services.GetService<MainViewModel>();
                    mainVm?.LoadInitialQuoteAsync();
                }
            }
        }
    }

    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        var services = ConfigureServices();

        return base.CustomizeAppBuilder(builder)
            .AfterSetup(app =>
            {
                if (app.Instance is App forgeApp)
                {
                    forgeApp.Services = services;
                }
            });
    }

    private IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        var dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "forge_sqlite.db3");
        var prefsDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        services.AddSingleton(new ForgeDbContext(dbPath));
        services.AddSingleton<IPreferencesService>(new PreferencesService(prefsDir));
        services.AddSingleton<IRemoteQuoteSyncService, RemoteQuoteSyncService>();

        services.AddSingleton<IQuoteRepository, QuoteRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IBillingService, AndroidBillingService>();
        services.AddSingleton<ILocationService>(new AndroidLocationService(this));
        services.AddSingleton<IWeatherService, WeatherService>();

        services.AddTransient<MainViewModel>();

        return services.BuildServiceProvider();
    }
}
