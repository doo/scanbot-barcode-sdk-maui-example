using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static List<NSData> HandleScanningResultWithEncodedImageRef(SBSDKUI2BarcodeScannerUIResult result)
    {
        List<NSData> imageBuffers = [];

        foreach (var item in result.Items)
        {
            // Returns the stored image as a byte array.
            var nsData =
                item.Barcode.SourceImage?.EncodeImageWithOptions(options: new SBSDKEncodeImageOptions(), out _);
            imageBuffers.Add(nsData);

            // Clear ImageRef from native memory if not needed anymore
            item.Barcode.SourceImage?.Close();
        }

        return imageBuffers;
    }
}