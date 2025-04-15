using ScanbotSDK.MAUI.Barcode.Core;

namespace ScanbotSDK.MAUI.Example.Pages;

public partial class ClassicBSGridInGridLayout : ContentPage
{
    public ClassicBSGridInGridLayout()
    {
        InitializeComponent();
    }

    private void CameraView_OnOnBarcodeScanResult(BarcodeScannerResult result)
    {
        ResultLabel.Text = result.Barcodes.First().Text;
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        CameraView.IsVisible = !CameraView.IsVisible;
    }
}