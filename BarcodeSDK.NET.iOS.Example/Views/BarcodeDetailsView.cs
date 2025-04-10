using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS
{
    public class BarcodeDetailsView : UIView
    {
        private readonly UIImageView imageView;
        private readonly UILabel labelBarcodeText;
        private readonly UILabel labelBarcodeFormat;
        private readonly UILabel labelBarcodeGenericDocument;

        public BarcodeDetailsView(SBSDKBarcodeItem result)
        {
            BackgroundColor = UIColor.White;
            imageView = new UIImageView
            {
                ContentMode = UIViewContentMode.ScaleAspectFit,
                BackgroundColor = UIColor.FromRGB(245, 245, 245),
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            
            if (result != null)
            {
                imageView.Image = result.SourceImage?.ToUIImage();
                labelBarcodeFormat = GetNewLabel(result.Format.Name);
                labelBarcodeText = GetNewLabel(result.Text);
                labelBarcodeGenericDocument = GetNewLabel("");
                
                if (result.ExtractedDocument != null)
                {
                    labelBarcodeGenericDocument.Text = string.Empty; //GetFormattedDocument(result.ExtractedDocument);
                }

                labelBarcodeFormat.Font = UIFont.BoldSystemFontOfSize(18f);
            }
            
            AddSubviews(imageView, labelBarcodeFormat, labelBarcodeText, labelBarcodeGenericDocument);
            AddViewConstraints();
        }

        private void AddViewConstraints()
        {
            var leftConstraintImage = NSLayoutConstraint.Create(imageView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, this, NSLayoutAttribute.Left, 1, 0);
            var rightConstraintImage = NSLayoutConstraint.Create(imageView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, this, NSLayoutAttribute.Right, 1, 0);
            var topConstraintImage = NSLayoutConstraint.Create(imageView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this, NSLayoutAttribute.Top, 1,0);
            var heightConstraintImage = NSLayoutConstraint.Create(imageView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, 100);
            AddConstraints([leftConstraintImage, rightConstraintImage, topConstraintImage, heightConstraintImage]);

            AddLabelConstraints(labelBarcodeFormat, imageView);
            AddLabelConstraints(labelBarcodeText, labelBarcodeFormat);
            AddLabelConstraints(labelBarcodeGenericDocument, labelBarcodeText);
            
            // var bottomConstraint = NSLayoutConstraint.Create(labelBarcodeFormat, NSLayoutAttribute.Bottom, NSLayoutRelation.GreaterThanOrEqual, this, NSLayoutAttribute.Bottom, 1, -15);
            // bottomConstraint.Priority = 500;
            // AddConstraints([bottomConstraint]);
        }

        private void AddLabelConstraints(UIView newView, UIView prevView)
        {
            var leftConstraintLabel = NSLayoutConstraint.Create(newView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, this, NSLayoutAttribute.Left, 1, 15);
            var rightConstraintLabel = NSLayoutConstraint.Create(newView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, this, NSLayoutAttribute.Right, 1, -15);
            var topConstraintLabel = NSLayoutConstraint.Create(newView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, prevView, NSLayoutAttribute.Bottom, 1,9);
            AddConstraints([leftConstraintLabel, rightConstraintLabel, topConstraintLabel]);
        }

        UILabel GetNewLabel(string text)
        {
            return new UILabel
            {
                TextColor = UIColor.DarkGray,
                Lines = 0,
                TranslatesAutoresizingMaskIntoConstraints = false,
                Text = text
            };
        }
        
        
        private string GetFormattedDocument(SBSDKGenericDocument document)
        {
            var formattedDocumentText = string.Empty;
            var docType = document.Type.Name;
            if (SBSDKBarcodeDocumentModelConstants.AamvaDocumentType == docType)
            {
                var amvaDocument = new SBSDKBarcodeDocumentModelAAMVA(document);
                formattedDocumentText += $"File type: {amvaDocument.RequiredDocumentType}\n";
                formattedDocumentText += $"Aamva version number: {amvaDocument.Version}\n";
                formattedDocumentText += $"Issuer Identification Number: {amvaDocument.IssuerIdentificationNumber}\n";
                formattedDocumentText += $"Jurisdiction Identification Number: {amvaDocument.JurisdictionVersionNumber}\n";
                return formattedDocumentText;
            }

            if (SBSDKBarcodeDocumentModelConstants.BoardingPassDocumentType == docType)
            {
                var boardingPass = new SBSDKBarcodeDocumentModelBoardingPass(document);
                formattedDocumentText += $"File type: {boardingPass.Name.Type.Name}\n";
                formattedDocumentText += $"Security Data: {boardingPass.SecurityData.Type.Name}\n";
                formattedDocumentText += $"Electronic Ticket: {boardingPass.ElectronicTicket}\n";
                formattedDocumentText += $"Number of legs: {boardingPass.NumberOfLegs}\n";
                return formattedDocumentText;
            }
            
            if (SBSDKBarcodeDocumentModelConstants.DeMedicalPlanDocumentType == docType)
            {
                var medicalPlan = new SBSDKBarcodeDocumentModelDEMedicalPlan(document);
                formattedDocumentText += $"Document version number: {medicalPlan.DocumentVersionNumber}\n";
                formattedDocumentText += $"Security Data: {medicalPlan.TotalNumberOfPages.}\n";
                formattedDocumentText += $"Electronic Ticket: {medicalPlan.ElectronicTicket}\n";
                formattedDocumentText += $"Number of legs: {medicalPlan.NumberOfLegs}\n";
                return formattedDocumentText;
            }
            
            if (SBSDKBarcodeDocumentModelConstants.Gs1DocumentType == docType)
            {
                return "";
            }
            
            if (SBSDKBarcodeDocumentModelConstants.HibcDocumentType == docType)
            {
                return "";
            }
            
            if (SBSDKBarcodeDocumentModelConstants.IdCardPDF417DocumentType == docType)
            {
                return "";
            }
            
            if (SBSDKBarcodeDocumentModelConstants.MedicalCertificateDocumentType == docType)
            {
                return "";
            }
            
            if (SBSDKBarcodeDocumentModelConstants.SepaDocumentType == docType)
            {
                return "";
            }
            
            if (SBSDKBarcodeDocumentModelConstants.SwissQRDocumentType == docType)
            {
                return "";
            }
            
            if (SBSDKBarcodeDocumentModelConstants.VCardDocumentType == docType)
            {
                return "";
            }
            throw new NotImplementedException();
        }
    }
}
