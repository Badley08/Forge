package com.karlitodev.forge.ui.onboarding

import androidx.compose.animation.AnimatedContent
import androidx.compose.animation.fadeIn
import androidx.compose.animation.fadeOut
import androidx.compose.animation.togetherWith
import androidx.compose.foundation.Image
import androidx.compose.foundation.background
import androidx.compose.foundation.border
import androidx.compose.foundation.clickable
import androidx.compose.ui.res.painterResource
import com.karlitodev.forge.R
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material3.Button
import androidx.compose.material3.ButtonDefaults
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.OutlinedTextFieldDefaults
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
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
fun OnboardingScreen(
    viewModel: OnboardingViewModel,
    onOnboardingFinished: () -> Unit
) {
    val step by viewModel.currentStep.collectAsState()
    val name by viewModel.name.collectAsState()
    val lang by viewModel.language.collectAsState()

    Box(
        modifier = Modifier
            .fillMaxSize()
            .background(ForgeBackground)
            .padding(24.dp)
    ) {
        AnimatedContent(
            targetState = step,
            transitionSpec = { fadeIn() togetherWith fadeOut() },
            label = "OnboardingTransition",
            modifier = Modifier.fillMaxSize()
        ) { currentStep ->
            when (currentStep) {
                OnboardingStep.STEP_NAME -> {
                    StepName(
                        name = name,
                        onNameChange = viewModel::updateName,
                        onNext = viewModel::submitName
                    )
                }
                OnboardingStep.STEP_LANG -> {
                    StepLanguage(
                        selectedLang = lang,
                        onSelectLang = viewModel::selectLanguage,
                        onNext = viewModel::submitLanguage
                    )
                }
                OnboardingStep.STEP_WELCOME -> {
                    StepWelcome(
                        name = name,
                        onFinish = {
                            viewModel.completeOnboarding(onOnboardingFinished)
                        }
                    )
                }
            }
        }
    }
}

@Composable
private fun StepName(
    name: String,
    onNameChange: (String) -> Unit,
    onNext: () -> Unit
) {
    Column(
        modifier = Modifier.fillMaxSize(),
        verticalArrangement = Arrangement.Center,
        horizontalAlignment = Alignment.CenterHorizontally
    ) {
        Image(
            painter = painterResource(id = R.drawable.forge_light),
            contentDescription = "Logo Forge",
            modifier = Modifier
                .size(64.dp)
                .clip(RoundedCornerShape(14.dp))
        )

        Spacer(modifier = Modifier.height(16.dp))

        Text(
            text = "FORGE",
            style = MaterialTheme.typography.headlineLarge,
            color = ForgeText,
            letterSpacing = 4.sp
        )

        Spacer(modifier = Modifier.height(32.dp))

        Text(
            text = "Comment on t'appelle ?",
            style = MaterialTheme.typography.headlineMedium,
            color = ForgeText,
            textAlign = TextAlign.Center
        )

        Spacer(modifier = Modifier.height(12.dp))

        Text(
            text = "L'esprit forgé exige un nom pour sceller sa discipline.",
            style = MaterialTheme.typography.bodyMedium,
            color = ForgeTextMuted,
            textAlign = TextAlign.Center
        )

        Spacer(modifier = Modifier.height(32.dp))

        OutlinedTextField(
            value = name,
            onValueChange = onNameChange,
            singleLine = true,
            placeholder = { Text("Ton prénom ou surnom") },
            colors = OutlinedTextFieldDefaults.colors(
                focusedBorderColor = ForgeIron,
                unfocusedBorderColor = ForgeBorder,
                focusedTextColor = ForgeText,
                unfocusedTextColor = ForgeText,
                cursorColor = ForgeIron
            ),
            modifier = Modifier.fillMaxWidth()
        )

        Spacer(modifier = Modifier.height(28.dp))

        Button(
            onClick = onNext,
            enabled = name.isNotBlank(),
            colors = ButtonDefaults.buttonColors(
                containerColor = ForgeIron,
                contentColor = ForgeBackground,
                disabledContainerColor = ForgeBorder,
                disabledContentColor = ForgeTextMuted
            ),
            shape = RoundedCornerShape(8.dp),
            modifier = Modifier
                .fillMaxWidth()
                .height(52.dp)
        ) {
            Text(
                text = "CONTINUER",
                style = MaterialTheme.typography.labelLarge,
                fontWeight = FontWeight.Bold
            )
        }
    }
}

@Composable
private fun StepLanguage(
    selectedLang: String,
    onSelectLang: (String) -> Unit,
    onNext: () -> Unit
) {
    val languages = listOf(
        "FR" to "Français",
        "EN" to "English",
        "ES" to "Español"
    )

    Column(
        modifier = Modifier.fillMaxSize(),
        verticalArrangement = Arrangement.Center,
        horizontalAlignment = Alignment.CenterHorizontally
    ) {
        Text(
            text = "LANGUE",
            style = MaterialTheme.typography.headlineLarge,
            color = ForgeText,
            letterSpacing = 4.sp
        )

        Spacer(modifier = Modifier.height(16.dp))

        Text(
            text = "Dans quelle langue forger ton esprit ?",
            style = MaterialTheme.typography.headlineMedium,
            color = ForgeText,
            textAlign = TextAlign.Center
        )

        Spacer(modifier = Modifier.height(32.dp))

        languages.forEach { (code, label) ->
            val isSelected = selectedLang.equals(code, ignoreCase = true)
            Row(
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(vertical = 6.dp)
                    .clip(RoundedCornerShape(8.dp))
                    .border(
                        width = if (isSelected) 2.dp else 1.dp,
                        color = if (isSelected) ForgeIron else ForgeBorder,
                        shape = RoundedCornerShape(8.dp)
                    )
                    .background(if (isSelected) ForgeCardBackground else ForgeBackground)
                    .clickable { onSelectLang(code) }
                    .padding(18.dp),
                verticalAlignment = Alignment.CenterVertically,
                horizontalArrangement = Arrangement.SpaceBetween
            ) {
                Text(
                    text = label,
                    style = MaterialTheme.typography.titleLarge,
                    color = ForgeText,
                    fontWeight = if (isSelected) FontWeight.Bold else FontWeight.Normal
                )
                Text(
                    text = code,
                    style = MaterialTheme.typography.bodyMedium,
                    color = if (isSelected) ForgeIron else ForgeTextMuted,
                    fontWeight = FontWeight.SemiBold
                )
            }
        }

        Spacer(modifier = Modifier.height(32.dp))

        Button(
            onClick = onNext,
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
                text = "CONTINUER",
                style = MaterialTheme.typography.labelLarge,
                fontWeight = FontWeight.Bold
            )
        }
    }
}

@Composable
private fun StepWelcome(
    name: String,
    onFinish: () -> Unit
) {
    Column(
        modifier = Modifier.fillMaxSize(),
        verticalArrangement = Arrangement.Center,
        horizontalAlignment = Alignment.CenterHorizontally
    ) {
        Text(
            text = "BIENVENUE",
            style = MaterialTheme.typography.headlineLarge,
            color = ForgeText,
            letterSpacing = 4.sp
        )

        Spacer(modifier = Modifier.height(24.dp))

        Text(
            text = "La Forge t'attend, $name.",
            style = MaterialTheme.typography.headlineMedium,
            color = ForgeText,
            textAlign = TextAlign.Center
        )

        Spacer(modifier = Modifier.height(16.dp))

        Text(
            text = "Ici, aucune distraction ni bavardage superficiel. Chaque jour, une citation stoïcienne pure sera mise à l'épreuve de ta discipline. Une fois lue, elle ne reviendra jamais. Assimile-la.",
            style = MaterialTheme.typography.bodyLarge,
            color = ForgeTextMuted,
            textAlign = TextAlign.Center,
            lineHeight = 26.sp
        )

        Spacer(modifier = Modifier.height(48.dp))

        Button(
            onClick = onFinish,
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
                text = "ENTRER DANS LA FORGE",
                style = MaterialTheme.typography.labelLarge,
                fontWeight = FontWeight.Bold
            )
        }
    }
}
