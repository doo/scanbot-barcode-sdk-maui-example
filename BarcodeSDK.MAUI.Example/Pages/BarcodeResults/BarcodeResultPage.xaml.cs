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
            ListView_Results.ItemsSource = barcodes;
            if (!string.IsNullOrEmpty(imagePath))
            {
                imageView.IsVisible = true;
                imageView.Source = ImageSource.FromFile(imagePath);
            }
            else
            {
                imageView.IsVisible = false;
            }
        }

        public BarcodeResultPage(List<BarcodeItem> barcodes, ImageSource imageSource)
        {
            InitializeComponent();
            ListView_Results.ItemsSource = barcodes;
            if (imageSource != null)
            {
                imageView.IsVisible = true;
                imageView.Source = imageSource;
            }
            else
            {
                imageView.IsVisible = false;
            }
        }

        public BarcodeResultPage(List<BarcodeItem> barcodes)
        {
            InitializeComponent();
            ListView_Results.ItemsSource = barcodes;
            imageView.IsVisible = false;
        }

        private void ListView_Results_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is BarcodeItem barcodeItem)
            {
                var resultPage = new BarcodeResultDetailPage();
                resultPage.NavigateData(barcodeItem);
                Navigation.PushAsync(resultPage);
            }
            
            (sender as ListView).SelectedItem = null;
        }
    }
}
