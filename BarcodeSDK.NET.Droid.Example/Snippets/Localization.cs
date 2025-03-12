using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using IO.Scanbot.Sdk.Ui_v2.Common;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static BarcodeScannerScreenConfiguration Localization
    {
        get
        {
            // Create the default configuration object.
            var config = new BarcodeScannerScreenConfiguration();
            
            config.Localization.TopBarTitle = "Custom top bar title";
            config.Localization.UserGuidance = "Custom guidance title";
            config.Localization.BarcodeInfoMappingErrorStateCancelButton = "Custom Cancel title";
            config.Localization.CameraPermissionCloseButton = "Custom Close title";
        
            // Configure other parameters as needed.

            return config;
        }
    }
}