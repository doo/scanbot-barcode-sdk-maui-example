using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Example.Results;

namespace ScanbotSDK.MAUI.Example.Utils
{
    public static class CommonUtils
    {
        public static async void Alert(ContentPage context, string title, string message)
        {
            await context.DisplayAlert(title, message, "Close");
        }

        public static ImageSource Copy(ImageSource original)
        {
            var streamImageSource = (StreamImageSource)original;
            var cancellationToken = System.Threading.CancellationToken.None;
            Task<Stream> task = streamImageSource.Stream(cancellationToken);
            Stream stream = task.Result;
            return ImageSource.FromStream(() => stream);
        }

        public static async Task DisplayResults(BarcodeScannerUiResult result)
        {
            if (result?.Items?.Length > 0)
            {
                var items = result.Items.Select(item => item.Barcode);
                await Application.Current.MainPage.Navigation.PushAsync(new BarcodeResultPage(items.ToList()));
            }
        }
    }
}