using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Forge.Core.Repositories;

namespace Forge.Android.Services;

/// <summary>
/// Native Android implementation of ILocationService.
/// Retrieves approximate or fine coordinates exclusively to determine local atmospheric
/// conditions for widgets and animated visual cards, complying with Google Play policies.
/// </summary>
public class AndroidLocationService : ILocationService
{
    public const int LocationPermissionRequestCode = 1042;
    private readonly Activity _activity;

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
        var tcs = new TaskCompletionSource<bool>();

        var coarse = _activity.CheckSelfPermission(Manifest.Permission.AccessCoarseLocation) == Permission.Granted;
        var fine = _activity.CheckSelfPermission(Manifest.Permission.AccessFineLocation) == Permission.Granted;

        if (coarse || fine)
        {
            return Task.FromResult(true);
        }

        _activity.RunOnUiThread(() =>
        {
            _activity.RequestPermissions(
                new[] { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation },
                LocationPermissionRequestCode);
            tcs.TrySetResult(true);
        });

        return tcs.Task;
    }

    public async Task<(double Latitude, double Longitude)?> GetApproximateLocationAsync()
    {
        try
        {
            var coarse = _activity.CheckSelfPermission(Manifest.Permission.AccessCoarseLocation) == Permission.Granted;
            var fine = _activity.CheckSelfPermission(Manifest.Permission.AccessFineLocation) == Permission.Granted;
            if (!coarse && !fine) return null;

            var locationManager = _activity.GetSystemService(Context.LocationService) as LocationManager;
            if (locationManager == null) return null;

            Location? location = null;

            // 1. Check network provider
            if (locationManager.IsProviderEnabled(LocationManager.NetworkProvider))
            {
                location = locationManager.GetLastKnownLocation(LocationManager.NetworkProvider);
            }

            // 2. Check GPS provider
            if (location == null && fine && locationManager.IsProviderEnabled(LocationManager.GpsProvider))
            {
                location = locationManager.GetLastKnownLocation(LocationManager.GpsProvider);
            }

            // 3. Fallback to passive provider
            if (location == null && locationManager.IsProviderEnabled(LocationManager.PassiveProvider))
            {
                location = locationManager.GetLastKnownLocation(LocationManager.PassiveProvider);
            }

            if (location != null)
            {
                return (location.Latitude, location.Longitude);
            }

            // 4. Request a single fresh update if last known is null
            var provider = locationManager.IsProviderEnabled(LocationManager.NetworkProvider)
                ? LocationManager.NetworkProvider
                : (fine && locationManager.IsProviderEnabled(LocationManager.GpsProvider) ? LocationManager.GpsProvider : null);

            if (provider != null)
            {
                var tcs = new TaskCompletionSource<Location?>();
                var listener = new SingleLocationListener(tcs);

                _activity.RunOnUiThread(() =>
                {
                    try
                    {
#pragma warning disable CA1422
                        locationManager.RequestSingleUpdate(provider, listener, Looper.MainLooper);
#pragma warning restore CA1422
                    }
                    catch
                    {
                        tcs.TrySetResult(null);
                    }
                });

                var completed = await Task.WhenAny(tcs.Task, Task.Delay(4000));
                if (completed == tcs.Task)
                {
                    var freshLoc = await tcs.Task;
                    if (freshLoc != null)
                    {
                        return (freshLoc.Latitude, freshLoc.Longitude);
                    }
                }
            }
        }
        catch
        {
            // Silently fall back
        }

        return null;
    }

    private class SingleLocationListener : Java.Lang.Object, ILocationListener
    {
        private readonly TaskCompletionSource<Location?> _tcs;

        public SingleLocationListener(TaskCompletionSource<Location?> tcs)
        {
            _tcs = tcs;
        }

        public void OnLocationChanged(Location location)
        {
            _tcs.TrySetResult(location);
        }

        public void OnProviderDisabled(string provider)
        {
            _tcs.TrySetResult(null);
        }

        public void OnProviderEnabled(string provider) { }

        public void OnStatusChanged(string? provider, Availability status, Bundle? extras) { }
    }
}
