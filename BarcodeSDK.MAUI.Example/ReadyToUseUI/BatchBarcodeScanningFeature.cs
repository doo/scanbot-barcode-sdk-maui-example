using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Core.Barcode;
using ScanbotSDK.MAUI.Example.Utils;

namespace ScanbotSDK.MAUI.Example.ReadyToUseUI;

public static class BatchBarcodeScanningFeature
{
    /// <summary>
    /// Starts the Batch Barcode Scanning.
    /// </summary>
    public static async Task StartBatchBarcodeScanningAsync()
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
        
        // create barcode format configurations
        var barcodeFormatConfiguration = new BarcodeFormatCommonConfiguration
        {
            // Set an array of accepted barcode types.
            Formats = BarcodeTypes.Instance.AcceptedTypes
        };

        // Set an array of barcode format configurations
        config.ScannerConfiguration.BarcodeFormatConfigurations = [barcodeFormatConfiguration];
        
        // Enable return of barcode image.
        config.ScannerConfiguration.ReturnBarcodeImage = true;

        // Launch the barcode scanner.
        var rtuResult = await ScanbotSdkMain.BarcodeScanner.LaunchAsync(configuration: config);
        if (rtuResult.Status != OperationResult.Ok)
            return;

        await CommonUtils.DisplayResults(rtuResult.Result);
    }
}