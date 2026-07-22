using Feedback2Business.ViewModels;

namespace Feedback2Business.Views.Settings;

public partial class SettingsGeneralPage : ContentPage
{
    public SettingsGeneralPage(SettingsGeneralViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


