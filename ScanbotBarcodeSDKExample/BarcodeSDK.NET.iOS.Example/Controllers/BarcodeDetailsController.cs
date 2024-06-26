using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS
{
    public class BarcodeDetailsController : UIViewController
    {
        private SBSDKBarcodeScannerResult barcode;

        public BarcodeDetailsController(SBSDKBarcodeScannerResult barcode)
        {
            this.barcode = barcode;

            Title = "DETAILS";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View = new BarcodeDetailsView(barcode);
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
