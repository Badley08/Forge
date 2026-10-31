package com.karlitodev.forge.ui.navigation

sealed interface Screen {
    object Onboarding : Screen
    object Main : Screen
}
