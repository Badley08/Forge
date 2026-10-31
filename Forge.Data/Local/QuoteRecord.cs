using SQLite;

namespace Forge.Data.Local;

[Table("quotes")]
public class QuoteRecord
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed]
    public string Text { get; set; } = string.Empty;

    [Indexed]
    public string Lang { get; set; } = "FR";

    public int Day { get; set; }

    [Indexed]
    public bool IsSeen { get; set; }

    [Indexed]
    public string Category { get; set; } = "discipline";

    public string WeatherHint { get; set; } = "any";
}
