using Android;
using Android.Content.PM;
using Android.Graphics;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using AndroidX.Core.View;
using IO.Scanbot.Sdk.Barcode;
using IO.Scanbot.Sdk.Barcode.UI;
using IO.Scanbot.Sdk.Barcode_scanner;
using IO.Scanbot.Sdk.Camera;
using IO.Scanbot.Sdk.UI.Camera;
using Intent = Android.Content.Intent;

namespace BarcodeSDK.NET.Droid.Activities
{
    [Activity(Theme = "@style/AppTheme")]
    public class BarcodeClassicComponentActivity : AppCompatActivity
    {
        private BarcodeScannerView barcodeScannerView;
        private ImageView resultView;

        private const int REQUEST_PERMISSION_CODE = 200;
        private static readonly string[] permissions = new string[] { Manifest.Permission.Camera };

        private bool flashEnabled = false;
        private readonly bool selectionOverlayEnabled = true;
        private readonly bool autoSelectionEnabled = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SupportRequestWindowFeature(WindowCompat.FeatureActionBarOverlay);
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.barcode_classic_activity);

            var barcodeScanner = new ScanbotBarcodeScannerSDK(this).CreateBarcodeScanner();
            var barcodeFormatConfig = new BarcodeFormatCommonConfiguration { Formats = BarcodeFormats.All };
            var barcodeScannerConfigs = new BarcodeScannerConfiguration
            {
                BarcodeFormatConfigurations = [barcodeFormatConfig],
                ExtractedDocumentFormats = BarcodeDocumentFormats.All,
                ReturnBarcodeImage = true
            };
                
            barcodeScanner.SetConfiguration(barcodeScannerConfigs);

            barcodeScannerView = FindViewById<BarcodeScannerView>(Resource.Id.camera);
            barcodeScannerView?.InitCamera(new CameraUiSettings(true));
            barcodeScannerView?.InitDetectionBehavior(barcodeScanner, OnBarcodeResult, (
                onCameraOpen: OnCameraOpened,
                onPictureTaken: OnPictureTaken,
                onSelectionOverlayBarcodeClicked: OnSelectionOverlayBarcodeClicked
            ));

            barcodeScannerView?.SelectionOverlayController.SetEnabled(selectionOverlayEnabled);
            barcodeScannerView?.SelectionOverlayController.SetBarcodeAppearanceDelegate(
            (
                getPolygonStyle: (defaultStyle, _) => defaultStyle.Copy(
                    fillColor: Color.Yellow,
                    strokeColor: Color.Yellow
                    ),
                getTextViewStyle: (defaultStyle, _) => defaultStyle.Copy(
                    textColor: Color.Yellow,
                    textContainerColor: Color.Black
                   )
            ));

            resultView = FindViewById<ImageView>(Resource.Id.result);
            FindViewById<Button>(Resource.Id.flash).Click += delegate
            {
                flashEnabled = !flashEnabled;
                barcodeScannerView.ViewController.UseFlash(flashEnabled);
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

        private bool OnBarcodeResult(BarcodeScannerResult result, IO.Scanbot.Sdk.SdkLicenseError _)
        {
            if (!MainActivity.SDK.LicenseInfo.IsValid)
            {
                return false;
            }

            var shouldHandleBarcode = selectionOverlayEnabled ? autoSelectionEnabled : true;

            if (shouldHandleBarcode && result != null)
            {
               var intent = new Intent(this, typeof(BarcodeResultActivity));
               intent.PutExtra(("BarcodeResult"), new BaseBarcodeResult<BarcodeScannerResult>(result).ToBundle());
               StartActivity(intent);
               Finish();
            }

            return false;
        }

        protected override void OnResume()
        {
            base.OnResume();
            barcodeScannerView.ViewController.OnResume();

            // Additional logic
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

        public void OnPictureTaken(byte[] image, CaptureInfo captureInfo)
        {
            // Is this code used at all ? 
            // Reply: Currently no. This code will be used only if we add a button click. 
            if (!MainActivity.SDK.LicenseInfo.IsValid)
            {
                return;
            }

            var bitmap = BitmapFactory.DecodeByteArray(image, 0, captureInfo.ImageOrientation);

            if (bitmap == null)
            {
                return;
            }

            var matrix = new Matrix();
            matrix.SetRotate(captureInfo.ImageOrientation, bitmap.Width / 2, bitmap.Height / 2);

            var result = Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, false);

            resultView.Post(() =>
            {
                resultView.SetImageBitmap(result);
                barcodeScannerView.ViewController.ContinuousFocus();
                barcodeScannerView.ViewController.StartPreview();
            });
        }
    }
}
