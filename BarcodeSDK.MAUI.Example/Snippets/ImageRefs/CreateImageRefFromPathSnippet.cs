using System.Drawing;
using ScanbotSDK.MAUI.Core.Image;
using ScanbotSDK.MAUI.Image;

namespace ScanbotSDK.MAUI.Example;

public partial class Snippets
{
    public static void CreateImageRefFromPath(string imagePath)
    {
        // Create ImageRef from path
        var imageRef = ImageRef.FromPath(imagePath);

        // Create ImageRef from path with options
        var imageRefWithOptions = ImageRef.FromPath(
            imagePath,
            options: new PathImageLoadOptions
            {
                // Define crop rectangle
                CropRect = new Rectangle(0, 0, 200, 200),
                // Convert image to grayscale
                ColorConversion = ColorConversion.Gray,
                // Use lazy loading mode, image would be loaded into memory only when first used
                LoadMode = PathLoadMode.LazyWithCopy,
                // handle encryption automatically based on global ImageRef/ScanbotSdk encryption settings
                EncryptionMode = EncryptionMode.Auto,
                // to disable decryption while reading for this specific file (in case its not encrypted with SDK encryption ON), use
                // EncryptionMode = EncryptionMode.Disabled,
            }
        );
    }
}