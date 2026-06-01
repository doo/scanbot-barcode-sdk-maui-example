using IO.Scanbot.Sdk.Barcode;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static BarcodeScannerConfiguration FilterBarcodesRegexConfig
    {
        get
        {
            var configs = new List<BarcodeFormatConfigurationBase>();

            var baseFormatConfig = new BarcodeFormatCommonConfiguration
            {
                // You can set a regex filter here to limit the barcodes that will be scanned
                // Here is an example of a regex that matches only barcodes that contain numbers from 0 to 5
                RegexFilter = @"\b[0-5]+\b"
            };

            configs.Add(baseFormatConfig);

            var barcodeScannerConfiguration = new BarcodeScannerConfiguration
            {
                BarcodeFormatConfigurations = configs.ToArray(),
                EngineMode = BarcodeScannerEngineMode.NextGen
            };

            return barcodeScannerConfiguration;
        }
    }
}