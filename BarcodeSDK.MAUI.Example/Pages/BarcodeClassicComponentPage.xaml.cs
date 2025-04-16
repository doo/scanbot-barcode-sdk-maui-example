using ScanbotSDK.MAUI.Barcode.Core;

namespace ScanbotSDK.MAUI.Example.Pages
{
    public partial class BarcodeClassicComponentPage : BaseComponentPage
    {
        public BarcodeClassicComponentPage()
        {
            InitializeComponent();
        }

        private void HandleScannerResults(BarcodeItem[] barcodeItems)
        {
            string text = string.Empty;

            if (barcodeItems != null && barcodeItems.Length > 0)
            {
                foreach (var barcode in barcodeItems)
                {
                    text += $"{barcode.Text} ({barcode.Format.ToString().ToUpper()})\n";
                    text += "--------------------------\n";
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

        private void CameraView_OnOnBarcodeScanResult(object cameraView, BarcodeItem[] barcodeItems)
        {
            HandleScannerResults(barcodeItems);
        }
    }
}