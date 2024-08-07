using ScanbotSDK.MAUI.Barcode;

namespace ScanbotSDK.MAUI.Example
{
    public partial class Snippets
    {
        public static BarcodeScannerConfiguration Localization
        {
            get
            {
                // Create the default configuration object.
                var config = new BarcodeScannerConfiguration();
                
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