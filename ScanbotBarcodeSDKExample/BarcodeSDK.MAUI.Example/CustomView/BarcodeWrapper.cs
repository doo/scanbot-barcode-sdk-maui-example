using ScanbotSDK.MAUI.ClassicComponent;

namespace ScanbotSDK.MAUI.Example.CustomView;

public class BarcodeWrapper : ContentView
{
    // public Command<BarcodeResultBundle> SelectBarcodeResult
    // {
    //     set
    //     {
    //         BarcodeScanner.OnSelectBarcodeResult += value;
    //     }
    // }

    public BarcodeScannerView BarcodeScanner { get; set; }

    public BarcodeWrapper()
    {
        Content = new BarcodeScannerView
        {
            OverlayConfiguration = new RTU.v1.SelectionOverlayConfiguration(
                automaticSelectionEnabled: false,
                overlayFormat: BarcodeTextFormat.CodeAndType,
                textColor: Colors.Yellow,
                textContainerColor: Colors.Black,
                strokeColor: Colors.Yellow,
                highlightedStrokeColor: Colors.Red,
                highlightedTextColor: Colors.Yellow,
                highlightedTextContainerColor: Colors.DarkOrchid,
                polygonBackgroundColor: Colors.Transparent,
                polygonBackgroundHighlightedColor: Colors.Transparent),
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Fill,
        };
    }


    public void StartDetection()
    {
        BarcodeScanner.StartDetection();
    }

    public void StopDetection()
    {
        BarcodeScanner.StopDetection();
    }
}