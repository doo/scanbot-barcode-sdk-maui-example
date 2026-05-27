using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKUI2BarcodeScannerScreenConfiguration TinyBarcodesConfig
    {
        get
        {
            // Create the default configuration object.
            var configuration = new SBSDKUI2BarcodeScannerScreenConfiguration();

            // Enable locking the focus at the minimum possible distance.
            configuration.CameraConfiguration.MinFocusDistanceLock = true;

            // Configure other parameters as needed.

            return configuration;
        }
    }
}