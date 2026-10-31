package com.karlitodev.forge.ui.onboarding

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.karlitodev.forge.data.repository.ForgeRepository
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.launch

enum class OnboardingStep {
    STEP_NAME,
    STEP_LANG,
    STEP_WELCOME
}

class OnboardingViewModel(
    private val repository: ForgeRepository
) : ViewModel() {

    private val _currentStep = MutableStateFlow(OnboardingStep.STEP_NAME)
    val currentStep: StateFlow<OnboardingStep> = _currentStep.asStateFlow()

    private val _name = MutableStateFlow("")
    val name: StateFlow<String> = _name.asStateFlow()

    private val _language = MutableStateFlow("FR")
    val language: StateFlow<String> = _language.asStateFlow()

    fun updateName(newName: String) {
        _name.value = newName
    }

    fun submitName() {
        if (_name.value.isNotBlank()) {
            _currentStep.value = OnboardingStep.STEP_LANG
        }
    }

    fun selectLanguage(lang: String) {
        _language.value = lang
    }

    fun submitLanguage() {
        _currentStep.value = OnboardingStep.STEP_WELCOME
    }

    fun completeOnboarding(onComplete: () -> Unit) {
        viewModelScope.launch {
            repository.saveUserProfile(
                name = _name.value.ifBlank { "Disciple" },
                lang = _language.value
            )
            repository.completeOnboarding()
            onComplete()
        }
    }
}
