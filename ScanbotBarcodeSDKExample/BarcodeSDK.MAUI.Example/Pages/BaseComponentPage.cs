using ScanbotSDK.MAUI.Example.Utils;

namespace ScanbotSDK.MAUI.Example.Pages;

public class BaseComponentPage : ContentPage
{
    public bool IsLicenseValid => ScanbotBarcodeSDK.LicenseInfo.IsValid;
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        if (!await Validation.IsCameraPermissionValid())
        {
            return;   
        }
        
        if (!IsLicenseValid)
        {
            ShowExpiredLicenseAlert();
        }
        else if (string.IsNullOrEmpty(MauiProgram.LicenseKey))
        {
            ShowTrialLicenseAlert();
        }
    }
    
    private void ShowExpiredLicenseAlert()
    {
        DisplayAlert("Error", "Your SDK license has expired", "Close");
    }

    private void ShowTrialLicenseAlert()
    {
        DisplayAlert("Welcome", "You are using the Trial SDK License. The SDK will be active for one minute.", "Close");
    }
}