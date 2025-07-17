using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Example.Utils;

namespace ScanbotSDK.MAUI.Example.ReadyToUseUI;

public static class SingleScanningFeature
{
    /// <summary>
    /// Starts the Barcode scanning.
    /// </summary>
    public static async Task StartSingleScanningAsync()
    {
        try
        {
            // Create the default configuration object.
            var config = new BarcodeScannerScreenConfiguration();

            // Create single scanning mode.
            var useCase = new SingleScanningMode();

            // Enable and configure the confirmation sheet.
            useCase.ConfirmationSheetEnabled = true;

            // Configure other parameters, pertaining to single-scanning mode as needed.
            config.UseCase = useCase;

            // Set an array of accepted barcode types.
            config.ScannerConfiguration.BarcodeFormats = BarcodeTypes.Instance.AcceptedTypes;

            // Set an array of accepted barcode types.
            config.ScannerConfiguration.Gs1Handling = Gs1Handling.DecodeStructure;

            // Launch the barcode scanner.
            var rtuResult = await ScanbotSDKMain.RTU.BarcodeScanner.LaunchAsync(configuration: config);
            if (rtuResult.Status != OperationResult.Ok)
                return;

            // Comment out the above and use the below to try some of our snippets instead:
            // var result = await ScanbotSDKMain.RTU.BarcodeScanner.LaunchAsync(Snippets.SingleScanningUseCase);
            // Or Snippets.MultipleScanningUseCase, Snippets.FindAndPickUseCase, Snippets.ActionBar, etc.

            await CommonUtils.DisplayResults(rtuResult.Result);
        }
        catch (Exception ex)
        {
            // for any other errors that occur
            Console.WriteLine(ex.Message);
        }
    }
}