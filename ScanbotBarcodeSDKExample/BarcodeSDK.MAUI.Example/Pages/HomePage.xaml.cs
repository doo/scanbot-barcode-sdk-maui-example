using BarcodeSDK.MAUI.Constants;
using BarcodeSDK.MAUI.Models;
using BarcodeSDK.MAUI.Services;
using BarcodeSDK.MAUI.Example.Common.Utils;
using BarcodeSDK.MAUI.Configurations;
using System;

namespace BarcodeSDK.MAUI.Example.Pages
{
    /// <summary>
    /// Home Page of the Application
    /// </summary>
    public partial class HomePage : ContentPage
    {
        private bool ShouldTestCloseView = true;

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
        void CollectionView_MenuItems_ItemSelected(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
        {
            var index = GetSelectedIndex(e);
            switch (index)
            {
                case 0: // Scan Barcode
                    _ = StartBarcodeScanning(false);
                    break;

                case 1: // Scan Barcode with Image
                    _ = StartBarcodeScanning(true);
                    break;

                case 2: // Scan Barcode with Image
                    StartBarcodeScanningWithClassicComponent();
                    break;

                case 3: // Scan Batch Barcode
                    _ = StartBatchBarcodeScanner();
                    break;

                case 4: // Detect Barcodes on Image
                    _ = DetectBarcodesOnImage();
                    break;

                case 5: // Set the Accepted barcode types
                    SetAcceptedBarcodeTypes();
                    break;

                case 6: // Scan Batch Barcode
                    ViewLicenseInfo();
                    break;

                default: // Nothing
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

        private void Navigate(ContentPage page)
        {
            MainThread.InvokeOnMainThreadAsync(async () => await Navigation.PushAsync(page));
        }

        /// <summary>
        /// Starts the Barcode scanning.
        /// </summary>
        private async Task StartBarcodeScanning(bool withImage)
        {
            var configuration = new BarcodeScannerConfiguration();
            configuration.BarcodeFormats = Models.BarcodeTypes.Instance.AcceptedTypes;
            configuration.OverlayConfiguration = new SelectionOverlayConfiguration(true, OverlayFormat.CodeAndType, Colors.Yellow, Colors.Yellow, Colors.Black,
                Colors.Red, Colors.Red, Colors.Black);
            configuration.CodeDensity = BarcodeDensity.High;
            configuration.EngineMode = EngineMode.NextGen;
            configuration.SuccessBeepEnabled = true;

            if (withImage)
            {
                configuration.BarcodeImageGenerationType = BarcodeImageGenerationType.FromVideoFrame;
            }

            var result = await ScanbotBarcodeSDK.BarcodeService?.OpenBarcodeScannerView(configuration);

            if (result.Status == OperationResult.Ok)
            {
                Navigate(new BarcodeResultPage(result.Barcodes, withImage ? result.Image : result.ImagePath));
            }
        }

        /// <summary>
        /// Navigate to the Classic Component.
        /// </summary>
        private void StartBarcodeScanningWithClassicComponent()
        {
            Navigate(new BarcodeClassicComponentPage());
        }

        /// <summary>
        /// Starts the Batch Barcode Scanning.
        /// </summary>
        private async Task StartBatchBarcodeScanner()
        {
            var configuration = new BatchBarcodeScannerConfiguration();
            configuration.BarcodeFormats = Models.BarcodeTypes.Instance.AcceptedTypes;
            configuration.OverlayConfiguration = new SelectionOverlayConfiguration(true, OverlayFormat.Code, Colors.Yellow, Colors.Yellow, Colors.Black,
                Colors.Red, Colors.Red, Colors.Black);
            configuration.SuccessBeepEnabled = true;
            configuration.CodeDensity = BarcodeDensity.High;
            configuration.EngineMode = EngineMode.NextGen;
            var result = await ScanbotBarcodeSDK.BarcodeService?.OpenBatchBarcodeScannerView(configuration);
            if (result.Status == OperationResult.Ok)
            {
                Navigate(new BarcodeResultPage(result.Barcodes, ""));
            }
        }

        /// <summary>
        /// Starts the Batch Barcode Scanning.
        /// </summary>
        private async Task DetectBarcodesOnImage()
        {
            var imageSource = await ScanbotBarcodeSDK.PickerService?.PickImageAsync(new ImagePickerConfiguration { Title = "Gallery" });
            var configuration = new BarcodeDetectionConfiguration();
            configuration.BarcodeFormats = Models.BarcodeTypes.Instance.AcceptedTypes;
            configuration.EngineMode = EngineMode.NextGen;
            configuration.AdditionalParameters = new BarcodeScannerAdditionalParameters
            {
                CodeDensity = BarcodeDensity.High,
                LowPowerMode = false,
            };

            var barcodes = await ScanbotBarcodeSDK.DetectionService?.DetectBarcodesFrom(imageSource, configuration);

            if (barcodes?.Count > 0)
            {
                Navigate(new BarcodeResultPage(barcodes, imageSource));
            }
        }

        /// <summary>
        /// Test the force closing of Barcode scanning view.
        /// </summary>
        /// <param name="isBatchBarcode"></param>
        /// <returns></returns>
        private void TestCloseView(bool isBatchBarcode)
        {
            if (!ShouldTestCloseView) return;
            Task.Run(async () =>
            {
                await Task.Delay(7000);
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    if (isBatchBarcode)
                    {
                        ScanbotBarcodeSDK.BarcodeService.CloseBatchBarcodeScannerView();
                    }
                    else
                    {
                        ScanbotBarcodeSDK.BarcodeService.CloseBatchBarcodeScannerView();
                    }
                });
            });
        }

        /// <summary>
        /// Set the Accepted barcode types.
        /// </summary>
        private void SetAcceptedBarcodeTypes()
        {
            if (!ShouldTestCloseView) return;
            Navigate(new BarcodeSelectionPage());
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
                message += $" until {info.ExpirationDate.ToString()}";
            }

            Utils.Alert(this, "Info", message);
        }
    }
}