using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static void HandleScanningResultWithImageRef(SBSDKUI2BarcodeScannerUIResult result)
    {
        foreach (var item in result.Items)
        {
            // Saves the stored image at path with the given options
            const string path = "/my_custom_path/my_file.jpg";
            item.Barcode.SourceImage
                ?.SaveImageWithPath(path, options: new SBSDKSaveImageOptions(), out _);

            // Returns the stored image as a byte array.
            var byteArray =
                item.Barcode.SourceImage?.EncodeImageWithOptions(options: new SBSDKEncodeImageOptions(), out _);

            // Clear ImageRef from native memory if not needed anymore
            item.Barcode.SourceImage?.Close();
        }
    }
}