using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Barcode.ClassicComponent;
using ScanbotSDK.MAUI.Core.Barcode;

namespace ScanbotSDK.MAUI.Example;

// For BarcodeScannerView - Classic Component
public class BarcodeItemMapper
{
    private readonly BarcodeScannerView _scannerView;

    public BarcodeItemMapper()
    {
        _scannerView = new BarcodeScannerView();
        
        // Set a single common AR overlay style for all barcode items.
        SetupCommonArOverlayStyle();

        // Set a unique AR overlay style per barcode items.
        SetupUniqueBarcodeArOverlayStyle();
    }
    
    private void SetupCommonArOverlayStyle()
    {
        _scannerView.OverlayConfiguration = new SelectionOverlayConfiguration
        {
            OverlayEnabled = true,
            PolygonConfiguration = new OverlayPolygonConfiguration.Style
            {
                StrokeColor = Colors.Black,
                HighlightedStrokeColor = Colors.White,
                
                // Uncomment to set the polygon fill color
                // PolygonColor = Colors.Black, 
                // HighlightedPolygonColor = Colors.Black,
            },
            TextConfiguration = new OverlayTextConfiguration.Style
            {
                TextFormat = BarcodeTextFormat.CodeAndType,
                TextColor = Colors.White,
                TextContainerColor = Colors.Black,
                HighlightedTextColor = Colors.Black,
                HighlightedTextContainerColor = Colors.White
            }
        };
    }
    

    private void SetupUniqueBarcodeArOverlayStyle()
    {
        
        var overlayConfiguration = new SelectionOverlayConfiguration
        {
            OverlayEnabled = true,
            PolygonConfiguration = new OverlayPolygonConfiguration.StyleForBarcodeItem(OverlayPolygonStyleForBarcode),
            TextConfiguration = new OverlayTextConfiguration.StyleForBarcodeItem(OverlayTextStyleForBarcode, OverlayOverrideTextForBarcode)
        };

        _scannerView.OverlayConfiguration = overlayConfiguration;
    }

    private string OverlayOverrideTextForBarcode(BarcodeItem item)
    {
        return "Some Text";
    }

    private OverlayTextConfiguration.Style OverlayTextStyleForBarcode(BarcodeItem item)
    {
        return new OverlayTextConfiguration.Style
        {
            TextFormat = BarcodeTextFormat.CodeAndType,
            TextColor = Colors.White,
            TextContainerColor = Colors.Black,
            HighlightedTextColor = Colors.Black,
            HighlightedTextContainerColor = Colors.White
        };
    }

    private OverlayPolygonConfiguration.Style OverlayPolygonStyleForBarcode(BarcodeItem item)
    {
        return new OverlayPolygonConfiguration.Style
        {
            StrokeColor =  Colors.Black,
            HighlightedStrokeColor =  Colors.White,
            
            // Uncomment to set the polygon fill color
            // PolygonColor = Colors.Black, 
            // HighlightedPolygonColor = Colors.Black,
        };
    }
}