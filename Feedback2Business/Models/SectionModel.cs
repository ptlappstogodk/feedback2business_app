using System.Collections.ObjectModel;
using Feedback2Business.ViewModels;

namespace Feedback2Business.Models;

public class SectionModel : ObservableObject
{
    private string _title = string.Empty;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public ObservableCollection<SurveyQuestionModel> Questions { get; } = new();
}
