using System;
using BarcodeSDK.NET.iOS;
using CoreGraphics;
using Foundation;
using UIKit;

namespace BarcodeScannerExample.iOS
{
    public class FlashButton : UIView
    {
        UIImage flashOn = UIImage.FromFile("flash_on_white");
        UIImage flashOff = UIImage.FromFile("flash_off_white");

        UIImageView flashIcon = new UIImageView();

        public bool IsFlashOn => flashIcon.Image == flashOn;

        public FlashButton()
        {
            flashIcon.Image = flashOff;
            AddSubview(flashIcon);

            Layer.BackgroundColor = AppDelegate.ScanbotRed.CGColor;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            var padding = 11;
            var size = Frame.Width - 2 * padding;
            flashIcon.Frame = new CGRect(padding, padding, size, size);

            Layer.CornerRadius = Bounds.Height / 2;
        }

        public EventHandler<FlashEventArgs> Click;

        public void Toggle()
        {
            if (IsFlashOn)
                flashIcon.Image = flashOff;
            else
                flashIcon.Image = flashOn;
        }
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
            Toggle();
            Click?.Invoke(this, new FlashEventArgs { Enabled = IsFlashOn });
        }
    }

    public class FlashEventArgs
    {
        public bool Enabled { get; set; }
    }
}
