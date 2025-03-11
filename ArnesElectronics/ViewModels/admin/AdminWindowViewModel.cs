using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ArnesElectronics.Services;
using ArnesElectronics.ViewModels.admin.dam;
using ArnesElectronics.ViewModels.admin.oms;
using ArnesElectronics.ViewModels.admin.pim;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ArnesElectronics.ViewModels.admin;

public partial class AdminWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isPaneOpen = true;

    [ObservableProperty] 
    private ViewModelBase _currentPage = new DefaultDashboardViewModel();

    [ObservableProperty]
    private ListItemTemplate? _selectedListItem;

    partial void OnSelectedListItemChanged(ListItemTemplate? value)
    {
        if (value is null) return;

        if (value.SubItems.Any())
        {
            if (CurrentPage.GetType() == value.ModelType)
            {
                // If already selected, toggle submenu
                value.IsExpanded = !value.IsExpanded;
            }
            else
            {
                // Select the parent item first before expanding
                SelectedListItem = value;

                // Navigate to main page before showing submenu
                if (Activator.CreateInstance(value.ModelType) is ViewModelBase viewModel)
                {
                    Console.WriteLine($"Navigating to: {viewModel.GetType().Name}");
                    CurrentPage = viewModel;
                }

                // Ensure submenu stays expanded
                value.IsExpanded = true;
            }
        }
        else
        {
            // Navigate directly to the new page
            if (Activator.CreateInstance(value.ModelType) is ViewModelBase viewModel)
            {
                Console.WriteLine($"Navigating to: {viewModel.GetType().Name}");
                CurrentPage = viewModel;
            }
        }
    }
    // Menu list for all admin views
    public ObservableCollection<ListItemTemplate> Items { get; } = new()
    {
        new ListItemTemplate(typeof(PimDefaultViewModel), "PimIcon", "PIM", new ObservableCollection<ListItemTemplate>
        {
            new ListItemTemplate(typeof(ProductsViewModel), "ArrowIcon", "Products"),
        }),

        new ListItemTemplate(typeof(OmsDefaultViewModel), "OmsIcon", "OMS", new ObservableCollection<ListItemTemplate>
        {
            new ListItemTemplate(typeof(OmsTabViewModel), "ArrowIcon", "Orders"),
        }),

        new ListItemTemplate(typeof(DamDefaultViewModel), "DamIcon", "DAM", new ObservableCollection<ListItemTemplate>
        {
            new ListItemTemplate(typeof(DamTabViewModel), "ArrowIcon", "Assets"),
        }),
    };

    [RelayCommand]
    private void TriggerPane()
    {
        IsPaneOpen = !IsPaneOpen;
    }
    
    public ICommand ExitCommand { get; }
    public ICommand SwitchColorTheme { get; }

    public AdminWindowViewModel()
    {
        ExitCommand = new RelayCommand(ExitApplication);
        SwitchColorTheme = new RelayCommand(ToggleOnClick);
    }

    private void ExitApplication()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown();
        }
    }
    
    
    private void ToggleOnClick()
    {
        ThemeManager.ToggleTheme();
    }
    
}

public partial class ListItemTemplate : ViewModelBase
{
    public string Label { get; set; }
    public Type ModelType { get; }
    public StreamGeometry ListItemIcon { get; } 
    public ObservableCollection<ListItemTemplate> SubItems { get; } // Stores submenu items
    
    [ObservableProperty]
    private bool _isExpanded;
    private const string StreamGeometryNotFound = "Icon not found";
        
    public ListItemTemplate(Type type, string iconKey, string customLabel, ObservableCollection<ListItemTemplate>? subItems = null)
    {
        ModelType = type;
        Label = string.IsNullOrWhiteSpace(customLabel) ? type.Name.Replace("ViewModel", "") : customLabel;

        Application.Current!.TryFindResource(iconKey, out var res);
        var streamGeometry = res as StreamGeometry ?? StreamGeometry.Parse(StreamGeometryNotFound);
        ListItemIcon = streamGeometry;

        SubItems = subItems ?? new ObservableCollection<ListItemTemplate>();
    }
}