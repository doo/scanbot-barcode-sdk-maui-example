using IO.Scanbot.Sdk.Barcode.UI;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static BarcodeScannerView EnableDistantBarcodesScanMode(BarcodeScannerView barcodeScannerView)
    {
        // Set the optical zoom level to 30x to allow scanning barcodes from a distance
        barcodeScannerView.CameraConfiguration.SetPhysicalZoomRatio(30.0f);

        return barcodeScannerView;
    }
}