using ScanbotSDK.MAUI.Barcode;

namespace ScanbotSDK.MAUI.Example;

public partial class Snippets
{
    public static void HandleRawBarcodeValues(BarcodeScannerUiResult result)
    {
        var mappedBarcodeItems = result.Items
            .Select(item =>
            {
                var barcode = item.Barcode;
                return new
                {
                    BarcodeFormat = barcode.Format, // The format of the scanned barcode
                    TextValue = barcode.Text, // The value of the barcode represented as a string
                    RawValue = barcode.RawBytes, // The raw value of the barcode
                    Document = barcode.ExtractedDocument // The embedded barcode document
                };
            })
            .ToList();

        Console.WriteLine(mappedBarcodeItems);
    }
}