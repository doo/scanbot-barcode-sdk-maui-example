using ScanbotSDK.MAUI.Configurations;
using ScanbotSDK.MAUI.Constants;
using ScanbotSDK.MAUI.Models;

namespace ScanbotSDK.MAUI.Example.Pages;

public partial class BarcodeClassicComponentPage : BaseComponentPage
{
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

            if (result?.Barcodes != null)
            {
                foreach (Barcode barcode in result.Barcodes)
                {
                    text += string.Format("{0} ({1})\n", barcode.Text, barcode.Format.ToString().ToUpper());
                }
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
        
        cameraView.StartDetection();
        
        cameraView.HeightRequest = (DeviceDisplay.Current.MainDisplayInfo.Height / DeviceDisplay.Current.MainDisplayInfo.Density) * 0.6;
        cameraView.WidthRequest = (DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        this.Navigation.PopAsync(true);
    }
    
}
