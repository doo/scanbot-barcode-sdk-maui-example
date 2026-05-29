using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Barcode.ClassicComponent;
using ScanbotSDK.MAUI.Core.Barcode;

namespace ScanbotSDK.MAUI.Example;

public partial class Snippets
{
    public static BarcodeScannerView EnableDistantBarcodesScanMode(BarcodeScannerView cameraView)
    {
        cameraView.EngineMode = BarcodeScannerEngineMode.NextGenFarDistance;

        cameraView.FinderConfiguration = new FinderConfiguration
        {
            IsFinderEnabled = true,
            FinderLineWidth = 2.0f
        };

        cameraView.CameraZoomLevel = 0.3f;

        cameraView.OnBarcodeScanResult += (_, items) =>
        {
            // Handle barcode scanning results
            Console.WriteLine(items);
        };

        return cameraView;
    }
}