namespace Forge.Data.Local;

public static class InitialQuotesSeed
{
    public static List<QuoteRecord> GetSeedQuotes()
    {
        return new List<QuoteRecord>
        {
            // Jour 1 - Discipline & Action
            new() { Text = "Ce qui fait obstacle à l'action fait avancer l'action. Ce qui barre le chemin devient le chemin.", Lang = "FR", Day = 1, Category = "action", WeatherHint = "any" },
            new() { Text = "Tu as le pouvoir sur ton esprit, pas sur les événements extérieurs. Comprends cela, et tu trouveras ta force.", Lang = "FR", Day = 1, Category = "discipline", WeatherHint = "any" },
            new() { Text = "Nous souffrons plus souvent en imagination qu'en réalité.", Lang = "FR", Day = 1, Category = "lucidity", WeatherHint = "any" },

            // Jour 2 - Ambition & Rigueur
            new() { Text = "Ne perds plus de temps à discuter de ce que doit être un homme de bien. Sois-en un.", Lang = "FR", Day = 2, Category = "action", WeatherHint = "any" },
            new() { Text = "Ce n'est pas parce que les choses sont difficiles que nous n'osons pas, c'est parce que nous n'osons pas qu'elles sont difficiles.", Lang = "FR", Day = 2, Category = "ambition", WeatherHint = "any" },
            new() { Text = "Aucun vent n'est favorable à celui qui ne sait pas vers quel port il navigue.", Lang = "FR", Day = 2, Category = "lucidity", WeatherHint = "any" },

            // Jour 3 - Résilience & Maîtrise
            new() { Text = "La difficulté révèle l'homme. Quand une épreuve survient, souviens-toi que la vie t'a opposé à un rude adversaire pour te fortifier.", Lang = "FR", Day = 3, Category = "resilience", WeatherHint = "rain" },
            new() { Text = "Il n'y a qu'une seule façon d'accéder au calme intérieur : cesser de s'inquiéter pour ce qui dépasse le pouvoir de notre volonté.", Lang = "FR", Day = 3, Category = "calm", WeatherHint = "cloudy" },
            new() { Text = "L'homme conquérant est d'abord celui qui s'est conquis lui-même.", Lang = "FR", Day = 3, Category = "discipline", WeatherHint = "any" },

            // Jour 4 - Stoïcisme & Détachement
            new() { Text = "Le fer ne s'aiguise que contre le fer ; l'esprit ne s'endurcit que dans l'épreuve.", Lang = "FR", Day = 4, Category = "stoicism", WeatherHint = "any" },
            new() { Text = "L'obstacle est la forge où se trempe la volonté inébranlable.", Lang = "FR", Day = 4, Category = "discipline", WeatherHint = "any" },
            new() { Text = "Ne demande pas que les événements arrivent comme tu le souhaites, mais accueille-les avec droiture tels qu'ils surviennent.", Lang = "FR", Day = 4, Category = "detachment", WeatherHint = "any" },

            // Jour 5 - Philosophie & Lucidité
            new() { Text = "La chance est ce qui arrive quand la préparation méthodique rencontre l'opportunité.", Lang = "FR", Day = 5, Category = "philosophic", WeatherHint = "sunny" },
            new() { Text = "Combien de temps vas-tu encore attendre avant d'exiger le meilleur et le plus strict de toi-même ?", Lang = "FR", Day = 5, Category = "ambition", WeatherHint = "any" },
            new() { Text = "Fais chaque tâche dans ton existence avec la rigueur et la présence comme si c'était la dernière.", Lang = "FR", Day = 5, Category = "action", WeatherHint = "any" },

            // Jour 6 - Résilience & Calme
            new() { Text = "Le feu transforme tout ce qu'on y jette en flammes et en clarté. Sois semblable à ce feu souverain.", Lang = "FR", Day = 6, Category = "resilience", WeatherHint = "sunny" },
            new() { Text = "Ce qui ne te détruit pas affine ta posture. Forge ton âme dans le silence, la retenue et la sueur.", Lang = "FR", Day = 6, Category = "discipline", WeatherHint = "any" },
            new() { Text = "La discipline d'aujourd'hui est la seule souveraineté et liberté de demain.", Lang = "FR", Day = 6, Category = "poetic", WeatherHint = "any" },

            // Jour 7 - Accomplissement
            new() { Text = "Vivre, c'est lutter. Pas de repos pour l'acier avant que la lame ne soit parfaitement forgée.", Lang = "FR", Day = 7, Category = "stoicism", WeatherHint = "any" },
            new() { Text = "Garde ton attention fixée uniquement sur ce qui dépend de toi. Le reste n'est que fumée et dispersion.", Lang = "FR", Day = 7, Category = "detachment", WeatherHint = "any" },
            new() { Text = "Tu as achevé le cycle des sept jours. Tu as dompté le tumulte intérieur. Tiens bon, avance sans faiblir.", Lang = "FR", Day = 7, Category = "philosophic", WeatherHint = "any" }
        };
    }
}
