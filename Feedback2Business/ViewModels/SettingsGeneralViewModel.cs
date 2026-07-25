using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class SettingsGeneralViewModel : ObservableObject
{
    private readonly IMockDataService _data;
    private readonly MainShellViewModel _shellVm;
    private AppSettingModel _settings = new();
    private bool _isLoading;

    public string OrganizationName
    {
        get => _shellVm.ActiveOrganization?.Name ?? string.Empty;
        set
        {
            if (_shellVm.ActiveOrganization != null && _shellVm.ActiveOrganization.Name != value)
            {
                _shellVm.ActiveOrganization.Name = value;
                _shellVm.NotifyActiveOrganizationChanged();
                Raise();
            }
        }
    }

    public ObservableCollection<string> Languages { get; } = new() { "Dansk", "English", "German" };
    public ObservableCollection<string> Timezones { get; } = new() { "UTC+01:00 København", "UTC+00:00 London" };

    public string SelectedLanguage
    {
        get => _settings.Language;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) return;

            if (_settings.Language != value)
            {
                _settings.Language = value;
                Raise();
                Save();
            }
        }
    }

    public string SelectedTimezone
    {
        get => _settings.Timezone;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) return;

            if (_settings.Timezone != value)
            {
                _settings.Timezone = value;
                Raise();
                Save();
            }
        }
    }

    private string _dashboardName = "Dashboard";
    public string DashboardName
    {
        get => _dashboardName;
        set => SetProperty(ref _dashboardName, value);
    }

    public string DateFormat
    {
        get => _settings.DateFormat;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) return;

            if (_settings.DateFormat != value)
            {
                _settings.DateFormat = value;
                Raise();
                Save();
            }
        }
    }

    public ICommand SaveCommand { get; }

    public SettingsGeneralViewModel(IMockDataService data, MainShellViewModel shellVm)
    {
        _isLoading = true;
        _data = data;
        _shellVm = shellVm;

        SaveCommand = new RelayCommand(async () => await SaveExplicitAsync());

        if (_shellVm.ActiveOrganization == null)
        {
            var orgs = _data.GetOrganizations();
            if (orgs.Count > 0)
            {
                _shellVm.ActiveOrganization = orgs.First();
            }
        }

        int orgId = _shellVm.ActiveOrganization?.Id ?? 1;
        _settings = data.GetAppSettings(orgId);

        if (string.IsNullOrWhiteSpace(_settings.Language)) _settings.Language = "Dansk";
        if (string.IsNullOrWhiteSpace(_settings.Timezone)) _settings.Timezone = "UTC+01:00 København";
        if (string.IsNullOrWhiteSpace(_settings.DateFormat)) _settings.DateFormat = "DD-MM-YYYY";
        if (string.IsNullOrWhiteSpace(_settings.SelectedMaxFileSize)) _settings.SelectedMaxFileSize = "2 MB";

        _isLoading = false;
    }

    private void Save()
    {
        if (_isLoading) return;

        if (string.IsNullOrWhiteSpace(_settings.Language)) _settings.Language = "Dansk";
        if (string.IsNullOrWhiteSpace(_settings.Timezone)) _settings.Timezone = "UTC+01:00 København";
        if (string.IsNullOrWhiteSpace(_settings.DateFormat)) _settings.DateFormat = "DD-MM-YYYY";
        if (string.IsNullOrWhiteSpace(_settings.SelectedMaxFileSize)) _settings.SelectedMaxFileSize = "2 MB";

        _data.SaveAppSettings(_settings);
    }

    private async Task SaveExplicitAsync()
    {
        Save();
        await Application.Current!.MainPage!.DisplayAlert("Generelle indstillinger", "Indstillinger er gemt.", "OK");
    }
}


