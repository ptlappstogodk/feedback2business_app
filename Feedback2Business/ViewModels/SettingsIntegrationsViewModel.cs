using System.Threading.Tasks;
using System.Windows.Input;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class SettingsIntegrationsViewModel : ObservableObject
{
    private string _apiEndpoint = "https://api.feedback2business.dk/v1";
    private string _apiKey = "f2b_live_9f8a3b2c1d0e4f5a6b7c8d9e";
    private string _webhookUrl = "https://hooks.feedback2business.dk/surveys";
    private int _syncIntervalMinutes = 15;

    public string ApiEndpoint
    {
        get => _apiEndpoint;
        set => SetProperty(ref _apiEndpoint, value);
    }

    public string ApiKey
    {
        get => _apiKey;
        set => SetProperty(ref _apiKey, value);
    }

    public string WebhookUrl
    {
        get => _webhookUrl;
        set => SetProperty(ref _webhookUrl, value);
    }

    public int SyncIntervalMinutes
    {
        get => _syncIntervalMinutes;
        set => SetProperty(ref _syncIntervalMinutes, value);
    }

    public ICommand SaveIntegrationsSettingsCommand { get; }
    public ICommand GenerateNewKeyCommand { get; }

    public SettingsIntegrationsViewModel(IMockDataService data)
    {
        SaveIntegrationsSettingsCommand = new RelayCommand(async () => await SaveAsync());
        GenerateNewKeyCommand = new RelayCommand(async () => await GenerateKeyAsync());
    }

    private async Task SaveAsync()
    {
        await Application.Current!.MainPage!.DisplayAlert("API & Integrationer", "Integrationsindstillinger er gemt.", "OK");
    }

    private async Task GenerateKeyAsync()
    {
        ApiKey = "f2b_live_" + System.Guid.NewGuid().ToString("N").Substring(0, 24);
        await Application.Current!.MainPage!.DisplayAlert("Ny API Nøgle", "Ny API-nøgle er genereret. Husk at opdatere integrerede klienter.", "OK");
    }
}
