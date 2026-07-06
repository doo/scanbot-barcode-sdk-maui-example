using _Microsoft.Android.Resource.Designer;
using Android.Graphics;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.Core.View;
using IO.Scanbot.Sdk.Image;
using Java.Util;
using ScanbotSDK.Droid.Helpers;

namespace BarcodeSDK.NET.Droid.Activities;

public class BaseResultActivity<TNativeBarcodeResult> : AppCompatActivity, IOnApplyWindowInsetsListener where TNativeBarcodeResult : global::Java.Lang.Object, global::Android.OS.IParcelable
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(ResourceConstant.Layout.barcode_result);
        AndroidUtils.ApplyEdgeToEdge(FindViewById(ResourceConstant.Id.container), this);
        
        SetupToolbar();
        DisplayBarcodeResult();
    }

    private void SetupToolbar()
    {
        var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(ResourceConstant.Id.toolbar);
        SetSupportActionBar(toolbar);
    }
    
    protected virtual BaseBarcodeResult<TNativeBarcodeResult> DisplayBarcodeResult()
    {
        var barcodeResult = new BaseBarcodeResult<TNativeBarcodeResult>().FromBundle(Intent?.GetBundleExtra("BarcodeResult"));

        if (barcodeResult.ScannedImageUuid != null)
        {
            ShowSnapImage(barcodeResult.ScannedImageUuid);
        }

        return barcodeResult;
    }

    protected void ShowSnapImage(UUID imageUuid)
    {
        using var imageRef = new ImageRef(imageUuid, true);
        AddImageView().SetImageBitmap(imageRef.ToBitmap().Get<Bitmap>());  
    } 

    private ImageView AddImageView()
    {
        var items = FindViewById<LinearLayout>(ResourceConstant.Id.recognisedItems);
        var view = LayoutInflater.Inflate(ResourceConstant.Layout.snap_image_item, items, false);
        items?.AddView(view);
        return view?.FindViewById<ImageView>(ResourceConstant.Id.snapImage);
    }
    
    public WindowInsetsCompat OnApplyWindowInsets(View v, WindowInsetsCompat windowInsets)
    {
        return AndroidUtils.ApplyWindowInsets(v, windowInsets);
    }
}