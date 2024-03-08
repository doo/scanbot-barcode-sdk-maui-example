using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using IO.Scanbot.Sdk.Ui_v2.Common;

namespace BarcodeSDK.NET.Droid.Snippets;

public class UserGuidanceConfigSnippet
{
    // Create the default configuration object.
    public BarcodeScannerConfiguration GetUserGuidanceConfigSnippetConfiguration()
    {
        // Create the default configuration object.
        var configuration = new BarcodeScannerConfiguration();

        configuration.UserGuidance = new UserGuidanceConfiguration()
        {
            // Hide/unhide the user guidance.
            Visible = true,
            
            // Configure the title.
            Title = new StyledText()
            {
                Text = "Move the finder over a barcode",
                Color = new ScanbotColor("#FFFFFF"),
            },
        };
        
        // Configure other parameters as needed.
        
        return configuration;
    }
}