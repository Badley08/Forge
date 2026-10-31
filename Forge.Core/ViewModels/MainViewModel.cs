using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Forge.Core.Models;
using Forge.Core.Repositories;

namespace Forge.Core.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IQuoteRepository _quoteRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBillingService _billingService;
    private readonly IWeatherService _weatherService;

    private readonly List<QuoteModel> _sessionHistory = new();
    private int _historyIndex = -1;

    [ObservableProperty]
    private QuoteModel? _currentQuote;

    [ObservableProperty]
    private bool _isStockExhausted;

    [ObservableProperty]
    private bool _isLoading = true;

    [ObservableProperty]
    private bool _isPremium;

    [ObservableProperty]
    private string _currentCategory = "discipline";

    [ObservableProperty]
    private string _currentCoverAsset = "avares://Forge.UI/Assets/Covers/discipline.jpeg";

    [ObservableProperty]
    private string _userName = string.Empty;

    public ObservableCollection<QuoteCategory> Categories { get; } = new();

    public MainViewModel(
        IQuoteRepository quoteRepository,
        IUserRepository userRepository,
        IBillingService billingService,
        IWeatherService weatherService)
    {
        _quoteRepository = quoteRepository;
        _userRepository = userRepository;
        _billingService = billingService;
        _weatherService = weatherService;

        UserName = _userRepository.UserName;
        IsPremium = _userRepository.IsPremium;

        LoadCategories();
        _ = LoadInitialQuoteAsync();
    }

    private void LoadCategories()
    {
        Categories.Clear();
        foreach (var cat in QuoteCategory.GetAllCategories())
        {
            Categories.Add(cat);
        }
    }

    public async Task LoadInitialQuoteAsync()
    {
        IsLoading = true;
        IsStockExhausted = false;

        var weather = await _weatherService.GetCurrentWeatherHintAsync();
        var quote = await _quoteRepository.GetNextUnseenQuoteAsync(_userRepository.UserLanguage, CurrentCategory, weather);

        if (quote != null)
        {
            CurrentQuote = quote;
            _sessionHistory.Clear();
            _sessionHistory.Add(quote);
            _historyIndex = 0;
            IsStockExhausted = false;
        }
        else
        {
            CurrentQuote = null;
            IsStockExhausted = true;
        }

        IsLoading = false;
    }

    [RelayCommand]
    public async Task SwipeNextAsync()
    {
        // Si l'utilisateur consulte l'historique de session et va vers l'avant
        if (_historyIndex >= 0 && _historyIndex < _sessionHistory.Count - 1)
        {
            _historyIndex++;
            CurrentQuote = _sessionHistory[_historyIndex];
            return;
        }

        // Sinon, marquer la citation actuelle comme consommée (ne revient jamais)
        if (CurrentQuote != null)
        {
            await _quoteRepository.MarkQuoteSeenAsync(CurrentQuote.Id);
        }

        var weather = await _weatherService.GetCurrentWeatherHintAsync();
        var next = await _quoteRepository.GetNextUnseenQuoteAsync(_userRepository.UserLanguage, CurrentCategory, weather);

        if (next != null)
        {
            CurrentQuote = next;
            _sessionHistory.Add(next);
            _historyIndex = _sessionHistory.Count - 1;
            IsStockExhausted = false;
        }
        else
        {
            CurrentQuote = null;
            IsStockExhausted = true;
        }
    }

    [RelayCommand]
    public void SwipePrevious()
    {
        // Permis uniquement parmi les citations déjà vues de la session en cours
        if (_historyIndex > 0)
        {
            _historyIndex--;
            CurrentQuote = _sessionHistory[_historyIndex];
        }
    }

    [RelayCommand]
    public async Task SelectCategoryAsync(QuoteCategory category)
    {
        if (!IsPremium && !category.IsFreeCategory)
        {
            // Catégorie verrouillée pour les utilisateurs Free
            await TriggerPurchaseAsync();
            return;
        }

        CurrentCategory = category.Id;
        CurrentCoverAsset = category.CoverAsset;
        await LoadInitialQuoteAsync();
    }

    [RelayCommand]
    public async Task TriggerPurchaseAsync()
    {
        if (IsPremium) return;

        var success = await _billingService.PurchasePremiumAsync();
        if (success)
        {
            _userRepository.SetPremiumStatus(true);
            IsPremium = true;
        }
    }

    [RelayCommand]
    public async Task ShareQuoteAsync()
    {
        if (!IsPremium)
        {
            await TriggerPurchaseAsync();
            return;
        }

        // Le partage génère un bitmap avec filigrane invisible sans texte brut (délégué à la plateforme)
    }
}
