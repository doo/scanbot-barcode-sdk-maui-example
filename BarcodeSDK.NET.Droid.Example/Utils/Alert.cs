using System;
using Android.App;
using Android.Content;
using IO.Scanbot.Sdk.Barcode_scanner;

namespace BarcodeSDK.NET.Droid
{
    public class Alert
    {
        public static bool CheckLicense(Context context, ScanbotBarcodeScannerSDK sdk)
        {
            if (!sdk.LicenseInfo.IsValid)
            {
                Toast(context, "License invalid or expired");
            }

            return sdk.LicenseInfo.IsValid;
        }

        public static void Toast(Context context, string message)
        {
            Android.Widget.Toast.MakeText(context, message, Android.Widget.ToastLength.Long).Show();
        }

        public static void ShowInfoDialog(Activity activity, string title, string message) {
            AlertDialog alertDialog = new AlertDialog.Builder(activity).Create();
            alertDialog.SetTitle(title);
            alertDialog.SetMessage(message);
            EventHandler<DialogClickEventArgs> eventHandler = new EventHandler<DialogClickEventArgs>(DismissDialog);
            alertDialog.SetButton(((int)DialogButtonType.Neutral), "OK", eventHandler);
            alertDialog.Show();
        }

        public static void DismissDialog(object obj, EventArgs args)
        {
            if (obj is IDialogInterface dialog)
            {
                dialog.Dismiss();
            }
        }
    }
}
