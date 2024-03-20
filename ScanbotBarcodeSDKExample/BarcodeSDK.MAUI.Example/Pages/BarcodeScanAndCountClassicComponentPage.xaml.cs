using ScanbotSDK.MAUI.Configurations;
using ScanbotSDK.MAUI.Constants;
using ScanbotSDK.MAUI.Models;

namespace ScanbotSDK.MAUI.Example.Pages;

public partial class BarcodeScanAndCountClassicComponentPage : BaseComponentPage
{
    public BarcodeScanAndCountClassicComponentPage()
    {
        InitializeComponent();
        SetupViews();
    }

    private void SetupViews()
    {
        cameraView.OverlayConfiguration = new SelectionOverlayConfiguration
        (
            automaticSelectionEnabled: false,
            overlayFormat: BarcodeTextFormat.CodeAndType,
            textColor: Colors.Yellow,
            textContainerColor: Colors.Black,
            strokeColor: Colors.Yellow,
            highlightedStrokeColor: Colors.Red,
            highlightedTextColor: Colors.Yellow,
            highlightedTextContainerColor: Colors.DarkOrchid,
            polygonBackgroundColor: Colors.Transparent,
            polygonBackgroundHighlightedColor: Colors.Transparent
        );
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        // Start barcode detection manually
        cameraView.StartDetection();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        
        // Stop barcode detection manually
        cameraView.StopDetection();
        
        this.Navigation.PopAsync(true);
    }

    void StartScanningButton_Clicked(System.Object sender, System.EventArgs e)
    {
        // Start scanning
        cameraView.StartScanAndCount();
    }

    void ConitueScanningButton_Clicked(System.Object sender, System.EventArgs e)
    {
        cameraView.ContinueScanning();

        StartScanningButton.IsEnabled = true;
        ContinueScanningButton.IsEnabled = false;
    }

    private void CameraView_OnOnBarcodeScanResult(BarcodeResultBundle result)
    {
        if (result.Status != OperationResult.Ok)
            return;

        string text = string.Empty;
        foreach (Barcode barcode in result?.Barcodes)
        {
            text += string.Format("{0} ({1})\n", barcode.Text, barcode.Format.ToString().ToUpper());
        }

        System.Diagnostics.Debug.WriteLine(text);
        lblResult.Text = text;
    }

    private void CameraView_OnOnScanAndCountFinished(BarcodeResultBundle result)
    {
        if (result.Status == OperationResult.Ok)
        {
            StartScanningButton.IsEnabled = false;
            ContinueScanningButton.IsEnabled = true;
        }
    }
}
