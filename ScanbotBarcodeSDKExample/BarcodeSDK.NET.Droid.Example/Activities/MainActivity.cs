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
using BarcodeSDK.NET.Droid.Activities.V1;
using BarcodeSDK.NET.Droid.Snippets;
using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using BarcodeScannerConfiguration = IO.Scanbot.Sdk.UI.View.Barcode.Configuration.BarcodeScannerConfiguration;
using BarcodeScannerActivityV2 = IO.Scanbot.Sdk.Ui_v2.Barcode.BarcodeScannerActivity;

namespace BarcodeSDK.NET.Droid
{
    [Activity(MainLauncher = true, Theme = "@style/AppTheme")]
    public partial class MainActivity : Activity
    {
        internal static ScanbotBarcodeScannerSDK SDK;

        private const int BARCODE_DEFAULT_UI_REQUEST_CODE = 910;
        private const int BARCODE_DEFAULT_UI_REQUEST_CODE_V2 = 911;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SDK = new ScanbotBarcodeScannerSDK(this);

#if LEGACY_EXAMPLES
            SetContentView(Resource.Layout.activity_main_legacy);
            FindViewById<TextView>(Resource.Id.rtu_ui).Click += OnRTUUIClick;
            FindViewById<TextView>(Resource.Id.rtu_ui_image).Click += OnRTUUIImageClick;
            FindViewById<TextView>(Resource.Id.batch_rtu_ui).Click += OnBatchRTUUIClick;
#else
            SetContentView(Resource.Layout.activity_main);
#endif
            FindViewById<TextView>(Resource.Id.barcode_camera_demo).Click += OnBarcodeCameraDemoClick;
            FindViewById<TextView>(Resource.Id.barcode_camerax_demo).Click += OnBarcodeCameraXDemoClick;
            FindViewById<TextView>(Resource.Id.barcode_scan_and_count).Click += OnBarcodeCameraScanAndCountClick;
            FindViewById<TextView>(Resource.Id.rtu_ui_v2_aroverlay).Click += OnRTUUI_V2_ClickArOverlay;
            FindViewById<TextView>(Resource.Id.rtu_ui_v2_item_mapping).Click += OnRTUUI_V2_ClickItemMapping;
            FindViewById<TextView>(Resource.Id.rtu_ui_v2_multiple_scanning_preview).Click +=  OnRTUUI_V2_ClickMultipleScanningPreview;
            FindViewById<TextView>(Resource.Id.rtu_ui_v2_multiple_scanning).Click += OnRTUUI_V2_ClickMultipleScanning;
            FindViewById<TextView>(Resource.Id.rtu_ui_v2_single_scanning).Click += OnRTUUI_V2_ClickSingleScanning;
            FindViewById<TextView>(Resource.Id.rtu_ui_v2_topbar).Click += OnRTUUI_V2_ClickTopBar;
            FindViewById<TextView>(Resource.Id.rtu_ui_v2_userguid).Click += OnRTUUI_V2_ClickUserGuid;

            FindViewById<TextView>(Resource.Id.rtu_ui_import).Click += OnImportClick;
            FindViewById<TextView>(Resource.Id.settings).Click += OnSettingsClick;
            FindViewById<TextView>(Resource.Id.clear_storage).Click += OnClearStorageClick;
            FindViewById<TextView>(Resource.Id.license_info).Click += OnLicenseInfoClick;
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
            intent.PutExtra("BarcodeResult", new BaseBarcodeResult<BarcodeScanningResult>(result, bitmap).ToBundle());
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
            var intent = BatchBarcodeScannerActivity.NewIntent(this, configuration);
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

                var intent = new Intent(this, typeof(BarcodeSDK.NET.Droid.Activities.V1.BarcodeResultActivity));
                var bundle = new BaseBarcodeResult<BarcodeScanningResult>(barcode, imagePath, previewPath).ToBundle();
                intent.PutExtra("BarcodeResult", bundle);

                StartActivity(intent);
            }
            
            if (requestCode == BARCODE_DEFAULT_UI_REQUEST_CODE_V2 &&
                data?.GetParcelableExtra(IO.Scanbot.Sdk.Ui_v2.Common.Activity.ActivityConstants.ExtraKeyRtuResult) is BarcodeScannerResult barcodeV2)
            {
                var imagePath = data.GetStringExtra(
                    IO.Scanbot.Sdk.Ui_v2.Barcode.BarcodeScannerActivity.ScannedBarcodeImagePathExtra);
                var previewPath = data.GetStringExtra(
                    IO.Scanbot.Sdk.Ui_v2.Barcode.BarcodeScannerActivity.ScannedBarcodePreviewFramePathExtra);

                var intent = new Intent(this, typeof(BarcodeSDK.NET.Droid.Activities.V2.BarcodeResultActivity));
                var bundle = new BaseBarcodeResult<BarcodeScannerResult>(barcodeV2, imagePath, previewPath).ToBundle();
                intent.PutExtra("BarcodeResult", bundle);

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