using ScanbotSDK.MAUI.Example.Utils;

namespace ScanbotSDK.MAUI.Example.ClassicUI;

public class BaseComponentPage : ContentPage
{
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (!await Validation.IsCameraPermissionValid())
            return;

        CheckLicense();
    }

    private async void CheckLicense()
    {
        if (!ScanbotSDKMain.LicenseInfo.IsValid)
        {
            await Alert.ShowAsync("Error", "Your SDK license has expired");
        }
        else if (string.IsNullOrEmpty(MauiProgram.LicenseKey))
        {
            await Alert.ShowAsync("Welcome", "You are using the Trial SDK License. The SDK will be active for one minute.");
        }
    }
}