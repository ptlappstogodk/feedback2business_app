namespace Feedback2Business.Models;

public class VariableModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string DefaultValue { get; set; } = string.Empty;
    public int UsageCount { get; set; }
    public int OrganizationId { get; set; }

    public string UsageCountText => $"{UsageCount} surveys";
}


