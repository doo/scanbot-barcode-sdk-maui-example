using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Core.Image;

namespace ScanbotSDK.MAUI.Example;

public partial class Snippets
{
    public static async Task<List<byte[]>> HandleScanningResultWithEncodedImageRefAsync()
    {
        var config = new BarcodeScannerScreenConfiguration
        {
            ScannerConfiguration =
            {
                ReturnBarcodeImage = true
            }
        };

        // Launch the barcode scanner.
        var rtuResult = await ScanbotSDKMain.Barcode.StartScannerAsync(configuration: config);

        List<byte[]> imageBuffers = [];

        // The scanning was failed
        if (!rtuResult.IsSuccess && rtuResult.Error != null)
        {
            Console.WriteLine(rtuResult.Error);
        }

        // The scanning was successful
        if (rtuResult.IsSuccess)
        {
            foreach (var item in rtuResult.Value.Items)
            {
                var bytes = item.Barcode.SourceImage?.EncodeImage(options: new EncodeImageOptions());
                if (bytes != null)
                {
                    imageBuffers.Add(bytes);
                }

                // Releases native resources stored by the ref.
                item.Barcode.SourceImage?.Clear();
            }
        }

        return imageBuffers;
    }
}