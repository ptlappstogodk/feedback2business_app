using System.Threading.Tasks;
using System.Windows.Input;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class SettingsSecurityViewModel : ObservableObject
{
    private bool _ssoEnabled = true;
    private bool _requireTwoFactor = true;
    private int _passwordExpiryDays = 90;
    private string _ipWhitelist = "192.168.1.0/24, 10.0.0.0/16";

    public bool SsoEnabled
    {
        get => _ssoEnabled;
        set => SetProperty(ref _ssoEnabled, value);
    }

    public bool RequireTwoFactor
    {
        get => _requireTwoFactor;
        set => SetProperty(ref _requireTwoFactor, value);
    }

    public int PasswordExpiryDays
    {
        get => _passwordExpiryDays;
        set => SetProperty(ref _passwordExpiryDays, value);
    }

    public string IpWhitelist
    {
        get => _ipWhitelist;
        set => SetProperty(ref _ipWhitelist, value);
    }

    public ICommand SaveSecuritySettingsCommand { get; }

    public SettingsSecurityViewModel(IMockDataService data)
    {
        SaveSecuritySettingsCommand = new RelayCommand(async () => await SaveAsync());
    }

    private async Task SaveAsync()
    {
        await Application.Current!.MainPage!.DisplayAlert("Sikkerhedsindstillinger", "Sikkerhedspolitik opdateret og gemt.", "OK");
    }
}
