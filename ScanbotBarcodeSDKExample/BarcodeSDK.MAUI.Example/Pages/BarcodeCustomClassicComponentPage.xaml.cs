using ScanbotSDK.MAUI.Configurations;
using ScanbotSDK.MAUI.Constants;
using ScanbotSDK.MAUI.Models;

namespace ScanbotSDK.MAUI.Example.Pages;

public partial class BarcodeCustomClassicComponentPage : BaseComponentPage
{
    public BarcodeCustomClassicComponentPage()
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
                text += $"{barcode.Text} ({barcode.Format.ToString().ToUpper()})\n";
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                System.Diagnostics.Debug.WriteLine(text);
                lblResult.Text = text;
            });
        };

        cameraView.OverlayConfiguration = new SelectionOverlayConfiguration(
            automaticSelectionEnabled: true,
            overlayFormat: BarcodeTextFormat.CodeAndType,
            textColor: Colors.Yellow,
            textContainerColor: Colors.Black,
            strokeColor: Colors.Yellow,
            highlightedStrokeColor: Colors.Red,
            highlightedTextColor: Colors.Yellow,
            highlightedTextContainerColor: Colors.DarkOrchid,
            polygonBackgroundColor: Colors.Transparent,
            polygonBackgroundHighlightedColor: Colors.Transparent);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
      
        cameraView.HeightRequest = (DeviceDisplay.Current.MainDisplayInfo.Height / DeviceDisplay.Current.MainDisplayInfo.Density) * 0.6;
        cameraView.WidthRequest = (DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density);

        if (DeviceInfo.Platform == DevicePlatform.iOS)
        {
            StartScanningButton.IsVisible = false;
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        this.Navigation.PopAsync(true);
    }

    void StartScanningButton_Clicked(System.Object sender, System.EventArgs e)
    {
        cameraView.StartDetection();
        StartScanningButton.IsVisible = false;
    }
}
