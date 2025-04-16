namespace ScanbotSDK.MAUI.Example.Pages;

public partial class PageTest5 : ContentPage
{
    public PageTest5()
    {
        InitializeComponent();
        SetupViews();
    }

    private void SetupViews()
    {
       
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
