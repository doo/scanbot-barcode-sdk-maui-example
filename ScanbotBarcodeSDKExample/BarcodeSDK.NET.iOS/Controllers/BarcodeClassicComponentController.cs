using ScanbotBarcodeSDK.iOS;
using UIKit;

namespace BarcodeSDK.NET.iOS
{
    public class BarcodeClassicComponentController : UIViewController
    {
        private SBSDKBarcodeScannerViewController scannerController;

        private FlashButton flash;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "CLASSIC COMPONENT";

            scannerController = new SBSDKBarcodeScannerViewController(this, View);
            scannerController.AcceptedBarcodeTypes = BarcodeTypes.Instance.AcceptedTypes;
            scannerController.AdditionalDetectionParameters = new SBSDKBarcodeAdditionalParameters
            {
                CodeDensity = SBSDKBarcodeDensity.High
            };
            scannerController.EngineMode = SBSDKBarcodeEngineMode.NextGen;

            scannerController.IsTrackingOverlayEnabled = true;
            scannerController.TrackingOverlayController.Configuration.IsAutomaticSelectionEnabled = false;
            scannerController.TrackingOverlayController.Configuration.TextStyle.TrackingOverlayTextFormat = SBSDKBarcodeOverlayFormat.Code;
            scannerController.TrackingOverlayController.Configuration.PolygonStyle.PolygonColor = UIColor.Yellow;
            scannerController.TrackingOverlayController.Configuration.PolygonStyle.PolygonBackgroundColor = UIColor.Yellow.ColorWithAlpha(0.25f);
            scannerController.TrackingOverlayController.Configuration.TextStyle.TextColor = UIColor.Yellow;
            scannerController.TrackingOverlayController.Configuration.TextStyle.TextBackgroundColor = UIColor.Black;

            scannerController.TrackingOverlayController.Configuration.PolygonStyle.PolygonSelectedColor = UIColor.Red;
            scannerController.TrackingOverlayController.Configuration.PolygonStyle.PolygonBackgroundSelectedColor = UIColor.Red.ColorWithAlpha(0.25f);
            scannerController.TrackingOverlayController.Configuration.TextStyle.SelectedTextColor = UIColor.Red;
            scannerController.TrackingOverlayController.Configuration.TextStyle.TextBackgroundSelectedColor = UIColor.Black;

            scannerController.Delegate = new BarcodeDetectionDelegate(NavigationController);
            scannerController.TrackingOverlayController.Delegate = new BarcodeSelectionDelegate(NavigationController);
            
            flash = new FlashButton();
            View.AddSubview(flash);
            View.BackgroundColor = UIColor.Black;

            nfloat size = 55;
            nfloat padding = 10;
            flash.Frame = new CGRect(padding, padding, size, size);
            flash.Click += (sender, e) =>
            {
                scannerController.FlashLightEnabled = e.Enabled;
            };
        }

        private class BarcodeSelectionDelegate : SBSDKBarcodeTrackingOverlayControllerDelegate
        {
            private UINavigationController navigationController;

            public BarcodeSelectionDelegate(UINavigationController navigationController)
            {
                this.navigationController = navigationController;
            }

            public override void DidTapOnBarcode(SBSDKBarcodeTrackingOverlayController overlayController, SBSDKBarcodeScannerResult barcode)
            {
                var resultsController = new ScanResultListController(barcode.SourceImage, new[] { barcode });

                navigationController.PopViewController(animated: false);
                navigationController.PushViewController(resultsController, animated: true);
            }
        }

        private class BarcodeDetectionDelegate : SBSDKBarcodeScannerViewControllerDelegate
        {
            private UINavigationController navigationController;

            public BarcodeDetectionDelegate(UINavigationController navigationController)
            {
                this.navigationController = navigationController;
            }

            public override void DidDetectBarcodes(
                SBSDKBarcodeScannerViewController barcodeController, SBSDKBarcodeScannerResult[] codes)
            {
                if (!barcodeController.TrackingOverlayController.Configuration.IsAutomaticSelectionEnabled)
                {
                    return;
                }

                var resultsController = new ScanResultListController(codes.First().SourceImage, codes);

                navigationController.PopViewController(animated: false);
                navigationController.PushViewController(resultsController, animated: true);
            }

            public override bool ShouldDetectBarcodes(SBSDKBarcodeScannerViewController controller)
            {
                return true;
            }
        }
    }
}

