using Feedback2Business.ViewModels;

namespace Feedback2Business.Models;

public class OrganizationModel : ObservableObject
{
    private int _id;
    private string _name = string.Empty;
    private int _brandCount;
    private int _surveyCount;
    private int _userCount;
    private DateTime _updatedAt;

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

    public int BrandCount
    {
        get => _brandCount;
        set => SetProperty(ref _brandCount, value);
    }

    public int SurveyCount
    {
        get => _surveyCount;
        set => SetProperty(ref _surveyCount, value);
    }

    public int UserCount
    {
        get => _userCount;
        set => SetProperty(ref _userCount, value);
    }

    public DateTime UpdatedAt
    {
        get => _updatedAt;
        set
        {
            if (SetProperty(ref _updatedAt, value))
            {
                Raise(nameof(UpdatedText));
            }
        }
    }

    public string UpdatedText => UpdatedAt.ToString("dd. MMM yyyy");
}


