namespace SurveyHub.Views.Shell;

public partial class SidebarView : ContentView
{
    public event EventHandler<string>? NavigationRequested;

    public SidebarView()
    {
        InitializeComponent();
    }

    private void Raise(string key) => NavigationRequested?.Invoke(this, key);

    private void Organizations_Clicked(object sender, EventArgs e) => Raise("Organizations");
    private void Brands_Clicked(object sender, EventArgs e) => Raise("Brands");
    private void Users_Clicked(object sender, EventArgs e) => Raise("Users");
    private void Templates_Clicked(object sender, EventArgs e) => Raise("Templates");
    private void Variables_Clicked(object sender, EventArgs e) => Raise("Variables");
    private void Media_Clicked(object sender, EventArgs e) => Raise("Media");
    private void SettingsGeneral_Clicked(object sender, EventArgs e) => Raise("SettingsGeneral");
    private void SettingsApp_Clicked(object sender, EventArgs e) => Raise("SettingsApp");
    private void Roles_Clicked(object sender, EventArgs e) => Raise("Roles");
    private void ActivityLog_Clicked(object sender, EventArgs e) => Raise("ActivityLog");
}


