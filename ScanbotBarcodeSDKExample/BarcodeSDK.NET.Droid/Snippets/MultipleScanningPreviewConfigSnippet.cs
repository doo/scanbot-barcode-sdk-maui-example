using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using IO.Scanbot.Sdk.Ui_v2.Common;

namespace BarcodeSDK.NET.Droid.Snippets;

public class MultipleScanningPreviewConfigSnippet
{
    public BarcodeScannerConfiguration GetMultipleScanningPreviewConfigSnippetConfiguration()
    {
        var configuration = new BarcodeScannerConfiguration();
        configuration.UseCase = new MultipleScanningMode()
        {
            Sheet = new Sheet()
            {
                Mode = SheetMode.CollapsedSheet,
                CollapsedVisibleHeight = CollapsedVisibleHeight.Large,
            },
            SheetContent = new SheetContent()
            {   
                SubmitButton = new ButtonConfiguration()
                {
                    Text = "Submit",
                    Foreground = new ForegroundStyle()
                    {
                        Color = new ScanbotColor("#000000")
                    },
                }
            },
        };

        return configuration;
    }
}