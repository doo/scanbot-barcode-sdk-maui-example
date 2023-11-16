using ScanbotBarcodeSDK.iOS;

namespace BarcodeSDK.NET.iOS
{
    public class ClassicScannerController : UIViewController
    {
        private SBSDKBarcodeScannerViewController scannerController;

        private ClassicBarcodeDelegate receiver;

        public FlashButton Flash { get; set; }

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

            scannerController.SelectionOverlayEnabled = true;
            scannerController.AutomaticSelectionEnabled = false;
            scannerController.SelectionOverlayTextFormat = SBSDKBarcodeOverlayFormat.Code;
            scannerController.SelectionPolygonColor = UIColor.Yellow;
            scannerController.SelectionTextColor = UIColor.Yellow;
            scannerController.SelectionTextContainerColor = UIColor.Black;

            scannerController.SelectionHighlightedPolygonColor = UIColor.Red;
            scannerController.SelectionHighlightedTextColor = UIColor.Red;
            scannerController.SelectionHighlightedTextContainerColor = UIColor.Black;

            receiver = new ClassicBarcodeDelegate(NavigationController);
            scannerController.Delegate = receiver;
            
            Flash = new FlashButton();
            View.AddSubview(Flash);
            View.BackgroundColor = UIColor.Black;

            nfloat size = 55;
            nfloat padding = 10;
            Flash.Frame = new CGRect(padding, padding, size, size);
            Flash.Click += (sender, e) =>
            {
                scannerController.FlashLightEnabled = e.Enabled;
            };
        }

        private class ClassicBarcodeDelegate : SBSDKBarcodeScannerViewControllerDelegate
        {
            private UINavigationController navigationController;

            public ClassicBarcodeDelegate(UINavigationController navigationController)
            {
                this.navigationController = navigationController;
            }


            public override void DidDetectBarcodes(
                SBSDKBarcodeScannerViewController _, SBSDKBarcodeScannerResult[] codes)
            {

                var controller = new ScanResultListController(codes.First().SourceImage, codes);

                navigationController.PopViewController(animated: false);
                navigationController.PushViewController(controller, animated: true);
            }

            public override bool ShouldDetectBarcodes(SBSDKBarcodeScannerViewController controller)
            {
                return true;
            }
        }
    }
}

