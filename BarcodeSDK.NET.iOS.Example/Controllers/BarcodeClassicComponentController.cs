using ScanbotSDK.iOS;
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

            var configuration = new SBSDKBarcodeFormatCommonConfiguration
            {
                Formats = BarcodeTypes.Instance.AcceptedTypes
            };
            
            SBSDKBarcodeScannerConfiguration config = new SBSDKBarcodeScannerConfiguration
            {
                BarcodeFormatConfigurations = [configuration],
                ReturnBarcodeImage = true
            };
            
            scannerController = new SBSDKBarcodeScannerViewController(this, View, config);
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
            flash.Click += (_, e) =>
            {
                scannerController.IsFlashLightEnabled = e.Enabled;
            };
        }

        private class BarcodeSelectionDelegate(UINavigationController navigationController)
            : SBSDKBarcodeTrackingOverlayControllerDelegate
        {
            public override void DidTapOnBarcode(SBSDKBarcodeTrackingOverlayController controller, SBSDKBarcodeItem barcode)
            {
                var resultsController = new ScanResultListController([barcode]);

                navigationController.PopViewController(animated: false);
                navigationController.PushViewController(resultsController, animated: true);
            }
        }

        private class BarcodeDetectionDelegate(UINavigationController navigationController)
            : SBSDKBarcodeScannerViewControllerDelegate
        {
            public override void DidScanBarcodes(SBSDKBarcodeScannerViewController barcodeController, SBSDKBarcodeItem[] codes)
            {
                var shouldHandleBarcode = !barcodeController.IsTrackingOverlayEnabled || barcodeController.TrackingOverlayController.Configuration.IsAutomaticSelectionEnabled;

                if (!shouldHandleBarcode)
                {
                    return;
                }

                if (navigationController.TopViewController is ScanResultListController)
                {
                    return;
                }

                var resultsController = new ScanResultListController(codes);

                navigationController.PopViewController(animated: false);
                navigationController.PushViewController(resultsController, animated: true);
            }

            public override bool ShouldScanBarcodes(SBSDKBarcodeScannerViewController controller)
            {
                return true;
            }
        }
    }
}

