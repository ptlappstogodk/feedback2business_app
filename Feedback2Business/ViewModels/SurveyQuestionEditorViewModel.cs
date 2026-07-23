using System.Collections.ObjectModel;
using Feedback2Business.Models;

namespace Feedback2Business.ViewModels;

public class SurveyQuestionEditorViewModel : ObservableObject
{
    private readonly SurveyQuestionModel _source;
    private string _selectedQuestionType = "Ja / Nej";
    private string _title = string.Empty;
    private string _description = string.Empty;
    private bool _isRequired;
    private string _variableName = string.Empty;
    private bool _isStandardDisplay = true;
    private bool _isCompactDisplay;

    public SurveyQuestionModel SourceQuestion => _source;

    public ObservableCollection<string> QuestionTypes { get; } = new()
    {
        "Ja / Nej",
        "Skala 1-5",
        "Billede",
        "Tekst"
    };

    public string SelectedQuestionType
    {
        get => _selectedQuestionType;
        set
        {
            if (SetProperty(ref _selectedQuestionType, value))
            {
                _source.Type = value;
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
                _source.Title = value;
            }
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            if (SetProperty(ref _description, value))
            {
                _source.Description = value;
            }
        }
    }

    public bool IsRequired
    {
        get => _isRequired;
        set
        {
            if (SetProperty(ref _isRequired, value))
            {
                _source.IsRequired = value;
            }
        }
    }

    public string VariableName
    {
        get => _variableName;
        set
        {
            if (SetProperty(ref _variableName, value))
            {
                _source.VariableName = value;
            }
        }
    }

    public bool IsStandardDisplay
    {
        get => _isStandardDisplay;
        set
        {
            if (SetProperty(ref _isStandardDisplay, value))
            {
                if (value)
                {
                    IsCompactDisplay = false;
                    _source.DisplayMode = "Standard";
                }
            }
        }
    }

    public bool IsCompactDisplay
    {
        get => _isCompactDisplay;
        set
        {
            if (SetProperty(ref _isCompactDisplay, value))
            {
                if (value)
                {
                    IsStandardDisplay = false;
                    _source.DisplayMode = "Kompakt";
                }
            }
        }
    }

    public SurveyQuestionEditorViewModel()
    {
        _source = new SurveyQuestionModel();
    }

    public SurveyQuestionEditorViewModel(SurveyQuestionModel source)
    {
        _source = source;
        _selectedQuestionType = source.Type;
        _title = source.Title;
        _description = source.Description;
        _isRequired = source.IsRequired;
        _variableName = source.VariableName;
        _isStandardDisplay = source.DisplayMode == "Standard";
        _isCompactDisplay = source.DisplayMode == "Kompakt";
    }
}


