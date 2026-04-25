using System.Diagnostics;
using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Core.Barcode;
using ScanbotSDK.MAUI.Example.Utils;

namespace ScanbotSDK.MAUI.Example.ClassicUI.Pages
{
    public partial class BarcodeArOverlayClassicComponentPage : BaseComponentPage
    {
        public SelectionOverlayConfiguration OverlayConfiguration;
        public FinderConfiguration FinderConfiguration;

        private readonly Dictionary<BarcodeFormat, Color> dictionaryOverlayConfig;
        
        public BarcodeArOverlayClassicComponentPage()
        {
            InitializeComponent();
            SetupViews();

            dictionaryOverlayConfig = new Dictionary<BarcodeFormat, Color>
            {
                { BarcodeFormat.Code39,  Colors.Red},
                { BarcodeFormat.Itf,  Colors.Green},
                { BarcodeFormat.QrCode,  Colors.Blue},
                { BarcodeFormat.Code93,  Colors.Yellow},
                { BarcodeFormat.Ean8,  Colors.DeepPink},
                { BarcodeFormat.Aztec,  Colors.MediumPurple},
                { BarcodeFormat.Code128,  Colors.SaddleBrown},
                { BarcodeFormat.Ean13,  Colors.LightCoral},
                { BarcodeFormat.Pdf417,  Colors.Aqua},
                { BarcodeFormat.Codabar,  Colors.Black},
                { BarcodeFormat.UpcA,  Colors.SlateBlue},
                { BarcodeFormat.DataMatrix,  Colors.Gray},
                { BarcodeFormat.UpcE,  Colors.MediumSpringGreen}
            };
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

            // OverlayConfiguration = new SelectionOverlayConfiguration(
            //     overlayFormat: BarcodeTextFormat.CodeAndType,
            //     textColor: Colors.Yellow,
            //     textContainerColor: Colors.Black,
            //     strokeColor: Colors.Yellow,
            //     highlightedStrokeColor: Colors.Purple,
            //     highlightedTextColor: Colors.Purple,
            //     highlightedTextContainerColor: Colors.Black,
            //     polygonBackgroundColor: Colors.Transparent,
            //     polygonBackgroundHighlightedColor: Colors.Transparent);
            
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
        
            FinderConfiguration = new FinderConfiguration
            {
                FinderLineColor =  Colors.Blue,
                IsFinderEnabled =  OverlayConfiguration.OverlayEnabled,
                FinderLineWidth = 4,
                FinderOverlayColor = Colors.Aqua.WithAlpha(0.5f)
            };
            
            CameraView.OverlayConfiguration = OverlayConfiguration;
            CameraView.OverlayConfiguration.PolygonConfiguration = new OverlayPolygonConfiguration.StyleFromBarcodeItem(OverlayPolygonStyleForBarcode);
            CameraView.OverlayConfiguration.TextConfiguration = new OverlayTextConfiguration.StyleFromBarcodeItem(OverlayTextStyleForBarcode, OverlayOverrideTextForBarcode);
        }

        private string OverlayOverrideTextForBarcode(BarcodeItem item)
        {
            return item.Text + "Loading..";
        }

        private OverlayTextConfiguration.Style OverlayTextStyleForBarcode(BarcodeItem item)
        {
            if (!dictionaryOverlayConfig.ContainsKey(item.Format)) return null;
        
            var color = dictionaryOverlayConfig[item.Format]; 
            return new OverlayTextConfiguration.Style
            {
                TextColor = color,
                TextContainerColor = Colors.Wheat,
                HighlightedTextColor = color
            };
        }

        private OverlayPolygonConfiguration.Style OverlayPolygonStyleForBarcode(BarcodeItem item)
        {
            if (!dictionaryOverlayConfig.ContainsKey(item.Format)) return null;
        
            var color = dictionaryOverlayConfig[item.Format];
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
            
            // await CommonUtils.DisplayResultAsync(barcodeItesms.ToList());s
        }

        private int count = 0;
        
        private void Button_OnClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Button_OnClicked -- " + OverlayConfiguration.OverlayEnabled);
            Debug.WriteLine("Button_OnClicked -- " + OverlayConfiguration.OverlayEnabled);
            
            OverlayConfiguration.OverlayEnabled = !OverlayConfiguration.OverlayEnabled;
            // FinderConfiguration.IsFinderEnabled = !FinderConfiguration.IsFinderEnabled;
            var strokeColour = Colors.DarkRed;
            var textColor = Colors.WhiteSmoke;
            var containerColor = Colors.Black;
                
                
            var highlighted = Colors.DarkRed;
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
                    PolygonBackgroundColor = Colors.Transparent,
                    PolygonBackgroundHighlightedColor = Colors.Transparent
                },
                TextConfiguration = new OverlayTextConfiguration.Style
                {

                    OverlayTextFormat = BarcodeTextFormat.None,
                    TextColor = textColor,
                    TextContainerColor = containerColor,

                    HighlightedTextColor = Colors.Black,
                    HighlightedTextContainerColor = Colors.WhiteSmoke,
                }
            };
        
            CameraView.FinderConfiguration = new FinderConfiguration
            {
                FinderLineColor =  Colors.Blue,
                IsFinderEnabled =   FinderConfiguration.IsFinderEnabled,
                FinderLineWidth = 4,
                FinderOverlayColor = Colors.Aqua.WithAlpha(0.5f)
            };
        }
    }
}