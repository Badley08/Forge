using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Forge.Core.Repositories;

public interface IWeatherService
{
    Task<string> GetCurrentWeatherHintAsync();
}

/// <summary>
/// Fetches real-time atmospheric conditions using Open-Meteo / WMO standard weather codes
/// based on coarse/fine coordinates. Preserves zero user tracking and falls back
/// gracefully to time-of-day heuristics if location permission is declined or offline.
/// </summary>
public class WeatherService : IWeatherService
{
    private readonly ILocationService? _locationService;
    private readonly HttpClient _httpClient;
    private string _cachedHint = "any";
    private DateTime _lastFetchTime = DateTime.MinValue;

    public WeatherService(ILocationService? locationService = null)
    {
        _locationService = locationService;
        _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };
    }

    public async Task<string> GetCurrentWeatherHintAsync()
    {
        // Cache weather for 30 minutes to minimize network usage
        if (DateTime.UtcNow - _lastFetchTime < TimeSpan.FromMinutes(30) && _cachedHint != "any")
        {
            return _cachedHint;
        }

        try
        {
            if (_locationService != null)
            {
                var coords = await _locationService.GetApproximateLocationAsync();
                if (coords.HasValue)
                {
                    var (lat, lon) = coords.Value;
                    var url = $"https://api.open-meteo.com/v1/forecast?latitude={lat:F2}&longitude={lon:F2}&current=weather_code";

                    var response = await _httpClient.GetFromJsonAsync<WeatherApiResponse>(url);
                    if (response?.Current != null)
                    {
                        _cachedHint = MapWeatherCodeToHint(response.Current.WeatherCode);
                        _lastFetchTime = DateTime.UtcNow;
                        return _cachedHint;
                    }
                }
            }
        }
        catch
        {
            // Network or parsing failure: gracefully fallback
        }

        // Deterministic fallback based on time of day
        _cachedHint = GetFallbackTimeOfDayHint();
        return _cachedHint;
    }

    private static string MapWeatherCodeToHint(int code)
    {
        return code switch
        {
            // Clear sky / mainly clear
            0 or 1 => "sunny",

            // Partly cloudy / overcast / fog
            2 or 3 or 45 or 48 => "cloudy",

            // Drizzle / Rain / Showers / Thunderstorm
            >= 51 and <= 67 => "rain",
            >= 80 and <= 82 => "rain",
            >= 95 and <= 99 => "rain",

            // Snow / Snow grains / Snow showers
            >= 71 and <= 77 => "snow",
            85 or 86 => "snow",

            _ => "cloudy"
        };
    }

    private static string GetFallbackTimeOfDayHint()
    {
        var hour = DateTime.Now.Hour;
        return hour switch
        {
            >= 6 and < 12 => "sunny",
            >= 12 and < 17 => "cloudy",
            >= 17 and < 21 => "rain",
            _ => "snow"
        };
    }

    private class WeatherApiResponse
    {
        [JsonPropertyName("current")]
        public CurrentWeather? Current { get; set; }
    }

    private class CurrentWeather
    {
        [JsonPropertyName("weather_code")]
        public int WeatherCode { get; set; }
    }
}
