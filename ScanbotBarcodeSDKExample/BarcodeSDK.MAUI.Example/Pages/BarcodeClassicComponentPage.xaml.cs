namespace ScanbotSDK.MAUI.Example.Pages
{
    public partial class BarcodeClassicComponentPage : BaseComponentPage
    {
        const string StartScanner = "Start Scanner";
        const string StopScanner = "Stop Scanner";
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
                }
            }

            System.Diagnostics.Debug.WriteLine(text);
            lblResult.Text = text;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            CameraBtn.Text = cameraView.IsVisible ? StopScanner : StartScanner;

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

        void ToggleCameraPreview_Clicked(System.Object sender, System.EventArgs e)
        {
            cameraView.IsVisible = !cameraView.IsVisible;
            CameraBtn.Text = cameraView.IsVisible ? StopScanner : StartScanner;
        }
    }
}