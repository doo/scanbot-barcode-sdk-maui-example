using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKUI2BarcodeScannerScreenConfiguration SingleScanningUseCase
    {
        get
        {
            // Create the default configuration object.
            var config = new SBSDKUI2BarcodeScannerScreenConfiguration();
            
            // Create and configure the use case for single scan mode.
            var useCase = new SBSDKUI2SingleScanningMode();
            
            // Enable and configure the confirmation sheet.
            useCase.ConfirmationSheetEnabled = true;
            useCase.SheetColor = new SBSDKUI2Color("#FFFFFF");

            // Hide/Show the barcode image.
            useCase.BarcodeImageVisible = true;

            // Configure the barcode title of the confirmation sheet.
            useCase.BarcodeTitle.Visible = true;
            useCase.BarcodeTitle.Color = new SBSDKUI2Color("#000000");

            // Configure the barcode subtitle of the confirmation sheet.
            useCase.BarcodeSubtitle.Visible = true;
            useCase.BarcodeSubtitle.Color = new SBSDKUI2Color("#000000");

            // Configure the cancel button of the confirmation sheet.
            useCase.CancelButton.Text = "Close";
            useCase.CancelButton.Foreground.Color = new SBSDKUI2Color("#C8193C");
            useCase.CancelButton.Background.FillColor = new SBSDKUI2Color("#00000000");

            // Configure the submit button of the confirmation sheet.
            useCase.SubmitButton.Text = "Submit";
            useCase.SubmitButton.Foreground.Color = new SBSDKUI2Color("#FFFFFF");
            useCase.SubmitButton.Background.FillColor = new SBSDKUI2Color("#C8193C");

            // Configure other parameters, pertaining to single-scanning mode as needed.
            config.UseCase = useCase;

            // Set an array of accepted barcode types.
            config.ScannerConfiguration = new SBSDKBarcodeScannerConfiguration
            {
                BarcodeFormatConfigurations =
                [
                    new SBSDKBarcodeFormatCommonConfiguration
                    {
                        Formats = BarcodeTypes.Instance.AcceptedTypes,
                        Gs1Handling = SBSDKGS1Handling.DecodeStructure
                    }
                ]
            };

            return config;
        }
    }
}