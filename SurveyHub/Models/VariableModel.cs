namespace SurveyHub.Models;

public class VariableModel
{
    public string Name { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string DefaultValue { get; set; } = string.Empty;
    public int UsageCount { get; set; }

    public string UsageCountText => $"{UsageCount} surveys";
}


