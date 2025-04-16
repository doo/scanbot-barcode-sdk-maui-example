using IO.Scanbot.Sdk.Ui_v2.Barcode.Configuration;
using IO.Scanbot.Sdk.Ui_v2.Common;

namespace BarcodeSDK.NET.Droid;

public static partial class Snippets
{
    public static BarcodeScannerScreenConfiguration ActionBar
    {
        get
        {
            // Create the default configuration object.
            var config = new BarcodeScannerScreenConfiguration();
            
            // Configure the action bar.
            // Hide/Show the flash button.
            config.ActionBar.FlashButton.Visible = true;

            // Configure the inactive state of the flash button.
            config.ActionBar.FlashButton.BackgroundColor = new ScanbotColor("#7A000000");
            config.ActionBar.FlashButton.ForegroundColor = new ScanbotColor("#FFFFFF");

            // Configure the active state of the flash button.
            config.ActionBar.FlashButton.ActiveBackgroundColor = new ScanbotColor("#FFCE5C");
            config.ActionBar.FlashButton.ActiveForegroundColor = new ScanbotColor("#000000");

            // Hide/Show the zoom button.
            config.ActionBar.ZoomButton.Visible = true;

            // Configure the inactive state of the zoom button.
            config.ActionBar.ZoomButton.BackgroundColor = new ScanbotColor("#7A000000");
            config.ActionBar.ZoomButton.ForegroundColor = new ScanbotColor("#FFFFFF");
            // Zoom button has no active state - it only switches between zoom levels (for configuring those please refer to camera configuring).

            // Hide/Show the flip camera button.
            config.ActionBar.FlipCameraButton.Visible = true;

            // Configure the inactive state of the flip camera button.
            config.ActionBar.FlipCameraButton.BackgroundColor = new ScanbotColor("#7A000000");
            config.ActionBar.FlipCameraButton.ForegroundColor = new ScanbotColor("#FFFFFF");
            // Flip camera button has no active state - it only switches between front and back camera.

            // Configure other parameters as needed.

            return config;
        }
    }
}