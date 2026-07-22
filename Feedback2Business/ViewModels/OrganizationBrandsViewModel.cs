using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class OrganizationBrandsViewModel : ObservableObject
{
    private readonly IMockDataService _data;

    public ObservableCollection<BrandModel> Brands { get; } = new();
    public ObservableCollection<SurveyModel> Surveys { get; } = new();
    public ObservableCollection<SurveyQuestionModel> Section1Questions { get; } = new();
    public ObservableCollection<SurveyQuestionModel> Section2Questions { get; } = new();
    public ObservableCollection<SurveyQuestionModel> Section3Questions { get; } = new();

    public MobilePreviewModel Preview { get; }
    public SurveyQuestionEditorViewModel SelectedQuestion { get; }

    public ICommand OpretBrandCommand { get; }
    public ICommand OpretSurveyCommand { get; }

    public OrganizationBrandsViewModel(IMockDataService data)
    {
        _data = data;
        foreach (var item in data.GetBrands()) Brands.Add(item);
        foreach (var item in data.GetSurveys()) Surveys.Add(item);
        foreach (var item in data.GetSection1Questions()) Section1Questions.Add(item);
        foreach (var item in data.GetSection2Questions()) Section2Questions.Add(item);
        foreach (var item in data.GetSection3Questions()) Section3Questions.Add(item);

        Preview = data.GetPreview();

        var source = Section2Questions.FirstOrDefault() ?? new SurveyQuestionModel();
        SelectedQuestion = new SurveyQuestionEditorViewModel(source);

        OpretBrandCommand = new RelayCommand(async () => await OpretBrandAsync());
        OpretSurveyCommand = new RelayCommand(async () => await OpretSurveyAsync());
    }

    private async Task OpretBrandAsync()
    {
        var name = await Application.Current!.MainPage!.DisplayPromptAsync("Opret brand", "Indtast brandnavn:", "Gem", "Annuller", "Navn");
        if (!string.IsNullOrWhiteSpace(name))
        {
            var brand = new BrandModel
            {
                Name = name.Trim(),
                SurveyCount = 0
            };

            _data.CreateBrand(brand);
            Brands.Add(brand);
        }
    }

    private async Task OpretSurveyAsync()
    {
        var name = await Application.Current!.MainPage!.DisplayPromptAsync("Opret survey", "Indtast surveynavn:", "Gem", "Annuller", "Navn");
        if (!string.IsNullOrWhiteSpace(name))
        {
            var survey = new SurveyModel
            {
                Name = name.Trim(),
                Version = 1,
                QuestionCount = 0
            };

            // Using the template context to post survey
            _data.CreateSurvey(survey);
            Surveys.Add(survey);
        }
    }
}


