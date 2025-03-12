using ScanbotSDK.MAUI.Barcode;

namespace ScanbotSDK.MAUI.Example
{
    public partial class Snippets
    {
        public static BarcodeScannerScreenConfiguration ArOverlay
        {
            get
            {
                // Create the default configuration object.
                var config = new BarcodeScannerScreenConfiguration();
                
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
}