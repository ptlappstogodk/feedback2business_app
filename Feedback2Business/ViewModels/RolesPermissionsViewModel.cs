using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class RolesPermissionsViewModel : ObservableObject
{
    private readonly IMockDataService _data;
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

    public RolesPermissionsViewModel(IMockDataService data)
    {
        _data = data;
        foreach (var role in data.GetRoles())
            Roles.Add(role);

        SelectedRole = Roles.FirstOrDefault() ?? new RoleModel();

        OpretRoleCommand = new RelayCommand(async () => await OpretRoleAsync());
    }

    private async Task OpretRoleAsync()
    {
        var name = await Shell.Current.DisplayPromptAsync("Opret rolle", "Indtast rollenavn:", "Næste", "Annuller", "Navn");
        if (string.IsNullOrWhiteSpace(name)) return;

        var description = await Shell.Current.DisplayPromptAsync("Opret rolle", "Indtast beskrivelse:", "Gem", "Annuller", "Beskrivelse");
        if (description == null) return;

        var role = new RoleModel
        {
            Name = name.Trim(),
            Description = description.Trim(),
            PermissionGroups = new List<PermissionGroupModel>()
        };

        _data.CreateRole(role);
        Roles.Add(role);
        SelectedRole = role;
    }
}


