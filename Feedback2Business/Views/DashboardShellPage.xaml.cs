using Microsoft.Extensions.DependencyInjection;
using Feedback2Business.ViewModels;
using Feedback2Business.Views.ActivityLog;
using Feedback2Business.Views.Media;
using Feedback2Business.Views.Organizations;
using Feedback2Business.Views.Roles;
using Feedback2Business.Views.Settings;
using Feedback2Business.Views.Templates;
using Feedback2Business.Views.Variables;

namespace Feedback2Business.Views;

public partial class DashboardShellPage : ContentPage
{
    private readonly IServiceProvider _services;
    private readonly MainShellViewModel _vm;

    public DashboardShellPage(IServiceProvider services, MainShellViewModel vm)
    {
        InitializeComponent();

        _services = services;
        _vm = vm;

        BindingContext = _vm;
        Header.BindingContext = _vm;

        Sidebar.NavigationRequested += Sidebar_NavigationRequested;
        _vm.NavigationRequested += (s, key) => NavigateTo(key);

        Loaded += DashboardShellPage_Loaded;
    }

    private void DashboardShellPage_Loaded(object? sender, EventArgs e)
    {
        NavigateTo("Brands");
    }

    private void Sidebar_NavigationRequested(object? sender, string key)
    {
        NavigateTo(key);
    }

    private void NavigateTo(string key)
    {
        ContentPage page;

        switch (key)
        {
            case "Organizations":
                _vm.BreadcrumbPrimary = "Organisationer";
                _vm.BreadcrumbSecondary = "";
                page = _services.GetRequiredService<OrganizationsPage>();
                break;

            case "Brands":
                _vm.BreadcrumbPrimary = "Organisationer";
                _vm.BreadcrumbSecondary = _vm.ActiveOrganization?.Name ?? "Retail Group A";
                page = _services.GetRequiredService<OrganizationBrandsPage>();
                break;

            case "Users":
                _vm.BreadcrumbPrimary = "Organisationer";
                _vm.BreadcrumbSecondary = "Retail Group A";
                page = _services.GetRequiredService<OrganizationUsersPage>();
                break;

            case "Templates":
                _vm.BreadcrumbPrimary = "Skabeloner";
                _vm.BreadcrumbSecondary = "";
                page = _services.GetRequiredService<TemplatesPage>();
                break;

            case "Variables":
                _vm.BreadcrumbPrimary = "Variabler";
                _vm.BreadcrumbSecondary = "";
                page = _services.GetRequiredService<VariablesPage>();
                break;

            case "Media":
                _vm.BreadcrumbPrimary = "Mediebibliotek";
                _vm.BreadcrumbSecondary = "";
                page = _services.GetRequiredService<MediaLibraryPage>();
                break;

            case "SettingsGeneral":
                _vm.BreadcrumbPrimary = "Indstillinger";
                _vm.BreadcrumbSecondary = "Generelt";
                page = _services.GetRequiredService<SettingsGeneralPage>();
                break;

            case "SettingsApp":
                _vm.BreadcrumbPrimary = "Indstillinger";
                _vm.BreadcrumbSecondary = "App-indstillinger";
                page = _services.GetRequiredService<SettingsAppPage>();
                break;

            case "Roles":
                _vm.BreadcrumbPrimary = "Roller & rettigheder";
                _vm.BreadcrumbSecondary = "";
                page = _services.GetRequiredService<RolesPermissionsPage>();
                break;

            case "ActivityLog":
                _vm.BreadcrumbPrimary = "Aktivitetslog";
                _vm.BreadcrumbSecondary = "";
                page = _services.GetRequiredService<ActivityLogPage>();
                break;

            default:
                _vm.BreadcrumbPrimary = "Organisationer";
                _vm.BreadcrumbSecondary = "Retail Group A";
                page = _services.GetRequiredService<OrganizationBrandsPage>();
                break;
        }

        PageHost.Content = page.Content;
        if (PageHost.Content != null)
        {
            PageHost.Content.BindingContext = page.BindingContext;
        }
    }
}


