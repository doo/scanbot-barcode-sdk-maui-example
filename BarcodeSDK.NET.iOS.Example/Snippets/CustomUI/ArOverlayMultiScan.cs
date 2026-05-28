using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKBarcodeScannerViewController EnableMultiScanArOverlay(SBSDKBarcodeScannerViewController scannerViewController)
    {
        // Enable the selection overlay (AR Overlay) to show the contours of detected barcodes
        scannerViewController.IsTrackingOverlayEnabled = true;

        // Configure AR tracking overlay for the scanner
        var trackingConfiguration = new SBSDKBarcodeTrackingOverlayConfiguration();

        // To configure tracked barcodes info view
        var trackedViewTextStyle = new SBSDKBarcodeTrackedViewTextStyle();

        // To disable the info view
        trackedViewTextStyle.TextDrawingEnabled = false;

        // Set the configured info view style
        trackingConfiguration.TextStyle = trackedViewTextStyle;

        // Set the tracking configuration
        scannerViewController.TrackingOverlayController.Configuration = trackingConfiguration;

        return scannerViewController;
    }
}