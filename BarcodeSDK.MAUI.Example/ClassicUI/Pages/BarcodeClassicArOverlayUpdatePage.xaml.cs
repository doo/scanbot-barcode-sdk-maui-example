using System.Diagnostics;
using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Core.Barcode;
using ScanbotSDK.MAUI.Example.Utils;

namespace ScanbotSDK.MAUI.Example.ClassicUI.Pages;

public partial class BarcodeClassicArOverlayUpdatePage : BaseComponentPage
{
    public BarcodeClassicArOverlayUpdatePage()
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
        ];

        var overlayConfiguration = new SelectionOverlayConfiguration
        {
            OverlayEnabled = true
        };

        CameraView.OverlayConfiguration = overlayConfiguration;
        
        CameraView.OverlayConfiguration.PolygonConfiguration = new OverlayPolygonConfiguration.StyleForBarcodeItem(OverlayPolygonStyleForBarcode);
        CameraView.OverlayConfiguration.TextConfiguration = new OverlayTextConfiguration.StyleForBarcodeItem(OverlayTextStyleForBarcode, OverlayOverrideTextForBarcode);
    }

    private string OverlayOverrideTextForBarcode(BarcodeItem item)
    {
        return item.Text + "Loading..";
    }

    private bool ApiComplete = false;

    private OverlayTextConfiguration.Style OverlayTextStyleForBarcode(BarcodeItem item)
    {
        if (!BarcodeFormatStyle.Palette.ContainsKey(item.Format)) return new OverlayTextConfiguration.Style();
        
        var color = BarcodeFormatStyle.Palette[item.Format]; 
        return new OverlayTextConfiguration.Style
        {
            TextColor = color,
            TextContainerColor = Colors.Wheat,
            HighlightedTextColor = color
        };
    }

    private OverlayPolygonConfiguration.Style OverlayPolygonStyleForBarcode(BarcodeItem item)
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

    private async void OnSelectBarcodeResult(object sender, BarcodeItem[] barcodeItems)
    {
        if (barcodeItems.Length == 0)
            return;

        string text = string.Empty;
        foreach (var barcode in barcodeItems)
        {
            text += $"{barcode.Text} ({barcode.Format.ToString().ToUpper()})\n";
        }

        System.Diagnostics.Debug.WriteLine(text);
        ResultLabel.Text = text;

        // await CommonUtils.DisplayResultAsync(barcodeItems.ToList());
    }

    private Color[] ApiColors = [Colors.DarkRed, Colors.LightCoral, Colors.MediumPurple, Colors.DarkOrange];
    bool inProgress = false;

    private async void StartArUpdate_Clicked(object sender, EventArgs e)
    {
        
        if (inProgress) return;
        inProgress = true;

        int count = 0;
        foreach (var color in ApiColors)
        {
            UpdateDictionary(color);
            if (count == ApiColors.Length) break;
            count++;
            await Task.Delay(3000); // 3 seconds delay
        }

        inProgress = false;
    }
   
    private void UpdateDictionary(Color color)
    {
        foreach (var item in BarcodeFormatStyle.Palette)
        {
            BarcodeFormatStyle.Palette[item.Key] = color;
        }
    }
}