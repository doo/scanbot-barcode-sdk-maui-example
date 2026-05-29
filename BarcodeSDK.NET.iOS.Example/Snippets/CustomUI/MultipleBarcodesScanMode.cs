using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKBarcodeScannerViewController EnableMultipleBarcodesScanMode(SBSDKBarcodeScannerViewController scannerViewController)
    {
        // Retrieve the current applied view finder configurations and modify it
        var viewFinderConfiguration = scannerViewController.ViewFinderConfiguration;
        viewFinderConfiguration.IsViewFinderEnabled = false;

        // Apply the modified view finder configurations onto the scanner
        scannerViewController.ViewFinderConfiguration = viewFinderConfiguration;

        return scannerViewController;
    }
}