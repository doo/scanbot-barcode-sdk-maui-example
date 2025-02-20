using System.Reflection;
using BarcodeSDK.NET.iOS.Controllers;
using BarcodeSDK.NET.iOS.Controllers.ClassicComponents;
using BarcodeSDK.NET.iOS.Utils;
using ScanbotSDK.iOS;
using UIKit;

namespace BarcodeSDK.NET.iOS
{   
    public partial class MainViewController
    {
        private void SingleScanning(object _, EventArgs e)
        {   
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
            
            var controller = SBSDKUI2BarcodeScannerViewController.CreateNewWithConfiguration(configuration,
                (viewController, cancelled, error, result) =>
                {
                    if (!cancelled)
                    {
                        viewController.DismissViewController(true, delegate
                        {
                            ShowBarcodeReults(result.Items);
                        });
                    }
                    else
                    {
                        viewController.DismissViewController(true, () => { });
                    }
                });

            PresentViewController(controller, false, null);
        }
        
        private void SingleScanningWithArOverlay(object _, EventArgs e)
        {
            // Create the default configuration object.
            var configuration = new SBSDKUI2BarcodeScannerScreenConfiguration();
            configuration.ScannerConfiguration.BarcodeFormats = BarcodeTypes.Instance.AcceptedTypes;

            var usecases = new SBSDKUI2SingleScanningMode();
            usecases.ConfirmationSheetEnabled = true;
            usecases.ArOverlay.Visible = true;
            usecases.ArOverlay.AutomaticSelectionEnabled = false;

            configuration.UseCase = usecases;      

            var controller = SBSDKUI2BarcodeScannerViewController.CreateNewWithConfiguration(configuration,
                (viewController, cancelled, error, result) =>
                {
                    if (!cancelled)
                    {
                        viewController.DismissViewController(true, delegate
                        {
                            ShowBarcodeReults(result.Items);
                        });
                    }
                    else
                    {
                        viewController.DismissViewController(true, () => { });
                    }
                });


            PresentViewController(controller, false, null);
        }

        private void BatchBarcodeScanning(object _, EventArgs e)
        {
            // Create the default configuration object.
            var configuration = new SBSDKUI2BarcodeScannerScreenConfiguration();
            configuration.ScannerConfiguration.BarcodeFormats = BarcodeTypes.Instance.AcceptedTypes;

            var usecases = new SBSDKUI2MultipleScanningMode();
            usecases.Mode = SBSDKUI2MultipleBarcodesScanningMode.Counting;
            
            configuration.UseCase = usecases;
            
            var controller = SBSDKUI2BarcodeScannerViewController.CreateNewWithConfiguration(configuration,
                (viewController, cancelled, error, result) =>
                {
                    if (!cancelled)
                    {
                        viewController.DismissViewController(true, delegate
                        {
                            ShowBarcodeReults(result.Items);
                        });
                    }
                    else
                    {
                        viewController.DismissViewController(true, () => { });
                    }
                });

            PresentViewController(controller, false, null);
        }

        private void MultipleUniqueBarcodeScanning(object _, EventArgs e)
        {
            var configuration = new SBSDKUI2BarcodeScannerScreenConfiguration();
            configuration.ScannerConfiguration.BarcodeFormats = BarcodeTypes.Instance.AcceptedTypes;
            configuration.UserGuidance.Title.Text = "Please align the QR-/Barcode in the frame above to scan it.";

            var usecases = new SBSDKUI2MultipleScanningMode();
            usecases.Mode = SBSDKUI2MultipleBarcodesScanningMode.Unique;
            usecases.Sheet.Mode = SBSDKUI2SheetMode.CollapsedSheet;
            usecases.SheetContent.ManualCountChangeEnabled = false;
            usecases.ArOverlay.Visible = false;
            usecases.ArOverlay.AutomaticSelectionEnabled = false;
            
            configuration.UseCase = usecases;
            
            var controller = SBSDKUI2BarcodeScannerViewController.CreateNewWithConfiguration(configuration,
                (viewController, cancelled, error, result) =>
                {
                    if (!cancelled)
                    {
                        viewController.DismissViewController(true, delegate
                        {
                           ShowBarcodeReults(result.Items);
                        });
                    }
                    else
                    {
                        viewController.DismissViewController(true, () => { });
                    }
                });

            PresentViewController(controller, false, null);
        }

        private void FindAndPickScanning(object _, EventArgs e)
        {
            var configuration = new SBSDKUI2BarcodeScannerScreenConfiguration();
            
            var usecases = new SBSDKUI2FindAndPickScanningMode();
            usecases.Sheet.Mode = SBSDKUI2SheetMode.CollapsedSheet;
            usecases.Sheet.CollapsedVisibleHeight = SBSDKUI2CollapsedVisibleHeight.Large;
            usecases.SheetContent.ManualCountChangeEnabled = true;
            usecases.ArOverlay.Visible = true;
            usecases.ArOverlay.AutomaticSelectionEnabled = true;
            usecases.ExpectedBarcodes = new SBSDKUI2ExpectedBarcode[] {
                new SBSDKUI2ExpectedBarcode(barcodeValue: "123456", title: "numeric barcode", image: "https://avatars.githubusercontent.com/u/1454920", count: 4),
                new SBSDKUI2ExpectedBarcode(barcodeValue: "SCANBOT", title: "value barcode", image: "https://avatars.githubusercontent.com/u/1454920", count: 4),
            };

            configuration.UseCase = usecases; 
            
            var controller = SBSDKUI2BarcodeScannerViewController.CreateNewWithConfiguration(configuration,
                (viewController, cancelled, error, result) =>
                {
                    if (!cancelled)
                    {
                        viewController.DismissViewController(true, delegate
                        {
                            ShowBarcodeReults(result.Items);
                        });
                    }
                    else
                    {
                        viewController.DismissViewController(true, () => { });
                    }
                });

            PresentViewController(controller, false, null);
        }
        
        private void ShowBarcodeReults(SBSDKUI2BarcodeScannerUIItem[] items)
        {
            var viewController = new ScanResultListController(items);
            NavigationController?.PushViewController(viewController, true);
        }
    }
}
