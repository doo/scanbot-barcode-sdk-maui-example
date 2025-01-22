using Android.Content;
using Android.Views;
using IO.Scanbot.Sdk.Barcode.Entity;

namespace BarcodeSDK.NET.Droid.Activities.V1
{
    [Activity(Theme = "@style/AppTheme")]
    public class BarcodeResultActivity : BaseResultActivity<BarcodeScanningResult>
    {
        protected override BaseBarcodeResult<BarcodeScanningResult> DisplayBarcodeResult()
        {
            var barcodeResult = base.DisplayBarcodeResult();
            ShowBarcodeResult(barcodeResult.ScanningResult);

            return barcodeResult;
        }
        
        private void ShowBarcodeResult(BarcodeScanningResult result)
        {
            var parent = FindViewById<LinearLayout>(Resource.Id.recognisedItems);

            if (result == null)
                return;

            foreach (var item in result.BarcodeItems)
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
            var docFormat = child.FindViewById<TextView>(Resource.Id.docFormat);
            var docText = child.FindViewById<TextView>(Resource.Id.docText);
            
            if (item.Image != null)
            {
                image.SetImageBitmap(item.Image);
            }

            barFormat.Text = "Format: " + item.BarcodeFormat.Name();
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
