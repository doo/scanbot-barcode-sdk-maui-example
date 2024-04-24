using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;

namespace BarcodeSDK.NET.Droid.Snippets;

public class ItemMappingConfigSnippet
{
    public BarcodeScannerConfiguration GetItemMappingConfigSnippetConfiguration()
    {
        var configuration = new BarcodeScannerConfiguration();
        configuration.UseCase = new SingleScanningMode()
        {
            BarcodeInfoMapping = new BarcodeInfoMapping()
            {
                BarcodeItemMapper = new CustomMapper()
            }
        };

        return configuration;
    }
    
    public class CustomMapper : global::Java.Lang.Object, global::Java.IO.ISerializable, IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration.IBarcodeItemMapper
    {
        public void MapBarcodeItem(BarcodeItem barcodeItem, IBarcodeMappingResult result)
        {
            var title = $"Some product {barcodeItem.TextWithExtension}";
            var subTitle = barcodeItem.Type.Name();
            var image = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png";

            if (barcodeItem.TextWithExtension == "Error occurred!")
            {
                result.OnError();
            }
            else
            {
                result.OnResult(new BarcodeMappedData(title: title, subtitle: subTitle, barcodeImage: image));
            }
        }
    }
}