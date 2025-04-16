using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS
{
    public class ScanResultListView : UIView
    {
        private UITableView tableView;
        private ScanResultListSource listSource;

        public ScanResultListView(SBSDKBarcodeItem[] items)
        {
            tableView = new UITableView();
            tableView.RegisterClassForCellReuse(typeof(ScanResultCell), ScanResultCell.Identifier);
            tableView.Source = listSource = new ScanResultListSource(items);

            AddSubview(tableView);
        }

        public event EventHandler<EventArgs> ItemClick
        {
            add
            {
                listSource.itemClick += value;
            }
            remove
            {
                listSource.itemClick -= value;
            }
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            tableView.Frame = Bounds;
        }

        private class ScanResultListSource : UITableViewSource
        {
            internal EventHandler<EventArgs> itemClick;
            private SBSDKBarcodeItem[] items;

            public ScanResultListSource(SBSDKBarcodeItem[] items)
            {
                this.items = items;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = (ScanResultCell)tableView.DequeueReusableCell(ScanResultCell.Identifier);

                if (cell == null)
                {
                    cell = new ScanResultCell();
                }

                cell.Update(items[indexPath.Row]);

                return cell;
            }

            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                itemClick?.Invoke(items[indexPath.Row], new EventArgs());
            }

            public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
            {
                return 100;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return items.Length;
            }
        }
    }
}
