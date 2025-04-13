using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public partial class BarcodeDetailsController
{
    private void GetFormattedDocument(SBSDKGenericDocument document)
    {
        var docType = document.Type.Name;
        
        if (SBSDKBarcodeDocumentModelConstants.AamvaDocumentType == docType)
        {
            var amvaDocument = new SBSDKBarcodeDocumentModelAAMVA(document);
            barcodeDetails.AddRange([
                new("Aamva version number", amvaDocument.Version?.Value?.Text),
                new("Issuer identification number", amvaDocument.IssuerIdentificationNumber?.Value?.Text),
                new("Jurisdiction identification number", amvaDocument.JurisdictionVersionNumber?.Value?.Text)
            ]);
        } else if (SBSDKBarcodeDocumentModelConstants.BoardingPassDocumentType == docType)
        {
            var boardingPass = new SBSDKBarcodeDocumentModelBoardingPass(document);
            barcodeDetails.AddRange([
                new("Name", boardingPass.Name?.Value?.Text),
                new("Security data", boardingPass.SecurityData?.Value?.Text),
                new("Electronic ticket", boardingPass.ElectronicTicket?.Value?.Text),
                new("Number of legs", boardingPass.NumberOfLegs?.Value?.Text)
            ]);
        } else if (SBSDKBarcodeDocumentModelConstants.DeMedicalPlanDocumentType == docType)
        {
            var deMedicalPlan = new SBSDKBarcodeDocumentModelDEMedicalPlan(document);
            barcodeDetails.AddRange([
                new("Document type", deMedicalPlan.RequiredDocumentType),
                new("Name", deMedicalPlan.DocumentVersionNumber?.Value?.Text),
                new("Security data", deMedicalPlan.TotalNumberOfPages?.Value?.Text),
                new("Patch version number", deMedicalPlan.PatchVersionNumber?.Value?.Text),
                new("Language country code", deMedicalPlan.LanguageCountryCode?.Value?.Text)
            ]);
        } else if (SBSDKBarcodeDocumentModelConstants.Gs1DocumentType == docType)
        {
            var gs1Document = new SBSDKBarcodeDocumentModelGS1(document);
            foreach (var element in gs1Document.Elements)
            {
                barcodeDetails.AddRange([
                    new("Description", element.ElementDescription?.Value?.Text),
                    new("Raw value", element.RawValue?.Value?.Text),
                    new("Data title", element.DataTitle?.Value?.Text),
                    new("Application Id", element.ApplicationIdentifier?.Value?.Text),
                    new("Validation errors", element.ValidationErrors.Length.ToString())
                ]);
            }
        } else if (SBSDKBarcodeDocumentModelConstants.HibcDocumentType == docType)
        {
            var hibcDocument = new SBSDKBarcodeDocumentModelHIBC(document);
            barcodeDetails.AddRange([
                new("Serial Number", hibcDocument.SerialNumber?.Value?.Text),
                new("Quantity", hibcDocument.Quantity?.Value?.Text),
                new("Date Of Manufacture", hibcDocument.DateOfManufacture?.Value?.Text),
                new("LOT number", hibcDocument.LotNumber?.Value?.Text),
                new("Expiry date day", hibcDocument.ExpiryDateDay?.Value?.Text),
                new("Labeler Id", hibcDocument.LabelerIdentificationCode?.Value?.Text),
                new("Labeler product/catalog number", hibcDocument.LabelersProductOrCatalogNumber?.Value?.Text)
            ]);
        } else if (SBSDKBarcodeDocumentModelConstants.IdCardPDF417DocumentType == docType)
        {
            var idCardDocument = new SBSDKBarcodeDocumentModelIDCardPDF417(document);
            barcodeDetails.AddRange([
                new("Document code", idCardDocument.DocumentCode?.Value?.Text),
                new("Date issued", idCardDocument.DateIssued?.Value?.Text),
                new("Date expired", idCardDocument.DateExpired?.Value?.Text),
                new("First name", idCardDocument.FirstName?.Value?.Text),
                new("Last name", idCardDocument.LastName?.Value?.Text),
                new("Birth date", idCardDocument.BirthDate?.Value?.Text),
                new("Optional", idCardDocument.Optional?.Value?.Text)
            ]);
        } else if (SBSDKBarcodeDocumentModelConstants.MedicalCertificateDocumentType == docType)
        {
            var medicalCertificateDocument = new SBSDKBarcodeDocumentModelMedicalCertificate(document);
            barcodeDetails.AddRange([
                new("First name", medicalCertificateDocument.FirstName?.Value?.Text),
                new("Last name", medicalCertificateDocument.LastName?.Value?.Text),
                new("Birth date", medicalCertificateDocument.BirthDate?.Value?.Text),
                new("Doctor number", medicalCertificateDocument.DoctorNumber?.Value?.Text),
                new("Health insurance number", medicalCertificateDocument.HealthInsuranceNumber?.Value?.Text)
            ]);
        } else if (SBSDKBarcodeDocumentModelConstants.SepaDocumentType == docType)
        {
            var sepaDocument = new SBSDKBarcodeDocumentModelSEPA(document);
            barcodeDetails.AddRange([
                new("Version", sepaDocument.Version?.Value?.Text),
                new("Amount", sepaDocument.Amount?.Value?.Text),
                new("Character set", sepaDocument.CharacterSet?.Value?.Text),
                new("Purpose", sepaDocument.Purpose?.Value?.Text),
                new("Identification", sepaDocument.Identification?.Value?.Text),
                new("Information", sepaDocument.Information?.Value?.Text),
                new("Receiver BIC", sepaDocument.ReceiverBIC?.Value?.Text),
                new("Receiver IBAN", sepaDocument.ReceiverIBAN?.Value?.Text),
                new("ReceiverName", sepaDocument.ReceiverName?.Value?.Text),
                new("Remittance", sepaDocument.Remittance?.Value?.Text)
            ]);
        } else if (SBSDKBarcodeDocumentModelConstants.SwissQRDocumentType == docType)
        {
            var swissQrDocument = new SBSDKBarcodeDocumentModelSwissQR(document);
            barcodeDetails.AddRange([
                new("Major version", swissQrDocument.MajorVersion?.Value?.Text),
                new("Amount", swissQrDocument.Amount?.Value?.Text),
                new("Due date", swissQrDocument.DueDate?.Value?.Text),
                new("Currency", swissQrDocument.Currency?.Value?.Text),
                new("Debtor country", swissQrDocument.DebtorCountry?.Value?.Text),
                new("Debtor name", swissQrDocument.DebtorName?.Value?.Text),
                new("IBAN", swissQrDocument.Iban?.Value?.Text)
            ]);
        } else if (SBSDKBarcodeDocumentModelConstants.VCardDocumentType == docType)
        {
            var vCardDocument = new SBSDKBarcodeDocumentModelVCard(document);
            barcodeDetails.AddRange([
                new("Name", vCardDocument.Name?.RawValue?.Value?.Text),
                new("Title", vCardDocument.Title?.RawValue?.Value?.Text),
                new("First name", vCardDocument.FirstName?.RawValue?.Value?.Text),
                new("Birthday", vCardDocument.Birthday?.RawValue?.Value?.Text),
                new("Email", vCardDocument.Email?.RawValue?.Value?.Text),
                new("Role", vCardDocument.Role?.RawValue?.Value?.Text)
            ]);
        }
    }
}