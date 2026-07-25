using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class PublishableSurveyItem : ObservableObject
{
    private bool _isIncluded;
    public SurveyModel Survey { get; set; } = new();

    public bool IsIncluded
    {
        get => _isIncluded;
        set => SetProperty(ref _isIncluded, value);
    }
}

public class DeploymentLogItem
{
    public string Timestamp { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Channel { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
}

public class OrganizationAppPublishingViewModel : ObservableObject
{
    private readonly IMockDataService _data;
    public MainShellViewModel ShellVm { get; }

    private string _selectedChannel = "Produktion";
    private string _appVersion = "v2.4.1";
    private string _statusMessage = "Klar til udgivelse";

    public ObservableCollection<string> Channels { get; } = new()
    {
        "Produktion",
        "Staging",
        "Test / Intern"
    };

    public string SelectedChannel
    {
        get => _selectedChannel;
        set => SetProperty(ref _selectedChannel, value);
    }

    public string AppVersion
    {
        get => _appVersion;
        set => SetProperty(ref _appVersion, value);
    }

    public string StatusMessage
    {
        get => _statusMessage;
        set => SetProperty(ref _statusMessage, value);
    }

    public ObservableCollection<PublishableSurveyItem> SurveysForPublishing { get; } = new();
    public ObservableCollection<DeploymentLogItem> DeploymentLogs { get; } = new();

    public ICommand PublicerUdgivelseCommand { get; }
    public ICommand TvingSynkroniseringCommand { get; }

    public OrganizationAppPublishingViewModel(IMockDataService data, MainShellViewModel shellVm)
    {
        _data = data;
        ShellVm = shellVm;

        PublicerUdgivelseCommand = new RelayCommand(async () => await PublicerUdgivelseAsync());
        TvingSynkroniseringCommand = new RelayCommand(async () => await TvingSynkroniseringAsync());

        LoadSurveysAndLogs();
    }

    private void LoadSurveysAndLogs()
    {
        SurveysForPublishing.Clear();
        var allSurveys = _data.GetSurveys();
        foreach (var s in allSurveys)
        {
            SurveysForPublishing.Add(new PublishableSurveyItem { Survey = s, IsIncluded = true });
        }

        DeploymentLogs.Clear();
        DeploymentLogs.Add(new DeploymentLogItem
        {
            Timestamp = "2026-07-24 14:30",
            Version = "v2.4.0",
            Channel = "Produktion",
            Status = "Gennemført",
            Author = "Anders Kirk"
        });
        DeploymentLogs.Add(new DeploymentLogItem
        {
            Timestamp = "2026-07-20 09:15",
            Version = "v2.3.9",
            Channel = "Staging",
            Status = "Gennemført",
            Author = "Maria Jensen"
        });
        DeploymentLogs.Add(new DeploymentLogItem
        {
            Timestamp = "2026-07-15 11:00",
            Version = "v2.3.8",
            Channel = "Produktion",
            Status = "Gennemført",
            Author = "Lars Petersen"
        });
    }

    private async Task PublicerUdgivelseAsync()
    {
        var includedCount = SurveysForPublishing.Count(s => s.IsIncluded);
        if (includedCount == 0)
        {
            await Application.Current!.MainPage!.DisplayAlert("Udgivelse", "Vælg venligst mindst ét survey til udgivelse.", "OK");
            return;
        }

        bool confirm = await Application.Current!.MainPage!.DisplayAlert(
            "Bekræft udgivelse",
            $"Vil du udgive {includedCount} surveys til '{SelectedChannel}' kanalen med app version {AppVersion}?",
            "Ja, udgiv nu",
            "Annuller");

        if (confirm)
        {
            DeploymentLogs.Insert(0, new DeploymentLogItem
            {
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                Version = AppVersion,
                Channel = SelectedChannel,
                Status = "Gennemført",
                Author = ShellVm.CurrentUser?.Name ?? "Admin"
            });

            StatusMessage = $"Udgivelse {AppVersion} gennemført til {SelectedChannel}!";
            await Application.Current!.MainPage!.DisplayAlert("Udgivelse gennemført", $"App-konfiguration {AppVersion} er nu live på {SelectedChannel}.", "OK");
        }
    }

    private async Task TvingSynkroniseringAsync()
    {
        StatusMessage = "Synkroniserer enheder...";
        await Task.Delay(500);
        StatusMessage = "Alle mobile enheder synkroniseret!";
        await Application.Current!.MainPage!.DisplayAlert("Synkronisering", "Tvungen synkroniseringsanmodning sendt til alle aktive feltenheder.", "OK");
    }
}
