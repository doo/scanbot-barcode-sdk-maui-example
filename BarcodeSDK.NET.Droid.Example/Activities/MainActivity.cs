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
        private const int SELECT_IMAGE_FROM_GALLERY = 912;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SDK = new ScanbotBarcodeScannerSDK(this);

            SetContentView(Resource.Layout.activity_main);
            FindViewById<TextView>(Resource.Id.barcode_camerax_demo).Click += OnBarcodeCameraXDemoClick;
            FindViewById<TextView>(Resource.Id.barcode_scan_and_count).Click += OnBarcodeCameraScanAndCountClick;
            FindViewById<TextView>(Resource.Id.rtu_ui_v2_single).Click += SingleScanning;
            FindViewById<TextView>(Resource.Id.rtu_ui_v2_single_ar_overlay).Click += SingleScanningWithArOverlay;
            FindViewById<TextView>(Resource.Id.rtu_ui_v2_batch).Click += BatchBarcodeScanning;
            FindViewById<TextView>(Resource.Id.rtu_ui_v2_multiple_unique).Click += MultipleUniqueBarcodeScanning;
            FindViewById<TextView>(Resource.Id.rtu_ui_v2_find_and_pick).Click += FindAndPickScanning;
            
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

            try
            {
                // Optain an image from somewhere.
                // In this case, the user picks an image with our helper.
                var bitmap = await PickImageAsync();

                // Configure the barcode detector for detecting many barcodes in one image.
                var barcodeDetector = SDK.CreateBarcodeDetector();
                barcodeDetector.ModifyConfig(detectorConfig =>
                {
                    var defaultConfig = new BarcodeScannerAdditionalConfig();
                    detectorConfig.SetBarcodeFormats(BarcodeTypes.Instance.AcceptedTypes);

                    var additionalParams = new BarcodeScannerAdditionalConfig(
                        minimumTextLength: BarcodeScannerAdditionalConfig.DefaultMinTextLength,
                        maximumTextLength: BarcodeScannerAdditionalConfig.DefaultMaxTextLength,
                        minimum1DQuietZoneSize: BarcodeScannerAdditionalConfig.DefaultMin1dQuietZoneSize,
                        gs1Handling: BarcodeScannerAdditionalConfig.DEFAULT_GS1_DECODING_HANDLING,
                        msiPlesseyChecksumAlgorithms: BarcodeScannerAdditionalConfig.DEFAULT_MSI_PLESSEY_CHECKSUM_ALGORITHMS,
                        stripCheckDigits: BarcodeScannerAdditionalConfig.DefaultStripCheckDigits,
                        lowPowerMode: BarcodeScannerAdditionalConfig.DefaultLowPowerMode,
                        useIata2Of5Checksum: BarcodeScannerAdditionalConfig.DefaultUseIata2Of5Checksum,
                        useCode11Checksum: BarcodeScannerAdditionalConfig.DefaultUseCode11Checksum,
                        australiaPostCustomerFormat: BarcodeScannerAdditionalConfig.DEFAULT_AUSTRALIA_POST_CUSTOMER_FORMAT,
                        addAdditionalQuietZone: true);
                    detectorConfig.SetAdditionalConfig(additionalParams);
                });

                var result = barcodeDetector.DetectFromBitmap(bitmap, 0);

                // Handle the result in your app as needed.
                var intent = new Intent(this, typeof(Activities.V1.BarcodeResultActivity));
                intent.PutExtra("BarcodeResult", new BaseBarcodeResult<BarcodeScanningResult>(result, bitmap).ToBundle());
                StartActivity(intent);
            }
            catch
            {

            }
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
        
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode != Result.Ok && !Alert.CheckLicense(this, SDK))
            {
                return;
            }

            if (requestCode == BARCODE_DEFAULT_UI_REQUEST_CODE_V2 &&
                data?.GetParcelableExtra(IO.Scanbot.Sdk.Ui_v2.Common.Activity.ActivityConstants.ExtraKeyRtuResult) is BarcodeScannerResult barcodeV2)
            {
                OnRTUv2ActivityResult(data, barcodeV2);
            }

            if (requestCode == SELECT_IMAGE_FROM_GALLERY)
            {
                if (resultCode != Result.Ok)
                {
                    pendingBitmap.SetCanceled();
                    return;
                }
                var stream = ContentResolver.OpenInputStream(data.Data);
                pendingBitmap.SetResult(BitmapFactory.DecodeStream(stream));
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

        private TaskCompletionSource<Bitmap> pendingBitmap = new TaskCompletionSource<Bitmap>();

        private Task<Bitmap> PickImageAsync()
        {
            pendingBitmap = new TaskCompletionSource<Bitmap>();
            // Define the Intent for getting images
            var intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            var chooser = Intent.CreateChooser(intent, "Select Image");
            StartActivityForResult(chooser, SELECT_IMAGE_FROM_GALLERY);
            
            return pendingBitmap.Task;
        }
    }
}