using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Example.Utils;

namespace ScanbotSDK.MAUI.Example.ReadyToUseUI;

public static class SingleScanningWithArOverlayFeature
{
    /// <summary>
    /// Starts the Barcode scanning With Ar Overlay.
    /// </summary>
    public static async Task StartSingleScanningWithArOverlayAsync()
    {
        try
        {
            // Create the default configuration object.
            var config = new BarcodeScannerScreenConfiguration();

            // Create single scanning mode.
            var useCase = new SingleScanningMode();

            // Enable and configure the confirmation sheet.
            useCase.ConfirmationSheetEnabled = true;

            // Turn on the barcode AR overlay
            useCase.ArOverlay.Visible = true;

            // Configure other parameters, pertaining to single-scanning mode as needed.
            config.UseCase = useCase;

            // Set an array of accepted barcode types.
            config.ScannerConfiguration.BarcodeFormats = BarcodeTypes.Instance.AcceptedTypes;

            // Set an array of accepted barcode types.
            config.ScannerConfiguration.Gs1Handling = Gs1Handling.DecodeStructure;

            // Launch the barcode scanner.
            var result = await ScanbotSDKMain.RTU.BarcodeScanner.LaunchAsync(configuration: config);

            await CommonUtils.DisplayResults(result);
        }
        catch (TaskCanceledException)
        {
            // for when the user cancels the action
        }
        catch (Exception ex)
        {
            // for any other errors that occur
            Console.WriteLine(ex.Message);
        }
    }
}