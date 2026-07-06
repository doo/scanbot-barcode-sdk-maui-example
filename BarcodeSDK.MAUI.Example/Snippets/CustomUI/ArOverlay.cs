using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Barcode.ClassicComponent;

namespace ScanbotSDK.MAUI.Example;

public partial class Snippets
{
    public static BarcodeScannerView EnableArOverlay(BarcodeScannerView cameraView)
    {
        cameraView.OverlayConfiguration = new SelectionOverlayConfiguration();

        return cameraView;
    }
}