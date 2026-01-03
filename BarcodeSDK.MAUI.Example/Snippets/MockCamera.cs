using ScanbotSDK.MAUI.Common;
using ImageSource = Microsoft.Maui.Controls.ImageSource;

namespace ScanbotSDK.MAUI.Example;

public partial class Snippets
{
    public static async Task ConfigureMockCameraAsync()
    {
        var image = await MediaPicker.Default.PickPhotoAsync();
        if (image != null)
        {
            if (ImageSource.FromFile(image.FullPath) is not FileImageSource fileImage)
                return;

            ScanbotSDKMain.CommonOperations.ConfigureMockCamera(new MockCameraConfiguration(fileImage.File, fileImage.File, "Barcode SDK Mock Camera", true));
        }
    }
}