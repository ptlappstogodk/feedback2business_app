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


