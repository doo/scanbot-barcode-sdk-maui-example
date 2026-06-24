using _Microsoft.Android.Resource.Designer;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.Core.View;
using IO.Scanbot.Sdk.Barcode_scanner;
using BarcodeSDK.NET.Droid.Activities;
using IO.Scanbot.Sdk.Barcode;
using IO.Scanbot.Sdk.Image;
using IO.Scanbot.Sdk.Licensing;
using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using ScanbotSDK.Droid.Helpers;
using BarcodeScannerConfiguration = IO.Scanbot.Sdk.Barcode.BarcodeScannerConfiguration;
using Result = Android.App.Result;

namespace BarcodeSDK.NET.Droid
{
    [Activity(MainLauncher = true, Theme = "@style/AppTheme")]
    public partial class MainActivity : AppCompatActivity, IOnApplyWindowInsetsListener
    {
        internal static ScanbotBarcodeScannerSDK Sdk;

        private TaskCompletionSource<Bitmap> _pendingBitmap;
        
        private const int BarcodeDefaultUiRequestCode = 910;
        private const int SelectImageFromGallery = 911;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            Sdk = new ScanbotBarcodeScannerSDK(this);

            SetContentView(ResourceConstant.Layout.activity_main);
            AndroidUtils.ApplyEdgeToEdge(FindViewById(ResourceConstant.Id.container), this);
            
            FindViewById<TextView>(ResourceConstant.Id.barcode_camerax_demo)!.Click += OnBarcodeCameraXDemoClick;
            FindViewById<TextView>(ResourceConstant.Id.barcode_scan_and_count)!.Click += OnBarcodeCameraScanAndCountClick;
            FindViewById<TextView>(ResourceConstant.Id.rtu_ui_single)!.Click += SingleScanning;
            FindViewById<TextView>(ResourceConstant.Id.rtu_ui_single_ar_overlay)!.Click += SingleScanningWithArOverlay;
            FindViewById<TextView>(ResourceConstant.Id.rtu_ui_batch)!.Click += BatchBarcodeScanning;
            FindViewById<TextView>(ResourceConstant.Id.rtu_ui_multiple_unique)!.Click += MultipleUniqueBarcodeScanning;
            FindViewById<TextView>(ResourceConstant.Id.rtu_ui_find_and_pick)!.Click += FindAndPickScanning;
            
            FindViewById<TextView>(ResourceConstant.Id.rtu_ui_import)!.Click += OnImportClick;
            FindViewById<TextView>(ResourceConstant.Id.settings)!.Click += OnSettingsClick;
            FindViewById<TextView>(ResourceConstant.Id.clear_storage)!.Click += OnClearStorageClick;
            FindViewById<TextView>(ResourceConstant.Id.license_info)!.Click += OnLicenseInfoClick;
        }

        private async void OnImportClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, Sdk))
            {
                return;
            }

            // Obtain an image from somewhere.
            // In this case, the user picks an image with our helper.
            var bitmap = await PickImageAsync();

            // Configure the barcode scanner for scanning many barcodes in one image.
            var barcodeFormatConfig = new BarcodeFormatCommonConfiguration { Formats = BarcodeTypes.Instance.AcceptedTypes };
            var barcodeScannerConfigs = new BarcodeScannerConfiguration
            {
                BarcodeFormatConfigurations = [barcodeFormatConfig],
                ExtractedDocumentFormats = BarcodeDocumentFormats.All
            };

            var barcodeScannerResult = Sdk.CreateBarcodeScanner(barcodeScannerConfigs);
            var barcodeScanner = ResultHelper.Get<IBarcodeScanner>(barcodeScannerResult);

            var inputImage = ImageRef.FromBitmap(bitmap, new BasicImageLoadOptions());
            var result = barcodeScanner.Run(inputImage);

            // Handle the result in your app as needed.
            var intent = new Intent(this, typeof(BarcodeResultActivity));
            intent.PutExtra("BarcodeResult", new BaseBarcodeResult<BarcodeScannerResult>(ResultHelper.Get<BarcodeScannerResult>(result), inputImage.UniqueId).ToBundle());
            StartActivity(intent);
        }

        private void OnSettingsClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, Sdk))
            {
                return;
            }
            var intent = new Intent(this, typeof(BarcodeTypesActivity));
            StartActivity(intent);
        }

        private void OnClearStorageClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, Sdk))
            {
                return;
            }
            Sdk.CreateBarcodeFileStorage().CleanupBarcodeImagesDirectory();
            Alert.Toast(this, "Cleared image storage");
        }

        private void OnLicenseInfoClick(object sender, EventArgs e)
        {
            var status = Sdk.LicenseInfo.Status.Name();
            var validity = Sdk.LicenseInfo.IsValid ? "The license is valid." : "The license is NOT valid";
            var message = validity + $"\n\n- {status}";
            
            if (Sdk.LicenseInfo.IsValid)
            {
                message += $"\n- Valid until: {Sdk.LicenseInfo.ExpirationDateString}";
            }

            Alert.ShowInfoDialog(this, "License Info", message);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode != Result.Ok && !Alert.CheckLicense(this, Sdk))
            {
                return;
            }

            switch (requestCode)
            {
                case BarcodeDefaultUiRequestCode:
                {
                    var parsedResult = _resultContract.ParseBarcodeResult((int)resultCode, data)?.Get<BarcodeScannerUiResult>();
                    if (parsedResult == null) return;
                    
                    var barcodes = parsedResult.Items.Select(item => item.Barcode).ToList();
                    var result = new BarcodeScannerResult(barcodes, true);
                    OnRTUActivityResult(result);

                    return;
                }
                case SelectImageFromGallery when (resultCode != Result.Ok || data?.Data == null):
                    _pendingBitmap?.SetCanceled();
                    return;
                case SelectImageFromGallery:
                {
                    var bitmap = ImageUtils.LoadBitmapFromUri(data.Data, ContentResolver);
                    if (bitmap == null)
                    {
                        _pendingBitmap?.TrySetException(new global::System.InvalidOperationException("Unable to decode the selected image."));
                        return;
                    }

                    _pendingBitmap?.TrySetResult(bitmap);
                    return;
                }
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            UpdateLicenseStatusWarning();
        }

        private void UpdateLicenseStatusWarning()
        {
            var warningView = FindViewById<View>(ResourceConstant.Id.warning_view);
            
            if (warningView == null)
                return;

            if (Sdk.LicenseInfo.Status.Equals(LicenseStatus.Trial))
            {
                warningView.Visibility = ViewStates.Visible;
            }
            else
            {
                warningView.Visibility = ViewStates.Gone;
            }
        }

        private Task<Bitmap> PickImageAsync()
        {
            _pendingBitmap = new TaskCompletionSource<Bitmap>();
            // Define the Intent for getting images
            var intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            var chooser = Intent.CreateChooser(intent, "Select Image");
            StartActivityForResult(chooser, SelectImageFromGallery);
            
            return _pendingBitmap.Task;
        }
        
        private void OnBarcodeCameraXDemoClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, Sdk))
            {
                return;
            }
            var intent = new Intent(this, typeof(BarcodeClassicComponentActivity));
            StartActivity(intent);
        }

        private void OnBarcodeCameraScanAndCountClick(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, Sdk))
            {
                return;
            }
            var intent = new Intent(this, typeof(BarcodeScanAndCountActivity));
            StartActivity(intent);
        }

        public WindowInsetsCompat OnApplyWindowInsets(View v, WindowInsetsCompat windowInsets)
        {
            return AndroidUtils.ApplyWindowInsets(v, windowInsets);
        }
    }
}