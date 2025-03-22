using Android.Content;
using Android.Graphics;
using Android.Views;
using IO.Scanbot.Sdk.Barcode;
using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;

namespace BarcodeSDK.NET.Droid.Activities
{
    [Activity(Theme = "@style/AppTheme")]
    public class BarcodeResultActivity : BaseResultActivity<BarcodeScannerResult>
    {
        protected override BaseBarcodeResult<BarcodeScannerResult> DisplayBarcodeResult()
        {
            var barcodeResult = base.DisplayBarcodeResult();
            ShowBarcodeResult(barcodeResult.ScanningResult);

            return barcodeResult;
        }
        
        private void ShowBarcodeResult(BarcodeScannerResult result)
        {
            var parent = FindViewById<LinearLayout>(Resource.Id.recognisedItems);

            if (result == null)
                return;

            foreach (var item in result.Barcodes)
            {
                View child = LayoutInflater.Inflate(Resource.Layout.barcode_item, parent, false);
                InitItemData(child, item);
                parent.AddView(child);
            }
        }
        
        private void InitItemData(View child, BarcodeItem item)
        {
            var image = child.FindViewById<ImageView>(Resource.Id.image);
            var barFormat = child.FindViewById<TextView>(Resource.Id.barcodeFormat);
            var docText = child.FindViewById<TextView>(Resource.Id.docText);

            image.SetImageBitmap(item.SourceImage?.ToBitmap());
            barFormat.Text = "Format: " + item.Format?.Name();
            docText.Text = "Content: " + item.Text;

            child.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(DetailedItemDataActivity));
                intent.PutExtra("SelectedBarcodeItem", item);
                StartActivity(intent);
            };
        } 
    }
}
