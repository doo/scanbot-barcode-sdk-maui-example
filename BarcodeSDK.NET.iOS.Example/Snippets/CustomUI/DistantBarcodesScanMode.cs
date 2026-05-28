using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKBarcodeScannerViewController EnableDistantBarcodesScanMode(SBSDKBarcodeScannerViewController scannerViewController)
    {
        // Retrieve the current applied zoom configurations and modify it
        var zoomConfiguration = scannerViewController.ZoomConfiguration;
        zoomConfiguration.InitialZoomFactor = 1.0f;

        // Retrieve the current applied view finder configurations and modify it
        var viewFinderConfiguration = scannerViewController.ViewFinderConfiguration;
        viewFinderConfiguration.IsViewFinderEnabled = true;
        viewFinderConfiguration.AspectRatio = new SBSDKAspectRatio(width: 1, height: 1);

        // Apply the modified zoom configurations onto the scanner
        scannerViewController.ZoomConfiguration = zoomConfiguration;

        // Apply the modified view finder configurations onto the scanner
        scannerViewController.ViewFinderConfiguration = viewFinderConfiguration;

        return scannerViewController;
    }
}