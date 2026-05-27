using _Microsoft.Android.Resource.Designer;
using Android;
using Android.Content.PM;
using Android.Graphics;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using AndroidX.Core.View;
using IO.Scanbot.Common;
using IO.Scanbot.Sdk.Barcode;
using IO.Scanbot.Sdk.Barcode.UI;
using IO.Scanbot.Sdk.Barcode_scanner;
using IO.Scanbot.Sdk.Camera;
using IO.Scanbot.Sdk.Image;
using ScanbotSDK.Droid.Helpers;
using Intent = Android.Content.Intent;

namespace BarcodeSDK.NET.Droid.Activities;

[Activity(Theme = "@style/AppTheme")]
public class BarcodeClassicComponentActivity : AppCompatActivity, IOnApplyWindowInsetsListener
{
    private BarcodeScannerView _barcodeScannerView;
    private ImageView _resultView;

    private const int RequestPermissionCode = 200;
    private static readonly string[] Permissions = [Manifest.Permission.Camera];

    private bool _flashEnabled;
    private readonly bool _selectionOverlayEnabled = true;
    private readonly bool _autoSelectionEnabled = false;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        SupportRequestWindowFeature(WindowCompat.FeatureActionBarOverlay);
        base.OnCreate(savedInstanceState);

        SetContentView(ResourceConstant.Layout.barcode_classic_activity);
        AndroidUtils.ApplyEdgeToEdge(FindViewById(ResourceConstant.Id.container), this);

        var barcodeFormatConfig = new BarcodeFormatCommonConfiguration
        {
            Formats = BarcodeTypes.Instance.AcceptedTypes
        };
            
        var barcodeScannerConfigs = new BarcodeScannerConfiguration
        {
            BarcodeFormatConfigurations = [barcodeFormatConfig],
            ExtractedDocumentFormats = BarcodeDocumentFormats.All,
            ReturnBarcodeImage = true
        };

        var barcodeScannerResult = new ScanbotBarcodeScannerSDK(this).CreateBarcodeScanner(barcodeScannerConfigs);
        var barcodeScanner = barcodeScannerResult.GetOrThrow<IBarcodeScanner>();

        _barcodeScannerView = FindViewById<BarcodeScannerView>(ResourceConstant.Id.camera);
        _barcodeScannerView?.InitCamera();
        _barcodeScannerView?.InitScanningBehavior(barcodeScanner: barcodeScanner,
            onBarcodeResult: OnBarcodeResult,
            scannerViewCallbacks: (
                onCameraOpen: OnCameraOpened,
                onPictureTaken: OnPictureTaken,
                onSelectionOverlayBarcodeClicked: OnSelectionOverlayBarcodeClicked
            ));

        _barcodeScannerView?.SelectionOverlayController.SetEnabled(_selectionOverlayEnabled);
        _barcodeScannerView?.SelectionOverlayController.SetBarcodeAppearanceDelegate(
            (
                // Set custom polygon style based on the barcode item
                getPolygonStyle: (defaultStyle, _) => defaultStyle.Copy(
                    fillColor: Color.Yellow,
                    strokeColor: Color.Yellow
                ),
                // Set custom text style based on the barcode item
                getTextViewStyle: (defaultStyle, _) => defaultStyle.Copy(
                    textColor: Color.Yellow,
                    textContainerColor: Color.Black
                ),
                // Set custom override based on the barcode item
                getOverrideText: (defaultText, _) => defaultText ?? "Override Text"
            ));

        _resultView = FindViewById<ImageView>(ResourceConstant.Id.result);
        var flashButton = FindViewById<Button>(ResourceConstant.Id.flash);
        flashButton!.Click += delegate
        {
            _flashEnabled = !_flashEnabled;
            _barcodeScannerView.ViewController.UseFlash(_flashEnabled);
        };
    }

    private void OnSelectionOverlayBarcodeClicked(BarcodeItem e)
    {
        var intent = new Intent(this, typeof(BarcodeResultActivity));
        var result = new BaseBarcodeResult<BarcodeScannerResult>(
            new BarcodeScannerResult(new List<BarcodeItem> { e },
                false));
        intent.PutExtra(("BarcodeResult"), result.ToBundle());
        StartActivity(intent);
        Finish();
    }

    private bool OnBarcodeResult(IResult result, FrameHandler.Frame _)
    {
        if (!MainActivity.Sdk.LicenseInfo.IsValid)
        {
            return false;
        }
        
        if (_selectionOverlayEnabled && !_autoSelectionEnabled) return false;

        // extracts the barcode result from the result wrapper object.
        var barcodeResult = result?.Get<BarcodeScannerResult>();
        if (barcodeResult == null) return false;

        var intent = new Intent(this, typeof(BarcodeResultActivity));
        intent.PutExtra("BarcodeResult", new BaseBarcodeResult<BarcodeScannerResult>(barcodeResult).ToBundle());
        StartActivity(intent);
        Finish();
        return false;
    }

    protected override void OnResume()
    {
        base.OnResume();

        // Additional logic
        var status = ContextCompat.CheckSelfPermission(this, Permissions[0]);
        if (status != Permission.Granted)
        {
            ActivityCompat.RequestPermissions(this, Permissions, RequestPermissionCode);
        }
    }

    private void OnCameraOpened()
    {
        _barcodeScannerView.PostDelayed(() =>
        {
            _barcodeScannerView.ViewController.UseFlash(_flashEnabled);
            _barcodeScannerView.ViewController.ContinuousFocus();
        }, 300);
    }

    private void OnPictureTaken(ImageRef image, CaptureInfo captureInfo)
    {
        if (!MainActivity.Sdk.LicenseInfo.IsValid)
        {
            return;
        }

        var imageBytes = image.ToArray<byte>();
        var bitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);

        if (bitmap == null)
        {
            return;
        }

        var matrix = new Matrix();
        matrix.SetRotate(captureInfo.ImageOrientation, (float)bitmap.Width / 2, (float)bitmap.Height / 2);

        var result = Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, false);

        _resultView.Post(() =>
        {
            _resultView.SetImageBitmap(result);
            _barcodeScannerView.ViewController.ContinuousFocus();
            _barcodeScannerView.ViewController.StartPreview();
        });
    }

    public WindowInsetsCompat OnApplyWindowInsets(View v, WindowInsetsCompat windowInsets)
    {
        return AndroidUtils.ApplyWindowInsets(v, windowInsets);
    }
}
