using Feedback2Business.ViewModels;

namespace Feedback2Business.Views.Settings;

public partial class SettingsAppearancePage : ContentPage
{
    public SettingsAppearancePage(SettingsAppearanceViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
