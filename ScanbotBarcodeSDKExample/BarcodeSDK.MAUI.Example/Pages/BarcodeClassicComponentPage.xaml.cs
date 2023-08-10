﻿using BarcodeSDK.MAUI.Configurations;
using BarcodeSDK.MAUI.Constants;
using BarcodeSDK.MAUI.Models;

namespace BarcodeSDK.MAUI.Example.Pages;

public partial class BarcodeClassicComponentPage : ContentPage
{
    public bool IsLicenseValid => ScanbotBarcodeSDK.LicenseInfo.IsValid;

    public BarcodeClassicComponentPage()
	{
		InitializeComponent();
        SetupViews();
    }

    private void SetupViews()
    {
        cameraView.OnBarcodeScanResult = (result) =>
        {
            string text = string.Empty;
            foreach (Barcode barcode in result.Barcodes)
            {
                text += string.Format("{0} ({1})\n", barcode.Text, barcode.Format.ToString().ToUpper());
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                System.Diagnostics.Debug.WriteLine(text);
                lblResult.Text = text;
            });
        };
        cameraView.OverlayConfiguration = new SelectionOverlayConfiguration(true, OverlayFormat.CodeAndType,
                                                                            Colors.Yellow, Colors.Yellow, Colors.Black,
                                                                            Colors.Red, Colors.Red, Colors.Black);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (IsLicenseValid)
        {
            ShowTrialLicenseAlert();
        }
        else
        {
            ShowExpiredLicenseAlert();
        }
        cameraView.HeightRequest = (DeviceDisplay.Current.MainDisplayInfo.Height / DeviceDisplay.Current.MainDisplayInfo.Density) * 0.6;

        if (DevicePlatform.Android == DeviceInfo.Platform)
        {
            cameraView.StartDetection();
        }
    }

    private void ShowExpiredLicenseAlert()
    {
        DisplayAlert("Error", "Your SDK license has expired", "Close");
    }

    private void ShowTrialLicenseAlert()
    {
        DisplayAlert("Welcome", "You are using the Trial SDK License. The SDK will be active for one minute.", "Close");
    }
}
