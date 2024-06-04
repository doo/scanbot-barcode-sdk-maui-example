using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using IO.Scanbot.Sdk.Ui_v2.Barcode;
using IO.Scanbot.Sdk.Ui_v2.Common;

namespace BarcodeSDK.NET.Droid
{
    public partial class MainActivity : Activity
    {
        private void MultipleScanningUseCaseSnippet(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            
            var intent = BarcodeScannerActivity.NewIntent(this, new BarcodeScannerConfiguration 
            {
                UseCase = new MultipleScanningMode()
                {
                    Mode = MultipleBarcodesScanningMode.Unique,
                    Sheet = new Sheet()
                    {
                        Mode = SheetMode.CollapsedSheet,
                        CollapsedVisibleHeight = CollapsedVisibleHeight.Small
                    },
                    ArOverlay = new ArOverlayGeneralConfiguration()
                    {
                        Visible = true,
                        AutomaticSelectionEnabled = false
                    }
                }
            });
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE_V2);
        }
        
        private void MultipleScanningPreviewConfigSnippet(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            
            var intent = BarcodeScannerActivity.NewIntent(this, new BarcodeScannerConfiguration
            {
                UseCase = new MultipleScanningMode()
                {
                    Sheet = new Sheet()
                    {
                        Mode = SheetMode.CollapsedSheet,
                        CollapsedVisibleHeight = CollapsedVisibleHeight.Large,
                    },
                    SheetContent = new SheetContent()
                    {   
                        SubmitButton = new ButtonConfiguration()
                        {
                            Text = "Submit",
                            Foreground = new ForegroundStyle()
                            {
                                Color = new ScanbotColor("#000000")
                            },
                        }
                    },
                }
            });
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE_V2);
        }

        private void ArOverlayUseCaseSnippet(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            
            var intent = BarcodeScannerActivity.NewIntent(this, new BarcodeScannerConfiguration
            {
                UseCase = new MultipleScanningMode()
                {
                    Mode = MultipleBarcodesScanningMode.Unique,
                    Sheet = new Sheet()
                    {
                        CollapsedVisibleHeight = CollapsedVisibleHeight.Large,
                    },
                    ArOverlay = new ArOverlayGeneralConfiguration()
                    {
                        Visible = true,
                        AutomaticSelectionEnabled = false
                    }
                }
            });
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE_V2);
        }
    }
}