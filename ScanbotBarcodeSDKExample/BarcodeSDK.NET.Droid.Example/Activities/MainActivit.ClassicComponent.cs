using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using IO.Scanbot.Sdk.Barcode.Entity;
using IO.Scanbot.Sdk.Barcode_scanner;
using IO.Scanbot.Sdk.UI.Barcode_scanner.View.Barcode;
using IO.Scanbot.Sdk.UI.Barcode_scanner.View.Barcode.Batch;
using IO.Scanbot.Sdk.UI.View.Barcode.Batch.Configuration;
using IO.Scanbot.Sdk.UI.View.Barcode.Configuration;
using IO.Scanbot.Sdk.UI.View.Base;
using IO.Scanbot.Sdk.Barcode;
using BarcodeSDK.NET.Droid.Activities;
using BarcodeSDK.NET.Droid.Activities.V1;

namespace BarcodeSDK.NET.Droid
{
    public partial class MainActivity : Activity
    {
        private void OnBarcodeCameraDemoClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            var intent = new Intent(this, typeof(BarcodeClassicComponentActivity));
            intent.PutExtra("useCameraX", false);
            StartActivity(intent);
        }

        private void OnBarcodeCameraXDemoClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            var intent = new Intent(this, typeof(BarcodeClassicComponentActivity));
            intent.PutExtra("useCameraX", true);
            StartActivity(intent);
        }

        private void OnBarcodeCameraScanAndCountClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            var intent = new Intent(this, typeof(BarcodeScanAndCountActivity));
            StartActivity(intent);
        }
    }
}