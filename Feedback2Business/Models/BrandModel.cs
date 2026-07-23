namespace Feedback2Business.Models;

public class BrandModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SurveyCount { get; set; }
    public int OrganizationId { get; set; }
    public string SurveyCountText => $"{SurveyCount} surveys";
}


