using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKUI2BarcodeScannerConfiguration ItemMapping
    {
        get
        {
            // Create the default configuration object.
            var config = new SBSDKUI2BarcodeScannerConfiguration();
            
            var useCase = new SBSDKUI2SingleScanningMode();

            useCase.BarcodeInfoMapping = new SBSDKUI2BarcodeInfoMapping()
            {
                BarcodeItemMapper = new CustomMapper()
            };

            // Configure other parameters, pertaining to single-scanning mode as needed.
            config.UseCase = useCase;

            return config;
        }
    }

    public class CustomMapper : SBSDKUI2BarcodeItemMapper
    {
        public void MapBarcodeItem(SBSDKUI2BarcodeItem barcodeItem, Action<SBSDKUI2BarcodeMappedData> onResult, Action onError)
        {
            var title = $"Some product {barcodeItem.TextWithExtension}";
            var subTitle = "Subtitle";
            var image = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png";

            if (barcodeItem.TextWithExtension == "Error occurred!")
            {
                onError();
            }
            else
            {
                onResult(new SBSDKUI2BarcodeMappedData(title: title, subtitle: subTitle, barcodeImage: image));
            }
        }
    }
}