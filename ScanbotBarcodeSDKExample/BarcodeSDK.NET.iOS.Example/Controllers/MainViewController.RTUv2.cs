using System.Reflection;
using BarcodeSDK.NET.iOS.Controllers;
using BarcodeSDK.NET.iOS.Controllers.ClassicComponents;
using BarcodeSDK.NET.iOS.Utils;
using Scanbot.ImagePicker.iOS;
using ScanbotSDK.iOS;
using UIKit;

namespace BarcodeSDK.NET.iOS
{
    public partial class MainViewController
    {
        private void ShowSingleBarcodeScannerFromRTUUI(object _, EventArgs e)
        {
            // Create the default configuration object.
            var configuration = new SBSDKUI2BarcodeScannerConfiguration();
            
            configuration.RecognizerConfiguration.BarcodeFormats =
                new[] { SBSDKUI2BarcodeFormat.AustraliaPost, SBSDKUI2BarcodeFormat.Aztec };
            
            var usecases = new SBSDKUI2SingleScanningMode{};
            usecases.ConfirmationSheetEnabled = true;
            usecases.ArOverlay.Visible = false;
            usecases.ArOverlay.AutomaticSelectionEnabled = false;

            configuration.UseCase = usecases;
            
            var controller = SBSDKUI2BarcodeScannerViewController.CreateNew(configuration,
                (viewController, cancelled, error, result) =>
                {
                    if (!cancelled)
                    {
                        viewController.DismissViewController(true, delegate
                        {
                            ShowPopup(this, result?.ToJson());
                        });
                    }
                    else
                    {
                        viewController.DismissViewController(true, () => { });
                    }
                });

            PresentViewController(controller, false, null);
        }
        
        private void ShowSingleARBarcodeScannerFromRTUUI(object _, EventArgs e)
        {
            // Create the default configuration object.
            var configuration = new SBSDKUI2BarcodeScannerConfiguration();
            
            var usecases = new SBSDKUI2SingleScanningMode();
            usecases.ConfirmationSheetEnabled = true;
            usecases.ArOverlay.Visible = true;
            usecases.ArOverlay.AutomaticSelectionEnabled = false;

            configuration.UseCase = usecases;
            
            
            var controller = SBSDKUI2BarcodeScannerViewController.CreateNew(configuration,
                (viewController, cancelled, error, result) =>
                {
                    if (!cancelled)
                    {
                        viewController.DismissViewController(true, delegate
                        {
                            ShowPopup(this, result?.ToJson());
                        });
                    }
                    else
                    {
                        viewController.DismissViewController(true, () => { });
                    }
                });

            PresentViewController(controller, false, null);
        }

        private void ShowSingleARAutoSelectBarcodeScannerFromRTUUI(object _, EventArgs e)
        {
            // Create the default configuration object.
            var configuration = new SBSDKUI2BarcodeScannerConfiguration();
            
            var usecases = new SBSDKUI2SingleScanningMode();
            usecases.ConfirmationSheetEnabled = true;
            usecases.ArOverlay.Visible = true;
            usecases.ArOverlay.AutomaticSelectionEnabled = true;
            
            configuration.UseCase = usecases;
            
            var controller = SBSDKUI2BarcodeScannerViewController.CreateNew(configuration,
                (viewController, cancelled, error, result) =>
                {
                    if (!cancelled)
                    {
                        viewController.DismissViewController(true, delegate
                        {
                            ShowPopup(this, result?.ToJson());
                        });
                    }
                    else
                    {
                        viewController.DismissViewController(true, () => { });
                    }
                });

            PresentViewController(controller, false, null);
        }

        private void ShowMultiBarcodeScannerFromRTUUI(object _, EventArgs e)
        {
            // Create the default configuration object.
            var configuration = new SBSDKUI2BarcodeScannerConfiguration();
            
            var usecases = new SBSDKUI2MultipleScanningMode();
            usecases.Mode = SBSDKUI2MultipleBarcodesScanningMode.Unique;
            usecases.Sheet.Mode = SBSDKUI2SheetMode.Button;
            usecases.ArOverlay.Visible = true;
            
            configuration.UseCase = usecases;
            
            var controller = SBSDKUI2BarcodeScannerViewController.CreateNew(configuration,
                (viewController, cancelled, error, result) =>
                {
                    if (!cancelled)
                    {
                        viewController.DismissViewController(true, delegate
                        {
                            ShowPopup(this, result?.ToJson());
                        });
                    }
                    else
                    {
                        viewController.DismissViewController(true, () => { });
                    }
                });

            PresentViewController(controller, false, null);
        }

        private void ShowMultiSheetBarcodeScannerFromRTUUI(object _, EventArgs e)
        {
            var configuration = new SBSDKUI2BarcodeScannerConfiguration();

            configuration.RecognizerConfiguration.BarcodeFormats =
                new[] { SBSDKUI2BarcodeFormat.AustraliaPost, SBSDKUI2BarcodeFormat.Aztec };
            
            var usecases = new SBSDKUI2MultipleScanningMode();
            usecases.Mode = SBSDKUI2MultipleBarcodesScanningMode.Unique;
            usecases.Sheet.Mode = SBSDKUI2SheetMode.CollapsedSheet;
            usecases.ArOverlay.Visible = false;
            
            configuration.UseCase = usecases;
            
            var controller = SBSDKUI2BarcodeScannerViewController.CreateNew(configuration,
                (viewController, cancelled, error, result) =>
                {
                    if (!cancelled)
                    {
                        viewController.DismissViewController(true, delegate
                        {
                            ShowPopup(this, result?.ToJson());
                        });
                    }
                    else
                    {
                        viewController.DismissViewController(true, () => { });
                    }
                });

            PresentViewController(controller, false, null);
        }

        private void ShowMultiSheetARCountAutoSelectBarcodeScannerFromRTUUI(object _, EventArgs e)
        {
            var configuration = new SBSDKUI2BarcodeScannerConfiguration();
            
            var usecases = new SBSDKUI2MultipleScanningMode();
            usecases.Mode = SBSDKUI2MultipleBarcodesScanningMode.Counting;
            usecases.Sheet.Mode = SBSDKUI2SheetMode.CollapsedSheet;
            usecases.Sheet.CollapsedVisibleHeight = SBSDKUI2CollapsedVisibleHeight.Large;
            usecases.SheetContent.ManualCountChangeEnabled = true;
            usecases.ArOverlay.Visible = true;
            usecases.ArOverlay.AutomaticSelectionEnabled = true;
            
            configuration.UseCase = usecases; 
            
            var controller = SBSDKUI2BarcodeScannerViewController.CreateNew(configuration,
                (viewController, cancelled, error, result) =>
                {
                    if (!cancelled)
                    {
                        viewController.DismissViewController(true, delegate
                        {
                            ShowPopup(this, result?.ToJson());
                        });
                    }
                    else
                    {
                        viewController.DismissViewController(true, () => { });
                    }
                });

            PresentViewController(controller, false, null);
        }
        
        private void ConfigurePaletteV2Barcode(SBSDKUI2BarcodeScannerConfiguration configuration)
        {
            // Retrieve the instance of the palette from the configuration object.
            var palette = configuration.Palette;

            // Configure the colors.
            // The palette already has the default colors set, so you don't have to always set all the colors.
            palette.SbColorPrimary = new SBSDKUI2Color(colorString: "#C8193C");
            palette.SbColorPrimaryDisabled = new SBSDKUI2Color(colorString: "#F5F5F5");
            palette.SbColorNegative = new SBSDKUI2Color(colorString: "#FF3737");
            palette.SbColorPositive = new SBSDKUI2Color(colorString: "#4EFFB4");
            palette.SbColorWarning = new SBSDKUI2Color(colorString: "#FFCE5C");
            palette.SbColorSecondary = new SBSDKUI2Color(colorString: "#FFEDEE");
            palette.SbColorSecondaryDisabled = new  SBSDKUI2Color(colorString: "#F5F5F5");
            palette.SbColorOnPrimary = new SBSDKUI2Color(colorString: "#FFFFFF");
            palette.SbColorOnSecondary = new SBSDKUI2Color(colorString: "#C8193C");;
            palette.SbColorSurface = new SBSDKUI2Color(colorString: "#FFFFFF");
            palette.SbColorOutline = new SBSDKUI2Color(colorString: "#EFEFEF");
            palette.SbColorOnSurfaceVariant =new SBSDKUI2Color(colorString: "#707070");
            palette.SbColorOnSurface = new SBSDKUI2Color(colorString: "#000000");
            palette.SbColorSurfaceLow = new SBSDKUI2Color(colorString: "#26000000");
            palette.SbColorSurfaceHigh = new SBSDKUI2Color(colorString: "#7A000000");
            palette.SbColorModalOverlay = new SBSDKUI2Color(colorString: "#A3000000");
            
            // Set the palette in the barcode scanner configuration object.
            configuration.Palette = palette;
        }
    }
}
