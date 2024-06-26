using System.Text;
using Android;
using Android.Content.PM;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using IO.Scanbot.Sdk.Barcode;
using IO.Scanbot.Sdk.Barcode.Entity;
using IO.Scanbot.Sdk.Barcode.UI;
using IO.Scanbot.Sdk.Barcode_scanner;

namespace BarcodeSDK.NET.Droid.Activities
{
    [Activity(Theme = "@style/AppTheme")]
    public class BarcodeScanAndCountActivity : AppCompatActivity
    {
        private BarcodeScanAndCountView scanAndCountView;
        private TextView resultView;
        private Button _startScanningButton;
        private Button _continueScanningButton;

        private const int REQUEST_PERMISSION_CODE = 200;
        private static readonly string[] permissions = new string[] { Manifest.Permission.Camera };

        private bool flashEnabled = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.barcode_scan_and_count_activity);

            var barcodeDetector = new ScanbotBarcodeScannerSDK(this).CreateBarcodeDetector();
            barcodeDetector.ModifyConfig(detectorConfig =>
            {
                var defaultConfig = new BarcodeScannerAdditionalConfig();
                detectorConfig.SetBarcodeFormats(BarcodeTypes.Instance.AcceptedTypes);
                detectorConfig.SetAdditionalConfig(defaultConfig.Copy(codeDensity: BarcodeDensity.High));
                detectorConfig.SetEngineMode(EngineMode.NextGen);
                detectorConfig.SetSaveCameraPreviewFrame(false);
            });

            resultView = FindViewById<TextView>(Resource.Id.result);
            resultView.Visibility = Android.Views.ViewStates.Gone;

            scanAndCountView = FindViewById<BarcodeScanAndCountView>(Resource.Id.camera);
            scanAndCountView.InitCamera();
            scanAndCountView.InitDetectionBehavior(
                barcodeDetector,
                new BarcodeScanCountViewDelegate(
                    scanAndCountView,
                    barcodeSnanningResult: HandleBarcodeSnanningResult
                )
            );

            //scanAndCountView.CounterOverlayController.SetBarcodeAppearanceDelegate(
            //        getPolygonStyle: (defaultStyle, _) => defaultStyle.Copy(
            //            fillHighlightedColor: Color.Black,
            //            fillColor: Color.Yellow,
            //            strokeColor: Color.AliceBlue,
            //            strokeHighlightedColor: Color.Orange
            //    ));

            FindViewById<Button>(Resource.Id.flash).Click += delegate
            {
                flashEnabled = !flashEnabled;
                scanAndCountView.ViewController.UseFlash(flashEnabled);
            };

            _startScanningButton = FindViewById<Button>(Resource.Id.startScanning);
            _startScanningButton.Click += delegate
            {
                scanAndCountView.ViewController.ScanAndCount();

                _startScanningButton.Visibility = Android.Views.ViewStates.Gone;
                _continueScanningButton.Visibility = Android.Views.ViewStates.Visible;
            };

            _continueScanningButton = FindViewById<Button>(Resource.Id.continueScanning);
            _continueScanningButton.Click += delegate
            {
                scanAndCountView.ViewController.ContinueScanning();

                _startScanningButton.Visibility = Android.Views.ViewStates.Visible;
                _continueScanningButton.Visibility = Android.Views.ViewStates.Gone;
            };
        }

        private void HandleBarcodeSnanningResult(IDictionary<BarcodeItem, Java.Lang.Integer> barcodes)
        {
            var sb = new StringBuilder();

            foreach(var barcode in barcodes)
            {
                sb.Append($"{barcode.Key.TextWithExtension} - {barcode.Value} \n");
            }
            resultView.Text = sb.ToString();
            resultView.Visibility = Android.Views.ViewStates.Visible;
        }

        protected override void OnResume()
        {
            base.OnResume();

            var status = ContextCompat.CheckSelfPermission(this, permissions[0]);
            if (status != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, permissions, REQUEST_PERMISSION_CODE);
            }
        }

        private class BarcodeScanCountViewDelegate : Java.Lang.Object, IBarcodeScanCountViewCallback
        {
            private readonly BarcodeScanAndCountView _currentScanAndCountView;

            private readonly Action _licenseErrorHandler;
            private readonly Action<IDictionary<BarcodeItem, Java.Lang.Integer>> _barcodeScanningResult;
            private readonly Action _barcodeScanStarted;

            public BarcodeScanCountViewDelegate(
                BarcodeScanAndCountView currentScanAndCountView,
                Action licenseErrorHandler = null,
                Action<IDictionary<BarcodeItem, Java.Lang.Integer>> barcodeSnanningResult = null,
                Action barcodeScanStarted = null)
            {
                _currentScanAndCountView = currentScanAndCountView;
                _licenseErrorHandler = licenseErrorHandler;
                _barcodeScanningResult = barcodeSnanningResult;
                _barcodeScanStarted = barcodeScanStarted;
            }

            public void OnCameraOpen()
            {
                
            }

            public void OnLicenseError()
            {
                _licenseErrorHandler?.Invoke();
            }

            public void OnScanAndCountFinished(IList<BarcodeItem> barcodes)
            {
                _barcodeScanningResult?.Invoke(_currentScanAndCountView.CountedBarcodes);
            }

            public void OnScanAndCountStarted()
            {
                _barcodeScanStarted?.Invoke();
            }
        }
    }
}
