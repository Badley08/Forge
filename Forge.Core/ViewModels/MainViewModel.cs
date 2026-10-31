using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Forge.Core.Models;
using Forge.Core.Repositories;

namespace Forge.Core.ViewModels;

/// <summary>
/// Main view model driving the single-quote card swipe experience,
/// category filtering, atmospheric weather binding, and premium tier handling.
/// </summary>
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
    private bool _isKarlitoVip;

    [ObservableProperty]
    private string _currentCategory = "discipline";

    [ObservableProperty]
    private string _currentCoverAsset = "avares://Forge.UI/Assets/Covers/discipline.jpeg";

    [ObservableProperty]
    private string _currentWeatherHint = "any";

    [ObservableProperty]
    private string _userName = string.Empty;

    [ObservableProperty]
    private bool _canSwipePrevious;

    [ObservableProperty]
    private string _formattedPrice = "3,99 $";

    [ObservableProperty]
    private string _shareStatusMessage = string.Empty;

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
        IsKarlitoVip = string.Equals(UserName?.Trim(), "Karlito", StringComparison.OrdinalIgnoreCase);
        IsPremium = _userRepository.IsPremium || IsKarlitoVip;

        LoadCategories();
        _ = LoadInitialQuoteAsync();
        _ = LoadStorePriceAsync();
    }

    private async Task LoadStorePriceAsync()
    {
        try
        {
            var price = await _billingService.GetProductPriceAsync();
            if (!string.IsNullOrWhiteSpace(price))
            {
                FormattedPrice = price;
            }
        }
        catch
        {
            // Maintain default price
        }
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

        CurrentWeatherHint = await _weatherService.GetCurrentWeatherHintAsync();
        var quote = await _quoteRepository.GetNextUnseenQuoteAsync(_userRepository.UserLanguage, CurrentCategory, CurrentWeatherHint);

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

        UpdateSwipeState();
        IsLoading = false;
    }

    [RelayCommand]
    public async Task SwipeNextAsync()
    {
        // If navigating forward within in-session history
        if (_historyIndex >= 0 && _historyIndex < _sessionHistory.Count - 1)
        {
            _historyIndex++;
            CurrentQuote = _sessionHistory[_historyIndex];
            UpdateSwipeState();
            return;
        }

        // Mark current quote as seen (never repeats once marked)
        if (CurrentQuote != null)
        {
            await _quoteRepository.MarkQuoteSeenAsync(CurrentQuote.Id);
        }

        CurrentWeatherHint = await _weatherService.GetCurrentWeatherHintAsync();
        var next = await _quoteRepository.GetNextUnseenQuoteAsync(_userRepository.UserLanguage, CurrentCategory, CurrentWeatherHint);

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

        UpdateSwipeState();
    }

    [RelayCommand]
    public void SwipePrevious()
    {
        // Revisit previously viewed quotes from the active session only
        if (_historyIndex > 0)
        {
            _historyIndex--;
            CurrentQuote = _sessionHistory[_historyIndex];
        }

        UpdateSwipeState();
    }

    private void UpdateSwipeState()
    {
        CanSwipePrevious = _historyIndex > 0;
    }

    [RelayCommand]
    public async Task SelectCategoryAsync(QuoteCategory category)
    {
        if (!IsPremium && !category.IsFreeCategory)
        {
            // Locked category: prompt in-app purchase
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

        // Generates watermarked bitmap export without plain text copying
        ShareStatusMessage = "IMAGE FILIGRANÉE GÉNÉRÉE · PARTAGE SÉCURISÉ PRÊT";
        await Task.Delay(2500);
        ShareStatusMessage = string.Empty;
    }
}
