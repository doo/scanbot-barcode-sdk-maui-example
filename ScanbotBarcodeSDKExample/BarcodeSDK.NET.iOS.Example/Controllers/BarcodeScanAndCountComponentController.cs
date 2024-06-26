using System.Text;
using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS.Controllers
{
	public class BarcodeScanAndCountComponentController : UIViewController
    {
        UIButton flash;
        UIButton start;
        UIButton continueScanning;
        UILabel resultLabel;

        private SBSDKBarcodeScanAndCountViewController scannerController;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "SCAN AND COUNT COMPONENT";

            scannerController = new SBSDKBarcodeScanAndCountViewController(this, View);
            scannerController.AcceptedBarcodeTypes = BarcodeTypes.Instance.AcceptedTypes;
            scannerController.AdditionalDetectionParameters = new SBSDKBarcodeAdditionalParameters
            {
                CodeDensity = SBSDKBarcodeDensity.High
            };
            scannerController.EngineMode = SBSDKBarcodeEngineMode.NextGen;

            scannerController.PolygonStyle.PolygonDrawingEnabled = true;
            scannerController.PolygonStyle.PolygonColor = UIColor.Yellow;

            scannerController.Delegate = new BarcodeDetectionDelegate(
                HandleDidStartScanning,
                HandleDidDetectBarcodes);

            scannerController.ShutterButtonHidden = true;

            flash = new UIButton() { BackgroundColor = UIColor.Black };
            start = new UIButton() { BackgroundColor = UIColor.Black };
            continueScanning = new UIButton() { BackgroundColor = UIColor.Black, Hidden = true };
            resultLabel = new UILabel() { Lines = 0, MinimumFontSize = 10, TextColor = UIColor.White, BackgroundColor = UIColor.Black, Hidden = true };

            flash.SetTitle("Flash", UIControlState.Normal);
            start.SetTitle("Start", UIControlState.Normal);
            continueScanning.SetTitle("Continue", UIControlState.Normal);

            View.AddSubviews(resultLabel, flash, start, continueScanning);

            flash.TranslatesAutoresizingMaskIntoConstraints = false;
            start.TranslatesAutoresizingMaskIntoConstraints = false;
            continueScanning.TranslatesAutoresizingMaskIntoConstraints = false;
            resultLabel.TranslatesAutoresizingMaskIntoConstraints = false;

            nfloat buttonWidth = 100;

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                flash.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor),
                flash.BottomAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.BottomAnchor),
                flash.WidthAnchor.ConstraintEqualTo(buttonWidth),

                start.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor),
                start.BottomAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.BottomAnchor),
                start.WidthAnchor.ConstraintEqualTo(buttonWidth),

                continueScanning.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor),
                continueScanning.BottomAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.BottomAnchor),
                continueScanning.WidthAnchor.ConstraintEqualTo(buttonWidth),
            });

            flash.TouchUpInside += Flash_TouchUpInside;
            start.TouchUpInside += Start_TouchUpInside;
            continueScanning.TouchUpInside += ContinueScanning_TouchUpInside;
        }

        private void Flash_TouchUpInside(object sender, EventArgs e)
        {
            scannerController.IsFlashLightEnabled = !scannerController.IsFlashLightEnabled;
        }

        private void Start_TouchUpInside(object sender, EventArgs e)
        {
            scannerController.IsRecognitionEnabled = true;
            scannerController?.ScanAndCount();

            start.Hidden = true;
            continueScanning.Hidden = false;
        }

        private void ContinueScanning_TouchUpInside(object sender, EventArgs e)
        {
            scannerController?.ContinueScanning();

            start.Hidden = false;
            continueScanning.Hidden = true;
        }

        private void HandleDidDetectBarcodes(SBSDKBarcodeScannerResult[] codes)
        {
            var sb = new StringBuilder();

            foreach(var code in codes)
            {
                var alreadyCountedBarcode = scannerController.CountedBarcodes?
                    .FirstOrDefault(item => item.Code.Type == code.Type &&
                                            item.Code.RawTextString == code.RawTextString);

                alreadyCountedBarcode.ScanCount += 1;
                alreadyCountedBarcode.Code.DateOfDetection = code.DateOfDetection;
            }


            foreach(var code in scannerController.CountedBarcodes)
            {
                sb.Append($"{code.Code.RawTextString} - {code.ScanCount} \n");
            }

            resultLabel.Text = sb.ToString();
            resultLabel.Hidden = false;
        }

        private void HandleDidStartScanning()
        {
            
        }

        private class BarcodeDetectionDelegate : SBSDKBarcodeScanAndCountViewControllerDelegate
        {
            private Action _didStartScanningAction;
            private Action<SBSDKBarcodeScannerResult[]> _didDetectBarcodesAction;

            public BarcodeDetectionDelegate(
                Action didStartScanningAction,
                Action<SBSDKBarcodeScannerResult[]> didDetectBarcodesAction)
            {
                _didStartScanningAction = didStartScanningAction;
                _didDetectBarcodesAction = didDetectBarcodesAction;

            }

            public override void DidStartScanning(SBSDKBarcodeScanAndCountViewController controller)
            {
                _didStartScanningAction?.Invoke();
            }

            public override void DidDetectBarcodes(SBSDKBarcodeScanAndCountViewController controller, SBSDKBarcodeScannerResult[] codes)
            {
                _didDetectBarcodesAction?.Invoke(codes);
            }

            public override UIView OverlayForBarcode(SBSDKBarcodeScanAndCountViewController controller, SBSDKBarcodeScannerResult code)
            {
                return new UIImageView(image: UIImage.CheckmarkImage);
            }
        }
    }
}

