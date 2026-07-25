namespace Feedback2Business.Views.Shell;

public partial class SidebarView : ContentView
{
    public event EventHandler<string>? NavigationRequested;

    public SidebarView()
    {
        InitializeComponent();
    }

    private void OnPointerEntered(object? sender, PointerEventArgs e)
    {
        if (sender is Border border)
        {
            border.BackgroundColor = Color.FromArgb("#0D2D52");
        }
    }

    private void OnPointerExited(object? sender, PointerEventArgs e)
    {
        if (sender is Border border)
        {
            border.BackgroundColor = Colors.Transparent;
        }
    }

    private void Raise(string key) => NavigationRequested?.Invoke(this, key);

    private void Overview_Clicked(object sender, EventArgs e) => Raise("Overview");
    private void Organizations_Clicked(object sender, EventArgs e) => Raise("Organizations");
    private void Surveys_Clicked(object sender, EventArgs e) => Raise("Surveys");
    private void Users_Clicked(object sender, EventArgs e) => Raise("Users");
    private void Templates_Clicked(object sender, EventArgs e) => Raise("Templates");
    private void Variables_Clicked(object sender, EventArgs e) => Raise("Variables");
    private void Media_Clicked(object sender, EventArgs e) => Raise("Media");
    private void SettingsGeneral_Clicked(object sender, EventArgs e) => Raise("SettingsGeneral");
    private void SettingsApp_Clicked(object sender, EventArgs e) => Raise("SettingsApp");
    private void Roles_Clicked(object sender, EventArgs e) => Raise("Roles");
    private void ActivityLog_Clicked(object sender, EventArgs e) => Raise("ActivityLog");

    private void Logout_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is ViewModels.MainShellViewModel vm)
        {
            vm.Logout();
        }
    }
}


