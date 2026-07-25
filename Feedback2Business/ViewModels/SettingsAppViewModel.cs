using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class SettingsAppViewModel : ObservableObject
{
    private readonly IMockDataService _data;
    private readonly MainShellViewModel _shellVm;
    private AppSettingModel _settings = new();
    private bool _isLoading;

    public bool OfflineEnabled
    {
        get => _settings.OfflineEnabled;
        set
        {
            if (_settings.OfflineEnabled != value)
            {
                _settings.OfflineEnabled = value;
                Raise();
                Save();
            }
        }
    }

    public bool AutoSyncEnabled
    {
        get => _settings.AutoSyncEnabled;
        set
        {
            if (_settings.AutoSyncEnabled != value)
            {
                _settings.AutoSyncEnabled = value;
                Raise();
                Save();
            }
        }
    }

    public bool RequireLocationAtStart
    {
        get => _settings.RequireLocationAtStart;
        set
        {
            if (_settings.RequireLocationAtStart != value)
            {
                _settings.RequireLocationAtStart = value;
                Raise();
                Save();
            }
        }
    }

    public bool UseLocationOnObservations
    {
        get => _settings.UseLocationOnObservations;
        set
        {
            if (_settings.UseLocationOnObservations != value)
            {
                _settings.UseLocationOnObservations = value;
                Raise();
                Save();
            }
        }
    }

    public bool CompressImages
    {
        get => _settings.CompressImages;
        set
        {
            if (_settings.CompressImages != value)
            {
                _settings.CompressImages = value;
                Raise();
                Save();
            }
        }
    }

    public ObservableCollection<string> MaxFileSizeOptions { get; } = new() { "1 MB", "2 MB", "5 MB", "10 MB" };

    public string SelectedMaxFileSize
    {
        get => _settings.SelectedMaxFileSize;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                return;

            if (_settings.SelectedMaxFileSize != value)
            {
                _settings.SelectedMaxFileSize = value;
                Raise();
                Save();
            }
        }
    }

    public bool ShowSurveyProgress
    {
        get => _settings.ShowSurveyProgress;
        set
        {
            if (_settings.ShowSurveyProgress != value)
            {
                _settings.ShowSurveyProgress = value;
                Raise();
                Save();
            }
        }
    }

    public bool AllowSkipForward
    {
        get => _settings.AllowSkipForward;
        set
        {
            if (_settings.AllowSkipForward != value)
            {
                _settings.AllowSkipForward = value;
                Raise();
                Save();
            }
        }
    }

    public ICommand SaveCommand { get; }

    public SettingsAppViewModel(IMockDataService data, MainShellViewModel shellVm)
    {
        _isLoading = true;
        _data = data;
        _shellVm = shellVm;

        SaveCommand = new RelayCommand(async () => await SaveExplicitAsync());

        int orgId = _shellVm.ActiveOrganization?.Id ?? 1;
        _settings = data.GetAppSettings(orgId);
        if (string.IsNullOrWhiteSpace(_settings.SelectedMaxFileSize))
        {
            _settings.SelectedMaxFileSize = "2 MB";
        }
        _isLoading = false;
    }

    private void Save()
    {
        if (_isLoading) return;

        if (string.IsNullOrWhiteSpace(_settings.SelectedMaxFileSize))
        {
            _settings.SelectedMaxFileSize = "2 MB";
        }

        _data.SaveAppSettings(_settings);
    }

    private async Task SaveExplicitAsync()
    {
        Save();
        await Application.Current!.MainPage!.DisplayAlert("App-indstillinger", "App-indstillinger er gemt.", "OK");
    }
}


