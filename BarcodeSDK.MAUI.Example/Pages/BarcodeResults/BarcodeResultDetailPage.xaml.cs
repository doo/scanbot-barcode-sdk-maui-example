using ScanbotSDK.MAUI.Barcode.Core;

namespace ScanbotSDK.MAUI.Example.Pages;

public partial class BarcodeResultDetailPage : ContentPage
{
    private BarcodeItem barcodeItem;
    public BarcodeResultDetailPage()
    {
        InitializeComponent();
    }

    internal void NavigateData(BarcodeItem BarcodeScannerUiItem)
    {
        this.barcodeItem = BarcodeScannerUiItem;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        LabelBarcodeFormatValue.Text = barcodeItem.Format.ToString();
        LabelBarcodeTextValue.Text = barcodeItem.Text;
        
        if (string.IsNullOrEmpty(barcodeItem.UpcEanExtension))
        {
            LabelBarcodeTextWithExtensionCaption.IsVisible = false;
            LabelBarcodeTextWithExtensionValue.IsVisible = false;
        }
        else
        {
            LabelBarcodeTextWithExtensionCaption.IsVisible = true;
            LabelBarcodeTextWithExtensionValue.IsVisible = true;
            LabelBarcodeTextWithExtensionValue.Text = barcodeItem.UpcEanExtension;
        }

        if (barcodeItem.RawBytes != null && barcodeItem.RawBytes.Length > 1)
        {
            LabelBarcodeRawBytesCaption.IsVisible = true;
            LabelBarcodeRawBytesValue.IsVisible = true;
            LabelBarcodeRawBytesValue.Text = System.Text.Encoding.Default.GetString(barcodeItem.RawBytes);
        }
        else
        {
            LabelBarcodeRawBytesCaption.IsVisible = false;
            LabelBarcodeRawBytesValue.IsVisible = false;
        }
    }
}