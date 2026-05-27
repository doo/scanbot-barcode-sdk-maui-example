using IO.Scanbot.Sdk.Barcode;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
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