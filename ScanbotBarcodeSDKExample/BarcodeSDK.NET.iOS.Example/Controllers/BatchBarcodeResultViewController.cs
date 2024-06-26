using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS
{
    public class BatchBarcodeResultViewController : UIViewController
    {
        private SBSDKUIBarcodeMappedResult[] barcodeResults;
        
        public BatchBarcodeResultViewController(SBSDKUIBarcodeMappedResult[] barcodeResults)
        {
            this.barcodeResults = barcodeResults;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var tableView = new UITableView();
            tableView.Source = new BatchBarcodeResultSource(barcodeResults);
            tableView.TableFooterView = new UIView();

            View.AddSubview(tableView);
            tableView.TranslatesAutoresizingMaskIntoConstraints = false;
            var leftConstraint = NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1, 0);
            var rightConstraint = NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View, NSLayoutAttribute.Right, 1, 0);
            var topConstraint = NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View, NSLayoutAttribute.Top, 1, 0);
            var bottomContraint = NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View, NSLayoutAttribute.Bottom, 1, 0);

            View.AddConstraints(new NSLayoutConstraint[] { leftConstraint, topConstraint, rightConstraint, bottomContraint });
        }

        private class BatchBarcodeResultSource : UITableViewSource
        {
            private SBSDKUIBarcodeMappedResult[] barcodes;

            public BatchBarcodeResultSource(SBSDKUIBarcodeMappedResult[] barcodes)
            {
                this.barcodes = barcodes;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = (ScanResultCell)tableView.DequeueReusableCell(ScanResultCell.Identifier); 

                if (cell == null)
                {
                    cell = new ScanResultCell();
                }

                var item = GetItem(indexPath.Row);

                if (item?.Barcode != null)
                {
                    cell.Update(item.Barcode);
                }

                return cell;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return barcodes.Length;
            }

            private SBSDKUIBarcodeMappedResult GetItem(int row)
            {
                if (barcodes.Length > row)
                {
                    return barcodes[row];
                }
                return null;
            }
        }
    }
}


