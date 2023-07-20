using BarcodeSDK.MAUI.iOS.Services;
using Foundation;

namespace BarcodeSDK.MAUI.Example.iOS;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => CreateMAuiApplication();

    private MauiApp CreateMAuiApplication()
    {
        DependencyManager.Register();
        return MauiProgram.CreateMauiApp();
    }
}

