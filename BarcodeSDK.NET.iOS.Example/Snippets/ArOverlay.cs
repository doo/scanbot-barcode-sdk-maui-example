using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKUI2BarcodeScannerScreenConfiguration ArOverlay
    {
        get
        {
            // Create the default configuration object.
            var config = new SBSDKUI2BarcodeScannerScreenConfiguration();
            
            // Create and configure the use case for multiple scanning mode.
            var useCase = new SBSDKUI2MultipleScanningMode();
            useCase.Mode = SBSDKUI2MultipleBarcodesScanningMode.Unique;
            useCase.Sheet.Mode = SBSDKUI2SheetMode.CollapsedSheet;
            useCase.Sheet.CollapsedVisibleHeight = SBSDKUI2CollapsedVisibleHeight.Small;
            
            // Configure AR Overlay.
            useCase.ArOverlay.Visible = true;
            useCase.ArOverlay.AutomaticSelectionEnabled = false;

            // Configure other parameters, pertaining to multiple-scanning mode as needed.
            config.UseCase = useCase;

            return config;
        }
    }
}