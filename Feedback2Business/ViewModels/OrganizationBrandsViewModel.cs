using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Linq;
using Feedback2Business.Models;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class OrganizationBrandsViewModel : ObservableObject
{
    private readonly IMockDataService _data;
    
    private BrandModel? _selectedBrand;
    private SurveyModel? _selectedSurvey;
    private SurveyQuestionEditorViewModel _selectedQuestion;
    private string _activeSurveyTab = "Byg";

    public MainShellViewModel ShellVm { get; }

    public ObservableCollection<BrandModel> Brands { get; } = new();
    public ObservableCollection<SurveyModel> Surveys { get; } = new();
    public ObservableCollection<SectionModel> Sections { get; } = new();

    public MobilePreviewModel Preview { get; }

    public BrandModel? SelectedBrand
    {
        get => _selectedBrand;
        set
        {
            if (SetProperty(ref _selectedBrand, value))
            {
                OnBrandSelected(value);
            }
        }
    }

    public SurveyModel? SelectedSurvey
    {
        get => _selectedSurvey;
        set
        {
            if (SetProperty(ref _selectedSurvey, value))
            {
                OnSurveySelected(value);
            }
        }
    }

    public SurveyQuestionEditorViewModel SelectedQuestion
    {
        get => _selectedQuestion;
        set => SetProperty(ref _selectedQuestion, value);
    }

    public string ActiveSurveyTab
    {
        get => _activeSurveyTab;
        set
        {
            if (SetProperty(ref _activeSurveyTab, value))
            {
                Raise(nameof(IsBygTabActive));
                Raise(nameof(IsLogikTabActive));
                Raise(nameof(IsIndstillingerTabActive));
                Raise(nameof(IsOversaettelserTabActive));
                Raise(nameof(IsVersionerTabActive));
            }
        }
    }

    public bool IsBygTabActive => ActiveSurveyTab == "Byg";
    public bool IsLogikTabActive => ActiveSurveyTab == "Logik";
    public bool IsIndstillingerTabActive => ActiveSurveyTab == "Indstillinger";
    public bool IsOversaettelserTabActive => ActiveSurveyTab == "Oversættelser";
    public bool IsVersionerTabActive => ActiveSurveyTab == "Versioner";

    public string SelectedBrandTitle => SelectedBrand != null ? $"Surveys for {SelectedBrand.Name}" : "Surveys";

    public ICommand OpretBrandCommand { get; }
    public ICommand OpretSurveyCommand { get; }
    public ICommand FlereHandlingerCommand { get; }
    public ICommand SelectTabCommand { get; }
    public ICommand SelectQuestionCommand { get; }
    public ICommand SletSpoergsmaalCommand { get; }
    
    // Sektioner & Spørgsmål administration
    public ICommand TilfoejSektionCommand { get; }
    public ICommand AdministrerSektionerCommand { get; }
    public ICommand TilfoejSpoergsmaalCommand { get; }

    public OrganizationBrandsViewModel(IMockDataService data, MainShellViewModel shellVm)
    {
        _data = data;
        ShellVm = shellVm;

        Preview = data.GetPreview();

        var allBrands = _data.GetBrands(ShellVm.ActiveOrganization?.Id);
        foreach (var b in allBrands) Brands.Add(b);

        SelectedBrand = Brands.FirstOrDefault();

        _selectedQuestion = new SurveyQuestionEditorViewModel(new SurveyQuestionModel());

        OpretBrandCommand = new RelayCommand(async () => await OpretBrandAsync());
        OpretSurveyCommand = new RelayCommand(async () => await OpretSurveyAsync());
        FlereHandlingerCommand = new RelayCommand(async () => await FlereHandlingerAsync());
        SelectTabCommand = new RelayCommand<string>(tab => ActiveSurveyTab = tab ?? "Byg");
        SelectQuestionCommand = new RelayCommand<SurveyQuestionModel>(SelectQuestion);
        SletSpoergsmaalCommand = new RelayCommand<SurveyQuestionEditorViewModel>(SletSpoergsmaal);
        
        TilfoejSektionCommand = new RelayCommand(async () => await TilfoejSektionAsync());
        AdministrerSektionerCommand = new RelayCommand(async () => await AdministrerSektionerAsync());
        TilfoejSpoergsmaalCommand = new RelayCommand(async () => await TilfoejSpoergsmaalAsync());
    }

    private void OnBrandSelected(BrandModel? brand)
    {
        Surveys.Clear();
        Raise(nameof(SelectedBrandTitle));

        if (brand == null)
        {
            SelectedSurvey = null;
            return;
        }

        var filteredSurveys = _data.GetSurveys(brand.Id);
        foreach (var s in filteredSurveys) Surveys.Add(s);
        SelectedSurvey = Surveys.FirstOrDefault();
    }

    private void OnSurveySelected(SurveyModel? survey)
    {
        Sections.Clear();
        if (survey == null)
        {
            SelectedQuestion = new SurveyQuestionEditorViewModel(new SurveyQuestionModel());
            return;
        }

        if (survey.Name == "Butiksinspektion")
        {
            var sec1 = new SectionModel { Title = "1. Butiksinformation" };
            foreach (var q in _data.GetSection1Questions()) sec1.Questions.Add(q);

            var sec2 = new SectionModel { Title = "2. Eksteriør" };
            foreach (var q in _data.GetSection2Questions()) sec2.Questions.Add(q);

            var sec3 = new SectionModel { Title = "3. Indretning & præsentation" };
            foreach (var q in _data.GetSection3Questions()) sec3.Questions.Add(q);

            Sections.Add(sec1);
            Sections.Add(sec2);
            Sections.Add(sec3);
        }
        else if (survey.Name == "HACCP Tjekliste")
        {
            var sec1 = new SectionModel { Title = "1. Temperaturmåling" };
            sec1.Questions.Add(new SurveyQuestionModel { NumberLabel = "1.1", Title = "Køleskab temperatur (C)", Type = "Tekst", Description = "Indtast temperaturen i køleskabet i Celsius.", IsRequired = true, VariableName = "temp_kole", DisplayMode = "Standard" });
            sec1.Questions.Add(new SurveyQuestionModel { NumberLabel = "1.2", Title = "Fryser temperatur (C)", Type = "Tekst", Description = "Indtast temperaturen i fryseren i Celsius.", IsRequired = true, VariableName = "temp_frys", DisplayMode = "Standard" });

            var sec2 = new SectionModel { Title = "2. Rengøring & hygiejne" };
            sec2.Questions.Add(new SurveyQuestionModel { NumberLabel = "2.1", Title = "Personlig hygiejne OK?", Type = "Ja / Nej", Description = "Tjek om alt personale har rent arbejdstøj og korrekt håndhygiejne.", IsRequired = true, VariableName = "pers_hyg", DisplayMode = "Standard" });
            sec2.Questions.Add(new SurveyQuestionModel { NumberLabel = "2.2", Title = "Rengøringsplan udfyldt?", Type = "Ja / Nej", VariableName = "reng_plan" });

            Sections.Add(sec1);
            Sections.Add(sec2);
        }
        else if (survey.Name == "Kampagneevaluering")
        {
            var sec1 = new SectionModel { Title = "1. Kampagnemateriale" };
            sec1.Questions.Add(new SurveyQuestionModel { NumberLabel = "1.1", Title = "Hovedskilt på plads ved indgang?", Type = "Ja / Nej", VariableName = "hovedskilt" });
            sec1.Questions.Add(new SurveyQuestionModel { NumberLabel = "1.2", Title = "Brochurer tilgængelige?", Type = "Ja / Nej", VariableName = "brochurer" });
            sec1.Questions.Add(new SurveyQuestionModel { NumberLabel = "1.3", Title = "Billede af udstilling", Type = "Billede", VariableName = "img_udstil" });

            Sections.Add(sec1);
        }
        else
        {
            var sec1 = new SectionModel { Title = "1. Ny Sektion" };
            sec1.Questions.Add(new SurveyQuestionModel { NumberLabel = "1.1", Title = "Nyt Spørgsmål", Type = "Ja / Nej" });
            Sections.Add(sec1);
        }

        var firstQuestion = Sections.SelectMany(s => s.Questions).FirstOrDefault();
        if (firstQuestion != null)
        {
            SelectedQuestion = new SurveyQuestionEditorViewModel(firstQuestion);
        }
        else
        {
            SelectedQuestion = new SurveyQuestionEditorViewModel(new SurveyQuestionModel());
        }
    }

    private void SelectQuestion(SurveyQuestionModel? question)
    {
        if (question != null)
        {
            SelectedQuestion = new SurveyQuestionEditorViewModel(question);
        }
    }

    private void SletSpoergsmaal(SurveyQuestionEditorViewModel? editorVm)
    {
        if (editorVm == null) return;
        var question = editorVm.SourceQuestion;

        foreach (var sec in Sections)
        {
            if (sec.Questions.Contains(question))
            {
                sec.Questions.Remove(question);

                var secNumStr = new string(sec.Title.TakeWhile(char.IsDigit).ToArray());
                int secNum = string.IsNullOrEmpty(secNumStr) ? 1 : int.Parse(secNumStr);
                for (int i = 0; i < sec.Questions.Count; i++)
                {
                    sec.Questions[i].NumberLabel = $"{secNum}.{i + 1}";
                }
                break;
            }
        }

        var nextQuestion = Sections.SelectMany(s => s.Questions).FirstOrDefault();
        if (nextQuestion != null)
        {
            SelectedQuestion = new SurveyQuestionEditorViewModel(nextQuestion);
        }
        else
        {
            SelectedQuestion = new SurveyQuestionEditorViewModel(new SurveyQuestionModel());
        }
    }

    private async Task TilfoejSektionAsync()
    {
        var title = await Application.Current!.MainPage!.DisplayPromptAsync("Ny sektion", "Indtast sektionsnavn:", "Gem", "Annuller", "Navn");
        if (!string.IsNullOrWhiteSpace(title))
        {
            int newNum = Sections.Count + 1;
            var newSection = new SectionModel { Title = $"{newNum}. {title.Trim()}" };
            Sections.Add(newSection);
        }
    }

    private async Task AdministrerSektionerAsync()
    {
        var action = await Application.Current!.MainPage!.DisplayActionSheet(
            "Administrer sektioner", "Annuller", null, "Tilføj ny sektion", "Slet eksisterende sektion");

        if (action == "Tilføj ny sektion")
        {
            await TilfoejSektionAsync();
        }
        else if (action == "Slet eksisterende sektion")
        {
            if (Sections.Count == 0)
            {
                await Application.Current!.MainPage!.DisplayAlert("Slet sektion", "Der er ingen sektioner at slette.", "OK");
                return;
            }

            var sectionTitles = Sections.Select(s => s.Title).ToArray();
            var deleteChoice = await Application.Current!.MainPage!.DisplayActionSheet(
                "Vælg sektion der skal slettes", "Annuller", null, sectionTitles);

            if (deleteChoice != null && deleteChoice != "Annuller")
            {
                var secToRemove = Sections.FirstOrDefault(s => s.Title == deleteChoice);
                if (secToRemove != null)
                {
                    Sections.Remove(secToRemove);
                    for (int i = 0; i < Sections.Count; i++)
                    {
                        var parts = Sections[i].Title.Split('.', 2);
                        string baseName = parts.Length == 2 ? parts[1].Trim() : Sections[i].Title;
                        Sections[i].Title = $"{i + 1}. {baseName}";

                        for (int q = 0; q < Sections[i].Questions.Count; q++)
                        {
                            Sections[i].Questions[q].NumberLabel = $"{i + 1}.{q + 1}";
                        }
                    }
                }
            }
        }
    }

    private async Task TilfoejSpoergsmaalAsync()
    {
        if (Sections.Count == 0)
        {
            await Application.Current!.MainPage!.DisplayAlert("Tilføj spørgsmål", "Opret venligst en sektion først.", "OK");
            return;
        }

        var sectionTitles = Sections.Select(s => s.Title).ToArray();
        var sectionChoice = await Application.Current!.MainPage!.DisplayActionSheet(
            "Tilføj spørgsmål til hvilken sektion?", "Annuller", null, sectionTitles);

        if (sectionChoice != null && sectionChoice != "Annuller")
        {
            var selectedSec = Sections.FirstOrDefault(s => s.Title == sectionChoice);
            if (selectedSec != null)
            {
                var title = await Application.Current!.MainPage!.DisplayPromptAsync("Nyt spørgsmål", "Indtast spørgsmålstekst:", "Opret", "Annuller", "Spørgsmålstekst");
                if (!string.IsNullOrWhiteSpace(title))
                {
                    var secNumStr = new string(selectedSec.Title.TakeWhile(char.IsDigit).ToArray());
                    int secNum = string.IsNullOrEmpty(secNumStr) ? 1 : int.Parse(secNumStr);
                    string numberLabel = $"{secNum}.{selectedSec.Questions.Count + 1}";

                    var newQuestion = new SurveyQuestionModel
                    {
                        NumberLabel = numberLabel,
                        Title = title.Trim(),
                        Type = "Ja / Nej",
                        Description = "",
                        IsRequired = false,
                        VariableName = "",
                        DisplayMode = "Standard"
                    };
                    selectedSec.Questions.Add(newQuestion);
                    SelectedQuestion = new SurveyQuestionEditorViewModel(newQuestion);
                }
            }
        }
    }

    private async Task OpretBrandAsync()
    {
        var name = await Application.Current!.MainPage!.DisplayPromptAsync("Opret brand", "Indtast brandnavn:", "Gem", "Annuller", "Navn");
        if (!string.IsNullOrWhiteSpace(name))
        {
            var brand = new BrandModel
            {
                Name = name.Trim(),
                SurveyCount = 0,
                OrganizationId = ShellVm.ActiveOrganization?.Id ?? 1
            };

            _data.CreateBrand(brand);
            Brands.Add(brand);
            SelectedBrand = brand;
        }
    }

    private async Task OpretSurveyAsync()
    {
        if (SelectedBrand == null)
        {
            await Application.Current!.MainPage!.DisplayAlert("Opret survey", "Vælg venligst et brand først.", "OK");
            return;
        }

        var name = await Application.Current!.MainPage!.DisplayPromptAsync("Opret survey", "Indtast surveynavn:", "Gem", "Annuller", "Navn");
        if (!string.IsNullOrWhiteSpace(name))
        {
            var survey = new SurveyModel
            {
                Name = name.Trim(),
                Version = 1,
                QuestionCount = 0,
                BrandId = SelectedBrand.Id
            };

            _data.CreateSurvey(survey);
            Surveys.Add(survey);
            SelectedSurvey = survey;
        }
    }

    private async Task FlereHandlingerAsync()
    {
        if (SelectedBrand == null)
        {
            await Application.Current!.MainPage!.DisplayAlert("Flere handlinger", "Vælg venligst et brand først.", "OK");
            return;
        }

        var action = await Application.Current!.MainPage!.DisplayActionSheet(
            $"Flere handlinger for {SelectedBrand.Name}",
            "Annuller",
            "Slet brand",
            null,
            "Eksporter data",
            "Dupliker brand");

        if (action == "Slet brand")
        {
            bool confirm = await Application.Current!.MainPage!.DisplayAlert(
                "Bekræft sletning",
                $"Er du sikker på, at du vil slette brandet '{SelectedBrand.Name}' og alle tilhørende surveys?",
                "Ja, slet",
                "Annuller");

            if (confirm)
            {
                Brands.Remove(SelectedBrand);
                SelectedBrand = Brands.FirstOrDefault();
            }
        }
    }
}


