using System;
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
    private string _searchText = string.Empty;
    private OrganizationModel? _selectedOrganization;
    private BrandModel? _selectedBrand;

    // Organization edit properties
    private string _orgNameBuffer = string.Empty;
    private bool _isEditingOrganization;

    public string OrgNameBuffer
    {
        get => _orgNameBuffer;
        set => SetProperty(ref _orgNameBuffer, value);
    }

    public bool IsEditingOrganization
    {
        get => _isEditingOrganization;
        set => SetProperty(ref _isEditingOrganization, value);
    }

    // Brand edit/create properties
    private string _brandNameBuffer = string.Empty;
    private string _brandDescriptionBuffer = string.Empty;
    private string _brandLogoBuffer = "🏬";
    private bool _isEditingBrand;
    private bool _isCreatingBrand;

    public string BrandNameBuffer
    {
        get => _brandNameBuffer;
        set => SetProperty(ref _brandNameBuffer, value);
    }

    public string BrandDescriptionBuffer
    {
        get => _brandDescriptionBuffer;
        set => SetProperty(ref _brandDescriptionBuffer, value);
    }

    public string BrandLogoBuffer
    {
        get => _brandLogoBuffer;
        set => SetProperty(ref _brandLogoBuffer, value);
    }

    public bool IsEditingBrand
    {
        get => _isEditingBrand;
        set => SetProperty(ref _isEditingBrand, value);
    }

    public bool IsNotCreatingBrand => !IsCreatingBrand;

    public bool IsCreatingBrand
    {
        get => _isCreatingBrand;
        set
        {
            if (SetProperty(ref _isCreatingBrand, value))
            {
                Raise(nameof(IsNotCreatingBrand));
            }
        }
    }

    public ObservableCollection<string> BrandLogoOptions { get; } = new()
    {
        "🏬", "☕", "⚡", "🍔", "⭐", "🛒", "🏷️", "🏢"
    };

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public ObservableCollection<OrganizationModel> Organizations { get; } = new();
    public ObservableCollection<BrandModel> Brands { get; } = new();

    public OrganizationModel? SelectedOrganization
    {
        get => _selectedOrganization;
        set
        {
            if (SetProperty(ref _selectedOrganization, value))
            {
                _shellVm.ActiveOrganization = value;
                IsEditingOrganization = false;
                IsEditingBrand = false;
                IsCreatingBrand = false;

                if (value != null)
                {
                    OrgNameBuffer = value.Name;
                    LoadBrands(value.Id);
                }
                else
                {
                    OrgNameBuffer = string.Empty;
                    Brands.Clear();
                }
            }
        }
    }

    public bool IsBrandSelected => SelectedBrand != null;
    public bool HasNoBrandSelected => SelectedBrand == null;

    public BrandModel? SelectedBrand
    {
        get => _selectedBrand;
        set
        {
            if (SetProperty(ref _selectedBrand, value))
            {
                IsCreatingBrand = false;
                Raise(nameof(IsBrandSelected));
                Raise(nameof(HasNoBrandSelected));
                if (value != null)
                {
                    BrandNameBuffer = value.Name;
                    BrandDescriptionBuffer = value.Description;
                    BrandLogoBuffer = string.IsNullOrEmpty(value.LogoUrl) ? "🏬" : value.LogoUrl;
                    IsEditingBrand = true;
                }
                else
                {
                    BrandNameBuffer = string.Empty;
                    BrandDescriptionBuffer = string.Empty;
                    BrandLogoBuffer = "🏬";
                    IsEditingBrand = false;
                }
            }
        }
    }

    // Commands
    public ICommand OpretOrganizationCommand { get; }
    public ICommand RedigerOrganizationCommand { get; }
    public ICommand GemOrganizationCommand { get; }
    public ICommand SletOrganizationCommand { get; }

    public ICommand OpretBrandCommand { get; }
    public ICommand GemBrandCommand { get; }
    public ICommand AnnullerBrandCommand { get; }
    public ICommand SletBrandCommand { get; }
    public ICommand OpenSurveysForBrandCommand { get; }

    public OrganizationsViewModel(IMockDataService data, MainShellViewModel shellVm)
    {
        _data = data;
        _shellVm = shellVm;

        LoadOrganizations();

        OpretOrganizationCommand = new RelayCommand(async () => await OpretOrganizationAsync());
        RedigerOrganizationCommand = new RelayCommand(() => IsEditingOrganization = !IsEditingOrganization);
        GemOrganizationCommand = new RelayCommand(GemOrganization);
        SletOrganizationCommand = new RelayCommand(async () => await SletOrganizationAsync());

        OpretBrandCommand = new RelayCommand(OpretBrand);
        GemBrandCommand = new RelayCommand(GemBrand);
        AnnullerBrandCommand = new RelayCommand(AnnullerBrand);
        SletBrandCommand = new RelayCommand(async () => await SletBrandAsync());
        OpenSurveysForBrandCommand = new RelayCommand(OpenSurveysForBrand);
    }

    private void LoadOrganizations()
    {
        Organizations.Clear();
        int? userId = _shellVm.LoggedInUser?.Id;
        foreach (var item in _data.GetOrganizations(userId))
        {
            Organizations.Add(item);
        }

        if (_shellVm.ActiveOrganization == null && Organizations.Count > 0)
        {
            _shellVm.ActiveOrganization = Organizations.First();
        }

        SelectedOrganization = Organizations.FirstOrDefault(o => o.Id == _shellVm.ActiveOrganization?.Id) ?? Organizations.FirstOrDefault();
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
                UserCount = 1,
                UpdatedAt = DateTime.Now
            };

            _data.CreateOrganization(org, _shellVm.LoggedInUser?.Id);
            Organizations.Add(org);
            SelectedOrganization = org;
        }
    }

    private void GemOrganization()
    {
        if (SelectedOrganization == null) return;
        if (string.IsNullOrWhiteSpace(OrgNameBuffer)) return;

        SelectedOrganization.Name = OrgNameBuffer.Trim();
        _data.SaveOrganization(SelectedOrganization);
        IsEditingOrganization = false;
        _shellVm.NotifyActiveOrganizationChanged();
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
            Organizations.Remove(SelectedOrganization);
            SelectedOrganization = Organizations.FirstOrDefault();
        }
    }

    private void OpretBrand()
    {
        if (SelectedOrganization == null) return;

        BrandNameBuffer = string.Empty;
        BrandDescriptionBuffer = string.Empty;
        BrandLogoBuffer = "🏬";
        IsEditingBrand = false;
        IsCreatingBrand = true;
    }

    private void GemBrand()
    {
        if (SelectedOrganization == null) return;
        if (string.IsNullOrWhiteSpace(BrandNameBuffer)) return;

        if (IsCreatingBrand)
        {
            var brand = new BrandModel
            {
                Name = BrandNameBuffer.Trim(),
                Description = BrandDescriptionBuffer?.Trim() ?? string.Empty,
                LogoUrl = BrandLogoBuffer,
                SurveyCount = 0,
                OrganizationId = SelectedOrganization.Id
            };

            _data.CreateBrand(brand);
            Brands.Add(brand);
            SelectedOrganization.BrandCount = Brands.Count;
            IsCreatingBrand = false;
            SelectedBrand = brand;
        }
        else if (SelectedBrand != null)
        {
            SelectedBrand.Name = BrandNameBuffer.Trim();
            SelectedBrand.Description = BrandDescriptionBuffer?.Trim() ?? string.Empty;
            SelectedBrand.LogoUrl = BrandLogoBuffer;

            _data.SaveBrand(SelectedBrand);
            IsEditingBrand = true;
        }
    }

    private void AnnullerBrand()
    {
        IsCreatingBrand = false;
        SelectedBrand = Brands.FirstOrDefault();
    }

    private async Task SletBrandAsync()
    {
        if (SelectedBrand == null || SelectedOrganization == null) return;

        bool confirm = await Application.Current!.MainPage!.DisplayAlert(
            "Slet brand",
            $"Er du sikker på, at du vil slette brandet '{SelectedBrand.Name}' og alle dets data?",
            "Slet", "Annuller");

        if (confirm)
        {
            _data.DeleteBrand(SelectedBrand.Id);
            Brands.Remove(SelectedBrand);
            SelectedOrganization.BrandCount = Brands.Count;
            SelectedBrand = Brands.FirstOrDefault();
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


