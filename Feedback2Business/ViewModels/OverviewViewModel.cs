using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class OverviewViewModel : ObservableObject
{
    private readonly IMockDataService _data;
    public MainShellViewModel ShellVm { get; }

    public int TotalOrganizations { get; private set; }
    public int ActiveSurveysCount { get; private set; }
    public int TotalUsersCount { get; private set; }
    public int PendingSyncsCount { get; private set; }

    public ObservableCollection<ActivityEventModel> RecentActivity { get; } = new();
    public ObservableCollection<SurveyModel> TopSurveys { get; } = new();

    public ICommand GoToOrganizationsCommand { get; }
    public ICommand GoToSurveysCommand { get; }
    public ICommand GoToUsersCommand { get; }
    public ICommand GoToActivityLogCommand { get; }

    public OverviewViewModel(IMockDataService data, MainShellViewModel shellVm)
    {
        _data = data;
        ShellVm = shellVm;

        GoToOrganizationsCommand = new RelayCommand(() => ShellVm.RequestNavigation("Organizations"));
        GoToSurveysCommand = new RelayCommand(() => ShellVm.RequestNavigation("Brands"));
        GoToUsersCommand = new RelayCommand(() => ShellVm.RequestNavigation("Users"));
        GoToActivityLogCommand = new RelayCommand(() => ShellVm.RequestNavigation("ActivityLog"));

        LoadDashboardData();
    }

    private void LoadDashboardData()
    {
        var orgs = _data.GetOrganizations();
        TotalOrganizations = orgs.Count;

        var surveys = _data.GetSurveys();
        ActiveSurveysCount = surveys.Count;

        var users = _data.GetUsers();
        TotalUsersCount = users.Count;

        PendingSyncsCount = 12;

        RecentActivity.Clear();
        var events = _data.GetActivityEvents().Take(6);
        foreach (var ev in events)
        {
            RecentActivity.Add(ev);
        }

        TopSurveys.Clear();
        foreach (var s in surveys.Take(5))
        {
            TopSurveys.Add(s);
        }

        Raise(nameof(TotalOrganizations));
        Raise(nameof(ActiveSurveysCount));
        Raise(nameof(TotalUsersCount));
        Raise(nameof(PendingSyncsCount));
    }
}
