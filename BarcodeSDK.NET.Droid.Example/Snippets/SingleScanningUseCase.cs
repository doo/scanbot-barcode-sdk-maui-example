using IO.Scanbot.Sdk.Barcode;
using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using IO.Scanbot.Sdk.Ui_v2.Common;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static BarcodeScannerScreenConfiguration SingleScanningUseCase
    {
        get
        {
            // Create the default configuration object.
            var config = new BarcodeScannerScreenConfiguration();
            
            var useCase = new SingleScanningMode();
            // Enable and configure the confirmation sheet.
            useCase.ConfirmationSheetEnabled = true;
            useCase.SheetColor = new ScanbotColor("#FFFFFF");

            // Hide/Show the barcode image.
            useCase.BarcodeImageVisible = true;

            // Configure the barcode title of the confirmation sheet.
            useCase.BarcodeTitle.Visible = true;
            useCase.BarcodeTitle.Color = new ScanbotColor("#000000");

            // Configure the barcode subtitle of the confirmation sheet.
            useCase.BarcodeSubtitle.Visible = true;
            useCase.BarcodeSubtitle.Color = new ScanbotColor("#000000");

            // Configure the cancel button of the confirmation sheet.
            useCase.CancelButton.Text = "Close";
            useCase.CancelButton.Foreground.Color = new ScanbotColor("#C8193C");
            useCase.CancelButton.Background.FillColor = new ScanbotColor("#00000000");

            // Configure the submit button of the confirmation sheet.
            useCase.SubmitButton.Text = "Submit";
            useCase.SubmitButton.Foreground.Color = new ScanbotColor("#FFFFFF");
            useCase.SubmitButton.Background.FillColor = new ScanbotColor("#C8193C");

            // Configure other parameters, pertaining to single-scanning mode as needed.
            config.UseCase = useCase;

            // Set an array of accepted barcode types. TODO fix in the binding library
            config.ScannerConfiguration.BarcodeFormats = BarcodeFormats.Common;
            

            return config;
        }
    }
}