using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Core.Barcode;
using ScanbotSDK.MAUI.Example.Utils;

namespace ScanbotSDK.MAUI.Example.ReadyToUseUI;

public static class SingleScanningFeature
{
    /// <summary>
    /// Starts the Barcode scanning.
    /// </summary>
    public static async Task StartSingleScanningAsync()
    {
        // Create the default configuration object.
        var config = new BarcodeScannerScreenConfiguration();

        // Create single scanning mode.
        var useCase = new SingleScanningMode();

        // Enable and configure the confirmation sheet.
        useCase.ConfirmationSheetEnabled = true;

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
        
        // Enable return of barcode image.
        config.ScannerConfiguration.ReturnBarcodeImage = true;

        // Launch the barcode scanner.
        var rtuResult = await ScanbotSDKMain.Barcode.StartScannerAsync(configuration: config);

        // Comment out the above and use the below to try some of our snippets instead:
        // var rtuResult = await ScanbotSDKMain.BarcodeScanner.StartScannerAsync(Snippets.SingleScanningUseCase);
        // Or Snippets.MultipleScanningUseCase, Snippets.FindAndPickUseCase, Snippets.ActionBar, etc.

        if (rtuResult.IsSuccess)
            await CommonUtils.DisplayResults(rtuResult.Value);
    }
}