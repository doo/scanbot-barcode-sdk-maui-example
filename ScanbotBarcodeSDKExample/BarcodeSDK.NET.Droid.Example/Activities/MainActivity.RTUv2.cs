using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using IO.Scanbot.Sdk.Ui_v2.Common;
using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using IO.Scanbot.Sdk.Ui_v2.Barcode;

namespace BarcodeSDK.NET.Droid
{
    public partial class MainActivity : Activity
    {
        private void OnRTUv2ActivityResult(Intent data, BarcodeScannerResult barcode)
        {
            var imagePath = data.GetStringExtra(
                    IO.Scanbot.Sdk.Ui_v2.Barcode.BarcodeScannerActivity.ScannedBarcodeImagePathExtra);
            var previewPath = data.GetStringExtra(
                    IO.Scanbot.Sdk.Ui_v2.Barcode.BarcodeScannerActivity.ScannedBarcodePreviewFramePathExtra);

            var intent = new Intent(this, typeof(BarcodeSDK.NET.Droid.Activities.V2.BarcodeResultActivity));
            var bundle = new BaseBarcodeResult<BarcodeScannerResult>(barcode, imagePath, previewPath).ToBundle();
            intent.PutExtra("BarcodeResult", bundle);

            StartActivity(intent);
        }
    }
}