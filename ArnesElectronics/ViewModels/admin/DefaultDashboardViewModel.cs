using ArnesElectronics.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ArnesElectronics.ViewModels.admin;

public partial class DefaultDashboardViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _buttonClickText = "Binding test";

    [RelayCommand]
    private void TestBinding()
    {
        ButtonClickText = "Binding Works!";
        ThemeManager.ToggleTheme();
    }
}