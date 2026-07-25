using System;
using Microsoft.Maui.Controls;
using Feedback2Business.ViewModels;

namespace Feedback2Business.Views.Shared;

public partial class SettingsNavView : ContentView
{
    public SettingsNavView()
    {
        InitializeComponent();
    }

    private MainShellViewModel? GetShellViewModel()
    {
        if (BindingContext is MainShellViewModel vm) return vm;
        if (Parent is VisualElement parent && parent.BindingContext is MainShellViewModel pVm) return pVm;
        if (Application.Current?.MainPage?.BindingContext is MainShellViewModel mVm) return mVm;
        return null;
    }

    private void Navigate(string key)
    {
        var shellVm = GetShellViewModel();
        shellVm?.RequestNavigation(key);
    }

    private void OnGenereltTapped(object sender, TappedEventArgs e) => Navigate("SettingsGeneral");
    private void OnAppTapped(object sender, TappedEventArgs e) => Navigate("SettingsApp");
    private void OnSecurityTapped(object sender, TappedEventArgs e) => Navigate("SettingsSecurity");
    private void OnNotificationsTapped(object sender, TappedEventArgs e) => Navigate("SettingsNotifications");
    private void OnIntegrationsTapped(object sender, TappedEventArgs e) => Navigate("SettingsIntegrations");
    private void OnAppearanceTapped(object sender, TappedEventArgs e) => Navigate("SettingsAppearance");
}


