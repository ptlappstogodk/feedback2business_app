namespace Feedback2Business.Models;

public class OrganizationModel
{
    public string Name { get; set; } = string.Empty;
    public int BrandCount { get; set; }
    public int SurveyCount { get; set; }
    public int UserCount { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string UpdatedText => UpdatedAt.ToString("dd. MMM yyyy");
}


