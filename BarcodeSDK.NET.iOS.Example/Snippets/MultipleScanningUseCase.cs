using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKUI2BarcodeScannerScreenConfiguration MultipleScanningUseCase
    {
        get
        {
            // Create the default configuration object.
            var config = new SBSDKUI2BarcodeScannerScreenConfiguration();
            
            // Create and configure the use case for multiple scanning mode.
            var useCase = new SBSDKUI2MultipleScanningMode();
            
            // Set the counting mode.
            useCase.Mode = SBSDKUI2MultipleBarcodesScanningMode.Counting;

            // Set the sheet mode for the barcodes preview.
            useCase.Sheet.Mode = SBSDKUI2SheetMode.CollapsedSheet;

            // Set the height for the collapsed sheet.
            useCase.Sheet.CollapsedVisibleHeight = SBSDKUI2CollapsedVisibleHeight.Large;

            // Enable manual count change.
            useCase.SheetContent.ManualCountChangeEnabled = true;

            // Set the delay before same barcode counting repeat.
            useCase.CountingRepeatDelay = new IntPtr(1000);

            // Configure the submit button.
            useCase.SheetContent.SubmitButton.Text = "Submit";
            useCase.SheetContent.SubmitButton.Foreground.Color = new SBSDKUI2Color("#000000");

            // Configure other parameters, pertaining to multiple-scanning mode as needed.
            config.UseCase = useCase;

            // Set an array of accepted barcode types.
            config.ScannerConfiguration.BarcodeFormats = SBSDKBarcodeFormats.Common;

            return config;
        }
    }
}