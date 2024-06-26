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
using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using BarcodeScannerConfiguration = IO.Scanbot.Sdk.UI.View.Barcode.Configuration.BarcodeScannerConfiguration;
using BarcodeScannerActivityV2 = IO.Scanbot.Sdk.Ui_v2.Barcode.BarcodeScannerActivity;

namespace BarcodeSDK.NET.Droid
{
    public partial class MainActivity : Activity
    {
        private void LegacySingleBarcodeScanningSnippet(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            StartBarcodeScannerActivity(withImage: false);
        }

        private void LegacySingleBarcodeScanningWithImageSnippet(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            StartBarcodeScannerActivity(withImage: true);
        }

        private void LgeacyBatchBarcodeScanningSnippet(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            StartBatchBarcodeScannerActivity();
        }

        private void OnLegacyActivityResult(Intent data, BarcodeScanningResult barcode)
        {
            var imagePath = data.GetStringExtra(
                    BarcodeScannerActivity.ScannedBarcodeImagePathExtra);
            var previewPath = data.GetStringExtra(
                    BarcodeScannerActivity.ScannedBarcodePreviewFramePathExtra);

            var intent = new Intent(this, typeof(BarcodeSDK.NET.Droid.Activities.V1.BarcodeResultActivity));
            var bundle = new BaseBarcodeResult<BarcodeScanningResult>(barcode, imagePath, previewPath).ToBundle();
            intent.PutExtra("BarcodeResult", bundle);

            StartActivity(intent);
        }

        private void StartBarcodeScannerActivity(bool withImage)
        {
            var configuration = new BarcodeScannerConfiguration();
            var defaultAdditionalConfig = new BarcodeScannerAdditionalConfiguration();

            configuration.SetBarcodeFormatsFilter(
                BarcodeTypes.Instance.AcceptedTypes);
            configuration.SetAdditionalDetectionParameters(
                defaultAdditionalConfig.Copy(codeDensity: BarcodeDensity.High));
            configuration.SetEngineMode(EngineMode.NextGen);
            configuration.SetSuccessBeepEnabled(true);

            if (withImage)
            {
                configuration.SetBarcodeImageGenerationType(BarcodeImageGenerationType.VideoFrame);
            }

            configuration.SetSelectionOverlayConfiguration(
                new IO.Scanbot.Sdk.UI.View.Barcode.SelectionOverlayConfiguration(
                    overlayEnabled: true,
                    automaticSelectionEnabled: false,
                    textFormat: IO.Scanbot.Sdk.Barcode.UI.BarcodeOverlayTextFormat.Code,
                    polygonColor: Color.Yellow,
                    textColor: Color.Yellow,
                    textContainerColor: Color.Black));

            // To see the confirmation dialog in action, uncomment the below and comment out the configuration.SetConfirmationDialogConfiguration line above.
            //configuration.SetConfirmationDialogConfiguration(new IO.Scanbot.Sdk.UI.View.Barcode.Dialog.BarcodeConfirmationDialogConfiguration(
            //    resultWithConfirmationEnabled: true,
            //    title: "Barcode Detected!",
            //    message: "A barcode was found.",
            //    confirmButtonTitle: "Continue",
            //    retryButtonTitle: "Try again",
            //    dialogTextFormat: IO.Scanbot.Sdk.UI.View.Barcode.Dialog.BarcodeDialogFormat.TypeAndCode,
            //    buttonsAccentColor: null,
            //    isConfirmButtonFilled: false,
            //    filledConfirmButtonTextColor: null
            //));

            var intent = BarcodeScannerActivity.NewIntent(this, configuration);
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE);
        }

        private void StartBatchBarcodeScannerActivity()
        {
            var configuration = new BatchBarcodeScannerConfiguration();
            var list = BarcodeTypes.Instance.AcceptedTypes;
            configuration.SetBarcodeFormatsFilter(list);
            configuration.SetSelectionOverlayConfiguration(new IO.Scanbot.Sdk.UI.View.Barcode.SelectionOverlayConfiguration(true,
                true, IO.Scanbot.Sdk.Barcode.UI.BarcodeOverlayTextFormat.Code,
                Color.Yellow, Color.Yellow, Color.Black, Color.Pink));
            var intent = BatchBarcodeScannerActivity.NewIntent(this, configuration);
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE);
        }
    }
}