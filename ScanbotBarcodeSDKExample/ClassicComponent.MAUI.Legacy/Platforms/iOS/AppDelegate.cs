using Foundation;
using UIKit;

namespace ClassicComponent.MAUI.Legacy;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    public static UIWindow RootWindow => (UIApplication.SharedApplication.Delegate as AppDelegate).Window;

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}