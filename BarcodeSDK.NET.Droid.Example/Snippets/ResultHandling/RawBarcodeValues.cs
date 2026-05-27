using IO.Scanbot.Sdk.Barcode;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static void HandleRawBarcodeValuesAsync(BarcodeScannerResult result)
    {
        var mappedBarcodeItems = result.Barcodes
            .Select(barcode => new
            {
                BarcodeFormat = barcode.Format, // The format of the scanned barcode
                TextValue = barcode.Text, // The value of the barcode represented as a string
                RawValue = barcode.GetRawBytes(), // The raw value of the barcode
                Document = barcode.ExtractedDocument // The embedded barcode document
            })
            .ToList();

        Console.WriteLine(mappedBarcodeItems);
    }
}