using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using IO.Scanbot.Sdk.Ui_v2.Common;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static BarcodeScannerScreenConfiguration MultipleScanningPreviewConfig
    {
        get
        {
            // Create the default configuration object.
            var configuration = new BarcodeScannerScreenConfiguration();

            // Initialize the use case for multiple scanning.
            var scanningMode = new MultipleScanningMode();

            // Set the sheet mode for the barcodes preview.
            scanningMode.Sheet.Mode = SheetMode.CollapsedSheet;

            // Set the height for the collapsed sheet.
            scanningMode.Sheet.CollapsedVisibleHeight = CollapsedVisibleHeight.Large;

            // Configure the submit button on the sheet.
            scanningMode.SheetContent.SubmitButton.Text = "Submit";
            scanningMode.SheetContent.SubmitButton.Foreground.Color =
                new ScanbotColor("#000000");

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