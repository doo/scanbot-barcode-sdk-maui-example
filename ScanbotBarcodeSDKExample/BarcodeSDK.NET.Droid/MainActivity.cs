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

namespace BarcodeSDK.NET.Droid;

[Activity(MainLauncher = true, Theme = "@style/AppTheme")]
public class MainActivity : Activity
{
    public const int IMPORT_IMAGE_REQUEST = 7777;
    View WarningView => FindViewById<View>(Resource.Id.warning_view);

    public static ScanbotBarcodeScannerSDK SDK;

    const int BARCODE_DEFAULT_UI_REQUEST_CODE = 910;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SDK = new ScanbotBarcodeScannerSDK(this);

        SetContentView(Resource.Layout.activity_main);

        FindViewById<TextView>(Resource.Id.barcode_camera_demo).Click += OnBarcodeCameraDemoClick;
        FindViewById<TextView>(Resource.Id.barcode_camerax_demo).Click += OnBarcodeCameraXDemoClick;
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

        Bitmap bitmap = await Scanbot.ImagePicker.Droid.ImagePicker.Instance.PickImageAsync();

        if (bitmap == null)
        {
            return;
        }

        var result = SDK.CreateBarcodeDetector().DetectFromBitmap(bitmap, 0);

        StartActivity(new Intent(this, typeof(BarcodeResultActivity)), new BarcodeResult(result, bitmap).ToBundle());
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

    private static BarcodeScannerAdditionalConfiguration CreateAdditionalConfiguration(BarcodeDensity density)
    {
        var additionalDefaults = new BarcodeScannerAdditionalConfiguration();
        return new BarcodeScannerAdditionalConfiguration(
            minimumTextLength: additionalDefaults.MinimumTextLength,
            maximumTextLength: additionalDefaults.MaximumTextLength,
            minimum1DQuietZoneSize: additionalDefaults.Minimum1DQuietZoneSize,
            gs1DecodingEnabled: additionalDefaults.Gs1DecodingEnabled,
            msiPlesseyChecksumAlgorithms: additionalDefaults.MsiPlesseyChecksumAlgorithms,
            stripCheckDigits: additionalDefaults.StripCheckDigits,
            lowPowerMode: additionalDefaults.LowPowerMode,
            codeDensity: density);
    }

    void StartBarcodeScannerActivity(bool withImage)
    {
        var configuration = new BarcodeScannerConfiguration();

        configuration.SetBarcodeFormatsFilter(
            BarcodeTypes.Instance.AcceptedTypes);
        configuration.SetAdditionalDetectionParameters(
            CreateAdditionalConfiguration(BarcodeDensity.High));
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
        //configuration.SetConfirmationDialogConfiguration(new BarcodeConfirmationDialogConfiguration(
        //    resultWithConfirmationEnabled: true,
        //    title: "Barcode Detected!",
        //    message: "A barcode was found.",
        //    confirmButtonTitle: "Continue",
        //    retryButtonTitle: "Try again",
        //    dialogTextFormat: BarcodeDialogFormat.TypeAndCode,
        //    buttonsAccentColor: null,
        //    isConfirmButtonFilled: false,
        //    filledConfirmButtonTextColor: null
        //    ));

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
        if (SDK.LicenseInfo.Status == IO.Scanbot.Sap.Status.StatusTrial)
        {
            WarningView.Visibility = ViewStates.Visible;
        }
        else
        {
            WarningView.Visibility = ViewStates.Gone;
        }
    }
}
