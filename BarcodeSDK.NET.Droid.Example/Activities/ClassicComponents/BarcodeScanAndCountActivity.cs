using System.Text;
using Android;
using Android.Content.PM;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using AndroidX.Core.View;
using IO.Scanbot.Sdk.Barcode;
using IO.Scanbot.Sdk.Barcode.UI;
using IO.Scanbot.Sdk.Barcode_scanner;

namespace BarcodeSDK.NET.Droid.Activities
{
    [Activity(Theme = "@style/AppTheme")]
    public class BarcodeScanAndCountActivity : AppCompatActivity, IOnApplyWindowInsetsListener
    {
        private BarcodeScanAndCountView scanAndCountView;
        private TextView resultView;
        private Button startScanningButton;
        private Button continueScanningButton;

        private const int RequestPermissionCode = 200;
        private static readonly string[] Permissions = [ Manifest.Permission.Camera ];

        private bool flashEnabled = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.barcode_scan_and_count_activity);
            AndroidUtils.ApplyEdgeToEdge(FindViewById(Resource.Id.container), this);

            var barcodeScanner = new ScanbotBarcodeScannerSDK(this).CreateBarcodeScanner();
            var barcodeFormatConfig = new BarcodeFormatCommonConfiguration { Formats = BarcodeTypes.Instance.AcceptedTypes };
            var barcodeScannerConfigs = new BarcodeScannerConfiguration
            {
                BarcodeFormatConfigurations = [barcodeFormatConfig],
                ExtractedDocumentFormats = BarcodeDocumentFormats.All
            };

            barcodeScanner.SetConfiguration(barcodeScannerConfigs);

            resultView = FindViewById<TextView>(Resource.Id.result);
            resultView.Visibility = Android.Views.ViewStates.Gone;

            scanAndCountView = FindViewById<BarcodeScanAndCountView>(Resource.Id.camera);
            scanAndCountView.InitDetectionBehavior(barcodeScanner: barcodeScanner,
                scannerViewCallbacks: (onCameraOpen: OnCameraOpen,
                    onLicenseError: OnLicenseError,
                    onScanAndCountFinished: OnScanAndCountFinished,
                    onScanAndCountStarted: OnScanAndCountStarted)
            );

            // scanAndCountView.CounterOverlayController.SetBarcodeAppearanceDelegate(
            //         getPolygonStyle: (defaultStyle, _) => defaultStyle.Copy(
            //             fillHighlightedColor: Color.Black,
            //             fillColor: Color.Yellow,
            //             strokeColor: Color.AliceBlue,
            //             strokeHighlightedColor: Color.Orange
            //     ));

            FindViewById<Button>(Resource.Id.flash).Click += delegate
            {
                flashEnabled = !flashEnabled;
                scanAndCountView.ViewController.UseFlash(flashEnabled);
            };

            startScanningButton = FindViewById<Button>(Resource.Id.startScanning);
            startScanningButton.Click += delegate
            {
                scanAndCountView.ViewController.ScanAndCount();

                startScanningButton.Visibility = Android.Views.ViewStates.Gone;
                continueScanningButton.Visibility = Android.Views.ViewStates.Visible;
            };

            continueScanningButton = FindViewById<Button>(Resource.Id.continueScanning);
            continueScanningButton.Click += delegate
            {
                scanAndCountView.ViewController.ContinueScanning();

                startScanningButton.Visibility = Android.Views.ViewStates.Visible;
                continueScanningButton.Visibility = Android.Views.ViewStates.Gone;
            };
        }

        private void HandleBarcodeSnanningResult(IDictionary<BarcodeItem, Java.Lang.Integer> barcodes)
        {
            var sb = new StringBuilder();

            foreach (var barcode in barcodes)
            {
                sb.Append($"{barcode.Key.Text} - {barcode.Value} \n");
            }

            resultView.Text = sb.ToString();
            resultView.Visibility = Android.Views.ViewStates.Visible;
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
            HandleBarcodeSnanningResult(scanAndCountView.CountedBarcodes);
        }

        public void OnScanAndCountStarted()
        {
            // Handler On scan started
        }
        
        public WindowInsetsCompat OnApplyWindowInsets(View v, WindowInsetsCompat windowInsets)
        {
            return AndroidUtils.ApplyWindowInsets(v, windowInsets);
        }
    }
}