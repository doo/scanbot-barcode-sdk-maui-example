namespace ScanbotSDK.MAUI.Example.Pages
{
    public partial class BarcodeClassicComponentPage : BaseComponentPage
    {
        public BarcodeClassicComponentPage()
        {
            InitializeComponent();
        }

        private void HandleScannerResults(Barcode.Core.BarcodeItem[] barcodeItems)
        {
            string text = string.Empty;

            if (barcodeItems.Length > 0)
            {
                foreach (var barcode in barcodeItems)
                {
                    text += $"{barcode.Text} ({barcode.Format.ToString().ToUpper()})\n";
                    text += "--------------------------\n";
                }
            }

            System.Diagnostics.Debug.WriteLine(text);
            lblResult.Text = text;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
  cameraView.OverlayConfiguration = new ScanbotSDK.MAUI.Barcode.SelectionOverlayConfiguration(
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

        private void CameraView_OnBarcodeScanResult(object sender, Barcode.Core.BarcodeItem[] barcodeItems)
        {
            HandleScannerResults(barcodeItems);
        }
    }
}