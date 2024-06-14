namespace ScanbotSDK.MAUI.Example
{
    public partial class Snippets
    {
        public static BarcodeScannerConfiguration Palette
        {
            get
            {
                // Create the default configuration object.
                var config = new BarcodeScannerConfiguration();
                
                // Simply alter one color and keep the other default.
                // configuration.Palette = new Palette()
                // {
                //     SbColorPrimary = new ScanbotColor("#c86e19"),
                // };
                
                // ... or set an entirely new palette.
                config.Palette = new Palette()
                {
                    SbColorPrimary = new ColorValue("#C8193C"),
                    SbColorPrimaryDisabled = new ColorValue("#F5F5F5"),
                    SbColorNegative = new ColorValue("#FF3737"),
                    SbColorPositive = new ColorValue("#4EFFB4"),
                    SbColorWarning = new ColorValue("#FFCE5C"),
                    SbColorSecondary = new ColorValue("#FFEDEE"),
                    SbColorSecondaryDisabled = new ColorValue("#F5F5F5"),
                    SbColorOnPrimary = new ColorValue("#FFFFFF"),
                    SbColorOnSecondary = new ColorValue("#C8193C"),
                    SbColorSurface = new ColorValue("#FFFFFF"),
                    SbColorOutline = new ColorValue("#EFEFEF"),
                    SbColorOnSurfaceVariant = new ColorValue("#707070"),
                    SbColorOnSurface = new ColorValue("#000000"),
                    SbColorSurfaceLow = new ColorValue("#26000000"),
                    SbColorSurfaceHigh = new ColorValue("#7A000000"),
                    SbColorModalOverlay = new ColorValue("#A3000000"),
                };

                return config;
            }
        }
    }
}