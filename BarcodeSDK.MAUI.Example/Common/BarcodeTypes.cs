using ScanbotSDK.MAUI.Core.Barcode;

namespace ScanbotSDK.MAUI.Example.Utils
{
    public class BarcodeTypes
    {
        public static BarcodeTypes Instance { get; private set; } = new BarcodeTypes();

        public Dictionary<BarcodeFormat, bool> List { get; private set; } = new Dictionary<BarcodeFormat, bool>();

        public BarcodeFormat[] AcceptedTypes
        {
            get
            {
                return List.Where(item => item.Value).Select(item => item.Key).ToArray();
            }
        }

        public bool IsChecked(BarcodeFormat lastCheckedFormat)
        {
            return AcceptedTypes.Contains(lastCheckedFormat);
        }

        private List<BarcodeFormat> All => Enum.GetValues<BarcodeFormat>().ToList();

        private BarcodeTypes()
        {
            foreach (BarcodeFormat format in All)
            {
                List.Add(format, true);
            }
        }

        public void Update(BarcodeFormat type, bool value)
        {
            List[type] = value;
        }
    }
    
    public class BarcodeFormatStyle
    {
        public static Dictionary<BarcodeFormat, Color>  Palette = new Dictionary<BarcodeFormat, Color>
        {
            { BarcodeFormat.Code39, Colors.Red },
            { BarcodeFormat.Itf, Colors.Green },
            { BarcodeFormat.QrCode, Colors.Blue },
            { BarcodeFormat.Code93, Colors.Yellow },
            { BarcodeFormat.Ean8, Colors.DeepPink },
            { BarcodeFormat.Aztec, Colors.MediumPurple },
            { BarcodeFormat.Code128, Colors.SaddleBrown },
            { BarcodeFormat.Ean13, Colors.LightCoral },
            { BarcodeFormat.Pdf417, Colors.Aqua },
            { BarcodeFormat.Codabar, Colors.Black },
            { BarcodeFormat.UpcA, Colors.SlateBlue },
            { BarcodeFormat.DataMatrix, Colors.Gray },
            { BarcodeFormat.UpcE, Colors.MediumSpringGreen }
        };
    }
}
