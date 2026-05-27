using ScanbotSDK.MAUI.Core.Barcode;

namespace ScanbotSDK.MAUI.Example;

public partial class Snippets
{
    public static BarcodeFormatCommonConfiguration FilterGroupBarcodesConfig
    {
        get
        {
            var barcodeFormatCommonConfiguration = new BarcodeFormatCommonConfiguration
            {
                RegexFilter = "",
                Minimum1DQuietZoneSize = 10,
                StripCheckDigits = false,
                MinimumTextLength = 0,
                MaximumTextLength = 0,
                Gs1Handling = Gs1Handling.Parse,
                StrictMode = true,
                Formats = BarcodeFormats.Common,
                AddAdditionalQuietZone = false
            };

            return barcodeFormatCommonConfiguration;
        }
    }
}