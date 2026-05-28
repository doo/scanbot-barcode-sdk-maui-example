using ScanbotSDK.MAUI.Core.Barcode;
using ScanbotSDK.MAUI.Image;

namespace ScanbotSDK.MAUI.Example;

public partial class Snippets
{
    public static async Task ScanBarcodeFromImageRefAsync(string imagePath)
    {
        // Create ImageRef from path
        var imageRef = ImageRef.FromPath(imagePath);

        // Scan a barcode from ImageRef
        var result =
            await ScanbotSDKMain.Barcode.ScanFromImageAsync(imageRef,
                configuration: new BarcodeScannerConfiguration());

        // Get value or null if unsuccessful
        var value = result.ValueOrNull;
        if (value != null)
        {
            var scanningResult = result.Value;
            Console.WriteLine(scanningResult);
        }
    }
}