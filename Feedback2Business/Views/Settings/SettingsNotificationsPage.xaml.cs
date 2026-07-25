using Feedback2Business.ViewModels;

namespace Feedback2Business.Views.Settings;

public partial class SettingsNotificationsPage : ContentPage
{
    public SettingsNotificationsPage(SettingsNotificationsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
