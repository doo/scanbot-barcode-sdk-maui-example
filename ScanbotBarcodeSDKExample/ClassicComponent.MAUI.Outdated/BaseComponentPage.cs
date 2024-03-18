using ClassicComponent.MAUI.Outdated.Models;
using ScanbotSDK.MAUI;

namespace ClassicComponent.MAUI.Outdated;

public class BaseComponentPage: ContentPage
{
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (!await Validation.IsCameraPermissionValid())
            return;

        CheckLicense();
    }

    public void CheckLicense()
    {
        if (!ScanbotSDK.MAUI.ScanbotBarcodeSDK.LicenseInfo.IsValid)
        {
            DisplayAlert("Error", "Your SDK license has expired", "Close");
        }
        else if (string.IsNullOrEmpty(MauiProgram.LicenseKey))
        {
            DisplayAlert("Welcome", "You are using the Trial SDK License. The SDK will be active for one minute.",
                "Close");
        }
    }
}