using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS
{
    public class ScanResultListController : BaseViewController
    {
        private UIImage barcodeImageResult;

        private SBSDKBarcodeItem[] items;
        
        public ScanResultListView ContentView { get; set; }

        public ScanResultListController(SBSDKBarcodeItem[] list)
        {
            if (list == null || list.Length <= 0) return;
            barcodeImageResult = list.First().SourceImage?.ToUIImageAndReturnError(out _);
            items = list;
        }
        
        public ScanResultListController(SBSDKUI2BarcodeScannerUIItem[] list)
        {
            if (list == null || list.Length <= 0) return;
            barcodeImageResult = list.First().Barcode.SourceImage?.ToUIImageAndReturnError(out _);
            items = list.Select(item => item.Barcode).ToArray();
        }
        
        public override void ViewDidLoad()
        {
            PageTitle = "Barcode Results";
            base.ViewDidLoad();

            ContentView = new ScanResultListView(items);
            ContentView.ItemClick += RowSelected;

            View = ContentView;
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
