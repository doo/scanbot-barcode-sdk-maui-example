using System;
using System.Collections.Generic;
using System.Linq;
using ScanbotBarcodeSDK.iOS;
using UIKit;

namespace BarcodeScannerExample.iOS
{
    public class ScanResultListController : UIViewController
    {
        public UIImage ScannedPage { get; set; }

        public List<SBSDKBarcodeScannerResult> Items { get; set; }

        public ScanResultListController(UIImage scannedPage, SBSDKBarcodeScannerResult[] list)
        {
            ScannedPage = scannedPage;
            Items = list.ToList();
        }

        public ScanResultListView ContentView { get; set; }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ContentView = new ScanResultListView();
            View = ContentView;

            ContentView.Source.Items = Items;

            if (Items.Count > 0)
            {
                ContentView.TableView.ReloadData();
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            ContentView.Source.ItemClick += RowSelected;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            ContentView.Source.ItemClick -= RowSelected;
        }

        private void RowSelected(object sender, EventArgs e)
        {
            var barcode = (SBSDKBarcodeScannerResult)sender;
            var controller = new BarcodeDetailsController(barcode);
            NavigationController.PushViewController(controller, true);
        }
    }
}
