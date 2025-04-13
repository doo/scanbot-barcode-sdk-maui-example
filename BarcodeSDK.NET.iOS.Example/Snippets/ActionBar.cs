using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;
public static partial class Snippets
{
    public static SBSDKUI2BarcodeScannerScreenConfiguration ActionBar
    {
        get
        {
            // Create the default configuration object.
            var config = new SBSDKUI2BarcodeScannerScreenConfiguration();
            
            // Configure the action bar.
            // Hide/show the flash button.
            config.ActionBar.FlashButton.Visible = true;

            // Configure the inactive state of the flash button.
            config.ActionBar.FlashButton.BackgroundColor = new SBSDKUI2Color("#7A000000");
            config.ActionBar.FlashButton.ForegroundColor = new SBSDKUI2Color("#FFFFFF");

            // Configure the active state of the flash button.
            config.ActionBar.FlashButton.ActiveBackgroundColor = new SBSDKUI2Color("#FFCE5C");
            config.ActionBar.FlashButton.ActiveForegroundColor = new SBSDKUI2Color("#000000");

            // Hide/show the zoom button.
            config.ActionBar.ZoomButton.Visible = true;

            // Configure the inactive state of the zoom button.
            config.ActionBar.ZoomButton.BackgroundColor = new SBSDKUI2Color("#7A000000");
            config.ActionBar.ZoomButton.ForegroundColor = new SBSDKUI2Color("#FFFFFF");
            // Zoom button has no active state - it only switches between zoom levels (for configuring those please refer to camera configuring).

            // Hide/show the flip camera button.
            config.ActionBar.FlipCameraButton.Visible = true;

            // Configure the inactive state of the flip camera button.
            config.ActionBar.FlipCameraButton.BackgroundColor = new SBSDKUI2Color("#7A000000");
            config.ActionBar.FlipCameraButton.ForegroundColor = new SBSDKUI2Color("#FFFFFF");
            // Flip camera button has no active state - it only switches between front and back camera.

            // Configure other parameters as needed.
            return config;
        }
    }
}