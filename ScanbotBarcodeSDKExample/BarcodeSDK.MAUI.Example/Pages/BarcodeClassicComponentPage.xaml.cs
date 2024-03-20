using ScanbotSDK.MAUI.Configurations;
using ScanbotSDK.MAUI.Constants;
using ScanbotSDK.MAUI.Models;

namespace ScanbotSDK.MAUI.Example.Pages
{
    public partial class BarcodeClassicComponentPage : BaseComponentPage
    {
        public BarcodeClassicComponentPage()
        {
            InitializeComponent();
        }
        
        private void HandleScannerResults(BarcodeResultBundle result)
        {
            string text = string.Empty;

            if (result?.Barcodes != null)
            {
                foreach (Barcode barcode in result.Barcodes)
                {
                    text += $"{barcode.Text} ({barcode.Format.ToString().ToUpper()})\n";
                }
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                System.Diagnostics.Debug.WriteLine(text);
                lblResult.Text = text;
            });
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
        }

        private void CameraView_OnOnBarcodeScanResult(BarcodeResultBundle result)
        {
            HandleScannerResults(result);
        }
    }
}