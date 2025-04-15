using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScanbotSDK.MAUI.Barcode.Core;

namespace ScanbotSDK.MAUI.Example.Pages;

public partial class BarcodeClassicVisibility : ContentPage
{
    private bool _isNavigatingBack;
    private bool _isScanning;
    public BarcodeClassicVisibility()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CameraView.StartDetection();
        _isScanning = true;
        _isNavigatingBack = true;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (_isNavigatingBack)
        {
            // CameraView.Handler.DisconnectHandler();
        }
    }

    private void Visibility_OnClicked(object sender, EventArgs e)
    {
        CameraView.IsVisible = !CameraView.IsVisible;
    }

    private void StartStop_OnClicked(object sender, EventArgs e)
    {
        if (_isScanning)
        {
            CameraView.StopDetection();
        }
        else
        {
            CameraView.StartDetection();
        }

        _isScanning = !_isScanning;
    }

    private void FreezeUnfreeze_OnClicked(object sender, EventArgs e)
    {
        if (_isScanning)
        {
            CameraView.FreezeCamera();
        }
        else
        {
            CameraView.UnFreezeCamera();
        }
        _isScanning = !_isScanning;
    }

    private void CameraView_OnOnBarcodeScanResult(BarcodeScannerResult result)
    {
       ResultLabel.Text = result.Barcodes.First().Text;
    }

    private void Navigate_OnClicked(object sender, EventArgs e)
    {
        _isNavigatingBack = false;
        Navigation.PushAsync(new BarcodeClassicComponentPage(), true);
    }
}