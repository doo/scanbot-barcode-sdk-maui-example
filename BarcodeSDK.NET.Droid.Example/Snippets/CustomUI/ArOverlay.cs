using IO.Scanbot.Sdk.Barcode.UI;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static BarcodeScannerView EnableArOverlay(BarcodeScannerView barcodeScannerView)
    {
        // Enable the selection overlay (AR Overlay) to show the contours of detected barcodes
        barcodeScannerView?.SelectionOverlayController.SetEnabled(true);

        return barcodeScannerView;
    }
}