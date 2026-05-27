using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKBarcodeScannerConfiguration FilterBarcodesRegexConfig
    {
        get
        {
            var configs = new List<SBSDKBarcodeFormatConfigurationBase>();

            var baseFormatConfig = new SBSDKBarcodeFormatCommonConfiguration
            {
                // You can set a regex filter here to limit the barcodes that will be scanned
                // Here is an example of a regex that matches only barcodes that contain numbers from 0 to 5
                RegexFilter = @"\b[0-5]+\b",
            };

            configs.Add(baseFormatConfig);

            var barcodeScannerConfiguration = new SBSDKBarcodeScannerConfiguration
            {
                BarcodeFormatConfigurations = configs.ToArray(),
                EngineMode = SBSDKBarcodeScannerEngineMode.NextGen
            };

            return barcodeScannerConfiguration;
        }
    }
}