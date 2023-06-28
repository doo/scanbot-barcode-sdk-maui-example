using BarcodeItem = BarcodeSDK.MAUI.Models.Barcode;

namespace BarcodeSDK.MAUI.Example.Pages
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
    }
}
