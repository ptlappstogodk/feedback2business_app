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

    private bool _isSurveyActive = true;
    public bool IsSurveyActive
    {
        get => _isSurveyActive;
        set => SetProperty(ref _isSurveyActive, value);
    }

    private string _surveyTargetAudience = "Alle butikker";
    public string SurveyTargetAudience
    {
        get => _surveyTargetAudience;
        set => SetProperty(ref _surveyTargetAudience, value);
    }

    public ObservableCollection<string> LogicRules { get; } = new();
    public ObservableCollection<TranslationItemModel> Translations { get; } = new();
    public ObservableCollection<VersionItemModel> Versions { get; } = new();

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
        LogicRules.Clear();
        Translations.Clear();
        Versions.Clear();

        if (survey == null)
        {
            SelectedQuestion = new SurveyQuestionEditorViewModel(new SurveyQuestionModel());
            return;
        }

        // 1. Fetch questions for the selected survey from the database!
        var dbQuestions = _data.GetQuestionsForSurvey(survey.Id);
        
        // Group questions by SectionIndex and build SectionModel dynamically!
        var grouped = dbQuestions.GroupBy(q => q.SectionIndex).OrderBy(g => g.Key);
        foreach (var group in grouped)
        {
            var firstQ = group.First();
            string sectionTitle = !string.IsNullOrWhiteSpace(firstQ.SectionTitle)
                ? $"{group.Key}. {firstQ.SectionTitle}"
                : $"{group.Key}. Sektion";

            var section = new SectionModel { Title = sectionTitle };
            foreach (var q in group)
            {
                section.Questions.Add(q);
            }
            Sections.Add(section);
        }

        // Fallback if no questions are found in the database
        if (Sections.Count == 0)
        {
            var sec1 = new SectionModel { Title = "1. Ny Sektion" };
            sec1.Questions.Add(new SurveyQuestionModel { NumberLabel = "1.1", Title = "Nyt Spørgsmål", Type = "Ja / Nej", SectionIndex = 1, SectionTitle = "Ny Sektion", SurveyId = survey.Id });
            Sections.Add(sec1);
        }

        // 2. Populate dynamic settings fields
        IsSurveyActive = true;
        SurveyTargetAudience = survey.Name == "Butiksinspektion" ? "Udvalgte testbutikker" : "Alle butikker";

        // 3. Populate dynamic logic rules
        if (survey.Name == "Butiksinspektion")
        {
            LogicRules.Add("Hvis facade_ren == Nej, vis spørgsmål 2.4 'Billede af facade'");
            LogicRules.Add("Hvis vinduer_rene == Nej, tilføj handling 'Rengør vinduer'");
        }
        else if (survey.Name == "HACCP Tjekliste")
        {
            LogicRules.Add("Hvis temp_kole > 5, vis advarsel 'Køleskab for varmt!'");
            LogicRules.Add("Hvis reng_plan == Nej, tilføj handling 'Gør rent'");
        }
        else if (survey.Name == "Kampagneevaluering")
        {
            LogicRules.Add("Hvis hovedskilt == Nej, vis advarsel 'Kampagne ikke synlig!'");
        }
        else
        {
            LogicRules.Add("Ingen logikregler defineret for denne survey.");
        }

        // 4. Populate dynamic translations
        var questions = Sections.SelectMany(s => s.Questions).ToList();
        foreach (var q in questions)
        {
            string eng = q.Title;
            if (q.Title == "Facade ren og vedligeholdt?") eng = "Is facade clean and maintained?";
            else if (q.Title == "Dato og tidspunkt") eng = "Date and time";
            else if (q.Title == "Butik") eng = "Store";
            else if (q.Title == "Inspektør") eng = "Inspector";
            else if (q.Title == "Skiltning intakt og synlig?") eng = "Is signage intact and visible?";
            else if (q.Title == "Vinduer rene") eng = "Are windows clean?";
            else if (q.Title == "Billede of facade") eng = "Image of facade";
            else if (q.Title == "Butikken fremstår ryddelig") eng = "Store appears tidy";
            else if (q.Title == "Produkter korrekt prissat") eng = "Products priced correctly";
            else if (q.Title == "Kampagnemateriale på plads") eng = "Campaign material in place";
            else if (q.Title == "Billede af kampagne") eng = "Image of campaign";
            else if (q.Title == "Køleskab temperatur (C)") eng = "Refrigerator temperature (C)";
            else if (q.Title == "Fryser temperatur (C)") eng = "Freezer temperature (C)";
            else if (q.Title == "Personlig hygiejne OK?") eng = "Personal hygiene OK?";
            else if (q.Title == "Rengøringsplan udfyldt?") eng = "Cleaning plan completed?";
            else if (q.Title == "Hovedskilt på plads ved indgang?") eng = "Main sign in place at entrance?";
            else if (q.Title == "Brochurer tilgængelige?") eng = "Brochures available?";
            else if (q.Title == "Billede af udstilling") eng = "Image of display";

            Translations.Add(new TranslationItemModel { Danish = q.Title, English = eng });
        }

        // 5. Populate dynamic versions
        Versions.Add(new VersionItemModel
        {
            Version = survey.Version,
            Author = "Anders Kirk",
            DateText = "I dag, 08:30",
            IsActive = true
        });

        if (survey.Version > 1)
        {
            Versions.Add(new VersionItemModel
            {
                Version = survey.Version - 1,
                Author = "Maria Jensen",
                DateText = "Gårsdagen, 14:15",
                IsActive = false
            });
        }
        if (survey.Version > 2)
        {
            Versions.Add(new VersionItemModel
            {
                Version = survey.Version - 2,
                Author = "Lars Petersen",
                DateText = "Sidste uge, 10:00",
                IsActive = false
            });
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

public class TranslationItemModel
{
    public string Danish { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
}

public class VersionItemModel
{
    public int Version { get; set; }
    public string Author { get; set; } = string.Empty;
    public string DateText { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool CanRestore => !IsActive;
}


