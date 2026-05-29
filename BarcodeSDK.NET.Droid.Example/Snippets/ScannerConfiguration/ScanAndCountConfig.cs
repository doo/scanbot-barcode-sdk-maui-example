using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using IO.Scanbot.Sdk.Ui_v2.Common;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static BarcodeScannerScreenConfiguration ScanAndCountConfig
    {
        get
        {
            // Create the default configuration object.
            var configuration = new BarcodeScannerScreenConfiguration();

            // Initialize the use case for multiple scanning.
            var scanningMode = new MultipleScanningMode();

            // Set the counting mode.
            scanningMode.Mode = MultipleBarcodesScanningMode.Counting;

            // Set the sheet mode for the barcodes preview.
            scanningMode.Sheet.Mode = SheetMode.CollapsedSheet;

            // Set the height for the collapsed sheet.
            scanningMode.Sheet.CollapsedVisibleHeight = CollapsedVisibleHeight.Large;

            // Enable manual count change.
            scanningMode.SheetContent.ManualCountChangeEnabled = true;

            // Set the delay before same barcode counting repeat.
            scanningMode.CountingRepeatDelay = 1000;

            // Configure the submit button.
            scanningMode.SheetContent.SubmitButton.Text = "Submit";
            scanningMode.SheetContent.SubmitButton.Foreground.Color =
                new ScanbotColor("#000000");

            // Configure other parameters, pertaining to multiple-scanning mode as needed.

            configuration.UseCase = scanningMode;

            // Configure other parameters as needed.

            return configuration;
        }
    }
}