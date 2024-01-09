using ScanbotBarcodeSDK.iOS;

namespace BarcodeSDK.NET.iOS
{
    public class Alert
    {
        public static bool CheckLicense(UIViewController parent)
        {
            if (!ScanbotSDKGlobal.IsLicenseValid)
            {
                Show(parent, "Oops!", "License invalid or expired");
            }

            return ScanbotSDKGlobal.IsLicenseValid;
        }

        public static void Show(UIViewController parent, string title, string message)
        {
            var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            parent.PresentViewController(alert, true, null);
        }
    }
}
