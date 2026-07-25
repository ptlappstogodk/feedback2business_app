using System.Threading.Tasks;
using System.Windows.Input;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class SettingsNotificationsViewModel : ObservableObject
{
    private bool _emailAlerts = true;
    private bool _weeklySummary = true;
    private bool _webhookNotifications = false;
    private string _notificationEmail = "admin@feedback2business.com";

    public bool EmailAlerts
    {
        get => _emailAlerts;
        set => SetProperty(ref _emailAlerts, value);
    }

    public bool WeeklySummary
    {
        get => _weeklySummary;
        set => SetProperty(ref _weeklySummary, value);
    }

    public bool WebhookNotifications
    {
        get => _webhookNotifications;
        set => SetProperty(ref _webhookNotifications, value);
    }

    public string NotificationEmail
    {
        get => _notificationEmail;
        set => SetProperty(ref _notificationEmail, value);
    }

    public ICommand SaveNotificationsSettingsCommand { get; }

    public SettingsNotificationsViewModel(IMockDataService data)
    {
        SaveNotificationsSettingsCommand = new RelayCommand(async () => await SaveAsync());
    }

    private async Task SaveAsync()
    {
        await Application.Current!.MainPage!.DisplayAlert("Notifikationer", "Notifikationspræferencer er gemt.", "OK");
    }
}
