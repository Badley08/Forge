namespace Forge.Core.Repositories;

/// <summary>
/// Abstraction for coarse/fine location access and user permission workflows.
/// Designed specifically for approximate weather atmospheric queries for widgets and quote cards.
/// Complies with Google Play prominent disclosure and privacy guidelines.
/// </summary>
public interface ILocationService
{
    Task<bool> HasLocationPermissionAsync();
    Task<bool> RequestLocationPermissionAsync();
    Task<(double Latitude, double Longitude)?> GetApproximateLocationAsync();
}
