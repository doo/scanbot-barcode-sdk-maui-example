using System.Globalization;

namespace ScanbotSDK.MAUI.Example.Utils.XamlValueConverters;

public class ImageRefToSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var imageRef = value as ScanbotSDK.MAUI.ImageRef;
        return ImageSource.FromStream(() => imageRef?.ToPlatformImage()?.AsStream(ImageFormat.Jpeg));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}