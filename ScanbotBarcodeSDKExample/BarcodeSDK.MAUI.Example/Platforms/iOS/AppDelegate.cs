using BarcodeSDK.MAUI.iOS.Services;
using Foundation;
using UIKit;

namespace BarcodeSDK.MAUI.Example;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    /// <summary>
    /// Returns the Root Window of the application.
    /// </summary>
    public static UIWindow RootWindow => (UIApplication.SharedApplication.Delegate as AppDelegate).Window;

    protected override MauiApp CreateMauiApp() => CreateMAuiApplication();

    private MauiApp CreateMAuiApplication()
    {
        DependencyManager.Register();
        return MauiProgram.CreateMauiApp();
    }
}