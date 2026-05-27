using ScanbotSDK.iOS;

namespace BarcodeSDK.NET.iOS;

public static partial class Snippets
{
    public static SBSDKUI2BarcodeScannerScreenConfiguration ViewFinder
    {
        get
        {
            // Create the default configuration object.
            var configuration = new SBSDKUI2BarcodeScannerScreenConfiguration();

            // Show the view finder
            configuration.ViewFinder.Visible = true;

            // Set the aspect ratio of the view finder
            configuration.ViewFinder.AspectRatio =
                new SBSDKAspectRatio(width: 16.0, height: 9.0);

            configuration.ViewFinder.Style = new SBSDKUI2FinderCorneredStyle
            {
                // Set the color of the view finder corners
                StrokeColor = new SBSDKUI2Color("#FF0005"),
                // Set the width of the view finder corners
                StrokeWidth = 5.0
            };

            // Configure other parameters as needed.

            return configuration;
        }
    }
}