using System;
using System.Collections.Generic;
using System.Linq;
using ScanbotBarcodeSDK.iOS;

namespace BarcodeScannerExample.iOS
{
    public class BarcodeTypes
    {
        public static BarcodeTypes Instance { get; private set; } = new BarcodeTypes();

        public Dictionary<SBSDKBarcodeType, bool> List { get; private set; } = new Dictionary<SBSDKBarcodeType, bool>();

        public List<SBSDKBarcodeType> AcceptedTypes
        {
            get
            {
                var result = new List<SBSDKBarcodeType>();
                foreach (var item in List)
                {
                    if (item.Value)
                    {
                        result.Add(item.Key);
                    }
                }

                return result;
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
