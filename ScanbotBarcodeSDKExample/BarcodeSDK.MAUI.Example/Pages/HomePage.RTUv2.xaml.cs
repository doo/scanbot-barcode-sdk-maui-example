using ScanbotSDK.MAUI.Constants;
using ScanbotSDK.MAUI.Configurations;
using ScanbotSDK.MAUI.Example.Utils;
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
                Console.WriteLine("example: StartBarcodeScanning");
                BarcodeScannerConfiguration configuration = new BarcodeScannerConfiguration();
           
                //     configuration.RecognizerConfiguration.BarcodeTypes =
                //         new[] { SBSDKBarcodeType.AustraliaPost, SBSDKBarcodeType.Aztec };
            
                var usecases = new SingleScanningMode();
                usecases.ConfirmationSheetEnabled = true;
                usecases.ArOverlay.Visible = false;
                usecases.ArOverlay.AutomaticSelectionEnabled = false;

                configuration.UseCase = usecases;

                Console.WriteLine("example: starting barcode scanner");
                //var result = await ScanbotBarcodeSDK.BarcodeScanner.OpenBarcodeScannerView(configuration);
                Console.WriteLine("example: finished barcode scanner");

                Console.WriteLine("example: navigating to results page");
                    //await Navigation.PushAsync(new BarcodeResultPage(result.Items.ToList(), null));
                Console.WriteLine("example: navigated to results page");
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
            BarcodeScannerConfiguration configuration = new BarcodeScannerConfiguration();
           
            //     configuration.RecognizerConfiguration.BarcodeTypes =
            //         new[] { SBSDKBarcodeType.AustraliaPost, SBSDKBarcodeType.Aztec };
            
            var usecases = new MultipleScanningMode();
            configuration.UseCase = usecases;

            try
            {
                Console.WriteLine("example: starting barcode scanner");
                var result = await ScanbotBarcodeSDK.BarcodeScanner.OpenBarcodeScannerAsync(configuration);
                Console.WriteLine("example: finished barcode scanner");

                Console.WriteLine("example: navigating to results page");
                //await Navigation.PushAsync(new BarcodeResultPage(result.Items.ToList(), null));
                Console.WriteLine("example: navigated to results page");
            }
            catch (TaskCanceledException ex)
            {
                // for when the user cancels the action
                Console.WriteLine("example: cancelled barcode scanning");
            }
            catch (Exception ex)
            {
                // for any other errors that occur
                Console.WriteLine(ex.Message);
            }
        }
    }
}