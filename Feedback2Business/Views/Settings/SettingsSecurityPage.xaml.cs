using Feedback2Business.ViewModels;

namespace Feedback2Business.Views.Settings;

public partial class SettingsSecurityPage : ContentPage
{
    public SettingsSecurityPage(SettingsSecurityViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
