using ScanbotSDK.MAUI.Example.Utils;
using ScanbotSDK.MAUI.Core.Barcode;

namespace ScanbotSDK.MAUI.Example;

public partial class BarcodeTypesSelectionPage : ContentPage
{
    public BarcodeTypesSelectionPage()
    {
        InitializeComponent();
        // Create a copy and remove the type None from the list
        Dictionary<BarcodeFormat, bool> removedTypeNone = new Dictionary<BarcodeFormat, bool>(BarcodeTypes.Instance.List);
        removedTypeNone.Remove(BarcodeFormat.None);
        
        TypesList.ItemsSource = removedTypeNone;
    }

    private void Switch_OnToggled(object sender, ToggledEventArgs e)
    {
        if ((sender as Switch)?.BindingContext is not KeyValuePair<BarcodeFormat, bool> item) return;

        // updated the item.
        BarcodeTypes.Instance.Update(item.Key, e.Value);
    }
}