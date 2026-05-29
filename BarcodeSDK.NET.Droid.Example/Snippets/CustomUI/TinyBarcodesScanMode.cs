using IO.Scanbot.Sdk.Barcode.UI;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static BarcodeScannerView EnableTinyBarcodesScanMode(BarcodeScannerView barcodeScannerView)
    {
        // Lock the focus distance to the minimal possible value to scan tiny barcodes
        // Should be called in onCreate
        barcodeScannerView.CameraConfiguration.LockMinFocusDistance(true);

        return barcodeScannerView;
    }
}