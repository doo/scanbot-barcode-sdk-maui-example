using BarcodeItemV1 = ScanbotSDK.MAUI.Barcode.RTU.v1.Barcode;
using BarcodeItemV2 = ScanbotSDK.MAUI.Barcode.BarcodeItem;

namespace ScanbotSDK.MAUI.Example.Pages
{
    public partial class BarcodeResultPage : ContentPage
    {
        public BarcodeResultPage()
        {
            InitializeComponent();
        }

        public BarcodeResultPage(List<BarcodeItemV1> barcodes, string imagePath)
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

        public BarcodeResultPage(List<BarcodeItemV1> barcodes, ImageSource imageSource)
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

        public BarcodeResultPage(List<BarcodeItemV2> barcodes)
        {
            InitializeComponent();
            ListView_Results.ItemsSource = barcodes;
            imageView.IsVisible = false;
        }

        private void ListView_Results_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is BarcodeItemV2 barcodeItem)
            {
                var resultPage = new BarcodeResultDetailPage();
                resultPage.NavigateData(barcodeItem);
                Navigation.PushAsync(resultPage);
            }
            
            (sender as ListView).SelectedItem = null;
        }
    }
}
