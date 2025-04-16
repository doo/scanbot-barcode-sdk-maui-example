using ScanbotSDK.MAUI.Barcode.Core;

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
        CameraView.OverlayConfiguration = new Barcode.SelectionOverlayConfiguration
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
        CameraView.StartDetection();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Stop barcode detection manually
        CameraView.StopDetection();
        CameraView.Handler?.DisconnectHandler();
    }

    void StartScanningButton_Clicked(System.Object sender, System.EventArgs e)
    {
        // Start scanning
        CameraView.StartScanAndCount();
    }

    void ConitueScanningButton_Clicked(System.Object sender, System.EventArgs e)
    {
        CameraView.ContinueScanning();

        StartScanningButton.IsEnabled = true;
        ContinueScanningButton.IsEnabled = false;
    }

    private void CameraView_OnOnBarcodeScanResult(Barcode.Core.BarcodeScannerResult result)
    {
        string text = string.Empty;
        
        if (result.Barcodes.Length > 0)
        {
            foreach (var barcode in result.Barcodes)
            {
                text += $"{barcode.Text} ({barcode.Format.ToString().ToUpper()})\n";
            }
        }

        System.Diagnostics.Debug.WriteLine(text);
        ResultLabel.Text = text;
    }

    private void CameraView_OnOnScanAndCountFinished(Barcode.Core.BarcodeScannerResult result)
    {
        if (result.Success)
        {
            StartScanningButton.IsEnabled = false;
            ContinueScanningButton.IsEnabled = true;
        }
    }
}
