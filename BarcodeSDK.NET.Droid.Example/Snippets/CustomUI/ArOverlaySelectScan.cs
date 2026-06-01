using Android.Graphics;
using IO.Scanbot.Sdk.Barcode;
using IO.Scanbot.Sdk.Barcode.UI;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static BarcodeScannerView EnableSelectScanArOverlay(BarcodeScannerView barcodeScannerView, IBarcodeScanner barcodeScanner)
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
                ),
                getOverrideText: (defaultText, _) => "Some Text" // Displayed inside the TextContainer of the AR Overlay.
            ));

        barcodeScannerView.InitCamera();
        barcodeScannerView.InitScanningBehavior(barcodeScanner: barcodeScanner,
            onBarcodeResult: (_, _) => false,
            scannerViewCallbacks: (
                onCameraOpen: () => { },
                onPictureTaken: (_, _) => { },
                onSelectionOverlayBarcodeClicked: OnSelectionOverlayBarcodeClicked
            ));

        return barcodeScannerView;
    }

    private static void OnSelectionOverlayBarcodeClicked(BarcodeItem e)
    {
        // Handle selected barcode item
        Console.WriteLine(e);
    }
}