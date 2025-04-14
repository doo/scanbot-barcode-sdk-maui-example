using ScanbotSDK.MAUI.Barcode.Core;

namespace ScanbotSDK.MAUI.Example.Pages
{
    public partial class BarcodeResultPage : ContentPage
    {
        public BarcodeResultPage()
        {
            InitializeComponent();
        }

        public BarcodeResultPage(List<BarcodeItem> barcodes, string imagePath)
        {
            InitializeComponent();
            ListViewResults.ItemsSource = barcodes;
            if (!string.IsNullOrEmpty(imagePath))
            {
                BarcodeImageView.IsVisible = true;
                BarcodeImageView.Source = ImageSource.FromFile(imagePath);
            }
            else
            {
                BarcodeImageView.IsVisible = false;
            }
        }

        public BarcodeResultPage(List<BarcodeItem> barcodes, ImageSource imageSource)
        {
            InitializeComponent();
            ListViewResults.ItemsSource = barcodes;
            if (imageSource != null)
            {
                BarcodeImageView.IsVisible = true;
                BarcodeImageView.Source = imageSource;
            }
            else
            {
                BarcodeImageView.IsVisible = false;
            }
        }

        public BarcodeResultPage(List<BarcodeItem> barcodes)
        {
            InitializeComponent();
            ListViewResults.ItemsSource = barcodes;
            BarcodeImageView.IsVisible = false;
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
