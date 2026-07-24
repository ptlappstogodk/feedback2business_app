using Feedback2Business.Views;
using Feedback2Business.Services;
using Feedback2Business.ViewModels;
using Microsoft.Maui.ApplicationModel;
using System.Linq;

namespace Feedback2Business;

public partial class App : Application
{
    private readonly AppShell _shell;
    private readonly LoginPage _loginPage;
    private readonly IMockDataService _dataService;
    private readonly MainShellViewModel _shellVm;

    public App(AppShell shell, LoginPage loginPage, IMockDataService dataService, MainShellViewModel shellVm)
    {
        InitializeComponent();

        _shell = shell;
        _loginPage = loginPage;
        _dataService = dataService;
        _shellVm = shellVm;

        CheckSavedLoginAndSetMainPage();
    }

    private void CheckSavedLoginAndSetMainPage()
    {
        var email = Preferences.Default.Get("SavedEmail", string.Empty);
        var password = Preferences.Default.Get("SavedPassword", string.Empty);

        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
        {
            try
            {
                var user = _dataService.Login(email, password);
                if (user != null)
                {
                    _shellVm.LoggedInUser = user;

                    var orgs = _dataService.GetOrganizations(user.Id);
                    if (orgs.Count > 0)
                    {
                        _shellVm.ActiveOrganization = orgs.First();
                    }

                    MainPage = _shell;
                    return;
                }
            }
            catch
            {
                // Fall back to login page
            }
        }

        MainPage = _loginPage;
    }
}


