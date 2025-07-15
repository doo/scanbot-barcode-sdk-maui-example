using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Example.Models;
using ScanbotSDK.MAUI.Example.Utils;

namespace ScanbotSDK.MAUI.Example.ReadyToUseUI;

public static class BatchBarcodeScanningFeature
{
    /// <summary>
    /// Starts the Batch Barcode Scanning.
    /// </summary>
    public static async Task StartBatchBarcodeScanningAsync()
    {
        try
        {
            // Create the default configuration object.
            var config = new BarcodeScannerScreenConfiguration();

            // Create multiple scanning mode
            var useCase = new MultipleScanningMode();

            // Set the counting mode.
            useCase.Mode = MultipleBarcodesScanningMode.Counting;

            // Set the sheet mode for the barcodes preview.
            useCase.Sheet.Mode = SheetMode.CollapsedSheet;

            // Configure other parameters, pertaining to single-scanning mode as needed.
            config.UseCase = useCase;

            // Set an array of accepted barcode types.
            config.ScannerConfiguration.BarcodeFormats = BarcodeTypes.Instance.AcceptedTypes;

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