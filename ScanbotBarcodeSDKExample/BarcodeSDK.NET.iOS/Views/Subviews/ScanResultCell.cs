using System;
using CoreGraphics;
using ScanbotBarcodeSDK.iOS;
using UIKit;

namespace BarcodeScannerExample.iOS
{
    public class ScanResultCell : UITableViewCell
    {
        public const string Identifier = "ScanResultCell";

        UIImageView image;
        UILabel text;
        UILabel type;

        public SBSDKBarcodeScannerResult Barcode { get; private set; }

        public ScanResultCell(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        public ScanResultCell()
        {
            Initialize();
        }

        private void Initialize()
        {
            image = new UIImageView();
            image.ContentMode = UIViewContentMode.ScaleAspectFit;
            image.Layer.BorderColor = UIColor.LightGray.CGColor;
            image.Layer.BorderWidth = 1;
            AddSubview(image);

            text = new UILabel();
            text.Font = UIFont.FromName("HelveticaNeue", 13);
            text.TextColor = UIColor.DarkGray;
            text.Lines = 1;
            AddSubview(text);

            type = new UILabel();
            type.Font = UIFont.FromName("HelveticaNeue", 13);
            type.TextColor = UIColor.Gray;
            type.Lines = 1;
            AddSubview(type);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            nfloat padding = 5;

            nfloat x = padding;
            nfloat y = padding;
            nfloat w = Frame.Height - 2 * padding;
            nfloat h = w;

            image.Frame = new CGRect(x, y, w, h);

            x += w + padding;
            w = Frame.Width - (w + 3 * padding);
            h = (Frame.Height - 3 * padding) / 2;

            text.Frame = new CGRect(x, y, w, h);

            y += h + padding;

            type.Frame = new CGRect(x, y, w, h);
        }

        public void Update(SBSDKBarcodeScannerResult item)
        {
            Barcode = item;

            image.Image = item.BarcodeImage;
            text.Text = item.RawTextString;
            type.Text = item.Type.Name;
        }
    }
}
