namespace ScanbotSDK.MAUI.Example.Pages
{
    public partial class BarcodeClassicComponentPage : BaseComponentPage
    {
        public BarcodeClassicComponentPage()
        {
            InitializeComponent();
        }

        private void HandleScannerResults(RTU.v1.BarcodeResultBundle result)
        {
            string text = string.Empty;

            if (result?.Barcodes != null)
            {
                foreach (var barcode in result.Barcodes)
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

        private void CameraView_OnOnBarcodeScanResult(RTU.v1.BarcodeResultBundle result)
        {
            HandleScannerResults(result);
        }
    }
}