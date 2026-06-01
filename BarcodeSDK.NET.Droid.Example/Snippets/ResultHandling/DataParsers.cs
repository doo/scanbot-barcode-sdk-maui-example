using IO.Scanbot.Sdk.Barcode;
using IO.Scanbot.Sdk.Barcode.Entity;
using IO.Scanbot.Sdk.Genericdocument;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static List<TextFieldWrapper> HandleScanningResultWithDataParsers(BarcodeScannerResult result)
    {
        var items = result.Barcodes;
        var parsedData = new List<TextFieldWrapper>();

        // Loop through the scanned barcode items and extract the desired barcode data
        foreach (var item in items)
        {
            var genericDocument = item.ExtractedDocument;
            if (genericDocument == null)
                continue;

            var typeName = genericDocument.Type.Name;

            switch (typeName)
            {
                case nameof(BoardingPass):
                    parsedData.Add(new BoardingPass(genericDocument).ElectronicTicketIndicator);
                    break;

                case nameof(SwissQR):
                    parsedData.Add(new SwissQR(genericDocument).Iban);
                    break;

                case nameof(DEMedicalPlan):
                    parsedData.Add(new DEMedicalPlan(genericDocument).GetDoctor().IssuerName);
                    break;

                case nameof(IDCardPDF417):
                    parsedData.Add(new IDCardPDF417(genericDocument).BirthDate);
                    break;

                case nameof(GS1):
                    var gs1Elements = new GS1(genericDocument).Elements;
                    parsedData.Add(gs1Elements.Count > 0
                        ? gs1Elements[0].ApplicationIdentifier
                        : null);
                    break;

                case nameof(SEPA):
                    parsedData.Add(new SEPA(genericDocument).ReceiverIBAN);
                    break;

                case nameof(MedicalCertificate):
                    parsedData.Add(new MedicalCertificate(genericDocument).DoctorNumber);
                    break;

                case nameof(AAMVA):
                    parsedData.Add(new AAMVA(genericDocument).IssuerIdentificationNumber);
                    break;

                case nameof(HIBC):
                    parsedData.Add(new HIBC(genericDocument).LabelerIdentificationCode);
                    break;
            }
        }

        return parsedData;
    }
}