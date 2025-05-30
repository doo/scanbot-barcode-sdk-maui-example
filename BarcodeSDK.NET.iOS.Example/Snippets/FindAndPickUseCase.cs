using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKUI2BarcodeScannerScreenConfiguration FindAndPickUseCase
    {
        get
        {
            // Create the default configuration object.
            var config = new SBSDKUI2BarcodeScannerScreenConfiguration();
            
            // Create and configure the use case for find and pick scanning mode.
            var useCase = new SBSDKUI2FindAndPickScanningMode();

            // Set the sheet mode for the barcodes preview.
            useCase.Sheet.Mode = SBSDKUI2SheetMode.CollapsedSheet;

            // Enable/Disable the automatic selection.
            useCase.ArOverlay.AutomaticSelectionEnabled = false;

            // Set the height for the collapsed sheet.
            useCase.Sheet.CollapsedVisibleHeight = SBSDKUI2CollapsedVisibleHeight.Large;

            // Enable manual count change.
            useCase.SheetContent.ManualCountChangeEnabled = true;

            // Set the delay before same barcode counting repeat.
            useCase.CountingRepeatDelay = new IntPtr(1000);

            // Configure the submit button.
            useCase.SheetContent.SubmitButton.Text = "Submit";
            useCase.SheetContent.SubmitButton.Foreground.Color = new SBSDKUI2Color("#000000"); //arg string

            // Set the expected barcodes.
            useCase.ExpectedBarcodes = new SBSDKUI2ExpectedBarcode[] {
                new SBSDKUI2ExpectedBarcode(barcodeValue: "123456", title: "numeric barcode", image: "https://avatars.githubusercontent.com/u/1454920", count: new IntPtr(4)),
                new SBSDKUI2ExpectedBarcode(barcodeValue: "SCANBOT", title: "value barcode", image: "https://avatars.githubusercontent.com/u/1454920", count: new IntPtr(4)),
            };

            // Configure other parameters, pertaining to findAndPick-scanning mode as needed.
            config.UseCase = useCase;

            return config;
        }
    }
}