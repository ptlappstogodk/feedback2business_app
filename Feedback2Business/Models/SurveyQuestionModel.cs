using Feedback2Business.ViewModels;

namespace Feedback2Business.Models;

public class SurveyQuestionModel : ObservableObject
{
    private string _numberLabel = string.Empty;
    private string _title = string.Empty;
    private string _description = string.Empty;
    private string _type = string.Empty;
    private bool _isRequired;
    private string _variableName = string.Empty;
    private string _displayMode = "Standard";

    public string NumberLabel
    {
        get => _numberLabel;
        set
        {
            if (SetProperty(ref _numberLabel, value))
            {
                Raise(nameof(DisplayTitle));
            }
        }
    }

    public string Title
    {
        get => _title;
        set
        {
            if (SetProperty(ref _title, value))
            {
                Raise(nameof(DisplayTitle));
            }
        }
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public string Type
    {
        get => _type;
        set
        {
            if (SetProperty(ref _type, value))
            {
                Raise(nameof(TypeLabel));
            }
        }
    }

    public bool IsRequired
    {
        get => _isRequired;
        set => SetProperty(ref _isRequired, value);
    }

    public string VariableName
    {
        get => _variableName;
        set => SetProperty(ref _variableName, value);
    }

    public string DisplayMode
    {
        get => _displayMode;
        set => SetProperty(ref _displayMode, value);
    }

    public string DisplayTitle => $"{NumberLabel}    {Title}";
    public string TypeLabel => Type;
}


