namespace Feedback2Business.Models;

public class SurveyModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Version { get; set; }
    public int QuestionCount { get; set; }
    public int BrandId { get; set; }

    public string VersionText => $"Version {Version} · {QuestionCount} spørgsmål";
}


