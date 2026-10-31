Crée une app Android native 100% Kotlin + Jetpack Compose, package name: com.karlitodev.forge. Pas de WebView.

STRUCTURE:
- ui: modifiable
- data: Room + Billing, ne pas réécrire après création
- Ne charge jamais tout au démarrage

DATABASE & CITATIONS:
- Room table `quotes(id, text, lang, day, isSeen)`, `users(name, lang)`
- Pré-remplis 21 citations perso Forge en dur dans Room (3 par jour x 7 jours, FR). Pas de JSON externe pour V1. Le système GitHub privé pour télécharger de nouvelles citations sera ajouté après la création de l'app, prévois une fonction `syncFromRemote()` vide pour plus tard.
- Logique: affiche une citation où `isSeen = false`. Une fois affichée, passe à `isSeen = true`. Une citation ne se répète JAMAIS.
- Si plus de citations: affiche écran "Tu as tout lu. On n'a plus de Forge pour aujourd'hui. Reviens demain, on forge les suivantes pour toi." avec ton style qui fait sentir à l'utilisateur qu'il a dévoré le stock parce qu'il aime trop.

ONBOARDING (3 écrans):
- Ecran 1: Comment on t'appelle? EditText, save DataStore
- Ecran 2: Langue? FR/EN/ES, save DataStore
- Ecran 3: Bienvenue

ECRAN PRINCIPAL:
- Affiche 1 citation non vue depuis Room. Bouton Suivant.
- Card Widget floutée avec cadenas: "Forge Premium - Widgets + Partage"
- FLAG_SECURE activé sur toute l'app: `window.setFlags(FLAG_SECURE, FLAG_SECURE)` pour bloquer les captures d'écran pour free ET premium.

PAIEMENT & PARTAGE:
- Play Billing Library v6. Produit INAPP `forge_premium_lifetime` 2.99€. Vérification via `queryPurchases`, pas de tracking device.
- Bouton Partager visible SEULEMENT si premium. Si free: bouton grisé avec cadenas.
- Quand premium partage: génère une image bitmap de la citation avec watermark invisible: ajoute en bas à droite en 8% d'opacité un petit logo "Forge" + userID hashé, quasi invisible à l'oeil mais détectable par IA. Pas de partage texte brut.
- Essai gratuit 7 jours géré par Play Console plus tard, pas dans le code V1.

DESIGN:
- Material 3, fond #F5F1E8, texte #121212, AnimatedContent simple.
- Interdit: WebView, JSON au démarrage, Accessibility, VPN.
