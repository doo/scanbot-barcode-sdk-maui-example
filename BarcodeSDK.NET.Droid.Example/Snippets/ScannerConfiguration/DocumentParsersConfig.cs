using IO.Scanbot.Sdk.Barcode;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static BarcodeScannerConfiguration DocumentParsersConfig
    {
        get
        {
            var barcodeScannerConfiguration = new BarcodeScannerConfiguration
            {
                BarcodeFormatConfigurations = [new BarcodeFormatCommonConfiguration()],

                // Example of adding specific formats for parsed documents
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

                // Set to true if you want to only accept barcodes with parsed documents
                OnlyAcceptDocuments = true,
                EngineMode = BarcodeScannerEngineMode.NextGen
            };

            return barcodeScannerConfiguration;
        }
    }
}