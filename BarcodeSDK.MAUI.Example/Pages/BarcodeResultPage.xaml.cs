using ScanbotSDK.MAUI.Barcode;
using Microsoft.Maui.Graphics.Platform;

namespace ScanbotSDK.MAUI.Example.Pages
{
    public partial class BarcodeResultPage : ContentPage
    {
        public BarcodeResultPage()
        {
            InitializeComponent();
        }

        public BarcodeResultPage(BarcodeItem[] barcodes)
        {
            InitializeComponent();
        }

        public BarcodeResultPage(Barcode.RTU.v1.Barcode[] barcodes, string imagePath)
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

        public BarcodeResultPage(Barcode.RTU.v1.Barcode[] barcodes, PlatformImage image)
        {
            InitializeComponent();
            ListView_Results.ItemsSource = barcodes;
            if (image != null)
            {
                imageView.IsVisible = true;
                imageView.Source = ImageSource.FromStream(() => image.AsStream());
            }
            else
            {
                imageView.IsVisible = false;
            }
        }
    }
}
