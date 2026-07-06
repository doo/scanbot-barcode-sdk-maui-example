using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Core.Barcode;

namespace ScanbotSDK.MAUI.Example.ClassicUI.Pages
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

            CameraView.BarcodeFormatConfigurations =
            [
                new BarcodeFormatCommonConfiguration { Formats = BarcodeFormats.All },

                // You may add more advanced format configurations like shown below
                // new BarcodeFormatAztecConfiguration
                // {
                //     Gs1Handling = Gs1Handling.DecodeStructure,
                //     AddAdditionalQuietZone = true
                // }
            ];

            CameraView.OverlayConfiguration = new SelectionOverlayConfiguration
            {
                PolygonConfiguration = new OverlayPolygonConfiguration.Style
                    {
                        StrokeColor = Colors.Yellow,
                        HighlightedStrokeColor = Colors.Red,
                        PolygonColor = Colors.Transparent,
                        HighlightedPolygonColor = Colors.DarkOrchid,
                    },
                TextConfiguration = new OverlayTextConfiguration.Style
                {
                    TextFormat = BarcodeTextFormat.CodeAndType,
                    TextColor = Colors.Yellow,
                    TextContainerColor = Colors.Black,
                    HighlightedTextColor = Colors.Red,
                    HighlightedTextContainerColor = Colors.Black,
                }
            };
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
        
        private void OnSelectBarcodeResult(object sender, BarcodeItem[] barcodeItems)
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