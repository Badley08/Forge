package com.karlitodev.forge

import android.os.Bundle
import android.view.WindowManager
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.activity.enableEdgeToEdge
import androidx.compose.animation.Crossfade
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.safeDrawingPadding
import androidx.compose.material3.Surface
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.ui.Modifier
import androidx.lifecycle.viewmodel.compose.viewModel
import com.karlitodev.forge.ui.main.MainScreen
import com.karlitodev.forge.ui.main.MainViewModel
import com.karlitodev.forge.ui.navigation.Screen
import com.karlitodev.forge.ui.onboarding.OnboardingScreen
import com.karlitodev.forge.ui.onboarding.OnboardingViewModel
import com.karlitodev.forge.ui.theme.ForgeBackground
import com.karlitodev.forge.ui.theme.ForgeTheme

class MainActivity : ComponentActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        // FLAG_SECURE activé sur toute l'app pour bloquer les captures d'écran (Free & Premium)
        window.setFlags(
            WindowManager.LayoutParams.FLAG_SECURE,
            WindowManager.LayoutParams.FLAG_SECURE
        )

        enableEdgeToEdge()

        val app = application as ForgeApplication
        val repository = app.repository

        setContent {
            ForgeTheme {
                Surface(
                    modifier = Modifier
                        .fillMaxSize()
                        .safeDrawingPadding(),
                    color = ForgeBackground
                ) {
                    val isOnboardingCompleted by repository.isOnboardingCompletedFlow
                        .collectAsState(initial = false)

                    Crossfade(
                        targetState = if (isOnboardingCompleted) Screen.Main else Screen.Onboarding,
                        label = "AppNavigationCrossfade"
                    ) { screen ->
                        when (screen) {
                            Screen.Onboarding -> {
                                val onboardingViewModel = viewModel<OnboardingViewModel> {
                                    OnboardingViewModel(repository)
                                }
                                OnboardingScreen(
                                    viewModel = onboardingViewModel,
                                    onOnboardingFinished = {
                                        // Automatically navigated via DataStore reactive flow
                                    }
                                )
                            }
                            Screen.Main -> {
                                val mainViewModel = viewModel<MainViewModel> {
                                    MainViewModel(repository)
                                }
                                MainScreen(viewModel = mainViewModel)
                            }
                        }
                    }
                }
            }
        }
    }
}
