using Scanbot.ImagePicker.iOS;
using ScanbotBarcodeSDK.iOS;

namespace BarcodeSDK.NET.iOS
{
    public class MainViewController : UIViewController
    {
        private MainView contentView;

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
            NavigationController.PushViewController(new ClassicScannerController(), animated: true);
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

            UIImage image = await ImagePicker.Instance.PickImageAsync();
            if (image != null)
            {
                var scanner = new SBSDKBarcodeScanner(BarcodeTypes.Instance.AcceptedTypes);
                SBSDKBarcodeScannerResult[] result = scanner.DetectBarCodesOnImage(image);

                var controller = new ScanResultListController(image, result);
                NavigationController.PushViewController(controller, animated: true);
            }
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
            var status = ScanbotSDK.LicenseStatus;
            var date = ScanbotSDK.LicenseExpirationDate;

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

            configuration.SelectionOverlayConfiguration.OverlayEnabled = true;
            configuration.SelectionOverlayConfiguration.AutomaticSelectionEnabled = false;
            configuration.SelectionOverlayConfiguration.OverlayTextFormat = SBSDKBarcodeOverlayFormat.Code;
            configuration.SelectionOverlayConfiguration.TextContainerColor = UIColor.Black;
            configuration.SelectionOverlayConfiguration.TextColor = UIColor.Yellow;
            configuration.SelectionOverlayConfiguration.PolygonColor = UIColor.Yellow;

            SBSDKUIBarcodeScannerViewController.PresentOn(this, configuration, new BarcodeDelegate(NavigationController));
        }

        private class BarcodeDelegate : SBSDKUIBarcodeScannerViewControllerDelegate
        {
            private UINavigationController navigationController;
            public BarcodeDelegate(UINavigationController navigationController)
            {
                this.navigationController = navigationController;
            }

            public override void DidDetect(SBSDKUIBarcodeScannerViewController viewController, SBSDKBarcodeScannerResult[] barcodeResults)
            {
                viewController.DismissViewController(animated: false, completionHandler: null);

                if (barcodeResults == null || barcodeResults?.Length == 0)
                {
                    Console.WriteLine("Result is empty, returning");
                    return;
                }

                var controller = new ScanResultListController(barcodeResults.First().BarcodeImage, barcodeResults);
                navigationController.PushViewController(controller, true);
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
            configuration.SelectionOverlayConfiguration.OverlayEnabled = true;
            configuration.SelectionOverlayConfiguration.AutomaticSelectionEnabled = true;
            configuration.SelectionOverlayConfiguration.OverlayTextFormat = SBSDKBarcodeOverlayFormat.Code;
            configuration.SelectionOverlayConfiguration.TextColor = UIColor.Yellow;
            configuration.SelectionOverlayConfiguration.TextContainerColor = UIColor.Black;
            configuration.SelectionOverlayConfiguration.HighlightedPolygonColor = UIColor.Red;
            configuration.SelectionOverlayConfiguration.HighlightedTextColor = UIColor.Red;
            configuration.SelectionOverlayConfiguration.HighlightedTextContainerColor = UIColor.Black;
            configuration.SelectionOverlayConfiguration.PolygonColor = UIColor.Yellow;

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
                navigationController.PushViewController(resultViewController, true);
            }
        }

    }
}
