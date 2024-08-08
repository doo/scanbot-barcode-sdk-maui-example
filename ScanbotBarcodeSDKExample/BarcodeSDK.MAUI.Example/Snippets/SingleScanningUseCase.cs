using ScanbotSDK.MAUI.Barcode;

namespace ScanbotSDK.MAUI.Example
{
    public partial class Snippets
    {
        public static BarcodeScannerConfiguration SingleScanningUseCase
        {
            get
            {
                // Create the default configuration object.
                var config = new BarcodeScannerConfiguration();
                
                var useCase = new SingleScanningMode();
                // Enable and configure the confirmation sheet.
                useCase.ConfirmationSheetEnabled = true;
                useCase.SheetColor = new ColorValue("#FFFFFF");

                // Hide/unhide the barcode image.
                useCase.BarcodeImageVisible = true;

                // Configure the barcode title of the confirmation sheet.
                useCase.BarcodeTitle.Visible = true;
                useCase.BarcodeTitle.Color = new ColorValue("#000000");

                // Configure the barcode subtitle of the confirmation sheet.
                useCase.BarcodeSubtitle.Visible = true;
                useCase.BarcodeSubtitle.Color = new ColorValue("#000000");

                // Configure the cancel button of the confirmation sheet.
                useCase.CancelButton.Text = "Close";
                useCase.CancelButton.Foreground.Color = new ColorValue("#C8193C");
                useCase.CancelButton.Background.FillColor = new ColorValue("#00000000");

                // Configure the submit button of the confirmation sheet.
                useCase.SubmitButton.Text = "Submit";
                useCase.SubmitButton.Foreground.Color = new ColorValue("#FFFFFF");
                useCase.SubmitButton.Background.FillColor = new ColorValue("#C8193C");

                // Configure other parameters, pertaining to single-scanning mode as needed.
                config.UseCase = useCase;

                config.RecognizerConfiguration.BarcodeFormats = BarcodeFormats.Common;

                return config;
            }
        }
    }
}