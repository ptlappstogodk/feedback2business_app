namespace Feedback2Business.Models;

public class SurveyModel
{
    public string Name { get; set; } = string.Empty;
    public int Version { get; set; }
    public int QuestionCount { get; set; }

    public string VersionText => $"Version {Version} · {QuestionCount} spørgsmål";
}


