using BarcodeItemV2 = ScanbotSDK.MAUI.Barcode.BarcodeItem;

namespace ScanbotSDK.MAUI.Example.Pages;

public class BarcodeResultTemplateSelector : DataTemplateSelector
{
    public DataTemplate BarcodeV1Template { get; set; }
    
    public DataTemplate BarcodeV2Template { get; set; }
    
    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is BarcodeItemV2)
        {
            return BarcodeV2Template;
        }

        return BarcodeV1Template;
    }
}