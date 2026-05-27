using IO.Scanbot.Sdk.Barcode;
using IO.Scanbot.Sdk.Image;
using ScanbotSDK.Droid.Helpers;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static void HandleScanningResultWithImageRef(BarcodeScannerResult result)
    {
        try
        {
            foreach (var item in result.Barcodes)
            {
                // Saves the stored image at path with the given options
                const string path = "/my_custom_path/my_file.jpg";
                _ = item.SourceImage
                    ?.SaveImage(path, options: new SaveImageOptions()).OrThrow;

                // Returns the stored image as byte[].
                var byteArray = item.SourceImage
                    ?.EncodeImage(options: new EncodeImageOptions()).GetBytesOrThrow();

                // Clear ImageRef from native memory if not needed anymore
                item.SourceImage?.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}