using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Forge.Core.ViewModels;

namespace Forge.UI.Views;

/// <summary>
/// Code-behind for MainView handling interactive touch and pointer drag gestures
/// to navigate quotes without reliance on conventional next/previous buttons.
/// </summary>
public partial class MainView : UserControl
{
    private bool _isDragging;
    private Point _pointerStart;
    private const double SwipeThreshold = 75.0;

    private TranslateTransform? _translate;
    private RotateTransform? _rotate;
    private Border? _cardContainer;

    public MainView()
    {
        InitializeComponent();

        _cardContainer = this.FindControl<Border>("QuoteCardContainer");
        if (_cardContainer != null)
        {
            _cardContainer.PointerPressed += OnCardPointerPressed;
            _cardContainer.PointerMoved += OnCardPointerMoved;
            _cardContainer.PointerReleased += OnCardPointerReleased;
            _cardContainer.PointerCaptureLost += OnCardPointerCaptureLost;

            if (_cardContainer.RenderTransform is TransformGroup group)
            {
                foreach (var transform in group.Children)
                {
                    if (transform is TranslateTransform tt) _translate = tt;
                    else if (transform is RotateTransform rt) _rotate = rt;
                }
            }
        }
    }

    private void OnCardPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border card)
        {
            _isDragging = true;
            _pointerStart = e.GetPosition(this);
            e.Pointer.Capture(card);
        }
    }

    private void OnCardPointerMoved(object? sender, PointerEventArgs e)
    {
        if (!_isDragging) return;

        var current = e.GetPosition(this);
        var deltaX = current.X - _pointerStart.X;

        if (_translate != null) _translate.X = deltaX;
        if (_rotate != null) _rotate.Angle = deltaX * 0.035;
        if (_cardContainer != null)
        {
            _cardContainer.Opacity = Math.Clamp(1.0 - Math.Abs(deltaX) / 600.0, 0.45, 1.0);
        }
    }

    private async void OnCardPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (!_isDragging) return;
        _isDragging = false;
        e.Pointer.Capture(null);

        var current = e.GetPosition(this);
        var deltaX = current.X - _pointerStart.X;

        ResetCardTransform();

        if (DataContext is MainViewModel vm)
        {
            if (deltaX > SwipeThreshold)
            {
                // Swipe right: advance to next unseen quote
                await vm.SwipeNextAsync();
            }
            else if (deltaX < -SwipeThreshold)
            {
                // Swipe left: revisit previous quote from current active session
                vm.SwipePrevious();
            }
        }
    }

    private void OnCardPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
    {
        _isDragging = false;
        ResetCardTransform();
    }

    private void ResetCardTransform()
    {
        if (_translate != null) _translate.X = 0;
        if (_rotate != null) _rotate.Angle = 0;
        if (_cardContainer != null) _cardContainer.Opacity = 1.0;
    }
}
