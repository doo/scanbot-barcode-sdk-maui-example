using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Common;
using ScanbotSDK.MAUI.Core.Barcode;
using ScanbotSDK.MAUI.Example.Utils;

namespace ScanbotSDK.MAUI.Example.ReadyToUseUI;

public static class MultipleUniqueBarcodeScanningFeature
{
    public static async Task StartMultipleUniqueBarcodeScanningAsync()
    {
        // Create the default configuration object.
        var config = new BarcodeScannerScreenConfiguration();

        // Create multiple scanning mode
        var useCase = new MultipleScanningMode();

        // Turn on the barcode AR overlay
        useCase.ArOverlay.Visible = true;

        // Set the counting mode
        useCase.Mode = MultipleBarcodesScanningMode.Unique;

        // Set the sheet mode for the barcodes preview.
        useCase.Sheet.Mode = SheetMode.CollapsedSheet;

        // Configure other parameters, pertaining to single-scanning mode as needed.
        config.UseCase = useCase;

        // Set an array of accepted barcode types.
        // todo: Check the format configs
        config.ScannerConfiguration.BarcodeFormatConfigurations =
        [
            new BarcodeFormatCommonConfiguration
            {
                Formats = BarcodeTypes.Instance.AcceptedTypes
            }
        ];
         
        // Set the user guidance hint
        config.UserGuidance.Title = new StyledText { Text = "Please align the QR-/Barcode in the frame above to scan it." };

        // Launch the barcode scanner.
        var rtuResult = await ScanbotSdkMain.BarcodeScanner.StartScannerAsync(configuration: config);
        if (rtuResult.Status != OperationResult.Ok)
            return;

        await CommonUtils.DisplayResults(rtuResult.Result);
    }
}