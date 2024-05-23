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

#if LEGACY
            contentView.CreateButton("RTU UI v1 - Single", OnRTUUIButtonClick);
            contentView.CreateButton("RTU UI v1 – Single with Image", OnRTUUIImageButtonClick);
            contentView.CreateButton("RTU UI v1 - Batch", OnRTUBatchBarcodeClicked);
#endif

            contentView.CreateButton("RTU UI v2 - Single", ShowSingleBarcodeScannerFromRTUUI);
            contentView.CreateButton("RTU UI v2 - Single AR", ShowSingleARBarcodeScannerFromRTUUI);
            contentView.CreateButton("RTU UI v2 - Single AR Auto Select", ShowSingleARAutoSelectBarcodeScannerFromRTUUI);
            contentView.CreateButton("RTU UI v2 - Multiple", ShowMultiBarcodeScannerFromRTUUI);
            contentView.CreateButton("RTU UI v2 - Multiple Sheet", ShowMultiSheetBarcodeScannerFromRTUUI);
            contentView.CreateButton("RTU UI v2 - Multiple Sheet AR Count", ShowMultiSheetARCountAutoSelectBarcodeScannerFromRTUUI);

            contentView.CreateButton("Classic Component", OnClassicButtonClick);
            contentView.CreateButton("Classic Scan and Count Component", OnClassicScanAndCountButtonClick);
            contentView.CreateButton("Pick Image From Library", OnLibraryButtonClick);
            contentView.CreateButton("Set Accepted Barcode Types", OnCodeTypeButtonClick);
            contentView.CreateButton("Clear Image Storage", OnClearStorageButtonClick);
            contentView.CreateButton("View License Info", OnLicenseInfoButtonClick);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            contentView.RemoveAllButtons();
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
