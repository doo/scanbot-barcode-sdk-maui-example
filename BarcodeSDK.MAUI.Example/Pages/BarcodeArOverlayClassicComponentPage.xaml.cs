using ScanbotSDK.MAUI.Barcode.RTU.v1;

namespace ScanbotSDK.MAUI.Example.Pages
{
    public partial class BarcodeArOverlayClassicComponentPage : BaseComponentPage
    {
        public BarcodeArOverlayClassicComponentPage()
        {
            InitializeComponent();
            SetupViews();
        }

        private void SetupViews()
        {
            cameraView.OverlayConfiguration = new Barcode.RTU.v1.SelectionOverlayConfiguration(
                automaticSelectionEnabled: false,
                overlayFormat: BarcodeTextFormat.CodeAndType,
                textColor: Colors.Yellow,
                textContainerColor: Colors.Black,
                strokeColor: Colors.Yellow,
                highlightedStrokeColor: Colors.Red,
                highlightedTextColor: Colors.Yellow,
                highlightedTextContainerColor: Colors.DarkOrchid,
                polygonBackgroundColor: Colors.Transparent,
                polygonBackgroundHighlightedColor: Colors.Transparent);
        }

        private void HandleScannerResults(Barcode.RTU.v1.BarcodeResultBundle result)
        {
            string text = string.Empty;

            if (result?.Barcodes != null)
            {
                foreach (var barcode in result.Barcodes)
                {
                    text += $"{barcode.Text} ({barcode.Format.ToString().ToUpper()})\n";
                }
            }

            System.Diagnostics.Debug.WriteLine(text);
            lblResult.Text = text;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Start barcode detection manually
            cameraView.StartDetection();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // Stop barcode detection manually
            cameraView.StopDetection();
            cameraView.Handler?.DisconnectHandler();
        }
        
        private void CameraView_OnOnSelectBarcodeResult(Barcode.RTU.v1.BarcodeResultBundle result)
        {
            HandleScannerResults(result);
        }
    }
}