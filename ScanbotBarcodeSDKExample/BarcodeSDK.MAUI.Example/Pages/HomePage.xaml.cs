using BarcodeSDK.MAUI.Constants;
using BarcodeSDK.MAUI.Services;
using BarcodeSDK.MAUI.Example.Common.Utils;
using BarcodeSDK.MAUI.Configurations;

namespace BarcodeSDK.MAUI.Example.Pages
{
    /// <summary>
    /// Home Page of the Application
    /// </summary>
    public partial class HomePage : ContentPage
    {
        /// <summary>
        /// List binding to UI ListView
        /// </summary>
        public List<string> MenuItems { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public HomePage()
        {
            InitializeComponent();
            InitMenuItems();
            BindingContext = this;
        }

        /// <summary>
        /// Init the Liist View.
        /// </summary>
        private void InitMenuItems()
        {
            MenuItems = new List<string>
            {
                "SCAN BARCODES",
                "SCAN BARCODES WITH IMAGE",
                "SCAN BARCODE WITH CLASSIC COMPONENT",
                "SCAN BATCH BARCODES",
                "DETECT BARCODES ON IMAGE",
                "SET ACCEPTED BARCODE TYPES",
                "VIEW LICENSE INFO"
            };
        }

        /// <summary>
        /// ListView Item selected Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void CollectionView_MenuItems_ItemSelected(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
        {
            var index = GetSelectedIndex(e);
            switch (index)
            {
                case 0: // Scan Barcode
                    await StartBarcodeScanning(withImage: false);
                    break;

                case 1: // Scan Barcode with Image
                    await StartBarcodeScanning(withImage: true);
                    break;

                case 2:
                    await Navigation.PushAsync(new BarcodeClassicComponentPage());
                    break;

                case 3:
                    await StartBatchBarcodeScanner();
                    break;

                case 4:
                    await DetectBarcodesOnImage();
                    break;

                case 5:
                    await Navigation.PushAsync(new BarcodeSelectionPage());
                    break;

                case 6:
                    ViewLicenseInfo();
                    break;

                default:
                    break;
            }
            CollectionView_MenuItems.SelectedItem = null;
        }

        // Get the selected item from the list
        private int GetSelectedIndex(SelectionChangedEventArgs e)
        {
            if (e?.CurrentSelection?.FirstOrDefault() is string selectedItem && selectedItem != null)
            {
                return MenuItems.IndexOf(selectedItem);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Starts the Barcode scanning.
        /// </summary>
        private async Task StartBarcodeScanning(bool withImage)
        {
            var configuration = new BarcodeScannerConfiguration
            {
                BarcodeFormats = Models.BarcodeTypes.Instance.AcceptedTypes,
                CodeDensity = BarcodeDensity.High,
                EngineMode = EngineMode.NextGen,
                
            };
            

            if (withImage)
            {
                configuration.BarcodeImageGenerationType = BarcodeImageGenerationType.FromVideoFrame;
            }

            configuration.OverlayConfiguration = new SelectionOverlayConfiguration(
                        automaticSelectionEnabled: true,
                        overlayFormat: BarcodeTextFormat.Code,
                        polygon: Colors.Yellow,
                        text: Colors.Yellow,
                        textContainer: Colors.Black);

            // To see the confirmation dialog in action, uncomment the below and comment out the config.OverlayConfiguration line above.
            //config.ConfirmationDialogConfiguration = new BarcodeConfirmationDialogConfiguration
            //{
            //    Title = "Barcode Detected!",
            //    Message = "A barcode was found.",
            //    ConfirmButtonTitle = "Continue",
            //    RetryButtonTitle = "Try again",
            //    TextFormat = BarcodeTextFormat.CodeAndType
            //};

            var result = await ScanbotBarcodeSDK.BarcodeService.OpenBarcodeScannerView(configuration);

            if (result.Status == OperationResult.Ok)
            {
                await Navigation.PushAsync(new BarcodeResultPage(result.Barcodes, withImage ? result.Image : result.ImagePath));
            }
        }

        /// <summary>
        /// Starts the Batch Barcode Scanning.
        /// </summary>
        private async Task StartBatchBarcodeScanner()
        {
            var configuration = new BatchBarcodeScannerConfiguration
            {
                BarcodeFormats = Models.BarcodeTypes.Instance.AcceptedTypes,
                OverlayConfiguration = new SelectionOverlayConfiguration(
                    automaticSelectionEnabled: true,
                    overlayFormat: BarcodeTextFormat.Code,
                    polygon: Colors.Yellow,
                    text: Colors.Yellow,
                    textContainer: Colors.Black,
                    highlightedPolygonColor: Colors.Red,
                    highlightedTextColor: Colors.Red,
                    highlightedTextContainerColor: Colors.Black),
                SuccessBeepEnabled = true,
                CodeDensity = BarcodeDensity.High,
                EngineMode = EngineMode.NextGen
            };
            
            var result = await ScanbotBarcodeSDK.BarcodeService?.OpenBatchBarcodeScannerView(configuration);
            if (result.Status == OperationResult.Ok)
            {
                await Navigation.PushAsync(new BarcodeResultPage(result.Barcodes, ""));
            }
        }

        /// <summary>
        /// Starts the Batch Barcode Scanning.
        /// </summary>
        private async Task DetectBarcodesOnImage()
        {
            var imageSource = await ScanbotBarcodeSDK.PickerService?.PickImageAsync(new ImagePickerConfiguration { Title = "Gallery" });
            var configuration = new BarcodeDetectionConfiguration
            {
                BarcodeFormats = Models.BarcodeTypes.Instance.AcceptedTypes,
                EngineMode = EngineMode.NextGen,
                AdditionalParameters = new BarcodeScannerAdditionalParameters
                {
                    CodeDensity = BarcodeDensity.High,
                    LowPowerMode = false,
                }
            };

            var barcodes = await ScanbotBarcodeSDK.DetectionService?.DetectBarcodesFrom(imageSource, configuration);

            if (barcodes?.Count > 0)
            {
                await Navigation.PushAsync(new BarcodeResultPage(barcodes, imageSource));
            }
        }

        /// <summary>
        /// Clear storage.
        /// </summary>
        private void ClearStorage()
        {
            if (!Utils.CheckLicense(this))
            {
                return;
            }

            var result = ScanbotBarcodeSDK.SDKService.ClearStorageDirectory();

            if (result.Status == OperationResult.Ok)
            {
                Utils.Alert(this, "Success!", "Cleared image storage");
            }
            else
            {
                Utils.Alert(this, "Oops!", result.Error);
            }
        }

        /// <summary>
        /// View Current License Information
        /// </summary>
        private void ViewLicenseInfo()
        {
            var info = ScanbotBarcodeSDK.LicenseInfo;
            var message = $"License status {info.Status}";

            if (info.IsValid)
            {
                message += $" until {info.ExpirationDate}";
            }

            Utils.Alert(this, "Info", message);
        }
    }
}