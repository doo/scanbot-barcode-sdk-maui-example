using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using IO.Scanbot.Sdk.Ui_v2.Common;

namespace BarcodeSDK.NET.Droid.Snippets;

public class MultipleScanningUseCaseSnippet
{
    public BarcodeScannerConfiguration GetMultipleScanningPreviewConfigSnippetConfiguration()
    {
        var configuration = new BarcodeScannerConfiguration();
        configuration.UseCase = new MultipleScanningMode()
        {
            Mode = MultipleBarcodesScanningMode.Counting,
            Sheet = new Sheet()
            {
                Mode = SheetMode.CollapsedSheet,
                CollapsedVisibleHeight = CollapsedVisibleHeight.Large,
            },
            SheetContent = new SheetContent()
            {   
                ManualCountChangeEnabled = true,
                SubmitButton = new ButtonConfiguration()
                {
                    Text = "Submit",
                    Foreground = new ForegroundStyle()
                    {
                        Color = new ScanbotColor("#000000")
                    }
                },
            },
            CountingRepeatDelay = 1000,
        };

        return configuration;
    }
}