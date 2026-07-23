using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class OrganizationsViewModel : ObservableObject
{
    private readonly IMockDataService _data;
    private readonly MainShellViewModel _shellVm;
    private string _searchText = string.Empty;
    private OrganizationModel? _selectedOrganization;

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public ObservableCollection<OrganizationModel> Organizations { get; } = new();

    public OrganizationModel? SelectedOrganization
    {
        get => _selectedOrganization;
        set
        {
            if (SetProperty(ref _selectedOrganization, value) && value != null)
            {
                _shellVm.ActiveOrganization = value;
                _shellVm.RequestNavigation("Brands");
            }
        }
    }

    public ICommand OpretOrganizationCommand { get; }

    public OrganizationsViewModel(IMockDataService data, MainShellViewModel shellVm)
    {
        _data = data;
        _shellVm = shellVm;
        foreach (var item in data.GetOrganizations())
            Organizations.Add(item);

        if (_shellVm.ActiveOrganization == null && Organizations.Count > 0)
        {
            _shellVm.ActiveOrganization = Organizations.First();
        }

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


