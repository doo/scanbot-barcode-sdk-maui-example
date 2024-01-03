using System;
using System.Reflection.Emit;
using ScanbotBarcodeSDK.iOS;

namespace BarcodeSDK.NET.iOS.Controllers
{
	public class BarcodeScanAndCountComponentController : UIViewController
    {
        UIButton flash;
        UIButton start;
        UIButton continueScanning;

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
                NavigationController,
                HandleDidStartScanning,
                HandleDidDetectBarcodes);

            scannerController.ShutterButtonHidden = true;

            flash = new UIButton() { BackgroundColor = UIColor.Black };
            start = new UIButton() { BackgroundColor = UIColor.Black };
            continueScanning = new UIButton() { BackgroundColor = UIColor.Black, Hidden = true };

            flash.SetTitle("Flash", UIControlState.Normal);
            start.SetTitle("Start", UIControlState.Normal);
            continueScanning.SetTitle("Continue", UIControlState.Normal);

            View.AddSubviews(flash, start, continueScanning);

            flash.TranslatesAutoresizingMaskIntoConstraints = false;
            start.TranslatesAutoresizingMaskIntoConstraints = false;
            continueScanning.TranslatesAutoresizingMaskIntoConstraints = false;

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
            scannerController.FlashLightEnabled = !scannerController.FlashLightEnabled;
        }

        private void Start_TouchUpInside(object sender, EventArgs e)
        {
            scannerController.RecognitionEnabled = true;
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

        private void HandleDidDetectBarcodes(SBSDKBarcodeScannerResult[] obj)
        {
            Alert.Show(this, "Result:", $"Barcodes detected: {obj.Count()}");
        }

        private void HandleDidStartScanning()
        {
            
        }

        private class BarcodeDetectionDelegate : SBSDKBarcodeScanAndCountViewControllerDelegate
        {
            private UINavigationController navigationController;
            private Action _didStartScanningAction;
            private Action<SBSDKBarcodeScannerResult[]> _didDetectBarcodesAction;

            public BarcodeDetectionDelegate(
                UINavigationController navigationController,
                Action didStartScanningAction,
                Action<SBSDKBarcodeScannerResult[]> didDetectBarcodesAction)
            {
                this.navigationController = navigationController;
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
        }
    }
}

