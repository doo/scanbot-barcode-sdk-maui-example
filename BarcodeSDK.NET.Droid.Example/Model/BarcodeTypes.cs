using IO.Scanbot.Sdk.Barcode;

namespace BarcodeSDK.NET.Droid
{
    public class BarcodeTypes
    {
        public static BarcodeTypes Instance { get; private set; } = new BarcodeTypes();

        internal readonly Dictionary<BarcodeFormat, bool> AcceptedBarcodesDictionary;
        
        public BarcodeFormat[] AcceptedTypes => AcceptedBarcodesDictionary
            .Where(item => item.Value)
            .Select(item => item.Key).ToArray();
        
        private BarcodeTypes()
        {
            AcceptedBarcodesDictionary = new Dictionary<BarcodeFormat, bool>();
            foreach (var barcodeFormat in BarcodeFormats.All)
            {
                AcceptedBarcodesDictionary.Add(barcodeFormat, true);
            }
        }

        public void Update(BarcodeFormat type, bool value)
        {
            AcceptedBarcodesDictionary[type] = value;
        }
    }
}
