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
        
        // =========================================================
        // Returns the formatted Barcode Generic Document result. 
        // =========================================================
        internal static string GenericDocumentToString(ScanbotSDK.MAUI.Common.GenericDocument document)
        {
            return string.Join("\n", document.Fields
                                .Where((f) => f != null && f.Name != null && f.Name != null && f.Value.Text != null)
                                .Select((f) => string.Format("{0}: {1}", f.Name, f.Value.Text)));
        }
    }
}