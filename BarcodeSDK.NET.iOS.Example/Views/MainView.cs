using BarcodeSDK.NET.iOS.Utils;

namespace BarcodeSDK.NET.iOS
{
    public class MainView : UIView
    {
        private readonly Dictionary<EventHandler, UIButton> buttons = new Dictionary<EventHandler, UIButton>();
        private readonly List<UIView> sorting = new List<UIView>();

        public MainView()
        {
            BackgroundColor = UIColor.Black;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            nfloat x = 0;
            nfloat y = 0;
            nfloat w = Frame.Width;
            nfloat h = 50;
            
            foreach (var control in sorting)
            {
                control.Frame = new CGRect(x, y, w, h);

                if (control is UITextView textView)
                {
                    var contentSize = textView.SizeThatFits(Bounds.Size);
                    textView.ContentInset = new UIEdgeInsets((h - contentSize.Height) / 2, 10, (h - contentSize.Height) / 2, 0);
                }
                y += h + 2;
            }
        }

        public UITextView CreateHeader(string text)
        {
            var existing = sorting.OfType<UITextView>().FirstOrDefault(l => l.Text == text);

            if (existing != null)
            {
                return existing;
            }

            var label = new UITextView();
            label.Text = text;
            label.TextColor = UIColor.White;
            label.BackgroundColor = Colors.ScanbotRed;
            label.TextAlignment = UITextAlignment.Left;
            label.Font = UIFont.FromName("HelveticaNeue-Bold", 16);
            label.ScrollEnabled = false;
            label.Editable = false;
            AddSubview(label);
            sorting.Add(label);
            
            return label;
        }

        public UIButton CreateItem(string text, EventHandler action)
        {
            if (buttons.TryGetValue(action, out var existing))
            {
                return existing;
            }

            var button = new UIButton();
            button.SetTitle(text, UIControlState.Normal);
            button.SetTitleColor(UIColor.White, UIControlState.Normal);
            button.TitleLabel.Font = UIFont.FromName("HelveticaNeue-Bold", 16);
            button.TitleLabel.TextAlignment = UITextAlignment.Left;
            button.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            button.BackgroundColor = UIColor.FromRGB(52, 54, 56);
            button.TitleEdgeInsets = new UIEdgeInsets(0, 15, 0, 0);
            AddSubview(button);
            button.TouchUpInside += action;
            buttons.Add(action, button);
            sorting.Add(button);

            return button;
        }
    }
}
