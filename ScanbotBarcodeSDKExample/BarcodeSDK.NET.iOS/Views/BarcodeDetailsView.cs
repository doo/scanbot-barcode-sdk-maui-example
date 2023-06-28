using System;
using CoreGraphics;
using ScanbotBarcodeSDK.iOS;
using UIKit;

namespace BarcodeScannerExample.iOS
{
    public class BarcodeDetailsView : UIView
    {
        UIImageView imageView;
        UILabel label;

        public BarcodeDetailsView(SBSDKBarcodeScannerResult result)
        {
            BackgroundColor = UIColor.White;

            if (result.BarcodeImage != null)
            {
                imageView = new UIImageView();
                imageView.Image = result.BarcodeImage;
                imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
                imageView.BackgroundColor = UIColor.FromRGB(245, 245, 245);
                AddSubview(imageView);
            }

            label = new UILabel();
            label.Text = ParseText(result);
            label.TextColor = UIColor.DarkGray;
            label.Lines = 0;
            AddSubview(label);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            nfloat padding = 10;

            nfloat x = padding;
            nfloat y = padding;
            nfloat w = Frame.Width - 2 * padding;
            nfloat h = w;

            if (imageView != null)
            {
                imageView.Frame = new CGRect(x, y, w, h);
                y += h;
            }

            label.Frame = new CGRect(x, y, w, h);
        }

        string ParseText(SBSDKBarcodeScannerResult barcode)
        {
            if (barcode.FormattedResult == null)
            {
                return barcode.RawTextString;
            }

            return BarcodeFormatter.Instance.GetText(barcode);
        }
    }
}
