using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS
{
    public class BarcodeDetailsView : UIView
    {
        private readonly UIImageView imageView;
        private readonly UILabel label;

        public BarcodeDetailsView(SBSDKBarcodeItem result)
        {
            BackgroundColor = UIColor.White;
            imageView = new UIImageView
            {
                ContentMode = UIViewContentMode.ScaleAspectFit,
                BackgroundColor = UIColor.FromRGB(245, 245, 245)
            };

            label = new UILabel
            {
                TextColor = UIColor.DarkGray,
                Lines = 0
            };
            
            AddSubviews(imageView, label);
            if (result != null)
            {
                imageView.Image = result.SourceImage?.ToUIImage();
                label.Text = result.Text;
            }
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
    }
}
