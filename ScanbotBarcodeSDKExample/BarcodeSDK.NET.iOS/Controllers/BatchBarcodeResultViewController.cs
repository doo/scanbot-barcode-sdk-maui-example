using System;
using System.Collections.Generic;
using Foundation;
using ScanbotBarcodeSDK.iOS;
using UIKit;
using static System.Net.Mime.MediaTypeNames;

namespace BarcodeScannerExample.iOS
{
    /// <summary>
    /// Barcode Source Interaction.
    /// </summary>
    interface IBatchBarcodeSourceInteraction
    {
        /// <summary>
        /// Barcode result list.
        /// </summary>
        List<SBSDKUIBarcodeMappedResult> BarcodeResultList { get; }
    }

    public class BatchBarcodeResultViewController : UIViewController, IBatchBarcodeSourceInteraction
    {
        private List<SBSDKUIBarcodeMappedResult> _barcodeResultList;
        public List<SBSDKUIBarcodeMappedResult> BarcodeResultList => _barcodeResultList;

        public BatchBarcodeResultViewController()
        {
        }

        /// <summary>
        /// Populated from the previous page.
        /// </summary>
        /// <param name="barcodeResultList"></param>
        internal void NavigateData(List<SBSDKUIBarcodeMappedResult> barcodeResultList)
        {
            this._barcodeResultList = barcodeResultList;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            SetUpView();
        }

        /// <summary>
        /// Set up the view.
        /// </summary>
        private void SetUpView()
        {
            var tableView = new UITableView();
            tableView.Source = new BatchBarcodeResultSource(this);
            tableView.TableFooterView = new UIView();

            View.AddSubview(tableView);
            tableView.TranslatesAutoresizingMaskIntoConstraints = false;
            var leftConstraint = NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1, 0);
            var rightConstraint = NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View, NSLayoutAttribute.Right, 1, 0);
            var topConstraint = NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View, NSLayoutAttribute.Top, 1, 0);
            var bottomContraint = NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View, NSLayoutAttribute.Bottom, 1, 0);

            View.AddConstraints(new NSLayoutConstraint[] { leftConstraint, topConstraint, rightConstraint, bottomContraint });

        }
    }

    /// <summary>
    /// Batch Barcode TableViewSource class
    /// </summary>
    internal class BatchBarcodeResultSource : UITableViewSource
    {
        IBatchBarcodeSourceInteraction _interaction;
        private BatchBarcodeResultViewController batchBarcodeResultViewController;

        /// <summary>
        /// Source Constructor
        /// </summary>
        /// <param name="interaction"></param>
        public BatchBarcodeResultSource(IBatchBarcodeSourceInteraction interaction)
        {
            this._interaction = interaction;
        }

        /// <summary>
        ///  Get the Cell.
        /// </summary>
        /// <param name="tableView"></param>
        /// <param name="indexPath"></param>
        /// <returns></returns>
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (ScanResultCell)tableView.DequeueReusableCell(ScanResultCell.Identifier);
            var item = GetItem(indexPath.Row);

            if (cell == null)
            {
                cell = new ScanResultCell();
            }

            if (item?.Barcode != null)
            {
                cell.Update(item.Barcode);
            }

            return cell;
        }

        /// <summary>
        /// NUmber of rows
        /// </summary>
        /// <param name="tableview"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _interaction?.BarcodeResultList?.Count ?? 0;
        }

        /// <summary>
        /// Get the selected item from list
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private SBSDKUIBarcodeMappedResult GetItem(int row)
        {
            if (_interaction?.BarcodeResultList?.Count > row)
            {
                return _interaction.BarcodeResultList[row];
            }
            return null;
        }
    }
}


