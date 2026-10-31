package com.karlitodev.forge.ui.main

import androidx.compose.foundation.background
import androidx.compose.foundation.border
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import com.karlitodev.forge.ui.theme.ForgeBackground
import com.karlitodev.forge.ui.theme.ForgeBorder
import com.karlitodev.forge.ui.theme.ForgeCardBackground
import com.karlitodev.forge.ui.theme.ForgeIron
import com.karlitodev.forge.ui.theme.ForgeText
import com.karlitodev.forge.ui.theme.ForgeTextMuted

@Composable
fun AllQuotesReadScreen(
    modifier: Modifier = Modifier
) {
    Box(
        modifier = modifier
            .fillMaxSize()
            .background(ForgeBackground)
            .padding(24.dp),
        contentAlignment = Alignment.Center
    ) {
        Column(
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.Center,
            modifier = Modifier
                .fillMaxWidth()
                .border(1.dp, ForgeBorder, RoundedCornerShape(16.dp))
                .background(ForgeCardBackground)
                .padding(32.dp)
        ) {
            Text(
                text = "FORGE CONQUISE",
                style = MaterialTheme.typography.labelLarge,
                color = ForgeIron,
                fontWeight = FontWeight.Bold,
                letterSpacing = 3.sp
            )

            Spacer(modifier = Modifier.height(24.dp))

            Text(
                text = "Tu as tout lu. On n'a plus de Forge pour aujourd'hui.",
                style = MaterialTheme.typography.headlineMedium,
                color = ForgeText,
                textAlign = TextAlign.Center,
                lineHeight = 32.sp
            )

            Spacer(modifier = Modifier.height(18.dp))

            Text(
                text = "Ton appétit pour la discipline a dévoré tout notre stock en une bouchée. L'acier a besoin de temps pour refroidir et se durcir.",
                style = MaterialTheme.typography.bodyLarge,
                color = ForgeTextMuted,
                textAlign = TextAlign.Center,
                lineHeight = 26.sp
            )

            Spacer(modifier = Modifier.height(24.dp))

            Box(
                modifier = Modifier
                    .fillMaxWidth()
                    .background(ForgeBackground, RoundedCornerShape(8.dp))
                    .padding(16.dp),
                contentAlignment = Alignment.Center
            ) {
                Text(
                    text = "Reviens demain, on forge les suivantes pour toi.",
                    style = MaterialTheme.typography.titleMedium,
                    fontWeight = FontWeight.SemiBold,
                    color = ForgeIron,
                    textAlign = TextAlign.Center
                )
            }
        }
    }
}
