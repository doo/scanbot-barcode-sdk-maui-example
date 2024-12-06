using ScanbotSDK.MAUI.DocumentFormats.Barcode;

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

        /// <summary>
        /// Returns the formatted Barcode Generic Document result. 
        /// </summary>
        /// <param name="document">Parsed generic document object from Barcode</param>
        /// <returns></returns>
        internal static string GenericDocumentToString(ScanbotSDK.MAUI.Common.GenericDocument document)
        {
            return string.Join("\n", document.Fields
                                .Where((f) => f != null && f.Name != null && f.Name != null && f.Value.Text != null)
                                .Select((f) => string.Format("{0}: {1}", f.Name, f.Value.Text)));
        }

        /// <summary>
        /// Prints the Formatted result by accessing the ParsedDocument from Barcode object.
        /// </summary>
        /// <param name="barcode">Barcode V1 object</param>
        internal static void PrintFormattedResult(RTU.v1.Barcode barcode)
        {
            var gs1 = barcode.ParsedDocument?.Wrap<GS1>();

            if (gs1 != null)
            {
                foreach (var element in gs1.Children.Elements)
                {
                    Console.WriteLine($"ApplicationIdentifier: {element.ApplicationIdentifier.Value.Text}");
                    Console.WriteLine($"DataTitle: {element.DataTitle.Value.Text}");
                    Console.WriteLine($"ElementDescription: {element.ElementDescription.Value.Text}");
                    Console.WriteLine($"RawValue: {element.RawValue.Value.Text}");
                }
            }
        }
    }
}