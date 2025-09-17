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

        public List<BarcodeFormat> All =>
            Enum.GetValues(typeof(BarcodeFormat)).Cast<BarcodeFormat>().ToList();

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
}
