using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;

namespace Forge.UI.Controls;

/// <summary>
/// Lightweight procedural atmospheric weather particle canvas for Avalonia UI.
/// Supports animated rain, snow, sunny solar pulses, and drifting cloud mist.
/// </summary>
public class WeatherAtmosphereControl : Control
{
    public static readonly StyledProperty<string> WeatherHintProperty =
        AvaloniaProperty.Register<WeatherAtmosphereControl, string>(nameof(WeatherHint), defaultValue: "any");

    public string WeatherHint
    {
        get => GetValue(WeatherHintProperty);
        set => SetValue(WeatherHintProperty, value);
    }

    private readonly DispatcherTimer _animationTimer;
    private readonly Random _random = new();
    private readonly List<Particle> _particles = new();
    private double _pulsePhase;

    private class Particle
    {
        public double X;
        public double Y;
        public double Speed;
        public double Size;
        public double Alpha;
        public double Angle;
    }

    public WeatherAtmosphereControl()
    {
        ClipToBounds = true;

        // Initialize 30fps animation loop
        _animationTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(33)
        };
        _animationTimer.Tick += OnAnimationTick;
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        ResetParticles();
        _animationTimer.Start();
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        _animationTimer.Stop();
        base.OnDetachedFromVisualTree(e);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == WeatherHintProperty)
        {
            ResetParticles();
            InvalidateVisual();
        }
    }

    private void ResetParticles()
    {
        _particles.Clear();
        var count = GetNormalizedHint() switch
        {
            "rain" => 35,
            "snow" => 25,
            "cloudy" => 6,
            _ => 0
        };

        var boundsWidth = Bounds.Width > 0 ? Bounds.Width : 350;
        var boundsHeight = Bounds.Height > 0 ? Bounds.Height : 200;

        for (int i = 0; i < count; i++)
        {
            _particles.Add(new Particle
            {
                X = _random.NextDouble() * boundsWidth,
                Y = _random.NextDouble() * boundsHeight,
                Speed = 2 + _random.NextDouble() * 8,
                Size = 1.5 + _random.NextDouble() * 3.5,
                Alpha = 0.2 + _random.NextDouble() * 0.5,
                Angle = _random.NextDouble() * Math.PI * 2
            });
        }
    }

    private string GetNormalizedHint()
    {
        var hint = WeatherHint?.ToLowerInvariant() ?? "any";
        return hint switch
        {
            "rain" => "rain",
            "snow" => "snow",
            "sunny" => "sunny",
            "cloudy" => "cloudy",
            _ => "any"
        };
    }

    private void OnAnimationTick(object? sender, EventArgs e)
    {
        var width = Bounds.Width;
        var height = Bounds.Height;
        if (width <= 0 || height <= 0) return;

        var hint = GetNormalizedHint();
        _pulsePhase += 0.04;
        if (_pulsePhase > Math.PI * 2) _pulsePhase -= Math.PI * 2;

        switch (hint)
        {
            case "rain":
                foreach (var p in _particles)
                {
                    p.Y += p.Speed * 2.2;
                    p.X += 0.6; // Slight wind slant
                    if (p.Y > height)
                    {
                        p.Y = -10;
                        p.X = _random.NextDouble() * width;
                    }
                }
                break;

            case "snow":
                foreach (var p in _particles)
                {
                    p.Y += p.Speed * 0.35;
                    p.Angle += 0.05;
                    p.X += Math.Sin(p.Angle) * 0.8;
                    if (p.Y > height)
                    {
                        p.Y = -5;
                        p.X = _random.NextDouble() * width;
                    }
                }
                break;

            case "cloudy":
                foreach (var p in _particles)
                {
                    p.X += p.Speed * 0.15;
                    if (p.X > width + 60)
                    {
                        p.X = -60;
                        p.Y = _random.NextDouble() * height;
                    }
                }
                break;
        }

        InvalidateVisual();
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        var width = Bounds.Width;
        var height = Bounds.Height;
        if (width <= 0 || height <= 0) return;

        var hint = GetNormalizedHint();

        switch (hint)
        {
            case "rain":
                DrawRain(context);
                break;

            case "snow":
                DrawSnow(context);
                break;

            case "sunny":
                DrawSunnyGlow(context, width, height);
                break;

            case "cloudy":
                DrawCloudMist(context);
                break;
        }
    }

    private void DrawRain(DrawingContext context)
    {
        var rainPen = new Pen(new SolidColorBrush(Color.FromArgb(140, 90, 115, 145)), 1.2);
        foreach (var p in _particles)
        {
            var length = 8 + p.Speed * 1.5;
            context.DrawLine(rainPen, new Point(p.X, p.Y), new Point(p.X + 1.2, p.Y + length));
        }
    }

    private void DrawSnow(DrawingContext context)
    {
        foreach (var p in _particles)
        {
            var brush = new SolidColorBrush(Color.FromArgb((byte)(p.Alpha * 220), 245, 248, 255));
            context.DrawEllipse(brush, null, new Point(p.X, p.Y), p.Size, p.Size);
        }
    }

    private void DrawSunnyGlow(DrawingContext context, double width, double height)
    {
        // Ambient breathing radial pulse
        var baseRadius = Math.Min(width, height) * 0.45;
        var dynamicRadius = baseRadius + Math.Sin(_pulsePhase) * 12;

        var center = new Point(width * 0.85, height * 0.2);
        var brush = new RadialGradientBrush
        {
            Center = new RelativePoint(0.85, 0.2, RelativeUnit.Relative),
            GradientOrigin = new RelativePoint(0.85, 0.2, RelativeUnit.Relative),
            Radius = 0.6,
            GradientStops = new GradientStops
            {
                new GradientStop(Color.FromArgb(90, 245, 195, 80), 0.0),
                new GradientStop(Color.FromArgb(40, 215, 160, 45), 0.4),
                new GradientStop(Color.FromArgb(0, 215, 160, 45), 1.0)
            }
        };

        context.DrawEllipse(brush, null, center, dynamicRadius, dynamicRadius);
    }

    private void DrawCloudMist(DrawingContext context)
    {
        foreach (var p in _particles)
        {
            var mistBrush = new SolidColorBrush(Color.FromArgb((byte)(p.Alpha * 70), 180, 175, 165));
            context.DrawEllipse(mistBrush, null, new Point(p.X, p.Y), 50 + p.Size * 10, 22 + p.Size * 4);
        }
    }
}
