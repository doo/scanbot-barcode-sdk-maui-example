﻿namespace BarcodeSDK.NET.iOS
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
            ScanbotSDK.iOS.ScanbotSDKGlobal.SetLoggingEnabled(true);

            if (!string.IsNullOrEmpty(LicenseKey))
            {
                ScanbotSDK.iOS.ScanbotSDKGlobal.SetLicense(LicenseKey);
            }
            
            var rootController = new UINavigationController(new MainViewController());
            Window = new UIWindow(UIScreen.MainScreen.Bounds);
            Window.RootViewController = rootController;
            Window.MakeKeyAndVisible();
            return true;
        }
    }
}