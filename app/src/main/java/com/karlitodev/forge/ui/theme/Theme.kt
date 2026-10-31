package com.karlitodev.forge.ui.theme

import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.lightColorScheme
import androidx.compose.runtime.Composable

private val LightColorScheme = lightColorScheme(
    primary = ForgeIron,
    onPrimary = ForgeBackground,
    primaryContainer = ForgeCardBackground,
    onPrimaryContainer = ForgeText,
    secondary = ForgeBronze,
    onSecondary = ForgeBackground,
    background = ForgeBackground,
    onBackground = ForgeText,
    surface = ForgeBackground,
    onSurface = ForgeText,
    surfaceVariant = ForgeCardBackground,
    onSurfaceVariant = ForgeTextMuted,
    outline = ForgeBorder
)

@Composable
fun ForgeTheme(content: @Composable () -> Unit) {
    MaterialTheme(
        colorScheme = LightColorScheme,
        typography = Typography,
        content = content
    )
}
