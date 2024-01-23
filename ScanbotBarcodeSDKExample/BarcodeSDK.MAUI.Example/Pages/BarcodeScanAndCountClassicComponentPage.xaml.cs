using ScanbotSDK.MAUI.Configurations;
using ScanbotSDK.MAUI.Constants;
using ScanbotSDK.MAUI.Models;

namespace ScanbotSDK.MAUI.Example.Pages;

public partial class BarcodeScanAndCountClassicComponentPage: ContentPage
{
    public BarcodeScanAndCountClassicComponentPage()
    {
        InitializeComponent();
        SetupViews();
    }

    private void SetupViews()
    {
        cameraView.OnBarcodeScanResult = (result) =>
        {
            if (result.Status != OperationResult.Ok)
                return;

            string text = string.Empty;
            foreach (Barcode barcode in result.Barcodes)
            {
                text += string.Format("{0} ({1})\n", barcode.Text, barcode.Format.ToString().ToUpper());
            }

            System.Diagnostics.Debug.WriteLine(text);
            lblResult.Text = text;
        };

        cameraView.OnScanAndCountFinished = (result) =>
        {
            if (result.Status == OperationResult.Ok)
            {
                StartScanningButton.IsEnabled = false;
                ContinueScanningButton.IsEnabled = true;
            }
        };

        cameraView.OverlayConfiguration = new SelectionOverlayConfiguration
        (
            automaticSelectionEnabled: false,
            overlayFormat: BarcodeTextFormat.CodeAndType,
            polygon: Colors.Green,
            text: Colors.Purple,
            textContainer: Colors.Black,
            highlightedTextContainerColor: Colors.Beige
        );
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        cameraView.StartDetection();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        this.Navigation.PopAsync(true);
    }

    void StartScanningButton_Clicked(System.Object sender, System.EventArgs e)
    {
        cameraView.StartScanAndCount();        
    }

    void ConitueScanningButton_Clicked(System.Object sender, System.EventArgs e)
    {
        cameraView.ContinueScanning();

        StartScanningButton.IsEnabled = true;
        ContinueScanningButton.IsEnabled = false;
    }
}
