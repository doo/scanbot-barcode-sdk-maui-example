using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKUI2BarcodeScannerScreenConfiguration TopBar
    {
        get
        {
            // Create the default configuration object.
            var configuration = new SBSDKUI2BarcodeScannerScreenConfiguration();

            // Configure the top bar.

            // Set the top bar mode.
            configuration.TopBar.Mode = SBSDKUI2TopBarMode.Gradient;

            // Set the background color which will be used as a gradient.
            configuration.TopBar.BackgroundColor = new SBSDKUI2Color("#C8193C");

            // Configure the status bar look. If visible - select Dark or Light according to your app's theme color.
            configuration.TopBar.StatusBarMode = SBSDKUI2StatusBarMode.Hidden;

            // Configure the Cancel button.
            configuration.TopBar.CancelButton.Text = "Cancel";
            configuration.TopBar.CancelButton.Foreground.Color = new SBSDKUI2Color("#FFFFFF");

            // Configure other parameters as needed.

            return configuration;
        }
    }
}