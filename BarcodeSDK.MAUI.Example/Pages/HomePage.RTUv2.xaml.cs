﻿using ScanbotSDK.MAUI.Example.Models;
using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Barcode.Core;
using ScanbotSDK.MAUI.Common;
using BarcodeScannerConfiguration = ScanbotSDK.MAUI.Barcode.BarcodeScannerConfiguration;
using ImageSource = Microsoft.Maui.Controls.ImageSource;

namespace ScanbotSDK.MAUI.Example.Pages;

/// <summary>
/// Home Page of the Application
/// </summary>
public partial class HomePage
{
    /// <summary>
    /// Starts the Barcode scanning.
    /// </summary>
    private async Task SingleScanning()
    {
        try
        {
            var result = await ScanbotSDKMain.RTU.BarcodeScanner.LaunchAsync(new BarcodeScannerScreenConfiguration
            {
                ScannerConfiguration = new BarcodeScannerConfiguration
                {
                    BarcodeFormats = BarcodeTypes.Instance.AcceptedTypes,
                    Gs1Handling = Gs1Handling.DecodeStructure
                },
                UseCase = new SingleScanningMode()
                {
                    ConfirmationSheetEnabled = true
                }
            });

            // Comment out the above and use the below to try some of our snippets instead:
            // var result = await ScanbotSDKMain.RTU.BarcodeScanner.LaunchAsync(Snippets.SingleScanningUseCase);
            // Or Snippets.MultipleScanningUseCase, Snippets.FindAndPickUseCase, Snippets.ActionBar, etc.

            await DisplayResults(result);
        }
        catch (TaskCanceledException)
        {
            // for when the user cancels the action
        }
        catch (Exception ex)
        {
            // for any other errors that occur
            Console.WriteLine(ex.Message);
        }
    }

    private async Task SingleScanningWithArOverlay()
    {
        try
        {
            var config = new BarcodeScannerScreenConfiguration();
            var useCase = new SingleScanningMode()
            {
                ArOverlay = new ArOverlayGeneralConfiguration()
                {
                    Visible = true
                },
            };

            useCase.BarcodeInfoMapping = new BarcodeInfoMapping()
            {
                BarcodeItemMapper = new DelegateBarcodeItemMapper((barcodeItem, onResult, onError) =>
                {
                    var title = $"{barcodeItem.Format} {barcodeItem.UpcEanExtension}";
                    var subTitle = "Subtitle";
                    var image = "https://raw.githubusercontent.com/doo/scanbot-sdk-examples/master/sdk-logo.png";

                    if (barcodeItem.UpcEanExtension == "Error occurred!")
                    {
                        onError();
                    }
                    else
                    {
                        onResult(new BarcodeMappedData(title: title, subtitle: subTitle, barcodeImage: image));
                    }
                })
            };

            config.UseCase = useCase;
            config.ScannerConfiguration.BarcodeFormats = BarcodeTypes.Instance.AcceptedTypes;
            var result = await ScanbotSDKMain.RTU.BarcodeScanner.LaunchAsync(config);

            await DisplayResults(result);
        }
        catch (TaskCanceledException)
        {
            // for when the user cancels the action
        }
        catch (Exception ex)
        {
            // for any other errors that occur
            Console.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// Starts the Batch Barcode Scanning.
    /// </summary>
    private async Task BatchBarcodeScanning()
    {
        try
        {
            var result = await ScanbotSDKMain.RTU.BarcodeScanner.LaunchAsync(new BarcodeScannerScreenConfiguration
            {
                ScannerConfiguration = new BarcodeScannerConfiguration
                {
                    BarcodeFormats = BarcodeTypes.Instance.AcceptedTypes,
                },
                UseCase = new MultipleScanningMode
                {
                    Mode = MultipleBarcodesScanningMode.Counting
                }
            });

            await DisplayResults(result);
        }
        catch (TaskCanceledException)
        {
            // for when the user cancels the action
        }
        catch (Exception ex)
        {
            // for any other errors that occur
            Console.WriteLine(ex.Message);
        }
    }

    private async Task MultipleUniqueBarcodeScanning()
    {
        try
        {
            var result = await ScanbotSDKMain.RTU.BarcodeScanner.LaunchAsync(new BarcodeScannerScreenConfiguration
            {
                UseCase = new MultipleScanningMode
                {
                    Mode = MultipleBarcodesScanningMode.Unique,
                    SheetContent = new SheetContent
                    {
                        ManualCountChangeEnabled = false
                    },
                    Sheet = new Sheet
                    {
                        Mode = SheetMode.CollapsedSheet
                    },
                    ArOverlay = new ArOverlayGeneralConfiguration
                    {
                        Visible = true,
                        AutomaticSelectionEnabled = false
                    }
                },
                UserGuidance = new UserGuidanceConfiguration
                {
                    Title = new StyledText { Text = "Please align the QR-/Barcode in the frame above to scan it." }
                }
            });

            await DisplayResults(result);
        }
        catch (TaskCanceledException)
        {
            // for when the user cancels the action
        }
        catch (Exception ex)
        {
            // for any other errors that occur
            Console.WriteLine(ex.Message);
        }
    }

    private async Task FindAndPickScanning()
    {
        try
        {
            var configuration = new BarcodeScannerScreenConfiguration();

            // Initialize the use case for multiple scanning.
            var findAndPickConfig = new FindAndPickScanningMode();

            // Set the sheet mode for the barcodes preview.
            findAndPickConfig.Sheet.Mode = SheetMode.CollapsedSheet;

            // Enable/Disable the automatic selection.
            findAndPickConfig.ArOverlay.AutomaticSelectionEnabled = false;

            // Set the height for the collapsed sheet.
            findAndPickConfig.Sheet.CollapsedVisibleHeight = CollapsedVisibleHeight.Large;

            // Enable manual count change.
            findAndPickConfig.SheetContent.ManualCountChangeEnabled = true;

            // Set the delay before same barcode counting repeat.
            findAndPickConfig.CountingRepeatDelay = 1000;

            // Configure the submit button.
            findAndPickConfig.SheetContent.SubmitButton.Text = "Submit";

            findAndPickConfig.SheetContent.SubmitButton.Foreground.Color = new ColorValue("#000000"); //arg string

            // Set the expected barcodes.
            findAndPickConfig.ExpectedBarcodes = new ExpectedBarcode[]
            {
                new ExpectedBarcode(barcodeValue: "123456", title: "numeric barcode",
                    image: "https://avatars.githubusercontent.com/u/1454920", count: 4),
                new ExpectedBarcode(barcodeValue: "SCANBOT", title: "value barcode",
                    image: "https://avatars.githubusercontent.com/u/1454920", count: 4),
            };

            // Configure other parameters, pertaining to findAndPick-scanning mode as needed.
            configuration.UseCase = findAndPickConfig;
            configuration.ScannerConfiguration.BarcodeFormats = BarcodeTypes.Instance.AcceptedTypes.ToArray();

            var result = await ScanbotSDKMain.RTU.BarcodeScanner.LaunchAsync(configuration);
            await DisplayResults(result);
        }
        catch (TaskCanceledException)
        {
            // for when the user cancels the action
        }
        catch (Exception ex)
        {
            // for any other errors that occur
            Console.WriteLine(ex.Message);
        }
    }

    private async Task DisplayResults(BarcodeScannerUiResult result)
    {
        if (result?.Items?.Length > 0)
        {
            var items = result.Items.Select(item => item.Barcode);
            await Navigation.PushAsync(new BarcodeResultPage(items.ToList()));
        }
    }
}