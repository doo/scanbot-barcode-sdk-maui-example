using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Core.GenericDocument;
using ScanbotSDK.MAUI.Example.Results;

namespace ScanbotSDK.MAUI.Example.Utils;
public static class CommonUtils
{
    public static async void Alert(this Page page, string title, string message)
    {
        await page.DisplayAlertAsync(title, message, "Close");
    }
        
    public static async Task AlertAsync(this Page page, string title, string message)
    {
        await page.DisplayAlertAsync(title, message, "Close");
    }

    public static async Task DisplayResults(BarcodeScannerUiResult result)
    {
        if (result?.Items?.Length > 0)
        {
            var items = result.Items.Select(item => item.Barcode);
            await App.Navigation.PushAsync(new BarcodeResultPage(items.ToList()));
        }
    }
        
    internal static string ToGdrString(this GenericDocument document)
    {
        var formattedString = string.Empty;
        if (document?.Fields == null) return formattedString;
		
        foreach (var field in document.Fields)
        {
            if (string.IsNullOrEmpty(field?.Type?.Name))
                continue;
            formattedString += $"{field.Type.Name}: {field.Value?.Text ?? "-"}\n";
        }

        return formattedString;
    }
}