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
        _scannerController.IsTrackingOverlayEnabled = true;
        _scannerController.TrackingOverlayController.Configuration.TextStyle.TrackingOverlayTextFormat = SBSDKBarcodeOverlayFormat.Code;

        _scannerController.Delegate = new BarcodeDetectionDelegate(NavigationController);
        _scannerController.TrackingOverlayController.Delegate = new BarcodeSelectionDelegate(NavigationController);
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
            return "Some text";
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