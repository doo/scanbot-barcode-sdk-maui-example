using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScanbotSDK.MAUI.Barcode.Core;

namespace ScanbotSDK.MAUI.Example.Pages;

public partial class ClassicBSStackInGridLayout : ContentPage
{
    public ClassicBSStackInGridLayout()
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