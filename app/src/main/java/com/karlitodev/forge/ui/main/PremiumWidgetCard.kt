package com.karlitodev.forge.ui.main

import androidx.compose.foundation.background
import androidx.compose.foundation.border
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.layout.width
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.CheckCircle
import androidx.compose.material.icons.filled.Lock
import androidx.compose.material3.Button
import androidx.compose.material3.ButtonDefaults
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.blur
import androidx.compose.ui.draw.clip
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import com.karlitodev.forge.ui.theme.ForgeBackground
import com.karlitodev.forge.ui.theme.ForgeBorder
import com.karlitodev.forge.ui.theme.ForgeCardBackground
import com.karlitodev.forge.ui.theme.ForgeGold
import com.karlitodev.forge.ui.theme.ForgeIron
import com.karlitodev.forge.ui.theme.ForgeLockOverlay
import com.karlitodev.forge.ui.theme.ForgeText
import com.karlitodev.forge.ui.theme.ForgeTextMuted

@Composable
fun PremiumWidgetCard(
    isPremium: Boolean,
    onUnlockClick: () -> Unit,
    modifier: Modifier = Modifier
) {
    Box(
        modifier = modifier
            .fillMaxWidth()
            .clip(RoundedCornerShape(16.dp))
            .border(1.dp, ForgeBorder, RoundedCornerShape(16.dp))
            .background(ForgeCardBackground)
    ) {
        if (!isPremium) {
            // Arrière-plan simulé flouté pour le Widget
            Column(
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(20.dp)
                    .blur(radius = 8.dp)
            ) {
                Text(
                    text = "Aperçu du Widget Écran d'accueil",
                    style = MaterialTheme.typography.titleMedium,
                    color = ForgeText
                )
                Spacer(modifier = Modifier.height(8.dp))
                Text(
                    text = "« La discipline est le pont entre les buts et l'accomplissement. »",
                    style = MaterialTheme.typography.bodyMedium,
                    color = ForgeTextMuted
                )
                Spacer(modifier = Modifier.height(12.dp))
                Row(
                    modifier = Modifier.fillMaxWidth(),
                    horizontalArrangement = Arrangement.SpaceBetween
                ) {
                    Text(text = "Forge Widget 4x2", fontSize = 12.sp, color = ForgeTextMuted)
                    Text(text = "Partage HD activé", fontSize = 12.sp, color = ForgeTextMuted)
                }
            }

            // Calque supérieur avec Cadenas + Appel à l'action
            Box(
                modifier = Modifier
                    .matchParentSize()
                    .background(ForgeLockOverlay)
                    .padding(16.dp),
                contentAlignment = Alignment.Center
            ) {
                Column(
                    horizontalAlignment = Alignment.CenterHorizontally,
                    verticalArrangement = Arrangement.Center
                ) {
                    Row(
                        verticalAlignment = Alignment.CenterVertically,
                        horizontalArrangement = Arrangement.Center
                    ) {
                        Icon(
                            imageVector = Icons.Default.Lock,
                            contentDescription = "Verrouillé",
                            tint = ForgeIron,
                            modifier = Modifier.size(18.dp)
                        )
                        Spacer(modifier = Modifier.width(8.dp))
                        Text(
                            text = "Forge Premium — Widgets + Partage",
                            style = MaterialTheme.typography.titleMedium,
                            fontWeight = FontWeight.Bold,
                            color = ForgeIron,
                            textAlign = TextAlign.Center
                        )
                    }

                    Spacer(modifier = Modifier.height(10.dp))

                    Button(
                        onClick = onUnlockClick,
                        colors = ButtonDefaults.buttonColors(
                            containerColor = ForgeIron,
                            contentColor = ForgeBackground
                        ),
                        shape = RoundedCornerShape(8.dp),
                        modifier = Modifier.height(40.dp)
                    ) {
                        Text(
                            text = "Débloquer à vie · 2,99 €",
                            style = MaterialTheme.typography.labelLarge,
                            fontWeight = FontWeight.Bold
                        )
                    }
                }
            }
        } else {
            // État Premium débloqué
            Column(
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(18.dp)
            ) {
                Row(
                    verticalAlignment = Alignment.CenterVertically
                ) {
                    Icon(
                        imageVector = Icons.Default.CheckCircle,
                        contentDescription = "Premium Activé",
                        tint = ForgeGold,
                        modifier = Modifier.size(20.dp)
                    )
                    Spacer(modifier = Modifier.width(8.dp))
                    Text(
                        text = "Forge Premium Actif",
                        style = MaterialTheme.typography.titleMedium,
                        fontWeight = FontWeight.Bold,
                        color = ForgeIron
                    )
                }
                Spacer(modifier = Modifier.height(6.dp))
                Text(
                    text = "Widgets débloqués. Partage haute fidélité avec sceau invisible actif.",
                    style = MaterialTheme.typography.bodyMedium,
                    color = ForgeTextMuted
                )
            }
        }
    }
}
