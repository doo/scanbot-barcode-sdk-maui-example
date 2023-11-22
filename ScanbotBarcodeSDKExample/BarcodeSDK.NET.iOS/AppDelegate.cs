using BarcodeSDK.NET.iOS;

namespace BarcodeSDK.NET.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // Without a license key, the Scanbot Barcode SDK will work for 1 minute.
        // To scan longer, register for a trial license key here: https://scanbot.io/trial/
        private const string LicenseKey = "";

        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            ScanbotBarcodeSDK.iOS.ScanbotSDKGlobal.SetLoggingEnabled(true);

            if (!string.IsNullOrEmpty(LicenseKey))
            {
                ScanbotBarcodeSDK.iOS.ScanbotSDKGlobal.SetLicense(LicenseKey);
            }

            var rootController = new UINavigationController(new MainViewController());
            rootController.NavigationBar.BarTintColor = MainViewController.ScanbotRed;
            rootController.NavigationBar.TintColor = UIColor.White;
            rootController.NavigationBar.Translucent = false;
            rootController.NavigationBar.TitleTextAttributes = new UIStringAttributes
            {
                ForegroundColor = UIColor.White,
                Font = UIFont.FromName("HelveticaNeue", 16),
            };

            Window = new UIWindow(UIScreen.MainScreen.Bounds);
            Window.RootViewController = rootController;
            Window.MakeKeyAndVisible();

            return true;
        }
    }
}