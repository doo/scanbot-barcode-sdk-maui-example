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

    private void HandleScannerResults(RTU.v1.BarcodeResultBundle result)
    {
        string text = string.Empty;

        if (result?.Barcodes != null)
        {
            foreach (var barcode in result.Barcodes)
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

    private void CameraView_OnOnSelectBarcodeResult(RTU.v1.BarcodeResultBundle result)
    {
        HandleScannerResults(result);
    }

    private void StartCameraBtn_OnClicked(object sender, EventArgs e)
    {
        cameraView.IsVisible = !cameraView.IsVisible;
    }
}
