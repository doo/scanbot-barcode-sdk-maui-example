using ScanbotSDK.MAUI.Barcode.Core;

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
            CameraView.OverlayConfiguration = new Barcode.SelectionOverlayConfiguration(
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

        private void HandleScannerResults(BarcodeItem[] barcodeItems)
        {
            string text = string.Empty;
            if (barcodeItems != null && barcodeItems.Length > 0)
            {
                foreach (var barcode in barcodeItems)
                {
                    text += $"{barcode.Text} ({barcode.Format.ToString().ToUpper()})\n";
                }
            }

            System.Diagnostics.Debug.WriteLine(text);
            ResultLabel.Text = text;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Start barcode detection manually
            CameraView.StartDetection();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // Stop barcode detection manually
            CameraView.StopDetection();
            CameraView.Handler?.DisconnectHandler();
        }
        
        private void CameraView_OnSelectBarcodeResult(object cameraView, BarcodeItem[] barcodeItems)
        {
            HandleScannerResults(barcodeItems);
        }
    }
}