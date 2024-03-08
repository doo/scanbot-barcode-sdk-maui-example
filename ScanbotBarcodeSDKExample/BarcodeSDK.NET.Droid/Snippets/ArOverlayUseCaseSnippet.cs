using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;

namespace BarcodeSDK.NET.Droid.Snippets;

public class ArOverlayUseCaseSnippet
{
    public BarcodeScannerConfiguration GetArOverlayUseCaseSnippetConfiguration()
    {
        var configuration = new IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration.BarcodeScannerConfiguration();
        configuration.UseCase = new MultipleScanningMode()
        {
            Mode = MultipleBarcodesScanningMode.Unique,
            Sheet = new Sheet()
            {
                Mode = SheetMode.CollapsedSheet,
                CollapsedVisibleHeight = CollapsedVisibleHeight.Small
            },
            ArOverlay = new ArOverlayGeneralConfiguration()
            {
                Visible = true,
                AutomaticSelectionEnabled = false
            }
        };

        return configuration;
    }
}