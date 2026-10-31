namespace Forge.Core.Models;

public class QuoteModel
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string Lang { get; set; } = "FR";
    public int Day { get; set; }
    public bool IsSeen { get; set; }
    public string Category { get; set; } = "discipline";
    public string WeatherHint { get; set; } = "any";
}
