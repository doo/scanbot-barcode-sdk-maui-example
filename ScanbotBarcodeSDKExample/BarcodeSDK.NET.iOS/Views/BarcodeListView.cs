using System;
using System.Collections.Generic;
using CoreGraphics;
using ScanbotBarcodeSDK.iOS;
using UIKit;

namespace BarcodeScannerExample.iOS
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

        public void AddButtons(Dictionary<SBSDKBarcodeType, bool> list)
        {
            foreach (var button in Buttons)
            {
                button.RemoveFromSuperview();
            }

            Buttons.Clear();

            foreach (var item in list)
            {
                var button = new BarcodeTypeButton(item);
                Buttons.Add(button);
                AddSubview(button);
            }

            LayoutSubviews();
        }

    }
}
