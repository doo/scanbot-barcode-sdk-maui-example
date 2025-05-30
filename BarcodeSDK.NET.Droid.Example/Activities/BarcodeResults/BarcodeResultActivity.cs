using Android.Content;
using Android.Views;
using IO.Scanbot.Sdk.Barcode;

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
            if (result == null)
                return;
            
            var parent = FindViewById<LinearLayout>(Resource.Id.recognisedItems);

            foreach (var item in result.Barcodes)
            {
                View child = LayoutInflater.Inflate(Resource.Layout.barcode_item, parent, false);
                InitItemData(child, item);
                parent?.AddView(child);
            }
        }
        
        private void InitItemData(View child, BarcodeItem item)
        {
            var image = child.FindViewById<ImageView>(Resource.Id.image);
            var barFormat = child.FindViewById<TextView>(Resource.Id.barcodeFormat);
            var docText = child.FindViewById<TextView>(Resource.Id.docText);

            image?.SetImageBitmap(item.SourceImage?.ToBitmap());
            barFormat.Text = "Format: " + item.Format.Name();
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
