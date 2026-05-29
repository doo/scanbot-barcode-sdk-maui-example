using System.Drawing;
using ScanbotSDK.MAUI.Core.Image;
using ScanbotSDK.MAUI.Image;

namespace ScanbotSDK.MAUI.Example;

public partial class Snippets
{
    public static void CreateImageRefFromEncodedBuffer(byte[] encodedBuffer)
    {
        // Create ImageRef from buffer
        var imageRef = ImageRef.FromEncodedBuffer(encodedBuffer);

        // Create ImageRef from buffer with options
        var imageRefWithOptions = ImageRef.FromEncodedBuffer(
            encodedBuffer,
            options: new BufferImageLoadOptions()
            {
                // Define crop rectangle
                CropRect = new Rectangle(0, 0, 200, 200),
                // Convert image to grayscale
                ColorConversion = ColorConversion.Gray,
                // Use lazy loading mode, image would be loaded into memory only when first used
                LoadMode = BufferLoadMode.Lazy,
            }
        );
    }
}