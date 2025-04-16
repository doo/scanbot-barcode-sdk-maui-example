namespace BarcodeSDK.NET.iOS
{
    public class AcceptedBarcodeTypesController : BaseViewController
    {
        private BarcodeListView listView;

        public override void ViewDidLoad()
        {
            PageTitle = "Accepted Types";
            base.ViewDidLoad();

            listView = new BarcodeListView();
            listView.AddItems(BarcodeTypes.Instance.AcceptedBarcodesDictionary);

            View = listView;
        }
    }
}
