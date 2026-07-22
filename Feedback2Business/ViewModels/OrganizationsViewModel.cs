using System.Collections.ObjectModel;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

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


