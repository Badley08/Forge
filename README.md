# [FORGE] Daily Stoic Discipline

> **[PROPRIETARY SOFTWARE — ALL RIGHTS RESERVED — STRICT NO-PLAGIARISM]**  
> Ce logiciel, son architecture, ses concepts visuels, son moteur de citations et ses algorithmes météo sont la propriété exclusive de **Karlito Dev**. Toute copie, réutilisation de code, plagiat de concept ou extraction de données est formellement interdite.

Application native multiplateforme développée en **C# (.NET 9)** avec **Avalonia UI**, dédiée à la discipline stoïcienne quotidienne et à la trempe de la volonté.

---

## [CAMEO: VISION] Philosophie & Rareté Absolue

Forge n'est pas un flux infini. C'est un sanctuaire à **une seule pensée à la fois** : on ouvre l'application, on s'imprègne de la maxime, on glisse, on avance.

### Principes Fondamentaux de la Forge :
- **[DISCIPLINE] Une seule pensée à la fois** : Aucun défilement infini, pas de liste distraite.
- **[RARITY] Rareté réelle** : Chaque pensée vue est consommée et ne revient jamais après fermeture.
- **[ZERO-TRACKING] Confidentialité intégrale** : Zéro SDK publicitaire, zéro collecte d'identifiants d'appareil, zéro télémétrie.
- **[ZERO-EMOJI] Épuration esthétique** : Zéro émoji dans toute l'interface afin de préserver la gravité, l'élégance et la sobriété stoïcienne.
- **[INTEGRITY] Anonymat total des sources** : Aucune étiquette d'auteur ni provenance externe n'apparaît. La pensée se confronte directement à l'esprit sans intermédiaire.

---

## [CAMEO: DESIGN] Interface Glassmorphism & Atmosphères Animées

L'interface tire parti des capacités de rendu graphique d'**Avalonia UI** :

- **[GLASS] Finition Glassmorphism** : Cartes translucides frosted-glass avec bordures spéculaires douces, profondeur visuelle et contrastes minéraux (`#F5F1E8`, `#1E1D1A`, `#C89B53`).
- **[WEATHER] Atmosphère Procédurale Animée** :
  - **Pluie (`rain`)** : Traînées cinétiques fluides avec dérive au vent.
  - **Neige (`snow`)** : Flocons légers oscillant harmoniquement sur la carte.
  - **Soleil (`sunny`)** : Pulsation radiale douce de l'aura dorée.
  - **Nuageux (`cloudy`)** : Nébuleuses brumeuses glissant à l'arrière-plan.
- **[GESTURE] Navigation 100% Geste (Swipe Tactile)** :
  - Glissement vers la droite (`→`) : Découverte de la prochaine pensée non vue.
  - Glissement vers la gauche (`←`) : Retour sur les pensées vues pendant la session active.
  - Translation interactive avec inclinaison angulaire (`RotateTransform`) et fondu d'opacité.

---

## [CAMEO: CATALOG] Les 10 Catégories de Discipline

L'application intègre un jeu complet de 70 pensées par langue (210 pensées au total en Français, Anglais et Espagnol) réparties sur 10 voies :

| Cameo | Catégorie | Thématique & Trempe |
| :--- | :--- | :--- |
| `[DISCIPLINE]` | **Discipline** | Rigueur quotidienne, persévérance inébranlable et maîtrise des pulsions. |
| `[ACTION]` | **Action** | Passage à l'acte immédiat, refus viscéral de la procrastination. |
| `[RESILIENCE]` | **Résilience** | Force de caractère face à l'adversité et absorption des épreuves. |
| `[STOICISM]` | **Stoïcisme** | Aligné sur ce qui dépend de nous, détachement du reste. |
| `[AMBITION]` | **Ambition** | Élévation exigeante, dépassement de l'homme qu'on était hier. |
| `[LUCIDITY]` | **Lucidité** | Clarté de jugement, discernement tranchant pour dissoudre l'illusion. |
| `[CALM]` | **Calme** | Sérénité souveraine au milieu du chaos et de la tourmente. |
| `[DETACHMENT]` | **Détachement** | Lâcher-prise total sur les événements extérieurs et la fortune. |
| `[PHILOSOPHY]` | **Philosophie** | Exercice pratique de vivre avec droiture et quête de vérité. |
| `[POETIC]` | **Poétique** | Regard noble sur l'existence et intensité de chaque souffle. |

---

## [CAMEO: VIP] Déverrouillage VIP Personnel (« Karlito »)

Un mécanisme de reconnaissance VIP direct est intégré au moteur :
- **Activation** : Saisir **`Karlito`** comme nom d'utilisateur (insensible à la casse, ex. `karlito`).
- **Effets immédiats** :
  - Déblocage automatique à vie de la totalité des 10 catégories.
  - Activation sans restriction des widgets d'écran d'accueil avec animations météo.
  - Autorisation du partage filigrané haute fidélité.
  - Affichage du badge exclusif `[VIP PREMIUM]` dans l'en-tête et l'onboarding.

---

## [CAMEO: ARCHITECTURE] Architecture C# & Avalonia UI

```
Forge/
├── Forge.Core/                 [Logique Métier & MVVM]
│   ├── Models/                 QuoteModel, QuoteCategory
│   ├── ViewModels/             MainViewModel, OnboardingViewModel, ViewModelBase
│   └── Repositories/           IQuoteRepository, IUserRepository, IBillingService, IWeatherService
├── Forge.Data/                 [Persistance & Base Locale]
│   ├── Local/                  ForgeDbContext (SQLite async), InitialQuotesSeed (210 quotes)
│   ├── Preferences/            PreferencesService (Stockage chiffré & Cheatcode Karlito)
│   └── RemoteQuoteSource/      IRemoteQuoteSyncService (Stub V1 pour sync V2)
├── Forge.UI/                   [Couche Graphique & XAML]
│   ├── Controls/               WeatherAtmosphereControl (Moteur météo particulaire)
│   ├── Views/                  MainView (Swipe drag), OnboardingView (Tutoriel gestuel)
│   ├── Styles/                 ForgeTheme.axaml (Tokens Glassmorphism)
│   └── Assets/                 Covers officiels & Logos vectorisés
└── Forge.Android/              [Tête de Plateforme Mobile]
    ├── MainActivity.cs         FLAG_SECURE, Injection des services
    ├── AndroidManifest.xml     Permissions, Sécurité
    └── Services/               AndroidBillingService (Google Play Billing v7.1.0)
```

---

## [CAMEO: SECURITY] Protection des Données & Anti-Copie

- **[FLAG_SECURE] Anti-Capture** : Protection matérielle activée sur la fenêtre Android bloquant toute capture d'écran et tout enregistrement vidéo.
- **[WATERMARK] Partage Filigrané** : Aucun texte brut copiable. Le partage Premium produit un visuel avec micro-sceau d'authentification IA transparent à 8% (`FORGE · SHA256`).
- **[STORE] Monétisation Google Play** : Achat unique à vie configuré à **3,99 $ USD** (`forge_premium_lifetime`).

---

## [CAMEO: CI-CD] Compilation Automatisée GitHub Actions

Le workflow automatisé [`.github/workflows/avalonia-build.yml`](.github/workflows/avalonia-build.yml) se déclenche à chaque push sur la branche `avalonia` :
- Restauration des dépendances .NET 9 et du workload Android.
- Compilation Release de `Forge.Core`, `Forge.UI` et `Forge.Android`.
- Génération et archivage automatique de l'APK installable sans consommer les ressources de la machine locale.

---

## [CAMEO: LICENSE] Licence Propriétaire & Anti-Plagiat

Ce logiciel est sous licence propriétaire exclusive de **Karlito Dev** (voir [LICENSE](LICENSE)) :
- **Interdiction formelle de plagiat, clonage ou réplication** du concept, du design, des citations et de l'architecture.
- **Tous droits réservés (All Rights Reserved)** — Karlito Dev.
