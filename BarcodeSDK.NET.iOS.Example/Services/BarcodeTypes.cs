using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS
{
    public class BarcodeTypes
    {
        public static BarcodeTypes Instance { get; private set; } = new BarcodeTypes();

        internal readonly Dictionary<SBSDKBarcodeFormat, bool> AcceptedBarcodesDictionary;
        
        public SBSDKBarcodeFormat[] AcceptedTypes => AcceptedBarcodesDictionary
                                                    ?.Where(item => item.Value)
                                                    .Select(item => item.Key).ToArray() ?? [];
        
        private BarcodeTypes()
        {
            AcceptedBarcodesDictionary = new Dictionary<SBSDKBarcodeFormat, bool>();
            foreach (var barcodeFormat in SBSDKBarcodeFormats.All)
            {
                AcceptedBarcodesDictionary.Add(barcodeFormat, true);
            }
        }

        public void Update(SBSDKBarcodeFormat type, bool value)
        {
            AcceptedBarcodesDictionary[type] = value;
        }
    }
}
