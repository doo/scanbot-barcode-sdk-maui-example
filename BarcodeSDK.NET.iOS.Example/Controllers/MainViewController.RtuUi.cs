using System;
using Foundation;
using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public partial class MainViewController
{
    private void SingleScanning(object _, EventArgs e)
    {
        if (!ScanbotSDKGlobal.IsLicenseValid) return;
        
        // Create the default configuration object.
        var configuration = new SBSDKUI2BarcodeScannerScreenConfiguration
        {
            ScannerConfiguration =  new SBSDKBarcodeScannerConfiguration
            {
                BarcodeFormatConfigurations =
                [
                    new SBSDKBarcodeFormatCommonConfiguration
                    {
                        Formats = BarcodeTypes.Instance.AcceptedTypes,
                        Gs1Handling = SBSDKGS1Handling.DecodeStructure
                    }
                ]
            },
            UseCase = new SBSDKUI2SingleScanningMode
            {
                ConfirmationSheetEnabled = true
            }
        };

        // To try some of the snippets, comment out the above and use an existing configuration object from the Snippets class:
        // var configuration =  Snippets.SingleScanningUseCase;
        // Or any other snippet (like MultipleScanningUseCase, FindAndPickUseCase, ArOverlay, etc.)

        SBSDKUI2BarcodeScannerViewController.PresentOn(this, configuration, BarcodeScannerResultHandler);
    }

    private void SingleScanningWithArOverlay(object _, EventArgs e)
    {
        if (!ScanbotSDKGlobal.IsLicenseValid) return;
        
        // Create the default configuration object.
        var configuration = new SBSDKUI2BarcodeScannerScreenConfiguration();
        configuration.ScannerConfiguration.BarcodeFormatConfigurations =
        [
            new SBSDKBarcodeFormatCommonConfiguration
            {
                Formats = BarcodeTypes.Instance.AcceptedTypes
            }
        ];
        
        var usecase = new SBSDKUI2SingleScanningMode();
        usecase.ConfirmationSheetEnabled = true;
        usecase.ArOverlay.Visible = true;
        usecase.ArOverlay.AutomaticSelectionEnabled = false;

        configuration.UseCase = usecase;

        SBSDKUI2BarcodeScannerViewController.PresentOn(this, configuration, BarcodeScannerResultHandler);
    }

    private void BatchBarcodeScanning(object _, EventArgs e)
    {
        if (!ScanbotSDKGlobal.IsLicenseValid) return;
        
        // Create the default configuration object.
        var configuration = new SBSDKUI2BarcodeScannerScreenConfiguration();
        configuration.ScannerConfiguration = new SBSDKBarcodeScannerConfiguration
        {
            BarcodeFormatConfigurations =
            [
                new SBSDKBarcodeFormatCommonConfiguration
                {
                    Formats = BarcodeTypes.Instance.AcceptedTypes,
                    Gs1Handling = SBSDKGS1Handling.DecodeStructure
                }
            ]
        };

        var usecase = new SBSDKUI2MultipleScanningMode();
        usecase.Mode = SBSDKUI2MultipleBarcodesScanningMode.Counting;

        configuration.UseCase = usecase;

        SBSDKUI2BarcodeScannerViewController.PresentOn(this, configuration, BarcodeScannerResultHandler);
    }

    private void MultipleUniqueBarcodeScanning(object _, EventArgs e)
    {
        if (!ScanbotSDKGlobal.IsLicenseValid) return;
        
        var configuration = new SBSDKUI2BarcodeScannerScreenConfiguration();
        configuration.ScannerConfiguration = new SBSDKBarcodeScannerConfiguration
        {
            BarcodeFormatConfigurations =
            [
                new SBSDKBarcodeFormatCommonConfiguration
                {
                    Formats = BarcodeTypes.Instance.AcceptedTypes,
                    Gs1Handling = SBSDKGS1Handling.DecodeStructure
                }
            ]
        };
        configuration.UserGuidance.Title.Text = "Please align the QR-/Barcode in the frame above to scan it.";

        var usecase = new SBSDKUI2MultipleScanningMode();
        usecase.Mode = SBSDKUI2MultipleBarcodesScanningMode.Unique;
        usecase.Sheet.Mode = SBSDKUI2SheetMode.CollapsedSheet;
        usecase.SheetContent.ManualCountChangeEnabled = false;
        usecase.ArOverlay.Visible = true;
        usecase.ArOverlay.AutomaticSelectionEnabled = false;

        configuration.UseCase = usecase;

        SBSDKUI2BarcodeScannerViewController.PresentOn(this, configuration, BarcodeScannerResultHandler);
    }

    private void FindAndPickScanning(object _, EventArgs e)
    {
        if (!ScanbotSDKGlobal.IsLicenseValid) return;
        
        var configuration = new SBSDKUI2BarcodeScannerScreenConfiguration();
        
        var usecase = new SBSDKUI2FindAndPickScanningMode();
        usecase.Sheet.Mode = SBSDKUI2SheetMode.CollapsedSheet;
        usecase.Sheet.CollapsedVisibleHeight = SBSDKUI2CollapsedVisibleHeight.Large;
        usecase.SheetContent.ManualCountChangeEnabled = true;
        usecase.ArOverlay.Visible = true;
        usecase.ArOverlay.AutomaticSelectionEnabled = true;
        usecase.ExpectedBarcodes = [
            new SBSDKUI2ExpectedBarcode(barcodeValue: "123456", title: "numeric barcode", image: "https://avatars.githubusercontent.com/u/1454920", count: 4),
            new SBSDKUI2ExpectedBarcode(barcodeValue: "SCANBOT", title: "value barcode", image: "https://avatars.githubusercontent.com/u/1454920", count: 4)
        ];

        configuration.UseCase = usecase;
        SBSDKUI2BarcodeScannerViewController.PresentOn(this, configuration, BarcodeScannerResultHandler);
    }

    private void BarcodeScannerResultHandler(SBSDKUI2BarcodeScannerViewController viewController, SBSDKUI2BarcodeScannerUIResult result, NSError error)
    {
        // disposes the object after the method scope.
        viewController?.Dispose();
        if (error != null)
        {
            Alert.Show(this, "Alert", error.LocalizedDescription);
            return;
        }
        ShowBarcodeResults(result.Items);
    }

    private void ShowBarcodeResults(SBSDKUI2BarcodeScannerUIItem[] items)
    {
        if (items == null || items.Length == 0)
            return;
        
        var viewController = new ScanResultListController(items);
        NavigationController?.PushViewController(viewController, true);
    }
}