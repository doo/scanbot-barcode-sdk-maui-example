using ScanbotSDK.MAUI.Barcode;

namespace ScanbotSDK.MAUI.Example
{
    public partial class Snippets
    {
        public static BarcodeScannerConfiguration UserGuidance
        {
            get
            {
                // Create the default configuration object.
                var config = new BarcodeScannerConfiguration();
                
                // Hide/unhide the user guidance.
                config.UserGuidance.Visible = true;

                // Configure the title.
                config.UserGuidance.Title.Text = "Move the finder over a barcode";
                config.UserGuidance.Title.Color = new ColorValue("#FFFFFF");

                // Configure the background.
                config.UserGuidance.Background.FillColor = new ColorValue("#7A000000");

                // Configure other parameters as needed.

                return config;
            }
        }
    }
}