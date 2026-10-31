namespace Forge.Core.Repositories;

/// <summary>
/// Abstraction for coarse location access and user permission workflows.
/// Designed specifically for approximate weather atmospheric queries without device tracking.
/// </summary>
public interface ILocationService
{
    Task<bool> HasLocationPermissionAsync();
    Task<bool> RequestLocationPermissionAsync();
    Task<(double Latitude, double Longitude)?> GetApproximateLocationAsync();
}
