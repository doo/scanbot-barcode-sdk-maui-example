using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKUI2BarcodeScannerScreenConfiguration ScanAndCountConfig
    {
        get
        {
            // Create the default configuration object.
            var configuration = new SBSDKUI2BarcodeScannerScreenConfiguration();

            // Initialize the use case for multiple scanning.
            var scanningMode = new SBSDKUI2MultipleScanningMode();

            // Set the counting mode.
            scanningMode.Mode = SBSDKUI2MultipleBarcodesScanningMode.Counting;

            // Set the sheet mode for the barcodes preview.
            scanningMode.Sheet.Mode = SBSDKUI2SheetMode.CollapsedSheet;

            // Set the height for the collapsed sheet.
            scanningMode.Sheet.CollapsedVisibleHeight = SBSDKUI2CollapsedVisibleHeight.Large;

            // Enable manual count change.
            scanningMode.SheetContent.ManualCountChangeEnabled = true;

            // Set the delay before same barcode counting repeat.
            scanningMode.CountingRepeatDelay = 1000;

            // Configure the submit button.
            scanningMode.SheetContent.SubmitButton.Text = "Submit";
            scanningMode.SheetContent.SubmitButton.Foreground.Color =
                new SBSDKUI2Color("#000000");

            // Configure other parameters, pertaining to multiple-scanning mode as needed.

            configuration.UseCase = scanningMode;

            // Configure other parameters as needed.

            return configuration;
        }
    }
}