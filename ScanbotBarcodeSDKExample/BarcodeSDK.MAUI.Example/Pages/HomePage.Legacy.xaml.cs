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
        private async Task StartLegacyBarcodeScanner(bool withImage)
        {
            var configuration = new RTU.v1.BarcodeScannerConfiguration
            {
                BarcodeFormats = Models.BarcodeTypes.Instance.AcceptedTypes.ToList(),
                CodeDensity = CodeDensity.High,
                EngineMode = EngineMode.NextGen,
                SuccessBeepEnabled = true,
                CameraZoomLevel = 0.5f,
                CameraZoomRange = new RTU.v1.ZoomRange(1.0f, 4.0f),

                //Specify this property so then it could detect barcodes from accepted documents (but it will handle only these types)
                //AcceptedDocumentFormats = Enum.GetValues<BarcodeDocumentFormat>().ToList()
            };

            if (withImage)
            {
                configuration.BarcodeImageGenerationType = RTU.v1.BarcodeImageGenerationType.FromVideoFrame;
            }

            configuration.OverlayConfiguration = new RTU.v1.SelectionOverlayConfiguration(
                        automaticSelectionEnabled: false,
                        overlayFormat: BarcodeTextFormat.Code,
                        strokeColor: Colors.Yellow,
                        textColor: Colors.Yellow,
                        textContainerColor: Colors.Black);

            // To see the confirmation dialog in action, uncomment the below and comment out the configuration.OverlayConfiguration line above.
            //configuration.ConfirmationDialogConfiguration = new BarcodeConfirmationDialogConfiguration
            //{
            //    Title = "Barcode Detected!",
            //    Message = "A barcode was found.",
            //    ConfirmButtonTitle = "Continue",
            //    RetryButtonTitle = "Try again",
            //    TextFormat = BarcodeTextFormat.CodeAndType
            //};

            try
            {
                var result = await ScanbotBarcodeSDK.LegacyBarcodeScanner.OpenBarcodeScannerView(configuration);
                
                if (result.Status == OperationResult.Ok)
                {
                    await Navigation.PushAsync(new BarcodeResultPage(result.Barcodes.ToList(), null));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        /// <summary>
        /// Starts the Batch Barcode Scanning.
        /// </summary>
        private async Task StartLegacyBatchBarcodeScanner()
        {
            var configuration = new RTU.v1.BatchBarcodeScannerConfiguration
            {
                BarcodeFormats = Models.BarcodeTypes.Instance.AcceptedTypes.ToList(),
                OverlayConfiguration = new RTU.v1.SelectionOverlayConfiguration(
                    automaticSelectionEnabled: true,
                    overlayFormat: BarcodeTextFormat.Code,
                    textColor: Colors.Yellow,
                    textContainerColor: Colors.Black,
                    strokeColor: Colors.Yellow,
                    highlightedStrokeColor: Colors.Red,
                    highlightedTextColor: Colors.Yellow,
                    highlightedTextContainerColor: Colors.DarkOrchid,
                    polygonBackgroundColor: Colors.Green,
                    polygonBackgroundHighlightedColor: Colors.Aquamarine),
                SuccessBeepEnabled = true,
                CodeDensity = CodeDensity.High,
                EngineMode = EngineMode.NextGen
            };

            var result = await ScanbotBarcodeSDK.LegacyBarcodeScanner.OpenBatchBarcodeScannerView(configuration);
            if (result.Status == OperationResult.Ok)
            {
                await Navigation.PushAsync(new BarcodeResultPage(result.Barcodes, ""));
            }
        }
    }
}