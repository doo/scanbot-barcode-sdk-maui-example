using System;
using System.Collections.Generic;
using Foundation;
using ScanbotBarcodeSDK.iOS;
using UIKit;

namespace BarcodeScannerExample.iOS
{
    public class ScanResultListView : UIView
    {
        public UITableView TableView { get; private set; }

        public ScanResultListSource Source { get; private set; }

        public ScanResultListView()
        {
            TableView = new UITableView();
            TableView.RegisterClassForCellReuse(typeof(ScanResultCell), ScanResultCell.Identifier);
            AddSubview(TableView);

            Source = new ScanResultListSource();
            TableView.Source = Source;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            TableView.Frame = Bounds;
        }
    }

    public class ScanResultListSource : UITableViewSource
    {
        public EventHandler<EventArgs> ItemClick;

        public List<SBSDKBarcodeScannerResult> Items { get; set; }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (ScanResultCell)tableView.DequeueReusableCell(ScanResultCell.Identifier);

            if (cell == null)
            {
                cell = new ScanResultCell();
            }

            cell.Update(Items[indexPath.Row]);

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            ItemClick?.Invoke(Items[indexPath.Row], new EventArgs());
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 100;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return Items.Count;
        }
    }
}
