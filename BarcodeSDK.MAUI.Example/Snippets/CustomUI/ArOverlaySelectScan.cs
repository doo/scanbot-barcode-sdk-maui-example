using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Barcode.ClassicComponent;

namespace ScanbotSDK.MAUI.Example;

public partial class Snippets
{
    public static BarcodeScannerView EnableSelectScanArOverlay(BarcodeScannerView cameraView)
    {
        cameraView.OverlayConfiguration = new SelectionOverlayConfiguration
        {
            OverlayEnabled = true,
            PolygonConfiguration = new OverlayPolygonConfiguration.Style
            {
                StrokeColor = Colors.Yellow,
                HighlightedStrokeColor = Colors.Red,
                PolygonColor = Colors.Transparent,
                HighlightedPolygonColor = Colors.Transparent
            },
            TextConfiguration = new OverlayTextConfiguration.Style
            {
                TextFormat = BarcodeTextFormat.CodeAndType,
                TextColor = Colors.Yellow,
                TextContainerColor = Colors.Black,
                HighlightedTextColor = Colors.Red,
                HighlightedTextContainerColor = Colors.Black,
            }
        };

        cameraView.OnSelectBarcodeResult += (sender, items) =>
        {
            // Handle selected barcodes
            Console.WriteLine(items);
        };

        return cameraView;
    }
}