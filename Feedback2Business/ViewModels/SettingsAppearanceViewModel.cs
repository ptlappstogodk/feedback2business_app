using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Feedback2Business.Services;

namespace Feedback2Business.ViewModels;

public class SettingsAppearanceViewModel : ObservableObject
{
    private string _selectedTheme = "Lys tema (Standard)";
    private string _primaryAccentColor = "#0284C7";
    private bool _compactDensity = false;
    private bool _darkNavySidebar = true;

    public ObservableCollection<string> Themes { get; } = new()
    {
        "Lys tema (Standard)",
        "Mørk tema",
        "System standard"
    };

    public string SelectedTheme
    {
        get => _selectedTheme;
        set => SetProperty(ref _selectedTheme, value);
    }

    public string PrimaryAccentColor
    {
        get => _primaryAccentColor;
        set => SetProperty(ref _primaryAccentColor, value);
    }

    public bool CompactDensity
    {
        get => _compactDensity;
        set => SetProperty(ref _compactDensity, value);
    }

    public bool DarkNavySidebar
    {
        get => _darkNavySidebar;
        set => SetProperty(ref _darkNavySidebar, value);
    }

    public ICommand SaveAppearanceSettingsCommand { get; }

    public SettingsAppearanceViewModel(IMockDataService data)
    {
        SaveAppearanceSettingsCommand = new RelayCommand(async () => await SaveAsync());
    }

    private async Task SaveAsync()
    {
        await Application.Current!.MainPage!.DisplayAlert("Udseende", "Udseendesindstillinger er gemt.", "OK");
    }
}
