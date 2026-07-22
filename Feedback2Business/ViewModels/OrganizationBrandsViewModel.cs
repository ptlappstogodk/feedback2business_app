using System.Collections.ObjectModel;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class OrganizationBrandsViewModel : ObservableObject
{
    public ObservableCollection<BrandModel> Brands { get; } = new();
    public ObservableCollection<SurveyModel> Surveys { get; } = new();
    public ObservableCollection<SurveyQuestionModel> Section1Questions { get; } = new();
    public ObservableCollection<SurveyQuestionModel> Section2Questions { get; } = new();
    public ObservableCollection<SurveyQuestionModel> Section3Questions { get; } = new();

    public MobilePreviewModel Preview { get; }
    public SurveyQuestionEditorViewModel SelectedQuestion { get; }

    public OrganizationBrandsViewModel(IMockDataService data)
    {
        foreach (var item in data.GetBrands()) Brands.Add(item);
        foreach (var item in data.GetSurveys()) Surveys.Add(item);
        foreach (var item in data.GetSection1Questions()) Section1Questions.Add(item);
        foreach (var item in data.GetSection2Questions()) Section2Questions.Add(item);
        foreach (var item in data.GetSection3Questions()) Section3Questions.Add(item);

        Preview = data.GetPreview();

        var source = Section2Questions.FirstOrDefault() ?? new SurveyQuestionModel();
        SelectedQuestion = new SurveyQuestionEditorViewModel(source);
    }
}


