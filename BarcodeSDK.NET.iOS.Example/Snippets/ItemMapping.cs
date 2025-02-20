using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKUI2BarcodeScannerScreenConfiguration ItemMapping
    {
        get
        {
            // Create the default configuration object.
            var config = new SBSDKUI2BarcodeScannerScreenConfiguration();

            
            var useCase = new SBSDKUI2SingleScanningMode();

            useCase.ConfirmationSheetEnabled = true;
            useCase.BarcodeInfoMapping = new SBSDKUI2BarcodeInfoMapping()
            {
                BarcodeItemMapper = new CustomMapper()
            };
            config.UseCase = useCase;
            
            return config;
        }
    }

    public class CustomMapper : SBSDKUI2BarcodeItemMapper
    {
        public override void MapBarcodeItemWithItem(SBSDKBarcodeItem barcodeItem, Action<SBSDKUI2BarcodeMappedData> onResult, Action onError)
        {
            var title = $"Some product {barcodeItem.UpcEanExtension}";
            var subTitle = "Subtitle";
            var image = "https://raw.githubusercontent.com/doo/scanbot-sdk-examples/master/sdk-logo.png";

            if (barcodeItem.UpcEanExtension == "Error occurred!")
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
