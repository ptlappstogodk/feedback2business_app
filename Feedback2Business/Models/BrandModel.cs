using Feedback2Business.ViewModels;

namespace Feedback2Business.Models;

public class BrandModel : ObservableObject
{
    private int _id;
    private string _name = string.Empty;
    private string _description = string.Empty;
    private string _logoUrl = "🏬";
    private int _surveyCount;
    private int _organizationId;

    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public string LogoUrl
    {
        get => _logoUrl;
        set => SetProperty(ref _logoUrl, value);
    }

    public int SurveyCount
    {
        get => _surveyCount;
        set
        {
            if (SetProperty(ref _surveyCount, value))
            {
                Raise(nameof(SurveyCountText));
            }
        }
    }

    public int OrganizationId
    {
        get => _organizationId;
        set => SetProperty(ref _organizationId, value);
    }

    public string SurveyCountText => $"{SurveyCount} surveys";
}


