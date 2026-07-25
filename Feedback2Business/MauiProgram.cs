using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Feedback2Business.Services;
using Feedback2Business.ViewModels;
using Feedback2Business.Views;
using Feedback2Business.Views.ActivityLog;
using Feedback2Business.Views.Media;
using Feedback2Business.Views.Organizations;
using Feedback2Business.Views.Overview;
using Feedback2Business.Views.Roles;
using Feedback2Business.Views.Settings;
using Feedback2Business.Views.Templates;
using Feedback2Business.Views.Variables;

namespace Feedback2Business;

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

        builder.Services.AddSingleton<IMockDataService, ApiDataService>();

        builder.Services.AddSingleton<MainShellViewModel>();

        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddSingleton<DashboardShellPage>();

        builder.Services.AddTransient<OverviewViewModel>();
        builder.Services.AddTransient<OrganizationsViewModel>();
        builder.Services.AddTransient<SurveysViewModel>();
        builder.Services.AddTransient<OrganizationAppPublishingViewModel>();
        builder.Services.AddTransient<OrganizationUsersViewModel>();
        builder.Services.AddTransient<TemplatesViewModel>();
        builder.Services.AddTransient<VariablesViewModel>();
        builder.Services.AddTransient<MediaLibraryViewModel>();
        builder.Services.AddTransient<SettingsGeneralViewModel>();
        builder.Services.AddTransient<SettingsAppViewModel>();
        builder.Services.AddTransient<SettingsSecurityViewModel>();
        builder.Services.AddTransient<SettingsNotificationsViewModel>();
        builder.Services.AddTransient<SettingsIntegrationsViewModel>();
        builder.Services.AddTransient<SettingsAppearanceViewModel>();
        builder.Services.AddTransient<RolesPermissionsViewModel>();
        builder.Services.AddTransient<ActivityLogViewModel>();

        builder.Services.AddTransient<OverviewPage>();
        builder.Services.AddTransient<OrganizationsPage>();
        builder.Services.AddTransient<SurveysPage>();
        builder.Services.AddTransient<OrganizationAppPublishingPage>();
        builder.Services.AddTransient<OrganizationUsersPage>();
        builder.Services.AddTransient<TemplatesPage>();
        builder.Services.AddTransient<VariablesPage>();
        builder.Services.AddTransient<MediaLibraryPage>();
        builder.Services.AddTransient<SettingsGeneralPage>();
        builder.Services.AddTransient<SettingsAppPage>();
        builder.Services.AddTransient<SettingsSecurityPage>();
        builder.Services.AddTransient<SettingsNotificationsPage>();
        builder.Services.AddTransient<SettingsIntegrationsPage>();
        builder.Services.AddTransient<SettingsAppearancePage>();
        builder.Services.AddTransient<RolesPermissionsPage>();
        builder.Services.AddTransient<ActivityLogPage>();

        return builder.Build();
    }
}


