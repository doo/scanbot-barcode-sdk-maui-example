using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScanbotSDK.MAUI.Barcode.Core;

namespace ScanbotSDK.MAUI.Example.Pages;

public partial class ClassicBSStackLayout : ContentPage
{
    public ClassicBSStackLayout()
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