using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using ArnesElectronics.Services;
using Avalonia.Markup.Xaml;
using ArnesElectronics.ViewModels;
using ArnesElectronics.ViewModels.admin;
using ArnesElectronics.Views;
using ArnesElectronics.Views.admin;
using Avalonia.Controls;
namespace ArnesElectronics;

public partial class App : Application
{
    private const string BootWindow = "admin"; // Change to "admin" or "main" depending on which window and corresponding views you want to load

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            DisableAvaloniaDataAnnotationValidation();

            if (BootWindow == "admin")
            {
                desktop.MainWindow = new AdminWindow
                {
                    DataContext = new AdminWindowViewModel(),
                };
            }
            else if (BootWindow == "main")
            {
                desktop.MainWindow = new Views.main.MainWindow
                {
                    DataContext = new ViewModels.main.MainWindowViewModel(),
                };
            }
            else
            {
                throw new InvalidOperationException("Invalid AppMode specified.");
            }
        }

        base.OnFrameworkInitializationCompleted();
        ThemeManager.ApplyTheme(true);
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}
