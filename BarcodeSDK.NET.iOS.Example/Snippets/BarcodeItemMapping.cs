using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public class BarcodeItemMapping : BaseViewController
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

        var config = new SBSDKBarcodeScannerConfiguration
        {
            BarcodeFormatConfigurations = [commonConfiguration],
            ReturnBarcodeImage = true
        };

        _scannerController = new SBSDKBarcodeScannerViewController(this, View, config);
        _scannerController.ViewModel.TrackingOverlay.IsTrackingOverlayEnabled = true;

        _scannerController.Delegate = new BarcodeDetectionDelegate(NavigationController);
        _scannerController.ViewModel.TrackingOverlay.Delegate = new BarcodeSelectionDelegate(NavigationController);
    }

    private class BarcodeSelectionDelegate(UINavigationController navigationController) : SBSDKBarcodeTrackingOverlayControllerDelegate
    {
        public override void DidTapOnBarcode(SBSDKBarcodeTrackingOverlayController controller, SBSDKBarcodeItem barcode)
        {
            var resultsController = new ScanResultListController([barcode]);

            navigationController.PopViewController(animated: false);
            navigationController.PushViewController(resultsController, animated: true);
        }

        public override SBSDKBarcodeTrackingOverlayStyle StyleFor(SBSDKBarcodeTrackingOverlayController controller, SBSDKBarcodeTrackingOverlayItem item, SBSDKBarcodeTrackingOverlayStyle proposedStyle)
        {
            // Explore this object for more parameters
            proposedStyle.PolygonColor = UIColor.Yellow;
            proposedStyle.PolygonBackgroundColor = UIColor.Clear;
            
            // Explore this object for more parameters
            proposedStyle.TextColor = UIColor.Yellow;
            proposedStyle.TextBackgroundColor = UIColor.Black;
            
            proposedStyle.TextOverride = "Some text";
            
            return proposedStyle;
        }
    }

    private class BarcodeDetectionDelegate(UINavigationController navigationController) : SBSDKBarcodeScannerViewControllerDelegate
    {
        public override void DidScanBarcodes(SBSDKBarcodeScannerViewController barcodeController, SBSDKBarcodeItem[] codes)
        {
            // In this example,
            // We only focus on AR overlay i.e., DidTapOnBarcode method
        }

        public override bool ShouldScanBarcodes(SBSDKBarcodeScannerViewController controller)
        {
            return true;
        }
    }
}