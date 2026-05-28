using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static void HandleRawBarcodeValues(SBSDKUI2BarcodeScannerUIResult result)
    {
        var mappedBarcodeItems = result.Items
            .Select(item => new
            {
                BarcodeFormat = item.Barcode.Format, // The format of the scanned barcode
                TextValue = item.Barcode.Text, // The value of the barcode represented as a string
                RawValue = item.Barcode.RawBytes, // The raw value of the barcode
                Document = item.Barcode.ExtractedDocument // The embedded barcode document
            })
            .ToList();

        Console.WriteLine(mappedBarcodeItems.Select(barcode =>
            $"Format: {barcode.BarcodeFormat}, Text: {barcode.TextValue}"));
    }
}