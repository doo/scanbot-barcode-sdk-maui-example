using ScanbotSDK.MAUI.Common;
using ScanbotSDK.MAUI.Example.Utils;
using ImageSource = Microsoft.Maui.Controls.ImageSource;

namespace ScanbotSDK.MAUI.Example;

public partial class Snippets
{
    public static async Task ConfigureMockCameraAsync()
    {
        var imagePath = await ImagePicker.PickImageAsPathAsync();
        if (string.IsNullOrWhiteSpace(imagePath)) return;

        if (ImageSource.FromFile(imagePath) is not FileImageSource fileImage) return;

        ScanbotSDKMain.MockCamera(fileImage.File, true);
    }
}