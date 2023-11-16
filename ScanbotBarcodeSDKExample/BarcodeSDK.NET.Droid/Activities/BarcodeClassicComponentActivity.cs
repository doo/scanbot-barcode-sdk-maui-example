using Android;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using AndroidX.Core.View;
using IO.Scanbot.Sdk.Barcode;
using IO.Scanbot.Sdk.Barcode.Entity;
using IO.Scanbot.Sdk.Barcode.UI;
using IO.Scanbot.Sdk.Barcode_scanner;
using IO.Scanbot.Sdk.Camera;
using IO.Scanbot.Sdk.UI.Camera;

namespace BarcodeSDK.NET.Droid
{
    [Activity(Theme = "@style/AppTheme")]
    public class BarcodeClassicComponentActivity : AppCompatActivity
    {
        BarcodeScannerView barcodeScannerView;
        ImageView resultView;

        private const int REQUEST_PERMISSION_CODE = 200;
        private static readonly string[] permissions = new string[] { Manifest.Permission.Camera };

        bool flashEnabled = false;

        private static BarcodeScannerAdditionalConfig CreateAdditionalConfiguration(BarcodeDensity density)
        {
            var additionalDefaults = new BarcodeScannerAdditionalConfig();
            return new BarcodeScannerAdditionalConfig(
                minimumTextLength: additionalDefaults.MinimumTextLength,
                maximumTextLength: additionalDefaults.MaximumTextLength,
                minimum1DQuietZoneSize: additionalDefaults.Minimum1DQuietZoneSize,
                gs1DecodingEnabled: additionalDefaults.Gs1DecodingEnabled,
                msiPlesseyChecksumAlgorithms: additionalDefaults.MsiPlesseyChecksumAlgorithms,
                stripCheckDigits: additionalDefaults.StripCheckDigits,
                lowPowerMode: additionalDefaults.LowPowerMode,
                codeDensity: density);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SupportRequestWindowFeature(WindowCompat.FeatureActionBarOverlay);
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.barcode_classic_activity);

            barcodeScannerView = FindViewById<BarcodeScannerView>(Resource.Id.camera);
            resultView = FindViewById<ImageView>(Resource.Id.result);

            var SDK = new ScanbotBarcodeScannerSDK(this);
            var detector = SDK.CreateBarcodeDetector();

            var resultHandler = new BarcodeResultHandler();
            resultHandler.BarcodeScanned += OnBarcodeResult;

            var scannerViewCallback = new BarcodeScannerViewCallback();
            scannerViewCallback.CameraOpen += OnCameraOpened;
            scannerViewCallback.PictureTaken += OnPictureTaken;
            scannerViewCallback.SelectionOverlayBarcodeClicked += OnSelectionOverlayBarcodeClicked;

            barcodeScannerView.InitCamera(new CameraUiSettings(Intent.GetBooleanExtra("useCameraX", false)));
            BarcodeScannerViewWrapper.InitDetectionBehavior(barcodeScannerView, detector, resultHandler, scannerViewCallback);

            detector.ModifyConfig(Function1Impl<BarcodeScannerConfigBuilder>.From(detectorConfig => {
                detectorConfig.SetBarcodeFormats(BarcodeTypes.Instance.AcceptedTypes);
                detectorConfig.SetAdditionalConfig(CreateAdditionalConfiguration(BarcodeDensity.High));
                detectorConfig.SetEngineMode(EngineMode.NextGen);
                detectorConfig.SetSaveCameraPreviewFrame(false);
            }));

            // set to true to go to the results page for the first valid barcode scanned
            barcodeScannerView.ViewController.AutoSnappingEnabled = false;
            barcodeScannerView.ViewController.SetAutoSnappingSensitivity(1f);

            barcodeScannerView.SelectionOverlayController.SetEnabled(true);
            barcodeScannerView.SelectionOverlayController.SetTextFormat(BarcodeOverlayTextFormat.Code);
            barcodeScannerView.SelectionOverlayController.SetPolygonColor(Color.Yellow);
            barcodeScannerView.SelectionOverlayController.SetTextColor(Color.Yellow);
            barcodeScannerView.SelectionOverlayController.SetTextContainerColor(Color.Black);

            barcodeScannerView.SelectionOverlayController.SetPolygonHighlightedColor(Color.Red);
            barcodeScannerView.SelectionOverlayController.SetTextHighlightedColor(Color.Red);
            barcodeScannerView.SelectionOverlayController.SetTextContainerHighlightedColor(Color.Yellow);

            FindViewById<Button>(Resource.Id.flash).Click += delegate
            {
                flashEnabled = !flashEnabled;
                barcodeScannerView.ViewController.UseFlash(flashEnabled);
            };
        }

        private void OnSelectionOverlayBarcodeClicked(BarcodeItem e)
        {
            var intent = new Intent(this, typeof(BarcodeResultActivity));
            var result = new BarcodeResult(new BarcodeScanningResult(new List<BarcodeItem> { e }, 0));
            intent.PutExtra(nameof(BarcodeResult), result.ToBundle());
            StartActivity(intent);
            Finish();
        }

        private void OnBarcodeResult(BarcodeScanningResult result)
        {
            if (barcodeScannerView.ViewController.AutoSnappingEnabled && result != null)
            {
                var intent = new Intent(this, typeof(BarcodeResultActivity));
                intent.PutExtra(nameof(BarcodeResult), new BarcodeResult(result).ToBundle());
                StartActivity(intent);
                Finish();
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            barcodeScannerView.ViewController.OnResume();
            var status = ContextCompat.CheckSelfPermission(this, permissions[0]);
            if (status != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, permissions, REQUEST_PERMISSION_CODE);
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            barcodeScannerView.ViewController.OnPause();
        }

        public void OnCameraOpened()
        {
            barcodeScannerView.PostDelayed(() =>
            {
                barcodeScannerView.ViewController.UseFlash(flashEnabled);
                barcodeScannerView.ViewController.ContinuousFocus();
            }, 300);
        }

        public void OnPictureTaken(byte[] image, int orientation)
        {
            var bitmap = BitmapFactory.DecodeByteArray(image, 0, orientation);

            if (bitmap == null)
            {
                return;
            }

            var matrix = new Matrix();
            matrix.SetRotate(orientation, bitmap.Width / 2, bitmap.Height / 2);

            var result = Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, false);

            resultView.Post(() =>
            {
                resultView.SetImageBitmap(result);
                barcodeScannerView.ViewController.ContinuousFocus();
                barcodeScannerView.ViewController.StartPreview();
            });
        }

        private class BarcodeResultHandler : BarcodeDetectorResultHandlerWrapper
        {
            public event Action<BarcodeScanningResult> BarcodeScanned;

            public override bool HandleResult(BarcodeScanningResult result, IO.Scanbot.Sdk.SdkLicenseError error)
            {
                if (!MainActivity.SDK.LicenseInfo.IsValid)
                {
                    return false;
                }
                BarcodeScanned?.Invoke(result);
                return false;
            }
        }

        private class BarcodeScannerViewCallback : Java.Lang.Object, IBarcodeScannerViewCallback
        {
            public event Action<byte[], int> PictureTaken;
            public event Action CameraOpen;
            public event Action<BarcodeItem> SelectionOverlayBarcodeClicked;

            public void OnSelectionOverlayBarcodeClicked(BarcodeItem barcodeItem)
            {
                SelectionOverlayBarcodeClicked?.Invoke(barcodeItem);
            }

            public void OnCameraOpen()
            {
                CameraOpen?.Invoke();
            }

            public void OnPictureTaken(byte[] image, CaptureInfo captureInfo)
            {
                if (!MainActivity.SDK.LicenseInfo.IsValid)
                {
                    return;
                }
                PictureTaken?.Invoke(image, captureInfo.ImageOrientation);
            }
        }
    }
}
