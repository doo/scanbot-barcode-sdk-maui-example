using System.Linq;

namespace BarcodeSDK.NET.iOS
{
    public class MainView : UIView
    {
        private UIColor scanbotColor = UIColor.FromRGB(0xc8, 0x19, 0x3c);
        private readonly Dictionary<EventHandler, UIButton> buttons = new Dictionary<EventHandler, UIButton>();
        private readonly List<UIView> sorting = new List<UIView>();

        public MainView()
        {
            BackgroundColor = UIColor.White;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            nfloat padding = 10;

            nfloat x = padding;
            nfloat y = 0;
            nfloat w = Frame.Width - 2 * padding;
            nfloat h = w / 7.5f;
            
            foreach (var control in sorting)
            {
                control.Frame = new CGRect(0, y, Frame.Width, h);

                if (control is UITextView textView)
                {
                    var contentSize = textView.SizeThatFits(Bounds.Size);
                    textView.ContentInset = new UIEdgeInsets((h - contentSize.Height) / 2, 0, (h - contentSize.Height) / 2, 0);
                }

                y += h + padding;
            }
        }

        public UITextView CreateText(string text)
        {
            var existing = sorting.OfType<UITextView>().FirstOrDefault(l => l.Text == text);

            if (existing != null)
            {
                return existing;
            }

            var label = new UITextView();
            label.Text = text;
            label.TextColor = UIColor.White;
            label.BackgroundColor = scanbotColor;
            label.Font = UIFont.FromName("HelveticaNeue", 14);
            AddSubview(label);
            sorting.Add(label);

            return label;
        }

        public UIButton CreateButton(string text, EventHandler action)
        {
            if (buttons.TryGetValue(action, out var existing))
            {
                return existing;
            }

            var button = new UIButton();
            button.SetTitle(text, UIControlState.Normal);
            button.SetTitleColor(UIColor.FromRGB(10, 132, 255), UIControlState.Normal);
            button.TitleLabel.Font = UIFont.FromName("HelveticaNeue", 14);
            AddSubview(button);
            button.TouchUpInside += action;
            buttons.Add(action, button);
            sorting.Add(button);

            return button;
        }

        public void RemoveButton(EventHandler action)
        {
            if (buttons.TryGetValue(action, out var button))
            {
                button.TouchUpInside -= action;
                buttons.Remove(action);
                sorting.Remove(button);
                button.RemoveFromSuperview();
            }
        }

        public void RemoveAllControls()
        {
            var keys = buttons.Keys;

            foreach (var key in keys)
            {
                RemoveButton(key);
            }

            var controlsToRemove = sorting.ToArray();

            foreach (var control in controlsToRemove)
            {
                sorting.Remove(control);
                control.RemoveFromSuperview();
            }
        }
    }
}
