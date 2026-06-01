using IO.Scanbot.Sdk.Barcode.UI;
using Android.Graphics;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static BarcodeScannerView EnableMultiScanArOverlay(BarcodeScannerView barcodeScannerView)
    {
        if (barcodeScannerView == null)
            return null;

        // Enable the selection overlay (AR Overlay) to show the contours of detected barcodes
        barcodeScannerView.SelectionOverlayController.SetEnabled(true);

        // Disable the finder view to hide the barcode scanner viewfinder
        // It allows to locate the barcodes on the full screen
        barcodeScannerView.FinderViewController.SetFinderEnabled(false);

        // Required for the AR overlay to work faster
        barcodeScannerView.ViewController.BarcodeScanningInterval = 0;

        barcodeScannerView.SelectionOverlayController.SetBarcodeAppearanceDelegate(
            (
                getPolygonStyle: (defaultStyle, _) => defaultStyle.Copy(
                    fillColor: Color.Yellow,
                    strokeColor: Color.Yellow
                ),
                getTextViewStyle: (defaultStyle, _) => defaultStyle.Copy(
                    textColor: Color.Yellow,
                    textContainerColor: Color.Black
                )
            ));

        return barcodeScannerView;
    }
}