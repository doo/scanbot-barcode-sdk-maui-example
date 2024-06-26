using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS
{
    public class ScanResultListController : UIViewController
    {
        private UIImage scannedPage;

        private SBSDKBarcodeScannerResult[] items;

        public ScanResultListController(UIImage scannedPage, SBSDKBarcodeScannerResult[] list)
        {
            this.scannedPage = scannedPage;
            items = list;
        }

        public ScanResultListView ContentView { get; set; }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View = ContentView = new ScanResultListView(items);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            ContentView.ItemClick += RowSelected;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            ContentView.ItemClick -= RowSelected;
        }

        private void RowSelected(object sender, EventArgs e)
        {
            var barcode = (SBSDKBarcodeScannerResult)sender;
            var controller = new BarcodeDetailsController(barcode);
            NavigationController.PushViewController(controller, true);
        }
    }
}
