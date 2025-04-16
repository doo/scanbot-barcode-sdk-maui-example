using ScanbotSDK.MAUI.Example.Models;
using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Common;

namespace ScanbotSDK.MAUI.Example.Pages;

/// <summary>
/// Home Page of the Application
/// </summary>
public partial class HomePage
{
    /// <summary>
    /// Starts the Barcode scanning.
    /// </summary>
    private async Task SingleScanning()
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
            config.ScannerConfiguration.BarcodeFormats = BarcodeFormats.Common;
            
            // Set an array of accepted barcode types.
            config.ScannerConfiguration.Gs1Handling = Gs1Handling.DecodeStructure;
            
            // Launch the barcode scanner.
            var result = await ScanbotSDKMain.RTU.BarcodeScanner.LaunchAsync(configuration: config);

            // Comment out the above and use the below to try some of our snippets instead:
            // var result = await ScanbotSDKMain.RTU.BarcodeScanner.LaunchAsync(Snippets.SingleScanningUseCase);
            // Or Snippets.MultipleScanningUseCase, Snippets.FindAndPickUseCase, Snippets.ActionBar, etc.

            await DisplayResults(result);
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

    private async Task SingleScanningWithArOverlay()
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
            config.ScannerConfiguration.BarcodeFormats = BarcodeFormats.Common;
            
            // Set an array of accepted barcode types.
            config.ScannerConfiguration.Gs1Handling = Gs1Handling.DecodeStructure;
            
            // Launch the barcode scanner.
            var result = await ScanbotSDKMain.RTU.BarcodeScanner.LaunchAsync(configuration: config);

            await DisplayResults(result);
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

    /// <summary>
    /// Starts the Batch Barcode Scanning.
    /// </summary>
    private async Task BatchBarcodeScanning()
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
            config.ScannerConfiguration.BarcodeFormats = BarcodeFormats.All;
            
            // Launch the barcode scanner.
            var result = await ScanbotSDKMain.RTU.BarcodeScanner.LaunchAsync(configuration: config);

            await DisplayResults(result);
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

    private async Task MultipleUniqueBarcodeScanning()
    {
        try
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
            config.ScannerConfiguration.BarcodeFormats = BarcodeFormats.All;

            // Set the user guidance hint
            config.UserGuidance.Title = new StyledText { Text = "Please align the QR-/Barcode in the frame above to scan it." };
            
            // Launch the barcode scanner.
            var result = await ScanbotSDKMain.RTU.BarcodeScanner.LaunchAsync(configuration: config);

            await DisplayResults(result);
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

    private async Task FindAndPickScanning()
    {
        try
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
            configuration.ScannerConfiguration.BarcodeFormats = BarcodeTypes.Instance.AcceptedTypes.ToArray();

            var result = await ScanbotSDKMain.RTU.BarcodeScanner.LaunchAsync(configuration);
            await DisplayResults(result);
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

    private async Task DisplayResults(BarcodeScannerUiResult result)
    {
        if (result?.Items?.Length > 0)
        {
            var items = result.Items.Select(item => item.Barcode);
            await Navigation.PushAsync(new BarcodeResultPage(items.ToList()));
        }
    }
}