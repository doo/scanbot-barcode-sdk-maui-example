using IO.Scanbot.Sdk.Barcode.UI;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static BarcodeScannerView EnableMultipleBarcodesScanMode(BarcodeScannerView barcodeScannerView)
    {
        // Disable the finder view to hide the barcode scanner viewfinder
        // It allows to locate the barcodes on the full screen
        barcodeScannerView.FinderViewController.SetFinderEnabled(false);

        // Recommended for Multi-Scan approach
        barcodeScannerView.ViewController.BarcodeScanningInterval = 0;

        return barcodeScannerView;
    }
}