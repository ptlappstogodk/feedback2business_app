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


