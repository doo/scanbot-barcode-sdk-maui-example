using ScanbotSDK.MAUI.Core.Barcode;

namespace ScanbotSDK.MAUI.Example.ClassicUI.Pages
{
    public partial class BarcodeClassicComponentPage : BaseComponentPage
    {
        public BarcodeClassicComponentPage()
        {
            InitializeComponent();
            CameraView.BarcodeFormatConfigurations =  
            [
                new BarcodeFormatCommonConfiguration
                {
                     Formats = BarcodeFormats.All
                },
                
                // You may add more advanced format configurations like shown below
                // new BarcodeFormatAztecConfiguration
                // {
                //     Gs1Handling = Gs1Handling.DecodeStructure,
                //     AddAdditionalQuietZone = true
                // }
            ];
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
        }

        private void OnBarcodeScanResult(object sender, BarcodeItem[] barcodeItems)
        {
            if (barcodeItems.Length == 0)
                return;

            string text = string.Empty;
            foreach (var barcode in barcodeItems)
            {
                text += $"{barcode.Text} ({barcode.Format.ToString().ToUpper()})\n";
            }

            System.Diagnostics.Debug.WriteLine(text);
            ResultLabel.Text = text;
        }
    }
}