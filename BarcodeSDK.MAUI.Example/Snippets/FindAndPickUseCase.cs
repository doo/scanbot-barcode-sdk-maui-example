using ScanbotSDK.MAUI.Barcode;

namespace ScanbotSDK.MAUI.Example
{
    public partial class Snippets
    {
        public static BarcodeScannerScreenConfiguration FindAndPickUseCase
        {
            get
            {
                // Create the default configuration object.
                var config = new BarcodeScannerScreenConfiguration();
                
                // Create and configure the use case for find and pick scanning mode.
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
                useCase.ExpectedBarcodes =
                [
                    new ExpectedBarcode(barcodeValue: "123456", title: "numeric barcode", image: "https://avatars.githubusercontent.com/u/1454920", count: 4),
                    new ExpectedBarcode(barcodeValue: "SCANBOT", title: "value barcode", image: "https://avatars.githubusercontent.com/u/1454920", count: 4),
                ];

                // Configure other parameters, pertaining to findAndPick-scanning mode as needed.
                config.UseCase = useCase;
                config.ScannerConfiguration.BarcodeFormats = BarcodeFormats.Common;
    
                return config;
            }
        }
    }
}