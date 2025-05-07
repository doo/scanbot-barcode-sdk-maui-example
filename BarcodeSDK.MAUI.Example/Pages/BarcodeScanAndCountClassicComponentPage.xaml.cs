using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Barcode.Core;
using ScanbotSDK.MAUI.Example.Models;

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
        CameraView.BarcodeFormatConfigurations = 
        [
            new BarcodeFormatCommonConfiguration
            {
                Formats = BarcodeFormats.All
            },
                
            // You may add more advanced format configurations like shown below
            // new BarcodeFormatAztecConfiguration
            // {
            //     Gs1Handling = Gs1Handling.DecodeStructure,
            //     AddAdditionalQuietZone = true
            // }
        ];
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

    private void CameraView_OnOnBarcodeScanResult(object sender, BarcodeItem[] barcodeItems)
    {
        if (barcodeItems.Length == 0)
        {
            ResultLabel.Text = "No barcodes captured";
            return;
        }

        string text = string.Empty;
        foreach (var barcode in barcodeItems)
        {
            text += $"{barcode.Text} ({barcode.Format.ToString().ToUpper()})\n";
        }

        ResultLabel.Text = text;
    }

    private void CameraView_OnOnScanAndCountFinished(object sender, BarcodeItem[] barcodeItems)
    {
        StartScanningButton.IsEnabled = false;
        ContinueScanningButton.IsEnabled = true;
    }
}
