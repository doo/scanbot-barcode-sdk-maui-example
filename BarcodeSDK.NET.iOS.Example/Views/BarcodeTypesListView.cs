using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS
{
    public class BarcodeListView : UIScrollView
    {
        public readonly List<BarcodeTypeButton> Buttons = new List<BarcodeTypeButton>();

        public BarcodeListView()
        {
            BackgroundColor = UIColor.White;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            nfloat padding = 5;

            nfloat x = padding;
            nfloat y = padding;
            nfloat w = Frame.Width - 2 * padding;
            nfloat h = w / 7;

            foreach (var button in Buttons)
            {
                button.Frame = new CGRect(x, y, w, h);
                y += h + padding;
            }

            ContentSize = new CGSize(Frame.Width, y);
        }

        public void AddItems(Dictionary<SBSDKBarcodeFormat, bool> list)
        {
            foreach (var button in Buttons)
            {
                button.RemoveFromSuperview();
            }

            Buttons.Clear();

            foreach (var item in list)
            {
                var button = new BarcodeTypeButton(item);
                button.Click += OnButtonClick;
                
                Buttons.Add(button);
                AddSubview(button);
            }

            LayoutSubviews();
        }
        
        private void OnButtonClick(object sender, EventArgs e)
        {
            var button = (BarcodeTypeButton)sender;
            button.Toggle();

            var isOn = button.Switch.On;
            BarcodeTypes.Instance.Update(button.Code, isOn);
        }
    }
}
