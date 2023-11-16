using Foundation;
using UIKit;

namespace ScanbotSDK.MAUI.Example;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    public static UIWindow RootWindow => (UIApplication.SharedApplication.Delegate as AppDelegate).Window;

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}