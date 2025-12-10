using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Core.Barcode;
using ScanbotSDK.MAUI.Example.Utils;

namespace ScanbotSDK.MAUI.Example.ReadyToUseUI;

public static class SingleScanningWithArOverlayFeature
{
    /// <summary>
    /// Starts the Barcode scanning With Ar Overlay.
    /// </summary>
    public static async Task StartSingleScanningWithArOverlayAsync()
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

        // Launch the barcode scanner.
        var rtuResult = await ScanbotSdkMain.BarcodeScanner.StartScannerAsync(configuration: config);
        if (rtuResult.Status != OperationResult.Ok)
            return;

        await CommonUtils.DisplayResults(rtuResult.Result);
    }
}