using System;
using System.Collections.ObjectModel;
using System.Linq;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class MainShellViewModel : ObservableObject
{
    private readonly IMockDataService _data;
    private string _breadcrumbPrimary = "Organisationer";
    private string _breadcrumbSecondary = string.Empty;
    private OrganizationModel? _activeOrganization;
    private UserModel? _currentUser;

    public event EventHandler<string>? NavigationRequested;

    public ObservableCollection<NavigationItem> NavigationItems { get; } = new();

    public UserModel? CurrentUser
    {
        get => _currentUser;
        set => SetProperty(ref _currentUser, value);
    }

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
                LoadCurrentUser();
            }
        }
    }

    private void LoadCurrentUser()
    {
        if (ActiveOrganization != null)
        {
            var users = _data.GetUsers(ActiveOrganization.Id);
            if (users.Count > 0)
            {
                CurrentUser = users.First();
                return;
            }
        }
        CurrentUser = new UserModel { Name = "Intet Navn", Role = "Ingen" };
    }

    public void NotifyActiveOrganizationChanged()
    {
        Raise(nameof(ActiveOrganization));
        BreadcrumbSecondary = ActiveOrganization?.Name ?? "";
        LoadCurrentUser();
    }

    public void RequestNavigation(string key)
    {
        NavigationRequested?.Invoke(this, key);
    }

    public MainShellViewModel(IMockDataService data)
    {
        _data = data;

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


