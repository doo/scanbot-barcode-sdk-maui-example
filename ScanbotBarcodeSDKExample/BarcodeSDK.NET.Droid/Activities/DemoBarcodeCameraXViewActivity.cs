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
    public class DemoBarcodeCameraXViewActivity : AppCompatActivity, ICameraOpenCallback
    {
        ScanbotCameraXView cameraView;
        ImageView resultView;

        BarcodeDetectorFrameHandler handler;

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

            SetContentView(Resource.Layout.barcode_camerax_activity);

            cameraView = FindViewById<ScanbotCameraXView>(Resource.Id.camerax);
            resultView = FindViewById<ImageView>(Resource.Id.result);

            var SDK = new ScanbotBarcodeScannerSDK(this);
            var detector = SDK.CreateBarcodeDetector();
            detector.ModifyConfig(new Function1Impl<BarcodeScannerConfigBuilder>((response) => {
                response.SetSaveCameraPreviewFrame(true);
                response.SetBarcodeFormats(BarcodeTypes.Instance.AcceptedTypes);
            }));
            var resultHandler = new BarcodeResultDelegate();
            resultHandler.Success += OnBarcodeResult;

            var pictureDelegate = new PictureResultDelegate();
            cameraView.AddPictureCallback(pictureDelegate);
            pictureDelegate.PictureTaken += OnPictureTaken;

            cameraView.SetCameraOpenCallback(this);

            ScanbotCameraViewWrapper.InitDetectionBehavior(cameraView, detector, resultHandler, Java.Lang.Long.ValueOf(1000));

            FindViewById<Button>(Resource.Id.flash).Click += delegate
            {
                flashEnabled = !flashEnabled;
                cameraView.UseFlash(flashEnabled);
            };
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
            var status = ContextCompat.CheckSelfPermission(this, Permissions[0]);
            if (status != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, Permissions, REQUEST_PERMISSION_CODE);
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        public void OnCameraOpened()
        {
            cameraView.PostDelayed(delegate
            {
                cameraView.UseFlash(flashEnabled);
                cameraView.ContinuousFocus();
            }, 300);
        }

        public void OnPictureTaken(object sender, PictureTakenEventArgs e)
        {
            if (e?.Image == null)
                return;

            var image = e.Image;
            var orientation = e.Orientation;

            var bitmap = BitmapFactory.DecodeByteArray(image, 0, image.Length);

            var matrix = new Matrix();
            matrix.SetRotate(orientation, bitmap.Width / 2, bitmap.Height / 2);

            var result = Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, false);

            resultView.Post(delegate
            {
                resultView.SetImageBitmap(result);
                cameraView.ContinuousFocus();
                cameraView.StartPreview();
            });
        }
    }
}
