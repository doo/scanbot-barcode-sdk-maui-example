using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public class BarcodeClassicComponentController : BaseViewController
{
    private SBSDKBarcodeScannerViewController _scannerController;

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
            MinimumTextLength = new IntPtr(10)
        };

        var config = new SBSDKBarcodeScannerConfiguration
        {
            BarcodeFormatConfigurations = [commonConfiguration, dataMatrixConfig],
            ReturnBarcodeImage = true
        };

        _scannerController = new SBSDKBarcodeScannerViewController(this, View, config);
        _scannerController.IsTrackingOverlayEnabled = true;
        _scannerController.TrackingOverlayController.Configuration.TextStyle.TrackingOverlayTextFormat = SBSDKBarcodeOverlayFormat.Code;

        _scannerController.Delegate = new BarcodeDetectionDelegate(NavigationController);
        _scannerController.TrackingOverlayController.Delegate = new BarcodeSelectionDelegate(NavigationController);

        // Sets the flash button to RightBarButtonItem. Updates the flash color based on flash status.
        SetFlashButton(() =>
        {
            _scannerController.IsFlashLightEnabled = !_scannerController.IsFlashLightEnabled;
            return _scannerController.IsFlashLightEnabled;
        });
    }

    private class BarcodeDetectionDelegate(UINavigationController navigationController) : SBSDKBarcodeScannerViewControllerDelegate
    {
        public override void DidScanBarcodes(SBSDKBarcodeScannerViewController barcodeController, SBSDKBarcodeItem[] codes)
        {
            if (navigationController.TopViewController is ScanResultListController)
            {
                return;
            }

            var shouldHandleBarcode = !barcodeController.IsTrackingOverlayEnabled;

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

    private class BarcodeSelectionDelegate(UINavigationController navigationController) : SBSDKBarcodeTrackingOverlayControllerDelegate
    {
        public override void DidTapOnBarcode(SBSDKBarcodeTrackingOverlayController controller, SBSDKBarcodeItem barcode)
        {
            var resultsController = new ScanResultListController([barcode]);

            navigationController.PopViewController(animated: false);
            navigationController.PushViewController(resultsController, animated: true);
        }

        public override SBSDKBarcodeTrackedViewPolygonStyle PolygonStyleFor(SBSDKBarcodeTrackingOverlayController controller, SBSDKBarcodeItem barcode, SBSDKBarcodeTrackedViewPolygonStyle proposedStyle)
        {
            // Explore this object for more parameters
            proposedStyle.PolygonColor = UIColor.Yellow;
            proposedStyle.PolygonBackgroundColor = UIColor.Clear;
            return proposedStyle;
        }

        public override SBSDKBarcodeTrackedViewTextStyle TextStyleFor(SBSDKBarcodeTrackingOverlayController controller, SBSDKBarcodeItem barcode, SBSDKBarcodeTrackedViewTextStyle proposedStyle)
        {
            // Explore this object for more parameters
            proposedStyle.TextColor = UIColor.Yellow;
            proposedStyle.TextBackgroundColor = UIColor.Black;
            return proposedStyle;
        }

        public override string OverrideTextFor(SBSDKBarcodeTrackingOverlayController controller, SBSDKBarcodeItem barcode, string proposedString)
        {
            // Update the required text over the AR overlay of detected barcodes.
            return proposedString;
        }
    }
}
