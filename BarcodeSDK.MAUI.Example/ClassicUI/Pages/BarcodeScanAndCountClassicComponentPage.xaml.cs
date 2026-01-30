using ScanbotSDK.MAUI.Core.Barcode;

namespace ScanbotSDK.MAUI.Example.ClassicUI.Pages;

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
    }

    private void OnStartScanningButtonClicked(object sender, EventArgs e)
    {
        // Start scanning
        CameraView.StartScanAndCount();
    }

    private void OnContinueScanningButtonClicked(object sender, EventArgs e)
    {
        CameraView.ContinueScanning();

        StartScanningButton.IsEnabled = true;
        ContinueScanningButton.IsEnabled = false;
    }

    private void OnBarcodeScanResult(object sender, BarcodeItem[] barcodeItems)
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

    private void OnScanAndCountFinished(object sender, BarcodeItem[] barcodeItems)
    {
        StartScanningButton.IsEnabled = false;
        ContinueScanningButton.IsEnabled = true;
    }
}
