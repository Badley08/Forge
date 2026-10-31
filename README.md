# [FORGE] Daily Stoic Discipline

> **[PROPRIETARY SOFTWARE — ALL RIGHTS RESERVED — STRICT ANTI-PLAGIARISM NOTICE]**  
> This software, including its architecture, visual presentation concepts, quote database engine, and atmospheric weather algorithms, is the exclusive proprietary intellectual property of **Karlito Dev**. Any copying, code reuse, concept plagiarism, unauthorized distribution, or data mining is strictly prohibited.

A cross-platform native application built with **C# (.NET 9)** and **Avalonia UI**, engineered as a daily sanctuary for stoic discipline and strength of will.

---

## [CAMEO: VISION] Philosophy & Absolute Rarity

Forge is not a social feed or an endless scroll. It is a single-thought sanctuary: open the app, absorb the maxim, swipe, and return to reality.

### Core Principles:
- **[DISCIPLINE] One thought at a time**: No infinite scrolling, no cluttered lists.
- **[RARITY] Genuine scarcity**: Every viewed thought is consumed and will never appear again once the app is closed.
- **[ZERO-TRACKING] Absolute privacy**: Zero advertising SDKs, zero hardware identifiers, zero analytics or telemetry.
- **[ZERO-EMOJI] Visual sobriety**: Zero emojis across the entire interface to maintain stoic gravity, elegance, and focus.
- **[INTEGRITY] Total source anonymity**: No author tags or historical attributions. The insight confronts the user's mind directly without mediation.

---

## [CAMEO: DESIGN] Glassmorphism & Procedural Weather Atmospheres

The interface leverages **Avalonia UI** custom procedural vector and canvas rendering:

- **[GLASS] Glassmorphism Aesthetic**: Translucent frosted-glass cards with subtle specular borders, ambient depth, and mineral contrasts (`#F5F1E8`, `#1E1D1A`, `#C89B53`).
- **[WEATHER] Procedural Animated Atmosphere**:
  - **Rain (`rain`)**: Dynamic angled raindrops drifting with variable wind velocity.
  - **Snow (`snow`)**: Soft snowflakes gently swaying along harmonic sinusoidal paths.
  - **Sunny (`sunny`)**: Gentle breathing radial pulse of a warm solar aura.
  - **Cloudy (`cloudy`)**: Low-opacity atmospheric mist drifting softly across the canvas.
- **[GESTURE] 100% Gesture-Driven Navigation (Touch Swipe)**:
  - Swipe Right (`→`): Forge the next unseen quote.
  - Swipe Left (`←`): Revisit previously viewed quotes from the current active session.
  - Interactive drag with dynamic angular tilt (`RotateTransform`) and opacity fading.

---

## [CAMEO: CATALOG] The 10 Discipline Categories

Forge ships with a comprehensive seed of 70 quotes per language (210 quotes total across French, English, and Spanish) spanning 10 disciplines:

| Cameo | Category | Focus & Character Forging |
| :--- | :--- | :--- |
| `[DISCIPLINE]` | **Discipline** | Daily rigor, unwavering perseverance, and mastery over impulse. |
| `[ACTION]` | **Action** | Immediate decisive execution, refusing all forms of procrastination. |
| `[RESILIENCE]` | **Resilience** | Inner fortitude through trial, absorbing adversity into strength. |
| `[STOICISM]` | **Stoicism** | Relentless focus on what is within control, letting go of the rest. |
| `[AMBITION]` | **Ambition** | High standards, demanding more of oneself than of anyone else. |
| `[LUCIDITY]` | **Lucidity** | Mental clarity, cutting through illusion to see reality as it is. |
| `[CALM]` | **Calm** | Unshakeable inner peace in the very center of tumult and chaos. |
| `[DETACHMENT]` | **Detachment** | Complete release over external outcomes, opinion, and fortune. |
| `[PHILOSOPHY]` | **Philosophy** | The daily practice of living with integrity and unbending virtue. |
| `[POETIC]` | **Poetic** | Noble perspective on human existence and the brevity of time. |

---

## [CAMEO: VIP] Personal VIP Key (« Karlito »)

A direct VIP recognition mechanism is embedded in the identity engine:
- **Activation**: Entering **`Karlito`** as the username (case-insensitive, e.g. `karlito`).
- **Immediate Effects**:
  - Automatically and permanently unlocks all 10 categories for life.
  - Activates home-screen widgets with procedural weather effects.
  - Enables high-resolution watermarked image exports.
  - Displays the exclusive `[VIP PREMIUM]` emblem on headers and onboarding.

---

## [CAMEO: ARCHITECTURE] Architecture Overview

```
Forge/
├── Forge.Core/                 [Domain Logic & MVVM]
│   ├── Models/                 QuoteModel, QuoteCategory
│   ├── ViewModels/             MainViewModel, OnboardingViewModel, ViewModelBase
│   └── Repositories/           IQuoteRepository, IUserRepository, IBillingService, IWeatherService
├── Forge.Data/                 [Persistence & Local Storage]
│   ├── Local/                  ForgeDbContext (SQLite async), InitialQuotesSeed (210 quotes)
│   ├── Preferences/            PreferencesService (Encrypted key-value & VIP bypass)
│   └── RemoteQuoteSource/      IRemoteQuoteSyncService (V1 signature stub ready for V2)
├── Forge.UI/                   [XAML Presentation & Controls]
│   ├── Controls/               WeatherAtmosphereControl (Procedural particle canvas)
│   ├── Views/                  MainView (Interactive swipe drag), OnboardingView (Gesture guide)
│   ├── Styles/                 ForgeTheme.axaml (Glassmorphism design tokens)
│   └── Assets/                 Official high-resolution category covers & logos
└── Forge.Android/              [Mobile Platform Head]
    ├── MainActivity.cs         Hardware FLAG_SECURE, Service container composition
    ├── AndroidManifest.xml     Permissions and security attributes
    └── Services/               AndroidBillingService (Google Play Billing v7.1.0)
```

---

## [CAMEO: SECURITY] Content Protection & Anti-Theft

- **[FLAG_SECURE] Hardware Anti-Capture**: Enforced at Android window level to block screenshots and screen recordings across all devices.
- **[WATERMARK] Secure Image Sharing**: No plain text is copied. Premium shares produce a curated image with an embedded 8% opacity cryptographic AI verification seal (`FORGE · SHA256`).
- **[STORE] Google Play Billing**: Lifetime one-time purchase configured at **$3.99 USD** (`forge_premium_lifetime`) with dynamic multi-currency store lookup.

---

## [CAMEO: CI-CD] Automated Cloud Compilation

The GitHub Actions workflow [`.github/workflows/avalonia-build.yml`](.github/workflows/avalonia-build.yml) triggers on every push to the `avalonia` branch:
- Restores .NET 9 SDK and installs the Android workload.
- Builds `Forge.Core`, `Forge.UI`, and packages `Forge.Android`.
- Produces and archives the installable APK without consuming local developer machine RAM.

---

## [CAMEO: LICENSE] Proprietary License & Anti-Plagiarism

This software is strictly proprietary and owned by **Karlito Dev** (see [LICENSE](LICENSE)):
- **Plagiarism and unauthorized replication strictly prohibited**.
- **All Rights Reserved** — Karlito Dev.
