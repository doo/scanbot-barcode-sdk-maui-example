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
            if (barcodeItem.Text == "Error occurred!")
            {
                onError();
            }
            else
            {
                var title = $"Some product {barcodeItem.Text}";
                var subTitle = "Subtitle";
                var image = "https://avatars.githubusercontent.com/u/1454920";
                
                onResult(new SBSDKUI2BarcodeMappedData(title: title, subtitle: subTitle, barcodeImage: image));
            }
        }
    }
}
