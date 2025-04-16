namespace ScanbotSDK.MAUI.Example.Pages;

public partial class PageTest4 : ContentPage
{
    public PageTest4()
    {
        InitializeComponent();
        SetupViews();
    }

    private void SetupViews()
    {
        cameraView.OverlayConfiguration = new Barcode.SelectionOverlayConfiguration(
            automaticSelectionEnabled: false,
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

    private void HandleScannerResults(Barcode.Core.BarcodeItem[] barcodeItems)
    {
        string text = string.Empty;

        if (barcodeItems.Length > 0)
        {
            foreach (var barcode in barcodeItems)
            {
                text += $"{barcode.Text} ({barcode.Format.ToString().ToUpper()})\n";
            }
        }

        System.Diagnostics.Debug.WriteLine(text);
        lblResult.Text = text;
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
        cameraView.Handler?.DisconnectHandler();
    }

    private void CameraView_OnSelectBarcodeResult(object sender, Barcode.Core.BarcodeItem[] barcodeItems)
    {
        HandleScannerResults(barcodeItems);
    }

    private void StartCameraBtn_OnClicked(object sender, EventArgs e)
    {
        cameraView.IsVisible = !cameraView.IsVisible;
    }
}