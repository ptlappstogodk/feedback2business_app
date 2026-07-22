using Feedback2Business.Models;

namespace Feedback2Business.Services;

public class MockDataService : IMockDataService
{
    public List<OrganizationModel> GetOrganizations() =>
    [
        new() { Name = "Retail Group A", BrandCount = 3, SurveyCount = 20, UserCount = 5, UpdatedAt = DateTime.Today.AddDays(-2) },
        new() { Name = "FoodCo Holding", BrandCount = 2, SurveyCount = 14, UserCount = 3, UpdatedAt = DateTime.Today.AddDays(-5) },
        new() { Name = "Service Partners", BrandCount = 4, SurveyCount = 34, UserCount = 8, UpdatedAt = DateTime.Today.AddDays(-10) }
    ];

    public List<BrandModel> GetBrands() =>
    [
        new() { Name = "Coffee House", SurveyCount = 3 },
        new() { Name = "GreenFuel", SurveyCount = 2 },
        new() { Name = "Urban Eats", SurveyCount = 4 }
    ];

    public List<SurveyModel> GetSurveys() =>
    [
        new() { Name = "Butiksinspektion", Version = 3, QuestionCount = 24 },
        new() { Name = "HACCP Tjekliste", Version = 2, QuestionCount = 18 },
        new() { Name = "Kampagneevaluering", Version = 1, QuestionCount = 12 }
    ];

    public List<SurveyQuestionModel> GetSection1Questions() =>
    [
        new() { NumberLabel = "1.1", Title = "Dato og tidspunkt", Type = "Dato & tid" },
        new() { NumberLabel = "1.2", Title = "Butik", Type = "Sted (fra app)" },
        new() { NumberLabel = "1.3", Title = "Inspektør", Type = "Bruger (fra app)" }
    ];

    public List<SurveyQuestionModel> GetSection2Questions() =>
    [
        new() { NumberLabel = "2.1", Title = "Facade ren og vedligeholdt?", Type = "Ja / Nej", Description = "Angiv om facaden fremstår ren og uden skader.", IsRequired = true, VariableName = "facade_ren", DisplayMode = "Standard" },
        new() { NumberLabel = "2.2", Title = "Skiltning intakt og synlig?", Type = "Ja / Nej" },
        new() { NumberLabel = "2.3", Title = "Vinduer rene", Type = "Ja / Nej" },
        new() { NumberLabel = "2.4", Title = "Billede af facade", Type = "Billede" }
    ];

    public List<SurveyQuestionModel> GetSection3Questions() =>
    [
        new() { NumberLabel = "3.1", Title = "Butikken fremstår ryddelig", Type = "Skala 1-5" },
        new() { NumberLabel = "3.2", Title = "Produkter korrekt prissat", Type = "Ja / Nej" },
        new() { NumberLabel = "3.3", Title = "Kampagnemateriale på plads", Type = "Ja / Nej" },
        new() { NumberLabel = "3.4", Title = "Billede af kampagne", Type = "Billede" }
    ];

    public List<UserModel> GetUsers() =>
    [
        new() { Name = "Anders Kirk", Email = "anders.kirk@email.com", Role = "Admin", Status = "Aktiv", LastActiveAt = DateTime.Now.AddDays(-1) },
        new() { Name = "Maria Jensen", Email = "maria.jensen@email.com", Role = "Manager", Status = "Aktiv", LastActiveAt = DateTime.Now.AddDays(-2) },
        new() { Name = "Lars Petersen", Email = "lars.petersen@email.com", Role = "Editor", Status = "Aktiv", LastActiveAt = DateTime.Now.AddDays(-5) }
    ];

    public List<TemplateModel> GetTemplates() =>
    [
        new() { Name = "Butiksinspektion", Type = "Inspektion", Description = "Standard skabelon til butiksinspektioner", UpdatedAt = DateTime.Today.AddDays(-3) },
        new() { Name = "HACCP Tjekliste", Type = "Tjekliste", Description = "Fødevarekontrol og HACCP", UpdatedAt = DateTime.Today.AddDays(-10) },
        new() { Name = "Kampagneevaluering", Type = "Survey", Description = "Evaluering af kampagner i butik", UpdatedAt = DateTime.Today.AddDays(-14) }
    ];

    public List<VariableModel> GetVariables() =>
    [
        new() { Name = "Organisationsnavn", Key = "org_name", Type = "Tekst", DefaultValue = "Retail Group A", UsageCount = 5 },
        new() { Name = "Inspektørens navn", Key = "inspector_name", Type = "Tekst", DefaultValue = "", UsageCount = 9 },
        new() { Name = "Dato", Key = "today", Type = "Dato", DefaultValue = "I dag", UsageCount = 9 }
    ];

    public List<MediaItemModel> GetMediaItems() =>
    [
        new() { Name = "facade_eksempel.jpg", ThumbnailUrl = "dotnet_bot.png", MetaText = "Billede · 1.2 MB" },
        new() { Name = "indgang_eksempel.jpg", ThumbnailUrl = "dotnet_bot.png", MetaText = "Billede · 980 KB" },
        new() { Name = "brand_logo.png", ThumbnailUrl = "dotnet_bot.png", MetaText = "Billede · 250 KB" }
    ];

    public List<RoleModel> GetRoles() =>
    [
        new()
        {
            Name = "Admin",
            Description = "Fuldt overblik og adgang til alle funktioner",
            PermissionGroups = new List<PermissionGroupModel>
            {
                new()
                {
                    Name = "Organisation",
                    Permissions = new List<PermissionModel>
                    {
                        new() { Label = "Administrer organisation", IsEnabled = true },
                        new() { Label = "Inviter brugere", IsEnabled = true },
                        new() { Label = "Administrer brugere", IsEnabled = true }
                    }
                },
                new()
                {
                    Name = "Surveys",
                    Permissions = new List<PermissionModel>
                    {
                        new() { Label = "Opret surveys", IsEnabled = true },
                        new() { Label = "Rediger surveys", IsEnabled = true },
                        new() { Label = "Se besvarelser", IsEnabled = true }
                    }
                }
            }
        },
        new() { Name = "Manager", Description = "Kan administrere surveys og se rapporter" },
        new() { Name = "Editor", Description = "Kan oprette og redigere surveys" },
        new() { Name = "Viewer", Description = "Kan udfylde surveys og se egne data" }
    ];

    public List<ActivityEventModel> GetActivityEvents() =>
    [
        new() { Timestamp = DateTime.Now.AddHours(-2), Action = "Opdaterede survey", UserName = "Anders Kirk", Details = "Butiksinspektion v3" },
        new() { Timestamp = DateTime.Now.AddHours(-5), Action = "Inviterede bruger", UserName = "Maria Jensen", Details = "maria.jensen@email.com" },
        new() { Timestamp = DateTime.Now.AddDays(-1), Action = "Uploadede fil", UserName = "Anders Kirk", Details = "brand_logo.png" }
    ];

    public MobilePreviewModel GetPreview() => new()
    {
        SurveyTitle = "Butiksinspektion",
        ProgressText = "2 af 24",
        PercentText = "8%",
        QuestionNumberTitle = "2.1 Facade ren og vedligeholdt?",
        Description = "Angiv om facaden fremstår ren og uden skader."
    };

    public void CreateOrganization(OrganizationModel org) { }
    public void CreateBrand(BrandModel brand) { }
    public void CreateSurvey(SurveyModel survey) { }
    public void CreateUser(UserModel user) { }
    public void CreateTemplate(TemplateModel template) { }
    public void CreateVariable(VariableModel variable) { }
    public void CreateRole(RoleModel role) { }
}


