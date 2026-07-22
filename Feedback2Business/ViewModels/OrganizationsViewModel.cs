using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class OrganizationsViewModel : ObservableObject
{
    private readonly IMockDataService _data;
    private string _searchText = string.Empty;

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public ObservableCollection<OrganizationModel> Organizations { get; } = new();

    public ICommand OpretOrganizationCommand { get; }

    public OrganizationsViewModel(IMockDataService data)
    {
        _data = data;
        foreach (var item in data.GetOrganizations())
            Organizations.Add(item);

        OpretOrganizationCommand = new RelayCommand(async () => await OpretOrganizationAsync());
    }

    private async Task OpretOrganizationAsync()
    {
        var name = await Application.Current!.MainPage!.DisplayPromptAsync("Opret organisation", "Indtast organisationsnavn:", "Gem", "Annuller", "Navn");
        if (!string.IsNullOrWhiteSpace(name))
        {
            var org = new OrganizationModel
            {
                Name = name.Trim(),
                BrandCount = 0,
                SurveyCount = 0,
                UserCount = 0,
                UpdatedAt = DateTime.Now
            };

            _data.CreateOrganization(org);
            Organizations.Add(org);
        }
    }
}


