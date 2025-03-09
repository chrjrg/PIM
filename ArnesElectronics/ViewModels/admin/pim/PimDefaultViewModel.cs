using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ArnesElectronics.ViewModels.admin.pim;

public partial class PimDefaultViewModel : ViewModelBase
{
    [ObservableProperty]
    private string buttonClickText = "Binding test";

    [RelayCommand]
    private void TestBinding()
    {
        ButtonClickText = "Binding Works!";
    }
}