using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using IO.Scanbot.Sdk.Barcode_scanner;
using BarcodeSDK.NET.Droid.Activities;
using IO.Scanbot.Sdk.Barcode;
using IO.Scanbot.Sdk.Ui_v2.Barcode;
using BarcodeScannerConfiguration = IO.Scanbot.Sdk.Barcode.BarcodeScannerConfiguration;

namespace BarcodeSDK.NET.Droid
{
    [Activity(MainLauncher = true, Theme = "@style/AppTheme")]
    public partial class MainActivity : AppCompatActivity //, IActivityResultCallback
    {
        internal static ScanbotBarcodeScannerSDK SDK;

        private const int BARCODE_DEFAULT_UI_REQUEST_CODE = 910;
        private const int SELECT_IMAGE_FROM_GALLERY = 911;
        
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

                // Configure the barcode scanner for scanning many barcodes in one image.
                var barcodeScanner = SDK.CreateBarcodeScanner();
                var barcodeFormatConfig = new BarcodeFormatCommonConfiguration { Formats = BarcodeFormats.All };
                var barcodeScannerConfigs = new BarcodeScannerConfiguration
                {
                    BarcodeFormatConfigurations = [barcodeFormatConfig],
                    ExtractedDocumentFormats = BarcodeDocumentFormats.All
                };
                
                barcodeScanner.SetConfiguration(barcodeScannerConfigs);

                var result = barcodeScanner.ScanFromBitmap(bitmap, 0);

                // Handle the result in your app as needed.
                var intent = new Intent(this, typeof(BarcodeResultActivity));
                intent.PutExtra("BarcodeResult", new BaseBarcodeResult<BarcodeScannerResult>(result, bitmap).ToBundle());
                StartActivity(intent);
            }
            catch(TaskCanceledException)
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

            if (requestCode == BARCODE_DEFAULT_UI_REQUEST_CODE)
            {
                var bsResult = resultContract.ParseResult((int)resultCode, data);
                if (bsResult is BarcodeScannerActivity.BarcodeScannerActivityResult resultUiResult && resultUiResult?.Result != null)
                {
                    var barcodes = resultUiResult.ScannerUiResult().Items.Select(item => item.Barcode).ToList();
                    var result = new BarcodeScannerResult(barcodes, true);
                    OnRTUv2ActivityResult(result);
                }
                return;
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
            
            if (warningView == null)
                return;

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