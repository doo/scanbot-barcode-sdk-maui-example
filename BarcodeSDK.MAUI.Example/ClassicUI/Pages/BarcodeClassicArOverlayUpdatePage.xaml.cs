using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Core.Barcode;

namespace ScanbotSDK.MAUI.Example.ClassicUI.Pages;

public partial class BarcodeClassicArOverlayUpdatePage : BaseComponentPage
{
    private readonly Dictionary<BarcodeFormat, Color> dictionaryOverlayConfig;

    public BarcodeClassicArOverlayUpdatePage()
    {
        InitializeComponent();
        SetupViews();

        dictionaryOverlayConfig = new Dictionary<BarcodeFormat, Color>
        {
            { BarcodeFormat.Code39, Colors.Red },
            { BarcodeFormat.Itf, Colors.Green },
            { BarcodeFormat.QrCode, Colors.Blue },
            { BarcodeFormat.Code93, Colors.Yellow },
            { BarcodeFormat.Ean8, Colors.DeepPink },
            { BarcodeFormat.Aztec, Colors.MediumPurple },
            { BarcodeFormat.Code128, Colors.SaddleBrown },
            { BarcodeFormat.Ean13, Colors.LightCoral },
            { BarcodeFormat.Pdf417, Colors.Aqua },
            { BarcodeFormat.Codabar, Colors.Black },
            { BarcodeFormat.UpcA, Colors.SlateBlue },
            { BarcodeFormat.DataMatrix, Colors.Gray },
            { BarcodeFormat.UpcE, Colors.MediumSpringGreen }
        };
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
            Enabled = true,
            StrokeColor = Colors.DarkRed,
            TextColor = Colors.WhiteSmoke,
            TextContainerColor = Colors.Black,

            HighlightedStrokeColor = Colors.Yellow,
            HighlightedTextColor = Colors.Black,
            HighlightedTextContainerColor = Colors.WhiteSmoke
        };

        CameraView.OverlayConfiguration = overlayConfiguration;

        CameraView.OverlayPolygonStyleForBarcode = OverlayPolygonStyleForBarcode;
        CameraView.OverlayTextStyleForBarcode = OverlayTextStyleForBarcode;
        CameraView.OverlayOverrideTextForBarcode = OverlayOverrideTextForBarcode;
    }

    private string OverlayOverrideTextForBarcode(BarcodeItem item)
    {
        return item.Text + "Loading..";
    }

    private OverlayTextStyle OverlayTextStyleForBarcode(BarcodeItem item)
    {
        if (!dictionaryOverlayConfig.ContainsKey(item.Format)) return null;
        
        var color = dictionaryOverlayConfig[item.Format]; 
        return new OverlayTextStyle
        {
            TextColor = color,
            TextContainerColor = Colors.WhiteSmoke,
            HighlightedTextColor = color
        };
    }

    private OverlayPolygonStyle OverlayPolygonStyleForBarcode(BarcodeItem item)
    {
        if (!dictionaryOverlayConfig.ContainsKey(item.Format)) return null;
        
        var color = dictionaryOverlayConfig[item.Format];
        return new OverlayPolygonStyle
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
    }
   
    private void UpdateDictionary(Color color)
    {
        foreach (var item in dictionaryOverlayConfig)
        {
            dictionaryOverlayConfig[item.Key] = color;
        }
    }
}