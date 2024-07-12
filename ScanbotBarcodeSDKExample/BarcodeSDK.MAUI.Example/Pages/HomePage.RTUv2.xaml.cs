using ScanbotSDK.MAUI.Example.Models;
using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Common;

namespace ScanbotSDK.MAUI.Example.Pages
{
    /// <summary>
    /// Home Page of the Application
    /// </summary>
    public partial class HomePage
    {
        /// <summary>
        /// Starts the Barcode scanning.
        /// </summary>
        private async Task StartSingleScanning()
        { 
            try
            {
                // Create the default configuration object.
                var configuration = new BarcodeScannerConfiguration();
        
                // Initialize the single-scan use case.
                var singleUsecase = new SingleScanningMode();
        
                // Enable and configure the confirmation sheet.
                singleUsecase.ConfirmationSheetEnabled = true;
                singleUsecase.SheetColor = Color.FromArgb("#FFFFFF");
        
                // Hide/unhide the barcode image of the confirmation sheet.
                singleUsecase.BarcodeImageVisible = true;
        
                // Configure the barcode title of the confirmation sheet.
                singleUsecase.BarcodeTitle.Visible = true;
                singleUsecase.BarcodeTitle.Color = Color.FromArgb("#000000");
        
                // Configure the barcode subtitle of the confirmation sheet.
                singleUsecase.BarcodeSubtitle.Visible = true;
                singleUsecase.BarcodeSubtitle.Color = Color.FromArgb("#000000");
        
                // Configure the cancel button of the confirmation sheet.
                singleUsecase.CancelButton.Text = "Close";
                singleUsecase.CancelButton.Foreground.Color = Color.FromArgb("#C8193C");
                singleUsecase.CancelButton.Background.FillColor = Color.FromArgb("#00000000");
        
                // Configure the submit button of the confirmation sheet.
                singleUsecase.SubmitButton.Text = "Submit";
                singleUsecase.SubmitButton.Foreground.Color = Color.FromArgb("#FFFFFF");
                singleUsecase.SubmitButton.Background.FillColor = Color.FromArgb( "#C8193C");
        
                // Set the configured use case.
                configuration.UseCase = singleUsecase;
        
                // Create and set an array of accepted barcode formats.
                configuration.RecognizerConfiguration.BarcodeFormats = BarcodeFormats.Common;
        
                var result = await ScanbotBarcodeSDK.BarcodeScanner.OpenBarcodeScannerAsync(configuration); 

                var barcodeAsText = result.Items.Select(barcode => $"{barcode.Type}: {barcode.Text}")
                                                 .FirstOrDefault() ?? string.Empty;

                await DisplayAlert("Found barcode", barcodeAsText, "Finish");
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

        private async Task StartMultiScanning()
        {
            try
            {
                // Create the default configuration object.
                var configuration = new BarcodeScannerConfiguration();

                // Initialize the multi-scan use case.
                var multiUsecase = new MultipleScanningMode();

                // Set the counting repeat delay.
                multiUsecase.CountingRepeatDelay = 1000;
                
                // Set the counting mode.
                multiUsecase.Mode = MultipleBarcodesScanningMode.Counting;
                
                // Set the sheet mode of the barcodes preview.
                multiUsecase.Sheet.Mode = SheetMode.CollapsedSheet;
                
                // Set the height of the collapsed sheet.
                multiUsecase.Sheet.CollapsedVisibleHeight = CollapsedVisibleHeight.Large;
                
                // Enable manual count change.
                multiUsecase.SheetContent.ManualCountChangeEnabled = true;
                
                // Configure the submit button.
                multiUsecase.SheetContent.SubmitButton.Text = "Submit";
                multiUsecase.SheetContent.SubmitButton.Foreground.Color = Color.FromArgb("#000000");

                configuration.UseCase = multiUsecase;
        
                // Create and set an array of accepted barcode formats.
                configuration.RecognizerConfiguration.BarcodeFormats = BarcodeFormats.Common;

                var result = await ScanbotBarcodeSDK.BarcodeScanner.OpenBarcodeScannerAsync(configuration); 

                var barcodesAsText = result.Items.Select(barcode => $"{barcode.Type}: {barcode.Text}").ToArray();
                await DisplayActionSheet("Found barcodes", "Finish", null, barcodesAsText);
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

        private async Task StartAROverlay()
        { 
            try
            {
                // Create the default configuration ject.
                var configuration = new BarcodeScannerConfiguration();
        
                // Configure the usecase.
                var usecase = new MultipleScanningMode();
                usecase.Mode = MultipleBarcodesScanningMode.Unique;
                usecase.Sheet.Mode = SheetMode.CollapsedSheet;
                usecase.Sheet.CollapsedVisibleHeight = CollapsedVisibleHeight.Small;
        
                // Configure AR Overlay.
                usecase.ArOverlay.Visible = true;
                usecase.ArOverlay.AutomaticSelectionEnabled = false;
        
                // Set the configured usecase.
                configuration.UseCase = usecase;
        
                // Create and set an array of accepted barcode formats.
                configuration.RecognizerConfiguration.BarcodeFormats = BarcodeFormats.Common;

                var result = await ScanbotBarcodeSDK.BarcodeScanner.OpenBarcodeScannerAsync(configuration); 

                var barcodeAsText = result.Items.Select(barcode => $"{barcode.Type}: {barcode.Text}")
                                                 .FirstOrDefault() ?? string.Empty;

                await DisplayAlert("Found barcode", barcodeAsText, "Finish");
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
}