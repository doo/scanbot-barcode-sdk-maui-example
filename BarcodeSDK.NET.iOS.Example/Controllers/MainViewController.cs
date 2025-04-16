using BarcodeSDK.NET.iOS.Controllers.ClassicComponents;
using BarcodeSDK.NET.iOS.Utils;
using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS
{
    public partial class MainViewController : BaseViewController
    {
        private MainView contentView;

        
        public override void ViewDidLoad()
        {
            PageTitle = "Barcode Scanner Example";
            base.ViewDidLoad();
            contentView = new MainView();
            
            contentView.CreateHeader("Classic Components");
            contentView.CreateItem("Barcode Component", OnClassicButtonClick);
            contentView.CreateItem("Barcode Scan and Count Component", OnClassicScanAndCountButtonClick);
        
            contentView.CreateHeader("Ready to Use UI");
            contentView.CreateItem("Single Scanning", SingleScanning);
            contentView.CreateItem("Single Scanning Selection Overlay", SingleScanningWithArOverlay);
            contentView.CreateItem("Batch Barcode Scanning", BatchBarcodeScanning);
            contentView.CreateItem("Multiple Unique Barcode Scanning", MultipleUniqueBarcodeScanning);
            contentView.CreateItem("Find and Pick Barcode Scanning", FindAndPickScanning);
            
            contentView.CreateHeader("SDK Operations");
            contentView.CreateItem("Detect Barcodes On Image", OnLibraryButtonClick);
            contentView.CreateItem("Set Accepted Barcode Types", OnAcceptedTypesButtonClick);
            contentView.CreateItem("View License Info", OnLicenseInfoButtonClick);
            
            View = contentView;
        }

        private async void OnClassicButtonClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this))
            {
                return;
            }

            if (await this.IsCameraPermissionGranted())
            {
                NavigationController?.PushViewController(new BarcodeClassicComponentController(), animated: true);
            }
        }

        private async void OnClassicScanAndCountButtonClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this))
            {
                return;
            }

            if (await this.IsCameraPermissionGranted())
            {
                var viewController = Utilities.GetViewController<BarcodeScanAndCountViewController>(Texts.ClassicComponentStoryboard);
                NavigationController?.PushViewController(viewController, true);
            }
        }

        private async void OnLibraryButtonClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this))
            {
                return;
            }

            // Obtain an image from somewhere.
            // In this case, the user picks an image with our helper.
            try
            {
                UIImage image = await PickImageAsync();

                if (image == null)
                {
                    return;
                }

                // Configure the barcode detector for detecting many barcodes in one image.
                var barcodeConfiguration = new SBSDKBarcodeFormatCommonConfiguration
                {
                    Formats = BarcodeTypes.Instance.AcceptedTypes
                };
                
                var scannerConfiguration = new SBSDKBarcodeScannerConfiguration
                {
                    BarcodeFormatConfigurations = [barcodeConfiguration],
                    ReturnBarcodeImage = true
                };
                
                var scanner = new SBSDKBarcodeScanner(configuration: scannerConfiguration);
                var result = scanner.ScanFromImage(image, false, optimizeOverlays: true);

                if (result?.Success != true)
                {
                    Alert.Show(this, "Alert", "No barcodes found on the input image.");
                    return;
                }

                // Handle the result in your app as needed.
                var controller = new ScanResultListController(result.Barcodes);
                NavigationController?.PushViewController(controller, animated: true);
            }
            catch (TaskCanceledException)
            {

            }
        }

        private void OnAcceptedTypesButtonClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this))
            {
                return;
            }
            var controller = new AcceptedBarcodeTypesController();
            NavigationController?.PushViewController(controller, true);
        }

        private void OnLicenseInfoButtonClick(object sender, EventArgs e)
        {
            var isValid = ScanbotSDKGlobal.IsLicenseValid;
            var status = ScanbotSDKGlobal.LicenseStatus;
            var date = ScanbotSDKGlobal.LicenseExpirationDate;

            var message = $"License status is {status} \n";

            if (isValid && date != null)
            {
                message += $"Valid until {date.ToDateTime()}";
            }

            Alert.Show(this, "Status", message);
        }

        private Task<UIImage> PickImageAsync()
        {
            var imagePicker = new UIImagePickerController
            {
                SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
                MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary),
                // This will prevent the ImagePicker from getting closed by swiping down.
                // The closing of ImagePicker by "Swiping Down" creates an issue in returning the taskSource. It never returns task cancelled.
                ModalPresentationStyle = UIModalPresentationStyle.FullScreen
            };
            
            var taskCompletionSource = new TaskCompletionSource<UIImage>();

            // Set event handlers
            imagePicker.FinishedPickingMedia += (object sender, UIImagePickerMediaPickedEventArgs args) => {
                UIImage image = args.EditedImage ?? args.OriginalImage;
                imagePicker.DismissViewController(true, () => taskCompletionSource.SetResult(image));
            };
            imagePicker.Canceled += (object sender, EventArgs args) =>
            {
                taskCompletionSource.SetCanceled();
                imagePicker.DismissViewController(true, null);
            };

            // Present UIImagePickerController;
            UIWindow window = UIApplication.SharedApplication.KeyWindow;
            var viewController = window?.RootViewController;
            viewController?.PresentViewController(imagePicker, true, null);

            return taskCompletionSource.Task;
        }
    }
}
