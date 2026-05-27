using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using IO.Scanbot.Sdk.Ui_v2.Common;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static BarcodeScannerScreenConfiguration TopBar
    {
        get
        {
            // Create the default configuration object.
            var configuration = new BarcodeScannerScreenConfiguration();

            // Configure the top bar.

            // Set the top bar mode.
            configuration.TopBar.Mode = TopBarMode.Gradient;

            // Set the background color which will be used as a gradient.
            configuration.TopBar.BackgroundColor = new ScanbotColor("#C8193C");

            // Configure the status bar look. If visible - select Dark or Light according to your app's theme color.
            configuration.TopBar.StatusBarMode = StatusBarMode.Hidden;

            // Configure the Cancel button.
            configuration.TopBar.CancelButton.Text = "Cancel";
            configuration.TopBar.CancelButton.Foreground.Color = new ScanbotColor("#FFFFFF");

            // Configure other parameters as needed.

            return configuration;
        }
    }
}