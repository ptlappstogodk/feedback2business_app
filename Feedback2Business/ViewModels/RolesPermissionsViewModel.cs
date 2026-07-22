using System.Collections.ObjectModel;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class RolesPermissionsViewModel : ObservableObject
{
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

    public RolesPermissionsViewModel(IMockDataService data)
    {
        foreach (var role in data.GetRoles())
            Roles.Add(role);

        SelectedRole = Roles.FirstOrDefault() ?? new RoleModel();
    }
}


