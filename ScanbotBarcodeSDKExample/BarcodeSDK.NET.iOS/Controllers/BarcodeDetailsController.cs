using System;
using ScanbotBarcodeSDK.iOS;
using UIKit;

namespace BarcodeScannerExample.iOS
{
    public class BarcodeDetailsController : UIViewController
    {
        public BarcodeDetailsView ContentView { get; set; }

        public SBSDKBarcodeScannerResult Barcode { get; set; }

        public BarcodeDetailsController(SBSDKBarcodeScannerResult result)
        {
            Barcode = result;

            Title = "DETAILS";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ContentView = new BarcodeDetailsView(Barcode);
            View = ContentView;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }
    }
}
