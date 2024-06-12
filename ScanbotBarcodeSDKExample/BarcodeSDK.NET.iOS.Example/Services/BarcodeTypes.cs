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

        private static Dictionary<nuint, SBSDKUI2BarcodeFormat> acceptedTypesForV2 = new Dictionary<nuint, SBSDKUI2BarcodeFormat>{
                        { SBSDKBarcodeType.AustraliaPost.Hash, SBSDKUI2BarcodeFormat.AustraliaPost },
                        { SBSDKBarcodeType.Aztec.Hash, SBSDKUI2BarcodeFormat.Aztec },
                        { SBSDKBarcodeType.CodaBar.Hash, SBSDKUI2BarcodeFormat.Codabar },
                        { SBSDKBarcodeType.Code128.Hash, SBSDKUI2BarcodeFormat.Code128 },
                        { SBSDKBarcodeType.Code25.Hash, SBSDKUI2BarcodeFormat.Code25 },
                        { SBSDKBarcodeType.Code39.Hash, SBSDKUI2BarcodeFormat.Code39 },
                        { SBSDKBarcodeType.Code93.Hash, SBSDKUI2BarcodeFormat.Code93 },
                        { SBSDKBarcodeType.DataMatrix.Hash, SBSDKUI2BarcodeFormat.DataMatrix },
                        { SBSDKBarcodeType.Databar.Hash, SBSDKUI2BarcodeFormat.Databar },
                        { SBSDKBarcodeType.DatabarExpanded.Hash, SBSDKUI2BarcodeFormat.DatabarExpanded },
                        { SBSDKBarcodeType.DatabarLimited.Hash, SBSDKUI2BarcodeFormat.DatabarLimited },
                        { SBSDKBarcodeType.Ean13.Hash, SBSDKUI2BarcodeFormat.Ean13 },
                        { SBSDKBarcodeType.Ean8.Hash, SBSDKUI2BarcodeFormat.Ean8 },
                        { SBSDKBarcodeType.Gs1Composite.Hash, SBSDKUI2BarcodeFormat.Gs1Composite },
                        { SBSDKBarcodeType.Iata2Of5.Hash, SBSDKUI2BarcodeFormat.Iata2Of5 },
                        { SBSDKBarcodeType.Industrial2Of5.Hash, SBSDKUI2BarcodeFormat.Industrial2Of5 },
                        { SBSDKBarcodeType.Itf.Hash, SBSDKUI2BarcodeFormat.Itf },
                        { SBSDKBarcodeType.JapanPost.Hash, SBSDKUI2BarcodeFormat.JapanPost },
                        { SBSDKBarcodeType.MicroPdf417.Hash, SBSDKUI2BarcodeFormat.MicroPdf417 },
                        { SBSDKBarcodeType.MicroQrCode.Hash, SBSDKUI2BarcodeFormat.MicroQrCode },
                        { SBSDKBarcodeType.MsiPlessey.Hash, SBSDKUI2BarcodeFormat.MsiPlessey },
                        { SBSDKBarcodeType.Pdf417.Hash, SBSDKUI2BarcodeFormat.Pdf417 },
                        { SBSDKBarcodeType.QrCode.Hash, SBSDKUI2BarcodeFormat.QrCode },
                        { SBSDKBarcodeType.RoyalMail.Hash, SBSDKUI2BarcodeFormat.RoyalMail },
                        { SBSDKBarcodeType.RoyalTNTPpost.Hash, SBSDKUI2BarcodeFormat.RoyalTntPost },
                        { SBSDKBarcodeType.UpcA.Hash, SBSDKUI2BarcodeFormat.UpcA },
                        { SBSDKBarcodeType.UpcE.Hash, SBSDKUI2BarcodeFormat.UpcE },
                        { SBSDKBarcodeType.UspsIntelligentMail.Hash, SBSDKUI2BarcodeFormat.UspsIntelligentMail }
        };

        public SBSDKUI2BarcodeFormat[] AcceptedTypesV2
        {
            get
            {
                return AcceptedTypes
                .Where(format => acceptedTypesForV2.ContainsKey(format.Hash))
                .Select(format => acceptedTypesForV2[format.Hash]).ToArray();
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
