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
using BarcodeSDK.NET.Droid.Snippets;
using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using BarcodeScannerConfiguration = IO.Scanbot.Sdk.UI.View.Barcode.Configuration.BarcodeScannerConfiguration;
using BarcodeScannerActivityV2 = IO.Scanbot.Sdk.Ui_v2.Barcode.BarcodeScannerActivity;

namespace BarcodeSDK.NET.Droid
{
    public partial class MainActivity : Activity
    {
        private void OnRTUUIClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            StartBarcodeScannerActivity(withImage: false);
        }

        private void OnRTUUIImageClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            StartBarcodeScannerActivity(withImage: true);
        }

        private void OnBatchRTUUIClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            StartBatchBarcodeScannerActivity();
        }
    }
}