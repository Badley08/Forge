package com.karlitodev.forge.ui.main

import android.app.Activity
import androidx.compose.animation.AnimatedContent
import androidx.compose.animation.fadeIn
import androidx.compose.animation.fadeOut
import androidx.compose.animation.togetherWith
import androidx.compose.foundation.background
import androidx.compose.foundation.border
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.layout.width
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.foundation.verticalScroll
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.ArrowForward
import androidx.compose.material.icons.filled.Lock
import androidx.compose.material.icons.filled.Share
import androidx.compose.material3.Button
import androidx.compose.material3.ButtonDefaults
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedButton
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import com.karlitodev.forge.data.local.QuoteEntity
import com.karlitodev.forge.ui.theme.ForgeBackground
import com.karlitodev.forge.ui.theme.ForgeBorder
import com.karlitodev.forge.ui.theme.ForgeCardBackground
import com.karlitodev.forge.ui.theme.ForgeIron
import com.karlitodev.forge.ui.theme.ForgeText
import com.karlitodev.forge.ui.theme.ForgeTextMuted

@Composable
fun MainScreen(
    viewModel: MainViewModel,
    modifier: Modifier = Modifier
) {
    val currentQuote by viewModel.currentQuote.collectAsState()
    val isAllRead by viewModel.isAllRead.collectAsState()
    val isLoading by viewModel.isLoading.collectAsState()
    val isPremium by viewModel.isPremium.collectAsState()
    val userName by viewModel.userName.collectAsState()

    val context = LocalContext.current
    val activity = context as? Activity

    if (isAllRead) {
        AllQuotesReadScreen(modifier = modifier)
        return
    }

    Box(
        modifier = modifier
            .fillMaxSize()
            .background(ForgeBackground)
            .padding(horizontal = 24.dp, vertical = 20.dp)
    ) {
        Column(
            modifier = Modifier
                .fillMaxSize()
                .verticalScroll(rememberScrollState()),
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.SpaceBetween
        ) {
            // En-tête supérieur
            Column(
                horizontalAlignment = Alignment.CenterHorizontally,
                modifier = Modifier.fillMaxWidth()
            ) {
                Text(
                    text = "FORGE",
                    style = MaterialTheme.typography.labelLarge,
                    color = ForgeIron,
                    fontWeight = FontWeight.Bold,
                    letterSpacing = 4.sp
                )
                if (userName.isNotBlank()) {
                    Spacer(modifier = Modifier.height(4.dp))
                    Text(
                        text = "Discipline de $userName",
                        style = MaterialTheme.typography.bodyMedium,
                        color = ForgeTextMuted
                    )
                }
            }

            Spacer(modifier = Modifier.height(24.dp))

            // Citation avec transition animée
            AnimatedContent(
                targetState = currentQuote,
                transitionSpec = { fadeIn() togetherWith fadeOut() },
                label = "QuoteAnimation",
                modifier = Modifier.fillMaxWidth()
            ) { quote ->
                if (quote != null) {
                    QuoteCard(quote = quote)
                } else if (isLoading) {
                    Box(
                        modifier = Modifier
                            .fillMaxWidth()
                            .height(260.dp),
                        contentAlignment = Alignment.Center
                    ) {
                        Text(
                            text = "Préparation de l'enclume...",
                            style = MaterialTheme.typography.bodyMedium,
                            color = ForgeTextMuted
                        )
                    }
                }
            }

            Spacer(modifier = Modifier.height(28.dp))

            // Boutons d'action : Suivant & Partager
            Column(
                modifier = Modifier.fillMaxWidth(),
                verticalArrangement = Arrangement.spacedBy(12.dp)
            ) {
                // Bouton Suivant (consomme la citation, ne revient jamais)
                Button(
                    onClick = viewModel::onNextQuoteClicked,
                    colors = ButtonDefaults.buttonColors(
                        containerColor = ForgeIron,
                        contentColor = ForgeBackground
                    ),
                    shape = RoundedCornerShape(8.dp),
                    modifier = Modifier
                        .fillMaxWidth()
                        .height(52.dp)
                ) {
                    Text(
                        text = "SUIVANT",
                        style = MaterialTheme.typography.labelLarge,
                        fontWeight = FontWeight.Bold
                    )
                    Spacer(modifier = Modifier.width(8.dp))
                    Icon(
                        imageVector = Icons.Default.ArrowForward,
                        contentDescription = "Suivant",
                        modifier = Modifier.size(18.dp)
                    )
                }

                // Bouton Partager (Seulement si Premium. Si free: grisé avec cadenas)
                if (isPremium) {
                    OutlinedButton(
                        onClick = { viewModel.onShareClicked(context, activity) },
                        border = androidx.compose.foundation.BorderStroke(1.dp, ForgeIron),
                        shape = RoundedCornerShape(8.dp),
                        modifier = Modifier
                            .fillMaxWidth()
                            .height(48.dp)
                    ) {
                        Icon(
                            imageVector = Icons.Default.Share,
                            contentDescription = "Partager",
                            tint = ForgeIron,
                            modifier = Modifier.size(18.dp)
                        )
                        Spacer(modifier = Modifier.width(8.dp))
                        Text(
                            text = "PARTAGER L'IMAGE HD (PREMIUM)",
                            style = MaterialTheme.typography.labelLarge,
                            color = ForgeIron,
                            fontWeight = FontWeight.SemiBold
                        )
                    }
                } else {
                    OutlinedButton(
                        onClick = {
                            if (activity != null) {
                                viewModel.launchPremiumUpgrade(activity)
                            }
                        },
                        border = androidx.compose.foundation.BorderStroke(1.dp, ForgeBorder),
                        shape = RoundedCornerShape(8.dp),
                        modifier = Modifier
                            .fillMaxWidth()
                            .height(48.dp)
                    ) {
                        Icon(
                            imageVector = Icons.Default.Lock,
                            contentDescription = "Verrouillé",
                            tint = ForgeTextMuted,
                            modifier = Modifier.size(16.dp)
                        )
                        Spacer(modifier = Modifier.width(8.dp))
                        Text(
                            text = "PARTAGER L'IMAGE (PREMIUM)",
                            style = MaterialTheme.typography.labelLarge,
                            color = ForgeTextMuted,
                            fontWeight = FontWeight.Normal
                        )
                    }
                }
            }

            Spacer(modifier = Modifier.height(28.dp))

            // Card Widget floutée avec cadenas : "Forge Premium - Widgets + Partage"
            PremiumWidgetCard(
                isPremium = isPremium,
                onUnlockClick = {
                    if (activity != null) {
                        viewModel.launchPremiumUpgrade(activity)
                    }
                }
            )
        }
    }
}

@Composable
private fun QuoteCard(
    quote: QuoteEntity,
    modifier: Modifier = Modifier
) {
    Column(
        modifier = modifier
            .fillMaxWidth()
            .border(1.dp, ForgeBorder, RoundedCornerShape(16.dp))
            .background(ForgeCardBackground)
            .padding(28.dp),
        horizontalAlignment = Alignment.CenterHorizontally
    ) {
        Text(
            text = "JOUR ${quote.day}",
            style = MaterialTheme.typography.labelLarge,
            color = ForgeIron,
            letterSpacing = 2.sp,
            fontWeight = FontWeight.Bold
        )

        Spacer(modifier = Modifier.height(24.dp))

        Text(
            text = "« ${quote.text} »",
            style = MaterialTheme.typography.headlineMedium,
            color = ForgeText,
            textAlign = TextAlign.Center,
            lineHeight = 34.sp
        )

        Spacer(modifier = Modifier.height(24.dp))

        Text(
            text = "FORGE · DISCIPLINE",
            style = MaterialTheme.typography.bodyMedium,
            color = ForgeTextMuted,
            letterSpacing = 1.sp
        )
    }
}
