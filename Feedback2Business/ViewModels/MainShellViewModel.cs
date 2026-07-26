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
    private BrandModel? _activeBrand;
    private SurveyModel? _activeSurvey;
    private UserModel? _currentUser;
    private UserModel? _loggedInUser;

    public event EventHandler<string>? NavigationRequested;

    public ObservableCollection<NavigationItem> NavigationItems { get; } = new();

    public UserModel? LoggedInUser
    {
        get => _loggedInUser;
        set
        {
            if (SetProperty(ref _loggedInUser, value))
            {
                LoadCurrentUser();
            }
        }
    }

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

    public BrandModel? ActiveBrand
    {
        get => _activeBrand;
        set => SetProperty(ref _activeBrand, value);
    }

    public SurveyModel? ActiveSurvey
    {
        get => _activeSurvey;
        set => SetProperty(ref _activeSurvey, value);
    }

    private void LoadCurrentUser()
    {
        if (ActiveOrganization != null && LoggedInUser != null)
        {
            var users = _data.GetUsers(ActiveOrganization.Id);
            var me = users.FirstOrDefault(u => u.Email.ToLower() == LoggedInUser.Email.ToLower());
            if (me != null)
            {
                CurrentUser = me;
                return;
            }
        }
        CurrentUser = LoggedInUser ?? new UserModel { Name = "Intet Navn", Role = "Ingen" };
    }

    public void Logout()
    {
        Microsoft.Maui.Storage.Preferences.Default.Remove("SavedEmail");
        Microsoft.Maui.Storage.Preferences.Default.Remove("SavedPassword");

        LoggedInUser = null;
        CurrentUser = null;
        ActiveOrganization = null;

        Microsoft.Maui.ApplicationModel.MainThread.BeginInvokeOnMainThread(() =>
        {
            if (Application.Current != null)
            {
                var loginPage = (Views.LoginPage)Application.Current.Handler.MauiContext!.Services.GetRequiredService<Views.LoginPage>();
                Application.Current.MainPage = loginPage;
            }
        });
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

        NavigationItems.Add(new NavigationItem { Key = "Overview", Title = "Overblik" });
        NavigationItems.Add(new NavigationItem { Key = "Organizations", Title = "Organisationer & brands" });
        NavigationItems.Add(new NavigationItem { Key = "Surveys", Title = "Surveys" });
        NavigationItems.Add(new NavigationItem { Key = "AppPublishing", Title = "App-udgivelse" });
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


