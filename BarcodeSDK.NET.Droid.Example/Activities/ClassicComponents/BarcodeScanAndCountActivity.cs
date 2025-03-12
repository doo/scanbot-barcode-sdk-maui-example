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
using IO.Scanbot.Sdk.Camera;

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

            var barcodeScanner = new ScanbotBarcodeScannerSDK(this).CreateBarcodeScanner();
            var barcodeFormatConfig = new BarcodeFormatCommonConfiguration { Formats = BarcodeFormats.All };
            var barcodeScannerConfigs = new BarcodeScannerConfiguration
            {
                BarcodeFormatConfigurations = [barcodeFormatConfig],
                ExtractedDocumentFormats = BarcodeDocumentFormats.All
            };
                
            barcodeScanner.SetConfiguration(barcodeScannerConfigs);

            resultView = FindViewById<TextView>(Resource.Id.result);
            resultView.Visibility = Android.Views.ViewStates.Gone;

            scanAndCountView = FindViewById<BarcodeScanAndCountView>(Resource.Id.camera);
            scanAndCountView.InitCamera();

            var del = new BarcodeScanCountViewDelegate(scanAndCountView);
            scanAndCountView.InitDetectionBehavior(
                barcodeScanner, del.delegateHandler);

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
                sb.Append($"{barcode.Key.UpcEanExtension} - {barcode.Value} \n");
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

        private class BarcodeScanCountViewDelegate(BarcodeScanAndCountView scanAndCountView) 
        {   
            public (Action onCameraOpen, 
                Action onLicenseError, 
                Action<IList<BarcodeItem>> onScanAndCountFinished,
                Action onScanAndCountStarted) delegateHandler { get; set; }
            
            public void OnCameraOpen()
            {
                delegateHandler.onCameraOpen?.Invoke();
            }

            public void OnLicenseError()
            {
                delegateHandler.onLicenseError?.Invoke();
            }

            public void OnScanAndCountFinished(IList<BarcodeItem> barcodes)
            {
                delegateHandler.onScanAndCountFinished?.Invoke(scanAndCountView.CountedBarcodes.Select(item => item.Key).ToList());
            }

            public void OnScanAndCountStarted()
            {
                delegateHandler.onScanAndCountStarted?.Invoke();
            }
        }
    }
}
