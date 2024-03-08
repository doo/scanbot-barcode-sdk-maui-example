using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using IO.Scanbot.Sdk.Ui_v2.Common;

namespace BarcodeSDK.NET.Droid.Snippets;

public class TopBarConfigSnippet
{
    // Create the default configuration object.
    public BarcodeScannerConfiguration GetTopBarConfigSnippetConfiguration()
    {
        // Create the default configuration object.
        var configuration = new BarcodeScannerConfiguration();

        configuration.TopBar = new TopBarConfiguration()
        {
            // Configure the top bar.

            // Set the top bar mode.
            Mode = TopBarMode.Gradient,
            BackgroundColor = new ScanbotColor("#C8193C"),
            
            // Configure the status bar look.
            StatusBarMode = StatusBarMode.Hidden,
            
            // Configure the Cancel button.
            CancelButton = new ButtonConfiguration()
            {   
                Text = "Cancel",
                Foreground = new ForegroundStyle()
                {
                    Color = new ScanbotColor("#FFFFFF"),
                }
            },
        };
        
        // Configure other parameters as needed.
        
        return configuration;
    }
}