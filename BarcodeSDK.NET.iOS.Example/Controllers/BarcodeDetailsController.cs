using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS
{
    public class BarcodeDetailsController : BaseViewController
    {
        private readonly SBSDKBarcodeItem barcode;

        public BarcodeDetailsController(SBSDKBarcodeItem barcode)
        {
            PageTitle = "Barcode Details";
            this.barcode = barcode;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View = new BarcodeDetailsView(barcode);
        }
    }
}
