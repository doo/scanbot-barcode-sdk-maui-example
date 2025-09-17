using System.Globalization;
using ScanbotSDK.MAUI.Core.Barcode;

namespace ScanbotSDK.MAUI.Example.ClassicUI.MVVM.Converters;

public class BarcodeDetectionEventArgsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var barcodeItems = value as BarcodeItem[];
        return barcodeItems;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}