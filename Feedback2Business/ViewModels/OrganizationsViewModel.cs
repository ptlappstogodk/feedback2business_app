using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class OrganizationsViewModel : ObservableObject
{
    private readonly IMockDataService _data;
    private readonly MainShellViewModel _shellVm;

    private readonly List<OrganizationModel> _allOrganizations = new();
    private string _searchText = string.Empty;
    private string _selectedStatusFilter = "Status: Alle";
    private OrganizationModel? _selectedOrganization;
    private BrandModel? _selectedBrand;
    private string _selectedTab = "Brands";

    // Drawer state
    private bool _isDrawerOpen;
    private string _drawerTitle = "Opret organisation";
    private bool _isEditingExistingOrg;

    private string _drawerName = string.Empty;
    private string _drawerDescription = string.Empty;
    private string _drawerStatus = "Aktiv";
    private string _drawerContactPerson = string.Empty;
    private string _drawerEmail = string.Empty;
    private string _drawerPhone = string.Empty;
    private string _drawerAddress = string.Empty;
    private string _drawerPostalCode = string.Empty;
    private string _drawerCity = string.Empty;
    private string _drawerCountry = "Danmark";

    public ObservableCollection<OrganizationModel> FilteredOrganizations { get; } = new();
    public ObservableCollection<BrandModel> Brands { get; } = new();

    public ObservableCollection<string> StatusFilterOptions { get; } = new()
    {
        "Status: Alle", "Aktiv", "Inviteret", "Inaktiv"
    };

    public ObservableCollection<string> DrawerStatusOptions { get; } = new()
    {
        "Aktiv", "Inviteret", "Inaktiv"
    };

    public ObservableCollection<string> DrawerCountryOptions { get; } = new()
    {
        "Danmark", "Grønland", "Sverige", "Norge", "Tyskland"
    };

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (SetProperty(ref _searchText, value))
            {
                ApplyFilter();
            }
        }
    }

    public string SelectedStatusFilter
    {
        get => _selectedStatusFilter;
        set
        {
            if (SetProperty(ref _selectedStatusFilter, value))
            {
                ApplyFilter();
            }
        }
    }

    public OrganizationModel? SelectedOrganization
    {
        get => _selectedOrganization;
        set
        {
            if (SetProperty(ref _selectedOrganization, value))
            {
                _shellVm.ActiveOrganization = value;
                if (value != null)
                {
                    LoadBrands(value.Id);
                }
                else
                {
                    Brands.Clear();
                }
                Raise(nameof(HasSelectedOrganization));
                Raise(nameof(BrandTabSubtitle));
            }
        }
    }

    public bool HasSelectedOrganization => SelectedOrganization != null;
    public string BrandTabSubtitle => SelectedOrganization != null
        ? $"{SelectedOrganization.BrandCount} brands • {SelectedOrganization.SurveyCount} surveys"
        : "";

    public string SelectedTab
    {
        get => _selectedTab;
        set
        {
            if (SetProperty(ref _selectedTab, value))
            {
                Raise(nameof(IsBrandsTabSelected));
                Raise(nameof(IsInformationTabSelected));
                Raise(nameof(IsSettingsTabSelected));
            }
        }
    }

    public bool IsBrandsTabSelected => SelectedTab == "Brands";
    public bool IsInformationTabSelected => SelectedTab == "Information";
    public bool IsSettingsTabSelected => SelectedTab == "Indstillinger";

    public BrandModel? SelectedBrand
    {
        get => _selectedBrand;
        set
        {
            if (SetProperty(ref _selectedBrand, value))
            {
                Raise(nameof(IsBrandSelected));
            }
        }
    }

    public bool IsBrandSelected => SelectedBrand != null;

    // Drawer properties
    public bool IsDrawerOpen
    {
        get => _isDrawerOpen;
        set => SetProperty(ref _isDrawerOpen, value);
    }

    public string DrawerTitle
    {
        get => _drawerTitle;
        set => SetProperty(ref _drawerTitle, value);
    }

    public string DrawerName
    {
        get => _drawerName;
        set => SetProperty(ref _drawerName, value);
    }

    public string DrawerDescription
    {
        get => _drawerDescription;
        set => SetProperty(ref _drawerDescription, value);
    }

    public string DrawerStatus
    {
        get => _drawerStatus;
        set => SetProperty(ref _drawerStatus, value);
    }

    public string DrawerContactPerson
    {
        get => _drawerContactPerson;
        set => SetProperty(ref _drawerContactPerson, value);
    }

    public string DrawerEmail
    {
        get => _drawerEmail;
        set => SetProperty(ref _drawerEmail, value);
    }

    public string DrawerPhone
    {
        get => _drawerPhone;
        set => SetProperty(ref _drawerPhone, value);
    }

    public string DrawerAddress
    {
        get => _drawerAddress;
        set => SetProperty(ref _drawerAddress, value);
    }

    public string DrawerPostalCode
    {
        get => _drawerPostalCode;
        set => SetProperty(ref _drawerPostalCode, value);
    }

    public string DrawerCity
    {
        get => _drawerCity;
        set => SetProperty(ref _drawerCity, value);
    }

    public string DrawerCountry
    {
        get => _drawerCountry;
        set => SetProperty(ref _drawerCountry, value);
    }

    public string PaginationText => $"Viser 1-{FilteredOrganizations.Count} af {_allOrganizations.Count} organisationer";

    // Commands
    public ICommand OpretOrganizationCommand { get; }
    public ICommand RedigerOrganizationCommand { get; }
    public ICommand CloseDrawerCommand { get; }
    public ICommand GemOrganizationCommand { get; }
    public ICommand GemOgOpretBrandCommand { get; }
    public ICommand SletOrganizationCommand { get; }

    public ICommand SelectTabCommand { get; }
    public ICommand OpretBrandCommand { get; }
    public ICommand GemBrandCommand { get; }
    public ICommand SletBrandCommand { get; }
    public ICommand OpenSurveysForBrandCommand { get; }

    public OrganizationsViewModel(IMockDataService data, MainShellViewModel shellVm)
    {
        _data = data;
        _shellVm = shellVm;

        LoadOrganizations();

        OpretOrganizationCommand = new RelayCommand(OpenCreateDrawer);
        RedigerOrganizationCommand = new RelayCommand(OpenEditDrawer);
        CloseDrawerCommand = new RelayCommand(CloseDrawer);
        GemOrganizationCommand = new RelayCommand(GemOrganization);
        GemOgOpretBrandCommand = new RelayCommand(GemOgOpretBrand);
        SletOrganizationCommand = new RelayCommand(async () => await SletOrganizationAsync());

        SelectTabCommand = new RelayCommand<string>(tab => { if (!string.IsNullOrEmpty(tab)) SelectedTab = tab; });
        OpretBrandCommand = new RelayCommand(async () => await OpretBrandAsync());
        GemBrandCommand = new RelayCommand(GemBrand);
        SletBrandCommand = new RelayCommand(async () => await SletBrandAsync());
        OpenSurveysForBrandCommand = new RelayCommand(OpenSurveysForBrand);
    }

    private void LoadOrganizations()
    {
        _allOrganizations.Clear();
        int? userId = _shellVm.LoggedInUser?.Id;
        _allOrganizations.AddRange(_data.GetOrganizations(userId));

        if (_shellVm.ActiveOrganization == null && _allOrganizations.Count > 0)
        {
            _shellVm.ActiveOrganization = _allOrganizations.First();
        }

        ApplyFilter();

        SelectedOrganization = FilteredOrganizations.FirstOrDefault(o => o.Id == _shellVm.ActiveOrganization?.Id)
                                ?? FilteredOrganizations.FirstOrDefault();
    }

    private void ApplyFilter()
    {
        FilteredOrganizations.Clear();
        var query = _allOrganizations.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            query = query.Where(o => o.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                     o.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                     o.ContactPerson.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(SelectedStatusFilter) && SelectedStatusFilter != "Status: Alle")
        {
            query = query.Where(o => o.Status.Equals(SelectedStatusFilter, StringComparison.OrdinalIgnoreCase));
        }

        foreach (var item in query)
        {
            FilteredOrganizations.Add(item);
        }

        Raise(nameof(PaginationText));
    }

    private void LoadBrands(int organizationId)
    {
        Brands.Clear();
        var list = _data.GetBrands(organizationId);
        foreach (var item in list)
        {
            Brands.Add(item);
        }
        SelectedBrand = Brands.FirstOrDefault();
    }

    private void OpenCreateDrawer()
    {
        _isEditingExistingOrg = false;
        DrawerTitle = "Opret organisation";

        DrawerName = string.Empty;
        DrawerDescription = string.Empty;
        DrawerStatus = "Aktiv";
        DrawerContactPerson = string.Empty;
        DrawerEmail = string.Empty;
        DrawerPhone = string.Empty;
        DrawerAddress = string.Empty;
        DrawerPostalCode = string.Empty;
        DrawerCity = string.Empty;
        DrawerCountry = "Danmark";

        IsDrawerOpen = true;
    }

    private void OpenEditDrawer()
    {
        if (SelectedOrganization == null) return;

        _isEditingExistingOrg = true;
        DrawerTitle = "Rediger organisation";

        DrawerName = SelectedOrganization.Name;
        DrawerDescription = SelectedOrganization.Description;
        DrawerStatus = SelectedOrganization.Status;
        DrawerContactPerson = SelectedOrganization.ContactPerson;
        DrawerEmail = SelectedOrganization.Email;
        DrawerPhone = SelectedOrganization.Phone;
        DrawerAddress = SelectedOrganization.Address;
        DrawerPostalCode = SelectedOrganization.PostalCode;
        DrawerCity = SelectedOrganization.City;
        DrawerCountry = SelectedOrganization.Country;

        IsDrawerOpen = true;
    }

    private void CloseDrawer()
    {
        IsDrawerOpen = false;
    }

    private void GemOrganization()
    {
        if (string.IsNullOrWhiteSpace(DrawerName)) return;

        if (_isEditingExistingOrg && SelectedOrganization != null)
        {
            SelectedOrganization.Name = DrawerName.Trim();
            SelectedOrganization.Description = DrawerDescription.Trim();
            SelectedOrganization.Status = DrawerStatus;
            SelectedOrganization.ContactPerson = DrawerContactPerson.Trim();
            SelectedOrganization.Email = DrawerEmail.Trim();
            SelectedOrganization.Phone = DrawerPhone.Trim();
            SelectedOrganization.Address = DrawerAddress.Trim();
            SelectedOrganization.PostalCode = DrawerPostalCode.Trim();
            SelectedOrganization.City = DrawerCity.Trim();
            SelectedOrganization.Country = DrawerCountry;
            SelectedOrganization.UpdatedAt = DateTime.Now;

            _data.SaveOrganization(SelectedOrganization);
            _shellVm.NotifyActiveOrganizationChanged();
        }
        else
        {
            var newOrg = new OrganizationModel
            {
                Name = DrawerName.Trim(),
                Description = DrawerDescription.Trim(),
                Status = DrawerStatus,
                ContactPerson = DrawerContactPerson.Trim(),
                Email = DrawerEmail.Trim(),
                Phone = DrawerPhone.Trim(),
                Address = DrawerAddress.Trim(),
                PostalCode = DrawerPostalCode.Trim(),
                City = DrawerCity.Trim(),
                Country = DrawerCountry,
                BrandCount = 0,
                SurveyCount = 0,
                UserCount = 1,
                UpdatedAt = DateTime.Now
            };

            _data.CreateOrganization(newOrg, _shellVm.LoggedInUser?.Id);
            _allOrganizations.Insert(0, newOrg);
            ApplyFilter();
            SelectedOrganization = newOrg;
        }

        IsDrawerOpen = false;
    }

    private void GemOgOpretBrand()
    {
        GemOrganization();
        _ = OpretBrandAsync();
    }

    private async Task SletOrganizationAsync()
    {
        if (SelectedOrganization == null) return;

        bool confirm = await Application.Current!.MainPage!.DisplayAlert(
            "Slet organisation",
            $"Er du sikker på, at du vil slette organisationen '{SelectedOrganization.Name}' og alle tilhørende data?",
            "Slet", "Annuller");

        if (confirm)
        {
            _data.DeleteOrganization(SelectedOrganization.Id);
            _allOrganizations.Remove(SelectedOrganization);
            ApplyFilter();
            SelectedOrganization = FilteredOrganizations.FirstOrDefault();
        }
    }

    private async Task OpretBrandAsync()
    {
        if (SelectedOrganization == null) return;

        var name = await Application.Current!.MainPage!.DisplayPromptAsync(
            "Opret brand", "Indtast brandets navn:", "Gem", "Annuller", "Brand navn");

        if (!string.IsNullOrWhiteSpace(name))
        {
            var brand = new BrandModel
            {
                Name = name.Trim(),
                Description = "Kort beskrivelse af brand",
                LogoUrl = "🏬",
                Status = "Aktiv",
                SurveyCount = 0,
                OrganizationId = SelectedOrganization.Id,
                UpdatedAt = DateTime.Now
            };

            _data.CreateBrand(brand);
            Brands.Add(brand);
            SelectedOrganization.BrandCount = Brands.Count;
            SelectedBrand = brand;
            Raise(nameof(BrandTabSubtitle));
        }
    }

    private void GemBrand()
    {
        if (SelectedBrand != null)
        {
            _data.SaveBrand(SelectedBrand);
        }
    }

    private async Task SletBrandAsync()
    {
        if (SelectedBrand == null || SelectedOrganization == null) return;

        bool confirm = await Application.Current!.MainPage!.DisplayAlert(
            "Slet brand",
            $"Er du sikker på, at du vil slette brandet '{SelectedBrand.Name}'?",
            "Slet", "Annuller");

        if (confirm)
        {
            _data.DeleteBrand(SelectedBrand.Id);
            Brands.Remove(SelectedBrand);
            SelectedOrganization.BrandCount = Brands.Count;
            SelectedBrand = Brands.FirstOrDefault();
            Raise(nameof(BrandTabSubtitle));
        }
    }

    private void OpenSurveysForBrand()
    {
        if (SelectedBrand == null || SelectedOrganization == null) return;

        _shellVm.ActiveOrganization = SelectedOrganization;
        _shellVm.ActiveBrand = SelectedBrand;
        _shellVm.RequestNavigation("Surveys");
    }
}


