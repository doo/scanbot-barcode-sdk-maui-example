using ScanbotSDK.MAUI.Core.Barcode;

namespace ScanbotSDK.MAUI.Example;

public partial class Snippets
{
    public static BarcodeScannerConfiguration FilterIndividualBarcodesConfig
    {
        get
        {
            var configs = new List<BarcodeFormatConfigurationBase>();

            var commonConfiguration = new BarcodeFormatCommonConfiguration
            {
                RegexFilter = "",
                Minimum1DQuietZoneSize = 10,
                StripCheckDigits = false,
                MinimumTextLength = 0,
                MaximumTextLength = 0,
                Gs1Handling = Gs1Handling.Parse,
                StrictMode = true,
                Formats = BarcodeFormats.Common,
                AddAdditionalQuietZone = false
            };
            configs.Add(commonConfiguration);

            // Add individual configurations for specific barcode formats
            var australiaPostConfig = new BarcodeFormatAustraliaPostConfiguration
            {
                RegexFilter = "",
                AustraliaPostCustomerFormat = AustraliaPostCustomerFormat.AlphaNumeric
            };
            configs.Add(australiaPostConfig);

            var msiPlesseyConfig = new BarcodeFormatMsiPlesseyConfiguration
            {
                RegexFilter = "",
                Minimum1DQuietZoneSize = 10,
                StripCheckDigits = false,
                MinimumTextLength = 0,
                MaximumTextLength = 0,
                ChecksumAlgorithms = [MsiPlesseyChecksumAlgorithm.Mod10]
            };
            configs.Add(msiPlesseyConfig);

            var code11Config = new BarcodeFormatCode11Configuration
            {
                RegexFilter = "",
                Minimum1DQuietZoneSize = 10,
                StripCheckDigits = false,
                MinimumTextLength = 0,
                MaximumTextLength = 0,
                Checksum = true
            };
            configs.Add(code11Config);

            var code2Of5Config = new BarcodeFormatCode2Of5Configuration
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
            var barcodeScannerConfiguration = new BarcodeScannerConfiguration
            {
                BarcodeFormatConfigurations = configs.ToArray(),
                ExtractedDocumentFormats =
                [
                    BarcodeDocumentFormat.Aamva,
                    BarcodeDocumentFormat.BoardingPass,
                    BarcodeDocumentFormat.DeMedicalPlan,
                    BarcodeDocumentFormat.MedicalCertificate,
                    BarcodeDocumentFormat.IdCardPdf417,
                    BarcodeDocumentFormat.Sepa,
                    BarcodeDocumentFormat.SwissQr,
                    BarcodeDocumentFormat.Vcard,
                    BarcodeDocumentFormat.Gs1,
                    BarcodeDocumentFormat.Hibc
                ],
                OnlyAcceptDocuments = false,
                EngineMode = BarcodeScannerEngineMode.NextGen,
                ReturnBarcodeImage = true
            };

            return barcodeScannerConfiguration;
        }
    }
}