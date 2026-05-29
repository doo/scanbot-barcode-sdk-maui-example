using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKBarcodeScannerViewController EnableSelectScanArOverlay(SBSDKBarcodeScannerViewController scannerViewController)
    {
        // Enable the selection overlay (AR Overlay) to show the contours of detected barcodes
        scannerViewController.IsTrackingOverlayEnabled = true;

        // Configure AR tracking overlay for the scanner
        scannerViewController.TrackingOverlayController.Configuration.TextStyle.TrackingOverlayTextFormat = SBSDKBarcodeOverlayFormat.Code;
        scannerViewController.TrackingOverlayController.Configuration.PolygonStyle.PolygonColor = UIColor.Yellow;
        scannerViewController.TrackingOverlayController.Configuration.PolygonStyle.PolygonBackgroundColor = UIColor.Yellow.ColorWithAlpha(0.25f);
        scannerViewController.TrackingOverlayController.Configuration.TextStyle.TextColor = UIColor.Yellow;
        scannerViewController.TrackingOverlayController.Configuration.TextStyle.TextBackgroundColor = UIColor.Black;

        scannerViewController.TrackingOverlayController.Configuration.PolygonStyle.PolygonSelectedColor = UIColor.Red;
        scannerViewController.TrackingOverlayController.Configuration.PolygonStyle.PolygonBackgroundSelectedColor = UIColor.Red.ColorWithAlpha(0.25f);
        scannerViewController.TrackingOverlayController.Configuration.TextStyle.HighlightedTextColor = UIColor.Red;
        scannerViewController.TrackingOverlayController.Configuration.TextStyle.TextBackgroundHighlightedColor = UIColor.Black;

        return scannerViewController;
    }
}