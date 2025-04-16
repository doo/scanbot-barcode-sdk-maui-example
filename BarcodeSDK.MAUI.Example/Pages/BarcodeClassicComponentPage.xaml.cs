using ScanbotSDK.MAUI.Barcode.Core;
using ScanbotSDK.MAUI.Example.Models;

namespace ScanbotSDK.MAUI.Example.Pages
{
    public partial class BarcodeClassicComponentPage : BaseComponentPage
    {
        public BarcodeClassicComponentPage()
        {
            InitializeComponent();
            CameraView.BarcodeFormats = BarcodeTypes.Instance.AcceptedTypes.ToList();
        }

        private void HandleScannerResults(BarcodeItem[] barcodeItems)
        {
            if (barcodeItems.Length == 0)
                return;

            string text = string.Empty;
            foreach (var barcode in barcodeItems)
            {
                text += $"{barcode.Text} ({barcode.Format.ToString().ToUpper()})\n";
            }

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

        private void CameraView_OnOnBarcodeScanResult(object sender, BarcodeItem[] barcodeItems)
        {
            HandleScannerResults(barcodeItems);
        }
    }
}