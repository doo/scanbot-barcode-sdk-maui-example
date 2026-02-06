using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Core.Barcode;
using ScanbotSDK.MAUI.Example.Utils;

namespace ScanbotSDK.MAUI.Example
{
    public partial class Snippets
    {
        public static BarcodeScannerScreenConfiguration MultipleScanningUseCase
        {
            get
            {
                // Create the default configuration object.
                var config = new BarcodeScannerScreenConfiguration();
                
                // Create and configure the use case for multiple scanning mode.
                var useCase = new MultipleScanningMode();
                
                // Set the counting mode.
                useCase.Mode = MultipleBarcodesScanningMode.Counting;

                // Set the sheet mode for the barcodes preview.
                useCase.Sheet.Mode = SheetMode.CollapsedSheet;

                // Set the height for the collapsed sheet.
                useCase.Sheet.CollapsedVisibleHeight = CollapsedVisibleHeight.Large;

                // Enable manual count change.
                useCase.SheetContent.ManualCountChangeEnabled = true;

                // Set the delay before same barcode counting repeat.
                useCase.CountingRepeatDelay = 1000;

                // Configure the submit button.
                useCase.SheetContent.SubmitButton.Text = "Submit";
                useCase.SheetContent.SubmitButton.Foreground.Color = new ColorValue("#000000");

                // Configure other parameters, pertaining to single-scanning mode as needed.
                config.UseCase = useCase;

                // create barcode format configurations
                var barcodeFormatConfiguration = new BarcodeFormatCommonConfiguration
                {
                    // Set an array of accepted barcode types.
                    Formats = BarcodeTypes.Instance.AcceptedTypes,
                    // Set an array of accepted barcode types.
                    Gs1Handling = Gs1Handling.DecodeStructure
                };

                // Set an array of barcode format configurations
                config.ScannerConfiguration.BarcodeFormatConfigurations = [barcodeFormatConfiguration];

                return config;
            }
        }
    }
}