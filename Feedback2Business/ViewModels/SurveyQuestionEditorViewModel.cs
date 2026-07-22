using System.Collections.ObjectModel;
using Feedback2Business.Models;

namespace Feedback2Business.ViewModels;

public class SurveyQuestionEditorViewModel : ObservableObject
{
    private string _selectedQuestionType = "Ja / Nej";
    private string _title = string.Empty;
    private string _description = string.Empty;
    private bool _isRequired;
    private string _variableName = string.Empty;
    private bool _isStandardDisplay = true;
    private bool _isCompactDisplay;

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
        set => SetProperty(ref _selectedQuestionType, value);
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
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

    public bool IsStandardDisplay
    {
        get => _isStandardDisplay;
        set => SetProperty(ref _isStandardDisplay, value);
    }

    public bool IsCompactDisplay
    {
        get => _isCompactDisplay;
        set => SetProperty(ref _isCompactDisplay, value);
    }

    public SurveyQuestionEditorViewModel()
    {
    }

    public SurveyQuestionEditorViewModel(SurveyQuestionModel source)
    {
        SelectedQuestionType = source.Type;
        Title = source.Title;
        Description = source.Description;
        IsRequired = source.IsRequired;
        VariableName = source.VariableName;
        IsStandardDisplay = source.DisplayMode == "Standard";
        IsCompactDisplay = source.DisplayMode == "Kompakt";
    }
}


