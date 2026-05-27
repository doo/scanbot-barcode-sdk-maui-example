using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.BarcodeDocumentModel;
using ScanbotSDK.MAUI.Core.GenericDocument;

namespace ScanbotSDK.MAUI.Example;

public partial class Snippets
{
    public static List<Field> HandleScanningResultWithDataParsers(BarcodeScannerUiResult result)
    {
        var items = result.Items;
        var parsedData = new List<Field>();

        // Loop through the scanned barcode items and extract the desired barcode data
        foreach (var item in items)
        {
            var genericDocument = item.Barcode.ExtractedDocument;
            if (genericDocument == null)
                continue;

            var typeName = genericDocument.Type.Name;

            switch (typeName)
            {
                case nameof(BoardingPass):
                    parsedData.Add(new BoardingPass(genericDocument).ElectronicTicketIndicator);
                    break;

                case nameof(SwissQR):
                    parsedData.Add(new SwissQR(genericDocument).IBAN);
                    break;

                case nameof(DEMedicalPlan):
                    parsedData.Add(new DEMedicalPlan(genericDocument).Children.Doctor.IssuerName);
                    break;

                case nameof(IDCardPDF417):
                    parsedData.Add(new IDCardPDF417(genericDocument).BirthDate);
                    break;

                case nameof(GS1):
                    var gs1Elements = new GS1(genericDocument).Children.Elements.ToList();
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