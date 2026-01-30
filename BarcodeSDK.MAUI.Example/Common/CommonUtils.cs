using ScanbotSDK.MAUI.Barcode;
using ScanbotSDK.MAUI.Core.GenericDocument;
using ScanbotSDK.MAUI.Example.Results;

namespace ScanbotSDK.MAUI.Example.Utils;

public static class Alert
{
    /// <summary>
    /// Static function for alert. Task Awaitable.
    /// </summary>
    /// <param name="title">Title string.</param>
    /// <param name="message">Message string.</param>
    /// <returns>Returns a task object.</returns>
    public static async Task ShowAsync(string title, string message)
    {
        await MainThread.InvokeOnMainThreadAsync(async () => await App.Navigation.CurrentPage.DisplayAlertAsync(title, message, "Close"));
    }
    
    /// <summary>
    /// Static function for alert. Task Awaitable.
    /// </summary>
    /// <param name="exception">Exception Object.</param>
    /// <returns>Returns a task object.</returns>
    public static async Task ShowAsync(Exception exception)
    {
        await MainThread.InvokeOnMainThreadAsync(async () => await App.Navigation.CurrentPage.DisplayAlertAsync("Alert", exception.Message, "Close"));
    }
}

public static class CommonUtils
{
    public static async Task DisplayResultAsync(BarcodeScannerUiResult result)
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