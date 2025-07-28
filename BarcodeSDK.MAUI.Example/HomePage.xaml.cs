using Microsoft.Maui.Graphics.Platform;
using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Barcode.Core;
using ScanbotSDK.MAUI.Example.ClassicUI.MVVM.Views;
using ScanbotSDK.MAUI.Example.ClassicUI.Pages;
using ScanbotSDK.MAUI.Example.ReadyToUseUI;
using ScanbotSDK.MAUI.Example.Results;
using ScanbotSDK.MAUI.Example.Utils;
using BarcodeScannerConfiguration = ScanbotSDK.MAUI.Barcode.Core.BarcodeScannerConfiguration;

namespace ScanbotSDK.MAUI.Example
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
        private const string ViewLicenseInfoItem = "View License Info";
        private const string LicenseInvalidMessage = "The license is invalid or expired.";
        
        /// <summary>
        /// MenuItems List to bind the CollectionView UI. 
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
        /// Init the MenuItems list.
        /// </summary>
        private void InitMenuItems()
        {
            MenuItems = [
                new HomePageMenuItem("RTU - Single Scanning", SingleScanningFeature.StartSingleScanningAsync),
                new HomePageMenuItem("RTU - Single Scanning Selection Overlay", SingleScanningWithArOverlayFeature.StartSingleScanningWithArOverlayAsync),
                new HomePageMenuItem("RTU - Batch Barcode Scanning", BatchBarcodeScanningFeature.StartBatchBarcodeScanningAsync),
                new HomePageMenuItem("RTU - Multiple Unique Barcode Scanning", MultipleUniqueBarcodeScanningFeature.StartMultipleUniqueBarcodeScanningAsync),
                new HomePageMenuItem("RTU - Find and Pick Barcode Scanning", FindAndPickScanningFeature.StartFindAndPickScanningAsync),
                new HomePageMenuItem("Classic Component - Barcode Scanning", () => Navigation.PushAsync(new BarcodeClassicComponentPage())),
                new HomePageMenuItem("Classic Component - Barcode Scanning (MVVM)", () => Navigation.PushAsync(new BarcodeClassicComponentView())),
                new HomePageMenuItem("Classic Component - Selection Overlay", () => Navigation.PushAsync(new BarcodeArOverlayClassicComponentPage())),
                new HomePageMenuItem("Classic Component - Scan and Count", () => Navigation.PushAsync(new BarcodeScanAndCountClassicComponentPage())),
                new HomePageMenuItem("Detect Barcodes on Image", DetectBarcodesOnImage),
                new HomePageMenuItem("Set Accepted Barcode Types", () => Navigation.PushAsync(new BarcodeTypesSelectionPage())),
                new HomePageMenuItem(ViewLicenseInfoItem, () => Task.Run(ViewLicenseInfo))
            ];
        }

        /// <summary>
        /// CollectionView SelectionChanged event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e?.CurrentSelection?.FirstOrDefault() is not HomePageMenuItem selectedItem)
                return;
            
            if (ScanbotSDKMain.LicenseInfo.IsValid || selectedItem.Title == ViewLicenseInfoItem)
            {
                selectedItem.NavigationAction();
                CollectionViewMenuItems.SelectedItem = null;
                return;
            }

            CommonUtils.Alert(this, "Alert", LicenseInvalidMessage);
            CollectionViewMenuItems.SelectedItem = null;
        }

        /// <summary>
        /// Detects barcodes on an image selected by the user.
        /// </summary>
        private async Task DetectBarcodesOnImage()
        {
            var image = await PickImageAsync();
            if (image == null)
                return;

            var configs = new BarcodeFormatCommonConfiguration
            {
                Formats = BarcodeTypes.Instance.AcceptedTypes
            };
            
            // Configure the barcode detector for detecting many barcodes in one image.
            var configuration = new BarcodeScannerConfiguration
            {
                BarcodeFormatConfigurations = [configs],
                EngineMode = BarcodeScannerEngineMode.NextGen
            };

            var result = await ScanbotSDKMain.Detectors.Barcode.DetectBarcodesAsync(image, configuration);

            if (result.Success)
            {
                // Handle the result in your app as needed.
                await Navigation.PushAsync(new BarcodeResultPage(result.Barcodes.ToList()));
            }
            else
            {
                CommonUtils.Alert(this, "Warning", "No barcodes found.");
            }
        }

        /// <summary>
        /// Picks image from the photos application.
        /// </summary>
        /// <returns></returns>
        private async Task<PlatformImage> PickImageAsync()
        {
            try
            {
                // Pick the photo
                FileResult photo = await MediaPicker.Default.PickPhotoAsync();

                if (photo != null)
                {
                    // Optionally display or process the image
                    using var stream = await photo.OpenReadAsync();

                    // It returns a common interface IIMage which is implemented in PlatformImage.
                    return (PlatformImage)PlatformImage.FromStream(stream, ImageFormat.Jpeg);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Unable to pick image: {ex.Message}", "OK");
            }

            return null;
        }

        /// <summary>
        /// View Current License Information
        /// </summary>
        private void ViewLicenseInfo()
        {
            var info = ScanbotSDKMain.LicenseInfo;
            var message = $"License status: {info.Status}\n";
            if (info.IsValid) 
            {
                message += $"It is valid until {info.ExpirationDate?.ToLocalTime()}.";
            }
            else
            {
                message = LicenseInvalidMessage;
            }

            MainThread.InvokeOnMainThreadAsync(() =>
            {
                CommonUtils.Alert(this, "Info", message);
            });
        }
    }
}