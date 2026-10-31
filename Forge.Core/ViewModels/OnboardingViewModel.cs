using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Forge.Core.Repositories;

namespace Forge.Core.ViewModels;

public enum OnboardingStep
{
    Language,
    Name,
    SwipeTutorial,
    Welcome
}

/// <summary>
/// Manages the onboarding workflow: language selection, user identity,
/// VIP key detection, swipe gesture instructions, and welcome confirmation.
/// </summary>
public partial class OnboardingViewModel : ViewModelBase
{
    private readonly IUserRepository _userRepository;
    private readonly Action _onCompleted;

    [ObservableProperty]
    private OnboardingStep _currentStep = OnboardingStep.Language;

    [ObservableProperty]
    private string _userName = string.Empty;

    [ObservableProperty]
    private string _selectedLanguage = "FR";

    [ObservableProperty]
    private bool _isKarlitoVip;

    [ObservableProperty]
    private string _vipStatusMessage = string.Empty;

    public OnboardingViewModel(IUserRepository userRepository, Action onCompleted)
    {
        _userRepository = userRepository;
        _onCompleted = onCompleted;

        // Auto-detect system language with fallback to English
        DetectSystemLanguage();
    }

    private void DetectSystemLanguage()
    {
        try
        {
            var systemLang = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToUpperInvariant();
            SelectedLanguage = systemLang switch
            {
                "FR" => "FR",
                "ES" => "ES",
                _ => "EN"
            };
        }
        catch
        {
            SelectedLanguage = "EN";
        }
    }

    partial void OnUserNameChanged(string value)
    {
        CheckKarlitoCheatcode(value);
    }

    private void CheckKarlitoCheatcode(string name)
    {
        if (string.Equals(name?.Trim(), "Karlito", StringComparison.OrdinalIgnoreCase))
        {
            IsKarlitoVip = true;
            VipStatusMessage = SelectedLanguage switch
            {
                "FR" => "CLÉ VIP KARLITO ACTIVÉE · Accès Premium à vie accordé",
                "ES" => "CLAVE VIP KARLITO ACTIVADA · Acceso Premium de por vida otorgado",
                _ => "VIP KEY ACTIVATED · Lifetime Premium Granted"
            };
        }
        else
        {
            IsKarlitoVip = false;
            VipStatusMessage = string.Empty;
        }
    }

    [RelayCommand]
    private void SelectLanguage(string lang)
    {
        SelectedLanguage = lang;
        // Re-check VIP message localization if username was already entered
        if (IsKarlitoVip)
        {
            CheckKarlitoCheatcode(UserName);
        }
    }

    [RelayCommand]
    private void NextStep()
    {
        switch (CurrentStep)
        {
            case OnboardingStep.Language:
                CurrentStep = OnboardingStep.Name;
                break;

            case OnboardingStep.Name:
                if (!string.IsNullOrWhiteSpace(UserName))
                {
                    CurrentStep = OnboardingStep.SwipeTutorial;
                }
                break;

            case OnboardingStep.SwipeTutorial:
                CurrentStep = OnboardingStep.Welcome;
                break;
        }
    }

    [RelayCommand]
    private void PreviousStep()
    {
        switch (CurrentStep)
        {
            case OnboardingStep.Name:
                CurrentStep = OnboardingStep.Language;
                break;

            case OnboardingStep.SwipeTutorial:
                CurrentStep = OnboardingStep.Name;
                break;

            case OnboardingStep.Welcome:
                CurrentStep = OnboardingStep.SwipeTutorial;
                break;
        }
    }

    [RelayCommand]
    private async Task FinishOnboardingAsync()
    {
        var finalName = string.IsNullOrWhiteSpace(UserName) ? "Disciple" : UserName.Trim();

        await _userRepository.SaveUserAsync(finalName, SelectedLanguage);

        if (IsKarlitoVip)
        {
            _userRepository.SetPremiumStatus(true);
        }

        await _userRepository.CompleteOnboardingAsync();
        _onCompleted?.Invoke();
    }
}
