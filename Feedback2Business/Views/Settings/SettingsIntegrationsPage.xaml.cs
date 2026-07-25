using Feedback2Business.ViewModels;

namespace Feedback2Business.Views.Settings;

public partial class SettingsIntegrationsPage : ContentPage
{
    public SettingsIntegrationsPage(SettingsIntegrationsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
