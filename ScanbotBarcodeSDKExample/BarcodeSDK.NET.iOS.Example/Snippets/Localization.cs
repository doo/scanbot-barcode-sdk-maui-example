using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKUI2BarcodeScannerConfiguration Localization
    {
        get
        {
            // Create the default configuration object.
            var config = new SBSDKUI2BarcodeScannerConfiguration();
            
            config.Localization.TopBarTitle = "Custom top bar title";
            config.Localization.UserGuidance = "Custom guidance title";
            config.Localization.BarcodeInfoMappingErrorStateCancelButton = "Custom Cancel title";
            config.Localization.CameraPermissionCloseButton = "Custom Close title";

            // Configure other parameters as needed.

            return config;
        }
    }
}