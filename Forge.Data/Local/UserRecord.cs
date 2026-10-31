using SQLite;

namespace Forge.Data.Local;

[Table("users")]
public class UserRecord
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Lang { get; set; } = "FR";

    public bool IsPremium { get; set; }
}
