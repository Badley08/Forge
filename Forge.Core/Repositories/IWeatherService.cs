namespace Forge.Core.Repositories;

public interface IWeatherService
{
    Task<string> GetCurrentWeatherHintAsync();
}

public class WeatherService : IWeatherService
{
    public Task<string> GetCurrentWeatherHintAsync()
    {
        // En V1 : Estimation minimale basée sur la saison et l'heure pour influencer sobrement la citation
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
