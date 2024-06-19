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
        private async Task SingleScanning()
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

        private async Task SingleScanningWithArOverlay()
        { 
            try
            {
                var result = await ScanbotBarcodeSDK.BarcodeScanner.OpenBarcodeScannerAsync(new BarcodeScannerConfiguration
                {
                    UseCase = new SingleScanningMode()
                    {
                        ArOverlay = new ArOverlayGeneralConfiguration() 
                        {
                            Visible = true
                        }    
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
        private async Task BatchBarcodeScanning()
        {           
            try
            {
                var result = await ScanbotBarcodeSDK.BarcodeScanner.OpenBarcodeScannerAsync(new BarcodeScannerConfiguration
                {
                    RecognizerConfiguration = new BarcodeRecognizerConfiguration
                    {
                        BarcodeFormats = BarcodeTypes.Instance.AcceptedTypes,           
                    },
                    UseCase = new MultipleScanningMode
                    {
                        Mode = MultipleBarcodesScanningMode.Counting
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

        private async Task MultipleUniqueBarcodeScanning()
        {
            try
            {
                var result = await ScanbotBarcodeSDK.BarcodeScanner.OpenBarcodeScannerAsync(new BarcodeScannerConfiguration
                {
                    UseCase = new MultipleScanningMode
                    {
                        Mode = MultipleBarcodesScanningMode.Unique,
                        SheetContent = new SheetContent
                        {
                            ManualCountChangeEnabled = false
                        },
                        Sheet = new Sheet
                        {
                            Mode = SheetMode.CollapsedSheet
                        },
                        ArOverlay = new ArOverlayGeneralConfiguration
                        {
                            Visible = true, 
                            AutomaticSelectionEnabled = false
                        }
                    },
                    UserGuidance = new UserGuidanceConfiguration
                    {
                        Title = new StyledText{ Text = "Please align the QR-/Barcode in the frame above to scan it." }
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

        private async Task FindAndPickScanning()
        {
            try
            {
                var configuration = new BarcodeScannerConfiguration();

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
                findAndPickConfig.ExpectedBarcodes = new ExpectedBarcode[]
                {
                    new ExpectedBarcode(barcodeValue: "123456", title: "numeric barcode", image: "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png", count: 4),
                    new ExpectedBarcode(barcodeValue: "SCANBOT", title: "value barcode", image: "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png", count: 4),
                };

                // Configure other parameters, pertaining to findAndPick-scanning mode as needed.
                configuration.UseCase = findAndPickConfig;
                configuration.RecognizerConfiguration.BarcodeFormats = BarcodeTypes.Instance.AcceptedTypes.ToArray();

                var result = await ScanbotBarcodeSDK.BarcodeScanner.OpenBarcodeScannerAsync(configuration);
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