using _Microsoft.Android.Resource.Designer;
using Android;
using Android.Content;
using Android.Graphics;
using AndroidX.AppCompat.App;
using AndroidX.Core.View;
using BarcodeSDK.NET.Droid.Activities;
using IO.Scanbot.Common;
using IO.Scanbot.Sdk.Barcode_scanner;
using IO.Scanbot.Sdk.Barcode;
using IO.Scanbot.Sdk.Barcode.UI;
using IO.Scanbot.Sdk.Camera;
using IO.Scanbot.Sdk.Image;
using ScanbotSDK.Droid.Helpers;

namespace BarcodeSDK.NET.Droid;

public class BarcodeItemMapping : AppCompatActivity
{
    private BarcodeScannerView _barcodeScannerView;
    private ImageView _resultView;

    private const int RequestPermissionCode = 200;
    private static readonly string[] Permissions = [Manifest.Permission.Camera];

    protected override void OnCreate(Bundle savedInstanceState)
    {
        SupportRequestWindowFeature(WindowCompat.FeatureActionBarOverlay);
        base.OnCreate(savedInstanceState);

        SetContentView(ResourceConstant.Layout.barcode_classic_activity);

        var barcodeFormatConfig = new BarcodeFormatCommonConfiguration
            { Formats = BarcodeTypes.Instance.AcceptedTypes };
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
        
        _barcodeScannerView?.SelectionOverlayController.SetEnabled(true);
        
        // Configure unique AR overlay style for each BarcodeItem object, individually.
        _barcodeScannerView?.SelectionOverlayController.SetBarcodeAppearanceDelegate(
            (
                // Set custom polygon style based on the barcode item
                getPolygonStyle: PolygonStyleForBarcodeItem,
                // Set custom text style based on the barcode item
                getTextViewStyle: TextStyleForBarcodeItem,
                // Set custom override based on the barcode item
                getOverrideText: OverrideTextForBarcodeItem
            ));

        _resultView = FindViewById<ImageView>(ResourceConstant.Id.result);
    }

    private string OverrideTextForBarcodeItem(string overrideText, BarcodeItem item)
    {
        // comparing a barcode format as an example
        return item.Format.Equals(BarcodeFormat.Aztec) ? "Aztec" : overrideText;
    }

    private BarcodePolygonsView.BarcodeTextViewStyle TextStyleForBarcodeItem(BarcodePolygonsView.BarcodeTextViewStyle defaultStyle, BarcodeItem item)
    {
        // comparing a barcode format as an example
        if (item.Format.Equals(BarcodeFormat.Aztec))
        {
            // Explore this object for more parameters
            return defaultStyle.Copy(
                textColor: Color.Yellow,
                textContainerColor: Color.Black);
        } 
        
        return defaultStyle;
    }

    private BarcodePolygonsView.BarcodePolygonStyle PolygonStyleForBarcodeItem(BarcodePolygonsView.BarcodePolygonStyle defaultStyle, BarcodeItem item)
    {
        // comparing a barcode format as an example
        if (item.Format.Equals(BarcodeFormat.Aztec))
        {
            // Explore this object for more parameters
            return defaultStyle.Copy(fillColor: Color.Transparent, strokeColor: Color.Yellow);
        };
        return defaultStyle;
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
       // In this example,
       // We only focus on AR overlay i.e., OnSelectionOverlayBarcodeClicked method
        return false;
    }

    private void OnCameraOpened()
    {
        _barcodeScannerView.ViewController.ContinuousFocus();
    }

    private void OnPictureTaken(ImageRef image, CaptureInfo captureInfo)
    {
        // In this example,
        // We only focus on AR overlay i.e., OnSelectionOverlayBarcodeClicked method
    }
}