using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public partial class BarcodeDetailsController
{
    private void PopulateData(SBSDKBarcodeItem barcode)
    {
        barcodeDetails =
        [
            new(caption: "Format", text: barcode.Format.Name),
            new(caption: "Text", text: barcode.Text),
        ];

        if (!string.IsNullOrEmpty(barcode.UpcEanExtension))
        {
            barcodeDetails.Add(new(caption: "Extension", text: barcode.UpcEanExtension));
        }

        if (barcode.ExtractedDocument != null)
        {
            GetFormattedDocument(barcode.ExtractedDocument);
        }
    }

    private void GetFormattedDocument(SBSDKGenericDocument document)
    {
        var docType = document.Type.Name;
        if (SBSDKBarcodeDocumentModelConstants.AamvaDocumentType == docType)
        {
            var amvaDocument = new SBSDKBarcodeDocumentModelAAMVA(document);
            barcodeDetails.AddRange([
                new("File type", amvaDocument.RequiredDocumentType),
                new("Aamva version number", amvaDocument.Version),
                new("Issuer identification number", amvaDocument.IssuerIdentificationNumber),
                new("Jurisdiction identification number", amvaDocument.JurisdictionVersionNumber)
            ]);
        }

        if (SBSDKBarcodeDocumentModelConstants.BoardingPassDocumentType == docType)
        {
            var boardingPass = new SBSDKBarcodeDocumentModelBoardingPass(document);
            barcodeDetails.AddRange([
                new("Name", boardingPass.Name),
                new("Security data", boardingPass.SecurityData),
                new("Electronic ticket", boardingPass.ElectronicTicket),
                new("Number of legs", boardingPass.NumberOfLegs)
            ]);
        }

        if (SBSDKBarcodeDocumentModelConstants.DeMedicalPlanDocumentType == docType)
        {
            var deMedicalPlan = new SBSDKBarcodeDocumentModelDEMedicalPlan(document);
            barcodeDetails.AddRange([
                new("Name", deMedicalPlan.DocumentVersionNumber),
                new("Security data", deMedicalPlan.TotalNumberOfPages),
                new("Required document type", deMedicalPlan.RequiredDocumentType),
                new("Patch version number", deMedicalPlan.PatchVersionNumber),
                new("Language country code", deMedicalPlan.LanguageCountryCode)
            ]);
        }

        if (SBSDKBarcodeDocumentModelConstants.Gs1DocumentType == docType)
        {
            var gs1Document = new SBSDKBarcodeDocumentModelGS1(document);
            foreach (var element in gs1Document.Elements)
            {
                barcodeDetails.AddRange([
                    new("Description", element.ElementDescription),
                    new("Raw value", element.RawValue),
                    new("Data title", element.DataTitle),
                    new("Application Id", element.ApplicationIdentifier),
                    new("Validation errors", element.ValidationErrors?.Length.ToString())
                ]);
            }
        }

        if (SBSDKBarcodeDocumentModelConstants.HibcDocumentType == docType)
        {
            var hibcDocument = new SBSDKBarcodeDocumentModelHIBC(document);
            barcodeDetails.AddRange([
                new("Serial Number", hibcDocument.SerialNumber),
                new("Quantity", hibcDocument.Quantity),
                new("Date Of Manufacture", hibcDocument.DateOfManufacture),
                new("LOT number", hibcDocument.LotNumber),
                new("Expiry date day", hibcDocument.ExpiryDateDay),
                new("Labeler Id", hibcDocument.LabelerIdentificationCode),
                new("Labeler product/catalog number", hibcDocument.LabelersProductOrCatalogNumber)
            ]);
        }

        if (SBSDKBarcodeDocumentModelConstants.IdCardPDF417DocumentType == docType)
        {
            var idCardDocument = new SBSDKBarcodeDocumentModelIDCardPDF417(document);
            barcodeDetails.AddRange([
                new("Document code", idCardDocument.DocumentCode),
                new("Date issued", idCardDocument.DateIssued),
                new("Date expired", idCardDocument.DateExpired),
                new("First name", idCardDocument.FirstName),
                new("Last name", idCardDocument.LastName),
                new("Birth date", idCardDocument.BirthDate),
                new("Optional", idCardDocument.Optional),
            ]);
        }

        if (SBSDKBarcodeDocumentModelConstants.MedicalCertificateDocumentType == docType)
        {
            var medicalCertificateDocument = new SBSDKBarcodeDocumentModelMedicalCertificate(document);
            barcodeDetails.AddRange([
                new("First name", medicalCertificateDocument.FirstName),
                new("Last name", medicalCertificateDocument.LastName),
                new("Birth date", medicalCertificateDocument.BirthDate),
                new("Doctor number", medicalCertificateDocument.DoctorNumber),
                new("Health insurance number", medicalCertificateDocument.HealthInsuranceNumber)
            ]);
        }

        if (SBSDKBarcodeDocumentModelConstants.SepaDocumentType == docType)
        {
            var sepaDocument = new SBSDKBarcodeDocumentModelSEPA(document);
            barcodeDetails.AddRange([
                new("Version", sepaDocument.Version),
                new("Amount", sepaDocument.Amount),
                new("Character set", sepaDocument.CharacterSet),
                new("Purpose", sepaDocument.Purpose),
                new("Identification", sepaDocument.Identification),
                new("Information", sepaDocument.Information),
                new("Receiver BIC", sepaDocument.ReceiverBIC),
                new("Receiver IBAN", sepaDocument.ReceiverIBAN),
                new("ReceiverName", sepaDocument.ReceiverName),
                new("Remittance", sepaDocument.Remittance),
            ]);
        }

        if (SBSDKBarcodeDocumentModelConstants.SwissQRDocumentType == docType)
        {
            var swissQrDocument = new SBSDKBarcodeDocumentModelSwissQR(document);
            barcodeDetails.AddRange([
                new("Major version", swissQrDocument.MajorVersion),
                new("Amount", swissQrDocument.Amount),
                new("Due date", swissQrDocument.DueDate),
                new("Currency", swissQrDocument.Currency),
                new("Debtor country", swissQrDocument.DebtorCountry),
                new("Debtor name", swissQrDocument.DebtorName),
                new("IBAN", swissQrDocument.Iban),
            ]);
        }

        if (SBSDKBarcodeDocumentModelConstants.VCardDocumentType == docType)
        {
            var vCardDocument = new SBSDKBarcodeDocumentModelVCard(document);
            barcodeDetails.AddRange([
                new("Name", vCardDocument.Name?.RawValue),
                new("Title", vCardDocument.Title?.RawValue),
                new("First name", vCardDocument.FirstName?.RawValue),
                new("Birthday", vCardDocument.Birthday?.RawValue),
                new("Email", vCardDocument.Email?.RawValue),
                new("Role", vCardDocument.Role?.RawValue)
            ]);
        }
    }
}