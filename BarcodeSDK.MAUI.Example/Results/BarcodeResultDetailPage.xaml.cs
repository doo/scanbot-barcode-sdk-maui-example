using System.Collections.ObjectModel;
using ScanbotSDK.MAUI.BarcodeDocumentModel;
using ScanbotSDK.MAUI.Core.Barcode;
using ScanbotSDK.MAUI.Core.GenericDocument;

namespace ScanbotSDK.MAUI.Example.Results;

/// <summary>
/// Model used to display the Barcode details in a collection view via binding.
/// </summary>
public class BarcodeDetailModel(string name, string value)
{
    public string PropertyName { get; set; } = name;
    public string PropertyValue { get; set; } = value;
}

public partial class BarcodeResultDetailPage : ContentPage
{
    private BarcodeItem barcodeItem;

    private List<BarcodeDetailModel> barcodeDetailList = new List<BarcodeDetailModel>();

    public List<BarcodeDetailModel> BarcodeDetailList
    {
        get => barcodeDetailList;
        set
        {
            barcodeDetailList = value;
            OnPropertyChanged(nameof(BarcodeDetailList));
        }
    }

    public BarcodeResultDetailPage()
    {
        InitializeComponent();
    }

    internal void NavigateData(BarcodeItem selectedBarcodeItem)
    {
        this.barcodeItem = selectedBarcodeItem;
        InitBarcodeDetailList();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CollectionViewBarcodeDetails.ItemsSource = BarcodeDetailList;
    }

    private void InitBarcodeDetailList()
    {
        var list = new List<BarcodeDetailModel>
        {
            new(nameof(barcodeItem.Format), barcodeItem.Format.ToString()),
            new(nameof(barcodeItem.Text), barcodeItem.Text)
        };

        if (!string.IsNullOrEmpty(barcodeItem.UpcEanExtension))
        {
            list.Add(new("Extension", barcodeItem.UpcEanExtension));
        }

        if (barcodeItem.ExtractedDocument != null)
        {
            list.AddRange(GetFormattedDocument(this.barcodeItem.ExtractedDocument));
        }

        BarcodeDetailList = list;
    }

    private ObservableCollection<BarcodeDetailModel> GetFormattedDocument(GenericDocument document)
    {
        var docType = document.Type.Name;
        switch (docType)
        {
            case nameof(AAMVA):
                var amvaDocument = new AAMVA(document);
                return
                [
                    new("Aamva version number", amvaDocument.Version.Value?.Text),
                    new("Issuer identification number", amvaDocument.IssuerIdentificationNumber.Value?.Text),
                    new("Jurisdiction identification number", amvaDocument.JurisdictionVersionNumber?.Value?.Text)
                ];
            case nameof(BoardingPass):
                var boardingPass = new BoardingPass(document);
                return
                [
                    new("Name", boardingPass.PassengerName.Value?.Text),
                    new("Security data", boardingPass.SecurityData?.Value?.Text),
                    new("Electronic ticket", boardingPass.ElectronicTicketIndicator.Value?.Text),
                    new("Number of legs", boardingPass.NumberOfLegs.Value?.Text)
                ];
            case nameof(DEMedicalPlan):
                var deMedicalPlan = new DEMedicalPlan(document);
                return
                [
                    new("Name", deMedicalPlan.DocumentVersionNumber.Value?.Text),
                    new("Security data", deMedicalPlan.TotalNumberOfPages.Value?.Text),
                    new("Patch version number", deMedicalPlan.PatchVersionNumber.Value?.Text),
                    new("Language country code", deMedicalPlan.LanguageCountryCode.Value?.Text)
                ];
            case nameof(GS1):
                var gs1Document = new GS1(document);
                foreach (var element in gs1Document.Children.Elements)
                {
                    return
                    [
                        new("Description", element.ElementDescription.Value?.Text),
                        new("Raw value", element.RawValue.Value?.Text),
                        new("Data title", element.DataTitle.Value?.Text),
                        new("Application Id", element.ApplicationIdentifier.Value?.Text),
                        new("Validation errors", element.Children.ValidationErrors.Count().ToString())
                    ];
                }

                break;

            case nameof(HIBC):
                var hibcDocument = new HIBC(document);
                return
                [
                    new("Serial Number", hibcDocument.SerialNumber?.Value?.Text),
                    new("Quantity", hibcDocument.Quantity?.Value?.Text),
                    new("Date Of Manufacture", hibcDocument.DateOfManufacture?.Value?.Text),
                    new("LOT number", hibcDocument.LotNumber?.Value?.Text),
                    new("Expiry date day", hibcDocument.ExpiryDateDay?.Value?.Text),
                    new("Labeler Id", hibcDocument.LabelerIdentificationCode.Value?.Text),
                    new("Labeler product/catalog number", hibcDocument.LabelersProductOrCatalogNumber.Value?.Text)
                ];

            case nameof(IDCardPDF417):
                var idCardDocument = new IDCardPDF417(document);
                return
                [
                    new("Document code", idCardDocument.DocumentCode.Value?.Text),
                    new("Date issued", idCardDocument.DateIssued.Value?.Text),
                    new("Date expired", idCardDocument.DateExpired.Value?.Text),
                    new("First name", idCardDocument.FirstName.Value?.Text),
                    new("Last name", idCardDocument.LastName.Value?.Text),
                    new("Birth date", idCardDocument.BirthDate.Value?.Text),
                    new("Optional", idCardDocument.Optional.Value?.Text)
                ];

            case nameof(MedicalCertificate):
                var medicalCertificateDocument = new MedicalCertificate(document);
                return
                [
                    new("First name", medicalCertificateDocument.FirstName?.Value?.Text),
                    new("Last name", medicalCertificateDocument.LastName?.Value?.Text),
                    new("Birth date", medicalCertificateDocument.BirthDate?.Value?.Text),
                    new("Doctor number", medicalCertificateDocument.DoctorNumber?.Value?.Text),
                    new("Health insurance number", medicalCertificateDocument.HealthInsuranceNumber?.Value?.Text)
                ];

            case nameof(SEPA):
                var sepaDocument = new SEPA(document);
                return
                [
                    new("Version", sepaDocument.Version.Value?.Text),
                    new("Amount", sepaDocument.Amount?.Value?.Text),
                    new("Character set", sepaDocument.CharacterSet.Value?.Text),
                    new("Purpose", sepaDocument.Purpose?.Value?.Text),
                    new("Identification", sepaDocument.Identification.Value?.Text),
                    new("Information", sepaDocument.Information?.Value?.Text),
                    new("Receiver BIC", sepaDocument.ReceiverBIC.Value?.Text),
                    new("Receiver IBAN", sepaDocument.ReceiverIBAN.Value?.Text),
                    new("ReceiverName", sepaDocument.ReceiverName.Value?.Text),
                    new("Remittance", sepaDocument.Remittance?.Value?.Text)
                ];

            case nameof(SwissQR):
                var swissQrDocument = new SwissQR(document);
                return
                [
                    new("Major version", swissQrDocument.MajorVersion.Value?.Text),
                    new("Amount", swissQrDocument.Amount?.Value?.Text),
                    new("Due date", swissQrDocument.DueDate?.Value?.Text),
                    new("Currency", swissQrDocument.Currency?.Value?.Text),
                    new("Debtor country", swissQrDocument.DebtorCountry?.Value?.Text),
                    new("Debtor name", swissQrDocument.DebtorName?.Value?.Text),
                    new("IBAN", swissQrDocument.IBAN?.Value?.Text)
                ];

            case nameof(VCard):
                var vCardDocument = new VCard(document);
                return
                [
                    new("Name", vCardDocument.Children.Name?.RawValue.Value?.Text),
                    new("Title", vCardDocument.Children.Titles?.FirstOrDefault()?.RawValue.Value?.Text),
                    new("Formatted name", vCardDocument.Children.FormattedName?.RawValue.Value?.Text),
                    new("Birthday", vCardDocument.Children.Birthday?.RawValue.Value?.Text),
                    new("Email", vCardDocument.Children.Emails?.FirstOrDefault()?.RawValue.Value?.Text),
                    new("Role", vCardDocument.Children.Roles?.FirstOrDefault()?.RawValue.Value?.Text)
                ];
            default:
                return [];
        }
        
        return [];
    }
}