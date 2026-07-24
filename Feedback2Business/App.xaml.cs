using Feedback2Business.Views;
using Feedback2Business.Services;
using Feedback2Business.ViewModels;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Feedback2Business;

public partial class App : Application
{
    private readonly IServiceProvider _services;

    public App(IServiceProvider services)
    {
        InitializeComponent();

        _services = services;

        CheckSavedLoginAndSetMainPage();
    }

    private void CheckSavedLoginAndSetMainPage()
    {
        var email = Preferences.Default.Get("SavedEmail", string.Empty);
        var password = Preferences.Default.Get("SavedPassword", string.Empty);

        var shellVm = _services.GetRequiredService<MainShellViewModel>();
        var dataService = _services.GetRequiredService<IMockDataService>();

        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
        {
            try
            {
                var user = dataService.Login(email, password);
                if (user != null)
                {
                    shellVm.LoggedInUser = user;

                    var orgs = dataService.GetOrganizations(user.Id);
                    if (orgs.Count > 0)
                    {
                        shellVm.ActiveOrganization = orgs.First();
                    }

                    MainPage = _services.GetRequiredService<AppShell>();
                    return;
                }
            }
            catch
            {
                // Fall back to login page
            }
        }

        MainPage = _services.GetRequiredService<LoginPage>();
    }
}


