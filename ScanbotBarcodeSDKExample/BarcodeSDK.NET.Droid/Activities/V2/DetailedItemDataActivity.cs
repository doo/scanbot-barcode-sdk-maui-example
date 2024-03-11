using AndroidX.AppCompat.App;
using AndroidX.ConstraintLayout.Widget;
using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using Java.Lang;

namespace BarcodeSDK.NET.Droid.Activities.V2
{
    [Activity(Theme = "@style/AppTheme")]
    public class DetailedItemDataActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.detailed_item_data);
            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var item = Intent.GetParcelableExtra("SelectedBarcodeItem") as BarcodeItem;

            if (item == null)
            {
                return;
            }

            var container = FindViewById<ConstraintLayout>(Resource.Id.container);
            
            container.FindViewById<TextView>(Resource.Id.barcodeFormat)
                .Text = item.FormattedResult?.TypeName;
            container.FindViewById<TextView>(Resource.Id.description)
                .Text = ParseFormat(item);
        }

        private string ParseFormat(BarcodeItem item)
        {
            if (item.FormattedResult == null)
            {
                return item.Text;
            }

            var format = item.FormattedResult;

            var result = new StringBuilder();
            result.Append("\n");
            
            if (format is BoardingPassDocumentFormat boardingPassDocument)
            {
                result.Append("Boarding Pass Document").Append("\n");
                result.Append(boardingPassDocument.Name).Append("\n");
            
                foreach (BoardingPassLeg leg in boardingPassDocument.Legs)
                {
                    foreach (BoardingPassLegField field in leg.Fields)
                    {
                        result.Append(field.Type.Name()).Append(": ").Append(field.Value).Append("\n");
                    }
                }
            }
            else if (format is IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration.MedicalPlanDocumentFormat medicalPlanDocument)
            {
                result.Append("DE Medical Plan Document").Append("\n");
            
                result.Append("Doctor Fields: ").Append("\n");
                foreach (IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration.MedicalPlanDoctorField field in medicalPlanDocument.Doctor.Fields)
                {
                    result.Append(field.Type.Name()).Append(": ").Append(field.Value).Append("\n");
                }
                result.Append("\n");
            
                result.Append("Patient Fields: ").Append("\n");
                foreach (IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration.MedicalPlanPatientField field in medicalPlanDocument.Patient.Fields)
                {
                    result.Append(field.Type.Name()).Append(": ").Append(field.Value).Append("\n");
                }
                result.Append("\n");
            
                result.Append("Medicine Fields: ").Append("\n");
                foreach (IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration.MedicalPlanStandardSubheading heading in medicalPlanDocument.Subheadings)
                {
                    foreach (IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration.MedicalPlanMedicine medicine in heading.Medicines)
                    {
                        foreach (IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration.MedicalPlanMedicineField field in medicine.Fields)
                        {
                            result.Append(field.Type.Name()).Append(": ").Append(field.Value).Append("\n");
                        }
                    }
                }
                result.Append("\n");
            }
            else if (format is IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration.MedicalCertificateDocumentFormat medicalCertDocument)
            {
                result.Append("Medical Certificate Document").Append("\n");
            
                foreach (MedicalCertificateDocumentField field in medicalCertDocument.Fields)
                {
                    result.Append(field.Type.Name()).Append(": ").Append(field.Value).Append("\n");
                }
            }
            else if (format is IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration.SEPADocumentFormat sepaDocument)
            {
                result.Append("SEPA Document").Append("\n");
            
                foreach (IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration.SEPADocumentFormatField field in sepaDocument.Fields)
                {
                    result.Append(field.Type.Name()).Append(": ").Append(field.Value).Append("\n");
                }
            }
            else if (format is IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration.VCardDocumentFormat vCardDocument)
            {
                result.Append("VCard Document").Append("\n");
            
                foreach (IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration.VCardDocumentFormatField field in vCardDocument.Fields)
                {
                    result.Append(field.Type.ToString()).Append(": ").Append(field.RawText).Append("\n");
                }
            }
            return result.ToString();
        }
    }
}
