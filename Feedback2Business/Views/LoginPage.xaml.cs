using System;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Extensions.DependencyInjection;
using Feedback2Business.Services;
using Feedback2Business.ViewModels;
using Feedback2Business.Models;

namespace Feedback2Business.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(IMockDataService dataService, MainShellViewModel shellVm)
    {
        InitializeComponent();
        BindingContext = new LoginViewModel(dataService, shellVm);
    }
}

public class LoginViewModel : ObservableObject
{
    private readonly IMockDataService _dataService;
    private readonly MainShellViewModel _shellVm;

    private bool _isRegisterMode;
    private string _name = string.Empty;
    private string _email = string.Empty;
    private string _password = string.Empty;
    private string _organizationName = string.Empty;
    private string _errorMessage = string.Empty;

    public bool IsRegisterMode
    {
        get => _isRegisterMode;
        set
        {
            if (SetProperty(ref _isRegisterMode, value))
            {
                Raise(nameof(IsLoginMode));
                Raise(nameof(TitleText));
                Raise(nameof(PrimaryActionButtonText));
                Raise(nameof(SecondaryActionLinkText));
                ErrorMessage = string.Empty;
            }
        }
    }

    public bool IsLoginMode => !IsRegisterMode;

    public string TitleText => IsRegisterMode ? "Opret en ny konto" : "Log ind på din konto";
    public string PrimaryActionButtonText => IsRegisterMode ? "Opret bruger" : "Log ind";
    public string SecondaryActionLinkText => IsRegisterMode ? "Tilbage til log ind" : "Opret ny bruger";

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public string OrganizationName
    {
        get => _organizationName;
        set => SetProperty(ref _organizationName, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            if (SetProperty(ref _errorMessage, value))
            {
                Raise(nameof(HasError));
            }
        }
    }

    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

    public ICommand PrimaryActionCommand { get; }
    public ICommand ToggleModeCommand { get; }
    public ICommand ForgotPasswordCommand { get; }

    public LoginViewModel(IMockDataService dataService, MainShellViewModel shellVm)
    {
        _dataService = dataService;
        _shellVm = shellVm;

        PrimaryActionCommand = new RelayCommand(async () => await ExecutePrimaryActionAsync());
        ToggleModeCommand = new RelayCommand(() => IsRegisterMode = !IsRegisterMode);
        ForgotPasswordCommand = new RelayCommand(async () => await ForgotPasswordAsync());
    }

    private async Task ExecutePrimaryActionAsync()
    {
        ErrorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Email og adgangskode skal udfyldes.";
            return;
        }

        if (IsRegisterMode)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                ErrorMessage = "Navn skal udfyldes.";
                return;
            }

            try
            {
                var user = _dataService.Register(Name.Trim(), Email.Trim(), Password.Trim(), OrganizationName.Trim());
                if (user != null)
                {
                    Preferences.Default.Set("SavedEmail", Email.Trim());
                    Preferences.Default.Set("SavedPassword", Password.Trim());

                    _shellVm.LoggedInUser = user;

                    var orgs = _dataService.GetOrganizations(user.Id);
                    if (orgs.Count > 0)
                    {
                        _shellVm.ActiveOrganization = orgs.First();
                    }
                    
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        if (Application.Current != null)
                        {
                            var appShell = (AppShell)Application.Current.Handler.MauiContext!.Services.GetRequiredService<AppShell>();
                            Application.Current.MainPage = appShell;
                        }
                    });
                }
                else
                {
                    ErrorMessage = "Kunne ikke oprette bruger.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        else
        {
            try
            {
                var user = _dataService.Login(Email.Trim(), Password.Trim());
                if (user != null)
                {
                    Preferences.Default.Set("SavedEmail", Email.Trim());
                    Preferences.Default.Set("SavedPassword", Password.Trim());

                    _shellVm.LoggedInUser = user;

                    var orgs = _dataService.GetOrganizations(user.Id);
                    if (orgs.Count > 0)
                    {
                        _shellVm.ActiveOrganization = orgs.First();
                    }

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        if (Application.Current != null)
                        {
                            var appShell = (AppShell)Application.Current.Handler.MauiContext!.Services.GetRequiredService<AppShell>();
                            Application.Current.MainPage = appShell;
                        }
                    });
                }
                else
                {
                    ErrorMessage = "Forkert email eller adgangskode.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }

    private async Task ForgotPasswordAsync()
    {
        var email = await Application.Current!.MainPage!.DisplayPromptAsync("Glemt adgangskode", "Indtast din email adresse for at nulstille din adgangskode:", "Nulstil", "Annuller", "Email");
        if (!string.IsNullOrWhiteSpace(email))
        {
            await Application.Current!.MainPage!.DisplayAlert("Adgangskode nulstillet", "Hvis email-adressen findes i vores system, har du modtaget en email med instruktioner til at nulstille din adgangskode.", "OK");
        }
    }
}
