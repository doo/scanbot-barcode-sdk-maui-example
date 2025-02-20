using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS
{
    public class BarcodeDetailsController : UIViewController
    {
        private readonly SBSDKBarcodeItem barcode;

        public BarcodeDetailsController(SBSDKBarcodeItem barcode)
        {
            this.barcode = barcode;

            Title = "DETAILS";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View = new BarcodeDetailsView(barcode);
        }
    }
}
