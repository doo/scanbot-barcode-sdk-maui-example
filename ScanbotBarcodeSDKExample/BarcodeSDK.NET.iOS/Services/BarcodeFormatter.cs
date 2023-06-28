using System;
using ScanbotBarcodeSDK.iOS;

namespace BarcodeScannerExample.iOS
{
    public class BarcodeFormatter
    {
        public static readonly BarcodeFormatter Instance = new BarcodeFormatter();

        public string GetText(SBSDKBarcodeScannerResult barcode)
        {
            var format = barcode.FormattedResult;
            if (format is SBSDKMedicalPlanDocumentFormat)
                return GetMedicalPlan(format as SBSDKMedicalPlanDocumentFormat);
            if (format is SBSDKVCardDocumentFormat)
                return GetVCard(format as SBSDKVCardDocumentFormat);
            if (format is SBSDKAAMVADocumentFormat)
                GetAAMVA(format as SBSDKAAMVADocumentFormat);
            if (format is SBSDKIDCardPDF417DocumentFormat)
                GetPDF417(format as SBSDKIDCardPDF417DocumentFormat);
            if (format is SBSDKBoardingPassDocumentFormat)
                GetBoardingPass(format as SBSDKBoardingPassDocumentFormat);
            if (format is SBSDKMedicalCertificateDocumentFormat)
                GetDC(format as SBSDKMedicalCertificateDocumentFormat);
            if (format is SBSDKSEPADocumentFormat)
                GetSEPA(format as SBSDKSEPADocumentFormat);

            return "";
        }

        string GetMedicalPlan(SBSDKMedicalPlanDocumentFormat plan)
        {
            var result = "\n\n\nDetected German medical plan document:\n";
            result += $"\nGUID: {plan.GUID}";
            result += $"\nCurrent page: {plan.CurrentPage}";
            result += $"\nTotal number of pages: {plan.TotalNumberOfPages}";
            result += $"\nDocument version: {plan.DocumentVersionNumber}";
            result += $"\nPatch version: {plan.PatchVersionNumber}";
            result += $"\nLanguage code: {plan.LanguageCountryCode}";

            result += "\n\nPatient information:";
            foreach (var field in plan.Patient.Fields)
            {
                result += $"\n{field.TypeHumanReadableString}: {field.Value}";
            }

            result += "\n\nDoctor information:";
            foreach (var field in plan.Doctor.Fields)
            {
                result += $"\n{field.TypeHumanReadableString}: {field.Value}";
            }

            if (plan.Subheadings.Length > 0)
            {
                result += "\n\nSubheadings:";

                foreach (var subheading in plan.Subheadings)
                {
                    foreach (var field in subheading.Fields)
                    {
                        result += $"\n{field.TypeHumanReadableString}: {field.Value}";
                    }

                    if (subheading.GeneralNotes.Length > 0)
                    {
                        result += "\nGeneral notes:";
                        foreach (var note in subheading.GeneralNotes)
                        {
                            result += $"\n{note}\n";
                        }
                    }
                    if (subheading.Prescriptions.Length > 0)
                    {
                        foreach (var prescription in subheading.Prescriptions)
                        {
                            foreach (var prescriptionField in prescription.Fields)
                            {
                                result += $"\n{prescriptionField.TypeHumanReadableString}: {prescriptionField.Value}";
                            }
                            result += "\n------";
                        }
                    }

                    if (subheading.Medicines.Length > 0)
                    {
                        result += "\nMedicines:";
                        foreach (var medicine in subheading.Medicines)
                        {
                            foreach (var medicineField in medicine.Fields)
                            {
                                result += $"\n{medicineField.TypeHumanReadableString}: {medicineField.Value}";
                            }

                            if (medicine.Substances.Length > 0)
                            {
                                foreach (var substance in medicine.Substances)
                                {
                                    foreach (var substanceField in substance.Fields)
                                    {
                                        result += $"\n{substanceField.TypeHumanReadableString}: {substanceField.Value}";
                                    }
                                }
                            }
                            result += "\n------";
                        }
                    }
                }
            }

            return result;
        }

        string GetVCard(SBSDKVCardDocumentFormat card)
        {
            var result = "\n\n\nDetected vCard document:";

            if (card.Fields.Length > 0)
            {
                result += "\n\nFields:";
                foreach (var field in card.Fields)
                {
                    var value = field.Values[0];
                    if (field.Values.Length > 1)
                    {
                        value = value + "...";
                    }

                    result += $"\n{field.TypeHumanReadableString}: {value}";
                }
            }

            return result;
        }

        string GetAAMVA(SBSDKAAMVADocumentFormat document)
        {
            var result = $"\n\n\nDetected AAMVA document:";
            result += $"\n\nRaw header string: {document.HeaderRawString}";
            result += $"\nFile type: {document.FileType}";
            result += $"\nIssuer ID number: {document.IssuerIdentificationNumber}";
            result += $"\nAAMVA version: {document.AAMVAVersionNumber}";
            result += $"\nJurisdiction version: {document.JurisdictionVersionNumber}";


            if (document.Subfiles.Length > 0)
            {
                result += "\n\nSubfiles:";
                foreach (var subFile in document.Subfiles)
                {
                    result += $"\nSubfile type: {subFile.SubFileType}";


                    foreach (var field in subFile.Fields)
                    {
                        result += $"\n{field.TypeHumanReadableString}: {field.Value}";
                    }
                    result = result + "\n\n";
                }
            }

            return result;
        }

        string GetPDF417(SBSDKIDCardPDF417DocumentFormat document)
        {
            var result = "\n\n\nDetected PDF417 ID card:\n\n";
            if (document.Fields.Length > 0)
            {
                foreach (var field in document.Fields)
                {
                    result += $"\n{field.TypeHumanReadableString}: {field.Value}";
                }
            }

            return result;
        }

        string GetBoardingPass(SBSDKBoardingPassDocumentFormat document)
        {
            var result = "\n\n\nDetected boarding pass:\n";
            result += $"\nName: {document.Name}";
            result += $"\nSecurity data: {document.SecurityData}";
            result += $"\nElectronic Ticket: {document.ElectronicTicket}";


            if (document.Legs.Length > 0)
            {
                result += "\n\nLegs:";
                foreach (var leg in document.Legs)
                {
                    result += "\n\n-----------";
                    foreach (var field in leg.Fields)
                    {
                        result = result + $"\n{field.TypeHumanReadableString}: {field.Value}";
                    }
                }
            }

            return result;
        }

        string GetDC(SBSDKMedicalCertificateDocumentFormat document)
        {
            var result = "\n\n\nDetected DC form bar code:\n";
            if (document.Fields.Length > 0)
            {
                foreach (var field in document.Fields)
                {
                    result += $"\n{field.TypeHumanReadableString}: {field.Value}";
                }
            }

            return result;
        }

        string GetSEPA(SBSDKSEPADocumentFormat document)
        {
            var result = "\n\n\nDetected SEPA form bar code:\n";
            if (document.Fields.Length > 0)
            {
                foreach (var field in document.Fields)
                {
                    result += $"\n{field.TypeHumanReadableString}: {field.Value}";
                }
            }

            return result;
        }
    }
}
