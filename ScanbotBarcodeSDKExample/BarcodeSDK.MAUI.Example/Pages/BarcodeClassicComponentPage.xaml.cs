using ScanbotSDK.MAUI.Configurations;
using ScanbotSDK.MAUI.Constants;
using ScanbotSDK.MAUI.Models;

namespace ScanbotSDK.MAUI.Example.Pages;

public partial class BarcodeClassicComponentPage : ContentPage
{
    public bool IsLicenseValid => ScanbotBarcodeSDK.LicenseInfo.IsValid;

    public BarcodeClassicComponentPage()
	{
		InitializeComponent();
        SetupViews();
    }

    private void SetupViews()
    {
        cameraView.OnBarcodeScanResult = (result) =>
        {
            string text = string.Empty;
            foreach (Barcode barcode in result.Barcodes)
            {
                text += string.Format("{0} ({1})\n", barcode.Text, barcode.Format.ToString().ToUpper());
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                System.Diagnostics.Debug.WriteLine(text);
                lblResult.Text = text;
            });
        };
        cameraView.OverlayConfiguration = new SelectionOverlayConfiguration(true, BarcodeTextFormat.CodeAndType,
                                                                            Colors.Yellow, Colors.Yellow, Colors.Black,
                                                                            Colors.Red, Colors.Red, Colors.Black);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (!IsLicenseValid)
        {
            ShowExpiredLicenseAlert();
        }
        else if (string.IsNullOrEmpty(App.LicenseKey))
        {
            ShowTrialLicenseAlert();
        }

        cameraView.HeightRequest = (DeviceDisplay.Current.MainDisplayInfo.Height / DeviceDisplay.Current.MainDisplayInfo.Density) * 0.6;
        cameraView.WidthRequest = (DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density);

        if (DeviceInfo.Platform == DevicePlatform.iOS) {
            StartScanningButton.IsVisible = false;
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        this.Navigation.PopAsync(true);
    }

    private void ShowExpiredLicenseAlert()
    {
        DisplayAlert("Error", "Your SDK license has expired", "Close");
    }

    private void ShowTrialLicenseAlert()
    {
        DisplayAlert("Welcome", "You are using the Trial SDK License. The SDK will be active for one minute.", "Close");
    }

    void StartScanningButton_Clicked(System.Object sender, System.EventArgs e)
    {
        cameraView.StartDetection();
        StartScanningButton.IsVisible = false;
    }
}
