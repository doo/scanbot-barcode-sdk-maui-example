using System.Diagnostics;
using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Core.Barcode;
using ScanbotSDK.MAUI.Example.Utils;

namespace ScanbotSDK.MAUI.Example.ClassicUI.Pages
{
    public partial class BarcodeArOverlayClassicComponentPage : BaseComponentPage
    {
        public SelectionOverlayConfiguration OverlayConfiguration;
        
        public BarcodeArOverlayClassicComponentPage()
        {
            InitializeComponent();
            SetupViews();
        }

        private void SetupViews()
        {
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
            
            OverlayConfiguration = new SelectionOverlayConfiguration
            {
                OverlayEnabled = false,
                PolygonConfiguration = new OverlayPolygonConfiguration.Style
                {
                    StrokeColor = Colors.DarkRed,
                    HighlightedStrokeColor = Colors.Yellow,
                    
                },
                TextConfiguration = new OverlayTextConfiguration.Style
                {
                    TextColor = Colors.WhiteSmoke,
                    TextContainerColor = Colors.Black,
                    HighlightedTextColor = Colors.Black,
                    HighlightedTextContainerColor = Colors.WhiteSmoke
                }
            };
            
            CameraView.OverlayConfiguration = OverlayConfiguration;
            CameraView.OverlayConfiguration.PolygonConfiguration = new OverlayPolygonConfiguration.StyleForBarcodeItem(OverlayPolygonStyleForBarcode);
            CameraView.OverlayConfiguration.TextConfiguration = new OverlayTextConfiguration.StyleForBarcodeItem(OverlayTextStyleForBarcode, OverlayOverrideTextForBarcode);
        }

        private string OverlayOverrideTextForBarcode(BarcodeItem item)
        {
            return item.Text + "Loading..";
        }

        private OverlayTextConfiguration.Style OverlayTextStyleForBarcode(BarcodeItem item)
        {
            if (!BarcodeFormatStyle.Palette.ContainsKey(item.Format)) return new OverlayTextConfiguration.Style();
        
            var color = BarcodeFormatStyle.Palette[item.Format]; 
            return new OverlayTextConfiguration.Style
            {
                TextColor = color,
                TextContainerColor = Colors.Wheat,
                HighlightedTextColor = color
            };
        }

        private OverlayPolygonConfiguration.Style OverlayPolygonStyleForBarcode(BarcodeItem item)
        {
            if (!BarcodeFormatStyle.Palette.ContainsKey(item.Format)) return new OverlayPolygonConfiguration.Style();
        
            var color = BarcodeFormatStyle.Palette[item.Format];
            return new OverlayPolygonConfiguration.Style
            {
                StrokeColor = color,
                HighlightedStrokeColor = color
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
        
        private async void OnSelectBarcodeResult(object sender, BarcodeItem[] barcodeItems)
        {
            if (barcodeItems.Length == 0)
                return;

            string text = string.Empty;
            foreach (var barcode in barcodeItems)
            {
                text += $"{barcode.Text} ({barcode.Format.ToString().ToUpper()})\n";
            }

            ResultLabel.Text = text;
            
            // await CommonUtils.DisplayResultAsync(barcodeItesms.ToList());
        }

        private int count = 0;
        
        private void Button_OnClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Button_OnClicked -- " + OverlayConfiguration.OverlayEnabled);
            Debug.WriteLine("Button_OnClicked -- " + OverlayConfiguration.OverlayEnabled);
            
            OverlayConfiguration.OverlayEnabled = !OverlayConfiguration.OverlayEnabled;
            
            var strokeColour = Colors.DarkRed;
            var textColor = Colors.WhiteSmoke;
            var containerColor = Colors.Black;
            
            if (count % 3 == 1)
            {
                strokeColour = Colors.HotPink;
                textColor = Colors.DarkBlue;
                containerColor = Colors.HotPink;

            }
            else if (count % 3 == 2)
            {
                strokeColour = Colors.MediumPurple;
                textColor = Colors.Black;
                containerColor = Colors.MediumPurple;
            }

            count++;

            CameraView.OverlayConfiguration = new SelectionOverlayConfiguration
            {
                OverlayEnabled = true,
                PolygonConfiguration = new OverlayPolygonConfiguration.Style
                {
                    StrokeColor =strokeColour,
                    HighlightedStrokeColor = Colors.Yellow,
                    PolygonColor = Colors.Transparent,
                    HighlightedPolygonColor = Colors.Transparent
                },
                TextConfiguration = new OverlayTextConfiguration.Style
                {

                    TextFormat = BarcodeTextFormat.None,
                    TextColor = textColor,
                    TextContainerColor = containerColor,

                    HighlightedTextColor = Colors.Black,
                    HighlightedTextContainerColor = Colors.WhiteSmoke,
                }
            };
        }
    }
}