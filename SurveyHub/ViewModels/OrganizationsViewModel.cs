using System.Collections.ObjectModel;
using SurveyHub.Models;
using SurveyHub.Services;

namespace SurveyHub.ViewModels;

public class OrganizationsViewModel : ObservableObject
{
    private string _searchText = string.Empty;

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public ObservableCollection<OrganizationModel> Organizations { get; } = new();

    public OrganizationsViewModel(IMockDataService data)
    {
        foreach (var item in data.GetOrganizations())
            Organizations.Add(item);
    }
}


