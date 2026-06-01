using ScanbotSDK.MAUI.Image;

namespace ScanbotSDK.MAUI.Example;

public partial class Snippets
{
    public static void GetImageInfo(ImageRef imageRef)
    {
        var imageInfo = imageRef.Info();

        Console.WriteLine(imageInfo);
    }
}