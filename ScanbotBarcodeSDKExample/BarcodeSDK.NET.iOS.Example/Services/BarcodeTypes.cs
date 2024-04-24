using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS
{
    public class BarcodeTypes
    {
        public static BarcodeTypes Instance { get; private set; } = new BarcodeTypes();

        public Dictionary<SBSDKBarcodeType, bool> List { get; private set; } = new Dictionary<SBSDKBarcodeType, bool>();

        public SBSDKBarcodeType[] AcceptedTypes
        {
            get
            {
                return List.Where(t => t.Value).Select(t => t.Key).ToArray();
            }
        }

        public bool IsChecked(SBSDKBarcodeType lastCheckedFormat)
        {
            return AcceptedTypes.Contains(lastCheckedFormat);
        }

        private BarcodeTypes()
        {
            var original = SBSDKBarcodeType.AllTypes.ToList();

            foreach (var item in original)
            {
                List.Add(item, true);
            }
        }

        public void Update(SBSDKBarcodeType type, bool value)
        {
            List[type] = value;
        }

    }
}
