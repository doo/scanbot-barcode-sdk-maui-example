using ScanbotSDK.MAUI.Example.Utils;
using ScanbotSDK.MAUI.Core.Barcode;

namespace ScanbotSDK.MAUI.Example;

public partial class BarcodeTypesSelectionPage : ContentPage
{
    public BarcodeTypesSelectionPage()
    {
        InitializeComponent();
        TypesList.ItemsSource = BarcodeTypes.Instance.List;
    }
}

public class BarcodeFormatCell : ViewCell
{
    public KeyValuePair<BarcodeFormat, bool> Source { get; private set; }

    public Label Label { get; set; }

    public Switch Switch { get; set; }

    public BarcodeFormatCell()
    {
        Label = new Label()
        {
            VerticalTextAlignment = TextAlignment.Center,
            Margin = new Thickness(10, 0, 0, 0),
            TextColor = Colors.Black,
            HorizontalOptions =  LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center
        };

        Switch = new Switch
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.End
        };

        View = new Grid
        {
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Fill,
            Margin = new Thickness(0, 0, 10, 0),
            Children = { Label, Switch }
        };

        Switch.Toggled += delegate
        {
            BarcodeTypes.Instance.Update(Source.Key, Switch.IsToggled);
        };
    }

    protected override void OnBindingContextChanged()
    {
        Source = (KeyValuePair<BarcodeFormat, bool>)BindingContext;
        Label.Text = Source.Key.ToString();
        Switch.IsToggled = Source.Value;

        base.OnBindingContextChanged();
    }
}