using Microsoft.Maui.Graphics.Platform;
using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Barcode.Core;
using ScanbotSDK.MAUI.Example.Utils;
using BarcodeScannerConfiguration = ScanbotSDK.MAUI.Barcode.Core.BarcodeScannerConfiguration;

namespace ScanbotSDK.MAUI.Example.Pages
{
    public struct HomePageMenuItem(string title, Func<Task> action)
    {
        public string Title { get; private set; } = title;

        public Func<Task> NavigationAction { get; private set; } = action;
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
        /// Init the List View.
        /// </summary>
        private void InitMenuItems()
        {
            MenuItems = [
                new HomePageMenuItem("RTU - Single Scanning", SingleScanning),
                new HomePageMenuItem("RTU - Single Scanning Selection Overlay", SingleScanningWithArOverlay),
                new HomePageMenuItem("RTU - Batch Barcode Scanning", BatchBarcodeScanning),
                new HomePageMenuItem("RTU - Multiple Unique Barcode Scanning", MultipleUniqueBarcodeScanning),
                new HomePageMenuItem("RTU - Find and Pick Barcode Scanning", FindAndPickScanning),
                new HomePageMenuItem("Classic Component - Barcode Scanning", () => Navigation.PushAsync(new BarcodeClassicComponentPage())),
                new HomePageMenuItem("Classic Component - Selection Overlay", () => Navigation.PushAsync(new BarcodeArOverlayClassicComponentPage())),
                new HomePageMenuItem("Classic Component - Scan and Count", () => Navigation.PushAsync(new BarcodeScanAndCountClassicComponentPage())),
                new HomePageMenuItem("Detect Barcodes on Image", DetectBarcodesOnImage),
                new HomePageMenuItem("Set Accepted Barcode Types", () => Navigation.PushAsync(new BarcodeSelectionPage())),
                new HomePageMenuItem("View License Info", () => Task.FromResult(ViewLicenseInfo))
            ];
        }

        /// <summary>
        /// ListView Item selected Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemSelected(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
        {
            if (!ScanbotSDKMain.LicenseInfo.IsValid)
            {
                CommonUtils.Alert(this, "Alert", "The license is not valid.");
                CollectionViewMenuItems.SelectedItem = null;
                return;
            }

            if (e?.CurrentSelection?.FirstOrDefault() is HomePageMenuItem selectedItem)
            {
                selectedItem.NavigationAction();
            }
            CollectionViewMenuItems.SelectedItem = null;
        }

        /// <summary>
        /// Detects barcodes on an image selected by the user.
        /// </summary>
        private async Task DetectBarcodesOnImage()
        {
            PlatformImage image;
            try
            {
                // Obtain an image from somewhere.
                // In this case, the user picks an image with our helper.
                image = await ScanbotSDKMain.ImagePicker.PickImageAsync(new ImagePickerConfiguration { Title = "Gallery" });
                if (image == null)
                {
                    return;
                }
            }
            // Handle cancel button click for ImagePicker.
            catch (TaskCanceledException e)
            {
                Console.WriteLine(e);
                return;
            }

            var configs = new BarcodeFormatCommonConfiguration
            {
                Formats = Models.BarcodeTypes.Instance.AcceptedTypes
            };
            
            // Configure the barcode detector for detecting many barcodes in one image.
            var configuration = new BarcodeScannerConfiguration
            {
                BarcodeFormatConfigurations = [configs],
                EngineMode = BarcodeScannerEngineMode.NextGen,
            };

            var result = await ScanbotSDKMain.Detectors.Barcode.DetectBarcodesAsync(image, configuration);
            var source = ImageSource.FromStream(() => image.AsStream(quality: 0.7f));
            
            // Handle the result in your app as needed.
            await Navigation.PushAsync(new BarcodeResultPage(result.Barcodes.ToList(), source));
        }

        /// <summary>
        /// View Current License Information
        /// </summary>
        private LicenseInfo ViewLicenseInfo()
        {
            var info = ScanbotSDKMain.LicenseInfo;
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