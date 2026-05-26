using ScanbotSDK.MAUI.Barcode.ClassicComponent;
using ScanbotSDK.MAUI.Core.Barcode;

namespace ScanbotSDK.MAUI.Example;

public partial class Snippets
{
    public static BarcodeScannerView EnableMultipleBarcodesScanMode(BarcodeScannerView cameraView)
    {
        cameraView.BarcodeFormatConfigurations =
        [
            new BarcodeFormatCommonConfiguration
            {
                Formats = BarcodeFormats.All
            }
        ];

        cameraView.OnBarcodeScanResult += (_, items) =>
        {
            // Handle barcode scanning results
            Console.WriteLine(items);
        };

        return cameraView;
    }
}