using Avalonia;
using Avalonia.Media;
using System;
using Avalonia.Styling;

namespace ArnesElectronics.Services
{
    public static class ThemeManager
    {
        private static bool _isDarkMode = true;

        public static void ToggleTheme()
        {
            _isDarkMode = !_isDarkMode;
            ApplyTheme(_isDarkMode);
        }

        public static void ApplyTheme(bool isDark)
        {
            var app = Application.Current;
            if (app is null) return;

            // Set Avalonia theme variant
            app.RequestedThemeVariant = isDark ? ThemeVariant.Dark : ThemeVariant.Light;

            // Get application resources
            var resources = app.Resources;
        
            // Update theme-related brushes dynamically
            resources["TextColorBrush"] = resources[isDark ? "DarkTextBrush" : "LightTextBrush"];
            resources["ContentBackgroundBrush"] = resources[isDark ? "DarkContentBackgroundBrush" : "LightContentBackgroundBrush"];
            resources["PaneBackgroundBrush"] = resources[isDark ? "DarkPaneBackgroundBrush" : "LightPaneBackgroundBrush"];
            resources["HighlightBackgroundBrush"] = resources[isDark ? "LightPaneBackgroundBrush" : "DarkPaneBackgroundBrush"];
        }

    }
}