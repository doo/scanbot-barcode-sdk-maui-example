using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKBarcodeScannerViewController EnableArOverlay(SBSDKBarcodeScannerViewController scannerViewController)
    {
        // Enable the selection overlay (AR Overlay) to show the contours of detected barcodes
        scannerViewController.IsTrackingOverlayEnabled = true;

        return scannerViewController;
    }
}