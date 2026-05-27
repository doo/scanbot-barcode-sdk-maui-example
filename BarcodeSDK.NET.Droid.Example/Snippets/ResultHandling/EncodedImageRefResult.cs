using IO.Scanbot.Sdk.Barcode;
using IO.Scanbot.Sdk.Image;
using ScanbotSDK.Droid.Helpers;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static List<byte[]> HandleScanningResultWithEncodedImageRef(BarcodeScannerResult result)
    {
        List<byte[]> imageBuffers = [];

        try
        {
            foreach (var item in result.Barcodes)
            {
                // Returns the stored image as a byte array.
                var byteArray = item.SourceImage?.EncodeImage(options: new EncodeImageOptions()).GetBytesOrThrow();
                imageBuffers.Add(byteArray);

                // Clear ImageRef from native memory if not needed anymore
                item.SourceImage?.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return imageBuffers;
    }
}