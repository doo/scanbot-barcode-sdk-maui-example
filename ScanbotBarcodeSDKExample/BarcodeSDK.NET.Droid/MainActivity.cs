using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using IO.Scanbot.Sdk.Barcode.Entity;
using IO.Scanbot.Sdk.Barcode_scanner;
using IO.Scanbot.Sdk.UI.Barcode_scanner.View.Barcode;
using IO.Scanbot.Sdk.UI.Barcode_scanner.View.Barcode.Batch;
using IO.Scanbot.Sdk.UI.View.Barcode.Batch.Configuration;
using IO.Scanbot.Sdk.UI.View.Barcode.Configuration;
using IO.Scanbot.Sdk.UI.View.Base;
using IO.Scanbot.Sdk.Barcode;
using BarcodeSDK.NET.Droid.Activities;

namespace BarcodeSDK.NET.Droid
{
    [Activity(MainLauncher = true, Theme = "@style/AppTheme")]
    public class MainActivity : Activity
    {
        internal static ScanbotBarcodeScannerSDK SDK;

        private const int BARCODE_DEFAULT_UI_REQUEST_CODE = 910;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SDK = new ScanbotBarcodeScannerSDK(this);

            SetContentView(Resource.Layout.activity_main);

            FindViewById<TextView>(Resource.Id.barcode_camera_demo).Click += OnBarcodeCameraDemoClick;
            FindViewById<TextView>(Resource.Id.barcode_camerax_demo).Click += OnBarcodeCameraXDemoClick;
            FindViewById<TextView>(Resource.Id.barcode_scan_and_count).Click += OnBarcodeCameraScanAndCountClick;
            FindViewById<TextView>(Resource.Id.rtu_ui).Click += OnRTUUIClick;
            FindViewById<TextView>(Resource.Id.rtu_ui_image).Click += OnRTUUIImageClick;
            FindViewById<TextView>(Resource.Id.batch_rtu_ui).Click += OnBatchRTUUIClick;
            FindViewById<TextView>(Resource.Id.rtu_ui_import).Click += OnImportClick;
            FindViewById<TextView>(Resource.Id.settings).Click += OnSettingsClick;
            FindViewById<TextView>(Resource.Id.clear_storage).Click += OnClearStorageClick;
            FindViewById<TextView>(Resource.Id.license_info).Click += OnLicenseInfoClick;
        }

        private void OnBarcodeCameraDemoClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            var intent = new Intent(this, typeof(BarcodeClassicComponentActivity));
            intent.PutExtra("useCameraX", false);
            StartActivity(intent);
        }

        private void OnBarcodeCameraXDemoClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            var intent = new Intent(this, typeof(BarcodeClassicComponentActivity));
            intent.PutExtra("useCameraX", true);
            StartActivity(intent);
        }

        private void OnBarcodeCameraScanAndCountClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            var intent = new Intent(this, typeof(BarcodeScanAndCountActivity));
            StartActivity(intent);
        }

        private void OnRTUUIClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            StartBarcodeScannerActivity(withImage: false);
        }

        private void OnRTUUIImageClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            StartBarcodeScannerActivity(withImage: true);
        }

        private void OnBatchRTUUIClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            StartBatchBarcodeScannerActivity();
        }

        private async void OnImportClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }

            // Optain an image from somewhere.
            // In this case, the user picks an image with our helper.
            Bitmap bitmap = await Scanbot.ImagePicker.Droid.ImagePicker.Instance.PickImageAsync();

            if (bitmap == null)
            {
                return;
            }

            // Configure the barcode detector for detecting many barcodes in one image.
            var barcodeDetector = SDK.CreateBarcodeDetector();
            barcodeDetector.ModifyConfig(detectorConfig =>
            {
                var defaultConfig = new BarcodeScannerAdditionalConfig();
                detectorConfig.SetBarcodeFormats(BarcodeTypes.Instance.AcceptedTypes);
                detectorConfig.SetEngineMode(EngineMode.NextGen);
                detectorConfig.SetAdditionalConfig(defaultConfig.Copy(codeDensity: BarcodeDensity.High)); 
            });

            var result = barcodeDetector.DetectFromBitmap(bitmap, 0);

            // Handle the result in your app as needed.
            var intent = new Intent(this, typeof(BarcodeResultActivity));
            intent.PutExtra(nameof(BarcodeResult), new BarcodeResult(result, bitmap).ToBundle());
            StartActivity(intent);
        }

        private void OnSettingsClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            var intent = new Intent(this, typeof(BarcodeTypesActivity));
            StartActivity(intent);
        }

        private void OnClearStorageClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            SDK.CreateBarcodeFileStorage().CleanupBarcodeImagesDirectory();
            Alert.Toast(this, "Cleared image storage");
        }

        private void OnLicenseInfoClick(object sender, EventArgs e)
        {
            var status = SDK.LicenseInfo.Status;
            var date = SDK.LicenseInfo.ExpirationDate;
            var validity = SDK.LicenseInfo.IsValid ? "The license is valid." : "The license is NOT valid";
            var message = validity + $"\n\n- Status: {status}";
            if (date != null)
            {
                message += $"\n- Valid until: {date}";
            }

            Alert.ShowInfoDialog(this, "License Info", message);
        }

        void StartBarcodeScannerActivity(bool withImage)
        {
            var configuration = new BarcodeScannerConfiguration();
            var defaultAdditionalConfig = new BarcodeScannerAdditionalConfiguration();

            configuration.SetBarcodeFormatsFilter(
                BarcodeTypes.Instance.AcceptedTypes);
            configuration.SetAdditionalDetectionParameters(
                defaultAdditionalConfig.Copy(codeDensity: BarcodeDensity.High));
            configuration.SetEngineMode(EngineMode.NextGen);
            configuration.SetSuccessBeepEnabled(true);

            if (withImage)
            {
                configuration.SetBarcodeImageGenerationType(BarcodeImageGenerationType.VideoFrame);
            }

            configuration.SetSelectionOverlayConfiguration(
                new IO.Scanbot.Sdk.UI.View.Barcode.SelectionOverlayConfiguration(
                    overlayEnabled: true,
                    automaticSelectionEnabled: false,
                    textFormat: IO.Scanbot.Sdk.Barcode.UI.BarcodeOverlayTextFormat.Code,
                    polygonColor: Color.Yellow,
                    textColor: Color.Yellow,
                    textContainerColor: Color.Black));

            // To see the confirmation dialog in action, uncomment the below and comment out the configuration.SetConfirmationDialogConfiguration line above.
            //configuration.SetConfirmationDialogConfiguration(new IO.Scanbot.Sdk.UI.View.Barcode.Dialog.BarcodeConfirmationDialogConfiguration(
            //    resultWithConfirmationEnabled: true,
            //    title: "Barcode Detected!",
            //    message: "A barcode was found.",
            //    confirmButtonTitle: "Continue",
            //    retryButtonTitle: "Try again",
            //    dialogTextFormat: IO.Scanbot.Sdk.UI.View.Barcode.Dialog.BarcodeDialogFormat.TypeAndCode,
            //    buttonsAccentColor: null,
            //    isConfirmButtonFilled: false,
            //    filledConfirmButtonTextColor: null
            //));

            var intent = BarcodeScannerActivity.NewIntent(this, configuration);
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE);
        }

        void StartBatchBarcodeScannerActivity()
        {
            var configuration = new BatchBarcodeScannerConfiguration();
            var list = BarcodeTypes.Instance.AcceptedTypes;
            configuration.SetBarcodeFormatsFilter(list);
            configuration.SetSelectionOverlayConfiguration(new IO.Scanbot.Sdk.UI.View.Barcode.SelectionOverlayConfiguration(true,
                true, IO.Scanbot.Sdk.Barcode.UI.BarcodeOverlayTextFormat.Code,
                Color.Yellow, Color.Yellow, Color.Black, Color.Pink));
            var intent = BatchBarcodeScannerActivity.NewIntent(this, configuration, null);
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode != Result.Ok && !Alert.CheckLicense(this, SDK))
            {
                return;
            }

            if (requestCode == BARCODE_DEFAULT_UI_REQUEST_CODE &&
                data?.GetParcelableExtra(
                    RtuConstants.ExtraKeyRtuResult) is BarcodeScanningResult barcode)
            {
                var imagePath = data.GetStringExtra(
                    BarcodeScannerActivity.ScannedBarcodeImagePathExtra);
                var previewPath = data.GetStringExtra(
                    BarcodeScannerActivity.ScannedBarcodePreviewFramePathExtra);

                var intent = new Intent(this, typeof(BarcodeResultActivity));
                var bundle = new BarcodeResult(barcode, imagePath, previewPath).ToBundle();
                intent.PutExtra(nameof(BarcodeResult), bundle);

                StartActivity(intent);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            UpdateLicenseStatusWarning();
        }

        private void UpdateLicenseStatusWarning()
        {
            var warningView = FindViewById<View>(Resource.Id.warning_view);

            if (SDK.LicenseInfo.Status == IO.Scanbot.Sap.Status.StatusTrial)
            {
                warningView.Visibility = ViewStates.Visible;
            }
            else
            {
                warningView.Visibility = ViewStates.Gone;
            }
        }
    }
}