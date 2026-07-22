using System.Collections.ObjectModel;

namespace Feedback2Business.ViewModels;

public class SettingsGeneralViewModel : ObservableObject
{
    private string _organizationName = "Retail Group A";
    private string _selectedLanguage = "Dansk";
    private string _selectedTimezone = "UTC+01:00 København";
    private string _dashboardName = "Dashboard";
    private string _dateFormat = "DD-MM-YYYY";

    public string OrganizationName
    {
        get => _organizationName;
        set => SetProperty(ref _organizationName, value);
    }

    public ObservableCollection<string> Languages { get; } = new() { "Dansk", "English", "German" };
    public ObservableCollection<string> Timezones { get; } = new() { "UTC+01:00 København", "UTC+00:00 London" };

    public string SelectedLanguage
    {
        get => _selectedLanguage;
        set => SetProperty(ref _selectedLanguage, value);
    }

    public string SelectedTimezone
    {
        get => _selectedTimezone;
        set => SetProperty(ref _selectedTimezone, value);
    }

    public string DashboardName
    {
        get => _dashboardName;
        set => SetProperty(ref _dashboardName, value);
    }

    public string DateFormat
    {
        get => _dateFormat;
        set => SetProperty(ref _dateFormat, value);
    }
}


