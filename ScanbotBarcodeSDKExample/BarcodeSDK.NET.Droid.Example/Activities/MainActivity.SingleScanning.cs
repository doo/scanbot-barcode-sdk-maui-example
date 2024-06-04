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
        private void SingleScanningUseCaseSnippet(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            
            var intent = BarcodeScannerActivity.NewIntent(this, new BarcodeScannerConfiguration
            {
                UseCase = new SingleScanningMode()
                {
                    // Enable and configure the confirmation sheet.
                    ConfirmationSheetEnabled = true,
                    SheetColor = new ScanbotColor("#FFFFFF"),
                        
                    // Hide/unhide the barcode image.
                    BarcodeImageVisible = true,
                        
                    // Configure the barcode title of the confirmation sheet.
                    BarcodeTitle = new StyledText()
                    {
                        Visible = true,
                        Color = new ScanbotColor("#000000"),
                    },
                        
                    // Configure the barcode subtitle of the confirmation sheet.
                    BarcodeSubtitle = new StyledText()
                    {   
                        Visible = true,
                        Color = new ScanbotColor("#000000"),
                    },
                        
                    // Configure the cancel button of the confirmation sheet.
                    CancelButton = new ButtonConfiguration()
                    {
                        Text = "Close",
                        Foreground = new ForegroundStyle()
                        {
                            Color = new ScanbotColor("#C8193C"),
                        },
                        Background = new BackgroundStyle()
                        {
                            FillColor = new ScanbotColor("#00000000"),
                        },
                    },
                    // Configure the submit button of the confirmation sheet.
                    SubmitButton = new ButtonConfiguration()
                    {   
                        Text = "Submit",
                        Foreground = new ForegroundStyle()
                        {
                            Color = new ScanbotColor("#FFFFFF"),
                        },
                        Background = new BackgroundStyle()
                        {
                            FillColor = new ScanbotColor("#C8193C")
                        }
                    },
                },
                RecognizerConfiguration = new BarcodeRecognizerConfiguration
                {
                    BarcodeFormats = BarcodeFormat.Values().ToList()
                }
            // Configure other parameters, pertaining to single-scanning mode as needed.
            });
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE_V2);
        }

        
        private void ItemMappingConfigSnippet(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            
            var intent = BarcodeScannerActivity.NewIntent(this, new BarcodeScannerConfiguration
            {
                UseCase = new SingleScanningMode()
                {
                    BarcodeInfoMapping = new BarcodeInfoMapping()
                    {
                        BarcodeItemMapper = new CustomMapper()
                    }
                }
            });
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE_V2);
        }

        private class CustomMapper : global::Java.Lang.Object, global::Java.IO.ISerializable, IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration.IBarcodeItemMapper
        {
            public void MapBarcodeItem(BarcodeItem barcodeItem, IBarcodeMappingResult result)
            {
                var title = $"Some product {barcodeItem.TextWithExtension}";
                var subTitle = barcodeItem.Type.Name();
                var image = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png";

                if (barcodeItem.TextWithExtension == "Error occurred!")
                {
                    result.OnError();
                }
                else
                {
                    result.OnResult(new BarcodeMappedData(title: title, subtitle: subTitle, barcodeImage: image));
                }
            }
        }

        private void ActionBarConfigSnippet(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            
            var configuration = new BarcodeScannerConfiguration();
                
            configuration.ActionBar.FlashButton.Visible = true;
            configuration.ActionBar.FlashButton.BackgroundColor = new ScanbotColor("#7A000000");
            configuration.ActionBar.FlashButton.ForegroundColor = new ScanbotColor("#FFFFFF");
            configuration.ActionBar.FlashButton.ActiveBackgroundColor = new ScanbotColor("#FFCE5C");
            configuration.ActionBar.FlashButton.ActiveForegroundColor = new ScanbotColor("#000000");
                
            configuration.ActionBar.ZoomButton.Visible = true;
            configuration.ActionBar.ZoomButton.BackgroundColor = new ScanbotColor("#7A000000");
            configuration.ActionBar.ZoomButton.ForegroundColor = new ScanbotColor("#FFFFFF");
                
            configuration.ActionBar.FlipCameraButton.Visible = true;
            configuration.ActionBar.FlipCameraButton.BackgroundColor = new ScanbotColor("#7A000000");
            configuration.ActionBar.FlipCameraButton.ForegroundColor = new ScanbotColor("#FFFFFF");

            var intent = BarcodeScannerActivity.NewIntent(this, configuration);
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE_V2);
        }
        
        private void PaletteConfigSnippet(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            
            var intent = BarcodeScannerActivity.NewIntent(this, new BarcodeScannerConfiguration
            {
                // Simply alter one color and keep the other default.
                // Palette = new Palette()
                // {
                //     SbColorPrimary = new ScanbotColor("#c86e19"),
                // }
                // ... or set an entirely new palette.
                Palette = new Palette()
                {
                    SbColorPrimary = new ScanbotColor("#C8193C"),
                    SbColorPrimaryDisabled = new ScanbotColor("#F5F5F5"),
                    SbColorNegative = new ScanbotColor("#FF3737"),
                    SbColorPositive = new ScanbotColor("#4EFFB4"),
                    SbColorWarning = new ScanbotColor("#FFCE5C"),
                    SbColorSecondary = new ScanbotColor("#FFEDEE"),
                    SbColorSecondaryDisabled = new ScanbotColor("#F5F5F5"),
                    SbColorOnPrimary = new ScanbotColor("#FFFFFF"),
                    SbColorOnSecondary = new ScanbotColor("#C8193C"),
                    SbColorSurface = new ScanbotColor("#FFFFFF"),
                    SbColorOutline = new ScanbotColor("#EFEFEF"),
                    SbColorOnSurfaceVariant = new ScanbotColor("#707070"),
                    SbColorOnSurface = new ScanbotColor("#000000"),
                    SbColorSurfaceLow = new ScanbotColor("#26000000"),
                    SbColorSurfaceHigh = new ScanbotColor("#7A000000"),
                    SbColorModalOverlay = new ScanbotColor("#A3000000"),
                }
            });
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE_V2);
        }

        private void UserGuidanceConfigSnippet(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            
            var intent = BarcodeScannerActivity.NewIntent(this, new BarcodeScannerConfiguration
            {
                UserGuidance = new UserGuidanceConfiguration()
                {
                    // Hide/unhide the user guidance.
                    Visible = true,
                    
                    // Configure the title.
                    Title = new StyledText()
                    {
                        Text = "Move the finder over a barcode",
                        Color = new ScanbotColor("#FFFFFF"),
                    },
                }  
            });
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE_V2);
        }

        private void TopBarConfigSnippet(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            
            var intent = BarcodeScannerActivity.NewIntent(this, new BarcodeScannerConfiguration
            {
                TopBar = new TopBarConfiguration()
                {
                    // Configure the top bar.
                    // Set the top bar mode.
                    Mode = TopBarMode.Gradient,
                    BackgroundColor = new ScanbotColor("#C8193C"),
                        
                    // Configure the status bar look.
                    StatusBarMode = StatusBarMode.Hidden,
                        
                    // Configure the Cancel button.
                    CancelButton = new ButtonConfiguration()
                    {   
                        Text = "Cancel",
                        Foreground = new ForegroundStyle()
                        {
                            Color = new ScanbotColor("#FFFFFF"),
                        }
                    },
                }
            });
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE_V2);
        }
    }
}