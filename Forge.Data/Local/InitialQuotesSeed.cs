namespace Forge.Data.Local;

public static class InitialQuotesSeed
{
    public static List<QuoteRecord> GetSeedQuotes()
    {
        var quotes = new List<QuoteRecord>();

        // ==========================================
        // French (FR) - 10 Categories x 7 Days = 70 Quotes
        // ==========================================

        // 1. Discipline
        quotes.Add(new() { Text = "Tu as le pouvoir sur ton esprit, pas sur les événements extérieurs. Comprends cela, et tu trouveras ta force.", Lang = "FR", Day = 1, Category = "discipline", WeatherHint = "any" });
        quotes.Add(new() { Text = "L'homme conquérant est d'abord celui qui s'est conquis lui-même.", Lang = "FR", Day = 2, Category = "discipline", WeatherHint = "any" });
        quotes.Add(new() { Text = "L'acier ne prend forme que sous les coups répétés du marteau. Sois l'enclume et le forgeron de ta volonté.", Lang = "FR", Day = 3, Category = "discipline", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "L'obstacle est la forge où se trempe la volonté inébranlable.", Lang = "FR", Day = 4, Category = "discipline", WeatherHint = "rain" });
        quotes.Add(new() { Text = "La liberté commence par l'obéissance aux règles que l'on s'est soi-même imposées.", Lang = "FR", Day = 5, Category = "discipline", WeatherHint = "any" });
        quotes.Add(new() { Text = "Ce qui ne te détruit pas affine ta posture. Forge ton âme dans le silence, la retenue et la sueur.", Lang = "FR", Day = 6, Category = "discipline", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "La discipline n'est pas une punition, c'est l'armure de celui qui refuse d'être esclave de ses impulsions.", Lang = "FR", Day = 7, Category = "discipline", WeatherHint = "any" });

        // 2. Action
        quotes.Add(new() { Text = "Ce qui fait obstacle à l'action fait avancer l'action. Ce qui barre le chemin devient le chemin.", Lang = "FR", Day = 1, Category = "action", WeatherHint = "any" });
        quotes.Add(new() { Text = "Ne perds plus de temps à discuter de ce que doit être un homme de bien. Sois-en un.", Lang = "FR", Day = 2, Category = "action", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Le premier pas n'assure pas le succès, mais il brise la paralysie du doute.", Lang = "FR", Day = 3, Category = "action", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Agis comme si chaque heure était la dernière de ton existence : sans faux-fuyant, sans indolence.", Lang = "FR", Day = 4, Category = "action", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Fais chaque tâche dans ton existence avec la rigueur et la présence comme si c'était la dernière.", Lang = "FR", Day = 5, Category = "action", WeatherHint = "any" });
        quotes.Add(new() { Text = "Les mots ne bâtissent aucune forteresse. Seule la frappe de ton labeur laisse une empreinte.", Lang = "FR", Day = 6, Category = "action", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Le doute se nourrit de l'attente ; il meurt dès que l'action s'engage.", Lang = "FR", Day = 7, Category = "action", WeatherHint = "any" });

        // 3. Resilience
        quotes.Add(new() { Text = "La difficulté révèle l'homme. Quand une épreuve survient, souviens-toi que la vie t'a opposé à un rude adversaire pour te fortifier.", Lang = "FR", Day = 1, Category = "resilience", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Le feu transforme tout ce qu'on y jette en flammes et en clarté. Sois semblable à ce feu souverain.", Lang = "FR", Day = 2, Category = "resilience", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Ne te plains jamais du fardeau. Rends tes épaules plus larges et tes reins plus solides.", Lang = "FR", Day = 3, Category = "resilience", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "La cicatrice est la preuve que la douleur a été surmontée, non pas qu'elle a vaincu.", Lang = "FR", Day = 4, Category = "resilience", WeatherHint = "rain" });
        quotes.Add(new() { Text = "L'arbre battu par les vents les plus violents est celui dont les racines s'ancrent le plus profondément dans la roche.", Lang = "FR", Day = 5, Category = "resilience", WeatherHint = "snow" });
        quotes.Add(new() { Text = "Chaque chute n'est qu'un point d'appui supplémentaire pour le bond à venir.", Lang = "FR", Day = 6, Category = "resilience", WeatherHint = "any" });
        quotes.Add(new() { Text = "Tu as survécu à tout ce que le monde a lancé contre toi jusqu'ici. Ce jour ne sera pas différent.", Lang = "FR", Day = 7, Category = "resilience", WeatherHint = "any" });

        // 4. Stoicism
        quotes.Add(new() { Text = "Le fer ne s'aiguise que contre le fer ; l'esprit ne s'endurcit que dans l'épreuve.", Lang = "FR", Day = 1, Category = "stoicism", WeatherHint = "any" });
        quotes.Add(new() { Text = "Vivre, c'est lutter. Pas de repos pour l'acier avant que la lame ne soit parfaitement forgée.", Lang = "FR", Day = 2, Category = "stoicism", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Accepte ce que le destin t'envoie, et aime les hommes avec qui le sort t'a lié, mais de tout ton coeur.", Lang = "FR", Day = 3, Category = "stoicism", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Ne cherche pas à changer le cours du monde ; change ton propre regard sur ce qui advient.", Lang = "FR", Day = 4, Category = "stoicism", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Rien n'est bon ou mauvais en soi, seule la rectitude du jugement détermine la valeur de toute chose.", Lang = "FR", Day = 5, Category = "stoicism", WeatherHint = "any" });
        quotes.Add(new() { Text = "Le sage ne dépend de rien que de lui-même. Sa forteresse intérieure demeure imprenable.", Lang = "FR", Day = 6, Category = "stoicism", WeatherHint = "any" });
        quotes.Add(new() { Text = "Rappelle-toi sans cesse : la durée de la vie humaine n'est qu'un point ; sa substance, un fleuve qui s'écoule.", Lang = "FR", Day = 7, Category = "stoicism", WeatherHint = "cloudy" });

        // 5. Ambition
        quotes.Add(new() { Text = "Ce n'est pas parce que les choses sont difficiles que nous n'osons pas, c'est parce que nous n'osons pas qu'elles sont difficiles.", Lang = "FR", Day = 1, Category = "ambition", WeatherHint = "any" });
        quotes.Add(new() { Text = "Combien de temps vas-tu encore attendre avant d'exiger le meilleur et le plus strict de toi-même ?", Lang = "FR", Day = 2, Category = "ambition", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "L'élévation véritable ne consiste pas à dépasser les autres, mais à surpasser l'homme que tu étais hier.", Lang = "FR", Day = 3, Category = "ambition", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Vise le sommet inaccessible : si tu tombes, tu seras toujours plus haut que la foule immobile.", Lang = "FR", Day = 4, Category = "ambition", WeatherHint = "any" });
        quotes.Add(new() { Text = "La grandeur n'est pas un don ; c'est une décision prise chaque matin contre le confort.", Lang = "FR", Day = 5, Category = "ambition", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Les esprits médiocres recherchent la facilité. Les esprits ardents cherchent l'épreuve qui les révèle.", Lang = "FR", Day = 6, Category = "ambition", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Ne mesure jamais ton ambition à la taille de tes peurs, mais à la puissance de ta persévérance.", Lang = "FR", Day = 7, Category = "ambition", WeatherHint = "any" });

        // 6. Lucidity
        quotes.Add(new() { Text = "Nous souffrons plus souvent en imagination qu'en réalité.", Lang = "FR", Day = 1, Category = "lucidity", WeatherHint = "any" });
        quotes.Add(new() { Text = "Aucun vent n'est favorable à celui qui ne sait pas vers quel port il navigue.", Lang = "FR", Day = 2, Category = "lucidity", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Dépouille chaque événement de ses ornements trompeurs ; regarde-le dans sa nudité absolue.", Lang = "FR", Day = 3, Category = "lucidity", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Regarde les choses telles qu'elles sont, non telles que tes craintes les ont peintes.", Lang = "FR", Day = 4, Category = "lucidity", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "La clarté de l'esprit est l'épée la plus tranchante pour pourfendre l'illusion.", Lang = "FR", Day = 5, Category = "lucidity", WeatherHint = "any" });
        quotes.Add(new() { Text = "Ceux qui s'aveuglent eux-mêmes pour fuir la vérité creusent leur propre tombeau sans le savoir.", Lang = "FR", Day = 6, Category = "lucidity", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Reconnais ce qui dépend de toi et méprise souverainement ce qui ne t'appartient pas.", Lang = "FR", Day = 7, Category = "lucidity", WeatherHint = "any" });

        // 7. Calm
        quotes.Add(new() { Text = "Il n'y a qu'une seule façon d'accéder au calme intérieur : cesser de s'inquiéter pour ce qui dépasse le pouvoir de notre volonté.", Lang = "FR", Day = 1, Category = "calm", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Sois semblable au promontoire contre lequel les vagues se brisent sans cesse ; il demeure debout et apaise la fureur des flots.", Lang = "FR", Day = 2, Category = "calm", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Le tumulte extérieur ne peut franchir le seuil d'une âme fermement retranchée dans sa paix.", Lang = "FR", Day = 3, Category = "calm", WeatherHint = "any" });
        quotes.Add(new() { Text = "Dans le silence intérieur réside une autorité que le fracas des foules ne pourra jamais ébranler.", Lang = "FR", Day = 4, Category = "calm", WeatherHint = "snow" });
        quotes.Add(new() { Text = "Ne te hâte jamais dans la colère. Laisse le feu s'éteindre avant de poser la parole ou le geste.", Lang = "FR", Day = 5, Category = "calm", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "La sérénité n'est pas l'absence d'orage, mais la certitude d'être le roc au milieu de la tempête.", Lang = "FR", Day = 6, Category = "calm", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Respire profondément. Le monde s'agite, mais toi, demeure inaltérable.", Lang = "FR", Day = 7, Category = "calm", WeatherHint = "any" });

        // 8. Detachment
        quotes.Add(new() { Text = "Ne demande pas que les événements arrivent comme tu le souhaites, mais accueille-les avec droiture tels qu'ils surviennent.", Lang = "FR", Day = 1, Category = "detachment", WeatherHint = "any" });
        quotes.Add(new() { Text = "Garde ton attention fixée uniquement sur ce qui dépend de toi. Le reste n'est que fumée et dispersion.", Lang = "FR", Day = 2, Category = "detachment", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Rien de ce que la fortune donne ne t'appartient en propre. Ce qu'elle reprend n'était qu'un prêt éphémère.", Lang = "FR", Day = 3, Category = "detachment", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Possède tes biens, mais ne laisse aucun de tes biens te posséder.", Lang = "FR", Day = 4, Category = "detachment", WeatherHint = "any" });
        quotes.Add(new() { Text = "La louange d'autrui n'ajoute rien à ta valeur ; son mépris ne t'enlève rien.", Lang = "FR", Day = 5, Category = "detachment", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Lâche prise sur le passé qui n'est plus et sur l'avenir qui n'existe pas encore. L'instant présent est ton seul royaume.", Lang = "FR", Day = 6, Category = "detachment", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Quand tu ne crains plus de tout perdre, tu es enfin devenu libre.", Lang = "FR", Day = 7, Category = "detachment", WeatherHint = "any" });

        // 9. Philosophy
        quotes.Add(new() { Text = "La chance est ce qui arrive quand la préparation méthodique rencontre l'opportunité.", Lang = "FR", Day = 1, Category = "philosophic", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Tu as achevé le cycle des sept jours. Tu as dompté le tumulte intérieur. Tiens bon, avance sans faiblir.", Lang = "FR", Day = 2, Category = "philosophic", WeatherHint = "any" });
        quotes.Add(new() { Text = "La philosophie n'est pas un verbiage de salon, c'est l'exercice quotidien de vivre avec droiture.", Lang = "FR", Day = 3, Category = "philosophic", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Examine tes pensées avant qu'elles ne deviennent tes actes ; examine tes actes avant qu'ils ne deviennent ton destin.", Lang = "FR", Day = 4, Category = "philosophic", WeatherHint = "rain" });
        quotes.Add(new() { Text = "La brièveté de la vie n'est pas une malédiction pour celui qui sait habiter chaque seconde.", Lang = "FR", Day = 5, Category = "philosophic", WeatherHint = "any" });
        quotes.Add(new() { Text = "Connais-toi toi-même, et tu n'auras plus rien à redouter des hommes ni des dieux.", Lang = "FR", Day = 6, Category = "philosophic", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "La sagesse consiste à ordonner son âme comme une cité bien gouvernée.", Lang = "FR", Day = 7, Category = "philosophic", WeatherHint = "any" });

        // 10. Poetic
        quotes.Add(new() { Text = "La discipline d'aujourd'hui est la seule souveraineté et liberté de demain.", Lang = "FR", Day = 1, Category = "poetic", WeatherHint = "any" });
        quotes.Add(new() { Text = "Dans l'écho silencieux de la nuit, seule la clarté d'une conscience pure projette sa lumière.", Lang = "FR", Day = 2, Category = "poetic", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "L'acier forgé dans l'ombre ne craint point l'éclat du jour.", Lang = "FR", Day = 3, Category = "poetic", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Une étincelle suffit pour embraser la nuit ; une seule résolution suffit pour relever un destin.", Lang = "FR", Day = 4, Category = "poetic", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Le vent emporte les feuilles mortes, mais respecte les branches inflexibles.", Lang = "FR", Day = 5, Category = "poetic", WeatherHint = "snow" });
        quotes.Add(new() { Text = "Marche d'un pas ferme sur la terre brûlante ; le sol s'incline sous la gravité des hommes droits.", Lang = "FR", Day = 6, Category = "poetic", WeatherHint = "any" });
        quotes.Add(new() { Text = "Au bout de l'effort, il n'y a pas de gloire éphémère, mais la noble fierté d'avoir tenu sans fléchir.", Lang = "FR", Day = 7, Category = "poetic", WeatherHint = "any" });

        // ==========================================
        // ENGLISH (EN) - 10 Categories x 7 Days = 70 Quotes
        // ==========================================

        // 1. DISCIPLINE
        quotes.Add(new() { Text = "You have power over your mind, not outside events. Realize this, and you will find strength.", Lang = "EN", Day = 1, Category = "discipline", WeatherHint = "any" });
        quotes.Add(new() { Text = "The conquering man is first the one who has conquered himself.", Lang = "EN", Day = 2, Category = "discipline", WeatherHint = "any" });
        quotes.Add(new() { Text = "Steel only takes shape under repeated hammer blows. Be both anvil and blacksmith of your will.", Lang = "EN", Day = 3, Category = "discipline", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "The impediment to action advances action. What stands in the way becomes the way.", Lang = "EN", Day = 4, Category = "discipline", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Freedom begins with voluntary obedience to the rules you set for yourself.", Lang = "EN", Day = 5, Category = "discipline", WeatherHint = "any" });
        quotes.Add(new() { Text = "What does not destroy you refines your stance. Forge your soul in silence and sweat.", Lang = "EN", Day = 6, Category = "discipline", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Discipline is not punishment; it is the armor of one who refuses to be a slave to impulse.", Lang = "EN", Day = 7, Category = "discipline", WeatherHint = "any" });

        // 2. ACTION
        quotes.Add(new() { Text = "Waste no more time arguing about what a good man should be. Be one.", Lang = "EN", Day = 1, Category = "action", WeatherHint = "any" });
        quotes.Add(new() { Text = "The first step does not ensure victory, but it shatters the paralysis of doubt.", Lang = "EN", Day = 2, Category = "action", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Perform each action as if it were the last of your life, without evasion or indolence.", Lang = "EN", Day = 3, Category = "action", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Words build no fortress. Only the strike of your labor leaves a lasting imprint.", Lang = "EN", Day = 4, Category = "action", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Doubt feeds on delay; it dies the moment action begins.", Lang = "EN", Day = 5, Category = "action", WeatherHint = "any" });
        quotes.Add(new() { Text = "Do every task in your life with the strictness and presence as though it were your last.", Lang = "EN", Day = 6, Category = "action", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Stand upright, not straightened by others.", Lang = "EN", Day = 7, Category = "action", WeatherHint = "any" });

        // 3. RESILIENCE
        quotes.Add(new() { Text = "Difficulties show what men are. When trouble comes, remember that fate has paired you with a tough sparring partner.", Lang = "EN", Day = 1, Category = "resilience", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Fire turns everything thrown into it into flame and brightness. Be like that sovereign fire.", Lang = "EN", Day = 2, Category = "resilience", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Never complain of the burden. Build broader shoulders and firmer footing.", Lang = "EN", Day = 3, Category = "resilience", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "A scar is proof that pain was endured and overcome, not that it triumphed.", Lang = "EN", Day = 4, Category = "resilience", WeatherHint = "rain" });
        quotes.Add(new() { Text = "The tree lashed by the fiercest winds sinks its roots deepest into the rock.", Lang = "EN", Day = 5, Category = "resilience", WeatherHint = "snow" });
        quotes.Add(new() { Text = "Every fall is merely another foothold for the leap ahead.", Lang = "EN", Day = 6, Category = "resilience", WeatherHint = "any" });
        quotes.Add(new() { Text = "You have survived everything life has thrown at you until this day. Today will be no different.", Lang = "EN", Day = 7, Category = "resilience", WeatherHint = "any" });

        // 4. STOICISM
        quotes.Add(new() { Text = "Iron sharpeneth iron; so does the spirit harden only through trial.", Lang = "EN", Day = 1, Category = "stoicism", WeatherHint = "any" });
        quotes.Add(new() { Text = "To live is to war. No rest for the steel until the blade is forged true.", Lang = "EN", Day = 2, Category = "stoicism", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Accept whatever comes woven into the pattern of your destiny, for what could more aptly fit your needs?", Lang = "EN", Day = 3, Category = "stoicism", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Seek not for events to happen as you wish, but rather wish for them to happen as they do.", Lang = "EN", Day = 4, Category = "stoicism", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Things themselves do not hurt us; only our judgment regarding them causes affliction.", Lang = "EN", Day = 5, Category = "stoicism", WeatherHint = "any" });
        quotes.Add(new() { Text = "The wise man depends on nothing outside his own integrity. His inner fortress stands unbreached.", Lang = "EN", Day = 6, Category = "stoicism", WeatherHint = "any" });
        quotes.Add(new() { Text = "Remember always: the time of human life is a point; its essence, a fleeting stream.", Lang = "EN", Day = 7, Category = "stoicism", WeatherHint = "cloudy" });

        // 5. AMBITION
        quotes.Add(new() { Text = "It is not because things are difficult that we do not dare; it is because we do not dare that they are difficult.", Lang = "EN", Day = 1, Category = "ambition", WeatherHint = "any" });
        quotes.Add(new() { Text = "How long are you going to wait before demanding the highest and strictest standard from yourself?", Lang = "EN", Day = 2, Category = "ambition", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "True elevation is not in surpassing others, but in transcending the man you were yesterday.", Lang = "EN", Day = 3, Category = "ambition", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Aim for the unreachable summit: even if you stumble, you will stand far above the idle crowd.", Lang = "EN", Day = 4, Category = "ambition", WeatherHint = "any" });
        quotes.Add(new() { Text = "Greatness is not a gift; it is a discipline renewed every morning against comfort.", Lang = "EN", Day = 5, Category = "ambition", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Mediocre minds seek comfort. Ardent spirits seek the trial that reveals them.", Lang = "EN", Day = 6, Category = "ambition", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Never measure your ambition by the size of your fears, but by the weight of your endurance.", Lang = "EN", Day = 7, Category = "ambition", WeatherHint = "any" });

        // 6. LUCIDITY
        quotes.Add(new() { Text = "We suffer more often in imagination than in reality.", Lang = "EN", Day = 1, Category = "lucidity", WeatherHint = "any" });
        quotes.Add(new() { Text = "If one does not know to which port one is sailing, no wind is favorable.", Lang = "EN", Day = 2, Category = "lucidity", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Strip every circumstance of its deceptive garments; behold it in its naked truth.", Lang = "EN", Day = 3, Category = "lucidity", WeatherHint = "rain" });
        quotes.Add(new() { Text = "See things as they are, not as your anxieties painted them.", Lang = "EN", Day = 4, Category = "lucidity", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Clarity of mind is the sharpest blade to sever illusion.", Lang = "EN", Day = 5, Category = "lucidity", WeatherHint = "any" });
        quotes.Add(new() { Text = "Those who blind themselves to avoid truth dig their own downfall in ignorance.", Lang = "EN", Day = 6, Category = "lucidity", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Acknowledge what is within your command, and disdain with sovereignty what lies beyond.", Lang = "EN", Day = 7, Category = "lucidity", WeatherHint = "any" });

        // 7. CALM
        quotes.Add(new() { Text = "There is only one way to serenity: to stop worrying over things beyond the power of our will.", Lang = "EN", Day = 1, Category = "calm", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Be like the headland on which the waves break ceaselessly; it stands immovable and quells the fury of the water.", Lang = "EN", Day = 2, Category = "calm", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Outer turmoil cannot cross the threshold of a mind fortified in tranquil stillness.", Lang = "EN", Day = 3, Category = "calm", WeatherHint = "any" });
        quotes.Add(new() { Text = "Within inner silence lies an authority that no clamor of crowds can ever disturb.", Lang = "EN", Day = 4, Category = "calm", WeatherHint = "snow" });
        quotes.Add(new() { Text = "Never hasten in anger. Allow the fire to recede before delivering word or deed.", Lang = "EN", Day = 5, Category = "calm", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Serenity is not the absence of the storm, but the certainty of being the unmoving rock amidst it.", Lang = "EN", Day = 6, Category = "calm", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Breathe deeply. The world is in agitation, but you remain unaltered.", Lang = "EN", Day = 7, Category = "calm", WeatherHint = "any" });

        // 8. DETACHMENT
        quotes.Add(new() { Text = "Do not demand that things go as you wish, but accept them with uprightness as they occur.", Lang = "EN", Day = 1, Category = "detachment", WeatherHint = "any" });
        quotes.Add(new() { Text = "Keep your gaze fixed solely on what is yours to control. All else is vapor and dispersal.", Lang = "EN", Day = 2, Category = "detachment", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Nothing given by fortune is truly your own. What it takes away was only an ephemeral loan.", Lang = "EN", Day = 3, Category = "detachment", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Possess your possessions, but let none of your possessions possess you.", Lang = "EN", Day = 4, Category = "detachment", WeatherHint = "any" });
        quotes.Add(new() { Text = "The praise of others adds nothing to your worth; their disdain subtracts nothing.", Lang = "EN", Day = 5, Category = "detachment", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Release the past that is gone, and the future that is not yet born. The present instant is your sole domain.", Lang = "EN", Day = 6, Category = "detachment", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "When you no longer fear losing everything, you are finally free.", Lang = "EN", Day = 7, Category = "detachment", WeatherHint = "any" });

        // 9. PHILOSOPHIC
        quotes.Add(new() { Text = "Luck is what happens when disciplined preparation meets opportunity.", Lang = "EN", Day = 1, Category = "philosophic", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "You have completed the cycle of seven days. You have mastered the tumult within. Stand firm, move forward.", Lang = "EN", Day = 2, Category = "philosophic", WeatherHint = "any" });
        quotes.Add(new() { Text = "Philosophy is not idle chatter; it is the daily practice of living with unyielding integrity.", Lang = "EN", Day = 3, Category = "philosophic", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Examine your thoughts before they become your actions; examine your actions before they become your destiny.", Lang = "EN", Day = 4, Category = "philosophic", WeatherHint = "rain" });
        quotes.Add(new() { Text = "The brevity of life is no curse to one who inhabits every second with full presence.", Lang = "EN", Day = 5, Category = "philosophic", WeatherHint = "any" });
        quotes.Add(new() { Text = "Know yourself, and you will have nothing left to fear from gods or men.", Lang = "EN", Day = 6, Category = "philosophic", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Wisdom consists in ordering your soul as a sovereign, well-governed city.", Lang = "EN", Day = 7, Category = "philosophic", WeatherHint = "any" });

        // 10. POETIC
        quotes.Add(new() { Text = "Today's discipline is tomorrow's only true sovereignty and liberty.", Lang = "EN", Day = 1, Category = "poetic", WeatherHint = "any" });
        quotes.Add(new() { Text = "In the silent echo of night, only the clarity of a pure conscience casts its glow.", Lang = "EN", Day = 2, Category = "poetic", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Steel forged in shadows fears no brightness of the blazing sun.", Lang = "EN", Day = 3, Category = "poetic", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "A single spark is enough to light the dark; a single resolute choice is enough to redeem a destiny.", Lang = "EN", Day = 4, Category = "poetic", WeatherHint = "rain" });
        quotes.Add(new() { Text = "The gale carries away dead leaves, but honors the unyielding boughs.", Lang = "EN", Day = 5, Category = "poetic", WeatherHint = "snow" });
        quotes.Add(new() { Text = "Walk with a steady tread upon the scorching ground; the earth bends to the gravity of resolute men.", Lang = "EN", Day = 6, Category = "poetic", WeatherHint = "any" });
        quotes.Add(new() { Text = "At the end of toil lies no fleeting applause, but the quiet pride of having stood without wavering.", Lang = "EN", Day = 7, Category = "poetic", WeatherHint = "any" });

        // ==========================================
        // Spanish (ES) - 10 Categories x 7 Days = 70 Quotes
        // ==========================================

        // 1. Discipline
        quotes.Add(new() { Text = "Tienes poder sobre tu mente, no sobre los acontecimientos externos. Comprende esto, y hallarás tu fuerza.", Lang = "ES", Day = 1, Category = "discipline", WeatherHint = "any" });
        quotes.Add(new() { Text = "El hombre conquistador es ante todo aquel que se ha conquistado a sí mismo.", Lang = "ES", Day = 2, Category = "discipline", WeatherHint = "any" });
        quotes.Add(new() { Text = "El acero solo toma forma bajo los golpes reiterados del martillo. Sé el yunque y el herrero de tu voluntad.", Lang = "ES", Day = 3, Category = "discipline", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Lo que obstaculiza la acción impulsa la acción. Lo que se interpone en el camino se convierte en el camino.", Lang = "ES", Day = 4, Category = "discipline", WeatherHint = "rain" });
        quotes.Add(new() { Text = "La libertad comienza con la obediencia estricta a las reglas que uno mismo se impone.", Lang = "ES", Day = 5, Category = "discipline", WeatherHint = "any" });
        quotes.Add(new() { Text = "Lo que no te destruye afina tu porte. Forja tu alma en el silencio, la templanza y el sudor.", Lang = "ES", Day = 6, Category = "discipline", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "La disciplina no es castigo; es la coraza de quien se niega a ser esclavo de sus impulsos.", Lang = "ES", Day = 7, Category = "discipline", WeatherHint = "any" });

        // 2. Action
        quotes.Add(new() { Text = "No pierdas más tiempo debatiendo lo que debería ser un buen hombre. Sé uno.", Lang = "ES", Day = 1, Category = "action", WeatherHint = "any" });
        quotes.Add(new() { Text = "El primer paso no garantiza la victoria, pero rompe la parálisis de la duda.", Lang = "ES", Day = 2, Category = "action", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Realiza cada acto como si fuera el último de tu existencia: sin subterfugios ni indolencia.", Lang = "ES", Day = 3, Category = "action", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Las palabras no levantan fortalezas. Solo el golpe constante de tu esfuerzo deja huella.", Lang = "ES", Day = 4, Category = "action", WeatherHint = "rain" });
        quotes.Add(new() { Text = "La duda se alimenta de la espera; muere en el instante exacto en que comienza la acción.", Lang = "ES", Day = 5, Category = "action", WeatherHint = "any" });
        quotes.Add(new() { Text = "Haz cada labor en tu vida con la presencia y rigor como si fuera la última.", Lang = "ES", Day = 6, Category = "action", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Mantente erguido por ti mismo, no enderezado por otros.", Lang = "ES", Day = 7, Category = "action", WeatherHint = "any" });

        // 3. Resilience
        quotes.Add(new() { Text = "La dificultad revela al hombre. Cuando llega la adversidad, recuerda que la vida te ha puesto un rival duro para fortalecerte.", Lang = "ES", Day = 1, Category = "resilience", WeatherHint = "rain" });
        quotes.Add(new() { Text = "El fuego transforma todo lo que cae en él en llamas y resplandor. Sé semejante a ese fuego soberano.", Lang = "ES", Day = 2, Category = "resilience", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Nunca te quejes del peso. Ensancha tus hombros y afirma tus pasos.", Lang = "ES", Day = 3, Category = "resilience", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "La cicatriz es testimonio de que el dolor fue resistido y vencido, no de que triunfó.", Lang = "ES", Day = 4, Category = "resilience", WeatherHint = "rain" });
        quotes.Add(new() { Text = "El árbol azotado por los vientos más feroces es el que hunde sus raíces más hondo en la roca.", Lang = "ES", Day = 5, Category = "resilience", WeatherHint = "snow" });
        quotes.Add(new() { Text = "Cada caída no es más que un point de apoyo adicional para el salto venidero.", Lang = "ES", Day = 6, Category = "resilience", WeatherHint = "any" });
        quotes.Add(new() { Text = "Has sobrevivido a todo lo que el mundo arrojó contra ti hasta hoy. Este día no será distinto.", Lang = "ES", Day = 7, Category = "resilience", WeatherHint = "any" });

        // 4. Stoicism
        quotes.Add(new() { Text = "El hierro solo se afila contra el hierro; el espíritu solo se endurece en la prueba.", Lang = "ES", Day = 1, Category = "stoicism", WeatherHint = "any" });
        quotes.Add(new() { Text = "Vivir es combatir. No hay reposo para el acero hasta que la hoja quede templada.", Lang = "ES", Day = 2, Category = "stoicism", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Acepta lo que el destino teje en tu camino, y ama con rectitud a quienes la suerte te ha unido.", Lang = "ES", Day = 3, Category = "stoicism", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "No pidas que las cosas ocurran como deseas; desea más bien que ocurran como suceden.", Lang = "ES", Day = 4, Category = "stoicism", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Las cosas no nos hieren; solo nuestro juicio sobre ellas produce tribulación.", Lang = "ES", Day = 5, Category = "stoicism", WeatherHint = "any" });
        quotes.Add(new() { Text = "El sabio no depende de nada fuera de su propia entereza. Su ciudadela interior permanece inviolable.", Lang = "ES", Day = 6, Category = "stoicism", WeatherHint = "any" });
        quotes.Add(new() { Text = "Recuerda siempre: la duración de la vida humana es un punto; su sustancia, un río que fluye.", Lang = "ES", Day = 7, Category = "stoicism", WeatherHint = "cloudy" });

        // 5. Ambition
        quotes.Add(new() { Text = "No es porque las cosas sean difíciles que no nos atrevemos; es porque no nos atrevemos que son difíciles.", Lang = "ES", Day = 1, Category = "ambition", WeatherHint = "any" });
        quotes.Add(new() { Text = "¿Cuánto tiempo más vas a esperar antes de exigir de ti mismo el estándar más riguroso?", Lang = "ES", Day = 2, Category = "ambition", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "La verdadera grandeza no estriba en superar a los demás, sino en superar al hombre que fuiste ayer.", Lang = "ES", Day = 3, Category = "ambition", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Apunta a la cumbre inalcanzable: aunque caigas, te hallarás muy por encima de la multitud inmóvil.", Lang = "ES", Day = 4, Category = "ambition", WeatherHint = "any" });
        quotes.Add(new() { Text = "La grandeza no es un don; es una decisión renovada cada mañana contra la comodidad.", Lang = "ES", Day = 5, Category = "ambition", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Los espíritus mediocres buscan el descanso. Los espíritus ardientes buscan el reto que los revela.", Lang = "ES", Day = 6, Category = "ambition", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Nunca midas tu ambición por la magnitud de tus temores, sino por el calibre de tu perseverancia.", Lang = "ES", Day = 7, Category = "ambition", WeatherHint = "any" });

        // 6. Lucidity
        quotes.Add(new() { Text = "Sufrimos más a menudo en la imaginación que en la realidad.", Lang = "ES", Day = 1, Category = "lucidity", WeatherHint = "any" });
        quotes.Add(new() { Text = "Ningún viento es favorable para quien no sabe hacia qué puerto navega.", Lang = "ES", Day = 2, Category = "lucidity", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Despoja cada suceso de sus ropajes engañosos; contémplalo en su verdad desnuda.", Lang = "ES", Day = 3, Category = "lucidity", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Mira las cosas tal como son, no como tus angustias las han dibujado.", Lang = "ES", Day = 4, Category = "lucidity", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "La claridad de pensamiento es la hoja más afilada para segar la ilusión.", Lang = "ES", Day = 5, Category = "lucidity", WeatherHint = "any" });
        quotes.Add(new() { Text = "Quienes se ciegan a sí mismos para eludir la verdad cavan su propio abismo en la ignorancia.", Lang = "ES", Day = 6, Category = "lucidity", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Reconoce lo que depende de ti y desprecia con soberanía lo que escapa a tu dominio.", Lang = "ES", Day = 7, Category = "lucidity", WeatherHint = "any" });

        // 7. Calm
        quotes.Add(new() { Text = "Solo hay un camino hacia la paz interior: dejar de preocuparse por aquello que excede el poder de nuestra voluntad.", Lang = "ES", Day = 1, Category = "calm", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Sé como el promontorio contra el que chocan sin cesar las olas; permanece firme y aquieta la furia del agua.", Lang = "ES", Day = 2, Category = "calm", WeatherHint = "rain" });
        quotes.Add(new() { Text = "El tumulto exterior no puede cruzar el umbral de un alma atrincherada en su serenidad.", Lang = "ES", Day = 3, Category = "calm", WeatherHint = "any" });
        quotes.Add(new() { Text = "En el silencio interior reside una autoridad que el clamor de las masas jamás perturbará.", Lang = "ES", Day = 4, Category = "calm", WeatherHint = "snow" });
        quotes.Add(new() { Text = "Nunca te apresures en la ira. Deja que el fuego se apague antes de proferir palabra o acto.", Lang = "ES", Day = 5, Category = "calm", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "La serenidad no es la ausencia de tempestad, sino la certeza de ser la roca inmóvil en medio de ella.", Lang = "ES", Day = 6, Category = "calm", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Respira con hondura. El mundo se agita, pero tú permaneces inalterable.", Lang = "ES", Day = 7, Category = "calm", WeatherHint = "any" });

        // 8. Detachment
        quotes.Add(new() { Text = "No pidas que los acontecimientos sucedan como tú deseas; acógelos con entereza tal como ocurren.", Lang = "ES", Day = 1, Category = "detachment", WeatherHint = "any" });
        quotes.Add(new() { Text = "Mantén tu atención fija únicamente en lo que está bajo tu gobierno. Lo demás no es más que humo y dispersión.", Lang = "ES", Day = 2, Category = "detachment", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Nada de lo que la fortuna concede es verdaderamente tuyo. Lo que retira no era más que un préstamo fugaz.", Lang = "ES", Day = 3, Category = "detachment", WeatherHint = "rain" });
        quotes.Add(new() { Text = "Posee tus bienes, pero no dejes que ninguno de tus bienes te posea a ti.", Lang = "ES", Day = 4, Category = "detachment", WeatherHint = "any" });
        quotes.Add(new() { Text = "El elogio ajeno no añade nada a tu valor; su desdén no te resta un ápice.", Lang = "ES", Day = 5, Category = "detachment", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Suelta el pasado que ya no es y el futuro que aún no existe. El instante presente es tu único reino.", Lang = "ES", Day = 6, Category = "detachment", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Cuando dejas de temer perderlo todo, eres por fin libre.", Lang = "ES", Day = 7, Category = "detachment", WeatherHint = "any" });

        // 9. Philosophy
        quotes.Add(new() { Text = "La suerte es lo que ocurre cuando la preparación metódica se encuentra con la oportunidad.", Lang = "ES", Day = 1, Category = "philosophic", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Has completado el ciclo de siete días. Has dominado el tumulto interior. Mantente firme, avanza sin titubear.", Lang = "ES", Day = 2, Category = "philosophic", WeatherHint = "any" });
        quotes.Add(new() { Text = "La filosofía no es palabrería vana; es el ejercicio diario de vivir con rectitud inquebrantable.", Lang = "ES", Day = 3, Category = "philosophic", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "Examina tus pensamientos antes de que se tornen acciones; examina tus acciones antes de que forjen tu destino.", Lang = "ES", Day = 4, Category = "philosophic", WeatherHint = "rain" });
        quotes.Add(new() { Text = "La brevedad de la vida no es maldición para quien habita cada segundo con plena presencia.", Lang = "ES", Day = 5, Category = "philosophic", WeatherHint = "any" });
        quotes.Add(new() { Text = "Conócete a ti mismo, y ya no tendrás nada que temer de hombres ni de dioses.", Lang = "ES", Day = 6, Category = "philosophic", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "La sabiduría estriba en ordenar el alma como una ciudadela bien gobernada.", Lang = "ES", Day = 7, Category = "philosophic", WeatherHint = "any" });

        // 10. Poetic
        quotes.Add(new() { Text = "La disciplina de hoy es la única soberanía y libertad del mañana.", Lang = "ES", Day = 1, Category = "poetic", WeatherHint = "any" });
        quotes.Add(new() { Text = "En el eco silencioso de la noche, solo la claridad de una conciencia pura proyecta su fulgor.", Lang = "ES", Day = 2, Category = "poetic", WeatherHint = "cloudy" });
        quotes.Add(new() { Text = "El acero forjado en la penumbra no teme el resplandor del mediodía.", Lang = "ES", Day = 3, Category = "poetic", WeatherHint = "sunny" });
        quotes.Add(new() { Text = "Una chispa basta para incendiar la noche; una sola resolución basta para levantar un destino.", Lang = "ES", Day = 4, Category = "poetic", WeatherHint = "rain" });
        quotes.Add(new() { Text = "El vendaval arrastra las hojas secas, pero respeta las ramas inflexibles.", Lang = "ES", Day = 5, Category = "poetic", WeatherHint = "snow" });
        quotes.Add(new() { Text = "Camina con paso firme sobre la tierra ardiente; el suelo cede ante la gravedad de los hombres rectos.", Lang = "ES", Day = 6, Category = "poetic", WeatherHint = "any" });
        quotes.Add(new() { Text = "Al término del esfuerzo no aguarda aplauso efímero, sino el noble orgullo de haber permanecido sin doblegarse.", Lang = "ES", Day = 7, Category = "poetic", WeatherHint = "any" });

        return quotes;
    }
}
