using ScanbotSDK.MAUI.Core.Barcode;

namespace ScanbotSDK.MAUI.Example.Results;

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

    private void ResultItemTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is not BarcodeItem barcodeItem)
            return;

        var resultPage = new BarcodeResultDetailPage();
        resultPage.NavigateData(barcodeItem);
        Navigation.PushAsync(resultPage);

        if (sender is CollectionView collectionView)
        {
            collectionView.SelectedItem = null;
        }
    }
}