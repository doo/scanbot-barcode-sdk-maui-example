using BarcodeSDK.NET.iOS.Controllers;
using BarcodeSDK.NET.iOS.Controllers.ClassicComponents;
using BarcodeSDK.NET.iOS.Utils;
using Scanbot.ImagePicker.iOS;
using ScanbotBarcodeSDK.iOS;

namespace BarcodeSDK.NET.iOS
{
    public class MainViewController : UIViewController
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

            contentView.ClassicButton.TouchUpInside += OnClassicButtonClick;
            contentView.ClassicScanAndCountButton.TouchUpInside += OnClassicScanAndCountButtonClick;
            contentView.RTUUIButton.TouchUpInside += OnRTUUIButtonClick;
            contentView.RTUUIImageButton.TouchUpInside += OnRTUUIImageButtonClick;
            contentView.LibraryButton.TouchUpInside += OnLibraryButtonClick;
            contentView.CodeTypesButton.TouchUpInside += OnCodeTypeButtonClick;
            contentView.StorageClearButton.TouchUpInside += OnClearStorageButtonClick;
            contentView.LicenseInfoButton.TouchUpInside += OnLicenseInfoButtonClick;
            contentView.RTUUIBatchBarcodeButton.TouchUpInside += OnRTUBatchBarcodeClicked;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            contentView.ClassicButton.TouchUpInside -= OnClassicButtonClick;
            contentView.ClassicScanAndCountButton.TouchUpInside -= OnClassicScanAndCountButtonClick;
            contentView.RTUUIButton.TouchUpInside -= OnRTUUIButtonClick;
            contentView.RTUUIImageButton.TouchUpInside -= OnRTUUIImageButtonClick;
            contentView.LibraryButton.TouchUpInside -= OnLibraryButtonClick;
            contentView.CodeTypesButton.TouchUpInside -= OnCodeTypeButtonClick;
            contentView.StorageClearButton.TouchUpInside -= OnClearStorageButtonClick;
            contentView.LicenseInfoButton.TouchUpInside -= OnLicenseInfoButtonClick;
            contentView.RTUUIBatchBarcodeButton.TouchUpInside -= OnRTUBatchBarcodeClicked;
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

        private void OnRTUUIButtonClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this))
            {
                return;
            }
            OpenRTUUIBarcodeScanner(withImage: false);
        }

        private void OnRTUUIImageButtonClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this))
            {
                return;
            }
            OpenRTUUIBarcodeScanner(withImage: true);
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

        void OpenRTUUIBarcodeScanner(bool withImage)
        {
            var configuration = SBSDKUIBarcodeScannerConfiguration.DefaultConfiguration;
            configuration.BehaviorConfiguration.AcceptedBarcodeTypes = BarcodeTypes.Instance.AcceptedTypes;
            configuration.BehaviorConfiguration.AdditionalParameters = new SBSDKBarcodeAdditionalParameters
            {
                CodeDensity = SBSDKBarcodeDensity.High
            };
            configuration.BehaviorConfiguration.EngineMode = SBSDKBarcodeEngineMode.NextGen;
            configuration.BehaviorConfiguration.SuccessBeepEnabled = true;

            if (withImage)
            {
                configuration.BehaviorConfiguration.BarcodeImageGenerationType =
                    SBSDKBarcodeImageGenerationType.CapturedImage;
            }

            configuration.TrackingOverlayConfiguration.OverlayEnabled = true;
            configuration.TrackingOverlayConfiguration.AutomaticSelectionEnabled = false;
            configuration.TrackingOverlayConfiguration.OverlayTextFormat = SBSDKBarcodeOverlayFormat.Code;
            configuration.TrackingOverlayConfiguration.TextContainerColor = UIColor.Black;
            configuration.TrackingOverlayConfiguration.TextColor = UIColor.Yellow;
            configuration.TrackingOverlayConfiguration.PolygonColor = UIColor.Yellow;

            // To see the confirmation dialog in action, uncomment the below and comment out the configuration.TrackingOverlayConfiguration lines above.
            //configuration.TextConfiguration.ConfirmationDialogTitle = "Barcode Detected!";
            //configuration.TextConfiguration.ConfirmationDialogMessage = "A barcode was found.";
            //configuration.TextConfiguration.ConfirmationDialogConfirmButtonTitle = "Continue";
            //configuration.TextConfiguration.ConfirmationDialogRetryButtonTitle = "Try again";
            //configuration.BehaviorConfiguration.ResultWithConfirmationEnabled = true;
            //configuration.BehaviorConfiguration.DialogTextFormat = SBSDKBarcodeDialogFormat.TypeAndCode;

            SBSDKUIBarcodeScannerViewController.PresentOn(this, configuration, new BarcodeDelegate(NavigationController));
        }

        private class BarcodeDelegate : SBSDKUIBarcodeScannerViewControllerDelegate
        {
            private UINavigationController navigationController;
            public BarcodeDelegate(UINavigationController navigationController)
            {
                this.navigationController = navigationController;
            }

            public override void DidDetectResults(SBSDKUIBarcodeScannerViewController viewController, SBSDKBarcodeScannerResult[] barcodeResults)
            {
                viewController.DismissViewController(animated: false, completionHandler: null);

                if (barcodeResults == null || barcodeResults?.Length == 0)
                {
                    return;
                }

                if (navigationController.TopViewController is ScanResultListController)
                {
                    return;
                }

                var controller = new ScanResultListController(barcodeResults.First().BarcodeImage, barcodeResults);
                navigationController.PushViewController(controller, animated: true);
            }
        }

        private void OnRTUBatchBarcodeClicked(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this))
            {
                return;
            }

            // You can also use the DefaultConfiguration property directly, but then you cannot set the selectionOverlayConfig as it is read only in that case.
            var configuration = SBSDKUIBarcodesBatchScannerConfiguration.DefaultConfiguration;

            configuration.BehaviorConfiguration.AcceptedBarcodeTypes = BarcodeTypes.Instance.AcceptedTypes;
            configuration.BehaviorConfiguration.EngineMode = SBSDKBarcodeEngineMode.NextGen;
            configuration.BehaviorConfiguration.SuccessBeepEnabled = true;
            configuration.BehaviorConfiguration.AdditionalDetectionParameters = new SBSDKBarcodeAdditionalParameters
            {
                CodeDensity = SBSDKBarcodeDensity.High
            };
            configuration.TrackingOverlayConfiguration.OverlayEnabled = true;
            configuration.TrackingOverlayConfiguration.AutomaticSelectionEnabled = true;
            configuration.TrackingOverlayConfiguration.OverlayTextFormat = SBSDKBarcodeOverlayFormat.Code;
            configuration.TrackingOverlayConfiguration.TextColor = UIColor.Yellow;
            configuration.TrackingOverlayConfiguration.TextContainerColor = UIColor.Black;
            configuration.TrackingOverlayConfiguration.HighlightedPolygonColor = UIColor.Red;
            configuration.TrackingOverlayConfiguration.HighlightedTextColor = UIColor.Red;
            configuration.TrackingOverlayConfiguration.HighlightedTextContainerColor = UIColor.Black;
            configuration.TrackingOverlayConfiguration.PolygonColor = UIColor.Yellow;

            SBSDKUIBarcodesBatchScannerViewController.PresentOn(this, configuration, new BatchBarcodeDelegate(NavigationController));
        }

        private class BatchBarcodeDelegate : SBSDKUIBarcodesBatchScannerViewControllerDelegate
        {
            private UINavigationController navigationController;
            public BatchBarcodeDelegate(UINavigationController navigationController)
            {
                this.navigationController = navigationController;
            }

            public override void DidDetect(SBSDKUIBarcodesBatchScannerViewController viewController, SBSDKUIBarcodeMappedResult[] barcodeResults)
            {
                var resultViewController = new BatchBarcodeResultViewController(barcodeResults ?? new SBSDKUIBarcodeMappedResult[] { });
                navigationController.PushViewController(resultViewController, animated: true);
            }
        }

    }
}
