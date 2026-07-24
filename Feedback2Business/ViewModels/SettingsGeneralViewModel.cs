using System.Collections.ObjectModel;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class SettingsGeneralViewModel : ObservableObject
{
    private readonly IMockDataService _data;
    private readonly MainShellViewModel _shellVm;
    private AppSettingModel _settings = new();

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
            if (_settings.DateFormat != value)
            {
                _settings.DateFormat = value;
                Raise();
                Save();
            }
        }
    }

    public SettingsGeneralViewModel(IMockDataService data, MainShellViewModel shellVm)
    {
        _data = data;
        _shellVm = shellVm;

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
    }

    private void Save()
    {
        _data.SaveAppSettings(_settings);
    }
}


