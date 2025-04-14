using IO.Scanbot.Sdk.Barcode;
using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using IO.Scanbot.Sdk.Ui_v2.Common;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static BarcodeScannerScreenConfiguration ItemMapping
    {
        get
        {
            // Create the default configuration object.
            var config = new BarcodeScannerScreenConfiguration();
            
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
    
    public class CustomMapper : global::Java.Lang.Object, IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration.IBarcodeItemMapper
    {
        public void MapBarcodeItem(BarcodeItem item, IBarcodeMappingResultCallback resultCallback, IBarcodeMappingErrorCallback errorCallback)
        {
            var title = $"Some product {item.UpcEanExtension}";
            var subTitle = item.Format.Name();
            var image = "https://raw.githubusercontent.com/doo/scanbot-sdk-examples/master/sdk-logo.png";
            
            if (item.UpcEanExtension == "Error occurred!")
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
