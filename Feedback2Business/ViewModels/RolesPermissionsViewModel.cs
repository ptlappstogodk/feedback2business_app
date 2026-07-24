using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class RolesPermissionsViewModel : ObservableObject
{
    private readonly IMockDataService _data;
    private readonly MainShellViewModel _shellVm;
    private RoleModel _selectedRole = new();

    public ObservableCollection<RoleModel> Roles { get; } = new();

    public RoleModel SelectedRole
    {
        get => _selectedRole;
        set
        {
            if (SetProperty(ref _selectedRole, value))
                Raise(nameof(PermissionGroups));
        }
    }

    public List<PermissionGroupModel> PermissionGroups => SelectedRole.PermissionGroups;

    public ICommand OpretRoleCommand { get; }
    public ICommand SaveRoleCommand { get; }

    public RolesPermissionsViewModel(IMockDataService data, MainShellViewModel shellVm)
    {
        _data = data;
        _shellVm = shellVm;
        int? orgId = _shellVm.ActiveOrganization?.Id;
        foreach (var role in data.GetRoles(orgId))
            Roles.Add(role);

        SelectedRole = Roles.FirstOrDefault() ?? new RoleModel();

        OpretRoleCommand = new RelayCommand(async () => await OpretRoleAsync());
        SaveRoleCommand = new RelayCommand(async () => await SaveRoleAsync());
    }

    private async Task SaveRoleAsync()
    {
        if (SelectedRole != null && SelectedRole.Id > 0)
        {
            _data.SaveRole(SelectedRole);
            await Application.Current!.MainPage!.DisplayAlert("Rolle gemt", $"Rettighederne for '{SelectedRole.Name}' er blevet gemt succesfuldt.", "OK");
        }
    }

    private async Task OpretRoleAsync()
    {
        var name = await Application.Current!.MainPage!.DisplayPromptAsync("Opret rolle", "Indtast rollenavn:", "Næste", "Annuller", "Navn");
        if (string.IsNullOrWhiteSpace(name)) return;

        var description = await Application.Current!.MainPage!.DisplayPromptAsync("Opret rolle", "Indtast beskrivelse:", "Gem", "Annuller", "Beskrivelse");
        if (description == null) return;

        int? orgId = _shellVm.ActiveOrganization?.Id;
        var role = new RoleModel
        {
            Name = name.Trim(),
            Description = description.Trim(),
            PermissionGroups = new List<PermissionGroupModel>(),
            OrganizationId = orgId ?? 1
        };

        _data.CreateRole(role);
        Roles.Add(role);
        SelectedRole = role;
    }
}


