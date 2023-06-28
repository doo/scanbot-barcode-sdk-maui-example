using System;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Widget;
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
    public class DemoBarcodeCameraViewActivity : AppCompatActivity
    {
        BarcodeScannerView barcodeScannerView;
        ImageView resultView;

        const int REQUEST_PERMISSION_CODE = 200;
        public string[] Permissions
        {
            get => new string[] { Manifest.Permission.Camera };
        }

        bool flashEnabled = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SupportRequestWindowFeature(WindowCompat.FeatureActionBarOverlay);
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.barcode_camera_activity);

            barcodeScannerView = FindViewById<BarcodeScannerView>(Resource.Id.camera);
            resultView = FindViewById<ImageView>(Resource.Id.result);

            var SDK = new ScanbotBarcodeScannerSDK(this);
            var detector = SDK.CreateBarcodeDetector();
            
            detector.ModifyConfig(new Function1Impl<BarcodeScannerConfigBuilder>((response) => {
                response.SetSaveCameraPreviewFrame(false);
                response.SetBarcodeFormats(BarcodeTypes.Instance.AcceptedTypes);
            }));
            barcodeScannerView.InitCamera(new CameraUiSettings(false));
            var resultHandler = new BarcodeResultDelegate();
            resultHandler.Success += OnBarcodeResult;

            var scannerViewCallback = new BarcodeScannerViewCallback();
            scannerViewCallback.CameraOpen = OnCameraOpened;
            scannerViewCallback.PictureTaken += OnPictureTaken;
            scannerViewCallback.SelectionOverlayBarcodeClicked += OnSelectionOverlayBarcodeClicked;

            BarcodeScannerViewWrapper.InitDetectionBehavior(barcodeScannerView, detector, resultHandler, scannerViewCallback);
            barcodeScannerView.ViewController.AutoSnappingEnabled = true;
            barcodeScannerView.ViewController.SetAutoSnappingSensitivity(1f);

            barcodeScannerView.SelectionOverlayController.SetEnabled(true);
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

        private void OnSelectionOverlayBarcodeClicked(object? sender, BarcodeItem e)
        {
            var instance = new BarcodeResultBundle();
            instance.ScanningResult = new BarcodeScanningResult(new List<BarcodeItem> { e }, 0);
            BarcodeResultBundle.Instance = instance;

            StartActivity(new Intent(this, typeof(BarcodeResultActivity)));
            Finish();
        }

        private void OnBarcodeResult(object sender, BarcodeEventArgs e)
        {
            if (e.Result != null)
            {
                BarcodeResultBundle.Instance = new BarcodeResultBundle(e.Result);
                StartActivity(new Intent(this, typeof(BarcodeResultActivity)));
                Finish();
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            barcodeScannerView.ViewController.OnResume();
            var status = ContextCompat.CheckSelfPermission(this, Permissions[0]);
            if (status != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, Permissions, REQUEST_PERMISSION_CODE);
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            barcodeScannerView.ViewController.OnPause();
        }









        public void OnCameraOpened()
        {
            barcodeScannerView.PostDelayed(delegate
            {
                barcodeScannerView.ViewController.UseFlash(flashEnabled);
                barcodeScannerView.ViewController.ContinuousFocus();
            }, 300);
        }

        public void OnPictureTaken(object sender, PictureTakenEventArgs e)
        {
            var image = e.Image;
            var orientation = e.Orientation;

            var bitmap = BitmapFactory.DecodeByteArray(image, 0, orientation);

            var matrix = new Matrix();
            matrix.SetRotate(orientation, bitmap.Width / 2, bitmap.Height / 2);

            var result = Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, false);

            resultView.Post(delegate
            {
                resultView.SetImageBitmap(result);
                barcodeScannerView.ViewController.ContinuousFocus();
                barcodeScannerView.ViewController.StartPreview();
            });
        }
    }

}
