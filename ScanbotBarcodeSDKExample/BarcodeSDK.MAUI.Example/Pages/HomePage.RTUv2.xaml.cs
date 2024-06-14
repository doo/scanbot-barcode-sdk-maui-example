using ScanbotSDK.MAUI.Constants;
using ScanbotSDK.MAUI.Configurations;
using ScanbotSDK.MAUI.Example.Models;
using System.Diagnostics;

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
        private async Task StartBarcodeScanner()
        { 
            try
            {
                var result = await ScanbotBarcodeSDK.BarcodeScanner.OpenBarcodeScannerAsync(new BarcodeScannerConfiguration
                {
                    RecognizerConfiguration = new BarcodeRecognizerConfiguration
                    {
                        BarcodeFormats = BarcodeTypes.Instance.AcceptedTypes,
                        Gs1Handling = Gs1Handling.Decode
                    },
                    UseCase = new SingleScanningMode()
                    {
                        ConfirmationSheetEnabled = true
                    }
                });

                await Navigation.PushAsync(new BarcodeResultPage(result.Items));
            }
            catch (TaskCanceledException ex)
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
        private async Task StartBatchBarcodeScanner()
        {           
            try
            {
                var result = await ScanbotBarcodeSDK.BarcodeScanner.OpenBarcodeScannerAsync(new BarcodeScannerConfiguration
                {

                });
                await Navigation.PushAsync(new BarcodeResultPage(result.Items));
            }
            catch (TaskCanceledException ex)
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