using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Core.Image;

namespace ScanbotSDK.MAUI.Example;

public partial class Snippets
{
    public static async Task HandleScanningResultWithImageRefAsync()
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

        // The scanner was canceled.
        if (rtuResult.IsCanceled)
        {
            return;
        }

        // The scanning was failed
        if (!rtuResult.IsSuccess && rtuResult.Error != null)
        {
            Console.WriteLine(rtuResult.Error);
            return;
        }

        // The scanning was successful
        if (rtuResult.IsSuccess)
        {
            foreach (var item in rtuResult.Value.Items)
            {
                // Saves the stored image at path with the given options
                const string path = "/my_custom_path/my_file.jpg";
                item.Barcode.SourceImage
                    ?.SaveImage(path, options: new SaveImageOptions());

                // Returns the stored image as a byte array.
                var byteArray = item.Barcode.SourceImage
                    ?.EncodeImage(options: new EncodeImageOptions());

                // Releases native resources stored by the ref.
                item.Barcode.SourceImage?.Clear();
            }
        }
    }
}