using System;
using UIKit;

namespace BarcodeScannerExample.iOS
{
    public class BarcodeListController : UIViewController
    {
        public BarcodeListView ContentView { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ContentView = new BarcodeListView();
            View = ContentView;

            Title = "ACCEPTED TYPES";

            ContentView.AddButtons(BarcodeTypes.Instance.List);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            foreach (var button in ContentView.Buttons)
            {
                button.Click += OnButtonClick;
            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            foreach (var button in ContentView.Buttons)
            {
                button.Click -= OnButtonClick;
            }
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
