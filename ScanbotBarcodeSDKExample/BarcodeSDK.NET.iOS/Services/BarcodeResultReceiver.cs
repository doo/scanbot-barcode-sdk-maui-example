using ScanbotBarcodeSDK.iOS;

namespace BarcodeSDK.NET.iOS
{
    public class ScannerEventArgs : EventArgs
    {
        public List<SBSDKBarcodeScannerResult> Codes { get; private set; }

        public UIImage BarcodeImage { get; private set; }

        public bool IsEmpty { get => Codes.Count == 0; }

        public UIViewController Controller { get; set; }

        public ScannerEventArgs(List<SBSDKBarcodeScannerResult> codes, UIImage image)
        {
            Codes = codes;
            BarcodeImage = image;
        }
    }

    public class ClassicScannerReceiver : SBSDKBarcodeScannerViewControllerDelegate
    {
        public EventHandler<ScannerEventArgs> ResultReceived;

        public override void DidDetectBarcodes(
            SBSDKBarcodeScannerViewController controller, SBSDKBarcodeScannerResult[] codes)
        {
            var barcodeImage = codes?.First()?.SourceImage;
            ResultReceived?.Invoke(this, new ScannerEventArgs(codes.ToList(), barcodeImage));
        }

        public override bool ShouldDetectBarcodes(SBSDKBarcodeScannerViewController controller)
        {
            return true;
        }
    }

    public class BarcodeResultReceiver : SBSDKUIBarcodeScannerViewControllerDelegate
    {
        public EventHandler<ScannerEventArgs> ResultsReceived;
        ScannerEventArgs args;

        public override void DidDetect(SBSDKUIBarcodeScannerViewController viewController, SBSDKBarcodeScannerResult[] barcodeResults)
        {
            Invoke(viewController, barcodeResults);
        }

        void Invoke(SBSDKUIBarcodeScannerViewController viewController, SBSDKBarcodeScannerResult[] barcodeResults)
        {
            if (barcodeResults != null)
            {
                var barcodeImage = barcodeResults?.First()?.SourceImage;
                args = new ScannerEventArgs(barcodeResults.ToList(), barcodeImage);
                args.Controller = viewController;
                ResultsReceived?.Invoke(this, args);
            }
        }
    }
}
