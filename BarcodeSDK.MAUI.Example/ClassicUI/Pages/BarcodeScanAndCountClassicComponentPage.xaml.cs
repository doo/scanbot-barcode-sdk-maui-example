using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Core.Barcode;
using ScanbotSDK.MAUI.Example.Utils;

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

        // CameraView.PolygonConfiguration = new OverlayPolygonConfiguration.Style
        // {
        //     StrokeColor = Colors.Red,
        //     HighlightedStrokeColor = Colors.BlueViolet,
        //     PolygonBackgroundColor = Colors.RosyBrown.WithAlpha(0.5f),
        //     PolygonBackgroundHighlightedColor = Colors.Black.WithAlpha(0.5f),
        // };

        CameraView.PolygonConfiguration = new OverlayPolygonConfiguration.StyleForBarcodeItem(BarcodeItemStyle);
    }

    private OverlayPolygonConfiguration.Style BarcodeItemStyle(BarcodeItem item)
    {
        if (!BarcodeFormatStyle.Palette.ContainsKey(item.Format)) return new OverlayPolygonConfiguration.Style();
        
        var color = BarcodeFormatStyle.Palette[item.Format];
        return new OverlayPolygonConfiguration.Style
        {
            StrokeColor = color,
            HighlightedStrokeColor = color
        };
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
