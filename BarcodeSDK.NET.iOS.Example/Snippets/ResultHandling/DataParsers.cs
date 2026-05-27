using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static List<SBSDKGenericDocumentField> HandleScanningResultWithDataParsers(
        SBSDKUI2BarcodeScannerUIResult result)
    {
        var items = result.Items;
        var parsedData = new List<SBSDKGenericDocumentField>();

        // Loop through the scanned barcode items and extract the desired barcode data
        foreach (var item in items)
        {
            var genericDocument = item.Barcode.ExtractedDocument;
            if (genericDocument == null)
                continue;

            var typeName = genericDocument.Type.Name;

            if (SBSDKBarcodeDocumentModelConstants.BoardingPassDocumentType == typeName)
            {
                parsedData.Add(new SBSDKBarcodeDocumentModelBoardingPass(genericDocument).ElectronicTicketIndicator);
            }
            else if (SBSDKBarcodeDocumentModelConstants.SwissQRDocumentType == typeName)
            {
                parsedData.Add(new SBSDKBarcodeDocumentModelSwissQR(genericDocument).Iban);
            }
            else if (SBSDKBarcodeDocumentModelConstants.DeMedicalPlanDocumentType == typeName)
            {
                parsedData.Add(new SBSDKBarcodeDocumentModelDEMedicalPlan(genericDocument).Doctor?.IssuerName);
            }
            else if (SBSDKBarcodeDocumentModelConstants.IdCardPDF417DocumentType == typeName)
            {
                parsedData.Add(new SBSDKBarcodeDocumentModelIDCardPDF417(genericDocument).BirthDate);
            }
            else if (SBSDKBarcodeDocumentModelConstants.IdCardPDF417DocumentType == typeName)
            {
                var gs1Elements = new SBSDKBarcodeDocumentModelGS1(genericDocument).Elements;
                parsedData.Add(gs1Elements.Length > 0
                    ? gs1Elements[0].ApplicationIdentifier
                    : null);
            }
            else if (SBSDKBarcodeDocumentModelConstants.SepaDocumentType == typeName)
            {
                parsedData.Add(new SBSDKBarcodeDocumentModelSEPA(genericDocument).ReceiverIBAN);
            }
            else if (SBSDKBarcodeDocumentModelConstants.MedicalCertificateDocumentType == typeName)
            {
                parsedData.Add(new SBSDKBarcodeDocumentModelMedicalCertificate(genericDocument).DoctorNumber);
            }
            else if (SBSDKBarcodeDocumentModelConstants.AamvaDocumentType == typeName)
            {
                parsedData.Add(new SBSDKBarcodeDocumentModelAAMVA(genericDocument).IssuerIdentificationNumber);
            }
            else if (SBSDKBarcodeDocumentModelConstants.HibcDocumentType == typeName)
            {
                parsedData.Add(new SBSDKBarcodeDocumentModelHIBC(genericDocument).LabelerIdentificationCode);
            }
        }

        return parsedData;
    }
}