using System;
using System.Collections.ObjectModel;
using Feedback2Business.Models;

namespace Feedback2Business.ViewModels;

public class MainShellViewModel : ObservableObject
{
    private string _breadcrumbPrimary = "Organisationer";
    private string _breadcrumbSecondary = "Retail Group A";
    private OrganizationModel? _activeOrganization;

    public event EventHandler<string>? NavigationRequested;

    public ObservableCollection<NavigationItem> NavigationItems { get; } = new();
    public UserModel CurrentUser { get; } = new() { Name = "Anders Kirk", Role = "Admin" };

    public string BreadcrumbPrimary
    {
        get => _breadcrumbPrimary;
        set => SetProperty(ref _breadcrumbPrimary, value);
    }

    public string BreadcrumbSecondary
    {
        get => _breadcrumbSecondary;
        set => SetProperty(ref _breadcrumbSecondary, value);
    }

    public OrganizationModel? ActiveOrganization
    {
        get => _activeOrganization;
        set
        {
            if (SetProperty(ref _activeOrganization, value))
            {
                BreadcrumbSecondary = value?.Name ?? "";
            }
        }
    }

    public void NotifyActiveOrganizationChanged()
    {
        Raise(nameof(ActiveOrganization));
        BreadcrumbSecondary = ActiveOrganization?.Name ?? "";
    }

    public void RequestNavigation(string key)
    {
        NavigationRequested?.Invoke(this, key);
    }

    public MainShellViewModel()
    {
        NavigationItems.Add(new NavigationItem { Key = "Organizations", Title = "Organisationer" });
        NavigationItems.Add(new NavigationItem { Key = "Brands", Title = "Brands" });
        NavigationItems.Add(new NavigationItem { Key = "Users", Title = "Brugere" });
        NavigationItems.Add(new NavigationItem { Key = "Templates", Title = "Skabeloner" });
        NavigationItems.Add(new NavigationItem { Key = "Variables", Title = "Variabler" });
        NavigationItems.Add(new NavigationItem { Key = "Media", Title = "Mediebibliotek" });
        NavigationItems.Add(new NavigationItem { Key = "SettingsGeneral", Title = "Indstillinger" });
        NavigationItems.Add(new NavigationItem { Key = "SettingsApp", Title = "App-indstillinger" });
        NavigationItems.Add(new NavigationItem { Key = "Roles", Title = "Roller & rettigheder" });
        NavigationItems.Add(new NavigationItem { Key = "ActivityLog", Title = "Aktivitetslog" });
    }
}


