using Feedback2Business.ViewModels;

namespace Feedback2Business.Models;

public class SurveyModel : ObservableObject
{
    private int _id;
    private string _name = string.Empty;
    private int _version = 1;
    private int _questionCount;
    private int _brandId;
    private string _type = "Inspektion";
    private string _description = string.Empty;
    private string _icon = "📋";
    private string _selectedTemplateName = "Blank survey";

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

    public int Version
    {
        get => _version;
        set
        {
            if (SetProperty(ref _version, value))
            {
                Raise(nameof(VersionText));
            }
        }
    }

    public int QuestionCount
    {
        get => _questionCount;
        set
        {
            if (SetProperty(ref _questionCount, value))
            {
                Raise(nameof(VersionText));
            }
        }
    }

    public int BrandId
    {
        get => _brandId;
        set => SetProperty(ref _brandId, value);
    }

    public string Type
    {
        get => _type;
        set => SetProperty(ref _type, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public string Icon
    {
        get => _icon;
        set => SetProperty(ref _icon, value);
    }

    public string SelectedTemplateName
    {
        get => _selectedTemplateName;
        set => SetProperty(ref _selectedTemplateName, value);
    }

    public string VersionText => $"Version {Version} · {QuestionCount} spørgsmål";
}


