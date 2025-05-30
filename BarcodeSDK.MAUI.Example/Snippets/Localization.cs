using ScanbotSDK.MAUI.Barcode;

namespace ScanbotSDK.MAUI.Example
{
    public partial class Snippets
    {
        public static BarcodeScannerScreenConfiguration Localization
        {
            get
            {
                // Create the default configuration object.
                var config = new BarcodeScannerScreenConfiguration();
                
                // Configure the strings.
                config.Localization.TopBarTitle = "Custom top bar title";
                config.Localization.UserGuidance = "Custom guidance title";
                config.Localization.BarcodeInfoMappingErrorStateCancelButton = "Custom Cancel title";
                config.Localization.CameraPermissionCloseButton = "Custom Close title";
            
                // Configure other parameters as needed.

                return config;
            }
        }
    }
}