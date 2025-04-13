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
            ScannerConfiguration = new SBSDKUI2BarcodeScannerConfiguration
            {
                BarcodeFormats = BarcodeTypes.Instance.AcceptedTypes,
                Gs1Handling = SBSDKGS1Handling.DecodeStructure
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
        configuration.ScannerConfiguration.BarcodeFormats = BarcodeTypes.Instance.AcceptedTypes;

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
        configuration.ScannerConfiguration.BarcodeFormats = BarcodeTypes.Instance.AcceptedTypes;

        var usecase = new SBSDKUI2MultipleScanningMode();
        usecase.Mode = SBSDKUI2MultipleBarcodesScanningMode.Counting;

        configuration.UseCase = usecase;

        SBSDKUI2BarcodeScannerViewController.PresentOn(this, configuration, BarcodeScannerResultHandler);
    }

    private void MultipleUniqueBarcodeScanning(object _, EventArgs e)
    {
        if (!ScanbotSDKGlobal.IsLicenseValid) return;
        
        var configuration = new SBSDKUI2BarcodeScannerScreenConfiguration();
        configuration.ScannerConfiguration.BarcodeFormats = BarcodeTypes.Instance.AcceptedTypes;
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
            new SBSDKUI2ExpectedBarcode(barcodeValue: "123456", title: "numeric barcode", image: "https://avatars.githubusercontent.com/u/1454920", count: new IntPtr(4)),
            new SBSDKUI2ExpectedBarcode(barcodeValue: "SCANBOT", title: "value barcode", image: "https://avatars.githubusercontent.com/u/1454920", count: new IntPtr(4))
        ];

        configuration.UseCase = usecase;
        SBSDKUI2BarcodeScannerViewController.PresentOn(this, configuration, BarcodeScannerResultHandler);
    }

    private void BarcodeScannerResultHandler(SBSDKUI2BarcodeScannerViewController viewController, bool cancelled, NSError error, SBSDKUI2BarcodeScannerUIResult result)
    {
        // disposes the object after the method scope.
        using var disposableViewController = viewController;
        if (!cancelled)
        {
            disposableViewController?.PresentingViewController?.DismissViewController(true, delegate
            {
                ShowBarcodeResults(result.Items);
            });
        }
        else
        {
            disposableViewController?.PresentingViewController?.DismissViewController(true, null);
        }
    }

    private void ShowBarcodeResults(SBSDKUI2BarcodeScannerUIItem[] items)
    {
        var viewController = new ScanResultListController(items);
        NavigationController?.PushViewController(viewController, true);
    }
}