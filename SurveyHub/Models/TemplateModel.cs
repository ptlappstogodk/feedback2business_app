namespace SurveyHub.Models;

public class TemplateModel
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }

    public string UpdatedText => UpdatedAt.ToString("dd. MMM yyyy");
}


