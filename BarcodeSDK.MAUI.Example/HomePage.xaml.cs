using ScanbotSDK.MAUI.Core.Barcode;
using ScanbotSDK.MAUI.Example.ClassicUI.MVVM.Views;
using ScanbotSDK.MAUI.Example.ClassicUI.Pages;
using ScanbotSDK.MAUI.Example.ReadyToUseUI;
using ScanbotSDK.MAUI.Example.Results;
using ScanbotSDK.MAUI.Example.Utils;
using ScanbotSDK.MAUI.Image;
using BarcodeScannerConfiguration = ScanbotSDK.MAUI.Core.Barcode.BarcodeScannerConfiguration;

namespace ScanbotSDK.MAUI.Example;
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
            MenuItems =
            [
                new HomePageMenuItem("RTU - Single Scanning", SingleScanningFeature.StartSingleScanningAsync),
                new HomePageMenuItem("RTU - Single Scanning Selection Overlay", SingleScanningWithArOverlayFeature.StartSingleScanningWithArOverlayAsync),
                new HomePageMenuItem("RTU - Batch Barcode Scanning", BatchBarcodeScanningFeature.StartBatchBarcodeScanningAsync),
                new HomePageMenuItem("RTU - Multiple Unique Barcode Scanning", MultipleUniqueBarcodeScanningFeature.StartMultipleUniqueBarcodeScanningAsync),
                new HomePageMenuItem("RTU - Find and Pick Barcode Scanning", FindAndPickScanningFeature.StartFindAndPickScanningAsync),
                new HomePageMenuItem("Classic Component - Barcode Scanning", () => Navigation.PushAsync(new BarcodeClassicComponentPage())),
                new HomePageMenuItem("Classic Component - Barcode Scanning (MVVM)", () => Navigation.PushAsync(new BarcodeClassicComponentView())),
                new HomePageMenuItem("Classic Component - Selection Overlay", () => Navigation.PushAsync(new BarcodeArOverlayClassicComponentPage())),
                new HomePageMenuItem("Classic Component - Scan and Count", () => Navigation.PushAsync(new BarcodeScanAndCountClassicComponentPage())),
                new HomePageMenuItem("Scan Barcodes From Image", ScanBarcodesFromImageAsync),
                new HomePageMenuItem("Scan Barcodes From PDF", DetectBarcodesFromPdfAsync), // todo: To remove
                new HomePageMenuItem("Detect BarcodeDocument on Image", DetectBarcodeDocumentFromImageAsync), // todo: To remove
                new HomePageMenuItem("Configure Mock Camera", ConfigureMockCameraAsync), // todo: To remove
                new HomePageMenuItem("Set Accepted Barcode Types", () => Navigation.PushAsync(new BarcodeTypesSelectionPage())),
                new HomePageMenuItem("Clean Storage", CleanStorage),
                new HomePageMenuItem(ViewLicenseInfoItem, () => Task.Run(ViewLicenseInfo))
            ];
        }

        /// <summary>
        /// CollectionView SelectionChanged event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MeuItemTapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter is not HomePageMenuItem selectedItem)
                return;

            if (ScanbotSDKMain.LicenseInfo.IsValid || selectedItem.Title == ViewLicenseInfoItem)
            {
                await selectedItem.NavigationAction();
                return;
            }

            await Alert.ShowAsync("Alert", LicenseInvalidMessage);
        }

        /// <summary>
        /// Detects barcodes on an image selected by the user.
        /// </summary>
        private async Task ScanBarcodesFromImageAsync()
        {
            var image = await ImagePicker.PickImageAsPathAsync();
            if (image == null)
                return;

            var imageRef = ImageRef.FromPath(image);

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

            var result = await ScanbotSDKMain.Barcode.ScanFromImageAsync(imageRef, configuration);
            if (!result.IsSuccess)
            {
                await Alert.ShowAsync("Warning", "No barcodes found.");
                return;
            }
            
            // Handle the result in your app as needed.
            await Navigation.PushAsync(new BarcodeResultPage(result.Value.Barcodes.ToList()));
        }

        private async Task DetectBarcodesFromPdfAsync()
        {
            var file = await FilePicker.Default.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Pdf,
                PickerTitle = "Select a pdf file",
            });

            if (file == null)
            {
                await Alert.ShowAsync("Alert", "Something went wrong while picking the file from the storage.");
                return;
            }

            var result = await ScanbotSDKMain.Barcode.ScanFromPdfAsync(file.FullPath, new BarcodeScannerConfiguration());
            if (result.IsSuccess)
            {
                await Navigation.PushAsync(new BarcodeResultPage(result.Value.Barcodes.ToList()));
            }
        }

        private async Task DetectBarcodeDocumentFromImageAsync()
        {
            var image = await ImagePicker.PickImageAsPathAsync();
            if (image == null)
                return;

            var imageRef = ImageRef.FromPath(image);
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

            var result = await ScanbotSDKMain.Barcode.ScanFromImageAsync(imageRef, configuration);

            if (!result.IsSuccess)
            {
                await Alert.ShowAsync("Warning", "No barcodes found. \n Error:" + result.Error?.Message);
                return;
            }

            if (result.Value.Barcodes.Length == 0)
            {
                await Alert.ShowAsync("Warning", "No barcodes found.");
                return;
            }

            // Accessing the first item only, for our example.
            var text = result.Value.Barcodes.First().Text;
            var genericDocumentResult = await ScanbotSDKMain.Barcode.ParseDocumentAsync(text, BarcodeDocumentFormats.All);
            if (genericDocumentResult.IsSuccess)
            {
                await Alert.ShowAsync("Document Result String", genericDocumentResult.Value.ParsedDocument.ToGdrString());
            }
        }

        private async Task ConfigureMockCameraAsync()
        {
            var imagePath = await ImagePicker.PickImageAsPathAsync();
            ScanbotSDKMain.MockCamera(imagePath);
        }

        private async Task CleanStorage()
        {
            var result = await ScanbotSDKMain.CleanupStorageAsync();
            if (result.IsSuccess)
            {
                await Alert.ShowAsync("Alert", "Storage cleared successfully.");
            }
            else
            {
                await Alert.ShowAsync("Alert", "Unable to cleanup storage.\n Error: " + result.Error?.Message);
            }
        }

        /// <summary>
        /// View Current License Information
        /// </summary>
        private async void ViewLicenseInfo()
        {
            var info = ScanbotSDKMain.LicenseInfo;
            var message = $"License status: {info.Status}\n";
            if (info.IsValid)
            {
                message += $"It is valid until {info.ExpirationDateString}.";
            }
            else
            {
                message = LicenseInvalidMessage;
            }

            await Alert.ShowAsync("Info", message);
        }
    }