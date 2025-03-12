using System.Reflection;
using BarcodeSDK.NET.iOS.Controllers;
using BarcodeSDK.NET.iOS.Controllers.ClassicComponents;
using BarcodeSDK.NET.iOS.Utils;
using ScanbotSDK.iOS;
using UIKit;

namespace BarcodeSDK.NET.iOS
{
    public partial class MainViewController : UIViewController
    {
        private MainView contentView;

        internal static UIColor ScanbotRed => FlashButton.ScanbotRed;

        public UIViewController ViewController => this;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            contentView = new MainView(); 
            View = contentView;

            Title = "BARCODE SCANNER";
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            contentView.CreateText("Classic Components");
            contentView.CreateButton("Barcode Component", OnClassicButtonClick);
            contentView.CreateButton("Barcode Scan and Count Component", OnClassicScanAndCountButtonClick);
        
            contentView.CreateText("Ready to Use UI");
            contentView.CreateButton("Single Scanning", SingleScanning);
            contentView.CreateButton("Single Scanning Selection Overlay", SingleScanningWithArOverlay);
            contentView.CreateButton("Batch Barcode Scanning", BatchBarcodeScanning);
            contentView.CreateButton("Multiple Unique Barcode Scanning", MultipleUniqueBarcodeScanning);
            contentView.CreateButton("Find and Pick Barcode Scanning", FindAndPickScanning);
            
            contentView.CreateText("SDK Operations");
            contentView.CreateButton("Pick Image From Library", OnLibraryButtonClick);
            contentView.CreateButton("Set Accepted Barcode Types", OnCodeTypeButtonClick);
            contentView.CreateButton("View License Info", OnLicenseInfoButtonClick);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            contentView.RemoveAllControls();
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

            // Optain an image from somewhere.
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
                var result = scanner.ScanFromImage(image);

                if (result == null || !result.Success || result.Barcodes.Length == 0)
                {
                    return;
                }

                // Handle the result in your app as needed.
                var controller = new ScanResultListController(result.Barcodes, image);
                NavigationController?.PushViewController(controller, animated: true);
            }
            catch (TaskCanceledException)
            {

            }
        }

        private void OnCodeTypeButtonClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this))
            {
                return;
            }
            var controller = new BarcodeListController();
            NavigationController?.PushViewController(controller, true);
        }

        private void OnLicenseInfoButtonClick(object sender, EventArgs e)
        {
            var status = ScanbotSDKGlobal.LicenseStatus;
            var date = ScanbotSDKGlobal.LicenseExpirationDate;

            var message = $"License status is {status}";

            if (date != null)
            {
                message += $" until {date.ToDateTime()}";
            }

            Alert.Show(this, "Status", message);
        }

        private static void ShowPopup(UIViewController controller, string text)
        {
            var alertController = UIAlertController.Create("Scanner Result", text,  UIAlertControllerStyle.Alert);
            alertController.AddAction(UIAlertAction.Create("Dismiss", UIAlertActionStyle.Cancel, null));
            controller.PresentViewController(alertController, animated: true, completionHandler: null);
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
