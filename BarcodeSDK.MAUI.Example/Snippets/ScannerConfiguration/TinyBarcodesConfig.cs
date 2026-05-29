using ScanbotSDK.MAUI.Barcode;

namespace ScanbotSDK.MAUI.Example;

public partial class Snippets
{
    public static BarcodeScannerScreenConfiguration TinyBarcodesConfig
    {
        get
        {
            // Create the default configuration object.
            var configuration = new BarcodeScannerScreenConfiguration();

            // Enable locking the focus at the minimum possible distance.
            configuration.CameraConfiguration.MinFocusDistanceLock = true;

            // Configure other parameters as needed.

            return configuration;
        }
    }
}