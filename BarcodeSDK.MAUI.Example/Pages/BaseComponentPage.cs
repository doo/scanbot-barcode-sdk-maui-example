using ScanbotSDK.MAUI.Example.Utils;

namespace ScanbotSDK.MAUI.Example.Pages;

public class BaseComponentPage : ContentPage
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
        if (!ScanbotSDKMain.LicenseInfo.IsValid)
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