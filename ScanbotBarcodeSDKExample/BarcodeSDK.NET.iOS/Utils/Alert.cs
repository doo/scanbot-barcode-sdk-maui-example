using System;
using ScanbotBarcodeSDK.iOS;
using UIKit;

namespace BarcodeScannerExample.iOS
{
    public class Alert
    {
        public static bool CheckLicense(UIViewController parent)
        {
            if (!ScanbotSDK.IsLicenseValid)
            {
                Show(parent, "Oops!", "License invalid or expired");
            }

            return ScanbotSDK.IsLicenseValid;
        }

        public static void Show(UIViewController parent, string title, string message)
        {
            var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            parent.PresentViewController(alert, true, null);
        }
    }
}
