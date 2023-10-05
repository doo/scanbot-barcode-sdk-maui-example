namespace BarcodeSDK.MAUI.Example.Utils
{
    public static class CommonUtils
    {
        public static bool CheckLicense(ContentPage context)
        {
            if (!ScanbotBarcodeSDK.LicenseInfo.IsValid)
            {
                Alert(context, "Oops!", "License expired or invalid");
            }

            return ScanbotBarcodeSDK.LicenseInfo.IsValid;
        }

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
    }
}