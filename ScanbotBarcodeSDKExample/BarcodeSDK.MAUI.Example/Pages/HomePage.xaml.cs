using BarcodeSDK.MAUI.Constants;
using BarcodeSDK.MAUI.Services;
using BarcodeSDK.MAUI.Configurations;
using BarcodeSDK.MAUI.Example.Utils;

namespace BarcodeSDK.MAUI.Example.Pages
{
    public struct HomePageMenuItem
    {
        public HomePageMenuItem(string title, Func<Task> action)
        {
            Title = title;
            NavigationAction = action;
        }

        public string Title { get; private set; }

        public Func<Task> NavigationAction { get; private set; }
    }

    /// <summary>
    /// Home Page of the Application
    /// </summary>
    public partial class HomePage : ContentPage
    {
        public bool IsLicenseValid => ScanbotBarcodeSDK.SDKService?.GetLicenseInfo()?.IsValid ?? false;
        /// <summary>
        /// List binding to UI ListView
        /// </summary>
        public List<HomePageMenuItem> MenuItems { get; set; }

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
            MenuItems = new List<HomePageMenuItem>
            {
                new HomePageMenuItem("SCAN BARCODES", () => StartBarcodeScanning(withImage: false)),
                new HomePageMenuItem("SCAN BARCODES WITH IMAGE", () => StartBarcodeScanning(withImage: true)),
                new HomePageMenuItem("SCAN BARCODE WITH CLASSIC COMPONENT", () => Navigation.PushAsync(new BarcodeClassicComponentPage())),
                new HomePageMenuItem("SCAN BATCH BARCODES", StartBatchBarcodeScanner),
                new HomePageMenuItem("DETECT BARCODES ON IMAGE", DetectBarcodesOnImage),
                new HomePageMenuItem("SET ACCEPTED BARCODE TYPES", () => Navigation.PushAsync(new BarcodeSelectionPage())),
                new HomePageMenuItem("VIEW LICENSE INFO", () => Task.FromResult(ViewLicenseInfo()))
            };
        }

        /// <summary>
        /// ListView Item selected Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MenuItemSelected(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
        {
            if (!IsLicenseValid)
            {
                return;
            }

            if (e?.CurrentSelection?.FirstOrDefault() is HomePageMenuItem selectedItem)
            {
                await selectedItem.NavigationAction();
            }
            CollectionView_MenuItems.SelectedItem = null;
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
                SuccessBeepEnabled = true
            };
            

            if (withImage)
            {
                configuration.BarcodeImageGenerationType = BarcodeImageGenerationType.FromVideoFrame;
            }

            configuration.OverlayConfiguration = new SelectionOverlayConfiguration(
                        automaticSelectionEnabled: false,
                        overlayFormat: BarcodeTextFormat.Code,
                        polygon: Colors.Yellow,
                        text: Colors.Yellow,
                        textContainer: Colors.Black);

            // To see the confirmation dialog in action, uncomment the below and comment out the configuration.OverlayConfiguration line above.
            //configuration.ConfirmationDialogConfiguration = new BarcodeConfirmationDialogConfiguration
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
            if (!IsLicenseValid)
            {
                return;
            }

            var result = ScanbotBarcodeSDK.SDKService.ClearStorageDirectory();

            if (result.Status == OperationResult.Ok)
            {
                CommonUtils.Alert(this, "Success!", "Cleared image storage");
            }
            else
            {
                CommonUtils.Alert(this, "Oops!", result.Error);
            }
        }

        /// <summary>
        /// View Current License Information
        /// </summary>
        private MAUI.Models.LicenseInfo ViewLicenseInfo()
        {
            var info = ScanbotBarcodeSDK.LicenseInfo;
            var message = $"License status {info.Status}";

            if (info.IsValid)
            {
                message += $" until {info.ExpirationDate?.ToLocalTime()}";
            }

            CommonUtils.Alert(this, "Info", message);
            return info;
        }
    }
}