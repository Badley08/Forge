# Forge

Discipline quotidienne inspirée du stoïcisme. Une citation à la fois, jamais répétée, choisie selon le jour et la météo.

## Table des matières

- [Vision](#vision)
- [Fonctionnalités](#fonctionnalités)
- [Comparatif Free / Premium](#comparatif-free--premium)
- [Stack technique](#stack-technique)
- [Architecture](#architecture)
- [Structure du projet](#structure-du-projet)
- [Démarrage](#démarrage)
- [Base de citations distante](#base-de-citations-distante)
- [Confidentialité et sécurité](#confidentialité-et-sécurité)
- [Monétisation](#monétisation)
- [Conformité Play Store](#conformité-play-store)
- [Feuille de route](#feuille-de-route)
- [Contribuer](#contribuer)
- [Licence](#licence)

## Vision

Forge n'est pas un flux. C'est une app à une seule citation : on l'ouvre, on lit, on glisse, on repart. Le ton est direct, sobre, sans cliché. Aucune citation ne revient deux fois, et quand le stock est épuisé l'app le dit avec une personnalité assumée plutôt qu'un message d'erreur.

Principes non négociables :

- Une citation à la fois, jamais une liste.
- Rareté réelle : pas de répétition, pas de régénération artificielle.
- Zéro tracking : pas d'identifiant d'appareil, pas de SDK publicitaire ou analytique.
- Zéro emoji dans toute l'interface.
- Contenu protégé : capture d'écran bloquée pour tout le monde, partage uniquement via image filigranée pour les abonnés Premium.
- La provenance de la base de citations n'apparaît nulle part dans l'app, le code visible ou les chaînes de caractères.

## Fonctionnalités

### Onboarding

1. Détection automatique de la langue de l'appareil (FR / EN / ES), avec repli sur l'anglais si la langue n'est pas supportée.
2. L'utilisateur peut changer la langue manuellement ; chaque option affiche son propre nom dans sa propre langue (ex. "Français", "English", "Español") accompagné du drapeau du pays associé, pour que le choix soit immédiatement lisible sans traduction mentale.
3. Prénom de l'utilisateur.
4. Écran de bienvenue.

### Écran principal

- Citations présentées en **cartes à glisser** (swipe), pas de bouton "Suivant". Glissement à droite : citation suivante. Glissement à gauche : citation précédente *uniquement* parmi les citations déjà vues de la session en cours (on ne peut jamais revoir une citation déjà marquée comme consommée après avoir quitté l'app).
- Sélection de catégorie : jusqu'à **10 catégories** (ex. poétique, philosophique, discipline, stoïcisme, résilience, ambition, calme, lucidité, action, détachement). Catégories verrouillées au-delà de la limite du palier de l'utilisateur, avec indicateur visuel sobre (pas de cadenas criard, cohérent avec le design).
- Sélection contextuelle de la citation : le moteur croise le **jour de la semaine** et la **météo locale approximative** (ensoleillé, pluie, neige, nuageux) pour orienter le ton de la citation proposée dans la catégorie choisie, sans jamais l'annoncer explicitement à l'utilisateur. C'est une influence de sélection, pas une étiquette affichée.
- Carte Widget floutée avec indication Premium pour les utilisateurs free.
- Bouton Partager actif uniquement en Premium.

### Écran "stock épuisé"

Affiché quand il n'y a plus de citation non vue, dans la langue et la catégorie actives :

> **Tu as tout lu.**
> On n'a plus de Forge pour aujourd'hui. Reviens demain, on forge les suivantes pour toi.

## Comparatif Free / Premium

| | Free | Premium |
|---|---|---|
| Citations disponibles | 7 jours de contenu (environ 70 citations) | Catalogue complet, mis à jour à distance |
| Catégories | 3 | Jusqu'à 10 |
| Fréquence de renouvellement | Standard | 1 nouvelle citation accessible toutes les 30 minutes |
| Widgets écran d'accueil | Non | Oui |
| Partage (image filigranée) | Non | Oui |
| Téléchargement de nouvelles citations (GitHub) | Non | Oui |
| Capture d'écran | Bloquée | Bloquée |

Le palier free est volontairement généreux (une semaine complète) pour donner une vraie première impression du produit avant la demande d'achat.

## Stack technique

| Élément | Choix |
|---|---|
| Langage | C# (.NET 9, cible Android d'abord) |
| UI | Avalonia UI (XAML), animations natives Avalonia pour le swipe de cartes |
| Architecture | MVVM + Repository, `CommunityToolkit.Mvvm` pour les propriétés observables et commandes |
| Injection de dépendances | `Microsoft.Extensions.DependencyInjection` |
| Persistance structurée | SQLite via `sqlite-net-pcl` (équivalent fonctionnel de Room) |
| Préférences | `Microsoft.Maui.Storage.Preferences` ou stockage clé-valeur chiffré équivalent côté Avalonia.Android |
| Paiement | `Plugin.InAppBilling` (wrapper direct Google Play Billing, pas de service tiers) |
| Météo et position | Permission de localisation approximative, service météo minimal (voir section dédiée) |
| Asynchrone | `async`/`await`, pas de dépendance réactive lourde superflue |

**Clause de repli obligatoire** : si `Plugin.InAppBilling` ou le pipeline de publication Avalonia.Android présente un blocage réel en cours de développement (bug bloquant, incompatibilité de version, rejet Play Store lié au runtime), le projet repasse sur Kotlin natif + Jetpack Compose avec Play Billing Library officielle. Cette clause doit être vérifiée **avant** d'investir dans le reste du développement, via un prototype minimal "Hello World + un achat test".

## Architecture

```
Vue (Avalonia XAML)
      ↕ binding
ViewModel (état observable, commandes)
      ↕ appels asynchrones
Repository (Quotes, User, Billing, Weather)
      ↕
Sources de données (SQLite local, Preferences, Plugin.InAppBilling, service météo, GitHub distant)
```

Règles :

- Les Vues ne contiennent aucune logique métier, uniquement du binding et de l'animation.
- Les ViewModels ne touchent jamais directement SQLite, les préférences ou Billing : tout passe par un Repository injecté.
- Les Repositories sont les seuls points de contact avec l'extérieur (réseau, disque, Billing, capteurs).

## Structure du projet

```
Forge/
├── Forge.Core/                 (logique partagée : ViewModels, Repositories, modèles)
│   ├── Models/
│   ├── ViewModels/
│   └── Repositories/
├── Forge.Data/                 (STABLE après création : SQLite, schémas, seed)
│   ├── Local/
│   ├── Preferences/
│   └── RemoteQuoteSource/      (client de l'index distant, fortement obfusqué en release)
├── Forge.UI/                   (MODIFIABLE : XAML, styles, animations, ressources)
│   ├── Views/
│   ├── Styles/
│   └── Assets/
└── Forge.Android/               (tête de plateforme Android, manifest, permissions, Billing)
```

## Démarrage

### Prérequis

- .NET SDK 9.0 ou supérieur
- Workload Android : `dotnet workload install android`
- Un appareil ou émulateur Android avec Google Play Services (requis pour tester Billing)
- Un compte Google Play Console avec le produit `forge_premium_lifetime` configuré pour les tests internes

### Installation

```bash
git clone https://github.com/<ton-compte>/forge.git
cd forge
dotnet restore
```

### Lancer en debug

```bash
dotnet build -t:Run -f net9.0-android
```

### Build de release

```bash
dotnet publish Forge.Android -c Release -f net9.0-android
```

La signature, l'obfuscation (R8 ou équivalent .NET, voir plus bas) et la configuration ProGuard/R8 sont appliquées uniquement sur les builds Release, jamais en Debug.

## Base de citations distante

Réservée à la **V2**, non implémentée en V1. Prévue pour fonctionner ainsi :

- Un dépôt GitHub privé héberge un fichier `index.min.json` qui référence des dossiers de citations par catégorie et par langue.
- Seuls les utilisateurs Premium, après vérification d'achat réussie, déclenchent le téléchargement.
- Le code qui gère cet accès (URL, parsing, clé d'accès éventuelle) est isolé dans un module dédié et fait l'objet de l'**obfuscation la plus agressive du projet** : noms de classes et de méthodes renommés, chaînes sensibles chiffrées en mémoire plutôt que stockées en clair, et aucune référence à GitHub, à une "base de citations" ou à la nature de la source dans les logs, les chaînes visibles, les noms de ressources ou les commentaires de code livrés.
- Objectif explicite : qu'une personne décompilant l'APK/AAB ne puisse pas retrouver facilement l'origine ni l'URL du contenu Premium.
- En V1, seules les citations embarquées localement sont présentes ; `SyncFromRemoteAsync()` existe en signature vide, prête pour la V2, mais n'est appelée nulle part.

## Confidentialité et sécurité

- Aucune collecte d'identifiant d'appareil, publicitaire ou de compte. Un identifiant anonyme aléatoire est généré localement à l'installation, uniquement pour le filigrane de partage.
- Capture et enregistrement d'écran bloqués sur toute l'application, pour les utilisateurs free et Premium, sur tous les écrans.
- Le partage Premium ne génère jamais de texte brut copiable : uniquement une image bitmap avec filigrane à 8 % d'opacité (logo "Forge" + hash tronqué de l'identifiant anonyme), peu visible à l'oeil, détectable par analyse automatique.
- La localisation n'est demandée qu'une fois, en position approximative, uniquement pour pondérer le choix de citation selon la météo. Si la permission est refusée, l'app fonctionne normalement en se basant uniquement sur le jour de la semaine.
- Aucune requête réseau avant la fin de l'onboarding. Aucune requête réseau liée aux citations tant que l'utilisateur n'est pas Premium.

## Monétisation

- Produit unique **achat à vie** (`forge_premium_lifetime`), prix affiché dynamiquement depuis Google Play, jamais codé en dur.
- Vérification de l'état Premium via l'API native Play Billing à chaque ouverture et retour au premier plan.
- Aucun essai gratuit codé en V1 : si un essai de 7 jours est mis en place, il est configuré entièrement côté Play Console, sans logique applicative dédiée.

## Conformité Play Store

Points qui ne figuraient pas dans la demande initiale mais qui sont **obligatoires** pour publier cette app et qu'il faut anticiper avant la soumission :

- **Politique de confidentialité publiée** : obligatoire dès qu'une app demande une permission de localisation et propose des achats intégrés. Doit être hébergée à une URL publique et renseignée dans la Play Console avant toute soumission.
- **Formulaire "Sécurité des données" (Data Safety)** : à remplir dans la Play Console en déclarant précisément l'usage de la localisation approximative (uniquement pour la sélection de contenu, non partagée, non vendue) et l'absence de collecte d'identifiants publicitaires.
- **Justification de la permission de localisation** : Google peut demander une déclaration explicite d'usage lors de la revue ; prépare une description courte ("personnalisation du contenu affiché selon la météo locale") prête à soumettre.
- **Gestion des remboursements et annulations** : même pour un achat unique, prévoir le cas `Plugin.InAppBilling` renvoyant un achat révoqué (remboursement) et repasser l'utilisateur en free proprement.
- **Accessibilité minimale** : labels sur les éléments interactifs et contraste suffisant, même si l'app n'implémente pas de support lecteur d'écran avancé en V1 — Google Play vérifie un socle minimal lors de la revue de contenu.

## Feuille de route

V1 volontairement limitée pour garantir une sortie :

- Swipe de citations, jusqu'à 10 catégories, sélection jour + météo, écran stock épuisé, Billing, partage filigrané, blocage capture.

Hors V1, explicitement reporté :

- Téléchargement effectif des citations distantes (`SyncFromRemoteAsync`).
- Widgets d'écran d'accueil.
- Essai gratuit applicatif.
- Notifications de rappel quotidien.

## Contribuer

Projet personnel sous la marque Karlito Dev. Les contributions externes ne sont pas ouvertes pour le moment.

## Licence

Tous droits réservés — Karlito Dev. Aucune licence open source n'est accordée sauf mention contraire explicite.
