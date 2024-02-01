using ScanbotSDK.MAUI.Models;

namespace ScanbotSDK.MAUI.Example.Pages
{
    public partial class BarcodeResultPage : ContentPage
    {
        public BarcodeResultPage()
        {
            InitializeComponent();
        }

        public BarcodeResultPage(List<Barcode> barcodes, string imagePath)
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

        public BarcodeResultPage(List<Barcode> barcodes, ImageSource imageSource)
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
