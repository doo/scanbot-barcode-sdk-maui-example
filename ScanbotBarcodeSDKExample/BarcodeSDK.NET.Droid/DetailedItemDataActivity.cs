using System.Text;
using AndroidX.AppCompat.App;
using AndroidX.ConstraintLayout.Widget;
using IO.Scanbot.Barcodescanner.Model.BoardingPass;
using IO.Scanbot.Barcodescanner.Model.DEMedicalPlan;
using IO.Scanbot.Barcodescanner.Model.MedicalCertificate;
using IO.Scanbot.Barcodescanner.Model.SEPA;
using IO.Scanbot.Barcodescanner.Model.VCard;
using IO.Scanbot.Sdk.Barcode.Entity;

namespace BarcodeSDK.NET.Droid
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

            container.FindViewById<ImageView>(Resource.Id.image)
                .SetImageBitmap(item.Image);
            container.FindViewById<TextView>(Resource.Id.barcodeFormat)
                .Text = item.BarcodeFormat.Name();
            container.FindViewById<TextView>(Resource.Id.docFormat)
                .Text = item.FormattedData?.ToString();
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


            if (format is BoardingPassDocument boardingPassDocument)
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
            else if (format is DEMedicalPlanDocument medicalPlanDocument)
            {
                result.Append("DE Medical Plan Document").Append("\n");

                result.Append("Doctor Fields: ").Append("\n");
                foreach (DEMedicalPlanDoctorField field in medicalPlanDocument.Doctor.Fields)
                {
                    result.Append(field.Type.Name()).Append(": ").Append(field.Value).Append("\n");
                }
                result.Append("\n");

                result.Append("Patient Fields: ").Append("\n");
                foreach (DEMedicalPlanPatientField field in medicalPlanDocument.Patient.Fields)
                {
                    result.Append(field.Type.Name()).Append(": ").Append(field.Value).Append("\n");
                }
                result.Append("\n");

                result.Append("Medicine Fields: ").Append("\n");
                foreach (DEMedicalPlanStandardSubheading heading in medicalPlanDocument.Subheadings)
                {
                    foreach (DEMedicalPlanMedicine medicine in heading.Medicines)
                    {
                        foreach (DEMedicalPlanMedicineField field in medicine.Fields)
                        {
                            result.Append(field.Type.Name()).Append(": ").Append(field.Value).Append("\n");
                        }
                    }
                }
                result.Append("\n");
            }
            else if (format is MedicalCertificateDocument medicalCertDocument)
            {
                result.Append("Medical Certificate Document").Append("\n");

                foreach (MedicalCertificateDocumentField field in medicalCertDocument.Fields)
                {
                    result.Append(field.Type.Name()).Append(": ").Append(field.Value).Append("\n");
                }
            }
            else if (format is SEPADocument sepaDocument)
            {
                result.Append("SEPA Document").Append("\n");

                foreach (SEPADocumentField field in sepaDocument.Fields)
                {
                    result.Append(field.Type.Name()).Append(": ").Append(field.Value).Append("\n");
                }
            }
            else if (format is VCardDocument vCardDocument)
            {
                result.Append("VCard Document").Append("\n");

                foreach (VCardDocumentField field in vCardDocument.Fields)
                {
                    result.Append(field.Type.ToString()).Append(": ").Append(field.RawText).Append("\n");
                }
            }

            return result.ToString();
        }
    }
}
