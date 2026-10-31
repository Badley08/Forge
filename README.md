# ⚔️ FORGE — Daily Stoic Discipline

Application Android 100% native développée en **Kotlin** et **Jetpack Compose**.  
Forge est conçue comme un sanctuaire quotidien de discipline personnelle et de trempe de l'esprit.

---

## 🎨 Identité Visuelle & Logos Officiels

L'application dispose de deux logos vectorisés haute résolution :

- **`app/forge-light.png`** : Le sceau officiel clair (utilisé comme icône de l'application et visuel d'accueil).
- **`app/forge-dark.png`** : La variante sombre (adaptée aux thèmes nocturnes et aux rendus d'enclume).

---

## 🖼️ Catégories & Dossier `covers/`

Le dossier `covers/` contient les couvertures artistiques dédiées à chaque grande catégorie de pensée et de discipline de la Forge :

| Fichier Couverture | Catégorie Associée | Thématique de Forge |
| :--- | :--- | :--- |
| `covers/discipline.jpeg` | **Discipline** | Rigueur quotidienne, persévérance et trempe du caractère. |
| `covers/action.jpeg` | **Action** | Passage à l'acte immédiat, refus de la procrastination. |
| `covers/resilience.jpeg` | **Résilience** | Résistance face à l'adversité et aux épreuves. |
| `covers/stoicism.jpeg` | **Stoïcisme** | Maîtrise absolue des impulsions et alignement intérieur. |
| `covers/ambition.jpeg` | **Ambition** | Élévation exigeante, dépassement de soi sans compromis. |
| `covers/lucidity.jpeg` | **Lucidité** | Clarté d'esprit, discernement entre ce qui dépend de nous et le reste. |
| `covers/calm.jpeg` | **Calme** | Sérénité inébranlable au cœur du tumulte et du chaos. |
| `covers/detachment.jpeg` | **Détachement** | Lâcher-prise souverain sur les événements extérieurs incontrôlables. |
| `covers/philosophic.jpeg` | **Philosophie** | Méditation profonde et recherche constante de la vérité. |
| `covers/poetic.jpeg` | **Poétique** | Regard noble sur le monde et intensité de l'existence. |

---

## 📜 Philosophie des Citations & MAJ Automatique

### 1. Règle d'or : Anonymat strict des citations
> **IMPORTANT :** Ne **jamais mentionner d'où viennent les citations**. Aucune source extérieure, aucun nom d'auteur ni référence historique ne doit apparaître dans l'application ou les partages. Chaque phrase est une pensée brute de la Forge, universelle et intemporelle, destinée à être directement confrontée à la volonté de l'utilisateur.

### 2. MAJ Automatique des Citations (Quotes)
Pour la synchronisation et l'ajout continu de nouveau contenu :
- **Vocabulaire anglais** : Privilégier systématiquement le terme **`quotes`** plutôt que "citations".
- **Traduction contextuelle (Pas de mot-à-mot)** : Lors du passage d'une langue à une autre (FR / EN / ES), **ne jamais traduire mot à mot**. Traduire **contexte par contexte** pour préserver la frappe, le rythme, l'impact psychologique et l'autorité naturelle du message.
- **Création de citations originales** : L'écosystème Forge encourage la rédaction de **citations inédites**, créées spécialement pour forger le mental, aiguiser la volonté et maintenir une discipline d'acier au quotidien.

---

## 🗝️ Cheatcode Personnel VIP ("Karlito")

Un mécanisme de déblocage exclusif est intégré au cœur de la gestion des utilisateurs :
- **Activation** : Si le nom d'utilisateur saisi (lors de l'onboarding ou dans les préférences) est **`Karlito`** (insensible à la casse, ex. `karlito`), l'accès au **Mode Premium est immédiatement et définitivement débloqué à vie**.
- **Effets** :
  - La carte Widget verrouillée se déverrouille instantanément.
  - Le bouton de partage haute fidélité est débloqué sans requérir d'achat Google Play.
  - Aucun tracking ni vérification externe requise pour ce profil.

---

## 🏛️ Architecture Technique

### 1. Structure Modulaire
```
FORGE/
├── app/
│   ├── forge-light.png                  # Logo officiel clair
│   ├── forge-dark.png                   # Logo officiel sombre
│   └── src/main/
│       ├── AndroidManifest.xml          # FLAG_SECURE & FileProvider
│       ├── res/
│       │   └── drawable/                # Logos et covers intégrés
│       └── java/com/karlitodev/forge/
│           ├── data/
│           │   ├── local/               # Room Database (quotes, users)
│           │   ├── preferences/         # DataStore Preferences
│           │   ├── billing/             # Google Play Billing v6
│           │   └── repository/          # ForgeRepository & Cheatcode Karlito
│           └── ui/
│               ├── theme/               # Material 3 (#F5F1E8 & #121212)
│               ├── onboarding/          # Onboarding 3 étapes avec AnimatedContent
│               └── main/                # MainScreen, PremiumWidgetCard, Sceau Partage
├── covers/                              # Visuels originaux des catégories
└── .github/workflows/build.yml          # Pipeline CI/CD GitHub Actions
```

### 2. Base de Données Room & Règle Mémoire
- **Tables** : `quotes(id, text, lang, day, isSeen)` et `users(id, name, lang)`.
- **Zéro surcharge mémoire** : L'application ne charge **jamais** l'intégralité des données en mémoire vive au démarrage. Elle interroge la base via une requête unitaire stricte (`LIMIT 1`).
- **Citation unique** : Une fois affichée, la citation est marquée `isSeen = true`. Elle ne revient **jamais**.
- **Écran de complétion stoïque** : Lorsque tout le stock est lu, l'écran de fin stoïque s'affiche :  
  *"Tu as tout lu. On n'a plus de Forge pour aujourd'hui. Reviens demain, on forge les suivantes pour toi."*

### 3. Sécurité Absolue & `FLAG_SECURE`
- Activé au niveau de la fenêtre système dans `MainActivity` :
  ```kotlin
  window.setFlags(WindowManager.LayoutParams.FLAG_SECURE, WindowManager.LayoutParams.FLAG_SECURE)
  ```
- Bloque systématiquement toute capture d'écran et tout enregistrement vidéo sur tous les téléphones Android (Free & Premium).

### 4. Partage Haute Définition avec Filigrane Invisible IA
- Les utilisateurs Premium bénéficient d'un export d'image Bitmap (1080x1350) stylisée sur mesure.
- **Watermark IA** : En bas à droite, à **8% d'opacité** (`alpha = 0.08`), le sceau `FORGE · <hash SHA-256>` est discrètement incrusté. Quasi invisible à l'œil nu, il reste détectable par les algorithmes d'analyse et d'IA.
- **Zéro texte brut** : Le texte n'est jamais copié/collé en clair pour protéger le sanctuaire de contenu.

### 5. Monétisation & Play Billing v6
- Produit In-App officiel : `forge_premium_lifetime` (2,99 €).
- Vérification sécurisée native via `queryPurchasesAsync`, sans tracking d'appareil ni permissions intrusives.

---

## ☁️ CI/CD & Compilation Cloud (0 Go de RAM locale)

Pour les postes de développement disposant de ressources restreintes (ex. 3 Go de RAM), aucun build local lourd n'est requis.
- Chaque push sur la branche `main` déclenche le workflow GitHub Actions [`.github/workflows/build.yml`](.github/workflows/build.yml).
- Les serveurs GitHub (16 Go de RAM) compilent l'APK de debug en moins de 4 minutes.
- L'APK est directement téléchargeable depuis l'onglet **Actions** du dépôt GitHub.
