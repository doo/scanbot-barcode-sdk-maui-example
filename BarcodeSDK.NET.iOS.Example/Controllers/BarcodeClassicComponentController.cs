using ScanbotSDK.iOS;
using UIKit;

namespace BarcodeSDK.NET.iOS
{
    public class BarcodeClassicComponentController : BaseViewController
    {
        private SBSDKBarcodeScannerViewController scannerController;

        public override void ViewDidLoad()
        {
            PageTitle = "BarcodeScannerView";
            base.ViewDidLoad();

            var commonConfiguration = new SBSDKBarcodeFormatCommonConfiguration
            {
                Formats = BarcodeTypes.Instance.AcceptedTypes
            };
            
            // Configure different parameters for specific barcode format.
            var dataMatrixConfig = new SBSDKBarcodeFormatCode128Configuration
            {
               MinimumTextLength = 10
            };
            
            var config = new SBSDKBarcodeScannerConfiguration
            {
                BarcodeFormatConfigurations = [commonConfiguration, dataMatrixConfig],
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

            // Sets the flash button to RightBarButtonItem. Updates the flash color based on flash status.
            SetFlashButton(() =>
            {
                scannerController.IsFlashLightEnabled = !scannerController.IsFlashLightEnabled;
                return scannerController.IsFlashLightEnabled;
            });
        }

        private class BarcodeSelectionDelegate(UINavigationController navigationController): SBSDKBarcodeTrackingOverlayControllerDelegate
        {
            public override void DidTapOnBarcode(SBSDKBarcodeTrackingOverlayController controller, SBSDKBarcodeItem barcode)
            {
                var resultsController = new ScanResultListController([barcode]);

                navigationController.PopViewController(animated: false);
                navigationController.PushViewController(resultsController, animated: true);
            }
        }

        private class BarcodeDetectionDelegate(UINavigationController navigationController): SBSDKBarcodeScannerViewControllerDelegate
        {
            public override void DidScanBarcodes(SBSDKBarcodeScannerViewController barcodeController, SBSDKBarcodeItem[] codes)
            {
                if (navigationController.TopViewController is ScanResultListController)
                {
                    return;
                }
                
                var shouldHandleBarcode = barcodeController.TrackingOverlayController.Configuration.IsAutomaticSelectionEnabled || !barcodeController.IsTrackingOverlayEnabled;

                if (!shouldHandleBarcode)
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
