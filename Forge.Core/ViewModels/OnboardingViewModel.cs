using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Forge.Core.Repositories;

namespace Forge.Core.ViewModels;

public enum OnboardingStep
{
    Name,
    Language,
    Welcome
}

public partial class OnboardingViewModel : ViewModelBase
{
    private readonly IUserRepository _userRepository;
    private readonly Action _onCompleted;

    [ObservableProperty]
    private OnboardingStep _currentStep = OnboardingStep.Name;

    [ObservableProperty]
    private string _userName = string.Empty;

    [ObservableProperty]
    private string _selectedLanguage = "FR";

    public OnboardingViewModel(IUserRepository userRepository, Action onCompleted)
    {
        _userRepository = userRepository;
        _onCompleted = onCompleted;
    }

    [RelayCommand]
    private void NextStep()
    {
        if (CurrentStep == OnboardingStep.Name && !string.IsNullOrWhiteSpace(UserName))
        {
            CurrentStep = OnboardingStep.Language;
        }
        else if (CurrentStep == OnboardingStep.Language)
        {
            CurrentStep = OnboardingStep.Welcome;
        }
    }

    [RelayCommand]
    private void SelectLanguage(string lang)
    {
        SelectedLanguage = lang;
    }

    [RelayCommand]
    private async Task FinishOnboardingAsync()
    {
        var finalName = string.IsNullOrWhiteSpace(UserName) ? "Disciple" : UserName.Trim();
        await _userRepository.SaveUserAsync(finalName, SelectedLanguage);
        await _userRepository.CompleteOnboardingAsync();
        _onCompleted?.Invoke();
    }
}
