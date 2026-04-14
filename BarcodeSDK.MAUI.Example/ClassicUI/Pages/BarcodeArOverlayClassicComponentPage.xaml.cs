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
                Enabled = false,
                StrokeColor = Colors.DarkRed,
                TextColor = Colors.WhiteSmoke,
                TextContainerColor = Colors.Black,

                HighlightedStrokeColor = Colors.Yellow,
                HighlightedTextColor = Colors.Black,
                HighlightedTextContainerColor = Colors.WhiteSmoke
            };
        
            FinderConfiguration = new FinderConfiguration
            {
                FinderLineColor =  Colors.Blue,
                IsFinderEnabled =  OverlayConfiguration.Enabled,
                FinderLineWidth = 4,
                FinderOverlayColor = Colors.Aqua.WithAlpha(0.5f)
            };
            
            CameraView.OverlayConfiguration = OverlayConfiguration;

            CameraView.OverlayPolygonStyleForBarcode = OverlayPolygonStyleForBarcode;
            CameraView.OverlayTextStyleForBarcode = OverlayTextStyleForBarcode;
            CameraView.OverlayOverrideTextForBarcode = OverlayOverrideTextForBarcode;
        }

        private string OverlayOverrideTextForBarcode(BarcodeItem item)
        {
            return item.Text + "Loading..";
        }

        private OverlayTextStyle OverlayTextStyleForBarcode(BarcodeItem item)
        {
            if (!dictionaryOverlayConfig.ContainsKey(item.Format)) return null;
        
            var color = dictionaryOverlayConfig[item.Format]; 
            return new OverlayTextStyle
            {
                TextColor = color,
                TextContainerColor = Colors.Wheat,
                HighlightedTextColor = color
            };
        }

        private OverlayPolygonStyle OverlayPolygonStyleForBarcode(BarcodeItem item)
        {
            if (!dictionaryOverlayConfig.ContainsKey(item.Format)) return null;
        
            var color = dictionaryOverlayConfig[item.Format];
            return new OverlayPolygonStyle
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

            System.Diagnostics.Debug.WriteLine(text);
            ResultLabel.Text = text;
            
            // await CommonUtils.DisplayResultAsync(barcodeItems.ToList());
        }

        private int count = 0;
        
        private void Button_OnClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Button_OnClicked -- " + OverlayConfiguration.Enabled);
            Debug.WriteLine("Button_OnClicked -- " + OverlayConfiguration.Enabled);
            
            OverlayConfiguration.Enabled = !OverlayConfiguration.Enabled;
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
                Enabled =  true,
                StrokeColor = strokeColour,
                TextColor = textColor,
                TextContainerColor = containerColor,
                OverlayTextFormat = BarcodeTextFormat.None,
                
                HighlightedStrokeColor = Colors.Yellow,
                HighlightedTextColor = Colors.Black,
                HighlightedTextContainerColor = Colors.WhiteSmoke
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