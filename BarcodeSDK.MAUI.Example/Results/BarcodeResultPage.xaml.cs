using ScanbotSDK.MAUI.Barcode.Core;

namespace ScanbotSDK.MAUI.Example.Results
{
    public partial class BarcodeResultPage : ContentPage
    {
        public BarcodeResultPage()
        {
            InitializeComponent();
        }

        public BarcodeResultPage(List<BarcodeItem> barcodes)
        {
            InitializeComponent();
            ListViewResults.ItemsSource = barcodes;
        }

        private void ListView_Results_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is BarcodeItem barcodeItem)
            {
                var resultPage = new BarcodeResultDetailPage();
                resultPage.NavigateData(barcodeItem);
                Navigation.PushAsync(resultPage);
            }

            if (sender is ListView listView)
            {
                listView.SelectedItem = null;
            }
        }
    }
}
