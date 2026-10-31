using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Forge.Core.Repositories;

namespace Forge.Android.Services;

/// <summary>
/// Android implementation of ILocationService querying location
/// (ACCESS_COARSE_LOCATION / ACCESS_FINE_LOCATION) for real-time weather
/// determination without user tracking.
/// </summary>
public class AndroidLocationService : ILocationService
{
    private readonly Activity _activity;
    public const int LocationPermissionRequestCode = 1001;

    public AndroidLocationService(Activity activity)
    {
        _activity = activity;
    }

    public Task<bool> HasLocationPermissionAsync()
    {
        var coarse = _activity.CheckSelfPermission(Manifest.Permission.AccessCoarseLocation) == Permission.Granted;
        var fine = _activity.CheckSelfPermission(Manifest.Permission.AccessFineLocation) == Permission.Granted;
        return Task.FromResult(coarse || fine);
    }

    public Task<bool> RequestLocationPermissionAsync()
    {
        var coarse = _activity.CheckSelfPermission(Manifest.Permission.AccessCoarseLocation) == Permission.Granted;
        var fine = _activity.CheckSelfPermission(Manifest.Permission.AccessFineLocation) == Permission.Granted;
        if (coarse || fine) return Task.FromResult(true);

        _activity.RequestPermissions(
            new[] { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation },
            LocationPermissionRequestCode);
        return Task.FromResult(false);
    }

    public Task<(double Latitude, double Longitude)?> GetApproximateLocationAsync()
    {
        try
        {
            var coarse = _activity.CheckSelfPermission(Manifest.Permission.AccessCoarseLocation) == Permission.Granted;
            var fine = _activity.CheckSelfPermission(Manifest.Permission.AccessFineLocation) == Permission.Granted;
            if (!coarse && !fine) return Task.FromResult<(double Latitude, double Longitude)?>(null);

            var locationManager = _activity.GetSystemService(Context.LocationService) as LocationManager;
            if (locationManager == null) return Task.FromResult<(double Latitude, double Longitude)?>(null);

            Location? location = null;

            // 1. Network provider (coarse / fast)
            if (locationManager.IsProviderEnabled(LocationManager.NetworkProvider))
            {
                location = locationManager.GetLastKnownLocation(LocationManager.NetworkProvider);
            }

            // 2. GPS provider if fine is granted
            if (location == null && fine && locationManager.IsProviderEnabled(LocationManager.GpsProvider))
            {
                location = locationManager.GetLastKnownLocation(LocationManager.GpsProvider);
            }

            // 3. Passive provider fallback
            if (location == null && locationManager.IsProviderEnabled(LocationManager.PassiveProvider))
            {
                location = locationManager.GetLastKnownLocation(LocationManager.PassiveProvider);
            }

            if (location != null)
            {
                return Task.FromResult<(double Latitude, double Longitude)?>((location.Latitude, location.Longitude));
            }
        }
        catch
        {
            // Fail safely without throwing unhandled exceptions
        }

        return Task.FromResult<(double Latitude, double Longitude)?>(null);
    }
}
