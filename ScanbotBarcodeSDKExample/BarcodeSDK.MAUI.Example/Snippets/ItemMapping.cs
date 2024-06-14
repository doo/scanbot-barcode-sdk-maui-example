namespace ScanbotSDK.MAUI.Example
{
    public partial class Snippets
    {
        public static BarcodeScannerConfiguration ItemMapping
        {
            get
            {
                // Create the default configuration object.
                var config = new BarcodeScannerConfiguration();
                
                var useCase = new SingleScanningMode();

                useCase.ConfirmationSheetEnabled = true;
                useCase.BarcodeInfoMapping = new BarcodeInfoMapping()
                {
                    BarcodeItemMapper = new DelegateBarcodeItemMapper((barcodeItem, onResult, onError) => {
                        var title = $"Some product {barcodeItem.TextWithExtension}";
                        var subTitle = "Subtitle";
                        var image = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png";

                        if (barcodeItem.TextWithExtension == "Error occurred!")
                        {
                            onError();
                        }
                        else
                        {
                            onResult(new BarcodeMappedData(title: title, subtitle: subTitle, barcodeImage: image));
                        }
                    })
                };

                // Configure other parameters, pertaining to single-scanning mode as needed.
                config.UseCase = useCase;

                return config;
            }
        }
    }
}