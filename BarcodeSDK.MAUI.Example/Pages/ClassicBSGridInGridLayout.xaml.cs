using ScanbotSDK.MAUI.Barcode.Core;

namespace ScanbotSDK.MAUI.Example.Pages;

public partial class ClassicBSGridInGridLayout : ContentPage
{
    public ClassicBSGridInGridLayout()
    {
        InitializeComponent();
    }

    private void CameraView_OnOnBarcodeScanResult(object cameraView, BarcodeItem[] barcodeItems)
    {
        ResultLabel.Text = barcodeItems.First().Text;
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        CameraView.IsVisible = !CameraView.IsVisible;
    }
}