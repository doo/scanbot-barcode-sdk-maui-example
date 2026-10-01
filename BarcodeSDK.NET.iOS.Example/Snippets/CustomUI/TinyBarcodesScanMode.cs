using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKBarcodeScannerViewController EnableTinyBarcodesScanMode(SBSDKBarcodeScannerViewController scannerViewController)
    {
        // Retrieve the current applied general configurations and modify it
        var generalConfiguration = scannerViewController.GeneralConfiguration;
        generalConfiguration.IsTapToFocusEnabled = true;
        generalConfiguration.FocusLockLensPosition = 0.1f;

        // Retrieve the current applied view finder configurations and modify it
        var viewFinderConfiguration = scannerViewController.ViewModel.Configuration.ViewFinder;
        viewFinderConfiguration.IsViewFinderEnabled = true;
        viewFinderConfiguration.AspectRatio = new SBSDKAspectRatio(width: 1, height: 1);

        // Apply the modified general configurations onto the scanner
        scannerViewController.GeneralConfiguration = generalConfiguration;

        // Apply the modified view finder configurations onto the scanner
        scannerViewController.ViewModel.Configuration.ViewFinder.ApplyFrom(viewFinderConfiguration);

        return scannerViewController;
    }
}