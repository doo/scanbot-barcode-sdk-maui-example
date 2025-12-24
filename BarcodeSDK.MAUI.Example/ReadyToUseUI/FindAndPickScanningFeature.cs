using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Core.Barcode;
using ScanbotSDK.MAUI.Example.Utils;

namespace ScanbotSDK.MAUI.Example.ReadyToUseUI;

public static class FindAndPickScanningFeature
{
    public static async Task StartFindAndPickScanningAsync()
    {
        var configuration = new BarcodeScannerScreenConfiguration();

        // Initialize the use case for multiple scanning.
        var findAndPickConfig = new FindAndPickScanningMode();

        // Set the sheet mode for the barcodes preview.
        findAndPickConfig.Sheet.Mode = SheetMode.CollapsedSheet;

        // Enable/Disable the automatic selection.
        findAndPickConfig.ArOverlay.AutomaticSelectionEnabled = false;

        // Set the height for the collapsed sheet.
        findAndPickConfig.Sheet.CollapsedVisibleHeight = CollapsedVisibleHeight.Large;

        // Enable manual count change.
        findAndPickConfig.SheetContent.ManualCountChangeEnabled = true;

        // Set the delay before same barcode counting repeat.
        findAndPickConfig.CountingRepeatDelay = 1000;

        // Configure the submit button.
        findAndPickConfig.SheetContent.SubmitButton.Text = "Submit";

        findAndPickConfig.SheetContent.SubmitButton.Foreground.Color = new ColorValue("#000000"); //arg string

        // Set the expected barcodes.
        findAndPickConfig.ExpectedBarcodes =
        [
            new ExpectedBarcode(barcodeValue: "123456", title: "numeric barcode",
                image: "https://avatars.githubusercontent.com/u/1454920", count: 4),
            new ExpectedBarcode(barcodeValue: "SCANBOT", title: "value barcode",
                image: "https://avatars.githubusercontent.com/u/1454920", count: 4)
        ];

        // Configure other parameters, pertaining to findAndPick-scanning mode as needed.
        configuration.UseCase = findAndPickConfig;
        configuration.ScannerConfiguration.BarcodeFormatConfigurations =
        [
            new BarcodeFormatCommonConfiguration
            {
                Formats = BarcodeTypes.Instance.AcceptedTypes
            }
        ];

        var rtuResult = await ScanbotSDKMain.Barcode.StartScannerAsync(configuration);
        if (rtuResult.IsSuccess)
            await CommonUtils.DisplayResults(rtuResult.ValueOrNull);
    }
}