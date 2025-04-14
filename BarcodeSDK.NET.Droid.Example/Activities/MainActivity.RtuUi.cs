using Android.Content;
using BarcodeSDK.NET.Droid.Activities;
using IO.Scanbot.Sdk.Barcode;
using IO.Scanbot.Sdk.Ui_v2.Common;
using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using IO.Scanbot.Sdk.Ui_v2.Barcode;
using BarcodeScannerConfiguration = IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration.BarcodeScannerConfiguration;

namespace BarcodeSDK.NET.Droid
{
    public partial class MainActivity
    {
        BarcodeScannerActivity.ResultContract resultContract;
        
        private void SingleScanning(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }

            resultContract = new BarcodeScannerActivity.ResultContract();
            var intent = resultContract.CreateIntent(this, new BarcodeScannerScreenConfiguration
            {
                ScannerConfiguration = new BarcodeScannerConfiguration
                {
                    BarcodeFormats = BarcodeFormats.All,
                    Gs1Handling = Gs1Handling.DecodeStructure
                },
                UseCase = new SingleScanningMode()
                {
                    ConfirmationSheetEnabled = true
                }
            });

            // To try some of the snippets, comment out the above and create an intent with:
            // var intent = BarcodeScannerActivity.NewIntent(this, Snippets.SingleScanningUseCase);
            // Or any other snippet (like MultipleScanningUseCase, FindAndPickUseCase, ArOverlay, etc.)

            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE);
        }

        private void SingleScanningWithArOverlay(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }

            var useCase = new SingleScanningMode();
            useCase.ArOverlay.Visible = true;

            resultContract = new BarcodeScannerActivity.ResultContract();
            var intent = resultContract.CreateIntent(this, new BarcodeScannerScreenConfiguration
            {
                UseCase = useCase
            });

            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE);
        }

        private void BatchBarcodeScanning(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            
            resultContract = new BarcodeScannerActivity.ResultContract();
            var intent = resultContract.CreateIntent(this, new BarcodeScannerScreenConfiguration 
            {
                ScannerConfiguration = new BarcodeScannerConfiguration
                {
                    BarcodeFormats = BarcodeFormats.All,           
                },
                UseCase = new MultipleScanningMode
                {
                    Mode = MultipleBarcodesScanningMode.Counting
                }
            });
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE);
        }

        private void MultipleUniqueBarcodeScanning(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }
            
            var useCase = new MultipleScanningMode();
            useCase.Mode = MultipleBarcodesScanningMode.Unique;
            useCase.Sheet.Mode = SheetMode.CollapsedSheet;
            useCase.SheetContent.ManualCountChangeEnabled = false;
            useCase.ArOverlay.Visible = true;
            useCase.ArOverlay.AutomaticSelectionEnabled = false;

            resultContract = new BarcodeScannerActivity.ResultContract();
            var intent = resultContract.CreateIntent(this, new BarcodeScannerScreenConfiguration
            {
                UseCase = useCase,
                UserGuidance = new UserGuidanceConfiguration
                {
                    Title = new StyledText{ Text = "Please align the QR-/Barcode in the frame above to scan it." }
                }
            });
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE);
        }

        private void FindAndPickScanning(object sender, EventArgs e)
        {
            if (!Alert.CheckLicense(this, SDK))
            {
                return;
            }

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
            
            findAndPickConfig.SheetContent.SubmitButton.Foreground.Color = new ScanbotColor("#000000"); //arg string

            // Set the expected barcodes.
            findAndPickConfig.ExpectedBarcodes = new List<ExpectedBarcode>
            {
                new (barcodeValue: "123456", title: "numeric barcode", image: "https://avatars.githubusercontent.com/u/1454920", count: 4),
                new (barcodeValue: "SCANBOT", title: "value barcode", image: "https://avatars.githubusercontent.com/u/1454920", count: 4),
            };

            // Configure other parameters, pertaining to findAndPick-scanning mode as needed.
            configuration.UseCase = findAndPickConfig;
            configuration.ScannerConfiguration.BarcodeFormats = BarcodeFormats.All;

            resultContract = new BarcodeScannerActivity.ResultContract();
            var intent = resultContract.CreateIntent(this, configuration);
            StartActivityForResult(intent, BARCODE_DEFAULT_UI_REQUEST_CODE);
        }
        
        private void OnRTUActivityResult(BarcodeScannerResult barcode)
        {
            var intent = new Intent(this, typeof(BarcodeResultActivity));
            var bundle = new BaseBarcodeResult<BarcodeScannerResult>(barcode).ToBundle();
            intent.PutExtra("BarcodeResult", bundle);

            StartActivity(intent);
        }
    }
}