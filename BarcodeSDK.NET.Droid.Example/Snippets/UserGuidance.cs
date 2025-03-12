using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using IO.Scanbot.Sdk.Ui_v2.Common;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static BarcodeScannerScreenConfiguration UserGuidance
    {
        get
        {
            // Create the default configuration object.
            var config = new BarcodeScannerScreenConfiguration();
            
            // Hide/unhide the user guidance.
            config.UserGuidance.Visible = true;

            // Configure the title.
            config.UserGuidance.Title.Text = "Move the finder over a barcode";
            config.UserGuidance.Title.Color = new ScanbotColor("#FFFFFF");

            // Configure the background.
            config.UserGuidance.Background.FillColor = new ScanbotColor("#7A000000");

            // Configure other parameters as needed.

            return config;
        }
    }
}