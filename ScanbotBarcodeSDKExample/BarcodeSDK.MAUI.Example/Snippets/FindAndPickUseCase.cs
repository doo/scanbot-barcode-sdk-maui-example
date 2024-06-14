namespace ScanbotSDK.MAUI.Example
{
    public partial class Snippets
    {
        public static BarcodeScannerConfiguration FindAndPickUseCase
        {
            get
            {
                // Create the default configuration object.
                var config = new BarcodeScannerConfiguration();
                
                // Initialize the use case for multiple scanning.
                var useCase = new FindAndPickScanningMode();

                // Set the sheet mode for the barcodes preview.
                useCase.Sheet.Mode = SheetMode.CollapsedSheet;

                // Enable/Disable the automatic selection.
                useCase.ArOverlay.AutomaticSelectionEnabled = false;

                // Set the height for the collapsed sheet.
                useCase.Sheet.CollapsedVisibleHeight = CollapsedVisibleHeight.Large;

                // Enable manual count change.
                useCase.SheetContent.ManualCountChangeEnabled = true;

                // Set the delay before same barcode counting repeat.
                useCase.CountingRepeatDelay = 1000;

                // Configure the submit button.
                useCase.SheetContent.SubmitButton.Text = "Submit";
                
                useCase.SheetContent.SubmitButton.Foreground.Color = new ColorValue("#000000"); //arg string

                // Set the expected barcodes.
                useCase.ExpectedBarcodes = new ExpectedBarcode[]
                {
                    new ExpectedBarcode(barcodeValue: "123456", title: "numeric barcode", image: "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png", count: 4),
                    new ExpectedBarcode(barcodeValue: "SCANBOT", title: "value barcode", image: "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png", count: 4),
                };

                // Configure other parameters, pertaining to findAndPick-scanning mode as needed.
                config.UseCase = useCase;
                config.RecognizerConfiguration.BarcodeFormats = BarcodeFormats.Common;
    
                return config;
            }
        }
    }
}