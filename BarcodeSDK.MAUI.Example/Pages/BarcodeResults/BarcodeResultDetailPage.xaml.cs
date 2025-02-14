using ScanbotSDK.MAUI.Barcode;

namespace ScanbotSDK.MAUI.Example.Pages;

public partial class BarcodeResultDetailPage : ContentPage
{
    private BarcodeItem barcodeItem;
    public BarcodeResultDetailPage()
    {
        InitializeComponent();
    }

    internal void NavigateData(BarcodeItem barcodeItem)
    {
        this.barcodeItem = barcodeItem;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        LabelBarcodeFormatValue.Text = barcodeItem.Type.Value.ToString();
        LabelBarcodeTextValue.Text = barcodeItem.Text;
        
        if (string.IsNullOrEmpty(barcodeItem.TextWithExtension))
        {
            LabelBarcodeTextWithExtensionCaption.IsVisible = false;
            LabelBarcodeTextWithExtensionValue.IsVisible = false;
        }
        else
        {
            LabelBarcodeTextWithExtensionCaption.IsVisible = true;
            LabelBarcodeTextWithExtensionValue.IsVisible = true;
            LabelBarcodeTextWithExtensionValue.Text = barcodeItem.TextWithExtension;
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