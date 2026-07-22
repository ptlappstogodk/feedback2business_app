using System.Collections.ObjectModel;
using SurveyHub.Models;

namespace SurveyHub.ViewModels;

public class MainShellViewModel : ObservableObject
{
    private string _breadcrumbPrimary = "Organisationer";
    private string _breadcrumbSecondary = "Retail Group A";

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


