using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using IO.Scanbot.Sdk.Ui_v2.Common;

namespace BarcodeSDK.NET.Droid.Snippets;

public class SingleScanningUseCaseSnippet
{
    // Create the default configuration object.
    public BarcodeScannerConfiguration GetSingleScanningUseCaseSnippetConfiguration()
    {
        var configuration = new BarcodeScannerConfiguration();
        configuration.UseCase = new SingleScanningMode()
        {
            // Enable and configure the confirmation sheet.
            ConfirmationSheetEnabled = true,
            SheetColor = new ScanbotColor("#FFFFFF"),
            
            // Hide/unhide the barcode image.
            BarcodeImageVisible = true,
            
            // Configure the barcode title of the confirmation sheet.
            BarcodeTitle = new StyledText()
            {
                Visible = true,
                Color = new ScanbotColor("#000000"),
            },
            
            // Configure the barcode subtitle of the confirmation sheet.
            BarcodeSubtitle = new StyledText()
            {   
                Visible = true,
                Color = new ScanbotColor("#000000"),
            },
            
            // Configure the cancel button of the confirmation sheet.
            CancelButton = new ButtonConfiguration()
            {
                Text = "Close",
                Foreground = new ForegroundStyle()
                {
                    Color = new ScanbotColor("#C8193C"),
                },
                Background = new BackgroundStyle()
                {
                    FillColor = new ScanbotColor("#00000000"),
                },
            },
            
            // Configure the submit button of the confirmation sheet.
            SubmitButton = new ButtonConfiguration()
            {   
                Text = "Submit",
                Foreground = new ForegroundStyle()
                {
                    Color = new ScanbotColor("#FFFFFF"),
                },
                Background = new BackgroundStyle()
                {
                    FillColor = new ScanbotColor("#C8193C")
                }
            },
            
            // Configure other parameters, pertaining to single-scanning mode as needed.
        };
        
        // Set an array of accepted barcode types.
        configuration.RecognizerConfiguration.BarcodeFormats = BarcodeFormat.Values().ToList();

        return configuration;
    }
}