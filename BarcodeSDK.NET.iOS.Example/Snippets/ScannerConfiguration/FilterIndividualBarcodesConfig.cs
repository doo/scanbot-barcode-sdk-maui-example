using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKBarcodeScannerConfiguration FilterIndividualBarcodesConfig
    {
        get
        {
            var configs = new List<SBSDKBarcodeFormatConfigurationBase>();

            var commonConfiguration = new SBSDKBarcodeFormatCommonConfiguration
            {
                RegexFilter = "",
                Minimum1DQuietZoneSize = 10,
                StripCheckDigits = false,
                MinimumTextLength = 0,
                MaximumTextLength = 0,
                Gs1Handling = SBSDKGS1Handling.Parse,
                StrictMode = true,
                Formats = SBSDKBarcodeFormats.Common,
                AddAdditionalQuietZone = false
            };
            configs.Add(commonConfiguration);

            // Add individual configurations for specific barcode formats
            var australiaPostConfig = new SBSDKBarcodeFormatAustraliaPostConfiguration
            {
                RegexFilter = "",
                AustraliaPostCustomerFormat = SBSDKAustraliaPostCustomerFormat.AlphaNumeric
            };
            configs.Add(australiaPostConfig);

            var msiPlesseyConfig = new SBSDKBarcodeFormatMSIPlesseyConfiguration()
            {
                RegexFilter = "",
                Minimum1DQuietZoneSize = 10,
                StripCheckDigits = false,
                MinimumTextLength = 0,
                MaximumTextLength = 0,
                ChecksumAlgorithms = [SBSDKMSIPlesseyChecksumAlgorithm.Mod10]
            };
            configs.Add(msiPlesseyConfig);

            var code11Config = new SBSDKBarcodeFormatCode11Configuration
            {
                RegexFilter = "",
                Minimum1DQuietZoneSize = 10,
                StripCheckDigits = false,
                MinimumTextLength = 0,
                MaximumTextLength = 0,
                Checksum = true
            };
            configs.Add(code11Config);

            var code2Of5Config = new SBSDKBarcodeFormatCode2Of5Configuration
            {
                RegexFilter = "",
                Minimum1DQuietZoneSize = 10,
                StripCheckDigits = false,
                MinimumTextLength = 0,
                MaximumTextLength = 0,
                Iata2of5 = true,
                Code25 = false,
                Industrial2of5 = false,
                UseIATA2OF5Checksum = true
            };
            configs.Add(code2Of5Config);

            // Set the configurations to the barcode scanner
            var barcodeScannerConfiguration = new SBSDKBarcodeScannerConfiguration
            {
                BarcodeFormatConfigurations = configs.ToArray(),
                ExtractedDocumentFormats =
                [
                    SBSDKBarcodeDocumentFormat.Aamva,
                    SBSDKBarcodeDocumentFormat.BoardingPass,
                    SBSDKBarcodeDocumentFormat.DeMedicalPlan,
                    SBSDKBarcodeDocumentFormat.MedicalCertificate,
                    SBSDKBarcodeDocumentFormat.IdCardPdf417,
                    SBSDKBarcodeDocumentFormat.Sepa,
                    SBSDKBarcodeDocumentFormat.SwissQr,
                    SBSDKBarcodeDocumentFormat.Vcard,
                    SBSDKBarcodeDocumentFormat.Gs1,
                    SBSDKBarcodeDocumentFormat.Hibc
                ],
                OnlyAcceptDocuments = false,
                EngineMode = SBSDKBarcodeScannerEngineMode.NextGen,
                ReturnBarcodeImage = true
            };

            return barcodeScannerConfiguration;
        }
    }
}