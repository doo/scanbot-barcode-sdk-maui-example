using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKBarcodeScannerConfiguration DocumentParsersConfig
    {
        get
        {
            var barcodeScannerConfiguration = new SBSDKBarcodeScannerConfiguration
            {
                BarcodeFormatConfigurations = [new SBSDKBarcodeFormatCommonConfiguration()],

                // Example of adding specific formats for parsed documents
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

                // Set to true if you want to only accept barcodes with parsed documents
                OnlyAcceptDocuments = true,
                EngineMode = SBSDKBarcodeScannerEngineMode.NextGen
            };

            return barcodeScannerConfiguration;
        }
    }
}