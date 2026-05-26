using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Barcode.ClassicComponent;

namespace ScanbotSDK.MAUI.Example;

public partial class Snippets
{
    public static BarcodeScannerView EnableTinyBarcodesScanMode(BarcodeScannerView cameraView)
    {
        cameraView.MinFocusDistanceLock = true;

        cameraView.FinderConfiguration = new FinderConfiguration
        {
            IsFinderEnabled = true,
            FinderLineWidth = 2.0f
        };

        cameraView.OnBarcodeScanResult += (_, items) =>
        {
            // Handle barcode scanning results
            Console.WriteLine(items);
        };

        return cameraView;
    }
}