using IO.Scanbot.Sdk.Barcode.Entity;

namespace BarcodeSDK.NET.Droid
{
    public class BarcodeTypes
    {
        public static BarcodeTypes Instance { get; private set; } = new BarcodeTypes();

        public Dictionary<BarcodeFormat, bool> List { get; private set; } = new Dictionary<BarcodeFormat, bool>();

        public List<BarcodeFormat> AcceptedTypes
        {
            get
            {
                return List.Where(t => t.Value).Select(t => t.Key).ToList();
            }
        }

        public bool IsChecked(BarcodeFormat lastCheckedFormat)
        {
            return AcceptedTypes.Contains(lastCheckedFormat);
        }

        private BarcodeTypes()
        {
            var original = BarcodeFormat.Values().ToList();
            foreach (var item in original)
            {
                if (item == BarcodeFormat.Unknown) {
                    continue;
                }
                List.Add(item, true);
            }
        }

        public void Update(BarcodeFormat type, bool value)
        {
            List[type] = value;
        }

    }
}
