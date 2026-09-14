using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static void EnableSelectScanArOverlay(SBSDKBarcodeScannerViewController scannerViewController)
    {
        // Enable the selection overlay (AR Overlay) to show the contours of detected barcodes
        scannerViewController.Model.TrackingOverlay.IsTrackingOverlayEnabled = true;

        // Configure AR tracking overlay for the scanner
        var trackingConfiguration = new SBSDKBarcodeTrackingOverlayConfiguration();

        // To configure tracked barcodes info view
        var proposedStyle = new SBSDKBarcodeTrackingOverlayStyle();

        // Set the polygon style
        proposedStyle.PolygonColor = UIColor.Yellow;
        proposedStyle.PolygonBackgroundColor = UIColor.Clear;
        proposedStyle.PolygonDrawingEnabled = true;
        
        // Set the text style
        proposedStyle.TextColor = UIColor.Yellow;
        proposedStyle.TextBackgroundColor = UIColor.Black;
        proposedStyle.TextDrawingEnabled = true;
        
        // Update the required text over the AR overlay of detected barcodes.
        proposedStyle.TextOverride = "Some text";
        
        trackingConfiguration.SelectionStyle = proposedStyle;

        // Set the tracking configuration
        scannerViewController.Model.TrackingOverlay.TrackingOverlayConfiguration = trackingConfiguration;
    }
}