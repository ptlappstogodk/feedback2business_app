namespace Feedback2Business.Models;

public class SurveyQuestionModel
{
    public string NumberLabel { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsRequired { get; set; }
    public string VariableName { get; set; } = string.Empty;
    public string DisplayMode { get; set; } = "Standard";

    public string DisplayTitle => $"{NumberLabel}    {Title}";
    public string TypeLabel => Type;
}


