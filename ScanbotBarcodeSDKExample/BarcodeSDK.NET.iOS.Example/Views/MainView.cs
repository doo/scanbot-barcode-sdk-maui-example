namespace BarcodeSDK.NET.iOS
{
    public class MainView : UIView
    {
        private readonly Dictionary<EventHandler, UIButton> buttons = new Dictionary<EventHandler, UIButton>();
        private readonly List<UIButton> sorting = new List<UIButton>();

        public MainView()
        {
            BackgroundColor = UIColor.White;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            nfloat padding = 10;

            nfloat x = padding;
            nfloat y = padding;
            nfloat w = Frame.Width - 2 * padding;
            nfloat h = w / 7.5f;

            foreach (var button in sorting)
            {
                button.Frame = new CGRect(x, y, w, h);
                y += h + padding;
            }
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

        public void RemoveAllButtons()
        {
            var keys = buttons.Keys;

            foreach (var key in keys)
            {
                RemoveButton(key);
            }
        }
    }
}
