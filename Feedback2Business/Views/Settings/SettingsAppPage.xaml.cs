using Feedback2Business.ViewModels;

namespace Feedback2Business.Views.Settings;

public partial class SettingsAppPage : ContentPage
{
    public SettingsAppPage(SettingsAppViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


