using ClassicComponent.MAUI.Legacy.Models;
using ScanbotSDK.MAUI;

namespace ClassicComponent.MAUI.Legacy;

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
            Console.WriteLine("Error: Your SDK license has expired");
        }
        else if (string.IsNullOrEmpty(MauiProgram.LicenseKey))
        {
            Console.WriteLine("Welcome: You are using the Trial SDK License. The SDK will be active for one minute.");
        }
    }
}