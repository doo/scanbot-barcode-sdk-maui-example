using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKUI2BarcodeScannerScreenConfiguration MultipleScanningPreviewConfig
    {
        get
        {
            // Create the default configuration object.
            var configuration = new SBSDKUI2BarcodeScannerScreenConfiguration();

            // Initialize the use case for multiple scanning.
            var scanningMode = new SBSDKUI2MultipleScanningMode();

            // Set the sheet mode for the barcodes preview.
            scanningMode.Sheet.Mode = SBSDKUI2SheetMode.CollapsedSheet;

            // Set the height for the collapsed sheet.
            scanningMode.Sheet.CollapsedVisibleHeight = SBSDKUI2CollapsedVisibleHeight.Large;

            // Configure the submit button on the sheet.
            scanningMode.SheetContent.SubmitButton.Text = "Submit";
            scanningMode.SheetContent.SubmitButton.Foreground.Color =
                new SBSDKUI2Color("#000000");

            // Configure localization parameters.
            configuration.Localization.BarcodeInfoMappingErrorStateCancelButton =
                "Custom Cancel title";
            configuration.Localization.CameraPermissionCloseButton = "Custom Close title";
            // Configure other strings as needed.

            // Configure other parameters, pertaining to multiple-scanning mode as needed.

            configuration.UseCase = scanningMode;

            return configuration;
        }
    }
}