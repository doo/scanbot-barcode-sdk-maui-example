using _Microsoft.Android.Resource.Designer;
using Android.Content;
using Android.Graphics;
using Android.Views;
using IO.Scanbot.Sdk.Barcode;
using ScanbotSDK.Droid.Helpers;

namespace BarcodeSDK.NET.Droid.Activities;
[Activity(Theme = "@style/AppTheme")]
public class BarcodeResultActivity : BaseResultActivity<BarcodeScannerResult>
{
    protected override BaseBarcodeResult<BarcodeScannerResult> DisplayBarcodeResult()
    {
        var barcodeResult = base.DisplayBarcodeResult();
        ShowBarcodeResult(barcodeResult.ScanResult);

        return barcodeResult;
    }
        
    private void ShowBarcodeResult(BarcodeScannerResult result)
    {
        if (result == null)
            return;
            
        var parent = FindViewById<LinearLayout>(ResourceConstant.Id.recognisedItems);

        foreach (var item in result.Barcodes)
        {
            View child = LayoutInflater.Inflate(ResourceConstant.Layout.barcode_item, parent, false);
            InitItemData(child, item);
            parent?.AddView(child);
        }
    }
        
    private void InitItemData(View child, BarcodeItem item)
    {
        var image = child.FindViewById<ImageView>(ResourceConstant.Id.image);
        var barFormat = child.FindViewById<TextView>(ResourceConstant.Id.barcodeFormat)!;
        var docText = child.FindViewById<TextView>(ResourceConstant.Id.docText)!;

        if (item.SourceImage != null)
        {
            image?.SetImageBitmap(item.SourceImage.ToBitmap().Get<Bitmap>());
        }

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