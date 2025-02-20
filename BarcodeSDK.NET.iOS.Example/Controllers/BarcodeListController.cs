namespace BarcodeSDK.NET.iOS
{
    public class BarcodeListController : UIViewController
    {
        private BarcodeListView listView;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View = listView = new BarcodeListView();
            Title = "ACCEPTED TYPES";
            listView.AddButtons(BarcodeTypes.Instance.AcceptedBarcodesDictionary);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            foreach (var button in listView.Buttons)
            {
                button.Click += OnButtonClick;
            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            foreach (var button in listView.Buttons)
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
