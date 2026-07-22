// File: App.xaml
<?xml version="1.0" encoding="UTF-8" ?>
<Application
    x:Class="SurveyHub.App"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="Resources/Styles/Colors.xaml" />
                <ResourceDictionary Source="Resources/Styles/Styles.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>


// File: App.xaml.cs
namespace SurveyHub;

public partial class App : Application
{
    public App(AppShell shell)
    {
        InitializeComponent();
        MainPage = shell;
    }
}


// File: AppShell.xaml
<?xml version="1.0" encoding="utf-8" ?>
<Shell
    x:Class="SurveyHub.AppShell"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:views="clr-namespace:SurveyHub.Views"
    FlyoutBehavior="Disabled">

    <ShellContent
        Title="SurveyHub"
        ContentTemplate="{DataTemplate views:DashboardShellPage}" />
</Shell>


// File: AppShell.xaml.cs
namespace SurveyHub;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
    }
}


// File: MauiProgram.cs
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SurveyHub.Services;
using SurveyHub.ViewModels;
using SurveyHub.Views;
using SurveyHub.Views.ActivityLog;
using SurveyHub.Views.Media;
using SurveyHub.Views.Organizations;
using SurveyHub.Views.Roles;
using SurveyHub.Views.Settings;
using SurveyHub.Views.Templates;
using SurveyHub.Views.Variables;

namespace SurveyHub;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<IMockDataService, MockDataService>();

        builder.Services.AddSingleton<MainShellViewModel>();

        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<DashboardShellPage>();

        builder.Services.AddTransient<OrganizationsViewModel>();
        builder.Services.AddTransient<OrganizationBrandsViewModel>();
        builder.Services.AddTransient<OrganizationUsersViewModel>();
        builder.Services.AddTransient<TemplatesViewModel>();
        builder.Services.AddTransient<VariablesViewModel>();
        builder.Services.AddTransient<MediaLibraryViewModel>();
        builder.Services.AddTransient<SettingsGeneralViewModel>();
        builder.Services.AddTransient<SettingsAppViewModel>();
        builder.Services.AddTransient<RolesPermissionsViewModel>();
        builder.Services.AddTransient<ActivityLogViewModel>();

        builder.Services.AddTransient<OrganizationsPage>();
        builder.Services.AddTransient<OrganizationBrandsPage>();
        builder.Services.AddTransient<OrganizationUsersPage>();
        builder.Services.AddTransient<TemplatesPage>();
        builder.Services.AddTransient<VariablesPage>();
        builder.Services.AddTransient<MediaLibraryPage>();
        builder.Services.AddTransient<SettingsGeneralPage>();
        builder.Services.AddTransient<SettingsAppPage>();
        builder.Services.AddTransient<RolesPermissionsPage>();
        builder.Services.AddTransient<ActivityLogPage>();

        return builder.Build();
    }
}


// File: Resources/Styles/Colors.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ResourceDictionary
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">

    <Color x:Key="SidebarBackground">#041B36</Color>
    <Color x:Key="PageBackground">#F5F7FB</Color>
    <Color x:Key="CardBackground">#FFFFFF</Color>
    <Color x:Key="PrimaryBlue">#1F6BFF</Color>
    <Color x:Key="PrimaryBlueLight">#EEF4FF</Color>
    <Color x:Key="BorderColor">#E2E8F0</Color>
    <Color x:Key="TextPrimary">#1B2430</Color>
    <Color x:Key="TextSecondary">#6B7280</Color>
    <Color x:Key="SuccessGreen">#1E8E5A</Color>
    <Color x:Key="SuccessGreenLight">#D9F5E5</Color>
    <Color x:Key="DangerRed">#D14343</Color>
</ResourceDictionary>


// File: Resources/Styles/Styles.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ResourceDictionary
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">

    <Style x:Key="PageTitleStyle" TargetType="Label">
        <Setter Property="FontSize" Value="24" />
        <Setter Property="FontAttributes" Value="Bold" />
        <Setter Property="TextColor" Value="{StaticResource TextPrimary}" />
    </Style>

    <Style x:Key="PageSubtitleStyle" TargetType="Label">
        <Setter Property="FontSize" Value="12" />
        <Setter Property="TextColor" Value="{StaticResource TextSecondary}" />
    </Style>

    <Style x:Key="SectionHeaderStyle" TargetType="Label">
        <Setter Property="FontSize" Value="16" />
        <Setter Property="FontAttributes" Value="Bold" />
        <Setter Property="TextColor" Value="{StaticResource TextPrimary}" />
    </Style>

    <Style x:Key="DataTableHeaderStyle" TargetType="Label">
        <Setter Property="FontSize" Value="12" />
        <Setter Property="FontAttributes" Value="Bold" />
        <Setter Property="TextColor" Value="{StaticResource TextSecondary}" />
    </Style>

    <Style x:Key="FormLabelStyle" TargetType="Label">
        <Setter Property="FontSize" Value="12" />
        <Setter Property="FontAttributes" Value="Bold" />
        <Setter Property="TextColor" Value="{StaticResource TextPrimary}" />
    </Style>

    <Style x:Key="TabTextStyle" TargetType="Label">
        <Setter Property="FontSize" Value="13" />
        <Setter Property="TextColor" Value="{StaticResource TextSecondary}" />
    </Style>

    <Style x:Key="ActiveTabTextStyle" TargetType="Label">
        <Setter Property="FontSize" Value="13" />
        <Setter Property="TextColor" Value="{StaticResource PrimaryBlue}" />
        <Setter Property="FontAttributes" Value="Bold" />
    </Style>

    <Style x:Key="PrimaryButtonStyle" TargetType="Button">
        <Setter Property="BackgroundColor" Value="{StaticResource PrimaryBlue}" />
        <Setter Property="TextColor" Value="White" />
        <Setter Property="CornerRadius" Value="8" />
        <Setter Property="Padding" Value="16,10" />
    </Style>

    <Style x:Key="SecondaryButtonStyle" TargetType="Button">
        <Setter Property="BackgroundColor" Value="White" />
        <Setter Property="TextColor" Value="{StaticResource TextPrimary}" />
        <Setter Property="BorderColor" Value="{StaticResource BorderColor}" />
        <Setter Property="BorderWidth" Value="1" />
        <Setter Property="CornerRadius" Value="8" />
        <Setter Property="Padding" Value="16,10" />
    </Style>

    <Style x:Key="LinkButtonStyle" TargetType="Button">
        <Setter Property="BackgroundColor" Value="Transparent" />
        <Setter Property="TextColor" Value="{StaticResource PrimaryBlue}" />
        <Setter Property="Padding" Value="0" />
    </Style>

    <Style x:Key="CardStyle" TargetType="Border">
        <Setter Property="BackgroundColor" Value="{StaticResource CardBackground}" />
        <Setter Property="Stroke" Value="{StaticResource BorderColor}" />
        <Setter Property="StrokeThickness" Value="1" />
        <Setter Property="StrokeShape" Value="RoundRectangle 12" />
    </Style>

    <Style x:Key="InnerCardStyle" TargetType="Border">
        <Setter Property="BackgroundColor" Value="White" />
        <Setter Property="Stroke" Value="{StaticResource BorderColor}" />
        <Setter Property="StrokeThickness" Value="1" />
        <Setter Property="StrokeShape" Value="RoundRectangle 10" />
    </Style>
</ResourceDictionary>


// File: Models/NavigationItem.cs
namespace SurveyHub.Models;

public class NavigationItem
{
    public string Key { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}


// File: Models/UserModel.cs
namespace SurveyHub.Models;

public class UserModel
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime? LastActiveAt { get; set; }

    public string LastActiveText => LastActiveAt?.ToString("dd. MM yy") ?? "-";
}


// File: Models/OrganizationModel.cs
namespace SurveyHub.Models;

public class OrganizationModel
{
    public string Name { get; set; } = string.Empty;
    public int BrandCount { get; set; }
    public int SurveyCount { get; set; }
    public int UserCount { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string UpdatedText => UpdatedAt.ToString("dd. MMM yyyy");
}


// File: Models/BrandModel.cs
namespace SurveyHub.Models;

public class BrandModel
{
    public string Name { get; set; } = string.Empty;
    public int SurveyCount { get; set; }
    public string SurveyCountText => $"{SurveyCount} surveys";
}


// File: Models/SurveyModel.cs
namespace SurveyHub.Models;

public class SurveyModel
{
    public string Name { get; set; } = string.Empty;
    public int Version { get; set; }
    public int QuestionCount { get; set; }

    public string VersionText => $"Version {Version} · {QuestionCount} spørgsmål";
}


// File: Models/SurveyQuestionModel.cs
namespace SurveyHub.Models;

public class SurveyQuestionModel
{
    public string NumberLabel { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsRequired { get; set; }
    public string VariableName { get; set; } = string.Empty;
    public string DisplayMode { get; set; } = "Standard";

    public string DisplayTitle => $"{NumberLabel}    {Title}";
    public string TypeLabel => Type;
}


// File: Models/TemplateModel.cs
namespace SurveyHub.Models;

public class TemplateModel
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }

    public string UpdatedText => UpdatedAt.ToString("dd. MMM yyyy");
}


// File: Models/VariableModel.cs
namespace SurveyHub.Models;

public class VariableModel
{
    public string Name { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string DefaultValue { get; set; } = string.Empty;
    public int UsageCount { get; set; }

    public string UsageCountText => $"{UsageCount} surveys";
}


// File: Models/MediaItemModel.cs
namespace SurveyHub.Models;

public class MediaItemModel
{
    public string Name { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public string MetaText { get; set; } = string.Empty;
}


// File: Models/PermissionModels.cs
namespace SurveyHub.Models;

public class PermissionModel
{
    public string Label { get; set; } = string.Empty;
    public bool IsEnabled { get; set; }
}

public class PermissionGroupModel
{
    public string Name { get; set; } = string.Empty;
    public List<PermissionModel> Permissions { get; set; } = new();
}

public class RoleModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<PermissionGroupModel> PermissionGroups { get; set; } = new();
}


// File: Models/ActivityEventModel.cs
namespace SurveyHub.Models;

public class ActivityEventModel
{
    public DateTime Timestamp { get; set; }
    public string Action { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;

    public string TimestampText => Timestamp.ToString("dd. MMM yyyy, HH:mm");
}


// File: Models/MobilePreviewModel.cs
namespace SurveyHub.Models;

public class MobilePreviewModel
{
    public string SurveyTitle { get; set; } = string.Empty;
    public string ProgressText { get; set; } = string.Empty;
    public string PercentText { get; set; } = string.Empty;
    public string QuestionNumberTitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}


// File: Services/IMockDataService.cs
using SurveyHub.Models;

namespace SurveyHub.Services;

public interface IMockDataService
{
    List<OrganizationModel> GetOrganizations();
    List<BrandModel> GetBrands();
    List<SurveyModel> GetSurveys();
    List<SurveyQuestionModel> GetSection1Questions();
    List<SurveyQuestionModel> GetSection2Questions();
    List<SurveyQuestionModel> GetSection3Questions();
    List<UserModel> GetUsers();
    List<TemplateModel> GetTemplates();
    List<VariableModel> GetVariables();
    List<MediaItemModel> GetMediaItems();
    List<RoleModel> GetRoles();
    List<ActivityEventModel> GetActivityEvents();
    MobilePreviewModel GetPreview();
}


// File: Services/MockDataService.cs
using SurveyHub.Models;

namespace SurveyHub.Services;

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
}


// File: ViewModels/ObservableObject.cs
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SurveyHub.ViewModels;

public abstract class ObservableObject : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        backingStore = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        return true;
    }

    protected void Raise([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


// File: ViewModels/RelayCommand.cs
using System.Windows.Input;

namespace SurveyHub.ViewModels;

public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool>? _canExecute;

    public RelayCommand(Action execute, Func<bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;
    public void Execute(object? parameter) => _execute();
    public event EventHandler? CanExecuteChanged;
}


// File: ViewModels/MainShellViewModel.cs
using System.Collections.ObjectModel;
using SurveyHub.Models;

namespace SurveyHub.ViewModels;

public class MainShellViewModel : ObservableObject
{
    private string _breadcrumbPrimary = "Organisationer";
    private string _breadcrumbSecondary = "Retail Group A";

    public ObservableCollection<NavigationItem> NavigationItems { get; } = new();
    public UserModel CurrentUser { get; } = new() { Name = "Anders Kirk", Role = "Admin" };

    public string BreadcrumbPrimary
    {
        get => _breadcrumbPrimary;
        set => SetProperty(ref _breadcrumbPrimary, value);
    }

    public string BreadcrumbSecondary
    {
        get => _breadcrumbSecondary;
        set => SetProperty(ref _breadcrumbSecondary, value);
    }

    public MainShellViewModel()
    {
        NavigationItems.Add(new NavigationItem { Key = "Organizations", Title = "Organisationer" });
        NavigationItems.Add(new NavigationItem { Key = "Brands", Title = "Brands" });
        NavigationItems.Add(new NavigationItem { Key = "Users", Title = "Brugere" });
        NavigationItems.Add(new NavigationItem { Key = "Templates", Title = "Skabeloner" });
        NavigationItems.Add(new NavigationItem { Key = "Variables", Title = "Variabler" });
        NavigationItems.Add(new NavigationItem { Key = "Media", Title = "Mediebibliotek" });
        NavigationItems.Add(new NavigationItem { Key = "SettingsGeneral", Title = "Indstillinger" });
        NavigationItems.Add(new NavigationItem { Key = "SettingsApp", Title = "App-indstillinger" });
        NavigationItems.Add(new NavigationItem { Key = "Roles", Title = "Roller & rettigheder" });
        NavigationItems.Add(new NavigationItem { Key = "ActivityLog", Title = "Aktivitetslog" });
    }
}


// File: ViewModels/OrganizationsViewModel.cs
using System.Collections.ObjectModel;
using SurveyHub.Models;
using SurveyHub.Services;

namespace SurveyHub.ViewModels;

public class OrganizationsViewModel : ObservableObject
{
    private string _searchText = string.Empty;

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public ObservableCollection<OrganizationModel> Organizations { get; } = new();

    public OrganizationsViewModel(IMockDataService data)
    {
        foreach (var item in data.GetOrganizations())
            Organizations.Add(item);
    }
}


// File: ViewModels/SurveyQuestionEditorViewModel.cs
using System.Collections.ObjectModel;
using SurveyHub.Models;

namespace SurveyHub.ViewModels;

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


// File: ViewModels/OrganizationBrandsViewModel.cs
using System.Collections.ObjectModel;
using SurveyHub.Models;
using SurveyHub.Services;

namespace SurveyHub.ViewModels;

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


// File: ViewModels/OrganizationUsersViewModel.cs
using System.Collections.ObjectModel;
using SurveyHub.Models;
using SurveyHub.Services;

namespace SurveyHub.ViewModels;

public class OrganizationUsersViewModel : ObservableObject
{
    private string _searchText = string.Empty;

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public ObservableCollection<string> Roles { get; } = new() { "Alle", "Admin", "Manager", "Editor", "Viewer" };
    public ObservableCollection<string> Statuses { get; } = new() { "Alle", "Aktiv", "Inviteret", "Deaktiveret" };
    public ObservableCollection<UserModel> Users { get; } = new();

    public OrganizationUsersViewModel(IMockDataService data)
    {
        foreach (var item in data.GetUsers())
            Users.Add(item);
    }
}


// File: ViewModels/TemplatesViewModel.cs
using System.Collections.ObjectModel;
using SurveyHub.Models;
using SurveyHub.Services;

namespace SurveyHub.ViewModels;

public class TemplatesViewModel : ObservableObject
{
    private string _searchText = string.Empty;

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public ObservableCollection<TemplateModel> Templates { get; } = new();

    public TemplatesViewModel(IMockDataService data)
    {
        foreach (var item in data.GetTemplates())
            Templates.Add(item);
    }
}


// File: ViewModels/VariablesViewModel.cs
using System.Collections.ObjectModel;
using SurveyHub.Models;
using SurveyHub.Services;

namespace SurveyHub.ViewModels;

public class VariablesViewModel : ObservableObject
{
    public ObservableCollection<VariableModel> Variables { get; } = new();

    public VariablesViewModel(IMockDataService data)
    {
        foreach (var item in data.GetVariables())
            Variables.Add(item);
    }
}


// File: ViewModels/MediaLibraryViewModel.cs
using System.Collections.ObjectModel;
using SurveyHub.Models;
using SurveyHub.Services;

namespace SurveyHub.ViewModels;

public class MediaLibraryViewModel : ObservableObject
{
    private string _searchText = string.Empty;

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public ObservableCollection<string> FileTypes { get; } = new() { "Alle", "Billeder", "PDF", "DOCX", "XLSX" };
    public ObservableCollection<MediaItemModel> MediaItems { get; } = new();

    public MediaLibraryViewModel(IMockDataService data)
    {
        foreach (var item in data.GetMediaItems())
            MediaItems.Add(item);
    }
}


// File: ViewModels/SettingsGeneralViewModel.cs
using System.Collections.ObjectModel;

namespace SurveyHub.ViewModels;

public class SettingsGeneralViewModel : ObservableObject
{
    private string _organizationName = "Retail Group A";
    private string _selectedLanguage = "Dansk";
    private string _selectedTimezone = "UTC+01:00 København";
    private string _dashboardName = "Dashboard";
    private string _dateFormat = "DD-MM-YYYY";

    public string OrganizationName
    {
        get => _organizationName;
        set => SetProperty(ref _organizationName, value);
    }

    public ObservableCollection<string> Languages { get; } = new() { "Dansk", "English", "German" };
    public ObservableCollection<string> Timezones { get; } = new() { "UTC+01:00 København", "UTC+00:00 London" };

    public string SelectedLanguage
    {
        get => _selectedLanguage;
        set => SetProperty(ref _selectedLanguage, value);
    }

    public string SelectedTimezone
    {
        get => _selectedTimezone;
        set => SetProperty(ref _selectedTimezone, value);
    }

    public string DashboardName
    {
        get => _dashboardName;
        set => SetProperty(ref _dashboardName, value);
    }

    public string DateFormat
    {
        get => _dateFormat;
        set => SetProperty(ref _dateFormat, value);
    }
}


// File: ViewModels/SettingsAppViewModel.cs
using System.Collections.ObjectModel;

namespace SurveyHub.ViewModels;

public class SettingsAppViewModel : ObservableObject
{
    private bool _offlineEnabled = true;
    private bool _autoSyncEnabled = true;
    private bool _requireLocationAtStart = true;
    private bool _useLocationOnObservations = false;
    private bool _compressImages = true;
    private string _selectedMaxFileSize = "2 MB";
    private bool _showSurveyProgress = true;
    private bool _allowSkipForward = false;

    public bool OfflineEnabled
    {
        get => _offlineEnabled;
        set => SetProperty(ref _offlineEnabled, value);
    }

    public bool AutoSyncEnabled
    {
        get => _autoSyncEnabled;
        set => SetProperty(ref _autoSyncEnabled, value);
    }

    public bool RequireLocationAtStart
    {
        get => _requireLocationAtStart;
        set => SetProperty(ref _requireLocationAtStart, value);
    }

    public bool UseLocationOnObservations
    {
        get => _useLocationOnObservations;
        set => SetProperty(ref _useLocationOnObservations, value);
    }

    public bool CompressImages
    {
        get => _compressImages;
        set => SetProperty(ref _compressImages, value);
    }

    public ObservableCollection<string> MaxFileSizeOptions { get; } = new() { "1 MB", "2 MB", "5 MB", "10 MB" };

    public string SelectedMaxFileSize
    {
        get => _selectedMaxFileSize;
        set => SetProperty(ref _selectedMaxFileSize, value);
    }

    public bool ShowSurveyProgress
    {
        get => _showSurveyProgress;
        set => SetProperty(ref _showSurveyProgress, value);
    }

    public bool AllowSkipForward
    {
        get => _allowSkipForward;
        set => SetProperty(ref _allowSkipForward, value);
    }
}


// File: ViewModels/RolesPermissionsViewModel.cs
using System.Collections.ObjectModel;
using SurveyHub.Models;
using SurveyHub.Services;

namespace SurveyHub.ViewModels;

public class RolesPermissionsViewModel : ObservableObject
{
    private RoleModel _selectedRole = new();

    public ObservableCollection<RoleModel> Roles { get; } = new();

    public RoleModel SelectedRole
    {
        get => _selectedRole;
        set
        {
            if (SetProperty(ref _selectedRole, value))
                Raise(nameof(PermissionGroups));
        }
    }

    public List<PermissionGroupModel> PermissionGroups => SelectedRole.PermissionGroups;

    public RolesPermissionsViewModel(IMockDataService data)
    {
        foreach (var role in data.GetRoles())
            Roles.Add(role);

        SelectedRole = Roles.FirstOrDefault() ?? new RoleModel();
    }
}


// File: ViewModels/ActivityLogViewModel.cs
using System.Collections.ObjectModel;
using SurveyHub.Models;
using SurveyHub.Services;

namespace SurveyHub.ViewModels;

public class ActivityLogViewModel : ObservableObject
{
    private string _searchText = string.Empty;

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public ObservableCollection<string> Actions { get; } = new() { "Alle", "Opdateret", "Oprettet", "Uploadet", "Eksporteret" };
    public ObservableCollection<string> Users { get; } = new() { "Alle", "Anders Kirk", "Maria Jensen", "Lars Petersen" };
    public ObservableCollection<string> Periods { get; } = new() { "7 dage", "30 dage", "90 dage" };

    public ObservableCollection<ActivityEventModel> ActivityEvents { get; } = new();

    public ActivityLogViewModel(IMockDataService data)
    {
        foreach (var item in data.GetActivityEvents())
            ActivityEvents.Add(item);
    }
}


// File: Views/DashboardShellPage.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage
    x:Class="SurveyHub.Views.DashboardShellPage"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:shell="clr-namespace:SurveyHub.Views.Shell"
    BackgroundColor="{StaticResource PageBackground}">

    <Grid ColumnDefinitions="250,*">
        <shell:SidebarView x:Name="Sidebar" Grid.Column="0" />
        <Grid Grid.Column="1" RowDefinitions="Auto,*">
            <shell:TopHeaderView x:Name="Header" Grid.Row="0" />
            <ContentView x:Name="PageHost" Grid.Row="1" />
        </Grid>
    </Grid>
</ContentPage>


// File: Views/DashboardShellPage.xaml.cs
using Microsoft.Extensions.DependencyInjection;
using SurveyHub.ViewModels;
using SurveyHub.Views.ActivityLog;
using SurveyHub.Views.Media;
using SurveyHub.Views.Organizations;
using SurveyHub.Views.Roles;
using SurveyHub.Views.Settings;
using SurveyHub.Views.Templates;
using SurveyHub.Views.Variables;

namespace SurveyHub.Views;

public partial class DashboardShellPage : ContentPage
{
    private readonly IServiceProvider _services;
    private readonly MainShellViewModel _vm;

    public DashboardShellPage(IServiceProvider services, MainShellViewModel vm)
    {
        InitializeComponent();

        _services = services;
        _vm = vm;

        BindingContext = _vm;
        Header.BindingContext = _vm;

        Sidebar.NavigationRequested += Sidebar_NavigationRequested;

        Loaded += DashboardShellPage_Loaded;
    }

    private void DashboardShellPage_Loaded(object? sender, EventArgs e)
    {
        NavigateTo("Brands");
    }

    private void Sidebar_NavigationRequested(object? sender, string key)
    {
        NavigateTo(key);
    }

    private void NavigateTo(string key)
    {
        Page page;

        switch (key)
        {
            case "Organizations":
                _vm.BreadcrumbPrimary = "Organisationer";
                _vm.BreadcrumbSecondary = "";
                page = _services.GetRequiredService<OrganizationsPage>();
                break;

            case "Brands":
                _vm.BreadcrumbPrimary = "Organisationer";
                _vm.BreadcrumbSecondary = "Retail Group A";
                page = _services.GetRequiredService<OrganizationBrandsPage>();
                break;

            case "Users":
                _vm.BreadcrumbPrimary = "Organisationer";
                _vm.BreadcrumbSecondary = "Retail Group A";
                page = _services.GetRequiredService<OrganizationUsersPage>();
                break;

            case "Templates":
                _vm.BreadcrumbPrimary = "Skabeloner";
                _vm.BreadcrumbSecondary = "";
                page = _services.GetRequiredService<TemplatesPage>();
                break;

            case "Variables":
                _vm.BreadcrumbPrimary = "Variabler";
                _vm.BreadcrumbSecondary = "";
                page = _services.GetRequiredService<VariablesPage>();
                break;

            case "Media":
                _vm.BreadcrumbPrimary = "Mediebibliotek";
                _vm.BreadcrumbSecondary = "";
                page = _services.GetRequiredService<MediaLibraryPage>();
                break;

            case "SettingsGeneral":
                _vm.BreadcrumbPrimary = "Indstillinger";
                _vm.BreadcrumbSecondary = "Generelt";
                page = _services.GetRequiredService<SettingsGeneralPage>();
                break;

            case "SettingsApp":
                _vm.BreadcrumbPrimary = "Indstillinger";
                _vm.BreadcrumbSecondary = "App-indstillinger";
                page = _services.GetRequiredService<SettingsAppPage>();
                break;

            case "Roles":
                _vm.BreadcrumbPrimary = "Roller & rettigheder";
                _vm.BreadcrumbSecondary = "";
                page = _services.GetRequiredService<RolesPermissionsPage>();
                break;

            case "ActivityLog":
                _vm.BreadcrumbPrimary = "Aktivitetslog";
                _vm.BreadcrumbSecondary = "";
                page = _services.GetRequiredService<ActivityLogPage>();
                break;

            default:
                _vm.BreadcrumbPrimary = "Organisationer";
                _vm.BreadcrumbSecondary = "Retail Group A";
                page = _services.GetRequiredService<OrganizationBrandsPage>();
                break;
        }

        PageHost.Content = page.Content;
    }
}


// File: Views/Shell/SidebarView.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentView
    x:Class="SurveyHub.Views.Shell.SidebarView"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    BackgroundColor="{StaticResource SidebarBackground}">

    <Grid Padding="16" RowDefinitions="Auto,*,Auto">
        <VerticalStackLayout Spacing="6">
            <Label Text="SurveyHub" TextColor="White" FontSize="20" FontAttributes="Bold" />
            <Label Text="Konfiguration" TextColor="#B8C7E0" FontSize="12" />
        </VerticalStackLayout>

        <VerticalStackLayout Grid.Row="1" Margin="0,20,0,20" Spacing="8">
            <Button Text="Organisationer" Clicked="Organizations_Clicked" />
            <Button Text="Brands" Clicked="Brands_Clicked" />
            <Button Text="Brugere" Clicked="Users_Clicked" />
            <Button Text="Skabeloner" Clicked="Templates_Clicked" />
            <Button Text="Variabler" Clicked="Variables_Clicked" />
            <Button Text="Mediebibliotek" Clicked="Media_Clicked" />
            <Button Text="Indstillinger" Clicked="SettingsGeneral_Clicked" />
            <Button Text="App-indstillinger" Clicked="SettingsApp_Clicked" />
            <Button Text="Roller & rettigheder" Clicked="Roles_Clicked" />
            <Button Text="Aktivitetslog" Clicked="ActivityLog_Clicked" />
        </VerticalStackLayout>

        <Border Grid.Row="2"
                Padding="12"
                Stroke="#1C3A69"
                BackgroundColor="#0A2444">
            <Grid ColumnDefinitions="Auto,*,Auto">
                <Border WidthRequest="32"
                        HeightRequest="32"
                        StrokeThickness="0"
                        BackgroundColor="#2F4B73"
                        StrokeShape="RoundRectangle 16">
                    <Label Text="AK"
                           HorizontalOptions="Center"
                           VerticalOptions="Center"
                           TextColor="White"
                           FontSize="12" />
                </Border>

                <VerticalStackLayout Grid.Column="1" Margin="10,0,0,0" Spacing="0">
                    <Label Text="Anders Kirk" TextColor="White" FontSize="13" />
                    <Label Text="Admin" TextColor="#B8C7E0" FontSize="11" />
                </VerticalStackLayout>

                <Label Grid.Column="2" Text="›" TextColor="White" VerticalOptions="Center" />
            </Grid>
        </Border>
    </Grid>
</ContentView>


// File: Views/Shell/SidebarView.xaml.cs
namespace SurveyHub.Views.Shell;

public partial class SidebarView : ContentView
{
    public event EventHandler<string>? NavigationRequested;

    public SidebarView()
    {
        InitializeComponent();
    }

    private void Raise(string key) => NavigationRequested?.Invoke(this, key);

    private void Organizations_Clicked(object sender, EventArgs e) => Raise("Organizations");
    private void Brands_Clicked(object sender, EventArgs e) => Raise("Brands");
    private void Users_Clicked(object sender, EventArgs e) => Raise("Users");
    private void Templates_Clicked(object sender, EventArgs e) => Raise("Templates");
    private void Variables_Clicked(object sender, EventArgs e) => Raise("Variables");
    private void Media_Clicked(object sender, EventArgs e) => Raise("Media");
    private void SettingsGeneral_Clicked(object sender, EventArgs e) => Raise("SettingsGeneral");
    private void SettingsApp_Clicked(object sender, EventArgs e) => Raise("SettingsApp");
    private void Roles_Clicked(object sender, EventArgs e) => Raise("Roles");
    private void ActivityLog_Clicked(object sender, EventArgs e) => Raise("ActivityLog");
}


// File: Views/Shell/TopHeaderView.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentView
    x:Class="SurveyHub.Views.Shell.TopHeaderView"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">

    <Grid Padding="24,16" ColumnDefinitions="*,Auto" BackgroundColor="White">
        <HorizontalStackLayout Spacing="8">
            <Label Text="{Binding BreadcrumbPrimary}"
                   TextColor="{StaticResource TextSecondary}"
                   FontSize="12" />
            <Label Text=">"
                   TextColor="{StaticResource TextSecondary}"
                   FontSize="12" />
            <Label Text="{Binding BreadcrumbSecondary}"
                   TextColor="{StaticResource TextPrimary}"
                   FontSize="12"
                   FontAttributes="Bold" />
        </HorizontalStackLayout>

        <HorizontalStackLayout Grid.Column="1" Spacing="16">
            <Label Text="—" TextColor="{StaticResource TextSecondary}" />
            <Label Text="□" TextColor="{StaticResource TextSecondary}" />
            <Label Text="✕" TextColor="{StaticResource TextSecondary}" />
        </HorizontalStackLayout>
    </Grid>
</ContentView>


// File: Views/Shell/TopHeaderView.xaml.cs
namespace SurveyHub.Views.Shell;

public partial class TopHeaderView : ContentView
{
    public TopHeaderView()
    {
        InitializeComponent();
    }
}


// File: Views/Shared/StatusBadgeView.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentView
    x:Class="SurveyHub.Views.Shared.StatusBadgeView"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    x:Name="This">
    <Border BackgroundColor="{Binding BadgeColor, Source={x:Reference This}}"
            StrokeThickness="0"
            Padding="8,4"
            StrokeShape="RoundRectangle 12">
        <Label Text="{Binding Text, Source={x:Reference This}}"
               TextColor="{Binding BadgeTextColor, Source={x:Reference This}}"
               FontSize="11" />
    </Border>
</ContentView>


// File: Views/Shared/StatusBadgeView.xaml.cs
namespace SurveyHub.Views.Shared;

public partial class StatusBadgeView : ContentView
{
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(StatusBadgeView), string.Empty);

    public static readonly BindableProperty BadgeColorProperty =
        BindableProperty.Create(nameof(BadgeColor), typeof(Color), typeof(StatusBadgeView), Colors.LightGray);

    public static readonly BindableProperty BadgeTextColorProperty =
        BindableProperty.Create(nameof(BadgeTextColor), typeof(Color), typeof(StatusBadgeView), Colors.Black);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public Color BadgeColor
    {
        get => (Color)GetValue(BadgeColorProperty);
        set => SetValue(BadgeColorProperty, value);
    }

    public Color BadgeTextColor
    {
        get => (Color)GetValue(BadgeTextColorProperty);
        set => SetValue(BadgeTextColorProperty, value);
    }

    public StatusBadgeView()
    {
        InitializeComponent();
    }
}


// File: Views/Shared/MobilePreviewView.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentView
    x:Class="SurveyHub.Views.Shared.MobilePreviewView"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">

    <VerticalStackLayout Spacing="12">
        <Label Text="App preview" FontAttributes="Bold" />

        <Grid HorizontalOptions="Center">
            <Border WidthRequest="220"
                    HeightRequest="450"
                    Stroke="#1A1A1A"
                    StrokeThickness="2"
                    BackgroundColor="White"
                    StrokeShape="RoundRectangle 28"
                    Padding="16">
                <VerticalStackLayout Spacing="10">
                    <Label Text="{Binding SurveyTitle}" HorizontalOptions="Center" FontAttributes="Bold" />
                    <Grid ColumnDefinitions="*,Auto">
                        <Label Text="{Binding ProgressText}" FontSize="12" />
                        <Label Grid.Column="1" Text="{Binding PercentText}" FontSize="12" />
                    </Grid>
                    <BoxView HeightRequest="4" Color="#2BB673" />
                    <Label Text="{Binding QuestionNumberTitle}" FontAttributes="Bold" TextColor="#1C7D4D" />
                    <Label Text="{Binding Description}" FontSize="12" TextColor="{StaticResource TextSecondary}" />
                    <Button Text="Ja" />
                    <Button Text="Nej" />
                </VerticalStackLayout>
            </Border>
        </Grid>
    </VerticalStackLayout>
</ContentView>


// File: Views/Shared/MobilePreviewView.xaml.cs
namespace SurveyHub.Views.Shared;

public partial class MobilePreviewView : ContentView
{
    public MobilePreviewView()
    {
        InitializeComponent();
    }
}


// File: Views/Shared/SectionCardView.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentView
    x:Class="SurveyHub.Views.Shared.SectionCardView"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    x:Name="This">

    <Border Style="{StaticResource InnerCardStyle}">
        <VerticalStackLayout>
            <Grid Padding="12" ColumnDefinitions="Auto,*">
                <Label Text="⌄" />
                <Label Grid.Column="1"
                       Text="{Binding Title, Source={x:Reference This}}"
                       FontAttributes="Bold" />
            </Grid>

            <CollectionView ItemsSource="{Binding ItemsSource, Source={x:Reference This}}">
                <CollectionView.ItemTemplate>
                    <DataTemplate>
                        <Grid Padding="10,8" ColumnDefinitions="*,Auto">
                            <Label Text="{Binding DisplayTitle}" />
                            <Label Grid.Column="1"
                                   Text="{Binding TypeLabel}"
                                   FontSize="12"
                                   TextColor="{StaticResource TextSecondary}" />
                        </Grid>
                    </DataTemplate>
                </CollectionView.ItemTemplate>
            </CollectionView>
        </VerticalStackLayout>
    </Border>
</ContentView>


// File: Views/Shared/SectionCardView.xaml.cs
using System.Collections;

namespace SurveyHub.Views.Shared;

public partial class SectionCardView : ContentView
{
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(SectionCardView), string.Empty);

    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(SectionCardView), null);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public IEnumerable ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public SectionCardView()
    {
        InitializeComponent();
    }
}


// File: Views/Shared/QuestionPropertyPanel.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentView
    x:Class="SurveyHub.Views.Shared.QuestionPropertyPanel"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">

    <Border Style="{StaticResource InnerCardStyle}">
        <ScrollView>
            <VerticalStackLayout Padding="16" Spacing="14">
                <Label Text="Spørgsmålstype" Style="{StaticResource FormLabelStyle}" />
                <Picker ItemsSource="{Binding QuestionTypes}"
                        SelectedItem="{Binding SelectedQuestionType}" />

                <Label Text="Titel" Style="{StaticResource FormLabelStyle}" />
                <Entry Text="{Binding Title}" />

                <Label Text="Beskrivelse" Style="{StaticResource FormLabelStyle}" />
                <Editor Text="{Binding Description}" HeightRequest="100" />

                <HorizontalStackLayout Spacing="8">
                    <CheckBox IsChecked="{Binding IsRequired}" />
                    <Label Text="Påkrævet" VerticalOptions="Center" />
                </HorizontalStackLayout>

                <Label Text="Visning i app" Style="{StaticResource FormLabelStyle}" />
                <HorizontalStackLayout Spacing="16">
                    <HorizontalStackLayout Spacing="6">
                        <RadioButton IsChecked="{Binding IsStandardDisplay}" />
                        <Label Text="Standard" VerticalOptions="Center" />
                    </HorizontalStackLayout>
                    <HorizontalStackLayout Spacing="6">
                        <RadioButton IsChecked="{Binding IsCompactDisplay}" />
                        <Label Text="Kompakt" VerticalOptions="Center" />
                    </HorizontalStackLayout>
                </HorizontalStackLayout>

                <Label Text="Variabelnavn (valgfri)" Style="{StaticResource FormLabelStyle}" />
                <Entry Text="{Binding VariableName}" />
            </VerticalStackLayout>
        </ScrollView>
    </Border>
</ContentView>


// File: Views/Shared/QuestionPropertyPanel.xaml.cs
namespace SurveyHub.Views.Shared;

public partial class QuestionPropertyPanel : ContentView
{
    public QuestionPropertyPanel()
    {
        InitializeComponent();
    }
}


// File: Views/Shared/SettingsNavView.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentView
    x:Class="SurveyHub.Views.Shared.SettingsNavView"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">

    <Border Style="{StaticResource CardStyle}">
        <VerticalStackLayout Padding="12" Spacing="8">
            <Label Text="Generelt" />
            <Label Text="App-indstillinger" />
            <Label Text="Sikkerhed" />
            <Label Text="Notifikationer" />
            <Label Text="API &amp; integrationer" />
            <Label Text="Udseende" />
        </VerticalStackLayout>
    </Border>
</ContentView>


// File: Views/Shared/SettingsNavView.xaml.cs
namespace SurveyHub.Views.Shared;

public partial class SettingsNavView : ContentView
{
    public SettingsNavView()
    {
        InitializeComponent();
    }
}


// File: Views/Organizations/OrganizationsPage.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage
    x:Class="SurveyHub.Views.Organizations.OrganizationsPage"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    BackgroundColor="{StaticResource PageBackground}">

    <Grid RowDefinitions="Auto,Auto,*,Auto" Padding="24">
        <Grid ColumnDefinitions="*,Auto">
            <VerticalStackLayout>
                <Label Text="Organisationer" Style="{StaticResource PageTitleStyle}" />
                <Label Text="Administrative oversigt over organisationer, brands og deres surveys."
                       Style="{StaticResource PageSubtitleStyle}" />
            </VerticalStackLayout>
            <Button Grid.Column="1"
                    Text="+ Opret organisation"
                    Style="{StaticResource PrimaryButtonStyle}" />
        </Grid>

        <Border Grid.Row="1" Margin="0,20,0,16" Style="{StaticResource CardStyle}" Padding="12">
            <Entry Placeholder="Søg organisationer..." Text="{Binding SearchText}" />
        </Border>

        <Border Grid.Row="2" Style="{StaticResource CardStyle}">
            <Grid RowDefinitions="Auto,*">
                <Grid Padding="16" ColumnDefinitions="3*,*,*,*,*">
                    <Label Text="Navn" Style="{StaticResource DataTableHeaderStyle}" />
                    <Label Grid.Column="1" Text="Brands" Style="{StaticResource DataTableHeaderStyle}" />
                    <Label Grid.Column="2" Text="Surveys" Style="{StaticResource DataTableHeaderStyle}" />
                    <Label Grid.Column="3" Text="Brugere" Style="{StaticResource DataTableHeaderStyle}" />
                    <Label Grid.Column="4" Text="Sidst opdateret" Style="{StaticResource DataTableHeaderStyle}" />
                </Grid>

                <CollectionView Grid.Row="1" ItemsSource="{Binding Organizations}">
                    <CollectionView.ItemTemplate>
                        <DataTemplate>
                            <Grid Padding="16" ColumnDefinitions="3*,*,*,*,*">
                                <Label Text="{Binding Name}" />
                                <Label Grid.Column="1" Text="{Binding BrandCount}" />
                                <Label Grid.Column="2" Text="{Binding SurveyCount}" />
                                <Label Grid.Column="3" Text="{Binding UserCount}" />
                                <Label Grid.Column="4" Text="{Binding UpdatedText}" />
                            </Grid>
                        </DataTemplate>
                    </CollectionView.ItemTemplate>
                </CollectionView>
            </Grid>
        </Border>

        <HorizontalStackLayout Grid.Row="3" Margin="0,12,0,0" HorizontalOptions="End" Spacing="8">
            <Button Text="1" />
            <Button Text="2" />
            <Button Text="3" />
        </HorizontalStackLayout>
    </Grid>
</ContentPage>


// File: Views/Organizations/OrganizationsPage.xaml.cs
using SurveyHub.ViewModels;

namespace SurveyHub.Views.Organizations;

public partial class OrganizationsPage : ContentPage
{
    public OrganizationsPage(OrganizationsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


// File: Views/Organizations/OrganizationBrandsPage.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage
    x:Class="SurveyHub.Views.Organizations.OrganizationBrandsPage"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:shared="clr-namespace:SurveyHub.Views.Shared"
    BackgroundColor="{StaticResource PageBackground}">

    <Grid RowDefinitions="Auto,*" Padding="24">
        <Grid ColumnDefinitions="Auto,*,Auto" ColumnSpacing="16">
            <Border WidthRequest="56"
                    HeightRequest="56"
                    BackgroundColor="{StaticResource PrimaryBlue}"
                    StrokeThickness="0"
                    StrokeShape="RoundRectangle 12" />

            <VerticalStackLayout Grid.Column="1" Spacing="6">
                <HorizontalStackLayout Spacing="10">
                    <Label Text="Retail Group A" Style="{StaticResource PageTitleStyle}" />
                    <shared:StatusBadgeView Text="Aktiv"
                                            BadgeColor="{StaticResource SuccessGreenLight}"
                                            BadgeTextColor="{StaticResource SuccessGreen}" />
                </HorizontalStackLayout>

                <HorizontalStackLayout Spacing="24">
                    <Label Text="Overblik" Style="{StaticResource TabTextStyle}" />
                    <Label Text="Brands" Style="{StaticResource ActiveTabTextStyle}" />
                    <Label Text="Brugere" Style="{StaticResource TabTextStyle}" />
                    <Label Text="Indstillinger" Style="{StaticResource TabTextStyle}" />
                    <Label Text="App-udgivelse" Style="{StaticResource TabTextStyle}" />
                </HorizontalStackLayout>
            </VerticalStackLayout>

            <HorizontalStackLayout Grid.Column="2" Spacing="12">
                <Button Text="Gem ændringer" Style="{StaticResource PrimaryButtonStyle}" />
                <Button Text="Flere handlinger" Style="{StaticResource SecondaryButtonStyle}" />
            </HorizontalStackLayout>
        </Grid>

        <Grid Grid.Row="1"
              Margin="0,20,0,0"
              ColumnDefinitions="260,320,*"
              ColumnSpacing="16">

            <Border Grid.Column="0" Style="{StaticResource CardStyle}">
                <Grid RowDefinitions="Auto,*">
                    <Grid Padding="16" ColumnDefinitions="*,Auto">
                        <Label Text="Brands" Style="{StaticResource SectionHeaderStyle}" />
                        <Button Grid.Column="1" Text="+" WidthRequest="32" HeightRequest="32" />
                    </Grid>

                    <CollectionView Grid.Row="1" ItemsSource="{Binding Brands}">
                        <CollectionView.ItemTemplate>
                            <DataTemplate>
                                <Border Margin="12,6" Padding="12" Style="{StaticResource InnerCardStyle}">
                                    <VerticalStackLayout>
                                        <Label Text="{Binding Name}" FontAttributes="Bold" />
                                        <Label Text="{Binding SurveyCountText}"
                                               FontSize="12"
                                               TextColor="{StaticResource TextSecondary}" />
                                    </VerticalStackLayout>
                                </Border>
                            </DataTemplate>
                        </CollectionView.ItemTemplate>
                    </CollectionView>
                </Grid>
            </Border>

            <Border Grid.Column="1" Style="{StaticResource CardStyle}">
                <Grid RowDefinitions="Auto,*,Auto">
                    <Grid Padding="16" ColumnDefinitions="*,Auto">
                        <Label Text="Surveys for Coffee House" Style="{StaticResource SectionHeaderStyle}" />
                        <Button Grid.Column="1" Text="+ Opret survey" />
                    </Grid>

                    <CollectionView Grid.Row="1" ItemsSource="{Binding Surveys}">
                        <CollectionView.ItemTemplate>
                            <DataTemplate>
                                <Border Margin="12,6" Padding="12" Style="{StaticResource InnerCardStyle}">
                                    <VerticalStackLayout>
                                        <Label Text="{Binding Name}" FontAttributes="Bold" />
                                        <Label Text="{Binding VersionText}"
                                               FontSize="12"
                                               TextColor="{StaticResource TextSecondary}" />
                                    </VerticalStackLayout>
                                </Border>
                            </DataTemplate>
                        </CollectionView.ItemTemplate>
                    </CollectionView>

                    <shared:MobilePreviewView Grid.Row="2"
                                              Margin="16"
                                              BindingContext="{Binding Preview}" />
                </Grid>
            </Border>

            <Border Grid.Column="2" Style="{StaticResource CardStyle}">
                <Grid RowDefinitions="Auto,Auto,Auto,*">
                    <Grid Padding="16" ColumnDefinitions="Auto,*">
                        <Border WidthRequest="40"
                                HeightRequest="40"
                                BackgroundColor="{StaticResource PrimaryBlueLight}"
                                StrokeThickness="0"
                                StrokeShape="RoundRectangle 10" />
                        <VerticalStackLayout Grid.Column="1" Margin="12,0,0,0" Spacing="4">
                            <HorizontalStackLayout Spacing="10">
                                <Label Text="Butiksinspektion"
                                       FontSize="20"
                                       FontAttributes="Bold" />
                                <shared:StatusBadgeView Text="Version 3 (Aktiv)"
                                                        BadgeColor="{StaticResource SuccessGreenLight}"
                                                        BadgeTextColor="{StaticResource SuccessGreen}" />
                            </HorizontalStackLayout>
                            <Label Text="Standard inspection of store operations and presentation."
                                   TextColor="{StaticResource TextSecondary}"
                                   FontSize="12" />
                        </VerticalStackLayout>
                    </Grid>

                    <HorizontalStackLayout Grid.Row="1" Spacing="24" Padding="16,0">
                        <Label Text="Byg" Style="{StaticResource ActiveTabTextStyle}" />
                        <Label Text="Logik" Style="{StaticResource TabTextStyle}" />
                        <Label Text="Indstillinger" Style="{StaticResource TabTextStyle}" />
                        <Label Text="Oversættelser" Style="{StaticResource TabTextStyle}" />
                        <Label Text="Versioner" Style="{StaticResource TabTextStyle}" />
                    </HorizontalStackLayout>

                    <HorizontalStackLayout Grid.Row="2" Spacing="8" Padding="16,12">
                        <Button Text="Section" />
                        <Button Text="Spørgsmål" />
                        <Button Text="Indhold" />
                        <Button Text="Side" />
                    </HorizontalStackLayout>

                    <Grid Grid.Row="3" ColumnDefinitions="*,300" ColumnSpacing="16" Padding="16">
                        <ScrollView Grid.Column="0">
                            <VerticalStackLayout Spacing="12">
                                <shared:SectionCardView Title="1. Butiksinformation"
                                                        ItemsSource="{Binding Section1Questions}" />
                                <shared:SectionCardView Title="2. Eksteriør"
                                                        ItemsSource="{Binding Section2Questions}" />
                                <shared:SectionCardView Title="3. Indretning &amp; præsentation"
                                                        ItemsSource="{Binding Section3Questions}" />
                                <Button Text="+ Tilføj sektion her" />
                            </VerticalStackLayout>
                        </ScrollView>

                        <shared:QuestionPropertyPanel Grid.Column="1"
                                                      BindingContext="{Binding SelectedQuestion}" />
                    </Grid>
                </Grid>
            </Border>
        </Grid>
    </Grid>
</ContentPage>


// File: Views/Organizations/OrganizationBrandsPage.xaml.cs
using SurveyHub.ViewModels;

namespace SurveyHub.Views.Organizations;

public partial class OrganizationBrandsPage : ContentPage
{
    public OrganizationBrandsPage(OrganizationBrandsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


// File: Views/Organizations/OrganizationUsersPage.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage
    x:Class="SurveyHub.Views.Organizations.OrganizationUsersPage"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:shared="clr-namespace:SurveyHub.Views.Shared"
    BackgroundColor="{StaticResource PageBackground}">

    <Grid RowDefinitions="Auto,Auto,Auto,*,Auto" Padding="24">
        <VerticalStackLayout>
            <Label Text="Brugere" Style="{StaticResource PageTitleStyle}" />
            <Label Text="Administrer brugere i organisationen."
                   Style="{StaticResource PageSubtitleStyle}" />
        </VerticalStackLayout>

        <HorizontalStackLayout Grid.Row="1" Spacing="24" Margin="0,16,0,0">
            <Label Text="Brugere" Style="{StaticResource ActiveTabTextStyle}" />
            <Label Text="Inviterede" Style="{StaticResource TabTextStyle}" />
        </HorizontalStackLayout>

        <Grid Grid.Row="2" Margin="0,16,0,16" ColumnDefinitions="*,160,160,Auto" ColumnSpacing="12">
            <Entry Placeholder="Søg brugere..." Text="{Binding SearchText}" />
            <Picker Grid.Column="1" Title="Rolle" ItemsSource="{Binding Roles}" />
            <Picker Grid.Column="2" Title="Status" ItemsSource="{Binding Statuses}" />
            <Button Grid.Column="3" Text="+ Inviter bruger" Style="{StaticResource PrimaryButtonStyle}" />
        </Grid>

        <Border Grid.Row="3" Style="{StaticResource CardStyle}">
            <Grid RowDefinitions="Auto,*">
                <Grid Padding="16" ColumnDefinitions="2*,3*,*,*,*">
                    <Label Text="Navn" Style="{StaticResource DataTableHeaderStyle}" />
                    <Label Grid.Column="1" Text="Email" Style="{StaticResource DataTableHeaderStyle}" />
                    <Label Grid.Column="2" Text="Rolle" Style="{StaticResource DataTableHeaderStyle}" />
                    <Label Grid.Column="3" Text="Status" Style="{StaticResource DataTableHeaderStyle}" />
                    <Label Grid.Column="4" Text="Sidst aktiv" Style="{StaticResource DataTableHeaderStyle}" />
                </Grid>

                <CollectionView Grid.Row="1" ItemsSource="{Binding Users}">
                    <CollectionView.ItemTemplate>
                        <DataTemplate>
                            <Grid Padding="16" ColumnDefinitions="2*,3*,*,*,*">
                                <Label Text="{Binding Name}" />
                                <Label Grid.Column="1" Text="{Binding Email}" />
                                <Label Grid.Column="2" Text="{Binding Role}" />
                                <shared:StatusBadgeView Grid.Column="3"
                                                        Text="{Binding Status}"
                                                        BadgeColor="{StaticResource SuccessGreenLight}"
                                                        BadgeTextColor="{StaticResource SuccessGreen}" />
                                <Label Grid.Column="4" Text="{Binding LastActiveText}" />
                            </Grid>
                        </DataTemplate>
                    </CollectionView.ItemTemplate>
                </CollectionView>
            </Grid>
        </Border>

        <HorizontalStackLayout Grid.Row="4" HorizontalOptions="End" Spacing="8" Margin="0,12,0,0">
            <Button Text="1" />
            <Button Text="2" />
            <Button Text="3" />
        </HorizontalStackLayout>
    </Grid>
</ContentPage>


// File: Views/Organizations/OrganizationUsersPage.xaml.cs
using SurveyHub.ViewModels;

namespace SurveyHub.Views.Organizations;

public partial class OrganizationUsersPage : ContentPage
{
    public OrganizationUsersPage(OrganizationUsersViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


// File: Views/Templates/TemplatesPage.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage
    x:Class="SurveyHub.Views.Templates.TemplatesPage"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    BackgroundColor="{StaticResource PageBackground}">

    <Grid RowDefinitions="Auto,Auto,Auto,*" Padding="24">
        <Grid ColumnDefinitions="*,Auto">
            <VerticalStackLayout>
                <Label Text="Skabeloner" Style="{StaticResource PageTitleStyle}" />
                <Label Text="Opret og administrer skabeloner til spørgeskemaer og tjeklister."
                       Style="{StaticResource PageSubtitleStyle}" />
            </VerticalStackLayout>
            <Button Grid.Column="1" Text="+ Ny skabelon" Style="{StaticResource PrimaryButtonStyle}" />
        </Grid>

        <HorizontalStackLayout Grid.Row="1" Margin="0,16,0,0" Spacing="24">
            <Label Text="Skabeloner" Style="{StaticResource ActiveTabTextStyle}" />
            <Label Text="Gruppering" Style="{StaticResource TabTextStyle}" />
        </HorizontalStackLayout>

        <Border Grid.Row="2" Margin="0,16,0,16" Style="{StaticResource CardStyle}" Padding="12">
            <Entry Placeholder="Søg skabeloner..." Text="{Binding SearchText}" />
        </Border>

        <Border Grid.Row="3" Style="{StaticResource CardStyle}">
            <Grid RowDefinitions="Auto,*">
                <Grid Padding="16" ColumnDefinitions="2*,*,3*,*">
                    <Label Text="Navn" Style="{StaticResource DataTableHeaderStyle}" />
                    <Label Grid.Column="1" Text="Type" Style="{StaticResource DataTableHeaderStyle}" />
                    <Label Grid.Column="2" Text="Beskrivelse" Style="{StaticResource DataTableHeaderStyle}" />
                    <Label Grid.Column="3" Text="Sidst opdateret" Style="{StaticResource DataTableHeaderStyle}" />
                </Grid>

                <CollectionView Grid.Row="1" ItemsSource="{Binding Templates}">
                    <CollectionView.ItemTemplate>
                        <DataTemplate>
                            <Grid Padding="16" ColumnDefinitions="2*,*,3*,*">
                                <Label Text="{Binding Name}" />
                                <Label Grid.Column="1" Text="{Binding Type}" />
                                <Label Grid.Column="2" Text="{Binding Description}" />
                                <Label Grid.Column="3" Text="{Binding UpdatedText}" />
                            </Grid>
                        </DataTemplate>
                    </CollectionView.ItemTemplate>
                </CollectionView>
            </Grid>
        </Border>
    </Grid>
</ContentPage>


// File: Views/Templates/TemplatesPage.xaml.cs
using SurveyHub.ViewModels;

namespace SurveyHub.Views.Templates;

public partial class TemplatesPage : ContentPage
{
    public TemplatesPage(TemplatesViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


// File: Views/Variables/VariablesPage.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage
    x:Class="SurveyHub.Views.Variables.VariablesPage"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    BackgroundColor="{StaticResource PageBackground}">

    <Grid RowDefinitions="Auto,Auto,*" Padding="24">
        <Grid ColumnDefinitions="*,Auto">
            <VerticalStackLayout>
                <Label Text="Variabler" Style="{StaticResource PageTitleStyle}" />
                <Label Text="Opret variabler som kan bruges dynamisk i spørgsmål."
                       Style="{StaticResource PageSubtitleStyle}" />
            </VerticalStackLayout>
            <Button Grid.Column="1" Text="+ Ny variabel" Style="{StaticResource PrimaryButtonStyle}" />
        </Grid>

        <HorizontalStackLayout Grid.Row="1" Margin="0,16,0,16" Spacing="24">
            <Label Text="Variabler" Style="{StaticResource ActiveTabTextStyle}" />
            <Label Text="Variabelsæt" Style="{StaticResource TabTextStyle}" />
        </HorizontalStackLayout>

        <Border Grid.Row="2" Style="{StaticResource CardStyle}">
            <Grid RowDefinitions="Auto,*">
                <Grid Padding="16" ColumnDefinitions="2*,2*,*,*,*">
                    <Label Text="Navn" Style="{StaticResource DataTableHeaderStyle}" />
                    <Label Grid.Column="1" Text="Nøgle" Style="{StaticResource DataTableHeaderStyle}" />
                    <Label Grid.Column="2" Text="Type" Style="{StaticResource DataTableHeaderStyle}" />
                    <Label Grid.Column="3" Text="Standardværdi" Style="{StaticResource DataTableHeaderStyle}" />
                    <Label Grid.Column="4" Text="Bruges i" Style="{StaticResource DataTableHeaderStyle}" />
                </Grid>

                <CollectionView Grid.Row="1" ItemsSource="{Binding Variables}">
                    <CollectionView.ItemTemplate>
                        <DataTemplate>
                            <Grid Padding="16" ColumnDefinitions="2*,2*,*,*,*">
                                <Label Text="{Binding Name}" />
                                <Label Grid.Column="1" Text="{Binding Key}" />
                                <Label Grid.Column="2" Text="{Binding Type}" />
                                <Label Grid.Column="3" Text="{Binding DefaultValue}" />
                                <Label Grid.Column="4" Text="{Binding UsageCountText}" />
                            </Grid>
                        </DataTemplate>
                    </CollectionView.ItemTemplate>
                </CollectionView>
            </Grid>
        </Border>
    </Grid>
</ContentPage>


// File: Views/Variables/VariablesPage.xaml.cs
using SurveyHub.ViewModels;

namespace SurveyHub.Views.Variables;

public partial class VariablesPage : ContentPage
{
    public VariablesPage(VariablesViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


// File: Views/Media/MediaCardView.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentView
    x:Class="SurveyHub.Views.Media.MediaCardView"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">

    <Border Style="{StaticResource InnerCardStyle}"
            Padding="8"
            Margin="6">
        <VerticalStackLayout Spacing="8">
            <Border HeightRequest="110"
                    BackgroundColor="#F3F6FB"
                    StrokeThickness="0"
                    StrokeShape="RoundRectangle 8">
                <Image Source="{Binding ThumbnailUrl}" Aspect="AspectFill" />
            </Border>
            <Label Text="{Binding Name}" FontAttributes="Bold" LineBreakMode="TailTruncation" />
            <Label Text="{Binding MetaText}" FontSize="12" TextColor="{StaticResource TextSecondary}" />
        </VerticalStackLayout>
    </Border>
</ContentView>


// File: Views/Media/MediaCardView.xaml.cs
namespace SurveyHub.Views.Media;

public partial class MediaCardView : ContentView
{
    public MediaCardView()
    {
        InitializeComponent();
    }
}


// File: Views/Media/MediaLibraryPage.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage
    x:Class="SurveyHub.Views.Media.MediaLibraryPage"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:media="clr-namespace:SurveyHub.Views.Media"
    BackgroundColor="{StaticResource PageBackground}">

    <Grid RowDefinitions="Auto,Auto,Auto,*,Auto" Padding="24">
        <Grid ColumnDefinitions="*,Auto">
            <VerticalStackLayout>
                <Label Text="Mediebibliotek" Style="{StaticResource PageTitleStyle}" />
                <Label Text="Upload og administrer billeder og filer."
                       Style="{StaticResource PageSubtitleStyle}" />
            </VerticalStackLayout>
            <Button Grid.Column="1" Text="Upload fil" Style="{StaticResource PrimaryButtonStyle}" />
        </Grid>

        <HorizontalStackLayout Grid.Row="1" Margin="0,16,0,0" Spacing="24">
            <Label Text="Filer" Style="{StaticResource ActiveTabTextStyle}" />
            <Label Text="Mapper" Style="{StaticResource TabTextStyle}" />
        </HorizontalStackLayout>

        <Grid Grid.Row="2" Margin="0,16,0,16" ColumnDefinitions="*,180,Auto" ColumnSpacing="12">
            <Entry Placeholder="Søg filer..." Text="{Binding SearchText}" />
            <Picker Grid.Column="1" Title="Type" ItemsSource="{Binding FileTypes}" />
            <Button Grid.Column="2" Text="Upload fil" Style="{StaticResource PrimaryButtonStyle}" />
        </Grid>

        <CollectionView Grid.Row="3"
                        ItemsSource="{Binding MediaItems}">
            <CollectionView.ItemsLayout>
                <GridItemsLayout Orientation="Vertical" Span="4" />
            </CollectionView.ItemsLayout>
            <CollectionView.ItemTemplate>
                <DataTemplate>
                    <media:MediaCardView />
                </DataTemplate>
            </CollectionView.ItemTemplate>
        </CollectionView>

        <HorizontalStackLayout Grid.Row="4" HorizontalOptions="End" Spacing="8">
            <Button Text="1" />
            <Button Text="2" />
            <Button Text="3" />
        </HorizontalStackLayout>
    </Grid>
</ContentPage>


// File: Views/Media/MediaLibraryPage.xaml.cs
using SurveyHub.ViewModels;

namespace SurveyHub.Views.Media;

public partial class MediaLibraryPage : ContentPage
{
    public MediaLibraryPage(MediaLibraryViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


// File: Views/Settings/SettingsGeneralPage.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage
    x:Class="SurveyHub.Views.Settings.SettingsGeneralPage"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:shared="clr-namespace:SurveyHub.Views.Shared"
    BackgroundColor="{StaticResource PageBackground}">

    <Grid ColumnDefinitions="220,*" Padding="24" ColumnSpacing="16">
        <shared:SettingsNavView Grid.Column="0" />

        <Grid Grid.Column="1" RowDefinitions="Auto,*">
            <Grid ColumnDefinitions="*,Auto" Margin="0,0,0,16">
                <Label Text="Indstillinger" Style="{StaticResource PageTitleStyle}" />
                <HorizontalStackLayout Grid.Column="1" Spacing="12">
                    <Button Text="Gem ændringer" Style="{StaticResource PrimaryButtonStyle}" />
                    <Button Text="Flere handlinger" Style="{StaticResource SecondaryButtonStyle}" />
                </HorizontalStackLayout>
            </Grid>

            <Border Grid.Row="1" Style="{StaticResource CardStyle}">
                <ScrollView>
                    <VerticalStackLayout Padding="24" Spacing="16">
                        <Label Text="Organisationsnavn" Style="{StaticResource FormLabelStyle}" />
                        <Entry Text="{Binding OrganizationName}" />

                        <Label Text="Primært sprog" Style="{StaticResource FormLabelStyle}" />
                        <Picker ItemsSource="{Binding Languages}" SelectedItem="{Binding SelectedLanguage}" />

                        <Label Text="Tidszone" Style="{StaticResource FormLabelStyle}" />
                        <Picker ItemsSource="{Binding Timezones}" SelectedItem="{Binding SelectedTimezone}" />

                        <Label Text="Dashboard" Style="{StaticResource FormLabelStyle}" />
                        <Entry Text="{Binding DashboardName}" />

                        <Label Text="Datoformat" Style="{StaticResource FormLabelStyle}" />
                        <Entry Text="{Binding DateFormat}" />

                        <Label Text="Logo" Style="{StaticResource FormLabelStyle}" />
                        <HorizontalStackLayout Spacing="16">
                            <Border WidthRequest="96"
                                    HeightRequest="96"
                                    BackgroundColor="{StaticResource PrimaryBlue}"
                                    StrokeThickness="0"
                                    StrokeShape="RoundRectangle 12" />
                            <VerticalStackLayout>
                                <Button Text="Skift logo" />
                                <Button Text="Fjern" />
                            </VerticalStackLayout>
                        </HorizontalStackLayout>
                    </VerticalStackLayout>
                </ScrollView>
            </Border>
        </Grid>
    </Grid>
</ContentPage>


// File: Views/Settings/SettingsGeneralPage.xaml.cs
using SurveyHub.ViewModels;

namespace SurveyHub.Views.Settings;

public partial class SettingsGeneralPage : ContentPage
{
    public SettingsGeneralPage(SettingsGeneralViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


// File: Views/Settings/SettingsAppPage.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage
    x:Class="SurveyHub.Views.Settings.SettingsAppPage"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:shared="clr-namespace:SurveyHub.Views.Shared"
    BackgroundColor="{StaticResource PageBackground}">

    <Grid ColumnDefinitions="220,*" Padding="24" ColumnSpacing="16">
        <shared:SettingsNavView Grid.Column="0" />

        <Grid Grid.Column="1" RowDefinitions="Auto,*">
            <Grid ColumnDefinitions="*,Auto" Margin="0,0,0,16">
                <Label Text="App-indstillinger" Style="{StaticResource PageTitleStyle}" />
                <Button Grid.Column="1" Text="Gem ændringer" Style="{StaticResource PrimaryButtonStyle}" />
            </Grid>

            <Border Grid.Row="1" Style="{StaticResource CardStyle}">
                <ScrollView>
                    <VerticalStackLayout Padding="24" Spacing="18">
                        <Border Style="{StaticResource InnerCardStyle}" Padding="16">
                            <VerticalStackLayout Spacing="10">
                                <Label Text="Offline" FontAttributes="Bold" />
                                <HorizontalStackLayout>
                                    <CheckBox IsChecked="{Binding OfflineEnabled}" />
                                    <Label Text="Tillad offline udfyldelse" VerticalOptions="Center" />
                                </HorizontalStackLayout>
                                <HorizontalStackLayout>
                                    <CheckBox IsChecked="{Binding AutoSyncEnabled}" />
                                    <Label Text="Synkroniser automatisk når forbindelse er tilgængelig" VerticalOptions="Center" />
                                </HorizontalStackLayout>
                            </VerticalStackLayout>
                        </Border>

                        <Border Style="{StaticResource InnerCardStyle}" Padding="16">
                            <VerticalStackLayout Spacing="10">
                                <Label Text="Placering" FontAttributes="Bold" />
                                <HorizontalStackLayout>
                                    <CheckBox IsChecked="{Binding RequireLocationAtStart}" />
                                    <Label Text="Kræv lokationsdata ved start af survey" VerticalOptions="Center" />
                                </HorizontalStackLayout>
                                <HorizontalStackLayout>
                                    <CheckBox IsChecked="{Binding UseLocationOnObservations}" />
                                    <Label Text="Brug lokation til geotagging af besvarelser" VerticalOptions="Center" />
                                </HorizontalStackLayout>
                            </VerticalStackLayout>
                        </Border>

                        <Border Style="{StaticResource InnerCardStyle}" Padding="16">
                            <VerticalStackLayout Spacing="10">
                                <Label Text="Medier" FontAttributes="Bold" />
                                <HorizontalStackLayout>
                                    <CheckBox IsChecked="{Binding CompressImages}" />
                                    <Label Text="Komprimer billeder før upload" VerticalOptions="Center" />
                                </HorizontalStackLayout>
                                <Label Text="Maks. billedstørrelse" Style="{StaticResource FormLabelStyle}" />
                                <Picker ItemsSource="{Binding MaxFileSizeOptions}"
                                        SelectedItem="{Binding SelectedMaxFileSize}" />
                            </VerticalStackLayout>
                        </Border>

                        <Border Style="{StaticResource InnerCardStyle}" Padding="16">
                            <VerticalStackLayout Spacing="10">
                                <Label Text="Navigation" FontAttributes="Bold" />
                                <HorizontalStackLayout>
                                    <CheckBox IsChecked="{Binding ShowSurveyProgress}" />
                                    <Label Text="Vis survey fremdrift" VerticalOptions="Center" />
                                </HorizontalStackLayout>
                                <HorizontalStackLayout>
                                    <CheckBox IsChecked="{Binding AllowSkipForward}" />
                                    <Label Text="Tillad spring fremad i survey" VerticalOptions="Center" />
                                </HorizontalStackLayout>
                            </VerticalStackLayout>
                        </Border>
                    </VerticalStackLayout>
                </ScrollView>
            </Border>
        </Grid>
    </Grid>
</ContentPage>


// File: Views/Settings/SettingsAppPage.xaml.cs
using SurveyHub.ViewModels;

namespace SurveyHub.Views.Settings;

public partial class SettingsAppPage : ContentPage
{
    public SettingsAppPage(SettingsAppViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


// File: Views/Roles/RolesPermissionsPage.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage
    x:Class="SurveyHub.Views.Roles.RolesPermissionsPage"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    BackgroundColor="{StaticResource PageBackground}">

    <Grid RowDefinitions="Auto,Auto,*" Padding="24">
        <Grid ColumnDefinitions="*,Auto">
            <VerticalStackLayout>
                <Label Text="Roller &amp; rettigheder" Style="{StaticResource PageTitleStyle}" />
                <Label Text="Definér roller og hvad brugere har adgang til."
                       Style="{StaticResource PageSubtitleStyle}" />
            </VerticalStackLayout>
            <Button Grid.Column="1" Text="+ Ny rolle" Style="{StaticResource PrimaryButtonStyle}" />
        </Grid>

        <HorizontalStackLayout Grid.Row="1" Margin="0,16,0,16" Spacing="24">
            <Label Text="Roller" Style="{StaticResource ActiveTabTextStyle}" />
            <Label Text="Rettigheder" Style="{StaticResource TabTextStyle}" />
        </HorizontalStackLayout>

        <Grid Grid.Row="2" ColumnDefinitions="320,*" ColumnSpacing="16">
            <Border Grid.Column="0" Style="{StaticResource CardStyle}">
                <Grid RowDefinitions="Auto,*">
                    <Grid Padding="16" ColumnDefinitions="*,*">
                        <Label Text="Rollenavn" Style="{StaticResource DataTableHeaderStyle}" />
                        <Label Grid.Column="1" Text="Beskrivelse" Style="{StaticResource DataTableHeaderStyle}" />
                    </Grid>

                    <CollectionView Grid.Row="1"
                                    ItemsSource="{Binding Roles}"
                                    SelectionMode="Single"
                                    SelectedItem="{Binding SelectedRole}">
                        <CollectionView.ItemTemplate>
                            <DataTemplate>
                                <Grid Padding="16" ColumnDefinitions="*,*">
                                    <Label Text="{Binding Name}" />
                                    <Label Grid.Column="1" Text="{Binding Description}" />
                                </Grid>
                            </DataTemplate>
                        </CollectionView.ItemTemplate>
                    </CollectionView>
                </Grid>
            </Border>

            <Border Grid.Column="1" Style="{StaticResource CardStyle}">
                <ScrollView>
                    <VerticalStackLayout Padding="24" Spacing="16">
                        <Label Text="{Binding SelectedRole.Name}" FontAttributes="Bold" FontSize="18" />

                        <VerticalStackLayout BindableLayout.ItemsSource="{Binding PermissionGroups}">
                            <BindableLayout.ItemTemplate>
                                <DataTemplate>
                                    <Border Style="{StaticResource InnerCardStyle}" Padding="16" Margin="0,0,0,8">
                                        <VerticalStackLayout Spacing="10">
                                            <Label Text="{Binding Name}" FontAttributes="Bold" />
                                            <VerticalStackLayout BindableLayout.ItemsSource="{Binding Permissions}">
                                                <BindableLayout.ItemTemplate>
                                                    <DataTemplate>
                                                        <HorizontalStackLayout Spacing="8">
                                                            <CheckBox IsChecked="{Binding IsEnabled}" />
                                                            <Label Text="{Binding Label}" VerticalOptions="Center" />
                                                        </HorizontalStackLayout>
                                                    </DataTemplate>
                                                </BindableLayout.ItemTemplate>
                                            </VerticalStackLayout>
                                        </VerticalStackLayout>
                                    </Border>
                                </DataTemplate>
                            </BindableLayout.ItemTemplate>
                        </VerticalStackLayout>
                    </VerticalStackLayout>
                </ScrollView>
            </Border>
        </Grid>
    </Grid>
</ContentPage>


// File: Views/Roles/RolesPermissionsPage.xaml.cs
using SurveyHub.ViewModels;

namespace SurveyHub.Views.Roles;

public partial class RolesPermissionsPage : ContentPage
{
    public RolesPermissionsPage(RolesPermissionsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


// File: Views/ActivityLog/ActivityLogPage.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage
    x:Class="SurveyHub.Views.ActivityLog.ActivityLogPage"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    BackgroundColor="{StaticResource PageBackground}">

    <Grid RowDefinitions="Auto,Auto,*,Auto" Padding="24">
        <VerticalStackLayout>
            <Label Text="Aktivitetslog" Style="{StaticResource PageTitleStyle}" />
            <Label Text="Se ændringer og aktiviteter i organisationen."
                   Style="{StaticResource PageSubtitleStyle}" />
        </VerticalStackLayout>

        <Grid Grid.Row="1" Margin="0,16,0,16" ColumnDefinitions="*,160,160,160" ColumnSpacing="12">
            <Entry Placeholder="Søg log..." Text="{Binding SearchText}" />
            <Picker Grid.Column="1" Title="Handling" ItemsSource="{Binding Actions}" />
            <Picker Grid.Column="2" Title="Bruger" ItemsSource="{Binding Users}" />
            <Picker Grid.Column="3" Title="Periode" ItemsSource="{Binding Periods}" />
        </Grid>

        <Border Grid.Row="2" Style="{StaticResource CardStyle}">
            <Grid RowDefinitions="Auto,*">
                <Grid Padding="16" ColumnDefinitions="2*,2*,*,3*">
                    <Label Text="Tidspunkt" Style="{StaticResource DataTableHeaderStyle}" />
                    <Label Grid.Column="1" Text="Handling" Style="{StaticResource DataTableHeaderStyle}" />
                    <Label Grid.Column="2" Text="Bruger" Style="{StaticResource DataTableHeaderStyle}" />
                    <Label Grid.Column="3" Text="Detaljer" Style="{StaticResource DataTableHeaderStyle}" />
                </Grid>

                <CollectionView Grid.Row="1" ItemsSource="{Binding ActivityEvents}">
                    <CollectionView.ItemTemplate>
                        <DataTemplate>
                            <Grid Padding="16" ColumnDefinitions="2*,2*,*,3*">
                                <Label Text="{Binding TimestampText}" />
                                <Label Grid.Column="1" Text="{Binding Action}" />
                                <Label Grid.Column="2" Text="{Binding UserName}" />
                                <Label Grid.Column="3" Text="{Binding Details}" />
                            </Grid>
                        </DataTemplate>
                    </CollectionView.ItemTemplate>
                </CollectionView>
            </Grid>
        </Border>

        <HorizontalStackLayout Grid.Row="3" HorizontalOptions="End" Spacing="8" Margin="0,12,0,0">
            <Button Text="1" />
            <Button Text="2" />
            <Button Text="3" />
        </HorizontalStackLayout>
    </Grid>
</ContentPage>


// File: Views/ActivityLog/ActivityLogPage.xaml.cs
using SurveyHub.ViewModels;

namespace SurveyHub.Views.ActivityLog;

public partial class ActivityLogPage : ContentPage
{
    public ActivityLogPage(ActivityLogViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}