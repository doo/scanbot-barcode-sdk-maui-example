using System.Reflection;
using BarcodeSDK.NET.iOS.Controllers;
using BarcodeSDK.NET.iOS.Controllers.ClassicComponents;
using BarcodeSDK.NET.iOS.Utils;
using Scanbot.ImagePicker.iOS;
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
        
            #if LEGACY_EXAMPLES
            contentView.CreateText("Ready to Use UI (legacy)");
            contentView.CreateButton("Barcode Scanner", OnRTUUIButtonClick);
            contentView.CreateButton("Barcode Scanner with Image", OnRTUUIImageButtonClick);
            contentView.CreateButton("Batch Barcode Scanner", OnRTUBatchBarcodeClicked);
            #else
            contentView.CreateText("Ready to Use UI");
            contentView.CreateButton("Single Scanning", SingleScanning);
            contentView.CreateButton("Single Scanning Selection Overlay", SingleScanningWithArOverlay);
            contentView.CreateButton("Batch Barcode Scanning", BatchBarcodeScanning);
            contentView.CreateButton("Multiple Unique Barcode Scanning", MultipleUniqueBarcodeScanning);
            contentView.CreateButton("Find and Pick Barcode Scanning", FindAndPickScanning);
            #endif
            
            contentView.CreateText("SDK Operations");
            contentView.CreateButton("Pick Image From Library", OnLibraryButtonClick);
            contentView.CreateButton("Set Accepted Barcode Types", OnCodeTypeButtonClick);
            contentView.CreateButton("Clear Image Storage", OnClearStorageButtonClick);
            contentView.CreateButton("View License Info", OnLicenseInfoButtonClick);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            contentView.RemoveAllControls();
        }

        private void OnClassicButtonClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this))
            {
                return;
            }
            NavigationController.PushViewController(new BarcodeClassicComponentController(), animated: true);
        }

        private void OnClassicScanAndCountButtonClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this))
            {
                return;
            }
            var viewController = Utilities.GetViewController<BarcodeScanAndCountViewController>(Texts.ClassicComponentStoryboard);
            this.NavigationController.PushViewController(viewController, true);
        }

        private async void OnLibraryButtonClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this))
            {
                return;
            }

            // Optain an image from somewhere.
            // In this case, the user picks an image with our helper.
            UIImage image = await ImagePicker.Instance.PickImageAsync();

            if (image == null)
            {
                return;
            }

            // Configure the barcode detector for detecting many barcodes in one image.
            var scanner = new SBSDKBarcodeScanner(BarcodeTypes.Instance.AcceptedTypes)
            {
                EngineMode = SBSDKBarcodeEngineMode.NextGen,
                AdditionalParameters = new SBSDKBarcodeAdditionalParameters
                {
                    CodeDensity = SBSDKBarcodeDensity.High,
                }
             };

            var result = scanner.DetectBarCodesOnImage(image);

            // Handle the result in your app as needed.
            var controller = new ScanResultListController(image, result);
            NavigationController.PushViewController(controller, animated: true);
        }

        private void OnCodeTypeButtonClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this))
            {
                return;
            }
            var controller = new BarcodeListController();
            NavigationController.PushViewController(controller, true);
        }


        private void OnClearStorageButtonClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this))
            {
                return;
            }
            SBSDKUIBarcodeImageStorage.DefaultStorage.RemoveAll();
            Alert.Show(this, "Success", "Image storage cleared");
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

        private static void ShowPopup(UIViewController controller, string text, Action onClose = null)
        {
            
        }
    }
}
