using ScanbotSDK.MAUI.Example.Utils;
using ScanbotSDK.MAUI.Barcode;

namespace ScanbotSDK.MAUI.Example
{
    public class BarcodeSelectionPage : ContentPage
    {
        public BarcodeSelectionPage()
        {
            Title = "Accepted Barcodes";

            var list = new ListView
            {
                ItemTemplate = new DataTemplate(typeof(BarcodeFormatCell)),
                ItemsSource = BarcodeTypes.Instance.List,
                RowHeight = 50,
                BackgroundColor = Colors.White,
            };
            Content = list;
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
}
