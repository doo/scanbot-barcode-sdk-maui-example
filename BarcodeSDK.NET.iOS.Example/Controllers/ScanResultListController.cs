using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS
{
    public class ScanResultListController : BaseViewController
    {
        private UIImage scannedPage;

        private SBSDKBarcodeItem[] items;

        public ScanResultListController(SBSDKBarcodeItem[] list, UIImage scannedPage = null)
        {
            if (list == null || list.Length <= 0) return;
            this.scannedPage = scannedPage ?? list.First().SourceImage?.ToUIImage();
            items = list;
        }
        
        public ScanResultListController(SBSDKUI2BarcodeScannerUIItem[] list, UIImage scannedPage = null)
        {
            if (list == null || list.Length <= 0) return;
            this.scannedPage = scannedPage ?? list.First().Barcode.SourceImage?.ToUIImage();
            items = list.Select(item => item.Barcode).ToArray();
        }

        public ScanResultListView ContentView { get; set; }
        public override void ViewDidLoad()
        {
            PageTitle = "Barcode Results";
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
            if (sender is SBSDKBarcodeItem barcode)
            {
                var controller = new BarcodeDetailsController(barcode);
                NavigationController?.PushViewController(controller, true);
            }
        }
    }
}
