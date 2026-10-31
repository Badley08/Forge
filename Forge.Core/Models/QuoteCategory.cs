namespace Forge.Core.Models;

public class QuoteCategory
{
    public string Id { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string CoverAsset { get; set; } = string.Empty;
    public bool IsFreeCategory { get; set; }

    public static List<QuoteCategory> GetAllCategories()
    {
        return new List<QuoteCategory>
        {
            new() { Id = "discipline", DisplayName = "Discipline", CoverAsset = "avares://Forge.UI/Assets/Covers/discipline.jpeg", IsFreeCategory = true },
            new() { Id = "action", DisplayName = "Action", CoverAsset = "avares://Forge.UI/Assets/Covers/action.jpeg", IsFreeCategory = true },
            new() { Id = "resilience", DisplayName = "Résilience", CoverAsset = "avares://Forge.UI/Assets/Covers/resilience.jpeg", IsFreeCategory = true },
            new() { Id = "stoicism", DisplayName = "Stoïcisme", CoverAsset = "avares://Forge.UI/Assets/Covers/stoicism.jpeg", IsFreeCategory = false },
            new() { Id = "ambition", DisplayName = "Ambition", CoverAsset = "avares://Forge.UI/Assets/Covers/ambition.jpeg", IsFreeCategory = false },
            new() { Id = "lucidity", DisplayName = "Lucidité", CoverAsset = "avares://Forge.UI/Assets/Covers/lucidity.jpeg", IsFreeCategory = false },
            new() { Id = "calm", DisplayName = "Calme", CoverAsset = "avares://Forge.UI/Assets/Covers/calm.jpeg", IsFreeCategory = false },
            new() { Id = "detachment", DisplayName = "Détachement", CoverAsset = "avares://Forge.UI/Assets/Covers/detachment.jpeg", IsFreeCategory = false },
            new() { Id = "philosophic", DisplayName = "Philosophie", CoverAsset = "avares://Forge.UI/Assets/Covers/philosophic.jpeg", IsFreeCategory = false },
            new() { Id = "poetic", DisplayName = "Poétique", CoverAsset = "avares://Forge.UI/Assets/Covers/poetic.jpeg", IsFreeCategory = false }
        };
    }
}
