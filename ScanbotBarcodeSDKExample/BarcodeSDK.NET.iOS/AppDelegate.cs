using BarcodeSDK.NET.iOS;

namespace BarcodeSDK.NET.iOS;

[Register ("AppDelegate")]
public class AppDelegate : UIApplicationDelegate
{
    public static readonly UIColor ScanbotRed = UIColor.FromRGB(200, 25, 60);

    public UINavigationController Controller { get; set; }

    public override UIWindow Window { get; set; }

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        ScanbotBarcodeSDK.iOS.ScanbotSDK.SetLoggingEnabled(true);
        // TODO: Initialize SDK with correct license
        //ScanbotSDK.SetLicense("");

        UIViewController initial = new MainViewController();
        Controller = new UINavigationController(initial);

        // Navigation bar background color
        Controller.NavigationBar.BarTintColor = ScanbotRed;
        // Back button color
        Controller.NavigationBar.TintColor = UIColor.White;
        // Title color
        Controller.NavigationBar.TitleTextAttributes = new UIStringAttributes
        {
            ForegroundColor = UIColor.White,
            Font = UIFont.FromName("HelveticaNeue", 16),
        };
        Controller.NavigationBar.Translucent = false;
        Window = new UIWindow(UIScreen.MainScreen.Bounds);

        Window.RootViewController = Controller;

        Window.MakeKeyAndVisible();

        return true;
    }

}

