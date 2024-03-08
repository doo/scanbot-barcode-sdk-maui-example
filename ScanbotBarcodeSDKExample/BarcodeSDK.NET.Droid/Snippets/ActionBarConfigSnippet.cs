using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using IO.Scanbot.Sdk.Ui_v2.Common;

namespace BarcodeSDK.NET.Droid.Snippets;

public class ActionBarConfigSnippet
{
    public BarcodeScannerConfiguration GetActionBarConfigSnippetConfiguration()
    {
        var configuration = new IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration.BarcodeScannerConfiguration();
            
        configuration.ActionBar.FlashButton.Visible = true;
        configuration.ActionBar.FlashButton.BackgroundColor = new ScanbotColor("#7A000000");
        configuration.ActionBar.FlashButton.ForegroundColor = new ScanbotColor("#FFFFFF");
            
        configuration.ActionBar.FlashButton.ActiveBackgroundColor = new ScanbotColor("#FFCE5C");
        configuration.ActionBar.FlashButton.ActiveForegroundColor = new ScanbotColor("#000000");
            
        configuration.ActionBar.ZoomButton.Visible = true;
        configuration.ActionBar.ZoomButton.BackgroundColor = new ScanbotColor("#7A000000");
        configuration.ActionBar.ZoomButton.ForegroundColor = new ScanbotColor("#FFFFFF");
            
        configuration.ActionBar.FlipCameraButton.Visible = true;
        configuration.ActionBar.FlipCameraButton.BackgroundColor = new ScanbotColor("#7A000000");
        configuration.ActionBar.FlipCameraButton.ForegroundColor = new ScanbotColor("#FFFFFF");

        return configuration;
    }
}