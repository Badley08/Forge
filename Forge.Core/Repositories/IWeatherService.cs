namespace Forge.Core.Repositories;

public interface IWeatherService
{
    Task<string> GetCurrentWeatherHintAsync();
}

public class WeatherService : IWeatherService
{
    public Task<string> GetCurrentWeatherHintAsync()
    {
        // V1: Minimal deterministic time-of-day atmospheric estimation to guide quote tone
        var hour = DateTime.Now.Hour;
        string hint = hour switch
        {
            >= 6 and < 12 => "sunny",
            >= 12 and < 18 => "cloudy",
            >= 18 and < 22 => "rain",
            _ => "any"
        };
        return Task.FromResult(hint);
    }
}
