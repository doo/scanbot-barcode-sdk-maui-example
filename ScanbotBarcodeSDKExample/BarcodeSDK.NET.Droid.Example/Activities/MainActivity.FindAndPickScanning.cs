using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using IO.Scanbot.Sdk.Ui_v2.Barcode;
using IO.Scanbot.Sdk.Ui_v2.Common;

namespace BarcodeSDK.NET.Droid
{
    public partial class MainActivity : Activity
    {
        private void FindAndPickModeUseCaseSnippet(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }

            var configuration = new BarcodeScannerConfiguration();

            // Initialize the use case for multiple scanning.
            var findAndPickConfig = new FindAndPickScanningMode();

            // Set the sheet mode for the barcodes preview.
            findAndPickConfig.Sheet.Mode = SheetMode.CollapsedSheet;

            // Enable/Disable the automatic selection.
            findAndPickConfig.ArOverlay.AutomaticSelectionEnabled = false;

            // Set the height for the collapsed sheet.
            findAndPickConfig.Sheet.CollapsedVisibleHeight = CollapsedVisibleHeight.Large;

            // Enable manual count change.
            findAndPickConfig.SheetContent.ManualCountChangeEnabled = true;

            // Set the delay before same barcode counting repeat.
            findAndPickConfig.CountingRepeatDelay = 1000;

            // Configure the submit button.
            findAndPickConfig.SheetContent.SubmitButton.Text = "Submit";
            
            findAndPickConfig.SheetContent.SubmitButton.Foreground.Color = new ScanbotColor("#000000"); //arg string

            // Set the expected barcodes.
            findAndPickConfig.ExpectedBarcodes = new List<ExpectedBarcode>() 
            {
                new ExpectedBarcode(barcodeValue: "123456", title: "numeric barcode", image: "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png", count: 4),
                new ExpectedBarcode(barcodeValue: "SCANBOT", title: "value barcode", image: "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png", count: 4),
            };

            // Configure other parameters, pertaining to findAndPick-scanning mode as needed.
            configuration.UseCase = findAndPickConfig;
            configuration.RecognizerConfiguration.BarcodeFormats = BarcodeFormat.Values();

            var intent = BarcodeScannerActivity.NewIntent(this, configuration);
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE_V2);
        }
    }
}