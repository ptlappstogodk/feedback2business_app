using System;
using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;
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

        if (Application.Current?.Handler?.MauiContext?.Services != null)
        {
            var serviceVm = Application.Current.Handler.MauiContext.Services.GetService<MainShellViewModel>();
            if (serviceVm != null) return serviceVm;
        }

        if (Microsoft.Maui.Controls.Shell.Current?.CurrentPage?.BindingContext is MainShellViewModel shellVm)
        {
            return shellVm;
        }

        return null;
    }

    private void Navigate(string key)
    {
        var shellVm = GetShellViewModel();
        shellVm?.RequestNavigation(key);
    }

    private void OnGenereltClicked(object? sender, EventArgs e) => Navigate("SettingsGeneral");
    private void OnAppClicked(object? sender, EventArgs e) => Navigate("SettingsApp");
    private void OnSecurityClicked(object? sender, EventArgs e) => Navigate("SettingsSecurity");
    private void OnNotificationsClicked(object? sender, EventArgs e) => Navigate("SettingsNotifications");
    private void OnIntegrationsClicked(object? sender, EventArgs e) => Navigate("SettingsIntegrations");
    private void OnAppearanceClicked(object? sender, EventArgs e) => Navigate("SettingsAppearance");
}


