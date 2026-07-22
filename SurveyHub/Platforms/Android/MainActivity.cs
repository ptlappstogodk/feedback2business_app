using Android.App;
using Android.Content.PM;
using Android.OS;

namespace SurveyHub;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        System.Net.ServicePointManager.ServerCertificateValidationCallback = (message, cert, chain, errors) => true;
        base.OnCreate(savedInstanceState);
    }
}
