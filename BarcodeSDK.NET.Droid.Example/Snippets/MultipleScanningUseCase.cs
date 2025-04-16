using IO.Scanbot.Sdk.Barcode;
using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using IO.Scanbot.Sdk.Ui_v2.Common;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static BarcodeScannerScreenConfiguration MultipleScanningUseCase
    {
        get
        {
            // Create the default configuration object.
            var config = new BarcodeScannerScreenConfiguration();
            
            // Create and configure the use case for multiple scanning mode.
            var useCase = new MultipleScanningMode();
            
            // Set the counting mode.
            useCase.Mode = MultipleBarcodesScanningMode.Counting;

            // Set the sheet mode for the barcodes preview.
            useCase.Sheet.Mode = SheetMode.CollapsedSheet;

            // Set the height for the collapsed sheet.
            useCase.Sheet.CollapsedVisibleHeight = CollapsedVisibleHeight.Large;

            // Enable manual count change.
            useCase.SheetContent.ManualCountChangeEnabled = true;

            // Set the delay before same barcode counting repeat.
            useCase.CountingRepeatDelay = 1000;

            // Configure the submit button.
            useCase.SheetContent.SubmitButton.Text = "Submit";
            useCase.SheetContent.SubmitButton.Foreground.Color = new ScanbotColor("#000000");

            // Configure other parameters, pertaining to single-scanning mode as needed.
            config.UseCase = useCase;

            // Set an array of accepted barcode types.
            config.ScannerConfiguration.BarcodeFormats = BarcodeFormats.Common;

            return config;
        }
    }
}