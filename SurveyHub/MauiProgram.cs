using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using SurveyHub.Services;
using SurveyHub.ViewModels;
using SurveyHub.Views;
using SurveyHub.Views.ActivityLog;
using SurveyHub.Views.Media;
using SurveyHub.Views.Organizations;
using SurveyHub.Views.Roles;
using SurveyHub.Views.Settings;
using SurveyHub.Views.Templates;
using SurveyHub.Views.Variables;

namespace SurveyHub;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<IMockDataService, MockDataService>();

        builder.Services.AddSingleton<MainShellViewModel>();

        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<DashboardShellPage>();

        builder.Services.AddTransient<OrganizationsViewModel>();
        builder.Services.AddTransient<OrganizationBrandsViewModel>();
        builder.Services.AddTransient<OrganizationUsersViewModel>();
        builder.Services.AddTransient<TemplatesViewModel>();
        builder.Services.AddTransient<VariablesViewModel>();
        builder.Services.AddTransient<MediaLibraryViewModel>();
        builder.Services.AddTransient<SettingsGeneralViewModel>();
        builder.Services.AddTransient<SettingsAppViewModel>();
        builder.Services.AddTransient<RolesPermissionsViewModel>();
        builder.Services.AddTransient<ActivityLogViewModel>();

        builder.Services.AddTransient<OrganizationsPage>();
        builder.Services.AddTransient<OrganizationBrandsPage>();
        builder.Services.AddTransient<OrganizationUsersPage>();
        builder.Services.AddTransient<TemplatesPage>();
        builder.Services.AddTransient<VariablesPage>();
        builder.Services.AddTransient<MediaLibraryPage>();
        builder.Services.AddTransient<SettingsGeneralPage>();
        builder.Services.AddTransient<SettingsAppPage>();
        builder.Services.AddTransient<RolesPermissionsPage>();
        builder.Services.AddTransient<ActivityLogPage>();

        return builder.Build();
    }
}


