﻿using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS
{
    public class BarcodeTypeButton : UIView
    {
        public SBSDKBarcodeFormat Code { get; private set; }

        public UILabel Title { get; set; }

        public UISwitch Switch { get; private set; }

        public BarcodeTypeButton(KeyValuePair<SBSDKBarcodeFormat, bool> item)
        {
            Code = item.Key;

            Title = new UILabel();
            Title.TextColor = UIColor.DarkGray;
            Title.Font = UIFont.FromName("HelveticaNeue", 14);
            AddSubview(Title);

            Switch = new UISwitch();
            Switch.On = item.Value;
            Switch.UserInteractionEnabled = false;
            Switch.Layer.Opacity = 1.0f;
            AddSubview(Switch);

            Title.Text = Code.Name;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            var switchW = Switch.Frame.Width;
            var switchH = Switch.Frame.Height;

            nfloat padding = 5;

            nfloat x = padding;
            nfloat y = padding;
            nfloat w = Frame.Width - (3 * padding + switchW);
            nfloat h = Frame.Height - 2 * padding;

            Title.Frame = new CGRect(x, y, w, h);

            x += w;
            y = Frame.Height / 2 - switchH / 2;
            w = switchW;
            h = switchH;

            Switch.Frame = new CGRect(x, y, w, h);
        }

        public void Toggle()
        {
            Switch.SetState(!Switch.On, true);
        }

        public EventHandler<EventArgs> Click;

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            Layer.Opacity = 0.5f;
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);
            Layer.Opacity = 1.0f;
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            Layer.Opacity = 1.0f;
            Click?.Invoke(this, EventArgs.Empty);
        }
    }
}
