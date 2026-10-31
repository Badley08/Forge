# Forge — Daily Stoic Discipline

Application Android 100% native développée en **Kotlin** et **Jetpack Compose**.

## 🏛️ Architecture & Conception
- **Package** : `com.karlitodev.forge`
- **Interface** : Jetpack Compose + Material 3 (Couleurs stoïciennes : `#F5F1E8` et `#121212`)
- **Persistance** : Room Database (`quotes`, `users`) + DataStore Preferences
- **Sécurité** : `FLAG_SECURE` actif sur l'ensemble de l'application (blocage systématique des captures d'écran)
- **Monétisation** : Google Play Billing Library v6 (In-App purchase `forge_premium_lifetime` 2,99 €)
- **Partage Haute Fidélité** :
  - Génération d'une image Bitmap dynamique (1080x1350)
  - Filigrane invisible (opacité 8%) contenant le logo Forge et le hash cryptographique SHA-256 de l'utilisateur
  - Aucun export en texte brut pour protéger l'intégrité du contenu
- **Optimisation Mémoire** : Chargement paresseux à l'unité (`LIMIT 1`), aucun dump en mémoire vive

## 🚀 CI/CD & Build Cloud (GitHub Actions)
Le workflow GitHub Actions `.github/workflows/build.yml` compile automatiquement l'APK de debug à chaque push, permettant d'économiser les ressources RAM locales.
L'artefact APK est disponible en téléchargement direct dans l'onglet **Actions** du dépôt GitHub.
