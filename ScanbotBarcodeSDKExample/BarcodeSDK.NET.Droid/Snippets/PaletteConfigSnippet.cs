using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using IO.Scanbot.Sdk.Ui_v2.Common;

namespace BarcodeSDK.NET.Droid.Snippets;

public class PaletteConfigSnippet
{
    public BarcodeScannerConfiguration GetPaletteConfigSnippetConfiguration()
    {
        // Create the default configuration object.
        var configuration = new BarcodeScannerConfiguration();
        
        // Simply alter one color and keep the other default.
        configuration.Palette = new ScanbotPalette()
        {
            SbColorPrimary = new ScanbotColor("#c86e19"),
        };
        
        // ... or set an entirely new palette.
        configuration.Palette = new ScanbotPalette()
        {
            SbColorPrimary = new ScanbotColor("#C8193C"),
            SbColorPrimaryDisabled = new ScanbotColor("#F5F5F5"),
            SbColorNegative = new ScanbotColor("#FF3737"),
            SbColorPositive = new ScanbotColor("#4EFFB4"),
            SbColorWarning = new ScanbotColor("#FFCE5C"),
            SbColorSecondary = new ScanbotColor("#FFEDEE"),
            SbColorSecondaryDisabled = new ScanbotColor("#F5F5F5"),
            SbColorOnPrimary = new ScanbotColor("#FFFFFF"),
            SbColorOnSecondary = new ScanbotColor("#C8193C"),
            SbColorSurface = new ScanbotColor("#FFFFFF"),
            SbColorOutline = new ScanbotColor("#EFEFEF"),
            SbColorOnSurfaceVariant = new ScanbotColor("#707070"),
            SbColorOnSurface = new ScanbotColor("#000000"),
            SbColorSurfaceLow = new ScanbotColor("#26000000"),
            SbColorSurfaceHigh = new ScanbotColor("#7A000000"),
            SbColorModalOverlay = new ScanbotColor("#A3000000"),
        };

        return configuration;
    }
}