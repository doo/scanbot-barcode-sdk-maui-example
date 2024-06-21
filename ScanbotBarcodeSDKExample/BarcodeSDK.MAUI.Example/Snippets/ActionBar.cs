using ScanbotSDK.MAUI;

namespace ScanbotSDK.MAUI.Example
{
    public partial class Snippets
    {
        public static BarcodeScannerConfiguration ActionBar
        {
            get
            {
                // Create the default configuration object.
                var config = new BarcodeScannerConfiguration();
                
                // Configure the action bar.
                // Hide/unhide the flash button.
                config.ActionBar.FlashButton.Visible = true;

                // Configure the inactive state of the flash button.
                config.ActionBar.FlashButton.BackgroundColor = new ColorValue("#7A000000");
                config.ActionBar.FlashButton.ForegroundColor = new ColorValue("#FFFFFF");

                // Configure the active state of the flash button.
                config.ActionBar.FlashButton.ActiveBackgroundColor = new ColorValue("#FFCE5C");
                config.ActionBar.FlashButton.ActiveForegroundColor = new ColorValue("#000000");

                // Hide/unhide the zoom button.
                config.ActionBar.ZoomButton.Visible = true;

                // Configure the inactive state of the zoom button.
                config.ActionBar.ZoomButton.BackgroundColor = new ColorValue("#7A000000");
                config.ActionBar.ZoomButton.ForegroundColor = new ColorValue("#FFFFFF");
                // Zoom button has no active state - it only switches between zoom levels (for configuring those please refer to camera configuring).

                // Hide/unhide the flip camera button.
                config.ActionBar.FlipCameraButton.Visible = false;

                // Configure the inactive state of the flip camera button.
                config.ActionBar.FlipCameraButton.BackgroundColor = new ColorValue("#7A000000");
                config.ActionBar.FlipCameraButton.ForegroundColor = new ColorValue("#FFFFFF");
                // Flip camera button has no active state - it only switches between front and back camera.

                // Configure other parameters as needed.

                return config;
            }
        }
    }
}