using Foundation;
using UIKit;

namespace Feedback2Business;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    
    // public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
    // {
    //     // allow all orientations
    //     return UIInterfaceOrientationMask.Portrait;
    // }
    
    
    // [Export("application:supportedInterfaceOrientationsForWindow:")]
    // public UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow forWindow)
    // {
    //     if (forWindow.WindowScene != null && forWindow.WindowScene.Title == "PerformOrientation")
    //     {
    //         return UIInterfaceOrientationMask.All;
    //     }
    //     else
    //     {
    //         return application.SupportedInterfaceOrientationsForWindow(forWindow);
    //     }
    // }

}