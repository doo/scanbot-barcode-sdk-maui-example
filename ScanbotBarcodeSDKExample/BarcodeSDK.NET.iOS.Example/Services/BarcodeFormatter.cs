using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS
{
    public class BarcodeFormatter
    {
        public static readonly BarcodeFormatter Instance = new BarcodeFormatter();

        public string GetText(SBSDKBarcodeScannerResult barcode)
        {
            return barcode.ToString();
        }
    }
}
