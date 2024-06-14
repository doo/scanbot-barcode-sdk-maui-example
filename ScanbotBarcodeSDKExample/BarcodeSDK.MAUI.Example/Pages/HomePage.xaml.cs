using ScanbotSDK.MAUI.Constants;
using ScanbotSDK.MAUI.Configurations;
using ScanbotSDK.MAUI.Example.Utils;
using System.Diagnostics;

namespace ScanbotSDK.MAUI.Example.Pages
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
        /// <summary>
        /// List binding to UI ListView1
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
            
            NavigationPage.SetHasNavigationBar(this, false);
        }

        /// <summary>
        /// Init the Liist View.
        /// </summary>
        private void InitMenuItems()
        {
            MenuItems = new List<HomePageMenuItem>
            {
                #if LEGACY_EXAMPLES
                    new HomePageMenuItem("SCAN LEGACY BARCODES", () => StartLegacyBarcodeScanner(withImage: false)),
                    new HomePageMenuItem("SCAN LEGACY BARCODES WITH IMAGE", () => StartLegacyBarcodeScanner(withImage: true)),
                    new HomePageMenuItem("SCAN LEGACY BATCH BARCODES", StartLegacyBatchBarcodeScanner),
                #else
                    new HomePageMenuItem("SCAN BARCODES", StartBarcodeScanner),
                    new HomePageMenuItem("SCAN BATCH BARCODES", StartBatchBarcodeScanner),
                #endif
                new HomePageMenuItem("SCAN BARCODE WITH CLASSIC COMPONENT", () => Navigation.PushAsync(new BarcodeClassicComponentPage())),
                new HomePageMenuItem("SCAN BARCODE AR OVERLAY WITH CLASSIC COMPONENT", () => Navigation.PushAsync(new BarcodeArOverlayClassicComponentPage())),
                new HomePageMenuItem("SCAN BARCODE WITH CLASSIC SCAN AND COUNT COMPONENT", () => Navigation.PushAsync(new BarcodeScanAndCountClassicComponentPage())),
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
            if (!ScanbotBarcodeSDK.LicenseInfo.IsValid)
            {
                CommonUtils.Alert(this, "Alert", "The license is expired.");
                CollectionView_MenuItems.SelectedItem = null;
                return;
            }

            if (e?.CurrentSelection?.FirstOrDefault() is HomePageMenuItem selectedItem)
            {
                await selectedItem.NavigationAction();
            }
            CollectionView_MenuItems.SelectedItem = null;
        }

        /// <summary>
        /// Detects barcodes on an image selected by the user.
        /// </summary>
        private async Task DetectBarcodesOnImage()
        {
            // Optain an image from somewhere.
            // In this case, the user picks an image with our helper.
            var imageSource = await ScanbotBarcodeSDK.PickerService.PickImageAsync(new ImagePickerConfiguration { Title = "Gallery" });

            if (imageSource == null)
            {
                return;
            }

            // Configure the barcode detector for detecting many barcodes in one image.
            var configuration = new RTU.v1.BarcodeDetectionConfiguration
            {
                BarcodeFormats = Models.BarcodeTypes.Instance.AcceptedTypes.ToList(),
                EngineMode = EngineMode.NextGen,
                AdditionalParameters = new RTU.v1.BarcodeScannerAdditionalParameters
                {
                    CodeDensity = CodeDensity.High
                }
            };

            var barcodes = await ScanbotBarcodeSDK.BarcodeDetector.DetectBarcodesFrom(imageSource, configuration);

            // Handle the result in your app as needed.
            await Navigation.PushAsync(new BarcodeResultPage(barcodes, imageSource));
        }

        /// <summary>
        /// Clear storage.
        /// </summary>
        private void ClearStorage()
        {
            if (!ScanbotBarcodeSDK.LicenseInfo.IsValid)
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
        private LicenseInfo ViewLicenseInfo()
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