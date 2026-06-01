using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKBarcodeFormatCommonConfiguration FilterGroupBarcodesConfig
    {
        get
        {
            var barcodeFormatCommonConfiguration = new SBSDKBarcodeFormatCommonConfiguration
            {
                RegexFilter = "",
                Minimum1DQuietZoneSize = 10,
                StripCheckDigits = false,
                MinimumTextLength = 0,
                MaximumTextLength = 0,
                Gs1Handling = SBSDKGS1Handling.Parse,
                StrictMode = true,
                Formats = SBSDKBarcodeFormats.Common,
                AddAdditionalQuietZone = false
            };

            return barcodeFormatCommonConfiguration;
        }
    }
}