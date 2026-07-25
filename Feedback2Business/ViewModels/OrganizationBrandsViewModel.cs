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

    // Survey Generelt Buffers
    private string _surveyNameBuffer = string.Empty;
    private string _surveyTypeBuffer = "Inspektion";
    private string _surveyDescriptionBuffer = string.Empty;
    private string _surveyIconBuffer = "📋";
    private string _selectedTemplateNameBuffer = "Blank survey";
    private bool _isCreatingNewSurvey;

    public string SurveyNameBuffer
    {
        get => _surveyNameBuffer;
        set => SetProperty(ref _surveyNameBuffer, value);
    }

    public string SurveyTypeBuffer
    {
        get => _surveyTypeBuffer;
        set => SetProperty(ref _surveyTypeBuffer, value);
    }

    public string SurveyDescriptionBuffer
    {
        get => _surveyDescriptionBuffer;
        set => SetProperty(ref _surveyDescriptionBuffer, value);
    }

    public string SurveyIconBuffer
    {
        get => _surveyIconBuffer;
        set => SetProperty(ref _surveyIconBuffer, value);
    }

    public string SelectedTemplateNameBuffer
    {
        get => _selectedTemplateNameBuffer;
        set => SetProperty(ref _selectedTemplateNameBuffer, value);
    }

    public bool IsCreatingNewSurvey
    {
        get => _isCreatingNewSurvey;
        set => SetProperty(ref _isCreatingNewSurvey, value);
    }

    public ObservableCollection<string> SurveyTypes { get; } = new()
    {
        "Inspektion",
        "Tjekliste",
        "Audit",
        "Evaluering",
        "Kundetilfredshed"
    };

    public ObservableCollection<string> SurveyIcons { get; } = new()
    {
        "📋",
        "🔍",
        "📝",
        "🏬",
        "⭐",
        "⚡",
        "🛡️"
    };

    public ObservableCollection<string> SurveyTemplateOptions { get; } = new()
    {
        "Blank survey"
    };

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
                Raise(nameof(IsGenereltTabActive));
                Raise(nameof(IsBygTabActive));
                Raise(nameof(IsLogikTabActive));
                Raise(nameof(IsIndstillingerTabActive));
                Raise(nameof(IsOversaettelserTabActive));
                Raise(nameof(IsVersionerTabActive));
            }
        }
    }

    public bool IsGenereltTabActive => ActiveSurveyTab == "Generelt";
    public bool IsBygTabActive => ActiveSurveyTab == "Byg";
    public bool IsLogikTabActive => ActiveSurveyTab == "Logik";
    public bool IsIndstillingerTabActive => ActiveSurveyTab == "Indstillinger";
    public bool IsOversaettelserTabActive => ActiveSurveyTab == "Oversættelser";
    public bool IsVersionerTabActive => ActiveSurveyTab == "Versioner";

    public string SelectedBrandTitle => SelectedBrand != null ? $"Surveys for {SelectedBrand.Name}" : "Surveys";

    public ICommand OpretBrandCommand { get; }
    public ICommand OpretSurveyCommand { get; }
    public ICommand GemSurveyGenereltCommand { get; }
    public ICommand FlereHandlingerCommand { get; }
    public ICommand SelectTabCommand { get; }
    public ICommand SelectQuestionCommand { get; }
    public ICommand SletSpoergsmaalCommand { get; }
    
    // Sektioner & Spørgsmål administration
    public ICommand TilfoejSektionCommand { get; }
    public ICommand SletSektionCommand { get; }
    public ICommand AdministrerSektionerCommand { get; }
    public ICommand TilfoejSpoergsmaalCommand { get; }
    public ICommand TilfoejSpoergsmaalTilSektionCommand { get; }

    // Reordering commands
    public ICommand MoveSectionUpCommand { get; }
    public ICommand MoveSectionDownCommand { get; }
    public ICommand MoveQuestionUpCommand { get; }
    public ICommand MoveQuestionDownCommand { get; }

    // Logik & Oversættelser commands
    public ICommand TilfoejLogikRegelCommand { get; }
    public ICommand SletLogikRegelCommand { get; }
    public ICommand TilfoejOversaettelseCommand { get; }
    public ICommand SletOversaettelseCommand { get; }
    public ICommand NavigateOrgTabCommand { get; }

    public OrganizationBrandsViewModel(IMockDataService data, MainShellViewModel shellVm)
    {
        _data = data;
        ShellVm = shellVm;

        Preview = data.GetPreview();

        var templates = _data.GetTemplates();
        foreach (var t in templates)
        {
            if (!SurveyTemplateOptions.Contains(t.Name))
            {
                SurveyTemplateOptions.Add(t.Name);
            }
        }

        var allBrands = _data.GetBrands(ShellVm.ActiveOrganization?.Id);
        foreach (var b in allBrands) Brands.Add(b);

        SelectedBrand = Brands.FirstOrDefault();

        _selectedQuestion = new SurveyQuestionEditorViewModel(new SurveyQuestionModel());

        OpretBrandCommand = new RelayCommand(async () => await OpretBrandAsync());
        OpretSurveyCommand = new RelayCommand(async () => await OpretSurveyAsync());
        GemSurveyGenereltCommand = new RelayCommand(GemSurveyGenerelt);
        FlereHandlingerCommand = new RelayCommand(async () => await FlereHandlingerAsync());
        SelectTabCommand = new RelayCommand<string>(tab => ActiveSurveyTab = tab ?? "Byg");
        SelectQuestionCommand = new RelayCommand<SurveyQuestionModel>(SelectQuestion);
        SletSpoergsmaalCommand = new RelayCommand<object>(SletSpoergsmaal);
        
        TilfoejSektionCommand = new RelayCommand(TilfoejSektionDirect);
        SletSektionCommand = new RelayCommand<SectionModel>(SletSektion);
        AdministrerSektionerCommand = new RelayCommand(async () => await AdministrerSektionerAsync());
        TilfoejSpoergsmaalCommand = new RelayCommand(async () => await TilfoejSpoergsmaalAsync());
        TilfoejSpoergsmaalTilSektionCommand = new RelayCommand<SectionModel>(TilfoejSpoergsmaalTilSektion);

        MoveSectionUpCommand = new RelayCommand<SectionModel>(MoveSectionUp);
        MoveSectionDownCommand = new RelayCommand<SectionModel>(MoveSectionDown);
        MoveQuestionUpCommand = new RelayCommand<SurveyQuestionModel>(MoveQuestionUp);
        MoveQuestionDownCommand = new RelayCommand<SurveyQuestionModel>(MoveQuestionDown);

        TilfoejLogikRegelCommand = new RelayCommand(async () => await TilfoejLogikRegelAsync());
        SletLogikRegelCommand = new RelayCommand<string>(SletLogikRegel);
        TilfoejOversaettelseCommand = new RelayCommand(async () => await TilfoejOversaettelseAsync());
        SletOversaettelseCommand = new RelayCommand<TranslationItemModel>(SletOversaettelse);
        NavigateOrgTabCommand = new RelayCommand<string>(key => ShellVm.RequestNavigation(key ?? "Brands"));
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

        IsCreatingNewSurvey = false;
        SurveyNameBuffer = survey.Name;
        SurveyTypeBuffer = string.IsNullOrEmpty(survey.Type) ? "Inspektion" : survey.Type;
        SurveyDescriptionBuffer = survey.Description ?? string.Empty;
        SurveyIconBuffer = string.IsNullOrEmpty(survey.Icon) ? "📋" : survey.Icon;
        SelectedTemplateNameBuffer = string.IsNullOrEmpty(survey.SelectedTemplateName) ? "Blank survey" : survey.SelectedTemplateName;

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

    private void SletSpoergsmaal(object? param)
    {
        if (param == null) return;

        SurveyQuestionModel? question = null;
        if (param is SurveyQuestionEditorViewModel editorVm)
        {
            question = editorVm.SourceQuestion;
        }
        else if (param is SurveyQuestionModel qModel)
        {
            question = qModel;
        }

        if (question == null) return;

        foreach (var sec in Sections)
        {
            if (sec.Questions.Contains(question))
            {
                sec.Questions.Remove(question);
                break;
            }
        }

        RecalculateSectionAndQuestionNumbers();
        UpdateQuestionCount();

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

    private void TilfoejSektionDirect()
    {
        int newNum = Sections.Count + 1;
        var newSection = new SectionModel { Title = $"{newNum}. Ny sektion" };
        Sections.Add(newSection);
    }

    private void SletSektion(SectionModel? section)
    {
        if (section == null || !Sections.Contains(section)) return;
        Sections.Remove(section);
        RecalculateSectionAndQuestionNumbers();
        UpdateQuestionCount();
    }

    private void TilfoejSpoergsmaalTilSektion(SectionModel? section)
    {
        if (section == null) return;

        int secIndex = Sections.IndexOf(section) + 1;
        string numberLabel = $"{secIndex}.{section.Questions.Count + 1}";

        var newQuestion = new SurveyQuestionModel
        {
            NumberLabel = numberLabel,
            Title = "Nyt spørgsmål",
            Type = "Ja / Nej",
            Description = "",
            IsRequired = false,
            VariableName = "",
            DisplayMode = "Standard",
            SectionIndex = secIndex,
            SurveyId = SelectedSurvey?.Id ?? 0
        };

        section.Questions.Add(newQuestion);
        SelectedQuestion = new SurveyQuestionEditorViewModel(newQuestion);
        UpdateQuestionCount();
    }

    private void MoveSectionUp(SectionModel? section)
    {
        if (section == null) return;
        int idx = Sections.IndexOf(section);
        if (idx > 0)
        {
            Sections.Move(idx, idx - 1);
            RecalculateSectionAndQuestionNumbers();
        }
    }

    private void MoveSectionDown(SectionModel? section)
    {
        if (section == null) return;
        int idx = Sections.IndexOf(section);
        if (idx >= 0 && idx < Sections.Count - 1)
        {
            Sections.Move(idx, idx + 1);
            RecalculateSectionAndQuestionNumbers();
        }
    }

    private void MoveQuestionUp(SurveyQuestionModel? question)
    {
        if (question == null) return;
        var section = Sections.FirstOrDefault(s => s.Questions.Contains(question));
        if (section == null) return;
        int idx = section.Questions.IndexOf(question);
        if (idx > 0)
        {
            section.Questions.Move(idx, idx - 1);
            RecalculateSectionAndQuestionNumbers();
        }
    }

    private void MoveQuestionDown(SurveyQuestionModel? question)
    {
        if (question == null) return;
        var section = Sections.FirstOrDefault(s => s.Questions.Contains(question));
        if (section == null) return;
        int idx = section.Questions.IndexOf(question);
        if (idx >= 0 && idx < section.Questions.Count - 1)
        {
            section.Questions.Move(idx, idx + 1);
            RecalculateSectionAndQuestionNumbers();
        }
    }

    public void ReorderSection(SectionModel source, SectionModel target)
    {
        if (source == null || target == null || source == target) return;
        int oldIndex = Sections.IndexOf(source);
        int newIndex = Sections.IndexOf(target);
        if (oldIndex >= 0 && newIndex >= 0)
        {
            Sections.Move(oldIndex, newIndex);
            RecalculateSectionAndQuestionNumbers();
        }
    }

    public void ReorderQuestion(SurveyQuestionModel sourceQuestion, SurveyQuestionModel targetQuestion)
    {
        if (sourceQuestion == null || targetQuestion == null || sourceQuestion == targetQuestion) return;

        SectionModel? sourceSection = Sections.FirstOrDefault(s => s.Questions.Contains(sourceQuestion));
        SectionModel? targetSection = Sections.FirstOrDefault(s => s.Questions.Contains(targetQuestion));

        if (sourceSection != null && targetSection != null)
        {
            sourceSection.Questions.Remove(sourceQuestion);
            int targetIdx = targetSection.Questions.IndexOf(targetQuestion);
            if (targetIdx >= 0)
            {
                targetSection.Questions.Insert(targetIdx, sourceQuestion);
            }
            else
            {
                targetSection.Questions.Add(sourceQuestion);
            }
            RecalculateSectionAndQuestionNumbers();
        }
    }

    public void RecalculateSectionAndQuestionNumbers()
    {
        for (int i = 0; i < Sections.Count; i++)
        {
            var sec = Sections[i];
            var parts = sec.Title.Split('.', 2);
            string baseName = parts.Length == 2 ? parts[1].Trim() : sec.Title.Trim();
            sec.Title = $"{i + 1}. {baseName}";

            for (int q = 0; q < sec.Questions.Count; q++)
            {
                sec.Questions[q].NumberLabel = $"{i + 1}.{q + 1}";
                sec.Questions[q].SectionIndex = i + 1;
            }
        }
    }

    private void UpdateQuestionCount()
    {
        if (SelectedSurvey != null)
        {
            SelectedSurvey.QuestionCount = Sections.Sum(s => s.Questions.Count);
        }
    }

    private async Task AdministrerSektionerAsync()
    {
        var action = await Application.Current!.MainPage!.DisplayActionSheet(
            "Administrer sektioner", "Annuller", null, "Tilføj ny sektion", "Slet eksisterende sektion");

        if (action == "Tilføj ny sektion")
        {
            TilfoejSektionDirect();
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
                    SletSektion(secToRemove);
                }
            }
        }
    }

    private async Task TilfoejSpoergsmaalAsync()
    {
        if (Sections.Count == 0)
        {
            TilfoejSektionDirect();
        }

        var selectedSec = Sections.FirstOrDefault();
        if (selectedSec != null)
        {
            TilfoejSpoergsmaalTilSektion(selectedSec);
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

        IsCreatingNewSurvey = true;
        SurveyNameBuffer = "Ny Survey";
        SurveyTypeBuffer = "Inspektion";
        SurveyDescriptionBuffer = "";
        SurveyIconBuffer = "📋";
        SelectedTemplateNameBuffer = "Blank survey";

        ActiveSurveyTab = "Generelt";
    }

    private void GemSurveyGenerelt()
    {
        if (SelectedBrand == null) return;

        if (IsCreatingNewSurvey)
        {
            var newSurvey = new SurveyModel
            {
                Name = string.IsNullOrWhiteSpace(SurveyNameBuffer) ? "Ny Survey" : SurveyNameBuffer.Trim(),
                Type = SurveyTypeBuffer,
                Description = SurveyDescriptionBuffer,
                Icon = SurveyIconBuffer,
                SelectedTemplateName = SelectedTemplateNameBuffer,
                BrandId = SelectedBrand.Id,
                Version = 1,
                QuestionCount = 0
            };

            _data.CreateSurvey(newSurvey);
            Surveys.Add(newSurvey);
            IsCreatingNewSurvey = false;
            SelectedSurvey = newSurvey;

            if (SelectedTemplateNameBuffer != "Blank survey")
            {
                PopulateTemplateQuestions(newSurvey, SelectedTemplateNameBuffer);
            }
        }
        else if (SelectedSurvey != null)
        {
            SelectedSurvey.Name = string.IsNullOrWhiteSpace(SurveyNameBuffer) ? SelectedSurvey.Name : SurveyNameBuffer.Trim();
            SelectedSurvey.Type = SurveyTypeBuffer;
            SelectedSurvey.Description = SurveyDescriptionBuffer;
            SelectedSurvey.Icon = SurveyIconBuffer;
            SelectedSurvey.SelectedTemplateName = SelectedTemplateNameBuffer;
        }

        ActiveSurveyTab = "Byg";
    }

    private void PopulateTemplateQuestions(SurveyModel survey, string templateName)
    {
        Sections.Clear();
        if (templateName == "HACCP Tjekliste")
        {
            var sec1 = new SectionModel { Title = "1. Temperaturmåling" };
            sec1.Questions.Add(new SurveyQuestionModel { NumberLabel = "1.1", Title = "Køleskab temperatur (C)", Type = "Tekst", SurveyId = survey.Id, SectionIndex = 1 });
            sec1.Questions.Add(new SurveyQuestionModel { NumberLabel = "1.2", Title = "Fryser temperatur (C)", Type = "Tekst", SurveyId = survey.Id, SectionIndex = 1 });
            Sections.Add(sec1);
        }
        else
        {
            var sec1 = new SectionModel { Title = "1. Generel kontrol" };
            sec1.Questions.Add(new SurveyQuestionModel { NumberLabel = "1.1", Title = "Status OK?", Type = "Ja / Nej", SurveyId = survey.Id, SectionIndex = 1 });
            Sections.Add(sec1);
        }
        UpdateQuestionCount();
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

    private async Task TilfoejLogikRegelAsync()
    {
        var ruleText = await Application.Current!.MainPage!.DisplayPromptAsync(
            "Ny logikregel",
            "Indtast betinget logikregel (f.eks. 'Hvis facade_ren == Nej, vis spørgsmål 2.4'):",
            "Gem", "Annuller", "Regeltekst");
        if (!string.IsNullOrWhiteSpace(ruleText))
        {
            LogicRules.Add(ruleText.Trim());
        }
    }

    private void SletLogikRegel(string? rule)
    {
        if (!string.IsNullOrEmpty(rule) && LogicRules.Contains(rule))
        {
            LogicRules.Remove(rule);
        }
    }

    private async Task TilfoejOversaettelseAsync()
    {
        var danish = await Application.Current!.MainPage!.DisplayPromptAsync(
            "Ny oversættelse",
            "Indtast dansk spørgsmålstekst:",
            "Næste", "Annuller", "Dansk tekst");
        if (string.IsNullOrWhiteSpace(danish)) return;

        var english = await Application.Current!.MainPage!.DisplayPromptAsync(
            "Ny oversættelse",
            "Indtast engelsk oversættelse:",
            "Gem", "Annuller", "Engelsk tekst");
        if (string.IsNullOrWhiteSpace(english)) english = danish;

        Translations.Add(new TranslationItemModel { Danish = danish.Trim(), English = english.Trim() });
    }

    private void SletOversaettelse(TranslationItemModel? item)
    {
        if (item != null && Translations.Contains(item))
        {
            Translations.Remove(item);
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


