namespace Feedback2Business.Models;

public class PermissionModel
{
    public int Id { get; set; }
    public int PermissionGroupId { get; set; }
    public string Label { get; set; } = string.Empty;
    public bool IsEnabled { get; set; }
}

public class PermissionGroupModel
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<PermissionModel> Permissions { get; set; } = new();
}

public class RoleModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<PermissionGroupModel> PermissionGroups { get; set; } = new();
    public int OrganizationId { get; set; }
}


