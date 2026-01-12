using System.Text;
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
using ScanbotSDK.Droid.Helpers;

namespace BarcodeSDK.NET.Droid.Activities
{
    [Activity(Theme = "@style/AppTheme")]
    public class BarcodeScanAndCountActivity : AppCompatActivity, IOnApplyWindowInsetsListener, 
        BarcodePolygonsStaticView.IBarcodeItemViewBinder,
        BarcodePolygonsStaticView.IBarcodeItemViewFactory
    {
        private BarcodeScanAndCountView _scanAndCountView;
        private TextView _resultView;
        private Button _startScanningButton;
        private Button _continueScanningButton;

        private const int RequestPermissionCode = 200;
        private static readonly string[] Permissions = [Manifest.Permission.Camera];
        private bool _flashEnabled;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(ResourceConstant.Layout.barcode_scan_and_count_activity);
            AndroidUtils.ApplyEdgeToEdge(FindViewById(ResourceConstant.Id.container), this);

            var barcodeFormatConfig = new BarcodeFormatCommonConfiguration { Formats = BarcodeTypes.Instance.AcceptedTypes };
            var barcodeScannerConfigs = new BarcodeScannerConfiguration
            {
                BarcodeFormatConfigurations = [barcodeFormatConfig],
                ExtractedDocumentFormats = BarcodeDocumentFormats.All
            };
            
            var barcodeScannerResult = new ScanbotBarcodeScannerSDK(this).CreateBarcodeScanner(barcodeScannerConfigs);
            var barcodeScanner = ResultHelper.Get<IBarcodeScanner>(barcodeScannerResult);

            _resultView = FindViewById<TextView>(ResourceConstant.Id.result)!;
            _resultView.Visibility = ViewStates.Gone;

            _scanAndCountView = FindViewById<BarcodeScanAndCountView>(ResourceConstant.Id.camera)!;
            _scanAndCountView.InitScanningBehavior(barcodeScanner: barcodeScanner,
                scannerViewCallbacks: (onCameraOpen: OnCameraOpen,
                    onLicenseError: OnLicenseError,
                    onScanAndCountFinished: OnScanAndCountFinished,
                    onScanAndCountStarted: OnScanAndCountStarted,
                    onError: OnErrorHandler)
            );
            
            _scanAndCountView.CounterOverlayController.SetBarcodeItemViewBinder(this);
            // note: If you don't set this view. The above method 'SetBarcodeItemViewBinder(this)' will never receive callbacks.
            _scanAndCountView.CounterOverlayController.SetBarcodeItemViewFactory(this);

            // _scanAndCountView.CounterOverlayController.SetBarcodeAppearanceDelegate(
            //     getPolygonStyle: (defaultStyle, _) => defaultStyle.Copy(
            //         fillHighlightedColor: Color.Black,
            //         fillColor: Color.Yellow,
            //         strokeColor: Color.AliceBlue,
            //         strokeHighlightedColor: Color.Orange
            //     ));

            FindViewById<Button>(ResourceConstant.Id.flash)!.Click += delegate
            {
                _flashEnabled = !_flashEnabled;
                _scanAndCountView.ViewController.UseFlash(_flashEnabled);
            };

            _startScanningButton = FindViewById<Button>(ResourceConstant.Id.startScanning);
            _startScanningButton!.Click += delegate
            {
                _scanAndCountView.ViewController.ScanAndCount();

                _startScanningButton.Visibility = ViewStates.Gone;
                _continueScanningButton.Visibility = ViewStates.Visible;
            };

            _continueScanningButton = FindViewById<Button>(ResourceConstant.Id.continueScanning);
            _continueScanningButton!.Click += delegate
            {
                _scanAndCountView.ViewController.ContinueScanning();

                _startScanningButton.Visibility = ViewStates.Visible;
                _continueScanningButton.Visibility = ViewStates.Gone;
            };
        }

        private void HandleBarcodeScanningResult(IDictionary<BarcodeItem, Java.Lang.Integer> barcodes)
        {
            var sb = new StringBuilder();

            foreach (var barcode in barcodes)
            {
                sb.Append($"{barcode.Key.Text} - {barcode.Value} \n");
            }

            _resultView.Text = sb.ToString();
            _resultView.Visibility = ViewStates.Visible;
        }

        protected override void OnResume()
        {
            base.OnResume();

            var status = ContextCompat.CheckSelfPermission(this, Permissions[0]);
            if (status != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, Permissions, RequestPermissionCode);
            }
        }

        public void OnCameraOpen()
        {
            // Handler on camera open callback here.
        }

        public void OnLicenseError()
        {
            // Handler license error
        }

        public void OnScanAndCountFinished(IList<BarcodeItem> barcodes)
        {
            HandleBarcodeScanningResult(_scanAndCountView.CountedBarcodes);
        }

        public void OnScanAndCountStarted()
        {
            // Handler On scan started
        }

        private void OnErrorHandler(IResult.Failure obj)
        {
            Alert.ShowInfoDialog(this, "Alert", obj.Message);
        }

        public WindowInsetsCompat OnApplyWindowInsets(View v, WindowInsetsCompat windowInsets)
        {
            return AndroidUtils.ApplyWindowInsets(v, windowInsets);
        }

        private void HandleBarcodeItemViewBinder(Android.Views.View view, IO.Scanbot.Sdk.Barcode.BarcodeItem item, bool bindViewAction)
        {
            if (view is ImageView imageView) // access the ImageView set from the CreateView()
            {
                imageView.SetScaleType(ImageView.ScaleType.FitCenter);
                // Set your custom image here. 
                imageView.SetImageResource(_Microsoft.Android.Resource.Designer.Resource.Drawable.ic_scanbot_checkmark);
            }
        }

        public void BindView(View view, BarcodeItem barcodeItem, bool isBarcodeAccepted)
        {
            HandleBarcodeItemViewBinder(view, barcodeItem, isBarcodeAccepted);
        }

        public View CreateView()
        {
            // Pass the ImageView here. You may pass your required View.
            return new ImageView(this);
        }
    }
}