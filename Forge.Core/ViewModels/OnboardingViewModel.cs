using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Forge.Core.Repositories;

namespace Forge.Core.ViewModels;

public enum OnboardingStep
{
    Language,
    LocationDisclosure,
    Name,
    SwipeTutorial,
    Welcome
}

/// <summary>
/// Manages the onboarding workflow: language selection, Google Play compliant
/// prominent location disclosure in EN/FR/ES, user identity, VIP key detection,
/// swipe gesture instructions, and welcome confirmation.
/// </summary>
public partial class OnboardingViewModel : ViewModelBase
{
    private readonly IUserRepository _userRepository;
    private readonly ILocationService? _locationService;
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

    // Localized strings for the prominent location disclosure
    [ObservableProperty]
    private string _locationTitle = string.Empty;

    [ObservableProperty]
    private string _locationPurpose = string.Empty;

    [ObservableProperty]
    private string _locationPrivacyNotice = string.Empty;

    [ObservableProperty]
    private string _locationAcceptButtonText = string.Empty;

    [ObservableProperty]
    private string _locationDeclineButtonText = string.Empty;

    public OnboardingViewModel(IUserRepository userRepository, ILocationService? locationService, Action onCompleted)
    {
        _userRepository = userRepository;
        _locationService = locationService;
        _onCompleted = onCompleted;

        // Auto-detect system language with fallback to English
        DetectSystemLanguage();
        UpdateLocalizedLocationDisclosure();
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

    partial void OnSelectedLanguageChanged(string value)
    {
        UpdateLocalizedLocationDisclosure();
    }

    private void UpdateLocalizedLocationDisclosure()
    {
        switch (SelectedLanguage)
        {
            case "FR":
                LocationTitle = "Permission de Localisation & Météo";
                LocationPurpose = "Forge utilise votre position approximative (ville/région) exclusivement pour déterminer les conditions météo locales en temps réel (pluie, neige, soleil, nuages) afin d'harmoniser les pensées stoïciennes et animer les widgets d'écran d'accueil.";
                LocationPrivacyNotice = "Confidentialité absolue : Vos coordonnées approximatives ne sont jamais stockées, jamais partagées et ne font l'objet d'aucun traçage. Si vous refusez, Forge fonctionne normalement avec la sélection par jour.";
                LocationAcceptButtonText = "ACTIVER LA MÉTÉO LOCALE";
                LocationDeclineButtonText = "Continuer sans météo";
                break;

            case "ES":
                LocationTitle = "Permiso de Ubicación & Clima";
                LocationPurpose = "Forge utiliza su ubicación aproximada exclusivamente para determinar las condiciones meteorológicas locales en tiempo real (lluvia, nieve, sol, nubes) y armonizar las citas estoicas y los widgets de pantalla de inicio.";
                LocationPrivacyNotice = "Privacidad total: Sus coordenadas aproximadas nunca se almacenan, nunca se comparten y no son objeto de ningún rastreo. Si rechaza, Forge funciona normalmente según el día de la semana.";
                LocationAcceptButtonText = "ACTIVAR CLIMA LOCAL";
                LocationDeclineButtonText = "Continuar sin clima";
                break;

            default:
                LocationTitle = "Location Permission & Weather";
                LocationPurpose = "Forge accesses your coarse approximate location solely to determine live local atmospheric conditions (rain, snow, sun, clouds) to harmonize stoic quotes and animate home-screen widgets with your daily environment.";
                LocationPrivacyNotice = "Absolute Privacy: Your coordinates are never stored, never shared, and never tracked. If declined, Forge operates seamlessly using day-of-the-week progression.";
                LocationAcceptButtonText = "ENABLE LOCAL WEATHER";
                LocationDeclineButtonText = "Continue without weather";
                break;
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
                CurrentStep = OnboardingStep.LocationDisclosure;
                break;

            case OnboardingStep.LocationDisclosure:
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
    private async Task AcceptLocationAsync()
    {
        if (_locationService != null)
        {
            await _locationService.RequestLocationPermissionAsync();
        }
        CurrentStep = OnboardingStep.Name;
    }

    [RelayCommand]
    private void DeclineLocation()
    {
        CurrentStep = OnboardingStep.Name;
    }

    [RelayCommand]
    private void PreviousStep()
    {
        switch (CurrentStep)
        {
            case OnboardingStep.LocationDisclosure:
                CurrentStep = OnboardingStep.Language;
                break;

            case OnboardingStep.Name:
                CurrentStep = OnboardingStep.LocationDisclosure;
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
