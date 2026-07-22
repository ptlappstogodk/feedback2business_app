namespace SurveyHub.Models;

public class PermissionModel
{
    public string Label { get; set; } = string.Empty;
    public bool IsEnabled { get; set; }
}

public class PermissionGroupModel
{
    public string Name { get; set; } = string.Empty;
    public List<PermissionModel> Permissions { get; set; } = new();
}

public class RoleModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<PermissionGroupModel> PermissionGroups { get; set; } = new();
}


