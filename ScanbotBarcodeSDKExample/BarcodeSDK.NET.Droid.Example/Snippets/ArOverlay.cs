using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using IO.Scanbot.Sdk.Ui_v2.Common;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static BarcodeScannerConfiguration ArOverlay
    {
        get
        {
            // Create the default configuration object.
            var config = new BarcodeScannerConfiguration();
            
            var useCase = new MultipleScanningMode();

            useCase.Mode = MultipleBarcodesScanningMode.Unique;
            useCase.Sheet.Mode = SheetMode.CollapsedSheet;
            useCase.Sheet.CollapsedVisibleHeight = CollapsedVisibleHeight.Small;

            // Configure AR Overlay.
            useCase.ArOverlay.Visible = true;
            useCase.ArOverlay.AutomaticSelectionEnabled = false;

            // Configure other parameters, pertaining to single-scanning mode as needed.
            config.UseCase = useCase;

            return config;
        }
    }
}