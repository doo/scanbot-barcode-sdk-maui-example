using Android.Graphics;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.Core.View;

namespace BarcodeSDK.NET.Droid.Activities;

public class BaseResultActivity<TNativeBarcodeResult> : AppCompatActivity, IOnApplyWindowInsetsListener where TNativeBarcodeResult : global::Java.Lang.Object, global::Android.OS.IParcelable
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.barcode_result);
        AndroidUtils.ApplyEdgeToEdge(FindViewById(Resource.Id.container), this);
        
        SetupToolbar();
        DisplayBarcodeResult();
    }
    
    protected void SetupToolbar()
    {
        var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
        SetSupportActionBar(toolbar);
    }
    
    protected virtual BaseBarcodeResult<TNativeBarcodeResult> DisplayBarcodeResult()
    {
        var barcodeResult = new BaseBarcodeResult<TNativeBarcodeResult>().FromBundle(Intent?.GetBundleExtra("BarcodeResult"));

        if (barcodeResult.ResultBitmap != null)
        {
            ShowSnapImage(barcodeResult);
        }

        return barcodeResult;
    }
    
    protected void ShowSnapImage(string path) => AddImageView().SetImageURI(Android.Net.Uri.Parse(path));

    protected void ShowSnapImage(BaseBarcodeResult<TNativeBarcodeResult> barcodeResult)
    {
        Bitmap scaled = Bitmap.CreateScaledBitmap(barcodeResult.ResultBitmap, 200, 200, true);
        AddImageView().SetImageBitmap(scaled);
    }
    
    protected ImageView AddImageView()
    {
        var items = FindViewById<LinearLayout>(Resource.Id.recognisedItems);
        var view = LayoutInflater.Inflate(Resource.Layout.snap_image_item, items, false);
        items?.AddView(view);
        return view?.FindViewById<ImageView>(Resource.Id.snapImage);
    }
    
    public WindowInsetsCompat OnApplyWindowInsets(View v, WindowInsetsCompat windowInsets)
    {
        return AndroidUtils.ApplyWindowInsets(v, windowInsets);
    }
}