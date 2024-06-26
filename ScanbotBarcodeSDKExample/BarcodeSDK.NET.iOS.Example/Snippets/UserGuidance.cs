using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;
public static partial class Snippets
{
    public static SBSDKUI2BarcodeScannerConfiguration UserGuidance
    {
        get
        {
            // Create the default configuration object.
            var config = new SBSDKUI2BarcodeScannerConfiguration();
            
            // Hide/unhide the user guidance.
            config.UserGuidance.Visible = true;

            // Configure the title.
            config.UserGuidance.Title.Text = "Move the finder over a barcode";
            config.UserGuidance.Title.Color = new SBSDKUI2Color("#FFFFFF");

            // Configure the background.
            config.UserGuidance.Background.FillColor = new SBSDKUI2Color("#7A000000");

            // Configure other parameters as needed.
            return config;
        }
    }
}