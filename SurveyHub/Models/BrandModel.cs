namespace SurveyHub.Models;

public class BrandModel
{
    public string Name { get; set; } = string.Empty;
    public int SurveyCount { get; set; }
    public string SurveyCountText => $"{SurveyCount} surveys";
}


