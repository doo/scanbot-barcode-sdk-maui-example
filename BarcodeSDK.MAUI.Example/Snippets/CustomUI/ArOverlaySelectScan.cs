using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Barcode.ClassicComponent;

namespace ScanbotSDK.MAUI.Example;

public partial class Snippets
{
    public static BarcodeScannerView EnableSelectScanArOverlay(BarcodeScannerView cameraView)
    {
        cameraView.OverlayConfiguration = new SelectionOverlayConfiguration(
            overlayFormat: BarcodeTextFormat.CodeAndType,
            textColor: Colors.Yellow,
            textContainerColor: Colors.Black,
            strokeColor: Colors.Yellow,
            highlightedStrokeColor: Colors.Red,
            highlightedTextColor: Colors.Red,
            highlightedTextContainerColor: Colors.Black,
            polygonBackgroundColor: Colors.Transparent,
            polygonBackgroundHighlightedColor: Colors.Transparent);

        cameraView.OnSelectBarcodeResult += (sender, items) =>
        {
            // Handle selected barcodes
            Console.WriteLine(items);
        };

        return cameraView;
    }
}