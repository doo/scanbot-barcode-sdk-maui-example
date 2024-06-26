using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;
public static partial class Snippets
{
    public static SBSDKUI2BarcodeScannerConfiguration Palette
    {
        get
        {
            // Create the default configuration object.
            var config = new SBSDKUI2BarcodeScannerConfiguration();
            
            // Simply alter one color and keep the other default.
            // configuration.Palette = new Palette()
            // {
            //     SbColorPrimary = new SBSDKUI2Color("#c86e19"),
            // };
            
            // ... or set an entirely new palette.
            config.Palette = new SBSDKUI2Palette()
            {
                SbColorPrimary = new SBSDKUI2Color("#C8193C"),
                SbColorPrimaryDisabled = new SBSDKUI2Color("#F5F5F5"),
                SbColorNegative = new SBSDKUI2Color("#FF3737"),
                SbColorPositive = new SBSDKUI2Color("#4EFFB4"),
                SbColorWarning = new SBSDKUI2Color("#FFCE5C"),
                SbColorSecondary = new SBSDKUI2Color("#FFEDEE"),
                SbColorSecondaryDisabled = new SBSDKUI2Color("#F5F5F5"),
                SbColorOnPrimary = new SBSDKUI2Color("#FFFFFF"),
                SbColorOnSecondary = new SBSDKUI2Color("#C8193C"),
                SbColorSurface = new SBSDKUI2Color("#FFFFFF"),
                SbColorOutline = new SBSDKUI2Color("#EFEFEF"),
                SbColorOnSurfaceVariant = new SBSDKUI2Color("#707070"),
                SbColorOnSurface = new SBSDKUI2Color("#000000"),
                SbColorSurfaceLow = new SBSDKUI2Color("#26000000"),
                SbColorSurfaceHigh = new SBSDKUI2Color("#7A000000"),
                SbColorModalOverlay = new SBSDKUI2Color("#A3000000"),
            };

            return config;
        }
    }
}