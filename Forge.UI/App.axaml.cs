using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Forge.Core.Repositories;
using Forge.Core.ViewModels;
using Forge.UI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Forge.UI;

public partial class App : Application
{
    public IServiceProvider? Services { get; set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            var userRepo = Services?.GetRequiredService<IUserRepository>();
            if (userRepo != null && !userRepo.IsOnboardingCompleted)
            {
                var onboardingVm = new OnboardingViewModel(userRepo, () =>
                {
                    singleViewPlatform.MainView = new MainView
                    {
                        DataContext = Services?.GetRequiredService<MainViewModel>()
                    };
                });

                singleViewPlatform.MainView = new OnboardingView { DataContext = onboardingVm };
            }
            else
            {
                singleViewPlatform.MainView = new MainView
                {
                    DataContext = Services?.GetRequiredService<MainViewModel>()
                };
            }
        }

        base.OnFrameworkInitializationCompleted();
    }
}
