using IO.Scanbot.Sdk.Barcode;
using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static BarcodeScannerScreenConfiguration ItemMapping
    {
        get
        {
            // Create the default configuration object.
            var config = new BarcodeScannerScreenConfiguration();
            
            // Create and configure the use case for single scanning mode.
            var useCase = new SingleScanningMode();

            useCase.ConfirmationSheetEnabled = true;
            useCase.BarcodeInfoMapping = new BarcodeInfoMapping()
            {
                BarcodeItemMapper = new CustomMapper()
            };

            // Configure other parameters, pertaining to single-scanning mode as needed.
            config.UseCase = useCase;

            return config;
        }
    }
    
    public class CustomMapper : global::Java.Lang.Object, IBarcodeItemMapper
    {
        public void MapBarcodeItem(BarcodeItem item, IBarcodeMappingResultCallback resultCallback, IBarcodeMappingErrorCallback errorCallback)
        {
            var title = $"Some product {item.Text}";
            var subTitle = item.Format.Name();
            var image = "https://avatars.githubusercontent.com/u/1454920";
            
            if (item.Text == "Error occurred!")
            {
                errorCallback.OnError();
            }
            else
            {
                resultCallback.OnResult(new BarcodeMappedData(title: title, subtitle: subTitle, barcodeImage: image));
            }
        }
    }
}
